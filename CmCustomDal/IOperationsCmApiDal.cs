using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface IOperationsCmApiDal
    {
        void atualizaPontoCompoment(int compPointID, string circuito, string status);
        void atualizaAtributosPortaOLT(Olt olt);
        Dictionary<int, IEnumerable<int>> ReadAttributePortsIds(int id);
        string getSession();
        int CreateAttributSubPorts(int idComponent);
    }
}
