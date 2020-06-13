﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace CmCustomDal
{
    public class ConnectionOracleDal
    {
        //public static string connectionString = ConfigurationManager.ConnectionStrings["CmWeb"].ConnectionString;

        public static string connectionString = ConfigurationManager.AppSettings["tnsName"] + "User Id=" + ConfigurationManager.AppSettings["userID"] + ";Password=" + ConfigurationManager.AppSettings["password"] + "";            

        #region Métodos
        public static OracleConnection AbreConexao()
        {             
            OracleConnection Con = new OracleConnection();
            try
            {
                Con.ConnectionString = connectionString;
                if (Con.State == ConnectionState.Closed)
                {
                    Con.Open();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ConnectionOracleDAL.AbreConexao(): " + ex.Message, ex.InnerException);
            }
            return Con;
        }

        public static void FechaConexao(OracleConnection ConParam)
        {
            try
            {
                ConParam.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("ConnectionOracleDAL.FechaConexao(): " + ex.Message, ex.InnerException);
            }
        }

        public static List<T> ExecutaSqlReaderGeneric<T>(string Sql)
        {
            OracleConnection Conexao = AbreConexao();
            OracleDataReader reader;
            List<T> lista = new List<T>();
            try
            {
                OracleCommand Comando = new OracleCommand();
                Comando.Connection = Conexao;
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = Sql;
                reader = Comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        lista.Add((T)Convert.ChangeType(reader.GetValue(0), typeof(T)));
                    }
                }
                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ConnectionOracleDAL.ExecutaSqlReaderGeneric(Sql): " + ex.Message, ex.InnerException);
            }
            finally
            {
                FechaConexao(Conexao);
            }

            return lista;
        }

        public static DataSet ExecutaSqlRetorno(string Sql)
        {
            OracleConnection Conexao = AbreConexao();
            DataSet DadosRet = new DataSet();
            try
            {
                OracleCommand Comando = new OracleCommand();
                Comando.Connection = Conexao;
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = Sql;

                OracleDataAdapter Adaptador = new OracleDataAdapter();
                Adaptador.SelectCommand = Comando;
                Adaptador.Fill(DadosRet);

            }
            catch (Exception ex)
            {
                throw new Exception("ERRO ConnectionOracle.ExecutaSqlRetorno(Sql): " + ex.Message, ex.InnerException);
            }
            finally
            {
                FechaConexao(Conexao);
            }

            return DadosRet;
        }

        public static DataRow ExecutaSqlRetornoRegistro(string Sql)
        {
            OracleConnection Conexao = AbreConexao();
            DataSet DadosRet = new DataSet();
            DataRow Reg = null;
            try
            {
                OracleCommand comando = new OracleCommand();
                comando.Connection = Conexao;
                comando.CommandType = CommandType.Text;
                comando.CommandText = Sql;

                OracleDataAdapter Adaptador = new OracleDataAdapter();
                Adaptador.SelectCommand = comando;
                Adaptador.Fill(DadosRet);

                if (DadosRet.Tables[0].Rows.Count > 0)
                {
                    Reg = DadosRet.Tables[0].Select()[0];
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ConnectionOracleDAL.ExecutaSqlRetornoRegistro(Sql): " + ex.Message, ex.InnerException);
            }
            finally
            {
                FechaConexao(Conexao);
            }

            return Reg;
        }

        public static int ExecutaSql(string Sql)
        {
            OracleTransaction Transacao = default(OracleTransaction);
            OracleConnection Conexao = AbreConexao();
            Transacao = Conexao.BeginTransaction();
            int N = -1;
            try
            {
                OracleCommand Comando = new OracleCommand();
                Comando.Connection = Conexao;
                Comando.CommandType = CommandType.Text;
                Comando.CommandText = Sql;
                Comando.Transaction = Transacao;
                N = Comando.ExecuteNonQuery();
                Transacao.Commit();
            }
            catch (Exception ex)
            {
                Transacao.Rollback();
                throw new Exception("ConnectionOracleDAL.ExecutaSql(Sql): " + ex.Message);
            }
            finally
            {
                FechaConexao(Conexao);
                Transacao.Dispose();
            }
            return N;
        }
        #endregion
    }
}
