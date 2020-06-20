import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Link } from 'react-router-dom';

class Brief extends Component {
    constructor(props) {
        super(props)
        this.state = {
        };
    }
    render() {
        return (
            <span>
                <h3>Brief of Today</h3>
                <hr className="style14" />    
                {renderDataTable(this.props, this.handleActionDownload)}
            </span>
        )
    }
}

function renderDataTable(props, handleDownload) {
    return (
        <div>      
            <div className="btn-add">
                <Link className="hvb-button btn btn-primary" to={`/brief/addoreditbrief`}>New Brief</Link>
            </div>
            <table className='table table-striped'>
                <thead className='thead-dark'>
                    <tr>
                        <th>Date Of Brief</th>
                        <th>Descriptions</th>                        
                        <th>Industry</th>
                        <th>Other Notes</th>                        
                        <th>Details</th>
                    </tr>
                </thead>
                <tbody>

                    {props.production && props.production.map(prod =>
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
            //users: filteredUsersSelector(state),
            //production: filteredAllProductionsSelector(state),
        }
    },
    dispatch => bindActionCreators({
        //fetchUsers: fetchUsersAction,
        //searchUsers: searchUsersAction,
        //fetchProductions: fetchProductionsAction,
        //fetchProductionAttachmentWithId,
    }, dispatch)
)(Brief);