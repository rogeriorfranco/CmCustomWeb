using CmCustomDto;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomDal
{
    public interface IFacilityDal
    {
        DataTable consultaFacilidades(string circuito, string estacao, string status, string cnl, string serial, string cto,
            string olt, string ipOlt, string cto_antigo, string outerVlan, string innerVlan);        
        DataRow getFacilityOLTByIdSubPorta(int idSubporta);
        int getIdStatusCompomentPoint(string status);
        int getIdSubPortaVagoByPorta(int idporta);
        int getTotalSubPortaVagoByPorta(int idporta);
        DataTable getFacilitysRedesignate(string cto, string ip, int pointId);
        Olt getOLTAttributsByIdSubPorta(int idSubporta, int id_olt = 0);
        string getVlanByIdSubPorta(int idSubporta);
        Cto getCtoDetaisPort(int pontId);
        int getCountCircuit(string circuito, string status, int pointId);
        int getCountVlan(string vlan, int subportid);
        DataTable getModelOnt(string fabricante);
        DataRow getLatLongByIdCto(int idCto);
        IEnumerable<Placemark> GetPlacemarksPoint(double lat, double lng, double radius, List<string> filters);
        IEnumerable<Placemark> GetPlacemarksPoint(string city, string state, List<string> filters);
        IEnumerable<Placemark> GetPlacemarksCTO(string city, string state);
        IEnumerable<Placemark> GetPlacemarksCTO(double lat, double lng, double radius);
    }
}
