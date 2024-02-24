using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class PipeLayerDB
    {
        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public DateTime ReportGeneratedAt { get; set; }

        [Key]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR(128)")]
        public string Material { get; set; }

        public double Width { get; set; }

        public double ThermalConductivityCoeff { get; set; }
    }
}
