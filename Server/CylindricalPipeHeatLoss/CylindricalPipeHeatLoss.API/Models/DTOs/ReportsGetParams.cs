namespace CylindricalPipeHeatLoss.API.Models.DTOs
{
    public class ReportsGetParams
    {
        public DateTime From { get; set; } = DateTime.MinValue;
        public DateTime To { get; set; } = DateTime.Now;

        public double? Ql { get; set; }

        public double QlPrecision { get; set; } = 1e-2;
    }
}
