﻿using System;
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
        public IList<DataColumn> GetPrimaryKeyColumns(string tablename)
        {
            var columns = this.RealConnection.GetPrimaryKeyColumns(tablename);

            return columns;
        }
        //public DataConnection Clone()
        //{
        //    var clone = new DataConnection
        //    {
        //        ConnectionType = this.ConnectionType,
        //        Username = this.Username,
        //        Password = this.Password,
        //        DatabaseOrSchema = this.DatabaseOrSchema
        //    };

        //    // os clones não vão compartilhar a mesma conexão, serão duas conexões distintas (uma para o objeto original outra para o clone), cada instancia com a sua

        //    return clone;
        //}

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
            string pluginLocation = $@"C:\lz\lz-super-tools\LeoZacche.DataTools.DataCopy\Plugins\{relativePath}";


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
                throw new ApplicationException(
                    $"Can't find any type which implements IDatabaseConnection in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }

    }
}