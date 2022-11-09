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


        ITable GetTable(string tablename, bool includePrimaryKey);
        void CreateTable(ITable table);
        void EnsureTableColumns(ITable tableAsItMustBe, IList<IColumn> actualColsOnDestination, out bool tableWasChanged);


        void DropConstraint(IConstraintBase constraint);


        //void EnsurePrimaryKeyConstraint(ITable tableAsItMustBe, IList<IColumn> actualColsOnDestination);
        //void DropConstraint(string tableName, string constraintName);
        //void CreatePrimaryConstraint(string tableName, string constraintName, IList<string> columns);
        void CreatePrimaryKeyConstraint(IConstraintPrimaryKey primaryKey);
        //void DropPrimaryKeyConstraint(IConstraintPrimaryKey primaryKey);


        //void EnsureCheckConstraints(ITable tableAsItMustBe, IList<IConstraintCheck> actualConstraints);
        //void CreateCheckConstraints(ITable tableAsItMustBe);
        /* void CreateCheckConstraint(IConstraintCheck checkConstraint); */
        //void DropCheckConstraint(IConstraintCheck checkConstraint);



        //void EnsureUniqueConstraints(ITable tableAsItMustBe, IList<IConstraintUniqueKey> actualConstraints);
        //void CreateUniqueConstraints(ITable tableAsItMustBe);
        
        /*
        void CreateUniqueConstraint(IConstraintUniqueKey uniqueKeyConstraint);
        void DropUniqueConstraint(IConstraintUniqueKey uniqueKeyConstraint);
        */

        /*
        void EnsureForeignKeyConstraints(ITable tableAsItMustBe, IList<IConstraintForeignKey> actualConstraints);
        void CreateForeignKeyConstraints(ITable tableAsItMustBe);
        */


        IList<string> GetDatabaseNames();
        IList<string> GetAllTableNames();
        IList<DataColumn> GetAllColumns(string tablename);
        IConstraintPrimaryKey GetPrimaryKey(string tablename);



        IList<IColumn> GetAllColumns_NEW(string tablename);
    }
}
