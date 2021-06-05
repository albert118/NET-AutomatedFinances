import React from 'react';
import styled, { keyframes } from 'styled-components';
import { bounce, flash } from 'react-animations';
import { motion } from "framer-motion"
import Wrapper from './Wrapper';

import "../../styles/contrastpalette.css"

export function BounceText (props) {
    const Bounce = styled.div`
        animation: 3.5s ${keyframes`${bounce}`} infinite;
        color: ${props.fontColor ? props.fontColor : "black"};
        font-size: ${props.fontSize ? props.fontSize : "2em"};
        font-weight: bold;
        text-align: center;
        place-self: center;
    `;

    return (
        <Bounce className="loading-bounce">
            {props.LoadingMessage ? props.LoadingMessage : "Loading..."}
        </Bounce>
    )
}

export function FlashText (props) {
    const Flash = styled.div`
        animation: 3.5s ${keyframes`${flash}`} infinite;
        color: ${props.fontColor ? props.fontColor : "black"};
        font-size: ${props.fontSize ? props.fontSize : "2em" };
        font-weight: bold;
        text-align: center;
        place-self: center;
    `;

    return (
        <Flash className={props.className}>
            {props.LoadingMessage ? props.LoadingMessage : "Loading..."}
        </Flash>)
}

export function LoadingSpinner (props) {
    const StyledSpinner = styled(motion.div)`
        background: black;
        border-radius: 10px;
        width: 50px;
        height: 50px;
        background: linear-gradient(180deg, var(--cobalt-blue, white) 0%, var(--gold-metallic, rgb(156, 26, 255)) 100%);
        place-self: center;
    `;

    return (
        <StyledSpinner className={props.ClassName}
            animate={{
                rotate: [0, 0, 270, 270, 0],
                scale: [1, 2, 2, 1, 1],
                borderRadius: ["20%", "20%", "50%", "50%", "20%"]
            }}
            transition={{
                duration: 2,
                ease: "easeInOut",
                times: [0, 0.2, 0.5, 0.8, 1],
                loop: Infinity,
                repeatDelay: 1
            }}
        />
        
    )
}

function StyledLoading(props) {
    const StyledLoader = styled.div`
        margin-right: auto;        
        margin-left: auto;
        padding-left: 16px;
        padding-right: 16px;
    `;
    function renderChildren() {
        return React.Children.map(props.children, child => {
            return React.cloneElement(child, {
                fontSize: props.fontSize,
                fontColor: props.fontColor,
                LoadingMessage: props.LoadingMessage
            })
        })
    }

    return (
        <StyledLoader>
            { renderChildren() }
        </StyledLoader>
    )
}

export function Demo() {
    // Great read on styling in general, but specifically wrappers
    // https://ishadeed.com/article/styling-wrappers-css/

    // React animations docs
    //https://github.com/FormidableLabs/react-animations

    // uses framer-motion
    // docs: https://www.framer.com/motion/

    const SingleGrid = styled.div`
        display: grid;
        grid-template-rows: 200px;
        row-gap: 16px
    `;

    return (
        <Wrapper>
            <SingleGrid>
                <LoadingSpinner />
                <StyledLoading fontColor="palevioletred" LoadingMessage="Loading...">
                    <BounceText />
                </StyledLoading >
                <FlashText fontColor="#114c55ff"/>
            </SingleGrid>
        </Wrapper>
    )
}