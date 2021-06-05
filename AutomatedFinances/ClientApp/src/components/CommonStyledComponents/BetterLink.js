import React from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';

import "../../styles/homestyles.css";

export default function BetterLink(props) {
    const StyledHyperLink = styled(Link)`
        color: inherit;
        width: inherit;
        color: inherit;
        place-content: center;
        place-items: center;
        padding: 0 var(--navigation-gap);
        color: inherit !important;
        text-decoration: none !important;
        font-size: ${props.FontSize ? props.FontSize : "18px" } !important;
        &:focus, &:hover, &:visited, &:link, &:active {
            text-decoration: none;
        }
    `;
    
    return (
        <StyledHyperLink to={{ pathname: props.Link }}>{ props.LinkText }</StyledHyperLink>
    )
}

