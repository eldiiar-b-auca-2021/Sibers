using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entity.Models
{
    public class Employee 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Column("first_name")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Column("middle_name")]
        [StringLength(255)]
        public string MiddleName { get; set; }

        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("position")]
        [StringLength(255)]
        public string Position { get; set; }

        public List<ProjectEmployee>? EmployeeProjects { get; set; } = new List<ProjectEmployee>();
    }
}
