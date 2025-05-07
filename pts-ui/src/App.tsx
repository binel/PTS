import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import BoardPage from './pages/BoardPage';
import CreateTicketPage from './pages/CreateTicket'
import SamplePage from './pages/SamplePage';

function App() {
  return (
    <Router>
      <div className="app-container">
        <nav className="navbar">
          <Link to="/board">Board</Link>
          <Link to="/createTicket">Create Ticket</Link>
          <Link to="/createIdentifier">Create Identifier</Link>
          <Link to="/users">Users</Link>
          <Link to="/search">Search</Link>
        </nav>

        <Routes>
          <Route path="/" element={<SamplePage />} />
          <Route path="/board" element={<BoardPage />} />
          <Route path="/createTicket" element={<CreateTicketPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;