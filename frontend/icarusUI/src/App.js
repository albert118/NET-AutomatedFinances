import './icarus-theme/styles.css'; 

import RouterConfig from './navigation/RouterConfig';
import { BrowserRouter } from 'react-router-dom';

export default function App() {
    return (
        <div className="icarus-monochromatic-blue-theme icarus-square-contrast-theme">
            <BrowserRouter>
                <RouterConfig />
            </BrowserRouter>
        </div>
    );
}
