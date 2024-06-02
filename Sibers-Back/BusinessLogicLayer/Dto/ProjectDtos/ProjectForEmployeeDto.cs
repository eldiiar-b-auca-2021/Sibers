using DataLayer.Enums;


namespace BusinessLogicLayer.Dto.ProjectDtos
{
    public class ProjectForEmployeeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ExecutorCompany { get; set; }

        public string CustomerCompany { get; set; }

        public ProjectPriorityEnum Priority { get; set; }

        public int? ProjectManagerId { get; set; }

    }
}
