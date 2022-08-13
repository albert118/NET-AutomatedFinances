import React from 'react';
import { Route, Routes  } from 'react-router-dom';

import { ROOT, TRANSACTION_RECORDS } from './ROUTER_PATH_CONSTANTS';

import HomeContainer from '../pages/Home/HomeContainer';
import TransactionRecordsContainer from '../pages/TransactionRecords/TransactionRecordsContainer';

export default function RouterConfig() {
    return (
        <Routes>
            <Route path={ROOT} element={<HomeContainer />} />
            <Route path={TRANSACTION_RECORDS} element={<TransactionRecordsContainer />} />

            {/* Redirect to Home until a 404 page is added */}
            <Route path="*" element={<HomeContainer />}/>
        </Routes >
    );
}