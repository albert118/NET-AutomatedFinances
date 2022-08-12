import React from 'react';

import BrandHero from './BrandHero';
import FrequentPagesNavBar from './FrequentPagesNavBar';

export default function NavigationContainer() {
    return (
        <header className="navigation-container">
            <BrandHero />
            <FrequentPagesNavBar />
        </header>
    );
};
