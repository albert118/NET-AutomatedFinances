import React from 'react';

const TextField = props => {
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

    return(
        <div className={`form-entry-card ${ className }`}>
            <label
                htmlFor={`text-input-${key}`}
                className="form-label--font form-value--label"
                id={`text-label-${key}`}
                value={inputLabel}
            >
                { inputLabel }
            </label>
            <input 
                className="form-value--font no-padding form-input form-text-value"
                id={`text-input-${key}`}
                type="text"
                name={name}
                title={inputLabel}
                placeholder={prompt}
                value={value}
                onChange={onValueChange}
            />
        </div>
    );
};

export default TextField;
