import * as React from 'react';
import { useState } from 'react';
import styled from 'styled-components';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar } from '@fortawesome/free-solid-svg-icons'
import BetterButton, { CircleButton } from './BetterButton';

const ButtonRowStack = styled.div`
        display: flex;
        flex: 1;
        margin: 0;
        padding: 7px;
    `;

const TimeLine = styled.div`
        position: relative;
    /* bar styling - I reckon a background gradient might work better dynamically though. */
        &::after {
            background: black; /* ensure zooming doesnt show a white background between the borders. */
            border-radius: 20px;
            content: "";
            position: absolute;
            left: 40px;
            top: 15px;
            border: black solid 0.5px;
            width: 85%;
            margin: 0;
            padding: 0;
            z-index: -1;
        }
    `;

// TODO: Extract to date time util/helper/extension
function GetMonth(Last = true): Date {
    var today = new Date(Date.now());
    if (Last) today.setMonth(today.getMonth()- 1);
    else today.setMonth(today.getMonth() + 1);
    return today;
}

function GetWeek(Last = true): Date {
    var today = new Date(Date.now());
    if (Last) today.setDate(today.getDate() - 7);
    else today.setDate(today.getDate() + 7);
    return today;
}

function GetDay(Last = true): Date {
    var today = new Date(Date.now());
    if (Last) today.setDate(today.getDate() - 1);
    else today.setDate(today.getDate() + 1);
    return today;
}

export default function DateFilter(props: { lowerDateCallBack: Function, upperDateCallBack: Function}): JSX.Element {
    return (
        <div style={{ width: "100%" }} className="date-filter">            
            <TimeLine />
            <ButtonRowStack>
                <FontAwesomeIcon color="var(--gold-metallic, black)" icon={faCalendar} size="lg" title="Jobs within..."/>
                <CircleButton className="last-month btn" $curveRadius="45px" onClick={() => props.lowerDateCallBack(GetMonth())}>
                    Within the last month
                </CircleButton>
                <CircleButton className="last-week btn" $curveRadius="45px" onClick={() => props.lowerDateCallBack(GetWeek())}>
                    Within the last week
                </CircleButton>
                <CircleButton className="last-day btn" $curveRadius="45px" onClick={() => props.lowerDateCallBack(GetDay())}>
                    Within the last day
                </CircleButton>
            </ButtonRowStack>
            <br />
            <TimeLine />
            <ButtonRowStack>
                <FontAwesomeIcon color="var(--gold-metallic, black)" icon={faCalendar} size="lg" title="Jobs expiring..." />
                <CircleButton className="next-month btn" $curveRadius="45px" onClick={() => props.upperDateCallBack(GetMonth(false))}>
                    Expires next month
                </CircleButton>
                <CircleButton className="next-week btn" $curveRadius="45px" onClick={() => props.upperDateCallBack(GetWeek(false))}>
                    Expires next week
                </CircleButton>
                <CircleButton className="next-day btn" $curveRadius="45px" onClick={() => props.upperDateCallBack(GetDay(false))}>
                    Expires tomorrow
                </CircleButton>
            </ButtonRowStack>
        </div>
    );
}