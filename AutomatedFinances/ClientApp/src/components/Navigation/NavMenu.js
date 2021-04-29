import React, { Component, useState, useEffect } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Button} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import styled from 'styled-components'

const StyledNavbar = styled.nav``;

const StyledLink = styled.li``;

const StyledButton = styled(Button)`
    /* A button that changes based on props! */
    background: ${props => props.primary ? "palevioletred" : "white"};
    background: ${props => props.isHovered ? "palered" : ""};
    colour: ${props => props.primary ? "white" : "palevioletred"};
    display: inline-block;

    font-size: 1em;
    margin: 1em;
    padding: 0.25em 1em;
    border: 2px solid palevioletred;
    border-radius: 3px;
`;

function BetterNavLinks(props) {
    const [isHovered, setHovered] = useState(false);

    return (
        <NavItem>
            <Button outline>
                <NavLink tag={Link} className="text-dark" to={String(props.Path)}>{String(props.Name)}</NavLink>
            </Button>
        </NavItem>
    );
}


export default function NavMenu() {
    const [collapsed, setCollapsed] = useState(true);

    function toggleNavbar() {
        setCollapsed(!collapsed);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand tag={Link} to="/">AutomatedFinances</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <BetterNavLinks Path='/' Name="Home" />
                            <BetterNavLinks Path='/counter' Name="Counter" />
                            <BetterNavLinks Path='/careerhubData' Name="Career Hub Data" />
                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}
