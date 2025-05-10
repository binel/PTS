import { useEffect, useState } from 'react';

import './IdentifierPage.css';

type Identifier = {
    id: number;
    text: string;
    highestValue: number;
  };

function IdentifierPage() {
    const [data, setData] = useState<any[]>([]);

    useEffect(() => {
      fetch('http://localhost:5021/identifier/getAllIdentifiers', {headers: {
        Authorization: 'Basic ' + btoa('test:autotest')
      }})
        .then(res => res.json())
        .then(setData);
    }, []);

    const mockIdentifiers = [{id: 1, text: 'TEST', highestValue: 34},
        {id: 2, text: 'CODE', highestValue: 177},
        {id: 3, text: 'LEARN', highestValue: 42}]
    return (
        <div className="app-container">
            <div>
                <button>Add Identifier</button>
            </div>
            <div>
                {mockIdentifiers
                .map(identifier => (
                    <div className="identifier-card" key={identifier.id}>
                        <h3>{identifier.text}</h3>
                        <p>Highest Value: {identifier.highestValue}</p>
                    </div>
                ))}                
            </div>
        </div>
    )
}

export default IdentifierPage;