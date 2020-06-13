﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDal;
using System.Data;

namespace CmCustomBll
{
    public class LocalityBll
    {
        ILocalityDal localityDal = new LocalityDal();

        public int getCodLocalidadeByIdLocalidade(int idlocalidade)
        {
            return localityDal.getCodLocalidadeByIdLocalidade(idlocalidade);
        }

        public int getCodLogradouroByIdLogradouro(int idLogradouro)
        {
            return localityDal.getCodLogradouroByIdLogradouro(idLogradouro);
        }

        public List<string> getLogradouroByIdLocalidade(int idLocalidade, string logradouro)
        {
            return localityDal.getLogradouroByIdLocalidade(idLocalidade, logradouro);
        }

        public List<string> getLogradouroByLocalidade(string Localidade, string logradouro)
        {
            return localityDal.getLogradouroByLocalidade(Localidade, logradouro);
        }

        public List<string> getBairroByIdLogradouro(int idLogradouro)
        {
            return localityDal.getBairroByIdLogradouro(idLogradouro);
        }

        public List<string> getMunicipioByEstado(string estado, string municipio)
        {
            return localityDal.getMunicipioByEstado(estado, municipio);
        }

        public List<string> getLocalidadeByMunicipio(int idMunicipio, string localidade)
        {
            return localityDal.getLocalidadeByMunicipio(idMunicipio, localidade);
        }
        
    }
}
