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

        public static Models.Statistic[] ConvertTo(DAL.Statistic[] s)
        {
            Models.Statistic[] toReturn = new Models.Statistic[s.Length];

            for (int i = 0; i < s.Length; i++)
                toReturn[i] = new Models.Statistic
                {
                    Temperature = s[i].Temperature,
                    Compas = s[i].Compas,
                    WaterLevel = s[i].WaterLevel,
                    Powerstate = s[i].Powerstate,
                    Drivestate = s[i].Drivestate
                };

            return toReturn;
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