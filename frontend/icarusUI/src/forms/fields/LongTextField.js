import React from 'react';

const LongTextField = props => {
    const { 
        className,
        labelName,
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
                htmlFor={`long-text-input-${key}`}
                className="form-label--font form-value--label"
                id={`long-text-label-${key}`}
                value={labelName}
            >
                { labelName }
            </label>
            <textarea 
                className="form-long-value--font no-padding form-input form-long-text-value scrollbar-vertical"
                id={`long-text-input-${key}`}
                rows="4"
                name={name}
                title={labelName}
                placeholder={prompt}
                value={value}
                onChange={onValueChange}
            />
        </div>
    );
};

export default LongTextField;
