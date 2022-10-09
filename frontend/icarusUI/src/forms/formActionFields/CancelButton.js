import React from 'react';

const CancelButton = props => {
    const { className, buttonText = "Cancel" } = props;

    return (
        <button type="button" className={`btn cancel-btn ${className}`}>
            { buttonText }
        </button>
    );
};

export default CancelButton;