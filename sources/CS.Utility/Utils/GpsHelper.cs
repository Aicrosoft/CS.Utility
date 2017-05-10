using System;

namespace CS.Utils
{
    /// <summary>
    ///     地理位置相关
    /// </summary>
    public class GpsHelper
    {
        #region 计算两点间的距离

        private const double EarthRadius = 6378.137; //地球半径

        private static double Rad(double d)
        {
            return d*Math.PI/180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            var radLat1 = Rad(lat1);
            var radLat2 = Rad(lat2);
            var a = radLat1 - radLat2;
            var b = Rad(lng1) - Rad(lng2);

            var s = 2*Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a/2), 2) +
                                          Math.Cos(radLat1)*Math.Cos(radLat2)*Math.Pow(Math.Sin(b/2), 2)));
            s = s*EarthRadius;
            s = Math.Round(s*10000)/10000;
            return s;
        }
        /// <summary>
        /// 返回两点间的距离
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetDistance(GpsPoint point1, GpsPoint point2)
        {
            return GetDistance(point1.Latitude, point1.Longitude, point2.Latitude, point2.Longitude);
        }

        #endregion
    }

    public struct GpsPoint
    {

        public GpsPoint(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        /// <summary>
        /// longitude// 经度，浮点数，范围为180 ~ -180。
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// latitude // 纬度，浮点数，范围为90 ~ -90
        /// </summary>
        public double Latitude { get; set; }
    }

}