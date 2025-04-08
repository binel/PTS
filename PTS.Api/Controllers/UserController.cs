using Microsoft.AspNetCore.Mvc;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    [HttpGet("getAllUsers")]
    public string getAllUsers() {
        return "todo";
    }

    [HttpPost("addUser")]
    public void AddUser() {
        
    }
}