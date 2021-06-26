import * as React from 'react';
import { Component } from 'react';
import { Route, Switch, BrowserRouter } from 'react-router-dom';
import Layout from './components/Navigation/Layout';
import { Home } from './components/Home';
import JobListingsSearchPage from './components/JobListings/JobListingsSearchPage';
import CareerHubData from './components/JobListings/CareerHub/CareerhubData';

import {
  HTTPCode400,
  HTTPCode401,
  HTTPCode403,
  HTTPCode404,
  HTTPCode500,
} from './components/AJAX/HTTPCodes/HTTPCodes.js';
import './custom.css'

// to enable cookies, uncomment below
//import { withCookies } from 'react-cookie'; // Cookies logic implented

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <BrowserRouter>
                <div className="App">
                    {/* Excellent answer on rendering a / route */}
                    {/* https://stackoverflow.com/a/44292410/9505707 */}
                    <Layout>
                        <Route exact path='/' component={Home} />
                        <Route path='/jobListingsSearch' component={JobListingsSearchPage} />
                        <Route
                            path='/jobsListingsResults'
                            render={((props) => <CareerHubData {...props} />)}
                        />
                        {/*  Error routes */}
                        <Route path='/error400' component={HTTPCode400} />
                        <Route path='/error401' component={HTTPCode401} />
                        <Route path='/error403' component={HTTPCode403} />
                        <Route path='/error404' component={HTTPCode404} />
                        <Route path='/error500' component={HTTPCode500} />
                    </Layout>
                </div>
            </BrowserRouter>
      );
  }
}


// see this link below on setting up cookies with React
// https://medium.com/@rossbulat/using-cookies-in-react-redux-and-react-router-4-f5f6079905dc
// export default withCookies(App);
