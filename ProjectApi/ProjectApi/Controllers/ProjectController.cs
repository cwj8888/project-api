using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.DataDefinition.Models;
using ProjectApi.Repositories;
using System.Text.RegularExpressions;

namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("Project")]
    public class ProjectController : Controller
    {

        private readonly ProjectRepository _projectRepository;  
        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(CreateProject request)
        {
            var r = new Regex(@"^[^\\]*\.\w+$");

            if (!r.Match(request.FileName).Success)
                return BadRequest("The filename must include an extension.");

           
            return Ok(_projectRepository.Create(request)); 
        }

        [HttpPost]
        [Route("Retrieve")]
        public ActionResult Retrieve(int[] projectIds)
        {
            var result = _projectRepository.Retrieve(projectIds);
            return _projectRepository.Retrieve(projectIds).Any() ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Route("Update")]
        public ActionResult Update(UpdateProject request)
        {
            var r = new Regex(@"^[^\\]*\.\w+$");

            if (!r.Match(request.FileName).Success)
                return BadRequest("The filename must include an extension.");

            return _projectRepository.Update(request) > 0 ? Ok() : NotFound();
           
        }

        [HttpPost]
        [Route("Delete")]
        public ActionResult Delete(int projectId)
        {
            return _projectRepository.Delete(projectId) > 0 ? Ok() : NotFound();
        }

    }
}
