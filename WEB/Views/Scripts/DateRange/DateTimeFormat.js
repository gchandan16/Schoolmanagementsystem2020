function FormatDateTimeRange(startdate, enddate, datepickerid) {

    var start = moment().subtract(29, 'days');
    var end = moment();
    if (startdate != '01-01-0001 00:00:00') {
        debugger;
        start = moment(startdate);
        end = moment(enddate);
    }

    function cb(start, end) {
        $('#' + datepickerid + ' span').html(start.format('MMM D, YYYY hh:mm A') + ' - ' + end.format('MMM D, YYYY hh:mm A'));
    }

    $('#' + datepickerid).daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        timePicker: true,
        timePickerIncrement: 10,
        locale: {
            format: 'MMM D, YYYY hh:mm A'
        }
    }, cb);

    cb(start, end);

}


function FormatDateRange(startdate, enddate, datepickerid) {

    var start = moment().subtract(29, 'days');
    var end = moment();
    if (startdate != '01-01-0001 00:00:00') {
        debugger;
        start = moment(startdate);
        end = moment(enddate);
    }

    function cb(start, end) {
        $('#' + datepickerid + ' span').html(start.format('DD/MMM/YYYY') + ' - ' + end.format('DD/MMM/YYYY'));
    }

    $('#' + datepickerid).daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        timePicker: false
       
    }, cb);

    cb(start, end);

}


function FormatDateRangeMonthly(startdate, enddate, datepickerid) {

    debugger;
    var start = moment().subtract(29, 'days');
    var end = moment();
    if (startdate != '01-01-0001 00:00:00') {
        debugger;
        start = moment(startdate);
        end = moment(enddate);
    }

    function cb(start, end) {
        $('#' + datepickerid + ' span').html(start.format('MMM/YYYY') + ' - ' + end.format('MMM/YYYY'));
    }

    $('#' + datepickerid).daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        format: "MMM/YYYY",
        viewMode: "months",
        minViewMode: "months",
        timePicker: false

    }, cb);

    cb(start, end);

}