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
        throw new NotImplementedException();
    }

    [HttpGet("ticketsInStatus")]
    public ActionResult<List<Ticket>> GetTicketsInStatus() {
        // Mocked out to test the UI 

        var tickets = new List<Ticket> {
            new Ticket {
                Id = 1,
                Identifier = "HARD-001",
                Title = "Hardcoded Ticket",
                Description = "This is a hardcoded ticket",
                Priority = Priority.High,
                AuthorId = 1,
                Status = Status.InProgress,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        return Ok(tickets);
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
        throw new NotImplementedException();
    }

    [HttpPost("updateDescription")]
    public void UpdateTicketDescription() {
        throw new NotImplementedException();
    }

    [HttpPost("addComment")]
    public void AddCommentToTicket() {
        throw new NotImplementedException();
    }

    [HttpPost("updateComment")]
    public void UpdateTicketComment() {
        throw new NotImplementedException();
    }

    [HttpPost("deleteComment")]
    public void DeleteTicketComment() {
        throw new NotImplementedException();
    }

    [HttpPost("changeTicketStatus")]
    public void ChangeTicketStatus() {
        throw new NotImplementedException();
    }

    [HttpPost("addTag")]
    public void AddTagToTicket() {
        throw new NotImplementedException();
    }

    [HttpPost("removeTag")]
    public void RemoveTagFromTicket() {
        throw new NotImplementedException();
    }

    [HttpPost("addWorkHistory")]
    public void AddWorkHistory() {
        throw new NotImplementedException();
    }
}