using PTS.Entity.Domain;

namespace PTS.Api.Models;

public class AddTicketModel {
    /// <summary>
    /// The id of the identifier that is to be used for this ticket 
    /// </summary>
    public long IdentifierId {get; set;}

    /// <summary>
    /// Title of the ticket 
    /// </summary>
    public string Title {get; set;}

    /// <summary>
    /// Description body of the ticket
    /// </summary>
    public string Description {get; set;}

    /// <summary>
    /// Initial priority of the ticket 
    /// </summary>
    public Priority Priority {get; set;}
}