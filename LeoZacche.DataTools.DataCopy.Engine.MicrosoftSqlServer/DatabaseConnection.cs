using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using LeoZacche.DataTools.DataCopy.Contracts;
using LeoZacche.Utils;

namespace LeoZacche.DataTools.DataCopy.Engine.MicrosoftSqlServer
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private SqlConnection _conn = null;

        #region IDatabaseConnection

        public ConnectionState State
        {
            get
            {
                return (this._conn == null ? ConnectionState.Closed : this._conn.State);
            }
        }

        public string ServerTitle
        {
            get
            {
                return "Servidor / Instância";
            }
        }

        public string DatabaseOrSchemaTitle
        {
            get
            {
                return "Database";
            }
        }



        public void Test(string server, ConnectionAuthenticationEnum authType, string username, string password)
        {
            using (var tmpConn = new SqlConnection())
            {
                tmpConn.ConnectionString = buildConnectionString(server, authType, username, password);

                try
                {
                    tmpConn.Open();
                    tmpConn.Close();
                }
                catch (Exception ex)
                {
                    var m = ex.Message;
                    throw;
                }
            }
        }

        public void Open(string server, ConnectionAuthenticationEnum authType, string username, string password)
        {
            this._conn = new SqlConnection();
            this._conn.ConnectionString = buildConnectionString(server, authType, username, password);
            this._conn.Open();
        }

        public void Close()
        {
            this._conn.Close();
        }



        public IList<string> GetDatabaseNames()
        {
            IList<string> lista = new List<string>();
            string dbName = null;

            var sql = "select name as DatabaseName from sys.sysdatabases";

            using (var cmd = this._conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    dbName = Convert.ToString(dr["DatabaseName"]);
                    lista.Add(dbName);
                }

                dr.Close();
            }

            return lista;
        }

        public IList<string> GetAllTableNames()
        {
            IList<string> lista = new List<string>();
            string table = null;

            var sql = "select name as tableName from sys.tables";

            using (var cmd = this._conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    table = Convert.ToString(dr["tableName"]);
                    lista.Add(table);
                }

                dr.Close();
            }

            return lista;
        }

        public IList<DataColumn> GetAllColumns(string tablename)
        {
            DataColumn col;
            int colOrder;
            string colName, typename;
            Type colType;

            IList<DataColumn> lista = new List<DataColumn>();

            var sql = @"
select  c.ColOrder, c.Name, --tp.name, c.length,
        case when c.scale is null then concat(tp.name, ' (', c.length, ')')
             when c.xprec <> tp.precision and c.xscale = tp.scale then concat(tp.name, ' (', c.xprec, ')')
             when c.xprec <> tp.precision and c.xscale <> tp.scale then concat(tp.name, ' (', c.xprec, ',', c.xscale, ')')
             else concat(tp.name, '')
        end as DataType
        --,c.* 
from    sys.syscolumns c 
        inner join sys.sysobjects t on t.id = c.id
        inner join sys.types tp on tp.user_type_id = c.xtype
where t.name = @tablename
";

            using (var cmd = this._conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("tablename", tablename));

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    colOrder = TypeUtil.ConvertTo<int>(dr["ColOrder"]);
                    colName = TypeUtil.ConvertTo<string>(dr["Name"]);
                    typename = TypeUtil.ConvertTo<string>(dr["DataType"]);

                    colType = convertoToType(typename);

                    col = new DataColumn(colName, colType);
                    lista.Add(col);
                }

                dr.Close();
            }

            return lista;
        }

        public IList<DataColumn> GetPrimaryKeyColumns(string tablename)
        {
            DataColumn col;
            int columnOrderInKey;
            string tableName, primaryKeyName, columnName, typename;
            Type colType;

            IList<DataColumn> lista = new List<DataColumn>();

            var sql = @"
select  t.name as TableName, pk.name as PrimaryKeyName, ic.key_ordinal as ColumnOrderInKey, c.name as ColumnName,
        case when c.scale is null then concat(tp.name, ' (', sc.length, ')')
             when sc.xprec <> tp.precision and sc.xscale = tp.scale then concat(tp.name, ' (', sc.xprec, ')')
             when sc.xprec <> tp.precision and sc.xscale <> tp.scale then concat(tp.name, ' (', sc.xprec, ',', sc.xscale, ')')
             else concat(tp.name, '')
        end as DataType
from    sys.key_constraints pk
        inner join sys.tables t on t.object_id = pk.parent_object_id
        inner join sys.indexes i on i.name = pk.name
        inner join sys.index_columns ic on ic.object_id = i.object_id and ic.index_id = i.index_id
        inner join sys.columns c on c.object_id = ic.object_id and c.column_id = ic.column_id
        inner join sys.syscolumns sc on sc.id = c.object_id and sc.colid = c.column_id 
        inner join sys.types tp on tp.user_type_id = sc.xtype
where t.name = @tablename
order by ic.key_ordinal
";

            using (var cmd = this._conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add(new SqlParameter("tablename", tablename));

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    tableName = TypeUtil.ConvertTo<string>(dr["TableName"]);
                    primaryKeyName = TypeUtil.ConvertTo<string>(dr["PrimaryKeyName"]);
                    columnName = TypeUtil.ConvertTo<string>(dr["ColumnName"]);
                    columnOrderInKey = TypeUtil.ConvertTo<int>(dr["ColumnOrderInKey"]);
                    typename = TypeUtil.ConvertTo<string>(dr["DataType"]);

                    colType = convertoToType(typename);

                    col = new DataColumn(columnName, colType);
                    lista.Add(col);
                }

                dr.Close();
            }

            return lista;
        }

        public void ChangeDatabaseOrSchema(string newDatabaseOrSchema)
        {
            this._conn.ChangeDatabase(newDatabaseOrSchema);
        }
        #endregion



        internal string buildConnectionString(string server, ConnectionAuthenticationEnum authType, string username, string password)
        {
            var str = "";

            //Database=ESIM;Server=HSVSQL04\ESIM;User ID=UsrEsimVG;Password=tbEM$P#8;Trusted_Connection=False;
            //Database=ESIMSORTEIO;Server=SFDATSQLCL02\ANALYTICS;User ID=usrEsimVgSorteio;Password=faV4?rU!eP;Trusted_Connection=False;

            str += $"Server={server}; ";

            if (authType == ConnectionAuthenticationEnum.WindowsIntegrated)
                str += $"Trusted_Connection=True; ";
            else
            {
                str += $"User ID={username}; ";
                str += $"Password={password}; ";
            }

            return str;
        }

        internal Type convertoToType(string databaseType)
        {
            Type resultado = null;
            int? precision = null, 
                 scale = null;

            var parentesesPosition = databaseType.IndexOf('(');
            var temParenteses = parentesesPosition >= 0;

            var typeTitle = temParenteses ? databaseType.Substring(0, parentesesPosition).Trim() : databaseType;
            if (temParenteses)
            {
                var length = databaseType.Substring(parentesesPosition).Replace("(", "").Replace(")", "");
                var partes = length.Split(new char[] { ',' });
                precision = TypeUtil.ConvertTo<int?>(partes[0]);
                if (partes.Length > 1)
                    scale = TypeUtil.ConvertTo<int?>(partes[1]);
            }

            switch (typeTitle)
            {

                case "smallint":
                    resultado = typeof(short);
                    break;

                case "int":
                    resultado = typeof(int);
                    break;

                case "bigint":
                    resultado = typeof(long);
                    break;

                case "date":
                case "datetime":
                    resultado = typeof(DateTime);
                    break;

                case "varchar":
                    resultado = typeof(string);
                    break;

                case "numeric":
                    if (scale != null && scale > 0)
                        resultado = typeof(decimal);
                    else
                    {
                        if (precision != null && precision > 5)
                            resultado = typeof(long);
                        else
                            resultado = typeof(int);
                    }
                    break;


                default:
                    throw new Exception($"Não sei como tratar o tipo '{databaseType}'.");
            }

            return resultado;
        }

    }
}
