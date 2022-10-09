import React from 'react';

import CommonButton from 'components';
import AppRoutes from 'navigation/AppRoutes';

export default function FrequentPagesNavBar() {
    return (
        <div className="frequent-pages-bar">
            <CommonButton buttonText="Home" link={AppRoutes.root} />
            <CommonButton buttonText="All Transactions" link={AppRoutes.allTransactionRecords} />
            <CommonButton buttonText="Add Transaction" link={AppRoutes.addTransactionRecord}/>
        </div>
    );
};
