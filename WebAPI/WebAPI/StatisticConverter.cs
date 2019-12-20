using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public static class StatisticConverter
    {
        public static DAL.Statistic ConvertFrom(Models.Statistic s)
        {
            return new DAL.Statistic
            {
                Temperature = s.Temperature,
                Compas = s.Compas,
                WaterLevel = s.WaterLevel,
                Powerstate = s.Powerstate,
                Drivestate = s.Drivestate
            };
        }

        public static Models.Statistic ConvertTo(DAL.Statistic s)
        {
            return new Models.Statistic
            {
                Temperature = s.Temperature,
                Compas = s.Compas,
                WaterLevel = s.WaterLevel,
                Powerstate = s.Powerstate,
                Drivestate = s.Drivestate
            };
        }
    }
}