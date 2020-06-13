using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface ILocalityDal
    {
        int getCodLocalidadeByIdLocalidade(int idlocalidade);
        int getCodLogradouroByIdLogradouro(int idLogradouro);
        List<string> getLogradouroByIdLocalidade(int idLocalidade, string logradouro);
        List<string> getLogradouroByLocalidade(string localidade, string logradouro);
        List<string> getBairroByIdLogradouro(int idLogradouro);
        List<string> getMunicipioByEstado(string estado, string municipio);
        List<string> getLocalidadeByMunicipio(int idMunicipio, string localidade);
    }
}
