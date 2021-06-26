import * as React from 'react';
import { useState } from 'react';
import styled from 'styled-components';
import { Redirect } from 'react-router-dom'
import { isNullOrUndefined } from 'util';
import SearchBar from '../CommonStyledComponents/SearchBar';
import DateFilter from '../CommonStyledComponents/DateFilter';
import Wrapper from '../CommonStyledComponents/Wrapper';

/////////////////////////////////////////////////////
// Page grid layout
/////////////////////////////////////////////////////

const TwoColPageGrid = styled.div`
        width: 100%;
        height: auto;
    /* grid config */            
        display: grid;
        grid-template-columns: [search-col] 50% [review-and-saved-col] 50%;
        grid-template-rows: [header] 20% [main] 60% [misc-footer] 20%;
        column-gap: 55px;
        row-gap: 12px;
     /* center align and left justify content of the grid by default. */
        justify-content: start;
        align-content: center;
     /* paddings and margins */
        margin: 0;
        padding: 0;
        padding-bottom: 4rem;
    `;

/////////////////////////////////////////////////////


const HeroContainer = styled.div`
    /* configure position in parent grid. */
        grid-column: search-col / search-col ;
        grid-row: header / main;
    /* spacing. */
        padding-top: 4rem; 
        padding-bottom: 4rem;
    `;

const MiscContainer = styled.div`
    /* configure position in parent grid. */
        grid-column: search-col / search-col;
        grid-row: misc-footer / misc-footer ;
    `;

const HeaderVisualBlockContainer = styled.div`
        max-width: 1000px;
        width: 100%;
        display: grid;
        align-items: center;
        justify-items: left;
        grid-row-gap: 5px;
        grid-template-rows: 40% 40% 20%;
    `;

const HeroHeaderTitle = styled.h1`
        font-size: xxx-large;
        letter-spacing: -0.15rem;
        font-weight: 600;
        line-height: 1.1;
    `;

const HeroDescription = styled.p`
        font-size: 16px;
        font-weight: 600;
        line-height: 1.05;
        letter-spacing: 0.01rem;
    `;

const SecondaryHeaders = styled.h1`
        font-size: xx-large;
        letter-spacing: -0.15rem;
        font-weight: 600;
        line-height: 1.08;
`;

const SecondaryContentGrid = styled.div`
        max-width: 1000px;
        width: 100%;
        display: grid;
        grid-column-gap: 30px;
        grid-templace-columns: 33% 66%;
    `;

const TrendingCardNegativeSpace = styled.div`
        grid-area: 1 / 1 / 2 / 2;
        border: 0.5px solid #000;
        box-shadow: 0 2.8px 2.2px rgba(0, 0, 0, 0.034),
            0 6.7px 5.3px rgba(0, 0, 0, 0.048),
            0 12.5px 10px rgba(0, 0, 0, 0.06),
            0 22.3px 17.9px rgba(0, 0, 0, 0.072),
            0 41.8px 33.4px rgba(0, 0, 0, 0.086);
        border-radius: 10px;
    `;

const ViewSavedCard = styled.div`
        grid-area: 1 / 2 / 1 / 2;
        border: 0.5px solid #fff;
        box-shadow: 0 2.8px 2.2px rgba(0, 0, 0, 0.034),
            0 6.7px 5.3px rgba(0, 0, 0, 0.048),
            0 12.5px 10px rgba(0, 0, 0, 0.06),
            0 22.3px 17.9px rgba(0, 0, 0, 0.072),
            0 41.8px 33.4px rgba(0, 0, 0, 0.086);
        background: grey;
        border-radius: 10px;
    `;

const InnerContentContainer = styled.div`
    margin: 10px;
    `;

function GetYYYMMDD(d: Date): string {
    return new Date(d.getTime() - d.getTimezoneOffset() * 60 * 1000).toISOString().split('T')[0];
}

export default function JobListingsSearchPage(): JSX.Element {
    const [searchTerms, setSearchTerms] = useState("");
    const [lowerDateFilter, setLowerDateFilter] = useState(new Date(Date.now()));
    const [upperDateFilter, setUpperDateFilter] = useState(new Date(Date.now()));

    if (!isNullOrUndefined(searchTerms) && searchTerms !== "") {
        return (
            <Redirect 
                push
                to={{
                    pathname: '/jobsListingsResults',
                    state: {
                        SearchTerms: searchTerms,
                        LowerDateFilter: GetYYYMMDD(lowerDateFilter),
                        UpperDateFilter: GetYYYMMDD(upperDateFilter)
                    }
                }}
            />
        );
    } 

    return (
        <Wrapper>
            <TwoColPageGrid className="two-col-grid">
                <HeroContainer className="hero-container">
                    <HeaderVisualBlockContainer>
                        <HeroHeaderTitle>Search Job Listings</HeroHeaderTitle>
                        <HeroDescription>
                            Search multiple jobs sites, and listings, all in one location.
                            Enter multiple search terms and see the results!
                        </HeroDescription>
                        <SearchBar searchCallback={setSearchTerms} />
                        <br />
                        <DateFilter lowerDateCallBack={setLowerDateFilter} upperDateCallBack={setUpperDateFilter} />
                    </HeaderVisualBlockContainer>
                </HeroContainer>
                <MiscContainer>
                    <SecondaryContentGrid>
                        {/*<TrendingCardNegativeSpace>
                            <InnerContentContainer>
                                <SecondaryHeaders>Trending</SecondaryHeaders>
                                <p>#1 TOPIC</p>
                                <p>#2 TOPIC</p>
                                <p>#3 TOPIC</p>
                                <p>#4 TOPIC</p>
                            </InnerContentContainer>
                        </TrendingCardNegativeSpace>*/}
                    </SecondaryContentGrid>
                </MiscContainer>
            </TwoColPageGrid>
        </Wrapper>
    );
}