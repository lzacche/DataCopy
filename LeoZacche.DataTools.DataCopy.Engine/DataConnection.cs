using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts;

namespace LeoZacche.DataTools.DataCopy.Engine
{
    public class DataConnection
    {
        private IDatabaseConnection _realConnection = null;
        private IList<string> _databases = null;
        private IList<string> _tables = null;
        private string _currentTabaseOrSchema = null;



        public DataConnection()
        {
            // valores default para os enums
            this.ConnectionType = ConnectionTypeEnum.MariaDb;
            this.Authentication = ConnectionAuthenticationEnum.WindowsIntegrated;
        }


        private IDatabaseConnection RealConnection
        {
            get
            {
                if (this._realConnection == null)
                    this._realConnection = createConcreteConnection(this.ConnectionType);

                return this._realConnection;
            }
        }

        public ConnectionTypeEnum ConnectionType { get; set; }
        public string Server { get; set; }
        public ConnectionAuthenticationEnum Authentication { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DatabaseOrSchema
        {
            get { return this._currentTabaseOrSchema; }
            set
            {
                var newDatabaseOrSchema = value;
                this.RealConnection.ChangeDatabaseOrSchema(newDatabaseOrSchema);
                this._currentTabaseOrSchema = newDatabaseOrSchema;
            }
        }
        public ConnectionState State
        {
            get
            {
                if (this._realConnection == null)
                    return ConnectionState.Closed;
                else
                    return this._realConnection.State;
            }
        }
        public string ServerTitle
        {
            get
            {
                return this.RealConnection.ServerTitle;
            }
        }
        public string DatabaseOrSchemaTitle
        {
            get
            {
                return this.RealConnection.DatabaseOrSchemaTitle;
            }
        }

        public void Test()
        {
            var tempConn = createConcreteConnection(this.ConnectionType);
            tempConn.Test(this.Server, this.Authentication, this.Username, this.Password);
        }
        public void Open()
        {
            //this._realConnection = createConcreteConnection(this.ConnectionType);
            this.RealConnection.Open(this.Server, this.Authentication, this.Username, this.Password);
        }
        public void Close()
        {
            this._realConnection.Close();
        }



        public IList<string> GetDatabaseNames()
        {
            if (this._databases == null)
                this._databases = this.RealConnection.GetDatabaseNames();

            return this._databases;
        }
        public IList<string> GetAllTableNames()
        {
            if (this._tables == null)
                this._tables = this.RealConnection.GetAllTableNames();

            return this._tables;
        }

        public ITable GetTable(string tablename)
        {
            var table = this.RealConnection.GetTable(tablename, true);
            return table;
        }
        //public string GetPrimaryConstraintName(string tablename)
        //{
        //    var pkName = this.RealConnection.GetPrimaryConstraintName(tablename);

        //    return pkName;
        //}
        //public IList<DataColumn> GetPrimaryKeyColumns(string tablename)
        //{
        //    var columns = this.RealConnection.GetPrimaryKeyColumns(tablename);

        //    return columns;
        //}
        public IList<DataColumn> GetAllColumns(string tablename)
        {
            var columns = this.RealConnection.GetAllColumns(tablename);

            return columns;
        }
        public IList<IColumn> GetAllColumns_NEW(string tablename)
        {
            var columns = this.RealConnection.GetAllColumns_NEW(tablename);

            return columns;
        }

        internal void CreateTable(ITable table)
        {
            this._realConnection.CreateTable(table);

            var cols = table.Columns.Where(c => c.IsPartOfPrimaryKey).Select(c => c.Name).ToList();

            //this._realConnection.CreatePrimaryConstraint(table.Name, table.PrimaryKeyConstraintName, cols);
            this._realConnection.CreatePrimaryKeyConstraint(table.PrimaryKey);

            /*
            foreach (var uk in table.UniqueConstraints)
                this._realConnection.CreateUniqueConstraint(uk);

            foreach (var check in table.CheckConstraints)
                this._realConnection.CreateCheckConstraint(check);
                */

            //foreach (var fk in table.ForeignKeyConstraints)
            //    this._realConnection.CreateForeignKeyConstraint(fk);
        }
        internal void EnsureTableStructure(ITable tableAsItMustBe)
        {
            var actualTable = this._realConnection.GetTable(tableAsItMustBe.Name, true);
            var actualColsOnDestination = GetAllColumns_NEW(tableAsItMustBe.Name);
            bool tableWasChanged;

            this._realConnection.EnsureTableColumns(tableAsItMustBe, actualColsOnDestination, out tableWasChanged);
            if (tableWasChanged)
                actualColsOnDestination = GetAllColumns_NEW(tableAsItMustBe.Name);

            //this._realConnection.EnsurePrimaryKeyConstraint(tableAsItMustBe, actualColsOnDestination);
            this.EnsurePrimaryKeyConstraint(tableAsItMustBe.PrimaryKey, actualTable.PrimaryKey);


            //this._realConnection.EnsureUniqueConstraints(tableAsItMustBe);
            //this._realConnection.EnsureCheckConstraints(tableAsItMustBe);
            //this._realConnection.EnsureForeignKeyConstraints(tableAsItMustBe);
        }
        public void EnsurePrimaryKeyConstraint(IConstraintPrimaryKey pkAsItMustBe, IConstraintPrimaryKey pkActual)
        {
            // verificar o nome, a quantidade de colunas, os nomes das colunas e a ordem das colunas
            bool columnsMatch = true;
            var namesMatch = pkAsItMustBe.ConstraintName == pkActual.ConstraintName;
            var colQtiesMatch = pkAsItMustBe.Columns.Count == pkActual.Columns.Count;

            foreach (var colMustBe in pkAsItMustBe.Columns)
            {
                var colActual = pkActual.Columns.FirstOrDefault(c => c.Name == colMustBe.Name);

                if (colActual == null)
                {
                    columnsMatch = false;
                    break;
                }
                else
                {
                    var colNamesMatch = colMustBe.Name == colActual.Name;
                    var colSequencesMatch = colMustBe.Ordinal == colActual.Ordinal;
                    if (!colNamesMatch || !colSequencesMatch)
                    {
                        columnsMatch = false;
                        break;
                    }
                }
            }


            var rebuildPk = !namesMatch || !colQtiesMatch || !columnsMatch;

            if (rebuildPk)
            {
            
                // dischard actual pk
                try
                {
                    //this.DropConstraint(pkActual.TableName, pkActual.ConstraintName);
                    if (pkActual.ConstraintName != null)
                        this.DropConstraint(pkActual);
                }
#pragma warning disable S108 // Nested blocks of code should not be left empty
                catch (ConstraintDoesNotExistsException) { } // NESTE CASO, se a constraint não existe, tudo bem! Podemos continuar sem problemas. Não precisa nem logar.
#pragma warning restore S108 // Nested blocks of code should not be left empty



                // rebuild pk based on must be
                this._realConnection.CreatePrimaryKeyConstraint(pkAsItMustBe);
                
            }

            /*
                var pkColsMustBe = tableAsItMustBe.Columns.Where(c => c.IsPartOfPrimaryKey).ToList();
                var pkColsActual = actualColsOnDestination.Where(c => c.IsPartOfPrimaryKey).ToList();

                if (pkColsMustBe.Count != pkColsActual.Count){

                }
                */

            /*
            // both collections must have all names must mutch. There cannot be not even one element more on either side.
            var allNamesOnMustBeExistsOnActual = pkColsMustBe.Any(c => pkColsActual.Exists(d => d.Name == c.Name));
            var allNamesOnActualExistsOnMustBe = pkColsActual.Any(c => pkColsMustBe.Exists(d => d.Name == c.Name));
            bool pkMacth = allNamesOnMustBeExistsOnActual && allNamesOnActualExistsOnMustBe;

            if (!pkMacth)
            {
            }
            */
        }


        /*
        internal void EnsureCheckConstraints(ITable tableAsItMustBe, IList<IConstraintCheck> actualConstraints)
        {
            // both collections must have elemensts with same name same condition

            var remainingUndesiredConstraints = actualConstraints.Select(c => c.ConstraintName).ToList();

            foreach (var checkInMustBe in tableAsItMustBe.CheckConstraints)
            {
                var checkInActual = actualConstraints.First(c => c.ConstraintName == checkInMustBe.ConstraintName);

                if (checkInActual == null)
                    this._realConnection.CreateCheckConstraint(checkInMustBe);
                else
                {
                    if (checkInMustBe.Condition != checkInActual.Condition)
                    {
                        this._realConnection.DropConstraint(checkInActual);
                        this._realConnection.CreateCheckConstraint(checkInMustBe);
                    }
                }
            }

            foreach (var checkToDrop in remainingUndesiredConstraints)
            {
                var theConstraint = actualConstraints.First(c => c.ConstraintName == checkToDrop);
                this._realConnection.DropConstraint(theConstraint);
            }
        }
        internal void CreateCheckConstraints(ITable tableAsItMustBe)
        {
            foreach (var checkInMustBe in tableAsItMustBe.CheckConstraints)
            {
                this._realConnection.CreateCheckConstraint(checkInMustBe);
            }
        }
        */
        //private void DropConstraint(string tablename, string constraintName)
        private void DropConstraint(IConstraintBase constraint)
        {
            this._realConnection.DropConstraint(constraint);
        }



        private IDatabaseConnection createConcreteConnection(ConnectionTypeEnum connectionType)
        {
            IDatabaseConnection theConn = null;
            string filename = null;

            switch(connectionType){
                case ConnectionTypeEnum.MicrosoftSqlServer:
                    filename = "LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer.dll";
                    break;

                case ConnectionTypeEnum.OracleDatabaseServer:
                case ConnectionTypeEnum.OracleMySqServer:
                case ConnectionTypeEnum.MariaDb:
                default:
                    throw new NotImplementedException($"Conexões do tipo {connectionType} ainda não estão disponíveis.");
            }

            //var assembly = LoadPlugin(filename);
            //theConn = CreateConnection(assembly);

            string[] pluginPaths = new string[]
            {
                filename
            };

            IEnumerable<IDatabaseConnection> connections = pluginPaths.SelectMany(pluginPath =>
            {
                Assembly pluginAssembly = LoadPlugin(pluginPath);
                return CreateConnections(pluginAssembly);
            }).ToList();

            theConn = connections.First();

            return theConn;
        }


        static Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            //string root = Path.GetFullPath(Path.Combine(
            //    Path.GetDirectoryName(
            //        Path.GetDirectoryName(
            //            Path.GetDirectoryName(
            //                Path.GetDirectoryName(
            //                    Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            //string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            var assemblyDir = AssemblyDirectory;
            string pluginLocation = $@"{assemblyDir}\Plugins\{relativePath}";
#if DEBUG
            var solutionDir = assemblyDir;  //   DataCopy\LeoZacche.DataTools.DataCopy.WindowsApp\bin\Debug\NetCoreApp3.1
            solutionDir = Path.GetDirectoryName(solutionDir); //   DataCopy\LeoZacche.DataTools.DataCopy.WindowsApp\bin\Debug\
            solutionDir = Path.GetDirectoryName(solutionDir); //   DataCopy\LeoZacche.DataTools.DataCopy.WindowsApp\bin\
            solutionDir = Path.GetDirectoryName(solutionDir); //   DataCopy\LeoZacche.DataTools.DataCopy.WindowsApp\
            solutionDir = Path.GetDirectoryName(solutionDir); //   DataCopy\
            pluginLocation = $@"{solutionDir}\Plugins\{relativePath}";
#endif


            Console.WriteLine($"Loading commands from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }


        static IEnumerable<IDatabaseConnection> CreateConnections(Assembly assembly)
        {
            int count = 0;
            var allTypes = assembly.GetTypes().ToList();


            foreach (Type type in allTypes)
            {
                if (typeof(IDatabaseConnection).IsAssignableFrom(type))
                {
                    IDatabaseConnection result = Activator.CreateInstance(type) as IDatabaseConnection;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new TypeLoadException(
                    $"Can't find any type which implements IDatabaseConnection in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }



    }
}