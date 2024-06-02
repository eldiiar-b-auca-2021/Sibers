using DataAccessLayer.Entity.Models;
using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dto.ProjectDtos
{
    public class ProjectDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? ProjectManager { get; set; }

        public string CustomerCompany { get; set; }

        public string ExecutorCompany { get; set; }

        public ProjectPriorityEnum Priority { get; set; }

        public int? ProjectManagerId { get; set; }

        public List<EmployeeGetDtoForProject>? Employees { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
