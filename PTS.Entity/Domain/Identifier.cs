namespace PTS.Entity.Domain;

public class Identifier {
    public long Id {get; set;}
    public string Text {get; set;}
    public long HighestValue {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
}