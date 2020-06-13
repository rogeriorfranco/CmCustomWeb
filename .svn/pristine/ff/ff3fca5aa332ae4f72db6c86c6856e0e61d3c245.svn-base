using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmCustomApiDto
{
    public class GeoLocation
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (System.Math.PI / 180) * degrees;
        }

        public static double ConvertRadiansToDegrees(double radian)
        {
            return radian * (180.0 / System.Math.PI);
        }

        private double radLat;  // latitude in radians
        private double radLon;  // longitude in radians

        private double degLat;  // latitude in degrees
        private double degLon;  // longitude in degrees

        private static double MIN_LAT = ConvertDegreesToRadians(-90d);  // -PI/2
        private static double MAX_LAT = ConvertDegreesToRadians(90d);   //  PI/2
        private static double MIN_LON = ConvertDegreesToRadians(-180d); // -PI
        private static double MAX_LON = ConvertDegreesToRadians(180d);  //  PI

        private const double earthRadius = 6371.01;

        private GeoLocation()
        {
        }

        /// <summary>
        /// Return GeoLocation from Degrees
        /// </summary>
        /// <param name="latitude">The latitude, in degrees.</param>
        /// <param name="longitude">The longitude, in degrees.</param>
        /// <returns>GeoLocation in Degrees</returns>
        public static GeoLocation FromDegrees(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = ConvertDegreesToRadians(latitude),
                radLon = ConvertDegreesToRadians(longitude),
                degLat = latitude,
                degLon = longitude
            };
            result.CheckBounds();
            return result;
        }

        /// <summary>
        /// Return GeoLocation from Radians
        /// </summary>
        /// <param name="latitude">The latitude, in radians.</param>
        /// <param name="longitude">The longitude, in radians.</param>
        /// <returns>GeoLocation in Radians</returns>
        public static GeoLocation FromRadians(double latitude, double longitude)
        {
            GeoLocation result = new GeoLocation
            {
                radLat = latitude,
                radLon = longitude,
                degLat = ConvertRadiansToDegrees(latitude),
                degLon = ConvertRadiansToDegrees(longitude)
            };
            result.CheckBounds();
            return result;
        }

        private void CheckBounds()
        {
            if (radLat < MIN_LAT || radLat > MAX_LAT ||
                    radLon < MIN_LON || radLon > MAX_LON)
                // FIX
                // throw new Exception("Arguments are out of bounds");
                System.Console.WriteLine("Argumentos fora dos limites");
        }

        /**
         * @return the latitude, in degrees.
         */
        public double getLatitudeInDegrees()
        {
            return degLat;
        }

        /**
         * @return the longitude, in degrees.
         */
        public double getLongitudeInDegrees()
        {
            return degLon;
        }

        /**
         * @return the latitude, in radians.
         */
        public double getLatitudeInRadians()
        {
            return radLat;
        }

        /**
         * @return the longitude, in radians.
         */
        public double getLongitudeInRadians()
        {
            return radLon;
        }

        public override string ToString()
        {
            return "(" + degLat + "\u00B0, " + degLon + "\u00B0) = (" +
                     radLat + " rad, " + radLon + " rad)";
        }

        /// <summary>
        /// Computes the bounding coordinates of all points on the surface
        /// of a sphere that have a great circle distance to the point represented
        /// by this GeoLocation instance that is less or equal to the distance
        /// argument.
        /// For more information about the formulae used in this method visit
        /// http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates
        /// </summary>
        /// <param name="distance">The distance from the point represented by this 
        /// GeoLocation instance. Must me measured in the same unit as the radius argument.
        /// </param>
        /// <returns>An array of two GeoLocation objects such that:
        /// 
        /// a) The latitude of any point within the specified distance is greater
        /// or equal to the latitude of the first array element and smaller or
        /// equal to the latitude of the second array element.
        /// If the longitude of the first array element is smaller or equal to
        /// the longitude of the second element, then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element and smaller or
        /// equal to the longitude of the second array element.
        /// 
        /// b) If the longitude of the first array element is greater than the
        /// longitude of the second element (this is the case if the 180th
        /// meridian is within the distance), then
        /// the longitude of any point within the specified distance is greater
        /// or equal to the longitude of the first array element
        /// or smaller or equal to the longitude of the second
        /// array element.</returns>
        public GeoLocation[] BoundingCoordinates(double distance)
        {
            // FIX
            //if (distance < 0d)
            //   throw new Exception("Distance cannot be less than 0");

            // angular distance in radians on a great circle
            double radDist = distance / earthRadius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLon, maxLon;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = System.Math.Asin(System.Math.Sin(radDist) /
                    System.Math.Cos(radLat));
                minLon = radLon - deltaLon;
                if (minLon < MIN_LON) minLon += 2d * System.Math.PI;
                maxLon = radLon + deltaLon;
                if (maxLon > MAX_LON) maxLon -= 2d * System.Math.PI;
            }
            else
            {
                // a pole is within the distance
                minLat = System.Math.Max(minLat, MIN_LAT);
                maxLat = System.Math.Min(maxLat, MAX_LAT);
                minLon = MIN_LON;
                maxLon = MAX_LON;
            }

            return new GeoLocation[]
            {
                FromRadians(minLat, minLon),
                FromRadians(maxLat, maxLon)
            };
        }
    }


}
