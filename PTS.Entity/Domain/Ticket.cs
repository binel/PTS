namespace PTS.Entity.Domain;


public class Ticket {

    public long Id {get; set;}

    public string Identifier {get; set;}

    public string Title {get; set;}

    public string Description {get; set;}

    public Priority Priority {get; set;}

    public User Author {get; set;}

    public Status Status {get; set;}

    public List<Comment> Comments {get; set;} = new List<Comment>();

    public List<Relationship> Relationships {get; set;} = new List<Relationship>();

    public List<Tag> Tags {get; set;} = new List<Tag>();

    public List<StatusHistory> StatusHistory {get; set;} = new List<StatusHistory>();

    public DateTime CreatedAt {get; set;}

    public DateTime UpdatedAt {get; set;}

    public DateTime? ResolvedAt {get; set;}

    public Project? Project {get; set;}

    public List<WorkHistory> WorkHistory {get; set;} = new List<WorkHistory>();
}