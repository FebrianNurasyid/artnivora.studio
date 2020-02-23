import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';
import { routingLocationSelector } from '../../store/selectors/generalSelector';
import './LoadingBar.css';

class LoadingBar extends React.Component {
    render() {
        const {
            routingLocation,
            urls
        } = this.props;

        const pathName = routingLocation.pathname;
        const splitPath = pathName.length == 1 ? [''] : routingLocation.pathname.split('/');

        const getPathToUse = (path, index) => {
            return index === 0 && path === '' ? '/' : path;
        }

        const filteredSplitPath = splitPath.filter((path, index) => {
            const pathToUse = getPathToUse(path, index);
            return urls.get(pathToUse) !== undefined;
        });

        return (
            <div className="loadingBar">
                {filteredSplitPath.map((path, index) => {
                    const pathToUse = getPathToUse(path, index);
                    const urlData = urls.get(pathToUse);

                    const isLast = index === filteredSplitPath.length - 1;
                    const isFirst = index === 0;
                    const className = isLast ? 'currentLocation' : '';

                    return (
                        <NavLink
                            key={`loadingbar-breadcrumb-${index}`}
                            className={`loadingbarLink ${className}`}
                            tag={Link}
                            to={urlData.get('to')}
                        >
                            {!isFirst && ` > `}{urlData && urlData.get('label')}
                        </NavLink>
                    );
                })}
            </div>
        );
    }
}

export default connect(
    (state) => {
        return {
            routingLocation: routingLocationSelector(state),
        }
    },
    null,
)(LoadingBar);