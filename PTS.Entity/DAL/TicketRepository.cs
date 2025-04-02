namespace PTS.Entity.DAL;

using PTS.Entity.Domain;

public class TicketRepository {
    // TODO complete 

    public void AddTicket(Ticket ticket) {
        throw new NotImplementedException();
    }

    public void UpdateTicketTitle(int id, string title) {
        throw new NotImplementedException();
    }

    public void UpdateTicketDescription(int id, string description) {
        throw new NotImplementedException();
    }

    public void UpdatePriority(int id, Priority priority) {
        throw new NotImplementedException();
    }

    public void AddComment(int ticketId, Comment comment) {
        throw new NotImplementedException();
    }

    public void DeleteComment(int ticketId, int commentId) {
        throw new NotImplementedException();
    }

    public void AddRelationship(int fromTicketId, int toTicketId) {
        throw new NotImplementedException();
    }

    public void AddTag(int ticketId, Tag tag) {
        throw new NotImplementedException();
    }

    public void RemoveTag(int ticketId, int tagId) {
        throw new NotImplementedException();
    }

    public void AddWorkHistory(int ticketId, WorkHistory workHistory) {
        throw new NotImplementedException();
    }

    public void RemoveWorkHistory(int ticketId, int workHistoryId) {
        throw new NotImplementedException();
    }

    public void AssociateWithProject(int ticketId, int projectId) {
        throw new NotImplementedException();
    }

    public void RemoveProjectAssociation(int ticketId, int projectId) {
        throw new NotImplementedException();
    }

    public string GetNextIdentifierForTicketInProject(int projectKey) {
        // Might not need this, maybe automatically set when ticket created?
        // Need to think more about how projects will work, b/c then removing 
        // project association would be weird 
        throw new NotImplementedException();
    }

    public void UpdateTicketStatus(int ticketId, Status status) {
        throw new NotImplementedException();
    }
}