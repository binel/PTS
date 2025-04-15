using Microsoft.AspNetCore.Mvc;
using PTS.Api.Models;
using PTS.Entity.DAL;
using PTS.Entity.Domain;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase {

    private readonly TicketRepository _ticketRepository;
    private readonly UserRepository _userRepository;
    private readonly IdentifierRepository _identifierRepository;

    public TicketController(TicketRepository ticketRepository,
        UserRepository userRepository,
        IdentifierRepository identifierRepository) {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _identifierRepository = identifierRepository;
    }

    [HttpGet("ticketById")]
    public string GetTicketByIdentifier() {
        return "todo";
    }

    [HttpGet("ticketsInStatus")]
    public string GetTicketsInStatus() {
        return "todo";
    }

    [HttpPost("createTicket")]
    public IActionResult CreateTicket(AddTicketModel addTicketModel) {

        // verify author, get from header? 

        // verify identifier 
        if (addTicketModel.IdentifierId == 0) {
            return BadRequest("IdentifierId must be provided");
        }

        var identifier = _identifierRepository.GetIdentifierById(addTicketModel.IdentifierId);

        if (identifier == null) {
            return BadRequest($"Identifier with id {addTicketModel.IdentifierId} does not exist");
        }

        string ticketIdentifier = $"{identifier.Text}-{identifier.HighestValue + 1}";

        _identifierRepository.UpdateHighestValue(identifier.Id, identifier.HighestValue + 1);

        Ticket newTicket = new Ticket {
            AuthorId = 1, // TODO complete
            Identifier = ticketIdentifier,
            Title = addTicketModel.Title,
            Description = addTicketModel.Description,
            Priority = addTicketModel.Priority,
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _ticketRepository.AddTicket(newTicket);

        return Ok();
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