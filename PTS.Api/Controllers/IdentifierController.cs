using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Api.Models;
using PTS.Entity.DAL;
using PTS.Entity.Domain;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class IdentifierController : ControllerBase {

    private readonly IdentifierRepository _identifierRepo;
    private readonly ILogger<IdentifierController> _logger;

    public IdentifierController(IdentifierRepository identifierRepo, ILogger<IdentifierController> logger) {
        _identifierRepo = identifierRepo;
        _logger = logger;
    }
    
    [HttpGet("getAllIdentifiers")]
    public List<Identifier> GetAllIdentifiers() {
        return _identifierRepo.GetAllIdentifiers();
    }

    [HttpPost("createIdentifier")]
    public IActionResult CreateIdentifier(AddIdentifierModel addModel) {

        // first check that an identifier with that name doesn't already exist
        // todo probably make a specific query for this 
        var identifiers = _identifierRepo.GetAllIdentifiers();
        if (identifiers != null) {
            if (identifiers.Where(i => i.Text.Equals(addModel.Text, StringComparison.InvariantCultureIgnoreCase)).Count() > 0) {
                return BadRequest($"Identifier with text {addModel.Text} already exists");
            }
        }

        Identifier identifier = new Identifier {
            Text = addModel.Text,
            HighestValue = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _identifierRepo.AddIdentifier(identifier);

        var username = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        if (username == null) {
            _logger.LogWarning("Could not determine username!");
        }
        _logger.LogInformation($"User {username} created identifier with text {addModel.Text}");
        return Ok();
    }
}