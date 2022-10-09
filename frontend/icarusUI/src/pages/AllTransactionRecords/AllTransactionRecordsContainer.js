import React from 'react';

import AllTransactionRecordsGrid from 'pages/AddTransactionRecord/AllTransactionRecordsGrid';


function AllTransactionRecordsView() {
    return (
        <div className="p-all-transaction-records-view">
            <AllTransactionRecordsGrid />
        </div>
    );
};


export default function TransactionRecordsContainer() {
    return (
        <AllTransactionRecordsView />
    );
};