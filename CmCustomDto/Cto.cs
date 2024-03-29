﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDto
{
    public class Cto
    {
        public int component_id { get; set; }
        public int point_id { get; set; }
        public int idOlt { get; set; }
        public int id { get; set; }        
        public string nome { get; set; }
        public string nome_antigo { get; set; }
        public string estacao { get; set; }
        public string cnl { get; set; }            
        public string status { get; set; }
        public string circuito { get; set; }
        public string localidade { get; set; }
        public string ponto { get; set; }
        public string ipOlt { get; set; }
        public string cliente { get; set; }
        public string shelf_slot_porta { get; set; }        
        private Olt _idSubPorta = new Olt();
        public int idSubPorta
        {
            get { return _idSubPorta.idSubPorta; }
            set { _idSubPorta.idSubPorta = value; }
        }
        private Olt _Olt_Nome = new Olt();
        public string Olt_Nome
        {
            get { return _Olt_Nome.nome; }
            set { _Olt_Nome.nome = value; }
        }
    }
}
