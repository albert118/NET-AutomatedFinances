import CommonButton from 'components/CommonButton';
import React from 'react';
import FormTitle from './FormTitle';
import TextField from './TextField';

export default function AddTransactionRecord() {
    return (
        <div className="transaction-record-grid">
            <div className="form-content-grid--contained">
                <FormTitle title="Transaction Records" />
                <div className="form-field-grid">
                    <TextField className="left-field" label="First name" prompt="Albert"/>
                    <TextField className="right-field" label="Last name" prompt="Ferguson" />
                    <TextField className="full-width" label="Email" prompt="albertferguson@test.com" />
                    <TextField className="full-width" label="Password" prompt="**************" />
                    <br />
                    <CommonButton className="left-field" buttonText="Cancel"/>
                    <CommonButton className="right-field" buttonText="Submit"/>
                </div>
            </div>
        </div>
    );
};
