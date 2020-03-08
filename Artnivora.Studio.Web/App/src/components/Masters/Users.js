import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import fetchUsersAction from '../../store/users/actions/fetchUsers';
import searchUsersAction from '../../store/users/actions/searchUsers';
import { filteredUsersSelector } from '../../store/users/selectors/userSelectors';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import { formatDateForMessage } from 'hvb-shared-frontend/src/helpers/messagesHelper';

class Users extends Component {
    componentDidMount() {
        this.props.fetchUsers();
    }

    render() {
        return (
            <div>
                <div>
                    <h3> Search the users here ... </h3>
                    <TextInputField
                        className={styles.filterInput}
                        key={'search'}
                        placeholder={'search query...'}
                        onChange={(key, value) => {
                            this.props.searchUsers(value);
                        }}
                    />
                </div>
                <br/>
                <div className="btn-add">
                    <Link className="hvb-button btn btn-primary" to={`/masters/users/register`}>Add New</Link>
                </div>
                {renderDataTable(this.props)}
            </div>
        );
    }
}


function renderDataTable(props) {
    return (        
        <table className='table table-striped'>
            <thead className='thead-dark'>
                <tr>
                    <th>Id</th>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Creation Date</th>
                    <th>~</th>
                </tr>
            </thead>
            <tbody>
                {props.users.map(user =>
                    <tr key={user.id}>
                        <td>{user.id}</td>
                        <td>{user.username}</td>
                        <td>{user.email}</td>
                        <td>{formatDateForMessage(user.creationDate)}</td>
                        <td>
                            {/*<Link to={`/users/create?id=${user.id}`}>Edit</Link>*/}
                        </td>
                    </tr>
                )}
            </tbody>
        </table>
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
)(Users);