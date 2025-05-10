import './App.css';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import BoardPage from './pages/BoardPage';
import CreateTicketPage from './pages/CreateTicket'
import SamplePage from './pages/SamplePage';
import IdentifierPage from './pages/IdentifierPage';

function App() {
  return (
    <Router>
      <div className="app-container">
        <nav className="navbar">
          <Link to="/board">Board</Link>
          <Link to="/createTicket">Create Ticket</Link>
          <Link to="/identifiers">Identifiers</Link>
          <Link to="/users">Users</Link>
          <Link to="/search">Search</Link>
        </nav>

        <Routes>
          <Route path="/" element={<SamplePage />} />
          <Route path="/identifiers" element={<IdentifierPage />} />
          <Route path="/board" element={<BoardPage />} />
          <Route path="/createTicket" element={<CreateTicketPage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;