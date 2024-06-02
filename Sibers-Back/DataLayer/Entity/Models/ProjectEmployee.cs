

using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity.Models
{
    public class ProjectEmployee
    {
        public int EmpId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public int PrId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
       
    }
}
