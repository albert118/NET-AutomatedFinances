import * as React from 'react';
import { useState, useEffect, useMemo } from 'react';
import styled, { keyframes } from 'styled-components';
import Wrapper from './CommonStyledComponents/Wrapper';
import { useAsyncDebounce } from 'react-table';
import * as LoadingSpinners from './CommonStyledComponents/LoadingSpinner';

import "../styles/homestyles.css";

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

const Searchbar = styled.input`
        border-radius: 5px;
        width: 10em;
        border: none;
        padding: 0, 2px;
        height: 2em;
    `;


export default function JobListingsSearchPage(): JSX.Element {
    const [input, setInput] = useState('');

    return (
        <Wrapper>
            <HeroContainer>
                <SearchBar term={input} updateTerm={setInput} />
            </HeroContainer>
        </Wrapper>
        );
}

function SearchBar(search: { term: string, updateTerm: Function }): JSX.Element {
    return (
        <Searchbar
            type="text"
            role="combobox"
            autoComplete="off"
            value={ search.term }
            placeholder={"Search..."}
            onChange={(e) => search.updateTerm(e.target.value)}
        />
    );
}
