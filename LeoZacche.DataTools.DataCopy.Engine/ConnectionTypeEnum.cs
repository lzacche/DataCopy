using System.ComponentModel;

namespace LeoZacche.DataTools.DataCopy.Engine
{
    public enum ConnectionTypeEnum
    {
        [Description("Microsoft SQL Server")]
        MicrosoftSqlServer,

        [Description("Oracle Database Server")]
        OracleDatabaseServer,

        [Description("Oracle MySQL Server")]
        OracleMySqServer,

        [Description("MariaDB")]
        MariaDb,
    }
}