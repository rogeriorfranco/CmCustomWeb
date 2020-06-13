using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public class AcessHistoricDal : CrudDAL<AcessHistoric>, IAcessHistoricDal
    {
        public void insereHistoricoAcesso(AcessHistoric acessHistoric)
        {            
            create(acessHistoric);
        }

        StringBuilder sql = new StringBuilder();
        public DataTable consultaHistorico(string usuario, string dtInicio, string dtFim)
        {
            try
            {
                sql.Append("SELECT USUARIO,PERFIL,TIPO_USUARIO,DT_ULTIMO_ACESSO ");
                sql.Append("FROM API.HISTORICO_ACESSO ");
                sql.Append("WHERE USUARIO IS NOT NULL ");

                if (usuario != "")
                    sql.Append("AND USUARIO LIKE '%" + usuario + "%'");
                if (dtInicio != "")
                    sql.Append("AND DT_ULTIMO_ACESSO >= '" + dtInicio + " 00:00:01' ");
                if (dtInicio != "")
                    sql.Append("AND DT_ULTIMO_ACESSO <= '" + dtFim + " 23:59:59' ");

                sql.Append("ORDER BY DT_ULTIMO_ACESSO DESC ");

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
