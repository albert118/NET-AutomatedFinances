import React from 'react';

const CommonButton = props => {
    const { className, buttonText } = props;

    return (
        <button type="button" className={`common-btn ${className}`}>
            { buttonText }
        </button>
    );
};

export default CommonButton;