using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Baza
    {
        public static Dictionary<int, DevClass> devices = new Dictionary<int, DevClass>();
        public static Dictionary<int, Agregator> agregators = new Dictionary<int, Agregator>();
        public static List<int> slobodni = new List<int>();
        public static List<int> slobodniAG = new List<int>();
    }
}
