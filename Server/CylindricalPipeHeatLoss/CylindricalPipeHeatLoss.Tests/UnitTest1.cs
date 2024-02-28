using CylindricalPipeHeatLoss.Library;
using CylindricalPipeHeatLoss.Library.Models;

namespace CylindricalPipeHeatLoss.Tests
{
    public class Tests
    {
        private Library.CylindricalPipeHeatLossLib _lib;

        [SetUp]
        public void Setup()
        {
            var precision = 1e-2;

            var a1 = 100;
            var e = 0.82;

            var pipeLen = 3;
            var innerRad = 0.5;

            var layers = new List<PipeLayer>
            {
                /*new PipeLayer(0.23, 0.009e-6, 0.372e-3, 0.974, "ью"),
                new PipeLayer(0.115, 0.157e-6, 0.055e-3, 1.745, "ьод-42"),
                new PipeLayer(0.115, 1.452e-6, -4.392e-3, 6.04, "й"),*/
            };

            var innerTemp = 1000;
            var outterTemp = 20;

            var lib = new Library.CylindricalPipeHeatLossLib(innerRad, a1, e, layers, innerTemp, outterTemp, precision);
            _lib = lib;
        }

        [Test]
        public void Test()
        {
            var report = _lib.GetReport();
            Assert.Pass();
        }
    }
}