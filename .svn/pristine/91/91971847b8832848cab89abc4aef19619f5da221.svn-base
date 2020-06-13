using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDal;
using CmCustomDto;
namespace CmCustomBll
{
    public class UserBll
    {
        IUser userDal = new UserDal();

        public User getSessionActiveCmClient(string username)
        {
            return userDal.getSessionActiveCmClient(username);
        }

        public void disconnectUserCmClient(int id, int serial)
        {
            userDal.disconnectUserCmClient(id, serial);
        }
    }
}
