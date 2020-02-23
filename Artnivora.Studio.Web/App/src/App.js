import React, { Component } from 'react';
import AllRoutes from './routes/AllRoutes';
import Layout from './components/Layout';
import Startup from './Startup';
import { ToastContainer } from 'react-toastify';

class App extends Component {
    render() {
        return (
            <span style={{
                height: "100%",
                fontFamily: "Montserrat, sans-serif"
            }}>
                <Layout>
                    <AllRoutes />
                </Layout>
                <Startup />
                <ToastContainer />
            </span>
        );
    }
}

export default (App);