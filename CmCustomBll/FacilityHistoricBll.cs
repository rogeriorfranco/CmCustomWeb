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
    public class FacilityHistoricBll
    {
        IFacilityHistoricDal facilityHistoricDal = new FacilityHistoricDal();

        public void save(Facility facility)
        {
            facilityHistoricDal.save(facility);
        }

        public DataTable consultaHistorico(string usuario, string circuito, string vlan, string vlanInner,
            string serial, string shelf, string slot, string porta, string ontId, string ipOlt, string dtInicio, string dtFim)
        {
            return facilityHistoricDal.consultaHistorico(usuario, circuito, vlan, vlanInner, serial, shelf,
                slot, porta, ontId, ipOlt, dtInicio, dtFim);
        }

        

    }
}
