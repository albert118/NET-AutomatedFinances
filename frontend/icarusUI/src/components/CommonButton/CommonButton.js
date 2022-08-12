import React from 'react';

export default function CommonButton(props) {
    return (
        <button type="button" className="common-btn">
            { props.buttonText }
        </button>
    );
};
