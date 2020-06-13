using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CmCustomDto
{    
    public class Elements
    {        
        public string cto { get; set; }        
        public string estacao { get; set; }        
        public string localidade { get; set; }        
        public string cnl { get; set; }        
        public string porta_cto { get; set; }        
        public string vlan_inner { get; set; }        
        public string vlan { get; set; }        
        public string ip_olt { get; set; }        
        public string olt { get; set; }
        public string modelo { get; set; }        
        public string shelf { get; set; }        
        public string slot { get; set; }        
        public string porta { get; set; }        
        public string porta_logica { get; set; }        
        public string tecnologia { get; set; }                        
        public string desc_retorno { get; set; }        
        public string status_viabilidade { get; set; }        
        public string velocidades { get; set; }                
        public string cliente { get; set; }        
        public string produto { get; set; }        
        public string banda_mb { get; set; }        
        public string banda_uplink { get; set; }        
        public string status { get; set; }        
        public string serial { get; set; }       
        public string acesso { get; set; }        
        public string validade_reserva { get; set; }        
        public string vlan_vobb { get; set; }        
        public string comprimento_drop { get; set; }
        public string latitude_rede { get; set; }        
        public string longitude_rede { get; set; }        
        public string latitude_cliente { get; set; }        
        public string longitude_cliente { get; set; }
        public List<Cto> cto_analisada { get; set; }
    }

}
