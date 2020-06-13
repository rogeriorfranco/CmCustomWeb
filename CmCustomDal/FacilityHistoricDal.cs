using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDto;
using System.Data;

namespace CmCustomDal
{
    public class FacilityHistoricDal : CrudDAL<Facility>, IFacilityHistoricDal
    {
        public void save(Facility facility)
        {
            create(facility);
        }


        StringBuilder sql = new StringBuilder();
        public DataTable consultaHistorico(string usuario, string circuito, string vlan, string vlanInner,
            string serial, string shelf, string slot, string porta, string ontId, string ipOlt, string dtInicio, string dtFim)
        {
            try
            {
                sql.Append("SELECT USUARIO,DATA_ALTERACAO,CNL ");
                sql.Append(",LOCALIDADE,CTO,PORTA_CTO,IP_OLT ");
                sql.Append(",SHELF,SLOT,PORTA,CIRCUITO_ANTIGO,CIRCUITO_NOVO ");
                sql.Append(",STATUS_ANTIGO,STATUS_NOVO,CLIENTE_ANTIGO,CLIENTE_NOVO ");
                sql.Append(",PRODUTO_ANTIGO,PRODUTO_NOVO,BANDA_MB_ANTIGO,BANDA_MB_NOVO ");
                sql.Append(",BANDA_UPLINK_ANTIGO,BANDA_UPLINK_NOVO ");
                sql.Append(",ONT_ID_ANTIGO,ONT_ID_NOVO ");
                sql.Append(",SERIAL_ANTIGO,SERIAL_NOVO ");
                sql.Append(",VLAN_INNER_ANTIGO,VLAN_INNER_NOVO ");
                sql.Append(",VLAN_ANTIGO,VLAN_NOVO ");
                sql.Append(",VALIDADE_RESERVA_ANTIGO ");
                sql.Append(",VALIDADE_RESERVA_NOVO ");
                //sql.Append(",TO_CHAR(TO_DATE(VALIDADE_RESERVA_ANTIGO,'DD/MM/YYYY'), 'DD/MM/YYYY') AS VALIDADE_RESERVA_ANTIGO ");
                //sql.Append(",TO_CHAR(TO_DATE(VALIDADE_RESERVA_NOVO,'DD/MM/YYYY'), 'DD/MM/YYYY') AS VALIDADE_RESERVA_NOVO ");
                sql.Append("FROM API.HISTORICO_FACILIDADE ");
                sql.Append("WHERE USUARIO IS NOT NULL ");

                if (usuario != "")
                    sql.Append("AND USUARIO LIKE '%" + usuario + "%'");
                
                if (circuito != "")
                    sql.Append("AND CIRCUITO_ANTIGO = '" + circuito + "'");

                if (vlan != "")
                    sql.Append("AND VLAN_ANTIGO = '" + vlan + "'");

                if (vlanInner != "")
                    sql.Append("AND VLAN_INNER_ANTIGO = '" + vlanInner + "'");

                if (serial != "")
                    sql.Append("AND SERIAL_ANTIGO = '" + serial + "'");

                if (shelf != "")
                    sql.Append("AND SHELF_ANTIGO = '" + shelf + "'");

                if (slot != "")
                    sql.Append("AND SLOT = '" + slot + "'");

                if (porta != "")
                    sql.Append("AND PORTA = '" + porta + "'");

                if (ontId != "")
                    sql.Append("AND ONT_ID = '" + ontId + "'");

                if (ipOlt != "")
                    sql.Append("AND IP_OLT = '" + ipOlt + "'");

                if (dtInicio != "")
                    sql.Append("AND DATA_ALTERACAO >= '" + dtInicio + " 00:00:01' ");
                
                if (dtInicio != "")
                    sql.Append("AND DATA_ALTERACAO <= '" + dtFim + " 23:59:59' ");

                sql.Append("ORDER BY DATA_ALTERACAO DESC ");

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
