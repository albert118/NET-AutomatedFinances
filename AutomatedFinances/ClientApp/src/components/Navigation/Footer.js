import React, { useState } from 'react';
import styled from 'styled-components'
import BetterLink from '../CommonStyledComponents/BetterLink';
import Wrapper from '../CommonStyledComponents/Wrapper';

import '../../styles/NavMenu.css';


function FooterSocial() {
    const StyledSocial = styled.ul`
        display: flex;
        padding: 0;
        margin: 0 0 1.2rem;
        list-style: none;
        place-content: center;
        place-items: center;
    `;

    return (
        <StyledSocial>
        </StyledSocial>
        )
}

export default function Footer() {
    const StyledFooter = styled.footer`
        background-color: black;
        width: 100%;
        height: auto;
        padding: 3.2rem 0;
        margin: 0 auto;
        z-index: 10;
        color: var(--navigation-noclick-color, white);
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

    return (
        <StyledFooter id="_footer">
            <Wrapper>
                <BetterLink Link="https://get.asp.net/" LinkText="ASP NET Core and C#" />
                <BetterLink Link="https://facebook.github.io/react/" LinkText="React" />
                <BetterLink Link="http://getbootstrap.com/" LinkText="React + Bootstrap" />
            </Wrapper>
            <FooterLine />
            <Wrapper>
                Copyright © 2021 Albert Ferguson
            </Wrapper>
            <FooterSocial />
        </StyledFooter>
   )
}