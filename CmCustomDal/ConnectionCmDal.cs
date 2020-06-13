using DDesign.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
   public class ConnectionCmDal
    {
        public static ConnectMasterAPI getConnectionCm()
        {
            ConnectMasterAPI cmAPI = new ConnectMasterAPI();
            cmAPI.Database.Connect(LanguageType.English, ConfigurationManager.AppSettings["dataSource"], ConfigurationManager.AppSettings["userID"], ConfigurationManager.AppSettings["password"]);
            return cmAPI;
        }
    }
}
