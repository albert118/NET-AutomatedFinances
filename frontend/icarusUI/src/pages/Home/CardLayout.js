import React from 'react';

export default function CardLayout() {
    const cardLayoutStyle = {
        display: 'grid',
        gridTemplateColumns: 'repeat(3, 1fr)',
        gridTemplateRows: 'repeat(3, 20vh)',
        gap: '20px',
        margin: '20px 0 20px 0',
        fontFamily: 'sans-serif'
    };

    const cardStyle = {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        backgroundColor: 'aliceblue'
    };

    return (
        <div style={cardLayoutStyle}>
            <div style={cardStyle}>Content</div>
            <div style={cardStyle}></div>
            <div style={cardStyle}>Content</div>
            <div style={cardStyle}></div>
            <div style={cardStyle}>Content</div>
            <div style={cardStyle}></div>
            <div style={cardStyle}>Content</div>
            <div style={cardStyle}></div>
            <div style={cardStyle}>Content</div>
        </div>
    );
};
