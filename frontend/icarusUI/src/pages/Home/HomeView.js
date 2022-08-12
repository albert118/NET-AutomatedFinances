import React from 'react';

import Navigation from './components/Navigation';
import CardLayout from './components/CardLayout/CardLayout';

export default function HomeView() {
    return (
        <div className="p-home-view">
            <Navigation />
            <CardLayout />
        </div>
    );
};
