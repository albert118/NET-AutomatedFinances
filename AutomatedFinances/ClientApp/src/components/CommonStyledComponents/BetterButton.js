import React from 'react';
import { Button } from 'reactstrap';
import styled from 'styled-components';

const StyledButton = styled(Button)`
        display: inline-block;
        background: black;
        border-radius: ${props => !props.$curveCorners ? "0px" : (!props.$curveRadius ? "20px" : props.$curveRadius)};
        place-content: center;
        place-items: center;

        &:hover {
            color: ${props => props.$hoverColor ? props.$hoverColor : "var(--gold-metallic)"};
        }
    `;

const CircleButtonFlexStack = styled.div`
        display: flex;
        align-items: center;
        justify-items: center;
        flex: 1;
    /* Make a stack for button with text underneath. */
        flex-direction: column;
        margin: 0;
        padding: 3px 2px 3px 2px;

    /* Circle button highlights the button on hover. */
        &:hover > *:nth-child(n+1) {
            background: ${props => props.$hoverColor ? props.$hoverColor : "var(--gold-metallic)"};
            border: white;
        }
    `;

export default function BetterButton(props) {
    return (
        <StyledButton
            className="better-button"
            $hoverColor={props.hoverColor}
            $curveCorners={props.curveCorners ? true : false} // idk but this is needed
            onClick={props.onClick ? props.onClick : undefined }
        >
            {props.children}    
        </StyledButton>
    );
}


export function CircleButton(props) {
    return (
        <CircleButtonFlexStack className="stack">
            <StyledButton
                className="circle-button"
                $hoverColor={props.hoverColor}
                $curveCorners={true}
                $curveRadius={props.$curveRadius}
                onClick={props.onClick ? props.onClick : undefined}
            />
            {props.children}
        </CircleButtonFlexStack>
    );
}
