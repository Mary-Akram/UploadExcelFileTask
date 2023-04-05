using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UploadExcelFileTask.DTO
{
    public class DetailsOfAais
    {
       
        public int WO_Number { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public DateTime Scheduled_FinishDate { get; set; }
        public DateTime Completion_Date { get; set; }
        public DateTime StartDate { get; set; }
        public string State { get; set; }
        public string Reason { get; set; }
    }
}
