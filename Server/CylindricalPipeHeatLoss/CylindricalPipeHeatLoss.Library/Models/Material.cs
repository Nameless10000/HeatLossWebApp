using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library.Models
{
    public class Material(double a, double b, double c, string name, string trademark)
    {
        public double ACoeff { get; set; } = a;

        public double BCoeff { get; set; } = b;

        public double CCoeff { get; set; } = c;

        public string Name { get; set; } = name;

        public string Trademark { get; set; } = trademark;
    }
}
