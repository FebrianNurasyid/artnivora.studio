import React from 'react';
import { List } from 'immutable';
import { Link } from 'react-router-dom';
import { Collapse, Container, Nav, Navbar, NavItem, NavLink } from 'reactstrap';
import { isOnPath } from '../../../helpers/routingHelper';
import NavMenuItem from './NavMenuItem';
import { FaArrowUp, FaArrowDown } from "react-icons/fa";

import './NavMenuItem.css';

class NavMenuItemWithSubItems extends React.Component {
    constructor(props) {
        super(props);
        const {
            navItem,
            pathname,
        } = this.props;

        const shouldOpen = isOnPath(pathname, navItem.get('to'), navItem.get('matchExact'));

        this.toggle = this.toggle.bind(this);
        this.state = {
            isOpen: shouldOpen || false,
        };
    }

    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    render() {
        const {
            pathname,
            navItem,
            subItems,
        } = this.props;

        return (
            <NavItem
                key={`${navItem.get('to')} ${navItem.get('label')} - withSubmenu`}
                className="subnav-submenu"
            >
                <span>
                    <NavLink
                        className="sidenav-link"
                        active={isOnPath(pathname, navItem.get('to'), navItem.get('matchExact'))}
                        tag={Link}
                        onClick={this.toggle}
                        to={'#'}
                        disabled={navItem.get('disabled')}
                    >
                        <span className="parent-link-span">
                            <span>{navItem.get('icon')} {navItem.get('label')}</span>
                            <span>{this.state.isOpen ? <FaArrowUp /> : <FaArrowDown />} </span>
                        </span>
                    </NavLink>
                </span>
                <span>
                    <Navbar className="subnav-navbar" light>
                        <Container className="subnav-container">
                            <Collapse isOpen={this.state.isOpen} className="subnav-collapse">
                                <Nav vertical>
                                    {subItems.reduce((navItems, navItem) => {
                                        return navItems.push(
                                            <NavMenuItem
                                                key={`navmenuWithSubItems-${navItem.get('to')}`}
                                                navItem={navItem}
                                                pathname={pathname}
                                                isSubNavItem
                                            />
                                        );
                                    }, List())}
                                </Nav>
                            </Collapse>
                        </Container>
                    </Navbar>
                </span>
            </NavItem>
        );
    }
}

export default (NavMenuItemWithSubItems);