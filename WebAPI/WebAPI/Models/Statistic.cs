using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Statistic
    {
        public int Temperature { get; set; }
        public int Compas { get; set; }
        public int WaterLevel { get; set; }
        public bool Powerstate { get; set; }
        public bool Drivestate { get; set; }

    }
}