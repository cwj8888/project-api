using ProjectApi.DataDefinition.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;

namespace ProjectApi.Repositories
{
    public class ProjectRepository
    {

        private readonly IConfiguration _configuration;

        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Create(CreateProject request)
        {
            var sql = "insert into dbo.Projects (ProjectName,FileName,CreatedOn,LastUpdated,ProjectStatus,ProjectType,CreatedBy) VALUES (@ProjectName,@FileName,@CreatedOn,@LastUpdated,@ProjectStatus,@ProjectType,@CreatedBy); select cast(SCOPE_IDENTITY() as int);";
            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                return (int)connection.ExecuteScalar(sql, 
                    new Project { CreatedBy = request.CreatedBy,
                                  CreatedOn = DateTime.Now,
                                  LastUpdated = DateTime.Now,
                                  FileName = request.FileName,  
                                  ProjectName = request.ProjectName,
                                  ProjectStatus = request.ProjectStatus,
                                  ProjectType = request.ProjectType
               });
            }

        }

        public IEnumerable<Project> Retrieve(int[] ids)
        {
            var sql = "select ProjectID, ProjectName, FileName, CreatedOn, LastUpdated, ProjectStatus, ProjectType, CreatedBy from dbo.Projects where ProjectID in @ids";

            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                return connection.Query<Project>(sql, new { ids });
            }
        }

        public int Update(UpdateProject request)
        {
            // retrieve current record so if the request body is missing any parameters we can keep just the original values
            var currentRecord = Retrieve(new int[] { request.ProjectId }).FirstOrDefault();
            if(currentRecord==null) return 0;

            var sql = "update dbo.Projects set ProjectName = @ProjectName,FileName=@FileName,LastUpdated=@LastUpdated,ProjectStatus=@ProjectStatus,ProjectType=@ProjectType where ProjectID = @ProjectID";
            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                return connection.Execute(sql,
                  new
                  {
                      LastUpdated = DateTime.Now,
                      FileName = request.FileName ?? currentRecord.FileName,
                      ProjectId = request.ProjectId,
                      ProjectName = request.ProjectName ?? currentRecord.ProjectName,
                      ProjectStatus = request.ProjectStatus ?? currentRecord.ProjectStatus,
                      ProjectType = request.ProjectType ?? currentRecord.ProjectType
                  });
            }
        }

        public int Delete(int id)
        {
            var sql = "delete from dbo.Projects where ProjectID = @id";

            using (var connection = new SqlConnection(_configuration["ConnectionString"]))
            {
                connection.Open();
                return connection.Execute(sql, new { id });
            }
        }

    }
}
