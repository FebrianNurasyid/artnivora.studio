import React, { Component} from 'react';
import { Link } from 'react-router-dom';
import { MdAttachFile } from "react-icons/md";
import { formatDateForMessage } from 'hvb-shared-frontend/src/helpers/messagesHelper';

class DetailTask extends Component {
    render() {
        const {
            production,
            linkto,
        } = this.props;
        return (
            <div>
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
                        {production.map(prod =>
                            <tr key={prod.id}>
                                <td>{prod.title}</td>
                                <td>{prod.category}</td>
                                <td>{prod.themes}</td>
                                <td>{prod.concept}</td>
                                <td>{prod.createdBy}</td>
                                <td>{formatDateForMessage(prod.createdDate)}</td>
                                <td><a id={prod.attacment.productionAttachementId}
                                    onClick={this.props.handleActionDownload}
                                    href="#">
                                    {prod.attacment.fileName}
                                </a></td>
                                <td>{prod.status}</td>
                                <td><Link to={linkto + prod.id}>Edit</Link></td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}
export default (DetailTask);