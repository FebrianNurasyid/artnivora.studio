import React from 'react';
import { connect } from 'react-redux';

const PageNotFound = props => (
    <div>
       Oops, this page doesn't exist!
    </div>
);

export default connect()(PageNotFound);
