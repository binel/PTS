namespace PTS.Entity.Domain;


public class Ticket { // TODO redo this to match database 

    public long Id {get; set;}

    public string Identifier {get; set;}

    public string Title {get; set;}

    public string Description {get; set;}

    public Priority Priority {get; set;}

    public long AuthorId {get; set;}

    public Status Status {get; set;}

    public DateTime CreatedAt {get; set;}

    public DateTime UpdatedAt {get; set;}

    public DateTime? ResolvedAt {get; set;}
}