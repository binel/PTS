using Microsoft.AspNetCore.Mvc;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase {
    [HttpGet("ticketById")]
    public string GetTicketByIdentifier() {
        return "todo";
    }

    [HttpGet("ticketsInStatus")]
    public string GetTicketsInStatus() {
        return "todo";
    }

    [HttpPost("createTicket")]
    public void CreateTicket() {
        
    }

    [HttpPost("updateTitle")]
    public void UpdateTicketTitle() {

    }

    [HttpPost("updateDescription")]
    public void UpdateTicketDescription() {

    }

    [HttpPost("addComment")]
    public void AddCommentToTicket() {

    }

    [HttpPost("updateComment")]
    public void UpdateTicketComment() {

    }

    [HttpPost("deleteComment")]
    public void DeleteTicketComment() {

    }

    [HttpPost("changeTicketStatus")]
    public void ChangeTicketStatus() {

    }

    [HttpPost("addTag")]
    public void AddTagToTicket() {

    }

    [HttpPost("removeTag")]
    public void RemoveTagFromTicket() {

    }

    [HttpPost("addWorkHistory")]
    public void AddWorkHistory() {

    }
}