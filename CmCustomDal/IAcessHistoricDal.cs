using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface IAcessHistoricDal
    {
        void insereHistoricoAcesso(AcessHistoric acessHistoric);
        DataTable consultaHistorico(string usuario, string dtInicio, string dtFim);
    }
}
