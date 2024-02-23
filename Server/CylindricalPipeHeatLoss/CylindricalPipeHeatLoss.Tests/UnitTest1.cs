using CylindricalPipeHeatLoss.Library;
using CylindricalPipeHeatLoss.Library.Models;

namespace CylindricalPipeHeatLoss.Tests
{
    public class Tests
    {
        private CylindricalPipeHeatLossLib _lib;

        [SetUp]
        public void Setup()
        {
            var a1 = 100;
            var a2 = 50;

            var pipeLen = 3;
            var innerDiam = 0.02;

            var layers = new List<PipeLayer>
            {
                new PipeLayer(0.025, 30),
                new PipeLayer(0.003, 5),
                new PipeLayer(0.005, 2.3),
            };

            var temps = new List<double>()
            {
                111.4,
                double.NaN,
                double.NaN,
                40.00601496,
                double.NaN,
                5
            };

            var lib = new CylindricalPipeHeatLossLib(innerDiam, pipeLen, layers, a1, a2, temps);
            _lib = lib;
        }

        [Test]
        public void TestDiameters()
        {
            var actualRes = _lib.GetDiameters();
            var expectedRes = new double[] { 0.02, 0.07, 0.076, 0.086 };

            Assert.That(actualRes.Count, Is.EqualTo(expectedRes.Count()));
            Assert.That(actualRes, Is.EqualTo(expectedRes).Within(0.001));
        }

        [Test]
        public void TestThermalResistances()
        {
            var actualRes = _lib.GetThermalResistances();
            var expectedRes = new double[] { 0.5, 0.020879383, 0.00822381, 0.026872599, 0.23255814 };

            Assert.That(actualRes.Count, Is.EqualTo(expectedRes.Count()));
            Assert.That(actualRes, Is.EqualTo(expectedRes).Within(0.001));
        }

        [Test]
        public void TestLinearHeatFlowDensity()
        {
            var actualRes = _lib.GetLinearHeatFlowDensity();
            var expectedRes = 423.9075138;

            Assert.That(actualRes, Is.EqualTo(expectedRes).Within(0.001));
        }

        [Test]
        public void TestTemperatures()
        {
            var actualRes = _lib.GetTemperatures();
            var expectedRes = new double[] { 111.4, 43.93302377, 41.11568612, 40.00601496, 36.37998894, 5 };

            Assert.That(actualRes.Count, Is.EqualTo(expectedRes.Count()));
            Assert.That(actualRes, Is.EqualTo(expectedRes).Within(0.001));
        }

        [Test]
        public void TestReport()
        {
            var report = _lib.GetReport();
            Assert.Multiple(() =>
            {
                Assert.That(report.Q, Is.EqualTo(1271.722541).Within(0.001));
                Assert.That(report.ql, Is.EqualTo(423.9075138).Within(0.001));
                Assert.That(report.kl, Is.EqualTo(1.268176245).Within(0.001));
                Assert.That(report.Rl, Is.EqualTo(0.788533931).Within(0.001));
                Assert.That(report.Diameters, Is.EqualTo(new double[] { 0.02, 0.07, 0.076, 0.086 }).Within(0.001));
                Assert.That(report.Rli, Is.EqualTo(new double[] { 0.5, 0.020879383, 0.00822381, 0.026872599, 0.23255814 }).Within(0.001));
                Assert.That(report.Temperatures, Is.EqualTo(new double[] { 111.4, 43.93302377, 41.11568612, 40.00601496, 36.37998894, 5 }).Within(0.001));
            });
        }
    }
}