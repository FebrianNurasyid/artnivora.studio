import React from 'react';
import { Button, } from 'reactstrap';
import './HVBButton.css';

class HVBButton extends React.Component {
    render() {
        const {
            extraClassName,
            children,
            borderRadius = '50px',
            onClick
        } = this.props;

        return (
            <Button
                className={`hvb-button ${extraClassName}`}
                style={{
                    'borderRadius': borderRadius
                }}
                onClick={onClick}
            >
                {children}
            </Button>
        );
    }
}

export default (HVBButton);