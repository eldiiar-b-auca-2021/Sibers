using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataAccessLayer.Entity.Models
{
    public class Project 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("customer_company")]
        [StringLength(255)]
        public string CustomerCompany { get; set; }

        [Column("executor_company")]
        [StringLength(255)]
        public string ExecutorCompany { get; set; }

        [Column("priority")]
        public ProjectPriorityEnum Priority { get; set; }

        [Column("project_manager_id")]
        public int? ProjectManagerId { get; set; }

        [ForeignKey("ProjectManagerId")]
        public Employee ProjectManager { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }


        [Column("end_date")]
        public DateTime EndDate { get; set; }

        public List<ProjectEmployee> EmployeeProjects { get; set; } = new List<ProjectEmployee>();
    }
}
