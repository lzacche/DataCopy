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

        void CreateTable(ITable table);
        void EnsureTableColumns(ITable tableAsItMustBe, IList<IColumn> actualColsOnDestination, out bool tableWasChanged);

        void EnsureTablePrimaryKey(ITable tableAsItMustBe, IList<IColumn> actualColsOnDestination);
        void DropConstraint(string tableName, string constraintName);
        void CreatePrimaryConstraint(string tableName, string constraintName, IList<string> columns);

        void EnsureTableCheckConstraints(ITable tableAsItMustBe);
        void EnsureTableForeignKeys(ITable tableAsItMustBe);


        IList<string> GetDatabaseNames();
        IList<string> GetAllTableNames();
        IList<DataColumn> GetAllColumns(string tablename);
        string GetPrimaryConstraintName(string tablename);
        IList<DataColumn> GetPrimaryKeyColumns(string tablename);



        IList<IColumn> GetAllColumns_NEW(string tablename);
    }
}
