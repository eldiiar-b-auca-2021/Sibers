using DataAccessLayer.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace BusinessLogicLayer.Dto.ProjectDtos
{
    public class ProjectPostDto
    {   
        public string Name { get; set; }

        public string CustomerCompany { get; set; }

        public string ExecutorCompany { get; set; }

        public int Priority { get; set; }

        public int ProjectManagerId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<int>? EmployeeIds { get; set; }
    }
}
