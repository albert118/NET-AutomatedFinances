import React from 'react';

export default function NavigationMenu() {
    return (
        <header>
            <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <div>This is a header, it will become a navbar</div>
                <div><button>Click Here</button></div>
                <div><button>Click Here</button></div>
                <div><button>Click Here</button></div>
            </div>
        </header>
    );
};
