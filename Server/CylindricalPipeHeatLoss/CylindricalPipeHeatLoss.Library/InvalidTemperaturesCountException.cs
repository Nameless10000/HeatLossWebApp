using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CylindricalPipeHeatLoss.Library
{
    public class InvalidTemperaturesCountException() : Exception
    {
        public override string Message => $"Invalid number of temparatures was provided according to layers count.";
    }
}
