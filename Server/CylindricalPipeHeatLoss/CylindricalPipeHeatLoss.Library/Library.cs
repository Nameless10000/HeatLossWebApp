using CylindricalPipeHeatLoss.Library.Models;
using static System.Math;

namespace CylindricalPipeHeatLoss.Library
{
    public class CylindricalPipeHeatLossLib
    {
        public List<PipeLayer> PipeLayers { get; private set; }

        public double InnerHeatOutputCoeff_a1 { get; private set; }

        public double OutterHeatOutputCoeff_a2 { get; private set; }

        public double PipeLength_l { get; private set; }

        public double HeatFlow_Q => GetLinearHeatFlowDensity() * PipeLength_l;

        public double LinearHeatTransferCoefficient_kl => 1 / PipeThermalResistance_Rl;

        public double PipeThermalResistance_Rl => GetThermalResistances().Sum();

        private double _linearHeatFlowDensity_ql { get; set; } = double.NaN;

        private List<double> _temperatures { get; set; }

        private List<double> _thermalResistances_Rli { get; set; }

        private List<double> _diameters { get; set; }

        public CylindricalPipeHeatLossLib(
            double innerPipeDiameter, 
            double pipeLength, 
            List<PipeLayer> pipeLayers,
            double a1,
            double a2,
            List<double> temps)
        {
            _diameters = new() { innerPipeDiameter };
            PipeLength_l = pipeLength;
            PipeLayers = new(pipeLayers);
            InnerHeatOutputCoeff_a1 = a1;
            OutterHeatOutputCoeff_a2 = a2;

            if (temps.Count != pipeLayers.Count + 3)
                throw new InvalidTemperaturesCountException();

            _temperatures = new(temps);

            _thermalResistances_Rli = new();
        }

        public List<double> GetDiameters()
        {
            if (_diameters.Count == PipeLayers.Count + 1)
                return _diameters;

            for (var i = 0; i < PipeLayers.Count; i++)
                _diameters.Add(_diameters[i] + 2 * PipeLayers[i].Width);

            return _diameters;
        }

        public List<double> GetThermalResistances()
        {
            if (_thermalResistances_Rli.Count != 0)
                return _thermalResistances_Rli;

            if (_diameters.Count != PipeLayers.Count + 1)
                _ = GetDiameters();

            _thermalResistances_Rli.Add(1 / (InnerHeatOutputCoeff_a1 * _diameters[0]));

            for (var i = 0; i < PipeLayers.Count; i++)
            {
                var layer = PipeLayers[i];
                var rli = 1 / (2 * layer.ThermalConductivityCoeff) * Log(_diameters[i + 1] / _diameters[i]);
                _thermalResistances_Rli.Add(rli);
            }

            _thermalResistances_Rli.Add(1 / (OutterHeatOutputCoeff_a2 * _diameters.Last()));

            return _thermalResistances_Rli;
        }

        public double GetLinearHeatFlowDensity()
        {
            if (!double.IsNaN(_linearHeatFlowDensity_ql))
                return _linearHeatFlowDensity_ql;

            if (_temperatures.Count(t => !double.IsNaN(t)) < 2)
                throw new Exception("Not enough number of temperatures was provided. At least 2 temperatures must be known");

            var knownTempsIndexes = _temperatures
                .Select((temp, index) => new
                {
                    Temp = temp,
                    Index = index
                })
                .Where(tempInfo => !double.IsNaN(tempInfo.Temp))
                .Take(2);
            
            var fstTempInfo = knownTempsIndexes.First();
            var lastTempInfo = knownTempsIndexes.Last();
            var RliSum = GetThermalResistances()[fstTempInfo.Index..lastTempInfo.Index].Sum();

            _linearHeatFlowDensity_ql = PI * Abs(fstTempInfo.Temp - lastTempInfo.Temp) / RliSum;

            return _linearHeatFlowDensity_ql;
        }

        public List<double> GetTemperatures()
        {
            if (_temperatures.All(temp => !double.IsNaN(temp)))
                return _temperatures;

            var firstKnownTemp = _temperatures
                .Select((temp, index) => new
                {
                    Temp = temp,
                    Index = index
                })
                .First(tempInfo => !double.IsNaN(tempInfo.Temp));

            for (var i = firstKnownTemp.Index + 1; i < _temperatures.Count; i++)
            {
                if (!double.IsNaN(_temperatures[i]))
                    continue;

                var prevLayerTemp = _temperatures[i - 1];
                _temperatures[i] = prevLayerTemp - GetLinearHeatFlowDensity() * GetThermalResistances()[i - 1] / PI;
            }

            for (var i = firstKnownTemp.Index - 1; i >= 0; i--)
            {
                if (!double.IsNaN(_temperatures[i]))
                    continue;

                var nextLayerTemp = _temperatures[i + 1];
                _temperatures[i] = GetLinearHeatFlowDensity() * GetThermalResistances()[i] / PI + nextLayerTemp;
            }

            return _temperatures;
        }

        public ReportModel GetReport()
        {
            return new ReportModel
            {
                Q = HeatFlow_Q,
                ql = GetLinearHeatFlowDensity(),
                Rli = GetThermalResistances(),
                Rl = PipeThermalResistance_Rl,
                kl = LinearHeatTransferCoefficient_kl,
                PipeLayers = PipeLayers,
                PipeLength = PipeLength_l,
                Diameters = GetDiameters(),
                Temperatures = GetTemperatures()
            };
        }
    }
}
