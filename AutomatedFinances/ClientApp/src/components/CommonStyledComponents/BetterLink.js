import React from 'react';
import styled from 'styled-components';
import { Link } from 'react-router-dom';

import "../../styles/homestyles.css";

const StyledHyperLink = styled(Link)`
        color: ${props => props.color ? props.color : "inherit"};
        width: ${props => props.width ? props.width : "inherit"};
        font-weight: ${props => props.fontWeight ? props.fontWeight : "400"};
        line-height: ${props => props.lineHeight ? props.lineHeight : "1rem"};
        letter-spacing: ${props => props.letterSpacing ? props.letterSpacing : "0.02rem"};
        place-content: center;
        place-items: center;
        padding: 0 var(--navigation-gap);
        text-decoration: none !important;
        font-size: ${props => props.fontSize ? props.fontSize : "18px"};
        
        &:hover, &:focus, &:visited, &:link, &:active {
            text-decoration: none !important;
        }
        
        &:hover {
            color: ${props => props.hoverColor ? props.hoverColor : "var(--gold-metallic)"};
        }
        
    `;

export default function BetterLink(props) {
    return (
        <StyledHyperLink
            className="better-link"
            color={props.color}
            width={props.width}
            fontSize={props.fontSize}
            hoverColor={props.hoverColor}
            fontWeight={props.fontWeight}
            lineHeight={props.lineHeight}
            letterSpacing={props.letterSpacing}
            to={{ pathname: props.Link }}
        >
            { props.LinkText}
        </StyledHyperLink>
    )
}
