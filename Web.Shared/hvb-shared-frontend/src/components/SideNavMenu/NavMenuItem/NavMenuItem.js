import React from 'react';
import { Link } from 'react-router-dom';
import { NavItem, NavLink } from 'reactstrap';
import { isOnPath } from '../../../helpers/routingHelper';

import './NavMenuItem.css';

class NavMenuItem extends React.Component {
    render() {
        const {
            pathname,
            navItem,
            isSubNavItem,
        } = this.props;

        return (
            <NavItem
                key={`${navItem.get('to')} ${navItem.get('label')}`}
            >
                <NavLink
                    className={`${isSubNavItem ? "subnav-link" : "sidenav-link"}`}
                    active={isOnPath(pathname, navItem.get('to'), navItem.get('matchExact'))}
                    tag={Link}
                    to={navItem.get('to')}
                    disabled={navItem.get('disabled')}
                >
                    <span className="link-span">
                        {!isSubNavItem && navItem.get('icon')} {navItem.get('label')}
                    </span>
                </NavLink>
            </NavItem>
        );
    }
}

export default (NavMenuItem);