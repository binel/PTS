namespace PTS.Entity.Domain;

public class WorkHistory { 
    public long Id {get; set;}
    public long TicketId {get; set;}
    public DateTime StartedAt {get; set;}
    public DateTime EndedAt {get; set;}
    public DateTime CreatedAt {get; set;}
}