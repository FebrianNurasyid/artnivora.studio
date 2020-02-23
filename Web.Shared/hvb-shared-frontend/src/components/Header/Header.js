import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { Collapse, Container, Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { bindActionCreators } from 'redux';
import { isOnPath } from '../../helpers/routingHelper';
import logOutAction from '../../store/actions/logout';
import { userLoggedInSelector } from '../../store/selectors/authSelectors';
import { routingLocationSelector } from '../../store/selectors/generalSelector';
import './Header.css';
import logo from './Images/logo1.png';

class Header extends React.Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.toggleDropDown = this.toggleDropDown.bind(this);

        this.state = {
            isOpen: false,
            dropdownOpen: false,
        };
    }

    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    toggleDropDown() {
        this.setState({
            dropDownIsOpen: !this.state.dropDownIsOpen
        });
    }

    render() {
        const {
            routingLocation,
            userLoggedIn,
            children,
        } = this.props;
        return (
            <header>
                <Navbar sticky="top" className="navbar-expand-sm navbar-toggleable-sm hvb-header-base border-bottom" light >
                    <Container className="headerContainer">
                        <NavbarBrand tag={Link} to="/">
                            <img src={logo} border="0" height={'51px'} />
                        </NavbarBrand>
                        <NavbarToggler onClick={this.toggle} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={this.state.isOpen} navbar>
                            <ul className="navbar-nav flex-grow">
                                {children}
                                {userLoggedIn && (
                                    <NavItem>
                                        <Dropdown
                                            className="button-light"
                                            isOpen={this.state.dropDownIsOpen}
                                            toggle={this.toggleDropDown}
                                        >
                                            <DropdownToggle
                                                className="header-dropdown"
                                                caret
                                            >
                                                Profile
                                            </DropdownToggle>
                                            <DropdownMenu
                                                right
                                            >
                                                <DropdownItem>Some Action</DropdownItem>
                                                <DropdownItem>Some other action</DropdownItem>
                                                <DropdownItem as={"button"} onClick={() => this.props.logOut()}>
                                                    Log out
                                                </DropdownItem>
                                            </DropdownMenu>
                                        </Dropdown>
                                    </NavItem>      
                                )}
                                {!userLoggedIn && (<NavItem>
                                    <NavLink
                                        active={isOnPath(routingLocation.pathname, '/users/login')}
                                        tag={Link}
                                        className="navmenu-link"
                                        to="/users/login"
                                    >
                                        Login
                                    </NavLink>
                                </NavItem>)}
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
            userLoggedIn: userLoggedInSelector(state),
        }
    },
    dispatch => bindActionCreators({
        logOut: logOutAction,
    }, dispatch),
)(Header);