namespace PTS.Entity.Domain;

public class Comment {
    public long Id {get; set;}
    public long AuthorId {get; set;}
    public long TicketId {get; set;}
    public string Content {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
}