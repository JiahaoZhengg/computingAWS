using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class StaticModel
    {
        public static string RunPath { get; set; }

        public static bool IsRun { get; set; }

        public static string WorkPath { get; set; }

        public static Entity.CffEntity CffEntity;

        public static string Result;
    }
}