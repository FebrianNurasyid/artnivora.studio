import React from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';
import { NavItem, NavLink } from 'reactstrap';
import { isOnPath } from 'hvb-shared-frontend/src/helpers/routingHelper';
import Header from 'hvb-shared-frontend/src/components/Header/Header';
import { userLoggedInSelector } from 'hvb-shared-frontend/src/store/selectors/authSelectors';
import { routingLocationSelector } from 'hvb-shared-frontend/src/store/selectors/generalSelector';

class WebHeader extends React.Component {
    render() {
        const {
            userLoggedIn,
            routingLocation
        } = this.props;
        return (
            <Header>
                <NavItem>
                    <NavLink
                        active={isOnPath(routingLocation.pathname, '/overview', true)}
                        tag={Link}
                        className="navmenu-link"
                        to="/overview"
                    >
                        Dashboard
                    </NavLink>
                </NavItem>               
            </Header>
        );
    }
}

export default connect(
    (state) => {
        return {
            userLoggedIn: userLoggedInSelector(state),
            routingLocation: routingLocationSelector(state),
        }
    },
    null,
)(WebHeader);