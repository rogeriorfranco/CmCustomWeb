using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public class ReportDal : IReportDal
    {
        StringBuilder sql = new StringBuilder();

        public DataTable relatorioBandaOltTotal(string olt)
        {
            try
            {
                sql.Append("SELECT * FROM( ");
                sql.Append("  SELECT ");

                sql.Append("    SUM((SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_NAME = 'BANDA_MB' ");
                sql.Append("    AND ATT.LINK_ID = SUB.CAP_ID)) AS BANDA_MB ");

                sql.Append("    ,SUM((SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_NAME = 'BANDA_UPLINK' ");
                sql.Append("    AND ATT.LINK_ID = SUB.CAP_ID)) AS BANDA_UPLINK ");

                sql.Append("    ,(SELECT C.COMPONENT_NAME ");
                sql.Append("    FROM COMPONENT C ");
                sql.Append("    WHERE C.COMPONENT_ID = SUB.CONT_ID) AS OLT ");

                sql.Append("    FROM( ");

                sql.Append("    SELECT CAP.CAP_ID, ELM.CONT_ID ");
                sql.Append("    FROM CCAP_CARD_CAP CAP ");
                sql.Append("    ,COEL_CONT_ELEM ELM ");
                sql.Append("    WHERE ELM.ELEM_ID = CAP.COMPONENT_ID ");
                sql.Append("    AND CAP.COMPONENT_ID IN ");
                sql.Append("    (SELECT EL.ELEM_ID ");
                sql.Append("    FROM COEL_CONT_ELEM EL ");
                sql.Append("    WHERE EL.CONT_ID IN ");

                sql.Append("      (SELECT CO.COMPONENT_ID FROM COMPONENT CO ");
                sql.Append("       WHERE CO.CATEGORY_ID = 6 ");
                sql.Append("       AND CO.SUBCATEGORYID = 213)) ");

                sql.Append("    )SUB ");

                sql.Append("   GROUP BY SUB.CONT_ID ");
                sql.Append(")SSUB ");

                if (olt != "")
                    sql.Append("WHERE SSUB.OLT LIKE '%" + olt.ToUpper() + "%' ");

                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
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

        public DataTable relatorioGeralFacilidade(string circuito, string cto, string olt, string status)
        {
            try
            {
                sql.Append("SELECT * FROM CMSYS.MVW_FACILITY_FULL ");
                sql.Append("WHERE 1 = 1 ");

                if (!circuito.Equals(""))
                    sql.Append("   AND CIRCUITO = '" + circuito + "' ");

                if (!cto.Equals(""))
                    sql.Append("   AND CTO LIKE '%" + cto + "%' ");

                if (!olt.Equals(""))
                    sql.Append("   AND OLT LIKE '%" + olt + "%' ");

                if (status != "0")
                    sql.Append("AND STATUS = '" + status + "'");


                sql.Append("ORDER BY CTO,PORTA ");

                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
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

        public DataTable getFabricante()
        {
            try
            {
                sql.Append("select distinct FABRICANTE from CMSYS.MVW_FACILITY_FULL where fabricante is not null ");
                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
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

        public DataTable relatorioQuantidadeRede(string tecnologia)
        {
            try
            {
                sql.Append("select fac.LOCALIDADE, ");
                sql.Append("       fac.FABRICANTE tecnologia, ");
                sql.Append("       count(distinct fac.OLT) olt, ");
                sql.Append("       count(distinct fac.CTO) cto, ");
                sql.Append("       count(fac.PORTA) total_portas, ");
                sql.Append("       sum(case when fac.STATUS = 'VAGO' then 1 else 0 end) portas_disponiveis ");
                sql.Append("  from CMSYS.MVW_FACILITY_FULL fac ");

                if (!tecnologia.Equals(""))
                    sql.Append("   where fac.FABRICANTE = '" + tecnologia + "' ");

                sql.Append(" group by fac.LOCALIDADE, fac.FABRICANTE ");
                sql.Append(" order by fac.LOCALIDADE, fac.FABRICANTE ");

                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
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

        public DataTable relatorioHistoricoViabilidade(string status, string circuito, string estado, 
            string localidade, string bairro,string logradouro, string dtInicio, string dtFim, string caixa)
        {
            try
            {
                sql.Append("SELECT DT_REQUISICAO,CIRCUITO,STATUS,MOTIVO,PONTO_DERIVACAO AS CAIXA ");
                sql.Append(",DISTANCIA,ESTADO,LOCALIDADE,BAIRRO,LOGRADOURO,NUMERO,LAT_PONTO_DERIVACAO AS LAT_CAIXA,LONG_PONTO_DERIVACAO AS LONG_CAIXA,LAT_CLIENTE,LONG_CLIENTE, LOC_PROVEDOR, LOC_TIPO, ALGORITMO_SELECAO, COD_LOGRADOURO, DISTANCIA_LINEAR, SISTEMA ");                
                sql.Append("FROM API.HISTORICO_INTEGRACAO ");
                sql.Append("WHERE 1 = 1 ");

                if (!status.Equals("0"))
                    sql.Append("AND STATUS= '" + status + "' ");

                if (!circuito.Equals(""))
                    sql.Append("AND CIRCUITO= '" + circuito + "' ");

                if (!caixa.Equals(""))
                    sql.Append("AND PONTO_DERIVACAO LIKE '%" + caixa + "%' ");

                if (!estado.Equals("0"))
                    sql.Append("AND ESTADO= '" + estado + "' ");

                if (!localidade.Equals(""))
                    sql.Append("AND LOCALIDADE LIKE '%" + localidade + "%' ");

                if (!bairro.Equals(""))
                    sql.Append("AND BAIRRO LIKE '%" + bairro + "%' ");

                if (!logradouro.Equals(""))
                    sql.Append("AND LOGRADOURO LIKE '%" + logradouro + "%' ");

                if (!dtInicio.Equals(""))
                    sql.Append("AND DT_REQUISICAO >= '" + dtInicio + " 00:00:01' ");

                if (!dtInicio.Equals(""))
                    sql.Append("AND DT_REQUISICAO <= '" + dtFim + " 23:59:59' ");

                sql.Append("ORDER BY DT_REQUISICAO ");

                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
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
