import React from 'react';

class SharedComponent extends React.Component {
    render() {
        const {
            color,
        } = this.props;
        return (
            <div style={{padding: 30, backgroundColor: color || 'red'}}>
                This is my shared component! test 100
            </div>
        )
    }
}

export default (SharedComponent);