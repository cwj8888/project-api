using ProjectApi.DataDefinition.Enums;

namespace ProjectApi.DataDefinition.Models
{
    public class UpdateProject
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string FileName { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public ProjectType? ProjectType { get; set; }
    }
}