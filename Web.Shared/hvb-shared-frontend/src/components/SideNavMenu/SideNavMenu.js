import React from 'react';
import { connect } from 'react-redux';
import { List } from 'immutable';
import { Collapse, Container, Nav, Navbar, NavbarToggler } from 'reactstrap';
import { routingLocationSelector } from '../../store/selectors/generalSelector';
import { loggedInUserSelector } from '../../store/selectors/authSelectors';
import NavMenuItem from './NavMenuItem/NavMenuItem';
import NavMenuItemWithSubItems from './NavMenuItem/NavMenuItemWithSubItems';
import { Map } from 'immutable';
import './SideNavMenu.css';
import {
    GUEST
} from '../../constants/roles';

class SideNavMenu extends React.Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);

        this.state = {
            isOpen: false,
        };
    }
    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    render() {
        const {
            routingLocation,
            loggedInUser,
            urls = Map(),
        } = this.props;

        const roles = loggedInUser ? loggedInUser.roles : [GUEST];
        const filteredUrls = urls.filter(url => {
            if (!url.get('allowedRoles')) {
                return true;
            } else {
                return url.get('allowedRoles').some(role => roles.includes(role));
            }
        });

        return (
            <Navbar className="navbar-expand-sm navbar-toggleable-sm admin-sidebar same col-md-3 col-lg-2 pl-0v" light>
                <Container className="sidebar-container">
                    <NavbarToggler onClick={this.toggle} className="mr-2" />
                    <Collapse isOpen={this.state.isOpen} navbar>
                        <Nav vertical className="sidebar-nav">
                            {filteredUrls.reduce((navItems, navItem) => {
                                if (navItem.get('children')) {
                                    return navItems.push(
                                        <NavMenuItemWithSubItems
                                            key={`navmenuItemWithSubItems-${navItem.get('to')}`}
                                            navItem={navItem}
                                            pathname={routingLocation.pathname}
                                            subItems={navItem.get('children')}
                                        />
                                    )
                                }

                                return navItems.push(
                                    <NavMenuItem
                                        key={`navmenuItems-${navItem.get('to')}`}
                                        navItem={navItem}
                                        pathname={routingLocation.pathname}
                                    />
                                );
                            }, List())}
                        </Nav>
                    </Collapse>
                </Container>
            </Navbar>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
            loggedInUser: loggedInUserSelector(state),
        }
    },
    null,
)(SideNavMenu);