import { useEffect, useState } from 'react';

import './IdentifierPage.css';

function IdentifierPage() {
    const [data, setData] = useState<any[]>([]);

    useEffect(() => {
      fetch('http://localhost:5021/identifier/getAllIdentifiers', {headers: {
        Authorization: 'Basic ' + btoa('test:autotest')
      }})
        .then(res => res.json())
        .then(setData);
    }, []);

    return (
        <div className="app-container">
            <div>
                <button>Add Identifier</button>
            </div>
            <div>
                {data
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