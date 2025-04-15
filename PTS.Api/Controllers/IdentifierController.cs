using Microsoft.AspNetCore.Mvc;

using PTS.Entity.DAL;
using PTS.Entity.Domain;

namespace PTS.Api.Controllers; 

[ApiController]
[Route("[controller]")]
public class IdentifierController : ControllerBase {

    private readonly IdentifierRepository _identifierRepo;

    public IdentifierController(IdentifierRepository identifierRepo, DatabaseCreator creator) {
        creator.CreateDatabase(1);
        _identifierRepo = identifierRepo;
    }
    
    [HttpGet("getAllIdentifiers")]
    public List<Identifier> GetAllIdentifiers() {
        return _identifierRepo.GetAllIdentifiers();
    }

    [HttpPost("createIdentifier")]
    public void CreateIdentifier(Identifier identifier) {
        _identifierRepo.AddIdentifier(identifier);
    }
}