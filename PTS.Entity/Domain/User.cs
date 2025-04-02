namespace PTS.Entity.Domain;

public class User {
    public int Id {get; set;}

    public string Username {get; set;}

    public string DisplayName {get; set;}

    public string Description {get; set;}

    public DateTime CreatedAt {get; set;}
    
    public DateTime UpdatedAt {get; set;}

    public int PasswordHashVersion {get; set;}

    public string PasswordHash {get; set;}

    public DateTime PasswordUpdatedAt {get; set;}

    public DateTime LastLoginAt {get; set;}
}