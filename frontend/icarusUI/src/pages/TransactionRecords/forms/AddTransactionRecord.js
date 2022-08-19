import React, { useState } from 'react';
import { 
    SaveButton,
    CancelButton,
    CurrencyField,
    DateField,
    FormTitle,
    LongTextField,
    TextField,
    KeyGenerator
} from 'forms';


const AddTransactionRecord = () => {
    const [formData, setFormData] = useState({
        txName: '',
        txDescription: '',
        txTotalCost: '',
        txDate: '',
        txReference: '',
    });

    const keyGenerator = KeyGenerator();

    const handleSubmit = event => {
        event.preventDefault();
        console.log(`Submit with ${JSON.stringify(formData)}`);
    }

    const handleFormDataChange = event => {
        setFormData(prevState => ({
            ...prevState,
            [event.target.name]: event.target.value
        }));
    };

    return (
        <div className="transaction-record-grid">
            <div className="form-content-grid--contained">
                <FormTitle title="Transaction Records" />
                <div className="form-field-grid">
                    <TextField
                        className="left-field"
                        inputLabel="Transaction name"
                        prompt="An easy to identify name"
                        value={formData.txName}
                        name="txName"
                        onValueChange={handleFormDataChange}
                        keyGenerator={keyGenerator}
                    />
                    <DateField
                        className="right-field"
                        labelName="Date"
                        value={formData.txDate}
                        name="txDate"
                        onValueChange={handleFormDataChange}
                        keyGenerator={keyGenerator}
                    />
                    <LongTextField
                        className="full-width"
                        labelName="Description"
                        prompt="A brief description of the transaction"
                        value={formData.txDescription}
                        name="txDescription"
                        onValueChange={handleFormDataChange}
                        keyGenerator={keyGenerator}
                    />
                    <CurrencyField
                        className="left-field"
                        inputLabel="Total cost"
                        prompt="$"
                        value={formData.txTotalCost}
                        name="txTotalCost"
                        onValueChange={handleFormDataChange}
                        keyGenerator={keyGenerator}
                    />
                    <TextField
                        className="right-field"
                        inputLabel="(External) transaction reference"
                        prompt="The receipt number typically"
                        value={formData.txReference}
                        name="txReference"
                        onValueChange={handleFormDataChange}
                        keyGenerator={keyGenerator}
                    />
                    <CancelButton className="left-field" />
                    <SaveButton
                        className="right-field"
                        handleClick={handleSubmit}
                    />
                </div>
            </div>
        </div>
    );
};


export default AddTransactionRecord;