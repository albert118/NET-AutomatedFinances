import React from 'react';

const CurrencyField = props => {
    const { 
        className,
        inputLabel,
        prompt,
        value,
        name,
        onValueChange,
        keyGenerator = null
    } = props;
    
    const key = keyGenerator ? keyGenerator.next().value : 0;
    const _label = inputLabel ? inputLabel : 'Dollar Value';

    return(
        <div className={`form-entry-card ${ className }`}>
            <label
                htmlFor={`monetary-input-${key}`}
                className="form-label--font form-value--label"
                id={`monetary-label-${key}`}
                value={_label}
            >
                { _label }
            </label>
            <input 
                className="form-value--font no-padding form-input form-small-monetary-entry-value"
                id={`text-input-${key}`}
                type="currency"
                name={name}
                title={_label}
                value={value}
                placeholder={prompt}
                onChange={onValueChange}
            />
        </div>
    );
};

export default CurrencyField;
