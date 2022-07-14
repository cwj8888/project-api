using ProjectApi.DataDefinition.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DataDefinition.Models
{
    public class CreateProject
    {
        public string ProjectName { get; set; }
        public string FileName { get; set; }
        public ProjectStatus? ProjectStatus { get; set; }
        public ProjectType? ProjectType { get; set; }
        public string CreatedBy { get; set; }
    }
}