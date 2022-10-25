using System;
using System.Data;
using System.Collections.Generic;


namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public interface IDatabaseConnection
    {
        ConnectionState State { get; }
        string ServerTitle { get; }
        string DatabaseOrSchemaTitle { get; }


        void Test(string server, ConnectionAuthenticationEnum authType, string username, string password);
        void Open(string server, ConnectionAuthenticationEnum authType, string username, string password);
        void Close();
        void ChangeDatabaseOrSchema(string newDatabaseOrSchema);

        IList<string> GetDatabaseNames();
        IList<string> GetAllTableNames();
        IList<DataColumn> GetAllColumns(string tablename);
        IList<DataColumn> GetPrimaryKeyColumns(string tablename);

    }
}
