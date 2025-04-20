using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PTS.Api.Models;
using PTS.Entity.DAL;
using PTS.Entity.Domain;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase {

    private readonly TicketRepository _ticketRepository;
    private readonly IdentifierRepository _identifierRepository;

    private readonly ILogger<TicketController> _logger;

    public TicketController(ILogger<TicketController> logger,
        TicketRepository ticketRepository,
        IdentifierRepository identifierRepository) {
        _ticketRepository = ticketRepository;
        _identifierRepository = identifierRepository;
        _logger = logger;
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

        // verify identifier 
        if (addTicketModel.IdentifierId == 0) {
            return BadRequest("IdentifierId must be provided");
        }

        var identifier = _identifierRepository.GetIdentifierById(addTicketModel.IdentifierId);

        if (identifier == null) {
            return BadRequest($"Identifier with id {addTicketModel.IdentifierId} does not exist");
        }

        var authorId = HttpContext.User.FindFirst("UserId")?.Value;
        if (authorId == null) {
            _logger.LogWarning("Could not determine authorId!");
            return BadRequest("Could not determine author Id");
        }

        string ticketIdentifier = $"{identifier.Text}-{identifier.HighestValue + 1}";

        _identifierRepository.UpdateHighestValue(identifier.Id, identifier.HighestValue + 1);

        Ticket newTicket = new Ticket {
            AuthorId = long.Parse(authorId),
            Identifier = ticketIdentifier,
            Title = addTicketModel.Title,
            Description = addTicketModel.Description,
            Priority = addTicketModel.Priority,
            Status = Status.Someday,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (username == null) {
            _logger.LogWarning("Could not determine username!");
        }

        _ticketRepository.AddTicket(newTicket);
        _logger.LogInformation($"Ticket {newTicket.Identifier} created by user {username}");
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