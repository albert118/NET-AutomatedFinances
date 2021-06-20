import React from 'react';
import styled from 'styled-components'
import BetterLink from '../CommonStyledComponents/BetterLink';
import Wrapper from '../CommonStyledComponents/Wrapper';

import '../../styles/NavMenu.css';

const StyledSocial = styled.ul`
        display: flex;
        padding: 0;
        margin: 0 0 1.2rem;
        list-style: none;
        place-content: center;
        place-items: center;
    `;

const StyledFooter = styled.footer`
        background-color: black;
        width: 100%;
        height: auto;
        padding: 3.2rem 0;
        margin: 0 auto;
        z-index: 10;
        color: var(--navigation-noclick-color, white);
        flex-shrink: 0;
        display: flex;
        flex-direction: column;
        flex-wrap: nowrap
    `;

const FooterContentGrid = styled.div`
        display: grid;
        grid-template-columns: repeat(6,1fr);
    `;

const FooterLine = styled.hr`
        height: 0;
        width: auto;
        color: white;
        border: none;
        border-bottom: 1px solid var(--navigation-noclick-color, white);
        margin-right: 45px;        
        margin-left: 45px;
        background: transparent;
        outline: none;
    `;

function FooterSocial() { return (<StyledSocial />); }

export default function Footer() {
    return (
        <StyledFooter className="styled-footer" id="_footer">
            <Wrapper>
                <BetterLink
                    className="footer-link"
                    fontWeight="400"
                    fontSize="1.1rem"
                    lineHeight="1rem"
                    letterSpacing="0.05rem"
                    Link="https://get.asp.net/" LinkText="ASP NET Core and C#" />
                <BetterLink
                    className="footer-link"
                    fontWeight="400"
                    fontSize="1.1rem"
                    lineHeight="1rem"
                    letterSpacing="0.05rem"
                    Link="https://facebook.github.io/react/" LinkText="React" />
                <BetterLink
                    className="footer-link"
                    fontWeight="400"
                    fontSize="1.1rem"
                    lineHeight="1rem"
                    letterSpacing="0.05rem"
                    Link="http://getbootstrap.com/" LinkText="React + Bootstrap" />
            </Wrapper>
            <FooterLine className="footer-line-break" />
            <Wrapper className="footer-legal">Copyright © 2021 Albert Ferguson</Wrapper>
            <FooterSocial />
        </StyledFooter>
    );
}