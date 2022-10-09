import React from 'react';

export default function BrandHero() {
    const brandHeaderTitle = "Icarus UI";

    return (
        <div className="brand-hero-container">
            <div className="brand-hero-icon" />
            <div className="brand-hero-title">{ brandHeaderTitle }</div>
        </div>
    );
};