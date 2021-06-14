import React, { Component } from 'react';
import NavMenu from './NavMenu';
import Footer from './Footer';
import styled, { keyframes } from 'styled-components';


const ContentFlexBox = styled.div`
    display: flex;
    flex-direction: column;
    `;

const ContentStyling = styled.div`
    flex: 1 0 auto;
    margin-top: 30px;
    `;

export default class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div>
                <NavMenu />
                <ContentFlexBox>
                    <ContentStyling>
                        {this.props.children}
                    </ContentStyling>
                </ContentFlexBox>
                <Footer />
            </div>
        );
    }
}
