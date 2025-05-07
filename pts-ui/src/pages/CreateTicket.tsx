import './CreateTicketPage.css';


function CreateTicketPage() {
    const handleSubmit = async () => {
        console.log("fake submit")
    };

    const mockIdentifiers = ['TEST', 'CODE', 'LEARN']
    const mockPriorities = ['None', 'Low', 'Medium', 'High', 'Critical']
    return (
        <div className="app-container">
            <form className="create-ticket-form" onSubmit={handleSubmit}>
                <div>
                    <label>Identifier: </label>
                    <select
                        name="identifier"
                    >
                        {mockIdentifiers.map((p) => (
                            <option key={p} value={p}>{p}</option>
                        ))}

                    </select>
                </div>
                <div>
                    <label>Title: </label>
                    <input
                        type="text"
                        name="title"
                        required
                    />
                </div>
                <div>
                    <label>Description: </label>
                    <textarea
                        name="description"
                        required
                    />
                </div>
                <div>
                    <label>Priority: </label>
                    <select
                        name="priority"
                    >
                        {mockPriorities.map((p) => (
                            <option key={p} value={p}>{p}</option>
                        ))}

                    </select>                    
                </div>
                <button type="submit">Create Ticket</button>

            </form>
        </div>
    )
}

export default CreateTicketPage;