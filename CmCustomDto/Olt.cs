﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDto
{
    [Serializable]
    public class Olt
    {
        public int idSubPorta { get; set; }
        public int idComponent { get; set; }        
        public string status { get; set; }
        public string circuito { get; set; }
        public string cliente { get; set; }
        public string acao { get; set; }
        public string vlan_inner { get; set; }
        public string vlan_outer { get; set; }
        public string vlan_banda_larga { get; set; }
        public string vlan_voz_vobb { get; set; }
        public string banda_mb { get; set; }
        public string banda_uplink { get; set; }
        public string nro_lote { get; set; }
        public string logradouro { get; set; }
        public string localidade { get; set; }
        public int idlogradouro { get; set; }
        public string serial { get; set; }
        public string produto { get; set; }
        public string bairro { get; set; }
        public string validade_reserva { get; set; }
        public string modelo { get; set; }
        public string ipOlt { get; set; }
        public string nome { get; set; }
        public string shelf { get; set; }
        public string slot { get; set; }
        public string porta { get; set; }
        public string tecnologia { get; set; }
        public string ont_id { get; set; }
        public string modelo_ont { get; set; }
        public string compl_nivel_1 { get; set; }
        public string compl_nivel_1_descricao { get; set; }
        public string compl_nivel_2 { get; set; }
        public string compl_nivel_2_descricao { get; set; }
        public string compl_nivel_3 { get; set; }
        public string compl_nivel_3_descricao { get; set; }
        public string observacao { get; set; }
        public bool copyVlan { get; set; }
        public string tipoVlanBandaLarga { get; set; }
        public string nome_gerencia { get; set; }
    }
}

