using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface IReportDal
    {
        DataTable relatorioBandaOltTotal(string olt);
        DataTable relatorioGeralFacilidade(string circuito, string cto, string olt, string status);
        DataTable getFabricante();
        DataTable relatorioQuantidadeRede(string tecnologia);
        DataTable relatorioHistoricoViabilidade(string status, string circuito, string estado, 
            string localidade, string bairro, string logradouro, string dtInicio, string dtFim, string caixa);
    }
}
