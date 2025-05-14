import { useEffect, useState } from 'react';
import './IdentifierPage.css';
import Modal from '../components/Modal';

function IdentifierPage() {
  const [newIdentifierTitle, setNewIdentifierTitle] = useState('');
  const [data, setData] = useState<any[]>([]);
  const [isModalOpen, setIsModalOpen] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [isErrorModalOpen, setIsErrorModalOpen] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);

  const loadIdentifiers = () => {
    fetch('http://localhost:5021/identifier/getAllIdentifiers', {
        headers: {
          Authorization: 'Basic ' + btoa('test:autotest'),
        },
      })
        .then((res) => {
          if (!res.ok) {
            throw new Error(`Server responded with status ${res.status}`);
          }
          return res.json();
        })
        .then(setData)
        .catch((err) => {
          console.error('Failed to fetch identifiers:', err);
          setError(err.message || 'Unknown error');
          setIsErrorModalOpen(true);
        })
        .finally(() => setLoading(false));
  }

  useEffect(() => {
    loadIdentifiers();
  }, []);

  const handleCreateIdentifier = () => {
    const requestBody = {
        Text: newIdentifierTitle
    };

    fetch('http://localhost:5021/identifier/createIdentifier', {
        method: 'POST',
        headers: {
            Authorization: 'Basic ' + btoa('test:autotest'),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            if (!res.ok) throw new Error('Failed to submit new identifier');
            return res.text;
        })
        .then(() => {
            setIsModalOpen(false);
            loadIdentifiers();
        })
        .catch((err) => {
            console.error(error);
        })
  }

  return (
    <div className="app-container">
      <div>
        <button onClick={() => setIsModalOpen(true)}>Create New Identifier</button>

        <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
          <h2>Create Identifier</h2>
          <div>
            <input
                type="text"
                placeholder="Identifier Title"
                value={newIdentifierTitle}
                onChange={(e) => setNewIdentifierTitle(e.target.value)}
            />
            <button onClick={handleCreateIdentifier}>Create</button>
          </div>
        </Modal>
      </div>

      {loading && <p>Loading...</p>}
      {error && <p>Something went wrong getting identifiers - try again</p>}
      <Modal isOpen={isErrorModalOpen} onClose={() => setIsErrorModalOpen(false)}>
            <h2>Error Getting Identifiers</h2>
            <p>{error}</p>
      </Modal>

      <div>
        {data.map((identifier) => (
          <div className="identifier-card" key={identifier.id}>
            <h3>{identifier.text}</h3>
            <p>Highest Value: {identifier.highestValue}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default IdentifierPage;