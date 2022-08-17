import React from 'react';
import AddTransactionRecord from './components/Forms/AddTransactionRecord';
import PageHeader from './components/PageHeader';

export default function TransactionRecordsView() {
    return (
        <div className="p-transaction-records-view">
            <PageHeader />
            <AddTransactionRecord />
        </div>
    );
};
