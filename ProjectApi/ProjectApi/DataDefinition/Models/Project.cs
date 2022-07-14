using ProjectApi.DataDefinition.Enums;

namespace ProjectApi.DataDefinition.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public ProjectType? ProjectType { get; set; }
        public string CreatedBy { get; set; }

    }
}