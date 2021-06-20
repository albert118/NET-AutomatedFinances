import React from 'react';
import { Button} from 'reactstrap';
import styled from 'styled-components'
import BetterLink from '../CommonStyledComponents/BetterLink';

import '../../styles/NavMenu.css';

const StyledNavbar = styled.nav`
    /* grid config */    
        display: grid;
        grid-template-columns: [branding-hero-nav] 20% [hero-nav-opts] 44% [button-nav] 35%;
        grid-template-rows: [nav-row] 100%;
        column-gap: 10px;
        row-gap: 7px;
    /* center and align nav options for read-ability*/
        justify-items: start;
        align-items: center; 
    /* top align our nav bar */
        background: black;
        color: white;
        width: 100%;
        height: var(--navigation-height, 60px);
        contain: layout;
        z-index: 10;
        overflow: hidden;
    /* make sure the nav doesnt stick right to the edge of the screen. */
        padding-left: 25px;
        padding-right: 25px;
    /* disable labels on nav options. */
        label {
            display: none;
        }
    `;

const StyledSpacer = styled.div`
    /* configure position in parent grid. */
        grid-column-start: hero-nav-opts 1;
    /* flex nav options. */   
        display: flex;
        flex-flow: row nowrap;
        place-content: center;
        place-items: center;
        width: auto;
        height: 100%;
        color: var(--navigation-noclick-color);
        margin: 0 auto 0 var(--navigation-gap);
    `;

const StyledUtils = styled.div`
        display: flex;
        height: 60px;
        padding: 0;
        place-content: center;
        place-items: center;
    `;

const StyledNavbarbrand = styled.div`
    /* configure position in parent grid. */
        grid-column-start: branding-hero-nav 1;
    /* flex brand. */        
        display: inline-flex;
        width: auto;        
        height: 100%;
        flex: none;
    /* padding and alignment. */
        padding: 0;
        justify-self: start;
        align-self: center;
    /* branding placement. */
        place-content: center;
        place-items: center;
    `;

const StyledLogo = styled.svg`
        min-width: 55px;
        max-width: 55px;
        min-height: 55px;
        max-height: 55px;
        margin-right: 7px;
        vertical-align: middle;
    `;

const StyledNavText = styled.li`
        display: flex;
        width: inherit;
        color: inherit;
        font-size: 15px;
        font-weight: 500;
        line-height: 1;
        place-content: center;
        place-items: center;
    `;

const StyledButton = styled(Button)`
        display: inline-block;
        background: black;
        padding: 0.25em 1em;
        margin-right: 7px;
        margin-left: 7px;
        border-radius: 20px;
        place-content: center;
        place-items: center;

        &:hover {
            color: ${props => props.hoverColor ? props.hoverColor : "var(--gold-metallic)"};
        }
    `;

function NavbarBrand(props) {
    const color = "var(--sky-blue-crayola, white)";
    const fontSize = "1.2rem";
    const fontWeight = "600";
    const lineHeight = "1.05";
    const letterSpacing = "0.49rem";

    return (
        <StyledNavbarbrand className="styled-navbar-brand">
            <StyledLogo className="styled-logo"/>
            {/* style this link with custom styling for StyledLogoWord */}
            <BetterLink
                className="styled-logo-word"
                color={color}
                fontSize={fontSize}
                fontWeight={fontWeight}
                lineHeight={lineHeight}
                letterSpacing={letterSpacing}
                Link="/"
                LinkText={props.brandText} />
        </StyledNavbarbrand>
    )
}

function NavTextLink(props) {
    const isButton = props.IsButton;
    
    if (!isButton) {
        return (
            <StyledNavText>
                <BetterLink Link={props.Link} LinkText={props.LinkText} />
            </StyledNavText>
        )
    }

    return (
        <StyledButton>
            <BetterLink Link={props.Link} LinkText={props.LinkText} />
        </StyledButton>
    )
}

function SpacedNavOptions() {
    return (
        <StyledSpacer>
            <NavTextLink Link='/' LinkText="Home" />
            <NavTextLink Link='/About' LinkText="About" />
            <NavTextLink Link='/Contact' LinkText="Contact" />
        </StyledSpacer>
    )
}

function SpacedNavUtilities() {
    return (
        <StyledUtils>
            <NavTextLink Link='/jobListingsSearch' LinkText="Search Jobs" IsButton={true} />
            <NavTextLink Link='/utsCareerHub' LinkText="UTS CareerHub Job Postings" IsButton={true} />
        </StyledUtils>
    )
}

export default function NavMenu() {
    const BrandTitleText = "Auto Finances";

    return (
        <StyledNavbar role="full-horizontal">
            {/* Full horizontal styling. */}
            <NavbarBrand brandText={BrandTitleText} />
            <SpacedNavOptions />
            <SpacedNavUtilities />
        </StyledNavbar>
    );
}
