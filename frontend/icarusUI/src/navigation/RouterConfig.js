import React from 'react';
import { Route, Routes  } from 'react-router-dom';

import AppRoutes from './AppRoutes';

import HomeContainer from 'pages/Home/HomeContainer';
import AddTransactionRecord from 'pages/AddTransactionRecord';
import AllTransactionRecords from 'pages/AllTransactionRecords';


const RouterConfig = () => {
    return (
        <Routes>
            <Route 
                path={AppRoutes.root} 
                element={<HomeContainer />}
            />
            <Route 
                path={AppRoutes.addTransactionRecord} 
                element={<AddTransactionRecord />}
            />
            <Route 
                path={AppRoutes.allTransactionRecords}
                element={<AllTransactionRecords />}
            />

            {/* TODO: Redirect to Home until a 404 page is added */}
            <Route 
                path="*" 
                element={<HomeContainer />}
            />
        </Routes >
    );
};

export default RouterConfig;