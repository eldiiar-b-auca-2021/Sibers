using DataAccessLayer.Entity.Models;
using DataLayer.Enums;

namespace BusinessLogicLayer.Dto.ProjectDtos
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CustomerCompany { get; set; }

        public string ExecutorCompany { get; set; }

        public ProjectPriorityEnum Priority{ get; set; }

        public int? ProjectManagerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
