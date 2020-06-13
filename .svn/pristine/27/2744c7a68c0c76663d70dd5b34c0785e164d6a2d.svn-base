using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CmCustomDto;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;


namespace CmCustomDal
{
   // [DbConfigurationType(typeof(OracleDbConfig))]
    public class CmContext : DbContext
    {
        public CmContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {            
        }


        public DbSet<Facility> Facility { get; set; }
        public DbSet<AcessHistoric> AcessHistoric { get; set; }

    }
    #region OracleDbConfig
    public class OracleDbConfig : DbConfiguration
    {
        public OracleDbConfig()
        {
            SetDefaultConnectionFactory(new OracleConnectionFactory());
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
            SetProviderFactory("Oracle.ManagedDataAccess.Client", new OracleClientFactory());
        }
    }
    #endregion
}
