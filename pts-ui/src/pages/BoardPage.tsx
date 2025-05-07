import { useEffect, useState } from 'react';
type Status = 'Someday' | 'To Do' | 'In Progress' | 'Done';


type Ticket = {
  id: number;
  identifier: string;
  title: string;
  description: string;
  status: Status;
};

const statuses: Status[] = ['Someday', 'To Do', 'In Progress', 'Done'];

function BoardPage() {
  const [data, setData] = useState<any[]>([]);

  useEffect(() => {
    fetch('http://localhost:5021/ticket/ticketsInStatus', {headers: {
      Authorization: 'Basic ' + btoa('test:autotest')
    }})
      .then(res => res.json())
      .then(setData);
  }, []);
  
  return (
    <div className="app-container">
      <h1>PTS</h1>
      <div className="board">
        {statuses.map(status => (
          <div className="column" key={status}>
            <h2>{status}</h2>
            {data
              //.filter(ticket => ticket.status === status)
              .map(ticket => (
                <div className="ticket-card" key={ticket.id}>
                  <h3>{ticket.identifier}</h3>
                  <h4>{ticket.title}</h4>
                  <p>{ticket.description}</p>
                </div>
              ))}
          </div>
        ))}
      </div>
    </div>
  );
}

export default BoardPage;