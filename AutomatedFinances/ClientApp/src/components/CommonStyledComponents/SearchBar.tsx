import * as React from 'react';
import { useState } from 'react';
import styled from 'styled-components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons'
import BetterButton from './BetterButton';

import "../../styles/SearchListings.css";

const MAX_SEARCH_SIZE_CHAR_LEN = 1024
const FILTER_RETRY_DELAY_MS = 500;

const SearchBarFlexBox = styled.div`
    /* flex the search content. */
        display: flex;
        padding: 0 0 0 16px;
        padding-left: 14px;
        width: 100%;

        background: var(--sky-blue-crayola, black);
    /* round the left borders, square the rights. */
        border-radius: 10px 0 0 10px;
    /* Apply a hover effect to help focus the user. */
        &:hover {
            background: linear-gradient(var(--cultured, white), var(--gold-metallic, black));
        }
    `;

const SearchBarHeroItem = styled.div`
        display: flex;
        align-items: center;
        padding-right: 13px;
        margin: 0;
        padding: 2px 13px 8px 10px;
    `;

const StyledSearchArea = styled.div`
        display: flex;
        flex: 1;
        flex-wrap: wrap;
        align-items: center;
    `;

const StyledInput = styled.input`
        flex: 100%;
        border: none;
        padding: 0;
        margin: -5px 0 0 0;
        padding-right: 5px;
    `;

const StyledSearchText = styled.div`
        background: black;
        border: none;
    /* grow with the space. */
        flex: 1;
        display: flex;
        flex-direction: column;
        justify-content: center;
    /* spacing. */
        padding: 7px;
        padding-left: 24px;
        padding-right: 13px;
        min-height: 100%;
        text-align: center;
    `;

export default function SearchBar(props: { searchCallback: Function }): JSX.Element {
    const [input, setInput] = useState("");

    return (
        <div style={{ width: "100%" }} className="search-bar">
            <SearchBarFlexBox className="searchBar-flexbox">
                <SearchBarHeroItem className="searchBar-flexbox">
                    <FontAwesomeIcon color="black" icon={faSearch} flip="horizontal" inverse />
                </SearchBarHeroItem>
                <StyledSearchArea>
                    <StyledInput
                        type="text"
                        role="combobox"
                        autoComplete="off"
                        autoCorrect="off"
                        autoCapitalize="off"
                        autoFocus={false}
                        spellCheck="false"
                        title="Search"
                        maxLength={MAX_SEARCH_SIZE_CHAR_LEN}
                        value={input}
                        onChange={e => { setInput(e.target.value) }}
                    />
                </StyledSearchArea>
                <BetterButton className="search-bar-submit btn-search" onClick={() => props.searchCallback(input)}>
                    <StyledSearchText>Search</StyledSearchText>
                </BetterButton>
            </SearchBarFlexBox>
        </div>
    );
}

