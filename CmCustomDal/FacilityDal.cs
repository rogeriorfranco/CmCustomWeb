﻿using CmCustomApiDto;
using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpKml.Dom;
using SharpKml.Base;
using System.Globalization;

namespace CmCustomDal
{
    public class FacilityDal : IFacilityDal
    {
        StringBuilder sql = new StringBuilder();

        public DataTable consultaFacilidades(string circuito, string estacao, string status, string cnl, string serial, string cto,
            string olt, string ipOlt, string cto_antigo, string outerVlan, string innerVlan)
        {
            try
            {
                //sql.Append("SELECT * ");
                //sql.Append("   FROM ");
                //sql.Append("   (SELECT ROWNUM RNUM, PAGE.* ");
                //sql.Append("      FROM ( ");

                sql.Append("SELECT * FROM ( ");
                sql.Append("  SELECT B.* ");

                sql.Append("    ,(SELECT CFL.FULLNAME ");
                sql.Append("    FROM COMPONENTFULLNAME CFL ");
                sql.Append("    WHERE CFL.COMPONENTID = COMPONENT_ID_PORTA_OLT) AS FULLNAMEOLT ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 88 ");
                sql.Append("    AND ATT.LINK_TYPE = 33 ");
                sql.Append("    AND ATT.LINK_ID = ");
                sql.Append("    (SELECT C.COMPONENT_TYPE_ID ");
                sql.Append("    FROM COMPONENT C ");
                sql.Append("    WHERE C.COMPONENT_ID = ID_OLT)) AS FABRICANTE ");

                sql.Append("    ,(SELECT T.COMPONENT_TYPE_NAME ");
                sql.Append("    FROM COMPONENT_TYPE T ");
                sql.Append("    ,COMPONENT C ");
                sql.Append("    WHERE C.COMPONENT_TYPE_ID = T.COMPONENT_TYPE_ID ");
                sql.Append("    AND C.COMPONENT_ID = ID_OLT) AS MODELO ");

                sql.Append("    ,(SELECT CON.COMPONENT_NAME FROM COMPONENT CON ");
                sql.Append("    WHERE CON.COMPONENT_ID = ID_OLT) AS OLT ");

                sql.Append("    ,(SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 216 ");
                sql.Append("    AND ATC.LINK_TYPE = 31 ");
                sql.Append("    AND ATC.LINK_ID = ID_OLT) AS NOME_OLT_GERENCIA ");

                sql.Append("    ,(SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 274 ");
                sql.Append("    AND ATC.LINK_TYPE = 31 ");
                sql.Append("    AND ATC.LINK_ID = ID_OLT) AS TIPO_VLAN_BANDA_LARGA ");

                sql.Append("    ,(SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 220 ");
                sql.Append("    AND ATC.LINK_TYPE = 31 ");
                sql.Append("    AND ATC.LINK_ID = ID_OLT) AS IP_DADOS_OLT ");

                sql.Append("    ,(SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 235 ");
                sql.Append("    AND ATC.LINK_TYPE = 31 ");
                sql.Append("    AND ATC.LINK_ID = ID_OLT) AS VLAN_BANDA_LARGA ");

                sql.Append("    ,(SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 240 ");
                sql.Append("    AND ATC.LINK_TYPE = 31 ");
                sql.Append("    AND ATC.LINK_ID = ID_OLT) AS VLAN_VOZ_VOBB ");

                sql.Append("    ,(SELECT A.VAL_INT ");
                sql.Append("    FROM A_ATTR A ");
                sql.Append("    ,ARD_ATTR_RED RED ");
                sql.Append("    WHERE A.A_ID = RED.A_ID ");
                sql.Append("    AND RED.AT_ID = 142 ");
                sql.Append("    AND RED.LINK_TYPE = 209 ");
                sql.Append("    AND RED.LINK_ID = ID_SUB_PORTA) AS ID_LOGRADOURO ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 231 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS PRODUTO ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 226 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS CLIENTE ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 227 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS BANDA_MB ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 230 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS SERIAL ");

                sql.Append("    ,(SELECT REGEXP_SUBSTR(ATT.A_VALUE, '[^:]+', 1, 1) ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 229 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS OUTER_VLAN ");

                sql.Append("    ,(SELECT REGEXP_SUBSTR(ATT.A_VALUE, '[^:]+', 1, 2) ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.AT_ID = 229 ");
                sql.Append("    AND ATT.LINK_TYPE = 209 ");
                sql.Append("    AND ATT.LINK_ID = ID_SUB_PORTA) AS INNER_VLAN ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 249 ");
                sql.Append("  AND ATC.LINK_TYPE = 209 ");
                sql.Append("  AND ATC.LINK_ID = ID_SUB_PORTA) AS ONT_ID ");

                sql.Append("FROM ( ");

                sql.Append("  SELECT A.* ");

                sql.Append("  ,CASE WHEN ");
                sql.Append("    (SELECT CT.SUBCATEGORYID ");
                sql.Append("    FROM COMPONENT CT WHERE CT.COMPONENT_ID = ");
                sql.Append("      (SELECT EL.CONT_ID ");
                sql.Append("      FROM COEL_CONT_ELEM EL ");
                sql.Append("      WHERE EL.ELEM_ID = COMPONENT_ID_PORTA_OLT)) = 213 ");
                sql.Append("   THEN ");
                sql.Append("    (SELECT EL.CONT_ID ");
                sql.Append("    FROM COEL_CONT_ELEM EL ");
                sql.Append("    WHERE EL.ELEM_ID = COMPONENT_ID_PORTA_OLT) ");
                sql.Append("   ELSE ");
                sql.Append("      (SELECT E.CONT_ID ");
                sql.Append("    FROM COEL_CONT_ELEM E ");
                sql.Append("    WHERE E.ELEM_ID = ");
                sql.Append("      (SELECT ELS.CONT_ID ");
                sql.Append("      FROM COEL_CONT_ELEM ELS ");
                sql.Append("      WHERE ELS.ELEM_ID = COMPONENT_ID_PORTA_OLT)) ");
                sql.Append("  END AS ID_OLT ");

                sql.Append("     ,(CASE CIRCUITO WHEN ' ' THEN NULL ELSE ");
                sql.Append("    (SELECT CAP.CAP_ID ");
                sql.Append("    FROM CCAP_CARD_CAP CAP ");
                sql.Append("    WHERE CAP.COMPONENT_ID = COMPONENT_ID_PORTA_OLT ");

                sql.Append("    AND (SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 224 ");
                sql.Append("    AND ATC.LINK_TYPE = 209 ");
                sql.Append("    AND ATC.LINK_ID = CAP.CAP_ID) = STATUS ");

                sql.Append("    AND (SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_ID = 245 ");
                sql.Append("    AND ATC.LINK_TYPE = 209 ");
                sql.Append("    AND ATC.LINK_ID = CAP.CAP_ID) = CIRCUITO ");
                sql.Append("    AND ROWNUM = 1 ");
                sql.Append("    )END) AS ID_SUB_PORTA ");

                sql.Append("    FROM( ");
                sql.Append("      SELECT C.COMPONENT_ID,C.DESIGNATION_ID,CP.POINT_ID,CP.POINT_ORDER,DES.TEXT ");

                sql.Append("    ,(SELECT ATT.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATT ");
                sql.Append("    WHERE ATT.LINK_TYPE = 31 ");
                sql.Append("    AND ATT.AT_ID = 195 ");
                sql.Append("    AND ATT.LINK_ID =  ");
                sql.Append("    (SELECT EL.CONT_ID ");
                sql.Append("    FROM COEL_CONT_ELEM EL ");
                sql.Append("    WHERE EL.ELEM_ID = C.COMPONENT_ID)) AS NOME_ANTIGO_CTO  ");

                sql.Append("      ,(SELECT PD.DESCRIPTION ");
                sql.Append("      FROM PD_POINT_DESCRIPTION PD ");
                sql.Append("      WHERE PD.POINT_ID = CP.POINT_ID) AS CIRCUITO ");

                sql.Append("      ,(SELECT ST.NAME FROM STY_STATETYPE ST ");
                sql.Append("      WHERE ST.STATETYPE_ID = ");
                sql.Append("      (NVL((SELECT OS.STATETYPE_ID FROM OST_OBJ_STATE OS WHERE OS.OBJECT_ID = CP.POINT_ID AND OS.STATETYPE_ID IN (34,37,30,31,32,33,38) AND ROWNUM = 1), 34))) AS STATUS ");

                sql.Append("     ,(SELECT D.DESCRIPTION FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
                sql.Append("      (SELECT D.PARENT_ID ");
                sql.Append("      FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
                sql.Append("     (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
                sql.Append("      WHERE D.DESIGNATION_ID = ");
                sql.Append("      (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
                sql.Append("     WHERE D.DESIGNATION_ID = C.DESIGNATION_ID)))) AS LOCALIDADE ");

                sql.Append("      ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 5) ");
                sql.Append("      FROM DFN_DESIG_FULLNAME DEF ");
                sql.Append("      WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS ESTACAO ");

                sql.Append("      ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 4) ");
                sql.Append("      FROM DFN_DESIG_FULLNAME DEF ");
                sql.Append("      WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS CNL ");

                sql.Append("      ,(SELECT PHE.COMP_ID ");
                sql.Append("      FROM PHE_PHGR_ENDS PHE ");
                sql.Append("      WHERE PHE.PHGR_ID = ( ");
                sql.Append("      SELECT CON.PHGR_ID FROM CONNECTION CON ");
                sql.Append("      WHERE CON.COMPONENT_ID_A = C.COMPONENT_ID ");
                sql.Append("      AND CON.POINT_ID_A = CP.POINT_ID) ");
                sql.Append("      AND PHE.CATEGORY_ID = 7) AS COMPONENT_ID_PORTA_OLT ");

                sql.Append("      FROM COMPONENT_POINT CP ");
                sql.Append("      ,COMPONENT C ");
                sql.Append("      ,DES_DESIGNATION DES ");
                sql.Append("      WHERE CP.COMPONENT_ID = C.COMPONENT_ID ");
                sql.Append("      AND DES.DESIGNATION_ID = C.DESIGNATION_ID ");
                sql.Append("      AND DES.DESIG_KIND_ID = 271 ");
                sql.Append("      AND CP.SIDE = 'L' ");
                sql.Append("      AND DES.DSY_ID = 10 ");
                sql.Append("      AND DES.LEVEL_NO = 7 ");
                sql.Append("      AND DES.DESIGLEVELID = 37 ");
                sql.Append("      AND C.SUBCATEGORYID = 283 ");

                if (circuito != "")
                {
                    sql.Append("      AND (SELECT PD.DESCRIPTION ");
                    sql.Append("      FROM PD_POINT_DESCRIPTION PD ");
                    sql.Append("      WHERE PD.POINT_ID = CP.POINT_ID)  = '" + circuito + "' ");
                }

                if (cto_antigo != "")
                {
                    sql.Append("AND (SELECT ATT.A_VALUE ");
                    sql.Append("FROM ARD_ATTR_RED ATT ");
                    sql.Append("WHERE ATT.LINK_TYPE = 31 ");
                    sql.Append("AND ATT.AT_ID = 195 ");
                    sql.Append("AND ATT.LINK_ID = ");
                    sql.Append("(SELECT EL.CONT_ID ");
                    sql.Append("FROM COEL_CONT_ELEM EL ");
                    sql.Append("WHERE EL.ELEM_ID = C.COMPONENT_ID AND ROWNUM = 1)) LIKE '%" + cto_antigo + "%' ");
                }

                sql.Append("      ORDER BY CP.POINT_ID ");
                sql.Append("   )A ");
                sql.Append(" )B ");
                sql.Append(")C ");
                sql.Append("WHERE COMPONENT_ID IS NOT NULL ");

                if (status != "0")
                    sql.Append("AND STATUS = '" + status + "'");

                if (estacao != "")
                    sql.Append("AND ESTACAO LIKE '%" + estacao.ToUpper() + "%'");

                if (cnl != "")
                    sql.Append("AND CNL LIKE '%" + cnl.ToUpper() + "%'");

                if (serial != "")
                    sql.Append("AND SERIAL LIKE '%" + serial + "%'");

                if (cto != "")
                    sql.Append("AND TEXT LIKE '%" + cto.ToUpper() + "%'");

                if (olt != "")
                    sql.Append("AND OLT LIKE '%" + olt.ToUpper() + "%'");

                if (ipOlt != "")
                    sql.Append("AND IP_DADOS_OLT LIKE '%" + ipOlt + "%'");

                if (outerVlan != "")
                    sql.Append("AND OUTER_VLAN = '" + outerVlan + "'");

                if (innerVlan != "")
                    sql.Append("AND INNER_VLAN = '" + innerVlan + "'");

                sql.Append("      AND ROWNUM <= 128 ");
                //sql.Append("    ) PAGE ");
                //sql.Append("  WHERE ROWNUM <= " + indexPage * 10 + ") ");
                //sql.Append(" WHERE RNUM >= " + (indexPage - 1) * 10 + "  ");

                return ConnectionOracleDal.ExecutaSqlRetorno(sql.ToString()).Tables[0];
            }
            finally
            {
                sql.Clear();
            }
        }

        //public DataRow getFacilityCTOByIdPonto(int idPonto)
        //{
        //    try
        //    {
        //        sql.Append("SELECT ");

        //        sql.Append("  (SELECT DES.TEXT FROM DES_DESIGNATION DES ");
        //        sql.Append("  WHERE DES.DESIGNATION_ID = C.DESIGNATION_ID) AS CTO ");

        //        sql.Append("  ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 5) ");
        //        sql.Append("  FROM DFN_DESIG_FULLNAME DEF ");
        //        sql.Append("  WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS ESTACAO ");

        //        sql.Append("  ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 4) ");
        //        sql.Append("  FROM DFN_DESIG_FULLNAME DEF ");
        //        sql.Append("  WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS CNL ");
        //        sql.Append("  ,CP.POINT_NAME ");

        //        sql.Append("  ,(SELECT D.DESCRIPTION FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
        //        sql.Append("  (SELECT D.PARENT_ID ");
        //        sql.Append("  FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
        //        sql.Append("  (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
        //        sql.Append("  WHERE D.DESIGNATION_ID = ");
        //        sql.Append("  (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
        //        sql.Append("  WHERE D.DESIGNATION_ID = C.DESIGNATION_ID)))) AS LOCALIDADE ");

        //        sql.Append("  ,(SELECT ST.NAME ");
        //        sql.Append("  FROM OST_OBJ_STATE OS ");
        //        sql.Append("  ,STY_STATETYPE ST ");
        //        sql.Append("  WHERE ST.STATETYPE_ID = OS.STATETYPE_ID ");
        //        sql.Append("  AND OS.OBJECT_ID = CP.POINT_ID) AS STATUS ");

        //        sql.Append("  ,(SELECT PD.DESCRIPTION ");
        //        sql.Append("  FROM PD_POINT_DESCRIPTION PD ");
        //        sql.Append("  WHERE PD.POINT_ID = CP.POINT_ID) AS CIRCUITO ");

        //        sql.Append("FROM COMPONENT_POINT CP ");
        //        sql.Append(",COMPONENT C ");
        //        sql.Append("WHERE C.COMPONENT_ID = CP.COMPONENT_ID ");
        //        sql.Append("AND CP.POINT_ID = " + idPonto + " ");

        //        return ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message, ex.InnerException);
        //    }
        //    finally
        //    {
        //        sql.Clear();
        //    }
        //}

        public DataRow getFacilityOLTByIdSubPorta(int idSubporta)
        {
            try
            {
                sql.Append("SELECT ");

                sql.Append("  (SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 228 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS BANDA_UPLINK ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 249 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS ONT_ID ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 229 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS VLAN_OUTER_INNER ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 244 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS VALIDADE_RESERVA ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 236 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS BAIRRO ");

                sql.Append(",(SELECT REGEXP_SUBSTR(D.TEXT, '[^(]+', 1, 1) ");
                sql.Append("FROM DES_DESIGNATION D ");
                sql.Append("WHERE D.DESIGNATION_ID = ");
                sql.Append("(SELECT A.VAL_INT ");
                sql.Append("FROM ARD_ATTR_RED RED ");
                sql.Append(",A_ATTR A ");
                sql.Append("WHERE A.A_ID = RED.A_ID ");
                sql.Append("AND RED.AT_ID = 142 ");
                sql.Append("AND RED.LINK_TYPE = 209 ");
                sql.Append("AND RED.LINK_ID =  CAP.CAP_ID)) AS LOGRADOURO ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 143 ");
                sql.Append("  AND ATC.LINK_TYPE = 209 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) AS NUMERO_LOTE ");

                sql.Append(",(SELECT ATC.A_VALUE ");
                sql.Append("FROM ARD_ATTR_RED ATC ");
                sql.Append("WHERE ATC.AT_ID = 272 ");
                sql.Append("AND ATC.LINK_TYPE = 209 ");
                sql.Append("AND ATC.LINK_ID = CAP.CAP_ID) AS MODELO_ONT ");

                sql.Append(",(SELECT ATC.A_VALUE ");
                sql.Append("FROM ARD_ATTR_RED ATC ");
                sql.Append("WHERE ATC.AT_ID = 129 ");
                sql.Append("AND ATC.LINK_TYPE = 209 ");
                sql.Append("AND ATC.LINK_ID = CAP.CAP_ID) AS OBSERVACAO ");

                sql.Append("FROM CCAP_CARD_CAP CAP ");
                sql.Append("WHERE CAP.CAP_ID = " + idSubporta + " ");

                return ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
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

        public int getIdStatusCompomentPoint(string status)
        {
            try
            {
                sql.Append("SELECT STATETYPE_ID FROM STY_STATETYPE ");
                sql.Append("WHERE NAME = '" + status + "'");
                return Convert.ToInt32(ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString())["STATETYPE_ID"]);
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

        public int getTotalSubPortaVagoByPorta(int idporta)
        {
            try
            {
                sql.Append("SELECT COUNT(CAP.CAP_ID) AS TOTAL_VAGO ");
                sql.Append("FROM CCAP_CARD_CAP CAP ");
                sql.Append("  WHERE CAP.COMPONENT_ID = " + idporta + " ");
                sql.Append("AND (SELECT ATC.A_VALUE ");
                sql.Append("FROM ARD_ATTR_RED ATC ");
                sql.Append("WHERE ATC.AT_ID = 224 ");
                sql.Append("AND ATC.LINK_TYPE = 209 ");
                sql.Append("AND ATC.LINK_ID = CAP.CAP_ID) = 'VAGO' ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
                return Convert.ToInt32(dr["TOTAL_VAGO"]);
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

        public int getIdSubPortaVagoByPorta(int idporta)
        {
            try
            {
                sql.Append("SELECT * FROM( ");
                sql.Append("  SELECT CAP.CAP_ID ");
                sql.Append("  FROM CCAP_CARD_CAP CAP ");
                sql.Append("  WHERE CAP.COMPONENT_ID = " + idporta + " ");
                sql.Append("  AND (SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_ID = 224 ");
                sql.Append("  AND ATC.LINK_TYPE = 209 ");
                sql.Append("  AND ATC.LINK_ID = CAP.CAP_ID) = 'VAGO' ");

                sql.Append("  AND (SELECT ATT.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATT ");
                sql.Append("  WHERE ATT.AT_ID = 229 ");
                sql.Append("  AND ATT.LINK_TYPE = 209 ");
                sql.Append("  AND ATT.LINK_ID = CAP.CAP_ID) <> ' ' ");

                sql.Append("  ORDER BY DBMS_RANDOM.VALUE ");
                sql.Append(") ");
                sql.Append("WHERE ROWNUM = 1 ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
                return (dr != null ? Convert.ToInt32(dr["CAP_ID"]) : 0);
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

        public DataTable getFacilitysRedesignate(string cto, string ip, int pointId)
        {
            try
            {
                sql.Append("SELECT POINT_ID,ID_SUB_PORTA,ID_OLT, ");
                sql.Append("CONCAT(CONCAT(CONCAT(CONCAT(IP_DADOS_OLT,'/'),SLOTNAME),' | '),CONCAT(CONCAT(CTO,'/'),POINT_ORDER)) AS FACILITY ");
                sql.Append("FROM( ");

                sql.Append("SELECT POINT_ID,CTO,POINT_ORDER,ID_SUB_PORTA,ID_OLT ");
                sql.Append("  ,CASE WHEN LENGTH(REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1)) = 5 ");
                sql.Append("  THEN REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1) ");
                sql.Append("  ELSE ");
                sql.Append("  CONCAT(CONCAT(REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1), '/'), ");
                sql.Append("  REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 3), '[^<]+', 1, 1)) ");
                sql.Append("  END AS SLOTNAME ");

                sql.Append("  ,(SELECT ATC.A_VALUE ");
                sql.Append("  FROM ARD_ATTR_RED ATC ");
                sql.Append("  WHERE ATC.AT_NAME ='IP_DADOS_OLT' ");
                sql.Append("  AND ATC.LINK_ID = ID_OLT) AS IP_DADOS_OLT ");

                sql.Append("FROM( ");
                sql.Append("  SELECT POINT_ID,CTO,POINT_ORDER ");

                sql.Append("    ,(SELECT CAP.CAP_ID ");
                sql.Append("  FROM CCAP_CARD_CAP CAP ");
                sql.Append("  WHERE (SELECT ATC.A_VALUE ");
                sql.Append("    FROM ARD_ATTR_RED ATC ");
                sql.Append("    WHERE ATC.AT_NAME = 'STATUS' ");
                sql.Append("    AND ATC.LINK_ID = CAP.CAP_ID) = 'VAGO' ");
                sql.Append("  AND CAP.COMPONENT_ID = COMPONENT_ID_PORTA_OLT ");
                sql.Append("  AND ROWNUM = 1) AS ID_SUB_PORTA ");

                sql.Append("    ,(SELECT CFL.FULLNAME ");
                sql.Append("    FROM COMPONENTFULLNAME CFL ");
                sql.Append("    WHERE CFL.COMPONENTID = COMPONENT_ID_PORTA_OLT) AS FULLNAMEOLT ");

                sql.Append("    ,CASE WHEN ");
                sql.Append("      (SELECT CT.SUBCATEGORYID ");
                sql.Append("      FROM COMPONENT CT WHERE CT.COMPONENT_ID = ");
                sql.Append("        (SELECT EL.CONT_ID ");
                sql.Append("        FROM COEL_CONT_ELEM EL ");
                sql.Append("        WHERE EL.ELEM_ID = COMPONENT_ID_PORTA_OLT)) = 213 ");
                sql.Append("     THEN ");
                sql.Append("      (SELECT EL.CONT_ID ");
                sql.Append("      FROM COEL_CONT_ELEM EL ");
                sql.Append("      WHERE EL.ELEM_ID = COMPONENT_ID_PORTA_OLT) ");
                sql.Append("     ELSE ");
                sql.Append("        (SELECT E.CONT_ID ");
                sql.Append("      FROM COEL_CONT_ELEM E ");
                sql.Append("      WHERE E.ELEM_ID = ");
                sql.Append("        (SELECT ELS.CONT_ID ");
                sql.Append("        FROM COEL_CONT_ELEM ELS ");
                sql.Append("        WHERE ELS.ELEM_ID = COMPONENT_ID_PORTA_OLT)) ");
                sql.Append("    END AS ID_OLT ");

                sql.Append("  FROM( ");
                sql.Append("    SELECT POINT_ID,COMPONENT_ID_PORTA_OLT,CTO,POINT_ORDER FROM( ");
                sql.Append("      SELECT CP.POINT_ID,CP.POINT_ORDER,DES.TEXT AS CTO ");

                sql.Append("      ,(SELECT ST.NAME FROM STY_STATETYPE ST ");
                sql.Append("      WHERE ST.STATETYPE_ID = ");
                sql.Append("      (NVL((SELECT OS.STATETYPE_ID FROM OST_OBJ_STATE OS WHERE OS.OBJECT_ID = CP.POINT_ID AND OS.STATETYPE_ID IN (34,37,30,31,32,33) AND ROWNUM = 1), 34))) AS STATUS_CIRCUITO ");

                sql.Append("      ,(SELECT PHE.COMP_ID ");
                sql.Append("      FROM PHE_PHGR_ENDS PHE ");
                sql.Append("      WHERE PHE.PHGR_ID = ( ");
                sql.Append("      SELECT CON.PHGR_ID FROM CONNECTION CON ");
                sql.Append("      WHERE CON.COMPONENT_ID_A = C.COMPONENT_ID ");
                sql.Append("      AND CON.POINT_ID_A = CP.POINT_ID) ");
                sql.Append("      AND PHE.CATEGORY_ID = 7) AS COMPONENT_ID_PORTA_OLT ");

                sql.Append("      FROM COMPONENT_POINT CP ");
                sql.Append("      ,COMPONENT C ");
                sql.Append("      ,DES_DESIGNATION DES ");
                sql.Append("      WHERE CP.COMPONENT_ID = C.COMPONENT_ID ");
                sql.Append("      AND DES.DESIGNATION_ID = C.DESIGNATION_ID ");
                sql.Append("      AND DES.DESIG_KIND_ID = 271 ");
                sql.Append("      AND CP.SIDE = 'L' ");
                sql.Append("      AND DES.DSY_ID = 10 ");
                sql.Append("      AND DES.LEVEL_NO = 7 ");
                sql.Append("      AND DES.DESIGLEVELID = 37 ");
                sql.Append("      AND C.SUBCATEGORYID = 283 ");
                sql.Append("      ORDER BY CP.POINT_ID ");
                sql.Append("    )  ");
                sql.Append("    WHERE STATUS_CIRCUITO = 'VAGO' ");
                sql.Append("   ) ");
                sql.Append(" )  ");
                sql.Append(")  ");

                sql.Append("   WHERE POINT_ID != " + pointId + " ");

                if (!ip.Equals(""))
                    sql.Append("  AND IP_DADOS_OLT = '" + ip.Trim() + "' ");

                if (!cto.Equals(""))
                    sql.Append("   AND CTO LIKE '%" + cto.Trim().ToUpper() + "%' ");

                sql.Append("AND (SELECT ATT.A_VALUE ");
                sql.Append("FROM ARD_ATTR_RED ATT ");
                sql.Append("WHERE ATT.AT_ID = 229 ");
                sql.Append("AND ATT.LINK_TYPE = 209 ");
                sql.Append("AND ATT.LINK_ID = ID_SUB_PORTA) <> ' ' ");

                sql.Append("  AND ROWNUM <= 128 ");

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

        public Olt getOLTAttributsByIdSubPorta(int idSubporta, int id_olt = 0)
        {
            try
            {
                sql.Append("SELECT ");

                sql.Append(" (SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='STATUS' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS STATUS ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='CIRCUITO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS CIRCUITO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='VALIDADE_RESERVA' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS VALIDADE_RESERVA ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='CLIENTE' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS CLIENTE ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='BANDA_MB' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS BANDA_MB ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='OBSERVACAO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS OBSERVACAO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='BANDA_UPLINK' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS BANDA_UPLINK ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='SERIAL_ONT' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS SERIAL_ONT ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='PRODUTO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS PRODUTO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='BAIRRO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS BAIRRO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='NUMERO_LOTE' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS NUMERO_LOTE ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='ONT_ID' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS ONT_ID ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='MODELO_ONT' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS MODELO_ONT ");

                sql.Append(" ,(SELECT A.VAL_INT ");
                sql.Append(" FROM A_ATTR A ");
                sql.Append(" ,ARD_ATTR_RED RED ");
                sql.Append(" WHERE A.A_ID = RED.A_ID ");
                sql.Append(" AND RED.AT_ID = 142 ");
                sql.Append(" AND RED.LINK_TYPE = 209 ");
                sql.Append(" AND RED.LINK_ID = CAP.CAP_ID) AS ID_LOGRADOURO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_1' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_1 ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_1_DESCRICAO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_1_DESCRICAO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC  ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_2' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_2 ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_2_DESCRICAO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_2_DESCRICAO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_3' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_3 ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='COMPLEMENTO_NIVEL_3_DESCRICAO' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP.CAP_ID) AS COMPLEMENTO_NIVEL_3_DESCRICAO ");

                sql.Append(" ,(SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='VLAN_OUTER:INNER' ");
                sql.Append(" AND ATC.LINK_TYPE = 209 ");
                sql.Append(" AND ATC.LINK_ID = CAP_ID) AS VLAN_OUTER_INNER ");

                if (!id_olt.Equals(0))
                {
                    sql.Append(" ,(SELECT ATC.A_VALUE ");
                    sql.Append(" FROM ARD_ATTR_RED ATC ");
                    sql.Append(" WHERE ATC.AT_ID = 220 ");
                    sql.Append(" AND ATC.LINK_TYPE = 31 ");
                    sql.Append(" AND ATC.LINK_ID = " + id_olt + ") AS IP_DADOS_OLT ");
                }

                sql.Append(" FROM CCAP_CARD_CAP CAP ");
                sql.Append(" WHERE CAP.CAP_ID = " + idSubporta + " ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());

                Olt olt = new Olt();
                if (dr != null)
                {
                    olt.status = dr["STATUS"].ToString();
                    olt.circuito = dr["CIRCUITO"].ToString();
                    olt.validade_reserva = dr["VALIDADE_RESERVA"].ToString();
                    olt.cliente = dr["CLIENTE"].ToString();
                    olt.observacao = dr["OBSERVACAO"].ToString();
                    olt.banda_mb = dr["BANDA_MB"].ToString();
                    olt.banda_uplink = dr["BANDA_UPLINK"].ToString();
                    olt.serial = dr["SERIAL_ONT"].ToString();
                    olt.produto = dr["PRODUTO"].ToString();
                    olt.bairro = dr["BAIRRO"].ToString();
                    olt.ont_id = dr["ONT_ID"].ToString();
                    olt.modelo_ont = dr["MODELO_ONT"].ToString();
                    olt.idlogradouro = (dr["ID_LOGRADOURO"] != DBNull.Value ? Convert.ToInt32(dr["ID_LOGRADOURO"]) : 0);
                    olt.nro_lote = dr["NUMERO_LOTE"].ToString();
                    olt.compl_nivel_1 = dr["COMPLEMENTO_NIVEL_1"].ToString();
                    olt.compl_nivel_1_descricao = dr["COMPLEMENTO_NIVEL_1_DESCRICAO"].ToString();
                    olt.compl_nivel_2 = dr["COMPLEMENTO_NIVEL_2"].ToString();
                    olt.compl_nivel_2_descricao = dr["COMPLEMENTO_NIVEL_2_DESCRICAO"].ToString();
                    olt.compl_nivel_3 = dr["COMPLEMENTO_NIVEL_3"].ToString();
                    olt.compl_nivel_3_descricao = dr["COMPLEMENTO_NIVEL_3_DESCRICAO"].ToString();
                    olt.vlan_inner = dr["VLAN_OUTER_INNER"].ToString().Split(':').Last().TrimStart('0');
                    olt.vlan_outer = dr["VLAN_OUTER_INNER"].ToString().Split(':').First().TrimStart('0');

                    if (!id_olt.Equals(0))
                        olt.ipOlt = dr["IP_DADOS_OLT"].ToString();
                }

                return olt;
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

        public string getVlanByIdSubPorta(int idSubporta)
        {
            try
            {
                sql.Append("SELECT ");
                sql.Append(" (SELECT ATC.A_VALUE ");
                sql.Append(" FROM ARD_ATTR_RED ATC ");
                sql.Append(" WHERE ATC.AT_NAME ='VLAN_OUTER:INNER' ");
                sql.Append(" AND ATC.LINK_ID = CAP_ID) AS VLAN_OUTER_INNER ");
                sql.Append(" FROM CCAP_CARD_CAP CAP ");
                sql.Append(" WHERE CAP.CAP_ID = " + idSubporta + " ");
                return ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString())["VLAN_OUTER_INNER"].ToString();
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

        public Cto getCtoDetaisPort(int pontId)
        {
            try
            {
                sql.Append("  SELECT X.* ");
                sql.Append(" ,CASE WHEN LENGTH(REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1)) = 5 ");
                sql.Append("  THEN REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1) ");
                sql.Append("  ELSE ");
                sql.Append("  CONCAT(CONCAT(REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 2), '[^<]+', 1, 1), '/'), ");
                sql.Append("  REGEXP_SUBSTR(REGEXP_SUBSTR(FULLNAMEOLT, '[^>]+', 1, 3), '[^<]+', 1, 1)) ");
                sql.Append("  END AS SLOTNAME ");
                sql.Append("FROM( ");
                sql.Append("  SELECT CP.POINT_ORDER,DES.TEXT ");

                sql.Append("  ,(SELECT CFL.FULLNAME ");
                sql.Append("  FROM COMPONENTFULLNAME CFL ");
                sql.Append("  WHERE CFL.COMPONENTID = ");
                sql.Append("  (SELECT PHE.COMP_ID ");
                sql.Append("  FROM PHE_PHGR_ENDS PHE ");
                sql.Append("  WHERE PHE.PHGR_ID = ( ");
                sql.Append("  SELECT CON.PHGR_ID FROM CONNECTION CON ");
                sql.Append("  WHERE CON.COMPONENT_ID_A = C.COMPONENT_ID ");
                sql.Append("  AND CON.POINT_ID_A = CP.POINT_ID) ");
                sql.Append("  AND PHE.CATEGORY_ID = 7)) AS FULLNAMEOLT ");

                sql.Append("  ,(SELECT D.DESCRIPTION FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
                sql.Append("  (SELECT D.PARENT_ID ");
                sql.Append("  FROM DES_DESIGNATION D WHERE D.DESIGNATION_ID = ");
                sql.Append("  (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
                sql.Append("  WHERE D.DESIGNATION_ID = ");
                sql.Append("  (SELECT D.PARENT_ID FROM DES_DESIGNATION D ");
                sql.Append("  WHERE D.DESIGNATION_ID = C.DESIGNATION_ID)))) AS LOCALIDADE ");

                sql.Append("  ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 5) ");
                sql.Append("  FROM DFN_DESIG_FULLNAME DEF ");
                sql.Append("  WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS ESTACAO ");

                sql.Append("  ,(SELECT REGEXP_SUBSTR(DEF.FULLNAME, '[^;]+', 1, 4) ");
                sql.Append("  FROM DFN_DESIG_FULLNAME DEF ");
                sql.Append("  WHERE DEF.DESIGNATION_ID = C.DESIGNATION_ID) AS CNL ");

                sql.Append("  FROM COMPONENT_POINT CP ");
                sql.Append("  ,COMPONENT C ");
                sql.Append("  ,DES_DESIGNATION DES ");
                sql.Append("  WHERE CP.COMPONENT_ID = C.COMPONENT_ID ");
                sql.Append("  AND DES.DESIGNATION_ID = C.DESIGNATION_ID ");
                sql.Append("  AND DES.DESIG_KIND_ID = 271 ");
                sql.Append("  AND CP.SIDE = 'L' ");
                sql.Append("  AND DES.DSY_ID = 10 ");
                sql.Append("  AND DES.LEVEL_NO = 7 ");
                sql.Append("  AND DES.DESIGLEVELID = 37 ");
                sql.Append("  AND C.SUBCATEGORYID = 283 ");
                sql.Append("  AND CP.POINT_ID = " + pontId + " ");
                sql.Append(")X");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());

                Cto cto = new Cto();
                if (dr != null)
                {
                    cto.cnl = dr["CNL"].ToString();
                    cto.localidade = dr["LOCALIDADE"].ToString();
                    cto.nome = dr["TEXT"].ToString();
                    cto.ponto = dr["POINT_ORDER"].ToString();
                    cto.shelf_slot_porta = dr["SLOTNAME"].ToString();
                }

                return cto;
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

        public int getCountCircuit(string circuito, string status, int pointId)
        {
            try
            {
                sql.Append("SELECT COUNT(PD.POINT_ID) AS TOTAL ");
                sql.Append("FROM PD_POINT_DESCRIPTION PD ");
                sql.Append(",OST_OBJ_STATE OS ");
                sql.Append(",STY_STATETYPE ST ");
                sql.Append("WHERE ST.STATETYPE_ID = OS.STATETYPE_ID ");
                sql.Append("AND PD.POINT_ID = OS.OBJECT_ID ");
                sql.Append("AND PD.DESCRIPTION = '" + circuito + "' ");
                sql.Append("AND ST.NAME = '" + status + "' ");
                sql.Append("AND PD.POINT_ID != " + pointId + " ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
                return (dr != null ? Convert.ToInt32(dr["TOTAL"]) : 0);
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

        public int getCountVlan(string vlan, int subportid)
        {
            try
            {
                sql.Append("SELECT COUNT(ATE.TEXT) AS TOTAL ");
                sql.Append("FROM A_ATTR A ");
                sql.Append(",ATE_ATTR_TEXT ATE ");
                sql.Append(",ARL_ATTR_LINK ARL ");
                sql.Append("WHERE A.A_ID = ATE.A_ID ");
                sql.Append("AND ARL.A_ID = A.A_ID ");
                sql.Append("AND A.AT_ID = 229 ");
                sql.Append("AND ATE.TEXT != '0:0' ");
                sql.Append("AND ATE.TEXT = '" + vlan + "' ");
                sql.Append("AND ARL.LINK_ID != " + subportid + " ");

                DataRow dr = ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
                return (dr != null ? Convert.ToInt32(dr["TOTAL"]) : 0);
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

        public DataTable getModelOnt(string fabricante)
        {
            try
            {
                sql.Append("SELECT MODELO ");
                sql.Append("FROM API.MODELO_ONT ");
                sql.Append("WHERE FABRICANTE = '" + fabricante + "' ");
                sql.Append("ORDER BY MODELO ");

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

        public DataRow getLatLongByIdCto(int idCto)
        {
            try
            {
                sql.Append("SELECT GEO.GEO_Y,GEO.GEO_X ");
                sql.Append("FROM GEO_OBJECT GEO ");
                sql.Append(",DES_DESIGNATION DES ");
                sql.Append("WHERE DES.DESIGNATION_ID = GEO.OBJECT_ID ");
                sql.Append("AND DES.DSY_ID = 10 ");
                sql.Append("AND DES.DESIG_KIND_ID = 271 ");
                sql.Append("AND DES.DESIGLEVELID = 37 ");
                sql.Append("AND DES.DESIGNATION_ID = " + idCto + " ");

                return ConnectionOracleDal.ExecutaSqlRetornoRegistro(sql.ToString());
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


        private void SetPlacemarkData(DataRow dr, Placemark placemark, int? idKind)
        {
            string typeElement = "";

            switch (Convert.ToInt32(idKind))
            {
                case 155: typeElement = "ST"; break;
                case 251: typeElement = "CEO"; break;
                default:
                    typeElement = null; break;
            }

            placemark.ExtendedData = new ExtendedData();
            placemark.Snippet = new Snippet { Text = typeElement };
            placemark.ExtendedData.AddData(new Data { Name = "LATITUDE", DisplayName = "LATITUDE", Value = Convert.ToString(dr["LATITUDE"]) });
            placemark.ExtendedData.AddData(new Data { Name = "LONGITUDE", DisplayName = "LONGITUDE", Value = Convert.ToString(dr["LONGITUDE"]) });
        }

        private void SetPlacemarkDataCTO(DataRow dr, Placemark placemark)
        {
            double occupation = Convert.ToDouble(dr["OCUPADO"]);
            double total = Convert.ToDouble(dr["TOTAL"]);

            double lengthMaxDropCTO = Convert.ToDouble(dr["COMPRIMENTO_MAX_DROP_CTO"]);
            double lengthMaxDropLocal = Convert.ToDouble(dr["COMPRIMENTO_MAX_DROP_LOCAL"]);
            double lengthMaxDrop = (lengthMaxDropCTO > 0.0) ? lengthMaxDropCTO : lengthMaxDropLocal;

            double percentOccupation = occupation / total;
            CultureInfo cultureInfoBR = new CultureInfo("pt-BR");


            placemark.ExtendedData = new ExtendedData();
            placemark.Snippet = new Snippet { Text = (occupation == total) ? "CTO_FULL" : "CTO" };
            placemark.ExtendedData.AddData(new Data { Name = "LATITUDE", DisplayName = "LATITUDE", Value = Convert.ToString(dr["LATITUDE"], cultureInfoBR) });
            placemark.ExtendedData.AddData(new Data { Name = "LONGITUDE", DisplayName = "LONGITUDE", Value = Convert.ToString(dr["LONGITUDE"], cultureInfoBR) });
            placemark.ExtendedData.AddData(new Data { Name = "LOCALIDADE", DisplayName = "LOCALIDADE", Value = Convert.ToString(dr["LOCALIDADE"]) });
            placemark.ExtendedData.AddData(new Data { Name = "ESTACAO", DisplayName = "ESTAÇÃO", Value = Convert.ToString(dr["ESTACAO"]) });
            placemark.ExtendedData.AddData(new Data { Name = "CNL", DisplayName = "CNL", Value = Convert.ToString(dr["CNL"]) });
            placemark.ExtendedData.AddData(new Data { Name = "FABRICANTE", DisplayName = "FABRICANTE", Value = Convert.ToString(dr["FABRICANTE"]) });
            placemark.ExtendedData.AddData(new Data { Name = "MODELO", DisplayName = "MODELO", Value = Convert.ToString(dr["MODELO"]) });
            placemark.ExtendedData.AddData(new Data { Name = "NOME_OLT_GERENCIA", DisplayName = "NOME OLT GERÊNCIA", Value = Convert.ToString(dr["NOME_OLT_GERENCIA"]) });
            placemark.ExtendedData.AddData(new Data { Name = "IP_DADOS_OLT", DisplayName = "IP DADOS OLT", Value = Convert.ToString(dr["IP_DADOS_OLT"]) });
            placemark.ExtendedData.AddData(new Data { Name = "OCUPADO", DisplayName = "OCUPADO", Value = occupation.ToString() });
            placemark.ExtendedData.AddData(new Data { Name = "TOTAL", DisplayName = "TOTAL", Value = total.ToString() });
            placemark.ExtendedData.AddData(new Data { Name = "UTILIZACAO", DisplayName = "UTILIZAÇÃO", Value = percentOccupation.ToString("0.0000%", cultureInfoBR) });
            placemark.ExtendedData.AddData(new Data { Name = "COMPRIMENTO_MAX_DROP", DisplayName = "COMPRIMENTO DROP", Value = lengthMaxDrop.ToString("0.00 m", cultureInfoBR) });
        }

        public IEnumerable<Placemark> GetPlacemarksPoint(double lat, double lng, double radius, List<string> filters)
        {
            string filter = string.Join(", ", filters);
            DataSet ds;

            try
            {
                LatLng latLng = new LatLng(lat, lng, radius);

                string sqlStr = new StringBuilder()
                        .Append(" SELECT                                                                   \n")
                        .Append("   GEO.GEO_Y AS LATITUDE,                                                 \n")
                        .Append("   GEO.GEO_X AS LONGITUDE,                                                \n")
                        .Append("   GEO.OBJECT_ID AS ID_CAIXA,                                             \n")
                        .Append("   REPLACE(DES.TEXT,CHR(9), '') AS NAME,                                  \n")
                        .Append("   DES.DESIG_KIND_ID AS DESIG_KIND_ID                                     \n")
                        .Append(" FROM                                                                     \n")
                        .Append("    GEO_OBJECT GEO                                                        \n")
                        .Append(" INNER JOIN DES_DESIGNATION DES ON DES.DESIGNATION_ID = GEO.OBJECT_ID     \n")
                        .Append(" WHERE                                                                    \n")
                        .Append(string.Format("    DES.DESIG_KIND_ID IN ({0}) \n", filter))
                        .Append(string.Format(" AND (GEO_Y >= {0} AND GEO_Y <= {1})  \n", latLng.GetMinLatStr(), latLng.GetMaxLatStr()))
                        .Append(string.Format(" AND (GEO_X >= {0} AND GEO_X <= {1}) \n", latLng.GetMinLngStr(), latLng.GetMaxLngStr()))
                        .Append(string.Format(" AND acos(trunc(sin(0.017453279 * {0}) * sin((0.017453279) * GEO_Y) + cos(0.017453279 * {1})  \n", latLng.GetLatStr(), latLng.GetLatStr()))
                        .Append(string.Format(" * cos((0.017453279) * GEO_Y) * cos((0.017453279) * GEO_X - (0.017453279 * {0})), 25)) <= {1}/1000/6371 \n", latLng.GetLngStr(), latLng.GetRadiusStr()))
                    .ToString();

                ds = ConnectionOracleDal.ExecutaSqlRetorno(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    Placemark placemark = new Placemark
                    {
                        Name = dr["NAME"] != DBNull.Value ? dr["NAME"].ToString() : "",

                        Geometry = new Point()
                        {
                            Coordinate = new Vector()
                            {
                                Latitude = Convert.ToDouble(dr["LATITUDE"]),
                                Longitude = Convert.ToDouble(dr["LONGITUDE"]),
                                Altitude = null
                            }
                        }
                    };

                    SetPlacemarkData(dr, placemark, Convert.ToInt32(dr["DESIG_KIND_ID"]));

                    yield return placemark;
                }
            }
        }

        public IEnumerable<Placemark> GetPlacemarksPoint(string city, string state, List<string> filters)
        {
            string filter = string.Join(", ", filters);

            DataSet ds;
            try
            {
                string sqlStr = new StringBuilder()
                    .Append(" SELECT * FROM                                                            \n")
                    .Append(" (                                                                        \n")
                    .Append("     SELECT                                                               \n")
                    .Append("         GEO.OBJECT_ID AS ID_CAIXA,                                       \n")
                    .Append("         GEO.GEO_Y AS LATITUDE,                                           \n")
                    .Append("         GEO.GEO_X AS LONGITUDE,                                          \n")
                    .Append("         DES.DESIG_KIND_ID AS DESIG_KIND_ID,                              \n")
                    .Append("         REPLACE(DES.TEXT,CHR(9), '') AS NAME,                            \n")
                    .Append("         DD.DESCRIPTION AS CITY,                                          \n")
                    .Append("         REPLACE(DD2.TEXT,CHR(9), '') AS STATE                            \n")
                    .Append("    FROM GEO_OBJECT GEO                                                   \n")
                    .Append("    INNER JOIN DES_DESIGNATION DES ON DES.DESIGNATION_ID = GEO.OBJECT_ID  \n")
                    .Append("    INNER JOIN DES_DESIGNATION DD  ON DD.DESIGNATION_ID  = DES.PARENT_ID  \n")
                    .Append("    INNER JOIN DES_DESIGNATION DD1 ON DD1.DESIGNATION_ID = DD.PARENT_ID   \n")
                    .Append("    INNER JOIN DES_DESIGNATION DD2 ON DD2.DESIGNATION_ID = DD1.PARENT_ID  \n")
                    .Append("    WHERE                                                                 \n")
                    .Append(string.Format("       DES.DESIG_KIND_ID IN ({0})\n", filter))
                    .Append(" )                                                                        \n")
                    .Append(" WHERE                                                                    \n")
                    .Append(string.Format(" 	CITY = '{0}' AND STATE = '{1}' \n", city, state))
                    .Append(" AND NOT(LATITUDE = 0 AND LONGITUDE = 0)                                  \n")
                    .ToString();

                ds = ConnectionOracleDal.ExecutaSqlRetorno(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Placemark placemark = new Placemark
                    {
                        Name = dr["NAME"] != DBNull.Value ? dr["NAME"].ToString() : "",

                        Geometry = new Point()
                        {
                            Coordinate = new Vector()
                            {
                                Latitude = Convert.ToDouble(dr["LATITUDE"]),
                                Longitude = Convert.ToDouble(dr["LONGITUDE"]),
                                Altitude = null
                            }
                        }
                    };

                    SetPlacemarkData(dr, placemark, Convert.ToInt32(dr["DESIG_KIND_ID"]));

                    yield return placemark;
                }
            }

        }


        public IEnumerable<Placemark> GetPlacemarksCTO(double lat, double lng, double radius)
        {
            DataSet ds;
            try
            {
                LatLng latLng = new LatLng(lat, lng, radius);
                string sqlStr = new StringBuilder()
                    .Append(" SELECT                                                                      \n")
                    .Append("    CTO AS NAME,                                                             \n")
                    .Append("    LONGITUDE,                                                               \n")
                    .Append("    LATITUDE,                                                                \n")
                    .Append("    LOCALIDADE,                                                              \n")
                    .Append("    ESTACAO,                                                                 \n")
                    .Append("    CNL,                                                                     \n")
                    .Append("    FABRICANTE,                                                              \n")
                    .Append("    MODELO,                                                                  \n")
                    .Append("    NOME_OLT_GERENCIA,                                                       \n")
                    .Append("    IP_DADOS_OLT,                                                            \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_CTO, '0.0') AS COMPRIMENTO_MAX_DROP_CTO,     \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_LOCAL, '0.0') AS COMPRIMENTO_MAX_DROP_LOCAL, \n")
                    .Append("    COUNT(CASE WHEN STATUS != 'VAGO' THEN STATUS ELSE NULL END) AS OCUPADO,  \n")
                    .Append("    COUNT(STATUS) AS TOTAL                                              \n")
                    .Append(" FROM                                                                   \n")
                    .Append(" 	CMSYS.MVW_FACILITY_FULL                                              \n")
                    .Append(" WHERE                                                                  \n")
                    .Append(string.Format(" (LATITUDE >= {0} AND LATITUDE <= {1})  \n", latLng.GetMinLatStr(), latLng.GetMaxLatStr()))
                    .Append(string.Format(" AND (LONGITUDE >= {0} AND LONGITUDE <= {1}) \n", latLng.GetMinLngStr(), latLng.GetMaxLngStr()))
                    .Append(string.Format(" AND acos(trunc(sin(0.017453279 * {0}) * sin((0.017453279) * LATITUDE) + cos(0.017453279 * {1})  \n", latLng.GetLatStr(), latLng.GetLatStr()))
                    .Append(string.Format(" * cos((0.017453279) * LATITUDE) * cos((0.017453279) * LONGITUDE - (0.017453279 * {0})),32)) <= {1}/1000/6371 \n", latLng.GetLngStr(), latLng.GetRadiusStr()))
                    .Append(" GROUP BY                                                               \n")
                    .Append("    CTO,                                                                \n")
                    .Append("    LONGITUDE,                                                          \n")
                    .Append("    LATITUDE,                                                           \n")
                    .Append("    LOCALIDADE,                                                         \n")
                    .Append("    ESTACAO,                                                            \n")
                    .Append("    CNL,                                                                \n")
                    .Append("    FABRICANTE,                                                         \n")
                    .Append("    MODELO,                                                             \n")
                    .Append("    NOME_OLT_GERENCIA,                                                  \n")
                    .Append("    IP_DADOS_OLT,                                                       \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_CTO, '0.0'),                          \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_LOCAL, '0.0')                         \n")
                    .Append(" ORDER BY CTO                                                           \n")
                    .ToString();

                ds = ConnectionOracleDal.ExecutaSqlRetorno(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Placemark placemark = new Placemark
                    {
                        Name = dr["NAME"] != DBNull.Value ? dr["NAME"].ToString() : "",
                        Geometry = new Point()
                        {

                            Coordinate = new Vector()
                            {
                                Latitude = Convert.ToDouble(dr["LATITUDE"]),
                                Longitude = Convert.ToDouble(dr["LONGITUDE"]),
                                Altitude = null
                            }
                        }
                    };

                    SetPlacemarkDataCTO(dr, placemark);
                    yield return placemark;
                }
            }
        }

        public IEnumerable<Placemark> GetPlacemarksCTO(string city, string state)
        {
            DataSet ds;
            try
            {
                string sqlStr = new StringBuilder()
                    .Append(" SELECT                                                                      \n")
                    .Append("    CTO AS NAME,                                                             \n")
                    .Append("    LONGITUDE,                                                               \n")
                    .Append("    LATITUDE,                                                                \n")
                    .Append("    LOCALIDADE,                                                              \n")
                    .Append("    ESTACAO,                                                                 \n")
                    .Append("    CNL,                                                                     \n")
                    .Append("    FABRICANTE,                                                              \n")
                    .Append("    MODELO,                                                                  \n")
                    .Append("    NOME_OLT_GERENCIA,                                                       \n")
                    .Append("    IP_DADOS_OLT,                                                            \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_CTO, '0.0') AS COMPRIMENTO_MAX_DROP_CTO,     \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_LOCAL, '0.0') AS COMPRIMENTO_MAX_DROP_LOCAL, \n")
                    .Append("    COUNT(CASE WHEN STATUS != 'VAGO' THEN STATUS ELSE NULL END) AS OCUPADO, \n")
                    .Append("    COUNT(STATUS) AS TOTAL                                              \n")
                    .Append(" FROM                                                                   \n")
                    .Append(" 	CMSYS.MVW_FACILITY_FULL                                              \n")
                    .Append(string.Format(" WHERE LOCALIDADE = '{0}' AND NOT(LATITUDE = 0 AND LONGITUDE = 0)   \n", city))
                    .Append(" GROUP BY                                                               \n")
                    .Append("    CTO,                                                                \n")
                    .Append("    LONGITUDE,                                                          \n")
                    .Append("    LATITUDE,                                                           \n")
                    .Append("    LOCALIDADE,                                                         \n")
                    .Append("    ESTACAO,                                                            \n")
                    .Append("    CNL,                                                                \n")
                    .Append("    FABRICANTE,                                                         \n")
                    .Append("    MODELO,                                                             \n")
                    .Append("    NOME_OLT_GERENCIA,                                                  \n")
                    .Append("    IP_DADOS_OLT,                                                       \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_CTO, '0.0'),                          \n")
                    .Append("    COALESCE(COMPRIMENTO_MAX_DROP_LOCAL, '0.0')                         \n")
                    .Append(" ORDER BY CTO                                                           \n")
                    .ToString();

                ds = ConnectionOracleDal.ExecutaSqlRetorno(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Placemark placemark = new Placemark
                    {
                        Name = dr["NAME"] != DBNull.Value ? dr["NAME"].ToString() : "",
                        Geometry = new Point()
                        {
                            Coordinate = new Vector()
                            {
                                Latitude = Convert.ToDouble(dr["LATITUDE"]),
                                Longitude = Convert.ToDouble(dr["LONGITUDE"]),
                                Altitude = null
                            }
                        }
                    };
                    SetPlacemarkDataCTO(dr, placemark);

                    yield return placemark;
                }
            }
        }

        public IEnumerable<Placemark> GetAttendanceAreaFlat(double lat, double lng, double radius)
        {
            DataSet ds;
            try
            {
                string sqlStr = new StringBuilder()
                    .Append(" SELECT                                                                      \n")
                    .Append("    FLT.CMNAME AS NAME,                                                      \n")
                    .Append("    FLT.GEOLOC.GET_WKT() AS POLYGON                                          \n")
                    .Append(" FROM                                                                        \n")
                    .Append("    MAPINFO.AREA_ATENDIMENTO_FLAT FLT                                        \n")
                    .ToString();

                ds = ConnectionOracleDal.ExecutaSqlRetorno(sqlStr);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Placemark placemark = new Placemark
                    {
                        Name = dr["NAME"].ToString() ?? String.Empty

                    };





                    yield return placemark;
                }
            }
        }
    }
}