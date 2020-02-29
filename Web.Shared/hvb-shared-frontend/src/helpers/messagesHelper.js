export const getValueFromLinkInfo = (linkInfo, isLimit) => {
    if (linkInfo) {
        if (!isLimit) {
            return parseInt(linkInfo.url.substring(linkInfo.url.indexOf("=") + 1, linkInfo.url.indexOf("&")));
        }
        else {
            return parseInt(linkInfo.url.substring(linkInfo.url.lastIndexOf("=") + 1, linkInfo.url.length))
        }
    }
    else return 0;
}

export const formatDateForMessage = (date) => {

    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];

    var d = new Date(date),
        month = '' + monthNames[d.getMonth()],
        day = '' + d.getDate(),
        year = d.getFullYear(),
        hour = d.getHours(),
        minute = d.getMinutes();
    
    if (day.length < 2)
        day = '0' + day;
    var newDate = [day, month, year].join('-');
    var newHour = [hour, minute].join(':');
    return [newDate, newHour].join(' ');
}
