import React from 'react';
import { Link } from 'react-router-dom';
import './Footer.css';

export default class Footer extends React.Component {
    render() {
        return (
            <footer className="border-top footer">
                <div className="container">
                    <div className="container-fluid text-center text-md-left">
                        <div className="row">
                            <div className="col-md-6 mt-md-0 mt-3">
                                <h5 className="text-uppercase">Footer Content</h5>
                                <p>Here you can use rows and columns to organize your footer content.</p>
                            </div>

                            <hr className="clearfix w-100 d-md-none pb-3" />

                            <div className="col-md-3 mb-md-0 mb-3">
                                <h5 className="text-uppercase">Links</h5>

                                <ul className="list-unstyled">
                                    <li>
                                        <Link to={`/`}>Home</Link>
                                    </li>
                                    <li>
                                        <Link to={`/users/overview`}>Users</Link> 
                                    </li>
                                    <li>
                                        <Link to={`/users/create`}>Create User</Link>
                                    </li>
                                </ul>

                            </div>

                            <div className="col-md-3 mb-md-0 mb-3">
                                <h5 className="text-uppercase">Links</h5>

                                <ul className="list-unstyled">
                                    <li>
                                        <a href="#!">Link 1</a>
                                    </li>
                                    <li>
                                        <a href="#!">Link 2</a>
                                    </li>
                                    <li>
                                        <a href="#!">Link 3</a>
                                    </li>
                                    <li>
                                        <a href="#!">Link 4</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
         );
    }
}
