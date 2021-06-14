import * as React from 'react';
import { useState, useEffect, useMemo } from 'react';
import styled from 'styled-components';
import Wrapper from '../CommonStyledComponents/Wrapper';
import SearchBar from '../CommonStyledComponents/SearchBar';
import * as LoadingSpinners from '../CommonStyledComponents/LoadingSpinner';

const HeroContainer = styled.div`
        padding-top: 4rem; 
        padding-bottom: 4rem;
        margin-right: 200px;
        margin-left: 200px;
    `;

const Container = styled.div`
        padding-top: 4rem;    
        padding-bottom: 4rem;
        margin-right: 200px;
        margin-left: 200px;
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
        grid-area: 1 / 1 / 1 / 1;
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

export default function JobListingsSearchPage(): JSX.Element {
    const [input, setInput] = useState('');

    return (
        <Wrapper>
            <HeroContainer>
                <HeaderVisualBlockContainer>
                    <HeroHeaderTitle>Search Job Listings</HeroHeaderTitle>
                    <HeroDescription>
                        Search multiple jobs sites, and listings, all in one location.
                        Enter multiple search terms and see the results!
                    </HeroDescription>
                        <SearchBar term={input} updateTerm={setInput} />
                        <p>Add some other search components here...</p>
                </HeaderVisualBlockContainer>
            </HeroContainer>
            <Container>
                <SecondaryContentGrid>
                    <TrendingCardNegativeSpace>
                        <InnerContentContainer>
                            <SecondaryHeaders>Trending</SecondaryHeaders>
                            <p>#1 TOPIC</p>
                            <p>#2 TOPIC</p>
                            <p>#3 TOPIC</p>
                            <p>#4 TOPIC</p>
                        </InnerContentContainer>
                    </TrendingCardNegativeSpace>
                    <ViewSavedCard>
                        <InnerContentContainer>
                            <SecondaryHeaders>View Saved</SecondaryHeaders>
                            <p>Last saved item...</p>
                            <p>Last saved item...</p>
                            <p>Last saved item...</p>
                            <p>Last saved item...</p>
                        </InnerContentContainer>
                    </ViewSavedCard>
                </SecondaryContentGrid>
            </Container>
        </Wrapper>
    );
}