import React from 'react';
import NavigationMenu from './NavigationMenu/NavigationMenu';

export default function Layout(props) {
    return (
        <div>
            <NavigationMenu />
            { props.children }
        </div>
    );
};
