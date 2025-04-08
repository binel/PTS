namespace PTS.Entity.Domain;

public class StatusHistory {

    public long Id {get; set;}

    public long TicketId {get; set;}

    public long MoverId {get; set;}

    public Status FromStatus {get; set;}

    public Status ToStatus {get; set;}

    public DateTime CreatedAt {get; set;}
}