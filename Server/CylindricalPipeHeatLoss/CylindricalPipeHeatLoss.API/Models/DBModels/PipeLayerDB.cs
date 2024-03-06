using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CylindricalPipeHeatLoss.API.Models.DBModels
{
    public class PipeLayerDB
    {
        [Key]
        public int ID { get; set; }

        public ReportDB Report { get; set; }

        public int ReportID { get; set; }

        public int MaterialID { get; set; }

        public MaterialDB Material { get; set; }

        public double Width { get; set; }
    }
}
