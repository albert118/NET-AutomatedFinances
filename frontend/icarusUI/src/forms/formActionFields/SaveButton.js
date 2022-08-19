import React from 'react';

const SaveButton = props => {
    const { className, handleClick, buttonText = "Save" } = props;

    return (
        <button 
            type="button"
            className={`btn save-btn ${className}`}
            onClick={handleClick}
        >
            { buttonText }
        </button>
    );
};

export default SaveButton;