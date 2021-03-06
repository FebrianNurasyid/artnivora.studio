﻿import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import fetchUsersAction from '../../store/users/actions/fetchUsers';
import searchUsersAction from '../../store/users/actions/searchUsers';
import { filteredUsersSelector } from '../../store/users/selectors/userSelectors';
import { filteredAllProductionsSelector } from '../../store/productions/selectors/productionsSelectors';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import { MdAttachFile } from "react-icons/md";
import fetchProductionsAction from '../../store/productions/actions/fetchProductions';
import { formatDateForMessage } from 'hvb-shared-frontend/src/helpers/messagesHelper';
import isEqual from 'react-fast-compare';
import fetchProductionAttachmentWithId from '../../store/productions/actions/fetchProductionAttachmentWithId';

class ProductionTask extends Component {
    constructor(props) {
        super(props)
        this.state = {
        };  
        this.handleActionDownload = this.handleActionDownload.bind(this);
    }

    componentDidMount() {
        this.props.fetchProductions("Production");
    }

    componentDidUpdate(prevProps) {
        const { production } = this.props;        
        if (!isEqual(production, prevProps.production)) {
            this.props.fetchProductions("Production");
        }

    }      

    handleActionDownload = (e) => {
        e.preventDefault();
        const attachment =
        {
            "id": e.target.id,
            "filename": e.target.innerText,
            "filepath": '',
        }        
        this.props.fetchProductionAttachmentWithId(attachment, 'real');
    }

    render() {
        return (
            <div>
                <div>
                    <h3> Production Task </h3>
                    <hr className="style14" />
                    <TextInputField
                        className={styles.filterInput}
                        key={'search'}
                        placeholder={'search production...'}
                        onChange={(key, value) => {
                            this.props.searchUsers(value);
                        }}
                    />
                </div>
                <br />
                {renderDataTable(this.props, this.handleActionDownload)}
            </div>
        );
    }
}

function renderDataTable(props,handleDownload) {
    return (
        <div>
            <div className="btn-add">
                <Link className="hvb-button btn btn-primary" to={`/production/addoredit`}>Add New</Link>
            </div>
            <table className='table table-striped'>
                <thead className='thead-dark'>
                    <tr>
                        <th>Tittle</th>
                        <th>Category</th>
                        <th>Themes</th>
                        <th>Concept</th>
                        <th>Created By</th>
                        <th>Created Date</th>
                        <th>Attachment <MdAttachFile size={16} /></th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>

                    {props.production.map(prod =>
                        <tr key={prod.id}>
                            <td>{prod.title}</td>
                            <td>{prod.category}</td>
                            <td>{prod.themes}</td>
                            <td>{prod.concept}</td>
                            <td>{prod.createdBy}</td>
                            <td>{formatDateForMessage(prod.createdDate)}</td>
                            <td><a id={prod.attacment.productionAttachementId} onClick={handleDownload} href="#">{prod.attacment.fileName}</a></td>
                            <td>{prod.status}</td>
                            <td><Link to={`/production/addoredit?id=${prod.id}&task=Production`}>Edit</Link></td>
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
            production: filteredAllProductionsSelector(state),
        }
    },
    dispatch => bindActionCreators({
        fetchUsers: fetchUsersAction,
        searchUsers: searchUsersAction,
        fetchProductions: fetchProductionsAction,
        fetchProductionAttachmentWithId,
    }, dispatch)
)(ProductionTask);