import React from 'react';
import { useNavigate } from 'react-router-dom'

const CommonButton = props => {
    const { className, buttonText, link } = props;

    let push  = useNavigate();

    const handleClick = () => {
        if (link) push(link);
    };

    return (
        <button 
            type="button" 
            className={`common-btn ${className}`}
            onClick={handleClick}
        >
            { buttonText }
        </button>
    );
};

export default CommonButton;