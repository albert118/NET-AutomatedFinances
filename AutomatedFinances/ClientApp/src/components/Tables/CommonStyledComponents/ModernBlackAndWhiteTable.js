import React from 'react';
import styled from 'styled-components'
import Wrapper from '../../CommonStyledComponents/Wrapper';
import { LoadingSpinner, FlashText } from '../../CommonStyledComponents/LoadingSpinner';

import '../../../styles/monochromaticbluepalette.css';
import '../../../styles/contrastpalette.css';


/* Fixed table header on scrolling. */
export const StyledTable = styled.table`
    table-layout: fixed;    
    width: 100%;
    margin: 0;
    padding: 0;
    border: 0;
    vertical-align: baseline;
    overflow-y: scroll;
    background-color: black;
    color: white;
    border-radius: 10px;
`;

export const StyledTableBody = styled.tbody`
    width: 100%;
    margin: 0;
    padding: 0;
    text-overflow: ellipsis;
    background-color: var(--cultured, #ccc);
    color: black;
`;

export const StyledTableHeader = styled.thead`
    height: 60px;
    width: 100%;
`;

export const StyledTableRow = styled.tr`
    padding: 0.25rem;
    text-align: left;
`;

export const StyledTableHeaderCell = styled.th`
    font-weight: bold;   
    padding-top: 16px;
    padding-bottom: 16px;
    text-align: left;
`;

export const StyledTableHeaderFirstCell = styled.th`
    font-weight: bold;   
    padding-top: 16px;
    padding-bottom: 16px;
    text-align: left;
    padding-left: 40px;
`;

export const StyledTableDataCell = styled.td`
    padding-top: 7px;
    padding-bottom: 7px;
    width: 20%;
`;

export const StyledTableDataFirstCell = styled.td`
    padding-top: 7px;
    padding-bottom: 7px;
    padding-left: 40px;
    width: 20%;
`;

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

export function TableWrapper(props) {
    return (
        <Wrapper>
            <HeroContainer>
                <GridContainer>
                    {props.children}
                </GridContainer>
            </HeroContainer>
        </Wrapper>
    )
}

/// Set LoadingMessage prop to add a loading message for the user.
export function TableLoadingPlaceHolder(props) {
    return (
        <TableWrapper>
            <LoadingSpinner />
            <br />
            <FlashText LoadingMessage={props.LoadingMessage}/>
        </TableWrapper>
    )
}
