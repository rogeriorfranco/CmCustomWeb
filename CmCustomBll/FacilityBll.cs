﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmCustomDal;
using System.Data;
using CmCustomDto;
using SharpKml.Dom;

namespace CmCustomBll
{
    public class FacilityBll
    {
        IFacilityDal facilityDal = new FacilityDal();

        public DataTable consultaFacilidades(string circuito, string estacao, string status, string cnl, string serial, string cto,
            string olt, string ipOlt, string cto_antigo, string outerVlan, string innerVlan)
        {
            //try
            //{
            if (circuito.Equals("") && estacao.Equals("") && status.Equals("0") && cnl.Equals("") && serial.Equals("") && cto.Equals("") &&
                olt.Equals("") && ipOlt.Equals("") && cto_antigo.Equals("") && outerVlan.Equals("") && innerVlan.Equals(""))
                status = "VAGO";

            return facilityDal.consultaFacilidades(circuito, estacao, status, cnl, serial, cto, olt, ipOlt, cto_antigo, outerVlan, innerVlan);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

        }

        public int getIdSubPortaVagoByPorta(int idporta)
        {
            return facilityDal.getIdSubPortaVagoByPorta(idporta);
        }

        public int getTotalSubPortaVagoByPorta(int idporta)
        {
            return facilityDal.getTotalSubPortaVagoByPorta(idporta);
        }

        //public DataRow getFacilityCTOByIdPonto(int idPonto)
        //{
        //    return facilityDal.getFacilityCTOByIdPonto(idPonto);
        //}

        public DataRow getFacilityOLTByIdSubPorta(int idSubporta)
        {
            return facilityDal.getFacilityOLTByIdSubPorta(idSubporta);
        }

        public DataTable getFacilitysRedesignate(string cto, string ip, int pointId)
        {
            return facilityDal.getFacilitysRedesignate(cto, ip, pointId);
        }

        //public Olt getOLTAttributsByIdSubPorta(int idSubporta)
        //{
        //    return facilityDal.getOLTAttributsByIdSubPorta(idSubporta);
        //}


        public DataTable getModelOnt(string fabricante)
        {
            return facilityDal.getModelOnt(fabricante);
        }

        public DataRow getLatLongByIdCto(int idCto)
        {
            return facilityDal.getLatLongByIdCto(idCto);
        }

        public IEnumerable<Placemark> GetPlacemarksPoint(double lat, double lng, double radius, List<string> filters)
        {
            if (filters.Contains(Constants.ID_ST) || filters.Contains(Constants.ID_CEO))
            {
                foreach (var ret in facilityDal.GetPlacemarksPoint(lat, lng, radius, filters))
                {
                    yield return ret;
                }
            }

            if (filters.Contains(Constants.ID_CTO))
            {
                foreach (var ret in facilityDal.GetPlacemarksCTO(lat, lng, radius))
                {
                    yield return ret;
                }
            }
        }

        public IEnumerable<Placemark> GetPlacemarksPoint(string city, string state, List<string> filters)
        {
            if (filters.Contains(Constants.ID_ST) || filters.Contains(Constants.ID_CEO))
            {
                foreach (var ret in facilityDal.GetPlacemarksPoint(city, state, filters.Where(x => x == Constants.ID_ST || x == Constants.ID_CEO).ToList()))
                {
                    yield return ret;
                }
            }

            if (filters.Contains(Constants.ID_CTO))
            {
                foreach (var ret in facilityDal.GetPlacemarksCTO(city, state))
                {
                    yield return ret;
                }
            }
        }
    }
}
