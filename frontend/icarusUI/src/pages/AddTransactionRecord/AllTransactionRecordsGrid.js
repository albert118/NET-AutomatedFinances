import React, { useState, useEffect } from "react";
import server from "Server";


// TODO: Refactor me and a nice spinner!
const LoadingWaiter = () => {
    return (
        <div className="all-transaction-record-grid">
            <div className="list-content-grid--contained">
                Loading ...
            </div>
        </div>
    );
};


const TransactionRecord = props => {
    const { name, description, occuredAtDateTime } = props;

    
    return(
        <div className="transaction-record-card">
            <label className="form-label--font form-value--label" value={ name }>
                { name }
            </label>
            <div>
                { occuredAtDateTime }
                <br />
                { description }
            </div>
        </div>
    )
};


export default function AllTransactionRecordsGrid() {
    const [isLoading, setLoading] = useState(true);
    
    const [records, setRecords] = useState([]);
    
    useEffect(() => {
        loadRecords();
    });
    
    const loadRecords = async () => {
        setRecords(await server.get(`/TransactionRecord`));
        setLoading(false);
    };

    if (isLoading) 
        return (<LoadingWaiter />);

    return (
        <div className="all-transaction-record-grid">
            <div className="list-content-grid--contained">
                <h2 className="form-title--font">
                    All Transaction Records
                </h2>
                { records.map(tx => {
                    return <TransactionRecord 
                        key={tx.savedAtDateTime} 
                        name={tx.name} 
                        description={tx.description} 
                        occuredAtDateTime={tx.occuredAtDateTime} 
                    />;
                })}
            </div>
        </div>
    );
}
