import React from 'react';

export default function NavigationMenu() {
    const menuBarWrapperStyle = {
        backgroundColor: 'black',
        marginBottom: '20px',
        height: '6vh',
        width: '100%'
    };

    const menuBarStyle = {
        display: 'flex',
        width: '100%',
    };

    const brandHeaderWrapperStyle = {
        display: 'flex',
        alignItems: 'center',
        margin: '8px',
        width: '20%',
        padding: '0 10px 0 10px'
    };

    const brandHeaderStyle = {
        fontSize: '24px',
        fontFamily: 'sans-serif',
        color: 'white'
    };

    const buttonGroupWrapperStyle = {
        display: 'flex',
        width: '80%',
        alignItems: 'center',
        justifyContent: 'space-between',
    };

    const buttonWrapperStyle = {
        padding: '15px',
        width: '100%'
    };

    const buttonStyle = {
        fontFamily: 'sans-serif',
        borderRadius: '4px',
        padding: '6px',
        width: '100%',
        backgroundColor: 'white',
        border: '1px solid blueviolet'
    };

    return (
        <header style={menuBarWrapperStyle}>
            <div style={menuBarStyle}>
                <div style={brandHeaderWrapperStyle}>
                    <div style={brandHeaderStyle}>
                        Icarus UI
                    </div>
                </div>

                <div style={buttonGroupWrapperStyle}>
                    <div style={buttonWrapperStyle}>
                        <button type="button" style={buttonStyle}>Demo Button to Nowhere #1</button>
                    </div>
                    <div style={buttonWrapperStyle}>
                        <button type="button" style={buttonStyle}>Bang!</button>
                    </div>
                    <div style={buttonWrapperStyle}>
                        <button type="button" style={buttonStyle}>Do not click more ... don't do it</button>
                    </div>
                </div>
            </div>
        </header>
    );
};
