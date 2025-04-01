# Design 

This file describes the overall design of PTS 

## Core Architecture 

PTS is a collection of smaller systems.

### PTS.App 

PTS.App is the website frontent for PTS. 

### PTS.Api 

PTS.Api is is a .NET WebAPI project that provides public APIs used by the website, and are also available for other systems to integrate with. Every API used by the website is provided in PTS.Api. 

### PTS.Installer 

This is a .net executable that is responsible for installing, upgrading, and uninstalling PTS from a system. It is capable of automatically checking for updates and installing them 
as needed. 

### PTS.Entity 

This is a .net library that is responsible for containing the common objects used by PTS, and also the logic for working with the Sqlite database that powers a single installation of PTS. 

### Sqlite Database

This is a sqlite database that is used by PTS.Entity for long term storage of data. 

#### Table Schema 

*pts_metadata*
pts_metadata stores information about PTS itself, including the version of the database, when it was last updated, and various database level configurations

Columns: 
- id (Integer) (Primary Key)
- dataKey (TEXT) - the name of whatever bit of metatdata is being stored 
- dataValue (TEXT) - whatever value is set for the given key. 

Contents: 
- 0 | dbVersion | 0.0.1
- 1 | dbLastUpdated | 2025-04-01 00:00:00
- 2 | ptsLastUpdated | 
- 3 | ptsNewestVersion | 
- 4 | ptsLastNewVersionCheck | 


*tickets*
the tickets table stores information about every ticket in the system. A ticket is the smallest unit of work in PTS. Tickets can have: 
- An Identifier (Required) - the identifier shown for a ticket. By default the identifier is "PTS-0", which increments as more tickets are added. If the ticket is part of a project, then the project prefix would be used. For example if the user had a project for fitness goals, a ticket might be "FIT-103"
- A Title (Required) - a high-level summary of the ticket. 
- A Description (Optional) - a more in-depth free-text summary of the ticket. 
- Priority (Required) - a priority setting for the ticket. Can be None, Low, Medium, High. 
- Status (Required) - a status that the ticket is in. Options are "Someday", "ToDo", "Blocked", "In Progress", "Done", or "Cancelled". 
- Comments (Optional) - a series of timestamped free-text messages associated with the ticket
- Tags (Optional) - a series of small snippets of text that can be used to categorize tickets together 
- Project (Optional) - a ticket can be a part of a project, which is a collection of other tickets with some additional metadata. 
- Relationships (Optional) - a ticket can have relationships with other tickets, such as "Blocks", "Related To", "Cloned From", etc 
- StatusHistory (required) - a history of all the status transitions this ticket has gone through, including the time it went through those status transitions. 
- WorkHistory (optional) - the user can "clock in" and "clock out" of tickets if they want to track the specific amount of time they spent on a particular project. This tracks that time. 
- Author (required) - although PTS is only meant for a single user to use it, it is designed to that extensions can automatically create tickets. 
- CreatedAt (required) - time the ticket was created 
- UpdatedAt (required) - the last time the ticket was updated. Initially set to the same value as CreatedAt
- ResolvedAt (optional) - the time the ticket moved into either ("Done") or ("Cancelled") 


Columns: 
- id (INTEGER) (PRIMARY KEY) 
- Identifier (TEXT) (NOT NULL) 
- Title (TEXT) (NOT NULL) 
- Description (TEXT) 
- Priority (INTEGER) (NOT NULL) 
- AuthorKey (INTEGER) (NOT NULL) - foreign key to users table 
- Status (INTEGER) (NOT NULL) - foreign key to status table 
- HasComments (INTEGER) (NOT NULL) - 0 if there are no comments, 1 if there are 
- HasRelationships (INTEGER) (NOT NULL) - 0 if there are no relationships, 1 if there are
- HasTags (INTEGER) (NOT NULL) - 0 if there are no tags, 1 if there are
- ProjectKey (INTEGER) - foreign key to relationships table 
- HasWorkHistory (INTEGER) (NOT NULL) - 0 if there is no work history, 1 if there is 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 
- UpdatedAt (INTEGER) (NOT NULL) - unix timestamp 
- ResolvedAt (INTEGER) (NOT NULL) - unix timestamp

