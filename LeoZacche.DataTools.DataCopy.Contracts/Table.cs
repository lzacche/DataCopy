using System;
using System.Text;
using System.Collections.Generic;

using LeoZacche.DataTools.DataCopy.Contracts.Extensions;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class Table : ITable
    {
        public string Name { get; set; }
        public IList<IColumn> Columns { get; private set; }
        public IList<IRow> RowsToCopy { get; private set; }
        public IConstraintPrimaryKey PrimaryKey { get; private set; }
        //public IList<IConstraintUniqueKey> UniqueConstraints { get; private set; }
        //public IList<IConstraintCheck> CheckConstraints { get; private set; }
        //public IList<IConstraintForeignKey> ForeignKeyConstraints { get; private set; }


        public Table()
        {
            this.PrimaryKey = new ConstraintPrimaryKey();
            this.Columns = new List<IColumn>();
            this.RowsToCopy = new List<IRow>();
            //this.UniqueConstraints = new List<IConstraintUniqueKey>();
            //this.CheckConstraints = new List<IConstraintCheck>();
            //this.ForeignKeyConstraints = new List<IConstraintForeignKey>();
        }
        //private Table(IConstraintPrimaryKey primaryKey, IList<IColumn> columns, IList<IRow> rowsToCopy, IList<IConstraintUniqueKey> uniqueConstraints, IList<IConstraintCheck> checkConstraints, IList<IConstraintForeignKey> foreignKeys) : this()
        private Table(IConstraintPrimaryKey primaryKey, IList<IColumn> columns, IList<IRow> rowsToCopy) : this()
        {
            this.PrimaryKey = (IConstraintPrimaryKey)primaryKey.Clone();
            this.Columns.CloneFrom(columns);
            this.RowsToCopy.CloneFrom(rowsToCopy);
            //this.UniqueConstraints.CloneFrom(uniqueConstraints);
            //this.CheckConstraints.CloneFrom(checkConstraints);
            //this.ForeignKeyConstraints.CloneFrom(foreignKeys);
        }

        public ITable Clone()
        {
            //var aClone = new Table(this.PrimaryKey, this.Columns, this.RowsToCopy, this.UniqueConstraints, this.CheckConstraints, this.ForeignKeyConstraints);
            var aClone = new Table(this.PrimaryKey, this.Columns, this.RowsToCopy);
            aClone.Name = this.Name;

            return aClone;
        }
    }
}
