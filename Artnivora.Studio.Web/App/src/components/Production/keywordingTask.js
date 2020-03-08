import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import fetchUsersAction from '../../store/users/actions/fetchUsers';
import searchUsersAction from '../../store/users/actions/searchUsers';
import { filteredUsersSelector } from '../../store/users/selectors/userSelectors';
import { filteredAllProductionsSelector } from '../../store/productions/selectors/productionsSelectors';
import styles from './Styles.css';
import TextInputField from 'hvb-shared-frontend/src/components/TextInputField/TextInputField';
import fetchProductionsAction from '../../store/productions/actions/fetchProductions';
import isEqual from 'react-fast-compare';
import fetchProductionAttachmentWithId from '../../store/productions/actions/fetchProductionAttachmentWithId';
import DetailTask from './detailTask';
import Pagination from './pagination'

class KeywordingTask extends Component {
    constructor(props) {
        super(props)
        this.state = {
            currentPage: 1,
            postsPerPage: 5,
        };
        this.handleActionDownload = this.handleActionDownload.bind(this);
        this.paginate = this.paginate.bind(this);
    }

    componentDidMount() {
        this.props.fetchProductions("Keywording");
    }

    componentDidUpdate(prevProps) {
        const { production } = this.props;
        if (!isEqual(production, prevProps.production)) {
            this.props.fetchProductions("Keywording");
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

    paginate = (pageNumber, e) => {
        e.preventDefault();
        this.setState({
            currentPage: pageNumber,
        });
    }

    render() {
        const { production } = this.props;
        const { currentPage, postsPerPage } = this.state;

        const indexOfLastPost = currentPage * postsPerPage;
        const indexOfFirstPost = indexOfLastPost - postsPerPage;
        const currentPosts = production.slice(indexOfFirstPost, indexOfLastPost);   
        return (
            <div>
                <div>
                    <h3> Keywording Task </h3>
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
                <DetailTask production={currentPosts} handleActionDownload={this.handleActionDownload} linkto={`/production/editkeywording?id=`} />
                <hr className="style14" />
                <Pagination
                    postsPerPage={postsPerPage}
                    totalPosts={production.length}
                    paginate={this.paginate}
                />
            </div>
        );
    }
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
)(KeywordingTask);