import React from 'react';

import CommonButton from '../CommonButton/CommonButton';

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
        display: 'flex',
        width: '100%',
        alignItems: 'center',
        justifyContent: 'center',
        padding: '2px',
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
                        <CommonButton buttonText="Button to Nowhere #1" />
                    </div>
                    <div style={buttonWrapperStyle}>
                        <CommonButton buttonText="Bang!" />
                    </div>
                    <div style={buttonWrapperStyle}>
                        <CommonButton buttonText="Do not click more ... don't do it" />
                    </div>
                </div>
            </div>
        </header>
    );
};
