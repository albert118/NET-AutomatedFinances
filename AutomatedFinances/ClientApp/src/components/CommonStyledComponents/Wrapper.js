import React from 'react';
import styled from 'styled-components';

import "../../styles/homestyles.css";

const StyledWrapper = styled.div`
        max-width: var(--max-wrapper-width, 1000px);
        margin-right: auto;        
        margin-left: auto;
        padding-left: 16px;
        padding-right: 16px;
    `;

export default function Wrapper(props) { return (<StyledWrapper>{props.children}</StyledWrapper>); }
