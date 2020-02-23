import React from 'react';
import { Container } from 'reactstrap';
import './Layout.css';

import { sidebarUrls, allUrls } from '../constants/urls';
import Footer from 'hvb-shared-frontend/src/components/Footer/Footer';
import WebHeader from './WebHeader/WebHeader';
import LoadingBar from 'hvb-shared-frontend/src/components/LoadingBar/LoadingBar';
import SideNavMenu from 'hvb-shared-frontend/src/components/SideNavMenu/SideNavMenu';

export default props => (
    <div className="layoutRoot">
        <div className="content">
            <WebHeader />
            <LoadingBar 
                urls={allUrls}
            />
            <Container className="mainContainer">
                <SideNavMenu
                    urls={sidebarUrls} 
                />
                <div className="mainContent">
                    {props.children}
                </div>
            </Container>
        </div>
        
    </div>
);
