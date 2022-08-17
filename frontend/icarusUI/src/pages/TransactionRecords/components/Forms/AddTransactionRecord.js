import CommonButton from 'components/CommonButton';
import React from 'react';
import FormTitle from './FormTitle';
import TextField from './TextField';
import DateField from './DateField';
import CurrencyField from './CurrencyField';

export default function AddTransactionRecord() {
    return (
        <div className="transaction-record-grid">
            <div className="form-content-grid--contained">
                <FormTitle title="Transaction Records" />
                <div className="form-field-grid">
                    <TextField className="left-field" label="Transaction name" prompt="An easy to identify name"/>
                    <DateField className="right-field" label="Date"/>
                    <TextField className="full-width" label="Description" prompt="A brief description of the transaction" />
                    <CurrencyField className="left-field" label="Total cost" prompt="$" />
                    <TextField className="right-field" label="(External) transaction reference" prompt="The receipt number typically" />
                    
                    <CommonButton className="left-field" buttonText="Cancel"/>
                    <CommonButton className="right-field" buttonText="Submit"/>
                </div>
            </div>
        </div>
    );
};
