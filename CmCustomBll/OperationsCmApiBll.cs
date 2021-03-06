﻿using CmCustomDal;
using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomBll
{
    public class OperationsCmApiBll
    {
        IOperationsCmApiDal operationsCmAPI = new OperationsCmApiDal();
        public void atualizaFacilidades(int compPointID, Olt olt, string usuario)
        {
            try
            {
                if (olt.acao.Equals("SALVAR"))
                {
                    if (olt.status.Equals("VAGO") || olt.status.Equals("AUDITORIA") || olt.status.Equals("DEFEITO"))
                    {
                        operationsCmAPI.atualizaPontoCompoment(compPointID, olt.circuito, olt.status);
                        throw new Exception("Porta atualizada: " + olt.status);
                    }

                    if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO")) && string.IsNullOrEmpty(olt.circuito.Trim()))
                        throw new Exception("O preenchimento do Circuito é obrigatório para o status OCUPADO e DESIGNADO");

                    FacilityDal facilityDal = new FacilityDal();
                    if (facilityDal.getCountCircuit(olt.circuito.Trim(), olt.status, compPointID) >= 1)
                        throw new Exception("Já existe esse circuito cadastrado no Connect Master com status: " + olt.status);

                    if (olt.tipoVlanBandaLarga == "DEDIDICADA")
                        if (!string.IsNullOrEmpty(olt.vlan_outer) || !string.IsNullOrEmpty(olt.vlan_inner))
                        {
                            int totalVlan = facilityDal.getCountVlan(string.Concat(olt.vlan_outer, ":", olt.vlan_inner), olt.idSubPorta);
                            if (totalVlan >= 1)
                                throw new Exception("Já existe (" + totalVlan + ") vlan outer inner (" + string.Concat(olt.vlan_outer, ":", olt.vlan_inner) + ") cadastrado no Connect Master");
                        }

                    if (olt.idSubPorta != 0)
                    {
                        if (!olt.vlan_inner.Equals("0") && !olt.vlan_outer.Equals("0"))
                            if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO")) && (string.IsNullOrEmpty(olt.vlan_inner) || string.IsNullOrEmpty(olt.vlan_outer)))
                                throw new Exception("O preenchimento da Vlan inner outer é obrigatório para o status OCUPADO e DESIGNADO");

                        if (!string.IsNullOrEmpty(olt.vlan_inner.TrimStart('0')) && !string.IsNullOrEmpty(olt.vlan_outer.TrimStart('0')))
                        {
                            olt.vlan_inner = olt.vlan_inner.TrimStart('0');
                            olt.vlan_outer = olt.vlan_outer.TrimStart('0');
                        }
                        else
                        {
                            olt.vlan_inner = "0";
                            olt.vlan_outer = "0";
                        }

                        if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO") || olt.status.Equals("RESERVADO")) && string.IsNullOrEmpty(olt.cliente.Trim()))
                            throw new Exception("O preenchimento do Cliente é obrigatório para o status OCUPADO, DESIGNADO e RESERVADO");

                        if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO") || olt.status.Equals("RESERVADO")) && string.IsNullOrEmpty(olt.produto.Trim()))
                            throw new Exception("O preenchimento do Produto é obrigatório para o status OCUPADO, DESIGNADO e RESERVADO");

                        if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO") || olt.status.Equals("RESERVADO")) && string.IsNullOrEmpty(olt.banda_mb.Trim()))
                            throw new Exception("O preenchimento do Banda MB é obrigatório para o status OCUPADO, DESIGNADO e RESERVADO");

                        if ((olt.status.Equals("OCUPADO") || olt.status.Equals("DESIGNADO") || olt.status.Equals("RESERVADO")) && string.IsNullOrEmpty(olt.banda_uplink.Trim()))
                            throw new Exception("O preenchimento do Banda UPLINK é obrigatório para o status OCUPADO, DESIGNADO e RESERVADO");

                        if (olt.status.Equals("OCUPADO") && string.IsNullOrEmpty(olt.serial.Trim()))
                            throw new Exception("O preenchimento do Serial é obrigatório para o status OCUPADO");

                        if (olt.status.Equals("OCUPADO") && string.IsNullOrEmpty(olt.ont_id.Trim()))
                            throw new Exception("O preenchimento do Ont ID é obrigatório para o status OCUPADO");
                    }
                    else
                    {
                        throw new Exception("Sem sub porta disponivel, favor verificar o cadastro");
                    }

                    saveFacility(compPointID, olt.circuito, olt.status, usuario, olt);
                    throw new Exception("Facilidades alteradas com sucesso");
                }

                if (olt.acao.Equals("LIBERAR"))
                {
                    Facility facility = new Facility();
                    saveFacility(compPointID, string.Empty, "VAGO", usuario, clearOlt(olt), clearfacility(facility));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Olt clearOlt(Olt olt)
        {
            olt.status = "VAGO";
            olt.circuito = string.Empty;
            olt.cliente = string.Empty;
            olt.banda_mb = "0";
            olt.banda_uplink = "0";
            olt.serial = string.Empty;
            olt.produto = string.Empty;
            olt.bairro = string.Empty;
            olt.nro_lote = "0";
            olt.validade_reserva = string.Empty;
            olt.idlogradouro = 0;
            olt.modelo_ont = string.Empty;
            olt.observacao = string.Empty;
            if (olt.acao.Equals("REDESIGNAR"))
            {
                olt.compl_nivel_1 = string.Empty;
                olt.compl_nivel_1_descricao = string.Empty;
                olt.compl_nivel_2 = string.Empty;
                olt.compl_nivel_2_descricao = string.Empty;
                olt.compl_nivel_3 = string.Empty;
                olt.compl_nivel_3_descricao = string.Empty;
            }
            return olt;
        }

        private Facility clearfacility(Facility facility)
        {
            facility.circuito_novo = string.Empty;
            facility.status_novo = string.Empty;
            facility.cliente_novo = string.Empty;
            facility.produto_novo = string.Empty;
            facility.banda_mb_novo = string.Empty;
            facility.banda_uplink_novo = string.Empty;
            facility.ont_id_novo = string.Empty;
            facility.serial_novo = string.Empty;
            facility.vlan_inner_novo = string.Empty;
            facility.vlan_novo = string.Empty;
            facility.validade_reserva_novo = string.Empty;
            return facility;
        }

        public string getSession()
        {
            return operationsCmAPI.getSession();
        }

        private void saveFacility(int idPoint, string circuito, string status, string usuario, Olt oltNew, Facility facility = null)
        {
            FacilityHistoricDal facilityHistoricDal = new FacilityHistoricDal();
            FacilityDal facilityDal = new FacilityDal();

            Olt oltOld = new Olt();
            oltOld = facilityDal.getOLTAttributsByIdSubPorta(oltNew.idSubPorta);

            operationsCmAPI.atualizaPontoCompoment(idPoint, circuito.Trim(), status);
            operationsCmAPI.atualizaAtributosPortaOLT(oltNew);

            Cto ctoOld = new Cto();
            ctoOld = facilityDal.getCtoDetaisPort(idPoint);

            if (facility == null)
                facility = new Facility();

            facility.usuario = usuario;
            facility.cto = ctoOld.nome;
            facility.cnl = ctoOld.cnl;
            facility.localidade = ctoOld.localidade;
            facility.porta_cto = ctoOld.ponto;
            facility.ip_olt = oltNew.ipOlt;
            facility.shelf = ctoOld.shelf_slot_porta.Split('/').First();
            facility.slot = ctoOld.shelf_slot_porta.Split('/')[1];
            facility.porta = ctoOld.shelf_slot_porta.Split('/').Last();

            facility.circuito_novo = oltNew.circuito.TrimStart();
            facility.status_novo = oltNew.status;
            facility.cliente_novo = oltNew.cliente;
            facility.produto_novo = oltNew.produto;
            facility.banda_mb_novo = oltNew.banda_mb;
            facility.banda_uplink_novo = oltNew.banda_uplink;
            facility.ont_id_novo = oltNew.ont_id;
            facility.serial_novo = oltNew.serial;
            facility.vlan_inner_novo = oltNew.vlan_inner;
            facility.vlan_novo = oltNew.vlan_outer;
            facility.validade_reserva_novo = oltNew.validade_reserva;

            facility.circuito_antigo = oltOld.circuito;
            facility.status_antigo = oltOld.status;
            facility.cliente_antigo = oltOld.cliente;
            facility.produto_antigo = oltOld.produto;
            facility.banda_mb_antigo = oltOld.banda_mb;
            facility.banda_uplink_antigo = oltOld.banda_uplink;
            facility.ont_id_antigo = oltOld.ont_id;
            facility.serial_antigo = oltOld.serial;
            facility.vlan_inner_antigo = oltOld.vlan_inner;
            facility.vlan_antigo = oltOld.vlan_outer;
            facility.validade_reserva_antigo = oltOld.validade_reserva;
            facilityHistoricDal.save(facility);
        }

        public void redesignateFacility(string usuario, int idOltActual, int idOltNew, int pointIdActual, int subPortaIdActual, int pointIdNew, int subPortaIdNew, string circuito, string status)
        {
            try
            {
                FacilityDal facilityDal = new FacilityDal();
                Olt olt = new Olt();
                olt = facilityDal.getOLTAttributsByIdSubPorta(subPortaIdActual, idOltNew);
                olt.idSubPorta = subPortaIdNew;
                olt.acao = "REDESIGNAR";
                olt.copyVlan = (idOltActual.Equals(idOltNew) ? true : false);
                operationsCmAPI.atualizaPontoCompoment(pointIdNew, circuito, status);
                operationsCmAPI.atualizaAtributosPortaOLT(olt);

                if (olt.copyVlan == false)
                {
                    FacilityHistoricDal facilityHistoricDal = new FacilityHistoricDal();
                    Facility facility = new Facility();
                    Cto cto = new Cto();
                    cto = facilityDal.getCtoDetaisPort(pointIdNew);

                    facility.usuario = usuario;
                    facility.cto = cto.nome;
                    facility.cnl = cto.cnl;
                    facility.localidade = cto.localidade;
                    facility.porta_cto = cto.ponto;
                    facility.ip_olt = olt.ipOlt;
                    facility.shelf = cto.shelf_slot_porta.Split('/').First();
                    facility.slot = cto.shelf_slot_porta.Split('/')[1];
                    facility.porta = cto.shelf_slot_porta.Split('/').Last();

                    facility.circuito_novo = olt.circuito;
                    facility.status_novo = olt.status;
                    facility.cliente_novo = olt.cliente;
                    facility.produto_novo = olt.produto;
                    facility.banda_mb_novo = olt.banda_mb;
                    facility.banda_uplink_novo = olt.banda_uplink;
                    facility.ont_id_novo = olt.ont_id;
                    facility.serial_novo = olt.serial;
                    string vlan_outer_inner = facilityDal.getVlanByIdSubPorta(subPortaIdNew);
                    facility.vlan_inner_novo = vlan_outer_inner.Split(':').Last();
                    facility.vlan_novo = vlan_outer_inner.Split(':').First();
                    facility.validade_reserva_novo = olt.validade_reserva;

                    facility.circuito_antigo = olt.circuito;
                    facility.status_antigo = olt.status;
                    facility.cliente_antigo = olt.cliente;
                    facility.produto_antigo = olt.produto;
                    facility.banda_mb_antigo = olt.banda_mb;
                    facility.banda_uplink_antigo = olt.banda_uplink;
                    facility.ont_id_antigo = olt.ont_id;
                    facility.serial_antigo = olt.serial;
                    facility.vlan_inner_antigo = olt.vlan_inner;
                    facility.vlan_antigo = olt.vlan_outer;
                    facility.validade_reserva_antigo = olt.validade_reserva;
                    facilityHistoricDal.save(facility);
                }

                operationsCmAPI.atualizaPontoCompoment(pointIdActual, string.Empty, "VAGO");
                olt.idSubPorta = subPortaIdActual;
                operationsCmAPI.atualizaAtributosPortaOLT(clearOlt(olt));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CreateAttributSubPorts(int idComponent)
        {
            return operationsCmAPI.CreateAttributSubPorts(idComponent);
        }
    }
}
