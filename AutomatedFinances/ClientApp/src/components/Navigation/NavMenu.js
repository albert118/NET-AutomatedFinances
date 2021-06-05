import React, { useState } from 'react';
import { Button} from 'reactstrap';
import styled from 'styled-components'
import BetterLink from '../CommonStyledComponents/BetterLink';

import '../../styles/NavMenu.css';


function NavbarBrand(props) {
    const StyledNavbarbrand = styled.div`
        display: inline-flex;        
        width: auto;        
        height: 100%;
        flex: none;
        padding: 0;
        place-content: center;
        place-items: center;
    `;

    const StyledLogo = styled.svg`
        width: 14px;
        heigh: 21px;
        margin-right: 12px;
        vertical-align: middle;
    `;

    const StyledLogoWord = styled.span`
        text-decoration: none;
        color: var(--sky-blue-crayola, white) !important;
        font-size: 0.8rem;
        font-weight: 650;
    `;

    // prop: Link should redirect to Home
    // prop: brandText the brand text
    return (
        <StyledNavbarbrand>
            <StyledLogo />
            <StyledLogoWord>
                <BetterLink Link="/" LinkText={props.brandText} />
            </StyledLogoWord>
        </StyledNavbarbrand>
    )
}

function NavTextLink(props) {
    const [isHovered, setHovered] = useState(false);
    const isButton = props.IsButton;

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
        colour: var(--egyptian-blue, inherit);
        padding: 0.25em 1em;
        border-radius: 20px;
        place-content: center;
        place-items: center;
    `;

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
    const StyledSpacer = styled.div`
        display: flex;
        height: 100%;
        flex-flow: row nowrap;
        color: var(--navigation-noclick-color);
        margin: 0 auto 0 var(--navigation-gap);
    `;

    return (
        <StyledSpacer>
            <NavTextLink Link='/' LinkText="Home" />
            <NavTextLink Link='/About' LinkText="About" />
            <NavTextLink Link='/Contact' LinkText="Contact" />
        </StyledSpacer>
    )
}

function SpacedNavUtilities() {
    const StyledUtils = styled.div`
        position: absolute;
        top: 0;
        right: 0;
        display: flex;
        height: 60px;        
        flex-direction: row;
        padding: 0;
        place-content: center;
        place-items: center;
    `;

    return (
        <StyledUtils>
            <NavTextLink Link='/' LinkText="Add something here!" IsButton={true} />
            <NavTextLink Link='/utsCareerHub' LinkText="Career Hub Data" IsButton={true} />
        </StyledUtils>
    )
}

export default function NavMenu() {
    const BrandTitleText = "Auto Finances";

    const StyledNavbar = styled.nav`
        position: fixed;
        top: 0:
        left: 0;
        background: black;
        color: white;
        width: 100vw;
        height: var(--navigation-height, 60px);
        display: flex;
        flex-row: column nowrap;
        contain: layout;
        z-index: 10;
    `;

    return (
        <StyledNavbar>
            <NavbarBrand brandText={BrandTitleText} />
            <SpacedNavOptions />
            <SpacedNavUtilities />
        </StyledNavbar>
    );
}
