using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomApiDto
{
    public class LatLng
    {
        private static CultureInfo cultureInfoUS = new CultureInfo("en-US");
        private double latitude;
        private double longitude;
        private double radius;
        private double minLat;
        private double minLng;
        private double maxLat;
        private double maxLng;

        public LatLng(double latitude, double longitude, double radius)
        {
            this.latitude = latitude;
            this.longitude = longitude;
            this.radius = radius;
            DoCalc();
        }

        public double GetMinLat()
        {
            return minLat;
        }

        public double GetMinLng()
        {
            return minLng;
        }

        public double GetMaxLat()
        {
            return maxLat;
        }

        public double GetMaxLng()
        {
            return maxLng;
        }

        public string GetMinLatStr()
        {
            return minLat.ToString(cultureInfoUS);
        }

        public string GetMinLngStr()
        {
            return minLng.ToString(cultureInfoUS);
        }

        public string GetMaxLatStr()
        {
            return maxLat.ToString(cultureInfoUS);
        }

        public string GetMaxLngStr()
        {
            return maxLng.ToString(cultureInfoUS);
        }

        public double GetLat()
        {
            return latitude;
        }

        public double GetLng()
        {
            return longitude;
        }

        public string GetLatStr()
        {
            return latitude.ToString(cultureInfoUS);
        }

        public string GetLngStr()
        {
            return longitude.ToString(cultureInfoUS);
        }

        public double GetRadius()
        {
            return radius;
        }

        public string GetRadiusStr()
        {
            return radius.ToString(cultureInfoUS);
        }

        private void DoCalc()
        {
            GeoLocation calculaLimites = GeoLocation.FromDegrees(latitude, longitude);
            GeoLocation[] limites = calculaLimites.BoundingCoordinates(radius / 1000);

            minLat = limites[0].getLatitudeInDegrees();
            minLng = limites[0].getLongitudeInDegrees();
            maxLat = limites[1].getLatitudeInDegrees();
            maxLng = limites[1].getLongitudeInDegrees();
        }

    }
}
