import React, { Component } from 'react';

export class CareerhubData extends Component {
    static displayName = CareerhubData.name;

    constructor(props) {
        super(props);
        this.state = {
            JSESSIONID: "",
            RELAYSTATEVAL: "",
            SAMLTOKEN: "",
            LOGINLOCATION: ""
        };
    }

    componentDidMount() {
        this.populateLoginFields();
    }

    async populateLoginFields() {
        console.log("here");
        const response = await fetch('/CareerHub');
        const data = await response.json();
        console.log(data);
        //this.setState({ forecasts: data, loading: false });
    }

    render() {
        return (
            <div>
                <h1>UTS CareerHub Data Testpage</h1>
                <p><strong>JSESSION ID</strong> {this.state.JSESSIONID}</p>
                <br />
                <p><strong>RELAY STATE VALUE</strong> { this.state.RELAYSTATEVAL}</p>
                <br />
                <p><strong>SAML TOKEN</strong> {this.state.SAMLTOKEN}</p>
                <br />
                <p><strong>LOGIN LOCATION</strong> {this.state.LOGINLOCATION}</p>
            </div>
        );
    }
}
