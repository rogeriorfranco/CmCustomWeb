using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDto;
using System.Data;

namespace CmCustomDal
{
    public class UserDal : IUser
    {
        StringBuilder sql = new StringBuilder();

        public User getSessionActiveCmClient(string username)
        {
            try
            {
                sql.Append("SELECT DISTINCT VS.SID, VS.SERIAL# ");
                sql.Append("FROM V$SESSION VS ");
                sql.Append("WHERE LOWER(VS.STATUS) <> 'KILLED' ");
                sql.Append("AND LOWER(VS.USERNAME)='" + username + "' ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());

                User user = new User();
                if (dr != null)
                {
                    user.id = Convert.ToInt32(dr["SID"]);
                    user.serial = Convert.ToInt32(dr["SERIAL#"]);
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                sql.Clear();
            }
        }

        public void disconnectUserCmClient(int id, int serial)
        {
            try
            {
                sql.Append("alter system disconnect session '" + id + "," + serial + "' immediate ");
                ConnectionOracleDal.ExecutaSql(sql.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                sql.Clear();
            }
        }
    }
}
