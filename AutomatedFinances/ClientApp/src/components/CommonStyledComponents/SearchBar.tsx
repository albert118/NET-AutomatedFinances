import * as React from 'react';
import styled from 'styled-components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons'
import "../../styles/SearchListings.css";

const MAX_SEARCH_SIZE_CHAR_LEN = 2048

const SearchBarFlexBox = styled.div`
        display: flex;
        padding: 5px 8px 0 16px;
        padding-left: 14px;
        width: 100%;
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
    `;


export default function SearchBar(search: { term: string, updateTerm: Function }): JSX.Element {
    return (
        <div className="searchBar">
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
                        value={search.term}
                        onChange={(e) => search.updateTerm(e.target.value)}
                    />
                </StyledSearchArea>
                <p className="search-text">Search</p>
            </SearchBarFlexBox>
        </div>
    );
}
