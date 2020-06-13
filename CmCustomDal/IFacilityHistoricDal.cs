﻿using CmCustomDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface IFacilityHistoricDal
    {
        void save(Facility facility);
        DataTable consultaHistorico(string usuario, string circuito, string vlan, string vlanInner,
               string serial, string shelf, string slot, string porta, string ontId, string ipOlt, string dtInicio, string dtFim);
    }

}