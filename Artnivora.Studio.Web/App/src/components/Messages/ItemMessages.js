import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './MessageReceived.css';

class ItemMessages extends Component {  

    formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear(),
            hour = d.getHours(),
            minute = d.getMinutes();

        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;
        var newDate = [day, month, year].join('-');
        var newHour = [hour, minute].join(':');
        return [newDate, newHour].join(' ');
    }    
    
    render() {
        return (
            <div key={`message-item-${this.props.id}`}>
                <hr className="style14" />
                <div className="row pointer" value={this.props.id} onClick={() => this.props.handleClickRow(this.props.id)} >
                    <div className="cont">
                        <div className="custom-control custom-checkbox">
                            <input
                                key={this.props.id}
                                id={this.props.id}
                                type="checkbox"
                                onChange={this.props.handleCheckChildElement}
                                checked={this.props.isChecked} value={this.props.id}
                            />
                            <label className="checkmark" htmlFor={this.props.id}></label>
                        </div>
                    </div>
                    <div className="col">{this.props.from}</div>
                    <div className="col-6" style={this.props.isRead ? { fontWeight: 'normal' } : { fontWeight: 'bold' }} >
                        {this.props.subject}
                    </div>
                    <div className="col">{this.props.attachment}</div>
                    <div className="col">{this.formatDate(this.props.date)}</div>
                </div>
            </div>
        );
    }
}

export default ItemMessages