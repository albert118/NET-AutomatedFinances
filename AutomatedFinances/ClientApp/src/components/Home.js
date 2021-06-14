import React from 'react';
import styled, { keyframes } from 'styled-components';
import BetterLink from './CommonStyledComponents/BetterLink';
import Wrapper from './CommonStyledComponents/Wrapper';

import "../styles/homestyles.css";

const StyledArrow = styled.span`
        height: ${props => props.Size ? props.Size : "6px"};
        margin-left: 0.32em;
        position: relative;
        display: inline-block;
        flex: none;
        color: inherit;
    `;

function Arrow(props) {
    return (
        <StyledArrow props>
            <svg 
                xmlns="http://www.w3.org/2000/svg" 
                viewBox="0 0 14 14" 
                display="block"
                height="100%"
                aria-hidden="true"
            >
                <path
                    d="M7 1.167L12.833 7 7 12.833M12.25 7H1.167"
                    fill="transparent"
                    strokeWidth="2"
                    stroke="currentColor"
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    vectorEffect="non-scaling-stroke">
                </path>
            </svg>
        </StyledArrow>
    )
}

const HeroContainer = styled.div`
        padding-top: 8rem; 
        padding-bottom: 4rem;
        margin-bottom: 4rem;  
    `;

const GridContainer = styled.div`
        position: relative;
        display: grid;
        grid-auto-columns: auto;
        grid-auto-rows: auto;
        row-gap: 16px
    `;

const StyledHeader = styled.h1`
        color: black;
        font-weight: 600;
        letter-spacing: -.046em;
        line-height: 1.08;
        font-size: 4rem;
    `;

export function Home() {
    const _fontSize = "22px";
    const _arrowSize = "12px";

    return (
        <Wrapper>
            <HeroContainer> 
                <GridContainer>
                    <StyledHeader>Auto Finances</StyledHeader>
                    <h3 >A supped up API with an SPA for your convenience.</h3>
                    <GridContainer>
                        <p>
                            <BetterLink Link='' LinkText='Read the docs' FontSize={_fontSize} />
                            <Arrow Size={_arrowSize}/>
                            <BetterLink Link='' LinkText='Checkout the GitHub' FontSize={_fontSize}/>
                            <Arrow Size={_arrowSize}/>
                            <BetterLink Link='' LinkText='Read the docs' FontSize={_fontSize} />
                            <Arrow Size={_arrowSize}/>
                        </p>
                    </GridContainer>
                </GridContainer>
            </HeroContainer>
            {/* for demo only */}
            <br /><br /><br /><br /><br /><br /><br /><br /><br />
        </Wrapper>
    );
}
