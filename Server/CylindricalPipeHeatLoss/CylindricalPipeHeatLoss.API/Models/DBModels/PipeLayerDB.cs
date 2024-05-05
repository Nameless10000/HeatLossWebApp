using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class PipeLayerDB
    {
        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public int MaterialID { get; set; }

        public MaterialDB Material { get; set; }

        public double Width { get; set; }
    }
}
