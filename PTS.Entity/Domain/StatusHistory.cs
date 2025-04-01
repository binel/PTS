namespace PTS.Entity.Domain;

public class StatusHistory {

    public int Id {get; set;}

    public User Mover {get; set;}

    public Status FromStatus {get; set;}

    public Status ToStatus {get; set;}

    public DateTime MovedAt {get; set;}
}