*status* 
Listing of the different possible status 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- Name (TEXT) (NOT NULL) 

Contents: 
- 0 | Someday 
- 1 | ToDo 
- 2 | In Progress
- 3 | Blocked 
- 4 | Done
- 5 | Cancelled 

*comments* 

The comments table stores comments on a ticket. 

Columns:
- id (INTEGER) (PRIMARY KEY) 
- AuthorKey (INTEGER) (NOT NULL) - foreign key to users table 
- TicketKey (INTEGER) (NOT NULL) - foreign key to tickets table  
- Content (TEXT) (NOT NULL) 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 
- UpdatedAt (INTEGER) (NOT NULL) - unix timestamp 
- Deleted (INTEGER) (NOT NULL) - 0 if active, 1 if deleted 

*tags*
Stores tags themselves. tags are a lightweight way to group tickets together, or to categorize them.

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- Name (TEXT) (NOT NULL) - the name of the tag 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 

*tag_mapping* 

Stores relationships between tags and tickets. 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- TicketKey (INTEGER) (NOT NULL) - foreign key to tickets table 
- TagKey (INTEGER) (NOT NULL) - foreign key to tags table 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 

*ticket_work_history*

Stores the history of work time that was associated with a ticket 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- TicketKey (INTEGER) (NOT NULL) - foreign key to tickets table 
- StartedAt (INTEGER) (NOT NULL) - unix timestamp 
- EndedAt (INTEGER) (NOT NULL) - unix timestamp

*ticket_status_history*

Stores the history of status transitions that a ticket went through 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- TicketKey (INTEGER) (NOT NULL) - foreign key to tickets table 
- MoverKey (INTEGER) (NOT NULL) - foreign key to users table for who made the move 
- FromStatus (INTEGER) (NOT NULL) - foreign key to status table for the starting status
- ToStatus (INTEGER) (NOT NULL) - foreign key to the status table for the ending status
- At (INTEGER) (NOT NULL) - unix timestamp for when the transition happened. 

*relationships* 
stores the possible relationship types 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- NameFrom (TEXT) - the name of the relationship on the "from" ticket 
- NameTo (TEXT) - the name of the relationship on the "to" ticket 

Contents: 
- 0 | Blocks | Is Blocked By

*relationship_mapping*
stores what mappings a ticket has with other tickets 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- RelationshipKey (INTEGER) (NOT NULL) - foreign key to relationships table 
- FromTicketKey (INTEGER) (NOT NULL) - foreign key to the tickets table 
- ToTicketKey (INTEGER) (NOT NULL) - foreign key to the tickets table 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp

*projects*
stores the different projects in the system. A project is like a super-ticket - it contains other tickets. All the tickets need to be done for a project to be complete. Projects can have a status in the same way a ticket can. 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- Identifier (TEXT) (NOT NULL) 
- Title (TEXT) (NOT NULL) 
- Description (TEXT) 
- Priority (INTEGER) (NOT NULL) 
- AuthorKey (INTEGER) (NOT NULL) - foreign key to users table 
- Status (INTEGER) (NOT NULL) - foreign key to status table 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 
- UpdatedAt (INTEGER) (NOT NULL) - unix timestamp 
- ResolvedAt (INTEGER) (NOT NULL) - unix timestamp

*project_mapping*
Maps what tickets are in what project 

Columns: 
- id (INTEGER) (PRIMARY KEY)
- ProjectKey (INTEGER) (NOT NULL) - foreign key to project table 
- TicketKey (INTEGER) (NOT NULL) - foreign key to tickets table 
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 


*users*
stores users of the system (most of which will be automated systems) 

Columns: 
- id (INTEGER) (PRIMARY KEY) 
- Username (TEXT) (NOT NULL)
- DisplayName (TEXT) (NOT NULL) 
- Description (TEXT) - some description of a user, useful for automated users
- CreatedAt (INTEGER) (NOT NULL) - unix timestamp 
- PasswordHashVersion (INTEGER) (NOT NULL) - what version of the password hashing algorithm was used for this password
- PasswordHash (TEXT) (NOT NULL) - hash of the password of the user 
- PasswordUpdatedAt (INTEGER) (NOT NULL) - unix timestamp 
- LastLoginAt (INTEGER) - unix timestamp 