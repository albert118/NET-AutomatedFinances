import React from 'react';
import { Route, Routes  } from 'react-router-dom';

import { ROOT } from './ROUTER_PATH_CONSTANTS';

import HomeContainer from '../pages/Home/HomeContainer';

export default function RouterConfig() {
    return (
        <Routes>
            <Route path={ROOT} element={<HomeContainer />} />
            {/* Redirect to Home until a 404 page is added */}
            <Route path="*" element={<HomeContainer />}/>
        </Routes >
    );
}