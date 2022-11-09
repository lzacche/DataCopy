using System;
using System.Text;
using System.Collections.Generic;
using LeoZacche.DataTools.DataCopy.Contracts.Extensions;

namespace LeoZacche.DataTools.DataCopy.Contracts
{
    public class ConstraintPrimaryKey : IConstraintPrimaryKey
    {
        public string ConstraintName { get; set; }
        public string TableName { get; set; }
        public IList<IColumn> Columns { get; private set; }

        public ConstraintPrimaryKey()
        {
            this.Columns = new List<IColumn>();
        }

        public IConstraintBase Clone()
        {
            var aClone = new ConstraintPrimaryKey()
            {
                ConstraintName = this.ConstraintName,
                TableName = this.TableName,
            };
            aClone.Columns.CloneFrom(this.Columns);

            return aClone;
        }
    }
}
