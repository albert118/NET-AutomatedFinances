import React from 'react';

// TODO: set a maximum date of today (future dating transactions should not be valid)
const DateField = props => {
    const { 
        className,
        labelName,
        value,
        name,
        onValueChange,
        keyGenerator = null
    } = props;
    
    const key = keyGenerator ? keyGenerator.next().value : 0;

    return(
        <div className={`form-entry-card ${ className }`}>
            <label
                htmlFor={`date-input-${key}`}
                className="form-label--font form-value--label"
                id={`date-label-${key}`}
                value={labelName}
            >
                { labelName }
            </label>
            <input 
                className="form-value--font no-padding form-input form-small-date-entry-value"
                id={`date-input-${key}`}
                type="date"
                name={name}
                title={labelName}
                value={value}
                onChange={onValueChange}
            />
        </div>
    );
};

export default DateField;