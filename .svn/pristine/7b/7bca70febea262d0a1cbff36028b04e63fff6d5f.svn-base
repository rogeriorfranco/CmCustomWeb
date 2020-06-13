using CmCustomDal;
using System;
using System.Data;

namespace CmCustomBll
{
    public class ReportBll
    {
        IReportDal relatorioDal = new ReportDal();

        public DataTable relatorioBandaOltTotal(string olt)
        {
            return relatorioDal.relatorioBandaOltTotal(olt);
        }

        public DataTable relatorioGeralFacilidade(string circuito, string cto, string olt, string status)
        {
            try
            {
                return relatorioDal.relatorioGeralFacilidade(circuito, cto, olt, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable getFabricante()
        {
            try
            {
                return relatorioDal.getFabricante();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable relatorioQuantidadeRede(string tecnologia)
        {
            try
            {
                return relatorioDal.relatorioQuantidadeRede(tecnologia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable relatorioHistoricoViabilidade(string status, string circuito, string estado, string localidade, string bairro, 
            string logradouro, string dtInicio, string dtFim, string caixa)
        {
            try
            {
                return relatorioDal.relatorioHistoricoViabilidade(status, circuito, estado, localidade, bairro, logradouro, dtInicio, dtFim, caixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
