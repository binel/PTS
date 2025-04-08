using Microsoft.AspNetCore.Mvc;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase {
    [HttpGet("getAllProjects")]
    public string GetAllProjects() {
        return "todo";
    }

    [HttpGet("getTicketsInProject")]
    public void GetTicketsInProject() {

    }

    [HttpPost("createProject")]
    public void CreateProject() {

    }

    [HttpPost("updateProjectTitle")]
    public void UpdateProjectTitle() {

    }

    [HttpPost("updateProjectDescription")]
    public void updateProjectDescription() {

    }

    [HttpPost("addTicketToProject")]
    public void AddTicketToProject() {

    }

    [HttpPost("removeTicketFromProject")]
    public void removeTicketFromProject() {
        
    }
}