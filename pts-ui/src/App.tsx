import './App.css';
import { useEffect, useState } from 'react';
type Status = 'Someday' | 'To Do' | 'In Progress' | 'Done';


type Ticket = {
  id: number;
  identifier: string;
  title: string;
  description: string;
  status: Status;
};

const mockTickets: Ticket[] = [
  { id: 1, identifier: 'TEST-001', title: 'Fix login bug', description: 'Users can’t log in after 5 PM.', status: 'Someday' },
  { id: 2, identifier: 'TEST-002', title: 'Add dark mode', description: 'Add toggle for dark mode.', status: 'In Progress' },
  { id: 3, identifier: 'TEST-003', title: 'Refactor dashboard', description: 'Clean up dashboard code.', status: 'Done' },
  { id: 4, identifier: 'TEST-004', title: 'Improve performance', description: 'Profile app and optimize.', status: 'To Do' },
];

const statuses: Status[] = ['Someday', 'To Do', 'In Progress', 'Done'];

function App() {
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

export default App;