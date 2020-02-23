import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import fetchUsersAction from '../../../store/users/actions/fetchUsers';
import searchUsersAction from '../../../store/users/actions/searchUsers';
import { filteredUsersSelector } from '../../../store/users/selectors/userSelectors';
import styles from '../Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import HVBButton from 'hvb-shared-frontend/src/components/HVBButton/HVBButton';

class Division extends Component {    
    componentDidMount() {
        this.props.fetchUsers();
    }

    render() {
        return (
            <div>
                <div>
                    <h3> Master Division </h3>
                    <hr className="style14" />
                    <TextInputField
                        className={styles.filterInput}
                        key={'search'}
                        placeholder={'search division...'}
                        onChange={(key, value) => {
                            this.props.searchUsers(value);
                        }}
                    />
                </div>
                <br />
                {renderDataTable(this.props)}
            </div>
        );
    }
}

function renderDataTable(props) {
    return (        
        <div>            
            <div className="btn-add">
                <Link className="hvb-button btn btn-primary" to={`/masters/divisions/creatediv`}>Add New</Link>                
            </div>
            <table className='table table-striped'>
                <thead className='thead-dark'>
                    <tr>
                        <th>Id</th>
                        <th>Division Name</th>
                        <th>Creation Date</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {props.users.map(user =>
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.username}</td>
                            <td>{user.creationDate}</td>
                            <td><Link to={`/users/create?id=${user.id}`}>Edit | </Link><Link to={`/users/create?id=${user.id}`}>Delete</Link></td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default connect(
    (state) => {
        return {
            users: filteredUsersSelector(state),
        }
    },
    dispatch => bindActionCreators({
        fetchUsers: fetchUsersAction,
        searchUsers: searchUsersAction,
    }, dispatch)
)(Division);