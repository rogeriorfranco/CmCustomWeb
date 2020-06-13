using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDto;

namespace CmCustomDal
{
    public interface IUser
    {
        User getSessionActiveCmClient(string usuario);
        void disconnectUserCmClient(int id, int serial);
    }
}
