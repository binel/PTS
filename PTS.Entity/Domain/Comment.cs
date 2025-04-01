namespace PTS.Entity.Domain;

public class Comment {
    public int Id {get; set;}
    public User Author {get; set;}
    public string Content {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public bool Deleted {get; set;}
}