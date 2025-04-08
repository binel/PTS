namespace PTS.Entity.Tests.DAL;

using PTS.Entity.Domain;
using PTS.Entity.DAL;
using PTS.Entity.Util; 

public class CommentRepositoryTests { 
    [Test]
    public void CreateComment() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        CommentRepository repo = new CommentRepository(db.GetConnection());

        var comment = new Comment {
            AuthorId = 1,
            Content = "Test Comment",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment);

        var commentCount = repo.GetCountComments();

        Assert.That(commentCount, Is.EqualTo(1));        
    }

    [Test]
    public void ReadCommentById() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        CommentRepository repo = new CommentRepository(db.GetConnection());

        var comment = new Comment {
            AuthorId = 1,
            Content = "Test Comment",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment);

        var readComment = repo.GetCommentById(1);

        Assert.That(readComment.AuthorId, Is.EqualTo(1));       
        Assert.That(readComment.Content, Is.EqualTo("Test Comment"));
        Assert.That(readComment.TicketId, Is.EqualTo(1));
        Assert.That(DateTimeConverter.StripToSeconds(comment.CreatedAt), Is.EqualTo(readComment.CreatedAt));
        Assert.That(DateTimeConverter.StripToSeconds(comment.UpdatedAt), Is.EqualTo(readComment.UpdatedAt));
    }

    [Test]
    public void GetCommentsForTicket() {
         Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        CommentRepository repo = new CommentRepository(db.GetConnection());

        var comment1 = new Comment {
            AuthorId = 1,
            Content = "Test Comment 1",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment1);

        var comment2 = new Comment {
            AuthorId = 1,
            Content = "Test Comment 2",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment2);        

        var comments = repo.GetCommentsForTicket(1);

        Assert.That(comments.Count(), Is.EqualTo(2));       
    }

    [Test]
    public void UpdateComment() {
         Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        CommentRepository repo = new CommentRepository(db.GetConnection());

        var comment = new Comment {
            AuthorId = 1,
            Content = "Test Comment",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment);

        repo.UpdateComment(1, "New content");

        var readComment = repo.GetCommentById(1);
 
        Assert.That(readComment.Content, Is.EqualTo("New content"));      
    }

    [Test]
    public void DeleteComment() {
        Database db = new Database();
        DatabaseCreator creator = new DatabaseCreator(db.GetConnection());
        creator.CreateDatabase(DatabaseCreator.CURRENT_DB_VERSION);

        CommentRepository repo = new CommentRepository(db.GetConnection());

        var comment = new Comment {
            AuthorId = 1,
            Content = "Test Comment",
            TicketId = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        repo.AddComment(comment);
        repo.DeleteComment(1);
        var commentCount = repo.GetCountComments();

        Assert.That(commentCount, Is.EqualTo(0));            
    }
}