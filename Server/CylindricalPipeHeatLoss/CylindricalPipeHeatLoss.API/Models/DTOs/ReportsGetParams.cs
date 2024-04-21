namespace CylindricalPipeHeatLoss.API.Models.DTOs
{
    public class ReportsGetParams
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public double? Ql { get; set; }

        public double QlPrecision { get; set; } = 0;
    }
}
