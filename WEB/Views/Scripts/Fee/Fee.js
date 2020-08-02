function ShowDetails(feeid, DeletePermission) {
    $('#hdnfeeid').val(feeid);
    BlockUI();
    //$('input[type="checkbox"]').prop('checked', false);
    $.ajax({
        type: "POST",
        url: UrlBase + '/Fee/GetTermDetails',
        data: '{"feeid":"' + feeid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('input[type="checkbox"]').removeAttr('disabled');
            $('input[type="checkbox"]').prop('checked', false);
            $('#myTbl').empty();
            if (response.length > 0) {
                $('#myTbl').append('<tr>');
                $('#myTbl').append('<th>Sr No</th>');
                $('#myTbl').append('<th>Fee Description</th>');
                $('#myTbl').append('<th>Month Detail </th>');
                $('#myTbl').append('<th>Start Date  </th>');
                $('#myTbl').append('<th> End Date </th>');
                $('#myTbl').append('<th> Due Date </th>');
                if (DeletePermission)
                    $('#myTbl').append('<th>Action </th>');
                $('#myTbl').append('</tr> ');
                var m = 1;
                $.each(response, function () {
                    var monthdetail = $(this)[0].Months;
                    var arr = $('input[type="checkbox"]');
                    for (var i = 0; i < arr.length; i++) {
                        if (monthdetail.indexOf(arr[i].className) > -1) {
                            $('.' + arr[i].className).prop('checked', true);
                            $('.' + arr[i].className).attr('disabled', 'disabled');
                        }
                        else {
                            //$('.' + arr[i].className).removeAttr('disabled');
                        }
                    }

                    $('#myTbl').append('<tr id=' + m + 'tr>');
                    $('#myTbl').append('<td> ' + m + ' </td>');
                    $('#myTbl').append('<td> ' + $(this)[0].FeeDescName + ' </td>');
                    $('#myTbl').append('<td> ' + $(this)[0].Months + '</td>');
                    $('#myTbl').append('<td> ' + $(this)[0].StartDate + ' </td> ');
                    $('#myTbl').append('<td> ' + $(this)[0].EndDate + '</td>');
                    $('#myTbl').append('<td> ' + $(this)[0].DueDate + '</td >');
                    if (DeletePermission)
                        $('#myTbl').append('<td>' + '<a href="javascript:;" title="Delete" onclick="return DeleteFeeHeadDescription(\'' + $(this)[0].FeeDescID + '\',\'' + $(this)[0].FeeID + '\')" > <i class="fa fa-trash bg_red"></i>  </a>' + '</td >');
                    $('#myTbl').append('</tr > ');

                    m = parseInt(m) + 1;
                });
            }
            $('#txtFeeDescName').val('');
            $('#StartDate').val('');
            $('#EndDate').val('');
            $('#DueDate').val('');
            $('#myModalFeeDesc').modal('show');
            $.unblockUI();
        },
        failure: function (response) {
            alert(response.d);
            $.unblockUI();
        },
        error: function (response) {
            alert(response.d);
            $.unblockUI();
        }
    });

}
function SaveFeeDescription() {
    var arr = [];
    arr = $('input:checked').not(':disabled');
    if (arr.length == 0) {
        alert('Please select month');
        return;
    }
    var monthdetail = '';
    for (var i = 0; i < arr.length; i++) {
        if (monthdetail.length > 0) {
            monthdetail = monthdetail + ',';
        }
        monthdetail = monthdetail + arr[i].className;
    }
    var FeeDescName = $.trim($('#txtFeeDescName').val());
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var DueDate = $('#DueDate').val();
    var feeid = $('#hdnfeeid').val();
    if (FeeDescName == '') {
        alert("Please enter Description Name.");
        $('#txtFeeDescName').focus();
        return false;
    }
    if (StartDate == '') {
        alert("Please enter start date.");
        $('#StartDate').focus();
        return false;
    }
    if (EndDate == '') {
        alert("Please enter End date.");
        $('#EndDate').focus();
        return false;
    }
    if (DueDate == '') {
        alert("Please enter due date.");
        $('#DueDate').focus();
        return false;
    }
    BlockUI();
    $.ajax({
        type: "POST",
        url: UrlBase + '/Fee/SaveFeeDescription',
        data: '{"feeid":"' + feeid + '","FeeDescName":"' + FeeDescName + '","StartDate":"' + StartDate + '","EndDate":"' + EndDate + '","DueDate":"' + DueDate + '","monthdetail":"' + monthdetail + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $.unblockUI();
            if (response == 'SUCCESS') {
                ShowDetails(feeid);
            }
            else {
                alert(response);
            }
        },
        failure: function (response) {
            alert(response.d);
            $.unblockUI();
        },
        error: function (response) {
            alert(response.d);
            $.unblockUI();
        }
    });
}
function DeleteFeeHeadDescription(FeeDescID, FeeID) {
    BlockUI();
    $.ajax({
        type: "POST",
        url: UrlBase + '/Fee/DeleteFeeHeadDescription',
        data: '{"feedescid":"' + FeeDescID + '","feeid":"' + FeeID + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response);
            $.unblockUI();
            ShowDetails(FeeID);
        },
        failure: function (response) {
            alert(response);
            $.unblockUI();
        },
        error: function (response) {
            alert(response);
            $.unblockUI();
        }
    });
}

function setStudentDetails(ui) {
    if (ui == undefined) {
        $("#studentPhoto").attr("src", "../Content/Student/dummystudent.png");
        $(".StudentID").val("");
        $(".FirstNameSpan").html("");
        $(".FirstName").val("");
        $(".FatherNameSpan").html("");
        $(".FatherName").val("");
        $(".AdmissionNoSpan").html("");
        $(".AdmissionNo").val("");
        $(".ClassNameSpan").html("");
        $(".ClassName").val("");
        $(".SectionNameSpan").html("");
        $(".SectionName").val("");
        //$("#TransportCost").val("0");
        //$("#hdnTPCost").val("0");
        $("#FeeHeadList").empty();
        $("#PayNow").hide();
    }
    else {
        // BlockUI();
        $("#studentPhoto").attr("src", ui.item.Photo.replace('~', '..'));
        $(".StudentID").val(ui.item.id);
        $(".FirstNameSpan").html(ui.item.FirstName);
        $(".FirstName").val(ui.item.FirstName);
        $(".FatherNameSpan").html(ui.item.FatherName);
        $(".FatherName").val(ui.item.FatherName);
        $(".AdmissionNoSpan").html(ui.item.AdmissionNo);
        $(".AdmissionNo").val(ui.item.AdmissionNo);
        $(".ClassNameSpan").html(ui.item.ClassName);
        $(".ClassName").val(ui.item.ClassName);
        $(".SectionNameSpan").html(ui.item.SectionName);
        $(".SectionName").val(ui.item.SectionName);
        $(".FeeGroupSpan").html(ui.item.FeeGroup);
        $(".FeeGroup").val(ui.item.FeeGroup);
        //$("#TransportCost").val(ui.item.TPCost);
        //$("#hdnTPCost").val(ui.item.TPCost);
        $("#PayNow").show();
        getStudentFeeData()
    }
}
function getStudentFeeData() {
    var months = "";
    var FeeBreakup = "";
    var Arrears = 0;
    var LastMonthBalance = 0;
    BlockUI();
    $.ajax({
        type: "POST",
        url: UrlBase + '/Fee/GetFeeDepositHistory',
        data: '{"StudentID":"' + $(".StudentID").val() + '","AdmissionNo":"' + $(".AdmissionNo").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (res) {
            var html = "";
            if (res.length > 0) {
                html += '<table class="table table-bordered table-hover">';
                html += '<tbody>';
                html += '<tr>';
                html += '<td>Date</td>';
                html += '<td>Rec No</td>';
                html += '<td>Paid Amt</td>';
                html += '<td>Status</td>';
                html += '<td>Remarks</td>';
                html += '<td>Print</td>';
                html += '</tr>';
                for (var i = 0; i < res.length; i++) {
                    LastMonthBalance += parseInt(res[i].Balance);
                    Arrears += parseInt(res[i].Arrears);
                    months += (months == "") ? res[i].months : ("," + res[i].months);
                    FeeBreakup += (FeeBreakup == "") ? res[i].TotalFeesBreakUp : (";" + res[i].TotalFeesBreakUp);
                    html += '<tr>';
                    html += '<td>' + res[i].DepositDate + '</td>';
                    html += '<td>' + res[i].ReceiptNo + '</td>';
                    html += '<td>' + res[i].Payment + '</td>';
                    html += '<td>Success</td>';
                    html += '<td>' + res[i].Remarks + '</td>';
                    html += '<td> <button type="button" onclick="return DisplayFeeReceipt(\'' + res[i].ReceiptNo + '\');" rno=' + res[i].ReceiptNo + ' class="btn btn-primary">Print</button> </td>';
                    html += '</tr>';
                }

                html += '</tbody>';
                html += '</table>';
            }
            else {
                html += 'No Payment History found for this student.';
            }
            $("#FeeHistory").empty();
            $("#FeeHistory").html(html);

            var oldBal = 0 - Arrears;
            $.ajax({
                type: "POST",
                url: UrlBase + '/Fee/GetFeeHeadDetails',
                data: '{"feeGroupName":"' + $(".FeeGroup").val() + '","AdmissionNo":"' + $(".AdmissionNo").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var paidMonths = months.split(',');
                    var FeeBreakupArr = FeeBreakup.split(';');
                    var html = "";
                    var Apr = 0, May = 0, Jun = 0, Jul = 0, Aug = 0, Sep = 0, Oct = 0, Nov = 0, Dec = 0, Jan = 0, Feb = 0, Mar = 0;
                    html += '<div class="box-header">';
                    html += '<h3 class="box-title">Fees Info</h3>';
                    html += '</div>';
                    html += '<div class="box-body">';
                    html += '<div class="form-group">';
                    if (response.length > 0) {
                        html += '<table id="example2" class="table table-bordered table-hover">';
                        html += '<thead>';
                        html += '<tr>';
                        html += '<th>S.N.</th>';
                        html += '<th>HeadName</th>';
                        if (jQuery.inArray("April", paidMonths) != -1)
                            html += '<th>Apr <input data-val="true" checked="checked" disabled="disabled" data-val-required="The April field is required." id="months_April" name="months.April" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Apr <input data-val="true" data-val-required="The April field is required." id="months_April" name="months.April" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("May", paidMonths) != -1)
                            html += '<th>May<input data-val="true"checked="checked" disabled="disabled"  data-val-required="The May field is required." id="months_May" name="months.May" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>May<input data-val="true" data-val-required="The May field is required." id="months_May" name="months.May" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("June", paidMonths) != -1)
                            html += '<th>Jun<input data-val="true"checked="checked" disabled="disabled"  data-val-required="The June field is required." id="months_June" name="months.June" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Jun<input data-val="true" data-val-required="The June field is required." id="months_June" name="months.June" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("July", paidMonths) != -1)
                            html += '<th>July<input data-val="true"checked="checked" disabled="disabled"  data-val-required="The July field is required." id="months_July" name="months.July" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>July<input data-val="true" data-val-required="The July field is required." id="months_July" name="months.July" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("August", paidMonths) != -1)
                            html += '<th>Aug<input data-val="true" checked="checked" disabled="disabled" data-val-required="The August field is required." id="months_August" name="months.August" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Aug<input data-val="true" data-val-required="The August field is required." id="months_August" name="months.August" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("September", paidMonths) != -1)
                            html += '<th>Sep<input data-val="true"checked="checked" disabled="disabled"  data-val-required="The September field is required." id="months_September" name="months.September" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Sep<input data-val="true" data-val-required="The September field is required." id="months_September" name="months.September" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';

                        if (jQuery.inArray("October", paidMonths) != -1)
                            html += '<th>Oct<input data-val="true"checked="checked" disabled="disabled"  data-val-required="The October field is required." id="months_October" name="months.October" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Oct<input data-val="true" data-val-required="The October field is required." id="months_October" name="months.October" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';

                        if (jQuery.inArray("November", paidMonths) != -1)
                            html += '<th>Nov<input data-val="true" checked="checked" disabled="disabled" data-val-required="The November field is required." id="months_November" name="months.November" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Nov<input data-val="true" data-val-required="The November field is required." id="months_November" name="months.November" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';

                        if (jQuery.inArray("December", paidMonths) != -1)
                            html += '<th>Dec<input data-val="true" checked="checked" disabled="disabled" data-val-required="The December field is required." id="months_December" name="months.December" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Dec<input data-val="true" data-val-required="The December field is required." id="months_December" name="months.December" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        if (jQuery.inArray("January", paidMonths) != -1)
                            html += '<th>Jan<input data-val="true" checked="checked" disabled="disabled" data-val-required="The January field is required." id="months_January" name="months.January" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Jan<input data-val="true" data-val-required="The January field is required." id="months_January" name="months.January" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';

                        if (jQuery.inArray("February", paidMonths) != -1)
                            html += '<th>Feb<input data-val="true" checked="checked" disabled="disabled" data-val-required="The February field is required." id="months_February" name="months.February" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Feb<input data-val="true" data-val-required="The February field is required." id="months_February" name="months.February" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';

                        if (jQuery.inArray("March", paidMonths) != -1)
                            html += '<th>Mar<input data-val="true" checked="checked" disabled="disabled"  data-val-required="The March field is required." id="months_March" name="months.March" type="checkbox" value="true" class="monthFeeCheck" onchange="return calculateMonthFee(event);"></td>';
                        else
                            html += '<th>Mar<input data-val="true" data-val-required="The March field is required." id="months_March" name="months.March" type="checkbox" value="true" class="monthFeeCheck new" onchange="return calculateMonthFee(event);"></td>';
                        html += '</tr>';
                        html += '</thead>';
                        html += '<tbody>';
                        var SNo = 0;
                        for (var l = 0; l < response.length; l++) {
                            if (response[l].Name != "Transport" && response[l].Name != "Concession") {
                                SNo = SNo + 1;
                                Apr += parseInt(response[l].Apr); May += parseInt(response[l].May); Jun += parseInt(response[l].Jun); Jul += parseInt(response[l].Jul);
                                Aug += parseInt(response[l].Aug); Sep += parseInt(response[l].Sep); Oct += parseInt(response[l].Oct); Nov += parseInt(response[l].Nov);
                                Dec += parseInt(response[l].Dec); Jan += parseInt(response[l].Jan); Feb += parseInt(response[l].Feb); Mar += parseInt(response[l].Mar);
                                html += '<tr>';
                                html += '<td>' + (SNo) + '</td>';
                                html += '<td>' + response[l].Name + '</td>';
                                html += '<td>' + response[l].Apr + '</td>';
                                html += '<td>' + response[l].May + '</td>';
                                html += '<td>' + response[l].Jun + '</td>';
                                html += '<td>' + response[l].Jul + '</td>';
                                html += '<td>' + response[l].Aug + '</td>';
                                html += '<td>' + response[l].Sep + '</td>';
                                html += '<td>' + response[l].Oct + '</td>';
                                html += '<td>' + response[l].Nov + '</td>';
                                html += '<td>' + response[l].Dec + '</td>';
                                html += '<td>' + response[l].Jan + '</td>';
                                html += '<td>' + response[l].Feb + '</td>';
                                html += '<td>' + response[l].Mar + '</td>';
                                html += '</tr>';
                            }
                        }
                        for (var l = 0; l < response.length; l++) {
                            if (response[l].Name == "Transport") {
                                SNo = SNo + 1;
                                Apr += parseInt(response[l].Apr); May += parseInt(response[l].May); Jun += parseInt(response[l].Jun); Jul += parseInt(response[l].Jul);
                                Aug += parseInt(response[l].Aug); Sep += parseInt(response[l].Sep); Oct += parseInt(response[l].Oct); Nov += parseInt(response[l].Nov);
                                Dec += parseInt(response[l].Dec); Jan += parseInt(response[l].Jan); Feb += parseInt(response[l].Feb); Mar += parseInt(response[l].Mar);
                                html += '<tr>';
                                html += '<td>' + (SNo) + '</td>';
                                html += '<td>' + response[l].Name + '</td>';
                                html += '<td>' + response[l].Apr + '</td>';
                                html += '<td>' + response[l].May + '</td>';
                                html += '<td>' + response[l].Jun + '</td>';
                                html += '<td>' + response[l].Jul + '</td>';
                                html += '<td>' + response[l].Aug + '</td>';
                                html += '<td>' + response[l].Sep + '</td>';
                                html += '<td>' + response[l].Oct + '</td>';
                                html += '<td>' + response[l].Nov + '</td>';
                                html += '<td>' + response[l].Dec + '</td>';
                                html += '<td>' + response[l].Jan + '</td>';
                                html += '<td>' + response[l].Feb + '</td>';
                                html += '<td>' + response[l].Mar + '</td>';
                                html += '</tr>';
                            }
                        }
                        for (var l = 0; l < response.length; l++) {
                            if (response[l].Name == "Concession") {
                                html += '<div class="Conc_months_April" style="display:none;">' + response[l].Apr + '</div>';
                                html += '<div class="Conc_months_May" style="display:none;">' + response[l].May + '</div>';
                                html += '<div class="Conc_months_June" style="display:none;">' + response[l].Jun + '</div>';
                                html += '<div class="Conc_months_July" style="display:none;">' + response[l].Jul + '</div>';
                                html += '<div class="Conc_months_August" style="display:none;">' + response[l].Aug + '</div>';
                                html += '<div class="Conc_months_September" style="display:none;">' + response[l].Sep + '</div>';
                                html += '<div class="Conc_months_October" style="display:none;">' + response[l].Oct + '</div>';
                                html += '<div class="Conc_months_November" style="display:none;">' + response[l].Nov + '</div>';
                                html += '<div class="Conc_months_December" style="display:none;">' + response[l].Dec + '</div>';
                                html += '<div class="Conc_months_January" style="display:none;">' + response[l].Jan + '</div>';
                                html += '<div class="Conc_months_February" style="display:none;">' + response[l].Feb + '</div>';
                                html += '<div class="Conc_months_March" style="display:none;">' + response[l].Mar + '</div>';
                            }
                        }
                        html += '<tr>';
                        html += '<td>' + (response.length + 1) + '</td>';
                        html += '<td>Total</td>';
                        var totalAmount = Apr;
                        if (jQuery.inArray("April", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Apr:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Apr - totalAmount;
                        if (Apr > totalAmount)
                            html += '<td class="months_April">' + Apr + '<br /><span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Apr - totalAmount) + ')' + '<span></td>';
                        else if (Apr < totalAmount)
                            html += '<td class="months_April">' + Apr + '<br /><span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Apr - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_April">' + Apr + '</td>';
                        totalAmount = May;
                        if (jQuery.inArray("May", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("May:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += May - totalAmount;
                        if (May > totalAmount)
                            html += '<td class="months_May">' + May + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (May - totalAmount) + ')' + '<span></td>';
                        else if (May < totalAmount)
                            html += '<td class="months_May">' + May + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (May - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_May">' + May + '</td>';
                        totalAmount = Jun;
                        if (jQuery.inArray("June", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Jun:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Jun - totalAmount;
                        if (Jun > totalAmount)
                            html += '<td class="months_June">' + Jun + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Jun - totalAmount) + ')' + '<span></td>';
                        else if (Jun < totalAmount)
                            html += '<td class="months_June">' + Jun + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Jun - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_June">' + Jun + '</td>';
                        totalAmount = Jul;
                        if (jQuery.inArray("July", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Jul:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Jul - totalAmount;
                        if (Jul > totalAmount)
                            html += '<td class="months_July">' + Jul + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Jul - totalAmount) + ')' + '<span></td>';
                        else if (Jul < totalAmount)
                            html += '<td class="months_July">' + Jul + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Jul - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_July">' + Jul + '</td>';
                        totalAmount = Aug;
                        if (jQuery.inArray("August", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Aug:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Aug - totalAmount;
                        if (Aug > totalAmount)
                            html += '<td class="months_August">' + Aug + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Aug - totalAmount) + ')' + '<span></td>';
                        else if (Aug < totalAmount)
                            html += '<td class="months_August">' + Aug + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Aug - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_August">' + Aug + '</td>';
                        totalAmount = Sep;
                        if (jQuery.inArray("September", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Sep:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Sep - totalAmount;
                        if (Sep > totalAmount)
                            html += '<td class="months_September">' + Sep + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Sep - totalAmount) + ')' + '<span></td>';
                        else if (Sep < totalAmount)
                            html += '<td class="months_September">' + Sep + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Sep - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_September">' + Sep + '</td>';
                        totalAmount = Oct;
                        if (jQuery.inArray("October", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Oct:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Oct - totalAmount;
                        if (Oct > totalAmount)
                            html += '<td class="months_October">' + Oct + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Oct - totalAmount) + ')' + '<span></td>';
                        else if (Oct < totalAmount)
                            html += '<td class="months_October">' + Oct + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Oct - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_October">' + Oct + '</td>';
                        totalAmount = Nov;
                        if (jQuery.inArray("November", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Nov:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Nov - totalAmount;
                        if (Nov > totalAmount)
                            html += '<td class="months_November">' + Nov + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Nov - totalAmount) + ')' + '<span></td>';
                        else if (Nov < totalAmount)
                            html += '<td class="months_November">' + Nov + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Nov - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_November">' + Nov + '</td>';
                        totalAmount = Dec;
                        if (jQuery.inArray("December", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Dec:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Dec - totalAmount;
                        if (Dec > totalAmount)
                            html += '<td class="months_December">' + Dec + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Dec - totalAmount) + ')' + '<span></td>';
                        else if (Dec < totalAmount)
                            html += '<td class="months_December">' + Dec + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Dec - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_December">' + Dec + '</td>';
                        totalAmount = Jan;
                        if (jQuery.inArray("January", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Jan:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Jan - totalAmount;
                        if (Jan > totalAmount)
                            html += '<td class="months_January">' + Jan + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Jan - totalAmount) + ')' + '<span></td>';
                        else if (Jan < totalAmount)
                            html += '<td class="months_January">' + Jan + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Jan - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_January">' + Jan + '</td>';
                        totalAmount = Feb;
                        if (jQuery.inArray("February", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Feb:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Feb - totalAmount;
                        if (Feb > totalAmount)
                            html += '<td class="months_February">' + Feb + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Feb - totalAmount) + ')' + '<span></td>';
                        else if (Feb < totalAmount)
                            html += '<td class="months_February">' + Feb + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Feb - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_February">' + Feb + '</td>';
                        totalAmount = Mar;
                        if (jQuery.inArray("March", paidMonths) != -1) {
                            totalAmount = 0;
                            for (var f = 0; f < FeeBreakupArr.length; f++) {
                                if (FeeBreakupArr[f].indexOf("Mar:") > -1) {
                                    var feeStr = FeeBreakupArr[f].split(':')[1].split('_');
                                    for (var a = 0; a < feeStr.length; a++) {
                                        totalAmount += parseFloat(feeStr[a].split('#')[1]);
                                    }
                                }
                            }
                        }
                        oldBal += Mar - totalAmount;
                        if (Mar > totalAmount)
                            html += '<td class="months_March">' + Mar + '<span style="color:red;"> (<i class="fa fa-long-arrow-up" aria-hidden="true"></i>' + (Mar - totalAmount) + ')' + '<span></td>';
                        else if (Mar > totalAmount)
                            html += '<td class="months_March">' + Mar + '<span style="color:green;"> (<i class="fa fa-long-arrow-down" aria-hidden="true"></i>' + (Mar - totalAmount) + ')' + '<span></td>';
                        else
                            html += '<td class="months_March">' + Mar + '</td>';
                        html += '</tr>';
                        html += '</tbody>';
                        html += '</table>';
                        html += '</div>';
                        html += '</div>';
                        $(".oldBalance").html(oldBal + LastMonthBalance);
                        $("#Arrears").val(oldBal + LastMonthBalance);

                    }
                    else {
                        html += 'No Fee Group allocated to this student.';
                    }
                    $("#FeeHeadList").empty();
                    $("#FeeHeadList").html(html);
                    $.unblockUI();
                },
                failure: function (response) {
                    $.unblockUI();
                },
                error: function (response) {
                    $.unblockUI();
                }
            });
        },
        failure: function (response) {
            $.unblockUI();
        },
        error: function (response) {
            $.unblockUI();
        }
    });
}
function calculateMonthFee(event) {
    var totalMonthFee = 0;
    var totalConcession = 0;
    $(".monthFeeCheck").each(function () {
        if ($(this).is(":checked") && !$(this).is(":disabled")) {
            totalMonthFee += parseInt($("." + this.id).html())
            if ($(".Conc_" + this.id).html() != undefined)
                totalConcession += parseInt($(".Conc_" + this.id).html())
        }
    });
    $("#Concession").val(totalConcession);
    $("#hdnTotalMonthFees").val(totalMonthFee);
    CalculatePayment(event);
}
function CalculatePayment(event) {
    //var TPCost = $("#TransportCost").val();
    var Bal = $(".oldBalance").html();
    var Fine = $("#Fine").val() == "" ? 0 : $("#Fine").val();
    if ($(event.currentTarget).attr("id") == "WaiveOff") {
        if ($("#" + $(event.currentTarget).attr("id")).is(":checked")) {
            Fine = 0;
        }
    }
    else if ($("#WaiveOff").is(":checked")) {
        Fine = 0;
    }
    var Concession = $("#Concession").val() == "" ? 0 : $("#Concession").val();
    var MonthFee = $("#hdnTotalMonthFees").val() == "" ? 0 : $("#hdnTotalMonthFees").val();
    var ActualFee = parseInt(MonthFee) + parseInt(Fine) + parseInt(Bal) - parseInt(Concession);
    var Payment = ActualFee;
    if ($(event.currentTarget).attr("id") == "Payment") {
        Payment = $("#" + $(event.currentTarget).attr("id")).val();
    }

    $("#TotalFees").val(ActualFee);
    $("#Payment").val(Payment);
    $("#Balance").val(parseInt(ActualFee) - parseInt(Payment));
    $("#hdnPayment").html($("#Payment").val());

}
function showHideMOPRmk(d) {
    if (d.value == "" || d.value == "Cash")
        $("#MOPRemark").hide();
    else
        $("#MOPRemark").show();
}
function FeeCollectionValidation() {
    if (inputValidation()) {
        if ((!($("#MOP").val() == "" || $("#MOP").val() == "Cash")) && ($.trim($("#MOPRemark").val()) == "")) {
            alert("Please enter payment remark");
            $("#MOPRemark").focus();
            return false;
        }
        else if (!$("input:checkbox.new").is(":checked")) {
            alert("Please select month.");
            return false;
        }
        else {
            BlockUI();
            $.ajax({
                type: "POST",
                url: UrlBase + '/Fee/GetFeeHeadDetails',
                data: '{"feeGroupName":"' + $("#Student_FeeGroup").val() + '","AdmissionNo":"' + $(".AdmissionNo").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (objFeeHead) {
                    $.unblockUI();
                    var months = "";
                    var TotalFee = 0;
                    var TotalFeesBreakUp = "";
                    if ($("input:checkbox#months_April.new").is(":checked")) {

                        months += (months == "") ? "April" : ",April";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Apr);
                            }
                        }
                        var BreakUp = "Apr:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Apr:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Apr;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);

                    }
                    if ($("input:checkbox#months_May.new").is(":checked")) {
                        months += (months == "") ? "May" : ",May";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].May);
                            }
                        }
                        var BreakUp = "May:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "May:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].May;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_June.new").is(":checked")) {
                        months += (months == "") ? "June" : ",June";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Jun);
                            }
                        }
                        var BreakUp = "Jun:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Jun:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Jun;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_July.new").is(":checked")) {
                        months += (months == "") ? "July" : ",July";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Jul);
                            }
                        }
                        var BreakUp = "Jul:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Jul:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Jul;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_August.new").is(":checked")) {
                        months += (months == "") ? "August" : ",August";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Aug);
                            }
                        }
                        var BreakUp = "Aug:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Aug:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Aug;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_September.new").is(":checked")) {
                        months += (months == "") ? "September" : ",September";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Sep);
                            }
                        }
                        var BreakUp = "Sep:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Sep:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Sep;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_October.new").is(":checked")) {
                        months += (months == "") ? "October" : ",October";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Oct);
                            }
                        }
                        var BreakUp = "Oct:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Oct:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Oct;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_November.new").is(":checked")) {
                        months += (months == "") ? "November" : ",November";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Nov);
                            }
                        }
                        var BreakUp = "Nov:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Nov:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Nov;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_December.new").is(":checked")) {
                        months += (months == "") ? "December" : ",December";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Dec);
                            }
                        }
                        var BreakUp = "Dec:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Dec:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Dec;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_January.new").is(":checked")) {
                        months += (months == "") ? "January" : ",January";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Jan);
                            }
                        }
                        var BreakUp = "Jan:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Jan:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Jan;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_February.new").is(":checked")) {
                        months += (months == "") ? "February" : ",February";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Feb);
                            }
                        }
                        var BreakUp = "Feb:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Feb:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Feb;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }
                    if ($("input:checkbox#months_March.new").is(":checked")) {
                        months += (months == "") ? "March" : ",March";
                        for (var a = 0; a < objFeeHead.length; a++) {
                            if (objFeeHead[a].Name != "Concession") {
                                TotalFee += parseInt(objFeeHead[a].Mar);
                            }
                        }
                        var BreakUp = "Mar:";
                        for (var ob = 0; ob < objFeeHead.length; ob++) {
                            if (objFeeHead[ob].Name != "Concession") {
                                BreakUp += (BreakUp == "Mar:") ? "" : "_";
                                BreakUp += objFeeHead[ob].Name + "#" + objFeeHead[ob].Mar;
                            }
                        }
                        TotalFeesBreakUp += (TotalFeesBreakUp == "") ? BreakUp : (";" + BreakUp);
                    }

                    var feeDeposit = {
                        "Action": "Deposit",
                        "AdmissionNo": $("#Student_Adminssionno").val(),
                        "Balance": $("#Balance").val(),
                        "Concession": $("#Concession").val(),
                        "Fine": $("#Fine").val(),
                        "months": months,
                        "MOP": $("#MOP").val(),
                        "MOPRemark": $("#MOPRemark").val(),
                        "Payment": $("#Payment").val(),
                        "Remarks": $("#Remarks").val(),
                        "StudentID": $("#Student_Smid").val(),
                        "StudentName": $("#Student_Firstname").val(),
                        "TotalFees": parseInt(TotalFee),
                        "TotalFeesBreakUp": TotalFeesBreakUp,
                        "WaiveOff": $("#WaiveOff").is(":checked"),
                        "FeeGroup": $("#Student_FeeGroup").val(),
                        "Arrears": $("#Arrears").val(),
                        //"TransportCost": $("#TransportCost").val()
                    }
                    $.ajax({
                        url: UrlBase + '/Fee/FeeCollection/',
                        contentType: 'application/json; charset=utf-8',
                        type: 'POST',
                        dataType: "json",
                        data: JSON.stringify(feeDeposit),
                        headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
                        success: function (rno) {
                            $.unblockUI();
                            //$("#TransportCost").val($("#hdnTPCost").val());
                            getStudentFeeData();
                            if (rno != 0)
                                DisplayFeeReceipt(rno);
                            else
                                alert("Some error occur, please try again.");

                        },
                        failure: function (response) {
                            $.unblockUI();
                            alert('Unable to perform event. Please try again.');
                        },
                    })
                },
                failure: function (response) {
                    $.unblockUI();
                    alert('Unable to perform event. Please try again.');
                },
                error: function (response) {
                    $.unblockUI();
                    alert('Unable to perform event. Please try again.');
                }
            });
        }
    }
    else {
        return false;
    }
}
function DisplayFeeReceipt(receiptNo) {
    BlockUI();
    $.ajax({
        url: UrlBase + '/Fee/GetFeeReceipt/',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        cache: false,
        data: '{"ReceiptNo":"' + receiptNo + '"}',
        success: function (response) {
            $.unblockUI();
            $("#FeeReceipt").html(response);
            $("#popupDiv").show();

        },
        failure: function (response) {
            alert('Unable to perform event. Please try again.');
            $.unblockUI();
        },
    })
}

function CloseReceiptDiv() {
    $("#FeeReceipt").empty();
    $("#popupDiv").hide();
}

function ValidatePassword() {
    if (inputValidation()) {
        if ($.trim($("#NewPassword").val()) != $.trim($("#ConfirmPassword").val())) {
            alert("New Password and confirm password should be match.");
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}
function LeaveValidation() {
    if (inputValidation()) {
        if (parseFloat($.trim($("#LeaveType").val().split('_')[1])) < parseFloat($("#spnTotalLeaves").html())) {
            alert("Selected leave type has not enough balance.");
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}
function updateRequest(Lid) {
    var ApproverRemark = $.trim($("#txtApproverRemark_" + Lid).val());
    var requestStatus = $.trim($("#optAction_" + Lid).val());
    if (ApproverRemark == '') {
        alert("Please enter remark");
        $("#txtApproverRemark_" + Lid).focus();
        return false;
    }
    else if (requestStatus == '') {
        alert("Please select status");
        $("#optAction_" + Lid).focus();
        return false;
    }
    else {

        var model = {
            "Lid": Lid,
            "ApproverRemark": ApproverRemark,
            "LeaveStatus": requestStatus,
            "Action": "UPD",
        }
        BlockUI();
        $.ajax({
            type: "POST",
            url: UrlBase + '/Home/UpdateLeaveRequest',
            headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $.unblockUI();
                if (response)
                    alert("Request updated successfully.");
                else
                    alert("Request could not update.");

                window.location.href = UrlBase + "/Home/Leave";
            },
            failure: function (response) {
                alert(response.d);
                $.unblockUI();
            },
            error: function (response) {
                alert(response.d);
                $.unblockUI();
            }
        });
    }

}



//region FeeConcession
var FeeConcessionDetails = null;
var flag = 0;
function GetSectionList(d, op) {

    if (op == "All") {
        $("#ddlSectionList").empty().append('<option value="0">All Section</option>');
    }
    else {
        $("#ddlSectionList").empty().append('<option value="0">--Select--</option>');
    }
    if (d.value != "0") {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Fee/GetSectionList/',
            data: '{"ClassID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    for (var l = 0; l < data.length; l++) {
                        $("#ddlSectionList").append('<option value="' + data[l].Secmid + '">' + data[l].SectionName + '</option>');
                    }
                    $("#ddlSectionList").trigger("chosen:updated");
                }
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function searchStudentDetails() {

    $("#StudentTbl").empty();
    if ($.trim($("#txtAdmissionNumber").val()) != "") {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Fee/GetStudentList/',
            data: '{"ClassID":"0","SectionID":"0","AdmissionNo":"' + $.trim($("#txtAdmissionNumber").val()) + '"}',
            dataType: "json",
            success: function (data) {
                DisplayStudentList(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }

}
function ListClassStudent() {

    $("#StudentTbl").empty();
    if ($("#ddlClassList").val() != 0) {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Fee/GetStudentList/',
            data: '{"ClassID":"' + $("#ddlClassList").val() + '","SectionID":"0","ApplicationNo":""}',
            dataType: "json",
            success: function (data) {
                DisplayStudentList(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function ListSectionStudent() {

    $("#StudentTbl").empty();
    if ($("#ddlSectionList").val() != 0) {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Fee/GetStudentList/',
            data: '{"ClassID":"' + $("#ddlClassList").val() + '","SectionID":"' + $("#ddlSectionList").val() + '","ApplicationNo":""}',
            dataType: "json",
            success: function (data) {
                DisplayStudentList(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function DisplayStudentList(record) {
    BlockUI();

    var html = '<table id="roletbl" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        html += '<th>SNo</th>';
        html += '<th>Student Name</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '<th>Class</th>';
        html += '<th>Section</th>';
        html += '<th>Father Name</th>';
        html += '<th>Image</th>';
        html += '<th>Action</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            html += '<td>' + parseInt(l + 1) + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].Adminssionno + '</td>';
            html += '<td>' + record[l].RollNo + '</td>';
            html += '<td>' + record[l].ClassName + '</td>';
            html += '<td>' + record[l].SectionName + '</td>';
            html += '<td>' + record[l].Bd_fathername + '</td>';
            html += '<td><img src="' + record[l].studentimage + '" height="70px;" width="70px;" /></td>'; //
            html += '<td><input type="button" value="Apply" class="btn btn-primary" onclick="return ApplyFeeConcession(\'' + record[l].Smid + '\',\'' + record[l].FeeConID + '\');" /></td>';
            html += '</tr>';
        }
        html += '</tbody>';
        $("#StudentTbl").html(html)
    }
    else {
        alert("No Student Registered.");
    }
    $.unblockUI();
}
function ApplyFeeConcession(studentid, feeconid) {
    FeeConcessionDetails = null;
    flag = 0;
    $("#hdnFeeConcessionID").val(feeconid);
    $("#hdnStudentID").val(studentid);
    $(".MonthAmount").val('');
    $("#divFeeConcessionDetails").hide();

    if (feeconid != 0) {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Fee/GetAssignFeeConcession/',
            data: '{"FeeConID":"' + feeconid + '"}',
            dataType: "json",
            async: true,
            success: function (FeeConData) {
                FeeConcessionDetails = FeeConData;
                $("#hdnAmount").val(FeeConData.Amount);
                $("#txtFeeConcession").val(FeeConData.Amount);
                $("#txtApr").val(FeeConData.Apr);
                $("#txtMay").val(FeeConData.May);
                $("#txtJun").val(FeeConData.Jun);
                $("#txtJul").val(FeeConData.Jul);
                $("#txtAug").val(FeeConData.Aug);
                $("#txtSep").val(FeeConData.Sep);
                $("#txtOct").val(FeeConData.Oct);
                $("#txtNov").val(FeeConData.Nov);
                $("#txtDec").val(FeeConData.Dec);
                $("#txtJan").val(FeeConData.Jan);
                $("#txtFeb").val(FeeConData.Feb);
                $("#txtMar").val(FeeConData.Mar);
                $.unblockUI();
            },
            error: function (data) {
            }
        });
    }
    $("#divFeeConcessionDetails").show();
    $('#popupDiv').modal('show');
}
function AssignFeeConcession() {

    var FeeConcessionDetails = {
        "FeeConID": $("#hdnFeeConcessionID").val() != 0 ? $("#hdnFeeConcessionID").val() : 0,
        "StudentID": $("#hdnStudentID").val(),
        "Amount": $("#hdnAmount").val() == "" ? "0" : $("#hdnAmount").val(),
        "Apr": $("#txtApr").val() == "" ? "0" : $("#txtApr").val(),
        "May": $("#txtMay").val() == "" ? "0" : $("#txtMay").val(),
        "Jun": $("#txtJun").val() == "" ? "0" : $("#txtJun").val(),
        "Jul": $("#txtJul").val() == "" ? "0" : $("#txtJul").val(),
        "Aug": $("#txtAug").val() == "" ? "0" : $("#txtAug").val(),
        "Sep": $("#txtSep").val() == "" ? "0" : $("#txtSep").val(),
        "Oct": $("#txtOct").val() == "" ? "0" : $("#txtOct").val(),
        "Nov": $("#txtNov").val() == "" ? "0" : $("#txtNov").val(),
        "Dec": $("#txtDec").val() == "" ? "0" : $("#txtDec").val(),
        "Jan": $("#txtJan").val() == "" ? "0" : $("#txtJan").val(),
        "Feb": $("#txtFeb").val() == "" ? "0" : $("#txtFeb").val(),
        "Mar": $("#txtMar").val() == "" ? "0" : $("#txtMar").val(),
    }
    BlockUI();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Fee/FeeConcession/',
        data: JSON.stringify(FeeConcessionDetails),
        dataType: "json",
        success: function (data) {
            if (data == 1) {
                alert("Updated successfully.");
                //$("#popupDiv").hide();
                $('#popupDiv').modal('hide');
            }
            else {
                alert("Could not update.");
                //$("#popupDiv").hide();
                $('#popupDiv').modal('hide');
            }
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}
//endregion FeeConcession

function GetDueFeeList() {
    $("#StudentTbl").empty().hide();
    if ($("#ClassID").val() == 0 || $("#ClassID").val() == "") {
        alert("Please select class.");
        $("#ClassID").focus();
        return false;
    }
    else if ($("#ddlSectionList").val() == 0 || $("#ddlSectionList").val() == "") {
        alert("Please select section.");
        $("#ddlSectionList").focus();
        return false;
    }
    else if ($("#FromMonth").val() == 0 || $("#FromMonth").val() == "") {
        alert("Please select start Month");
        $("#FromMonth").focus();
        return false;
    }
    else if ($("#ToMonth").val() == 0 || $("#ToMonth").val() == "") {
        alert("Please select end Month");
        $("#ToMonth").focus();
        return false;
    }
    else if (parseInt($("#FromMonth").val()) > parseInt($("#ToMonth").val())) {
        alert("Start month can not be greater than end month");
        $("#FromMonth").focus();
        return false;
    }
    BlockUI();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Fee/GetDueFeeList/',
        data: '{"ClassID":"' + $("#ClassID").val() + '","SectionID":"' + $("#ddlSectionList").val() + '","FromMonth":"' + $("#FromMonth").val() + '","ToMonth":"' + $("#ToMonth").val() + '"}',
        dataType: "json",
        success: function (data) {
            DisplayDueFeeStudentList(data);
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}
function DisplayDueFeeStudentList(record) {
    BlockUI();
    var html = '<table id="collectiontbllist" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        //html += '<th></th>';
        html += '<th>SNo</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '<th>Student Name</th>';
        html += '<th>Father Name</th>';
        html += '<th>Month</th>';
        html += '<th>School Fee</th>';
        html += '<th>Contact</th>';
        html += '<th>Details</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            //html += '<td><input type="checkbox" /></td>';
            html += '<td>' + (l + 1) + '</td>';
            html += '<td>' + record[l].Adminssionno + '</td>';
            html += '<td>' + record[l].RollNo + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].Bd_fathername + '</td>';
            html += '<td>' + record[l].UnPaidMonth + '</td>';
            html += '<td>' + record[l].PendingAmount + '</td>';
            html += '<td>' + record[l].Bd_contactno + '</td>';
            html += '<td><a href="#" onclick="return getDueFeeDetails(\'' + record[l].FeeGroup + '\',\'' + record[l].Adminssionno + '\',\'' + record[l].UnPaidMonth + '\')">Details</a></td>';
            html += '</tr>';
        }
        html += '</tbody>';
        $("#StudentTbl").html(html).show();
        $('#collectiontbllist').DataTable({
            "scrollX": true,
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excel',
                    title: function () {
                        return 'Due Fee List';
                    },
                    text: '<i class="fa fa-file-pdf-o"> Excel</i>',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: 'th:not(:last-child)'
                    }
                },
            ]
        });
    }
    else {
        alert("No record found.");
    }
    $.unblockUI();
}
function getDueFeeDetails(FeeGroupName, AdmissionNo, UnPaidMonth) {
    if (FeeGroupName == "") {
        alert("Mo fee group allocated to this student.");
        return false;
    }
    BlockUI();
    $.ajax({
        url: UrlBase + '/Fee/GetUnPaidMonthDetails/',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        cache: false,
        data: '{"FeeGroupName":"' + FeeGroupName + '","AdmissionNo":"' + AdmissionNo + '","UnPaidMonth":"' + UnPaidMonth + '"}',
        success: function (response) {
            $.unblockUI();
            $("#UnpaidMonthDetails").html(response);
            $("#popupDiv").show();

        },
        failure: function (response) {
            alert('Unable to perform event. Please try again.');
            $.unblockUI();
        },
    })
}

function GetTotalDueFeeList() {
    $("#StudentTbl").empty().hide();
    if ($("#ClassID").val() == 0 || $("#ClassID").val() == "") {
        alert("Please select class.");
        $("#ClassID").focus();
        return false;
    }
    else if ($("#ddlSectionList").val() == 0 || $("#ddlSectionList").val() == "") {
        alert("Please select section.");
        $("#ddlSectionList").focus();
        return false;
    }
    BlockUI();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Fee/GetTotalDueFeeList/',
        data: '{"ClassID":"' + $("#ClassID").val() + '","SectionID":"' + $("#ddlSectionList").val() + '"}',
        dataType: "json",
        success: function (data) {
            DisplayTotalDueFeeStudentList(data);
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}
function DisplayTotalDueFeeStudentList(record) {
    BlockUI();
    var html = '<table id="collectiontbllist" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        //html += '<th></th>';
        html += '<th>SNo</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '<th>Student Name</th>';
        html += '<th>Father Name</th>';
        html += '<th>Month</th>';
        html += '<th>School Fee</th>';
        html += '<th>Contact</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            //html += '<td><input type="checkbox" /></td>';
            html += '<td>' + (l + 1) + '</td>';
            html += '<td>' + record[l].Adminssionno + '</td>';
            html += '<td>' + record[l].RollNo + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].Bd_fathername + '</td>';
            html += '<td>' + record[l].UnPaidMonth + '</td>';
            html += '<td>' + record[l].PendingAmount + '</td>';
            html += '<td>' + record[l].Bd_contactno + '</td>';
            html += '</tr>';
        }
        html += '</tbody>';
        $("#StudentTbl").html(html).show();
        $('#collectiontbllist').DataTable({
            "scrollX": true,
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excel',
                    title: function () {
                        return 'Due Fee List';
                    },
                    text: '<i class="fa fa-file-pdf-o"> Excel</i>',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: 'th:not(:last-child)'
                    }
                },
            ]
        });
    }
    else {
        alert("No record found.");
    }
    $.unblockUI();
}


function setStudentDetailsIndiscipline(ui) {
    if (ui == undefined) {
        $("#studentPhoto").attr("src", "../Content/Student/dummystudent.png");
        $(".StudentID").val("");
        $(".FirstNameSpan").html("");
        $(".FirstName").val("");
        $(".FatherNameSpan").html("");
        $(".FatherName").val("");
        $(".AdmissionNoSpan").html("");
        $(".AdmissionNo").val("");
        $(".ClassNameSpan").html("");
        $(".ClassName").val("");
        $(".SectionNameSpan").html("");
        $(".SectionName").val("");
    }
    else {
        // BlockUI();
        $("#studentPhoto").attr("src", ui.item.Photo.replace('~', '..'));
        $(".StudentID").val(ui.item.id);
        $(".FirstNameSpan").html(ui.item.FirstName);
        $(".FirstName").val(ui.item.FirstName);
        $(".FatherNameSpan").html(ui.item.FatherName);
        $(".FatherName").val(ui.item.FatherName);
        $(".AdmissionNoSpan").html(ui.item.AdmissionNo);
        $(".AdmissionNo").val(ui.item.AdmissionNo);
        $(".ClassNameSpan").html(ui.item.ClassName);
        $(".ClassName").val(ui.item.ClassName);
        $(".SectionNameSpan").html(ui.item.SectionName);
        $(".SectionName").val(ui.item.SectionName);
        $(".FeeGroupSpan").html(ui.item.FeeGroup);
        $(".FeeGroup").val(ui.item.FeeGroup);
    }
}
function IndisciplineValidation() {
    if (inputValidation()) {
        if ($(".AdmissionNo").val() == "") {
            alert("Please select student.");
            $("#txtStudent").focus();
            return false;
        }
    }
    else {
        return false;
    }
}
function GetIndisciplineFeeReceipt(IndisciplineID, AdmissionNo) {
    BlockUI();
    $.ajax({
        url: UrlBase + '/Fee/GetIndisciplineFeeReceipt/',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        cache: false,
        data: '{"IndisciplineID":"' + IndisciplineID + '","AdmissionNo":"' + AdmissionNo + '"}',
        success: function (response) {
            $.unblockUI();
            $("#IndisciplineFeeReceipt").html(response);
            $("#popupDiv").show();

        },
        failure: function (response) {
            alert('Unable to perform event. Please try again.');
            $.unblockUI();
        },
    })
}


function GetClassUnAllocatedStudentList() {
    $("#feeStudentTbl").empty();
    $("#feeStudentDiv").hide();
    BlockUI();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Fee/UnallocatedFeeStudentMaster/',
        data: '{"ClassID":"' + $("#ClassName").val() + '"}',
        dataType: "json",
        success: function (data) {
            var html = '<table class="table">';
            if (data.length > 0) {
                html += '<thead class="thead-lights">';
                html += '<tr>';
                html += '<th>Select</th>';
                html += '<th>Admission No</th>';
                html += '<th>Student Name</th>';
                html += '<th>Father Name</th>';
                html += '<th>Class/Section</th>';
                html += '</thead>';
                html += '<tbody>';
                for (var l = 0; l < data.length; l++) {
                    html += '<tr>';
                    html += '<input name="StudentList[' + l + '].Smid" id="StudentList_' + l + '__Smid" type="hidden" value="' + data[l].Smid + '">';
                    html += '<input name="StudentList[' + l + '].SecMid" id="StudentList_' + l + '__SecMid" type="hidden" value="' + data[l].SecMid + '">';
                    html += '<input name="StudentList[' + l + '].Firstname" id="StudentList_' + l + '__Firstname" type="hidden" value="' + data[l].Firstname + '">';
                    html += '<td><input name="StudentList[' + l + '].Check" id="StudentList_' + l + '__Check" type="checkbox" value="' + data[l].Check + '" data-val-required="The Check field is required." data-val="true" class="chkStudent" ></td>';
                    html += '<td>' + data[l].Adminssionno + '</td>';
                    html += '<td>' + data[l].Firstname + '</td>';
                    html += '<td>' + data[l].Bd_fathername + '</td>';
                    html += '<td>' + data[l].ClassName + '/' + data[l].SectionName + '</td>';
                    html += '</tr>';
                }
                html += '</tbody>';
                $("#feeStudentTbl").html(html)
                $("#feeStudentDiv").show();
                $.unblockUI();
            }
            else {
                $("#feeStudentTbl").html("No record found.")
                $("#feeStudentDiv").show();
                $.unblockUI();
            }
        },
        error: function (data) {
        }
    });
}
function UnAllocatedValidation() {
    if ($(".chkStudent").filter(":checked").length == 0) {
        alert("Please select atleast one student.");
        return false;
    }
    else if ($("#FeeGroupName").val() == "") {
        alert("Please select Fee Group.");
        $("#FeeGroupName").focus();
        return false;
    }
}



function setStudentDetailsExtraFee(ui) {
    if (ui == undefined) {
        $("#studentPhoto").attr("src", "../Content/Student/dummystudent.png");
        $(".StudentID").val("");
        $(".FirstNameSpan").html("");
        $(".FirstName").val("");
        $(".FatherNameSpan").html("");
        $(".FatherName").val("");
        $(".AdmissionNoSpan").html("");
        $(".AdmissionNo").val("");
        $(".ClassNameSpan").html("");
        $(".ClassName").val("");
        $(".SectionNameSpan").html("");
        $(".SectionName").val("");
        $("#FeeHead").empty().append('<option value="">--Select--</option>');
    }
    else {
        // BlockUI();
        $("#studentPhoto").attr("src", ui.item.Photo.replace('~', '..'));
        $(".StudentID").val(ui.item.id);
        $(".FirstNameSpan").html(ui.item.FirstName);
        $(".FirstName").val(ui.item.FirstName);
        $(".FatherNameSpan").html(ui.item.FatherName);
        $(".FatherName").val(ui.item.FatherName);
        $(".AdmissionNoSpan").html(ui.item.AdmissionNo);
        $(".AdmissionNo").val(ui.item.AdmissionNo);
        $(".ClassNameSpan").html(ui.item.ClassName);
        $(".ClassName").val(ui.item.ClassName);
        $(".SectionNameSpan").html(ui.item.SectionName);
        $(".SectionName").val(ui.item.SectionName);
        $(".FeeGroupSpan").html(ui.item.FeeGroup);
        $(".FeeGroup").val(ui.item.FeeGroup);
        GetFeeHeadDetails();
    }
}
function GetFeeHeadDetails() {
    $.ajax({
        type: "POST",
        url: UrlBase + '/Fee/GetFeeHeadDetails',
        data: '{"feeGroupName":"' + $(".FeeGroup").val() + '","AdmissionNo":"' + $(".AdmissionNo").val() + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var html = "";
            if (response.length > 0) {

                $("#FeeHead").empty().append('<option value="">--Select--</option>');
                for (var l = 0; l < response.length; l++) {
                    if (response[l].Name != "Concession")
                        $("#FeeHead").append('<option value="' + response[l].Name + '">' + response[l].Name + '</option>');
                }
                $("#FeeHead").trigger("chosen:updated");
            }
            else {
                alert('No Fee Group allocated to this student.');
            }
            $.unblockUI();
        },
        failure: function (response) {
            $.unblockUI();
        },
        error: function (response) {
            $.unblockUI();
        }
    });
}
function ExtraFeeDeposit() {
    if ($("#FeeHead").val() == "") {
        alert("Please select Fee Head.");
        $("#FeeHead").focus();
        return false;
    }
    if ($.trim($("#Amount").val()) == 0 || $.trim($("#Amount").val()) == "") {
        alert("Please enter Amount.");
        $("#Amount").val("");
        $("#Amount").focus();
        return false;
    }
    if ($("#MOP").val() == "") {
        alert("Please select mode of payment.");
        $("#MOP").focus();
        return false;
    }
    if ($("#Remarks").val() == "") {
        alert("Please enter remark.");
        $("#Remarks").focus();
        return false;
    }
    var TotalFee = parseInt($.trim($("#Amount").val())) + parseInt($.trim($("#Fine").val()));
    var TotalFeesBreakUp = $("#FeeHead").val() + "#" + $.trim($("#Amount").val()) + "_Fine#" + $.trim($("#Fine").val());
    var feeDeposit = {
        "Action": "ExtraDeposit",
        "AdmissionNo": $("#Student_Adminssionno").val(),
        "Balance": 0,
        "Concession": 0,
        "Fine": $("#Fine").val(),
        "months": "",
        "MOP": $("#MOP").val(),
        "MOPRemark": "",
        "Payment": parseInt(TotalFee),
        "Remarks": $("#Remarks").val(),
        "StudentID": $("#Student_Smid").val(),
        "StudentName": $("#Student_Firstname").val(),
        "TotalFees": parseInt(TotalFee),
        "TotalFeesBreakUp": TotalFeesBreakUp,
        "WaiveOff": "0",
        "FeeGroup": $("#Student_FeeGroup").val(),
        "Arrears": "0",
    }
    $.ajax({
        url: UrlBase + '/Fee/ExtraFeeDeposit/',
        contentType: 'application/json; charset=utf-8',
        type: 'POST',
        dataType: "json",
        data: JSON.stringify(feeDeposit),
        headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
        success: function (rno) {
            $.unblockUI();
            //$("#TransportCost").val($("#hdnTPCost").val());
            //getStudentFeeData();
            if (rno != 0)
                DisplayFeeReceipt(rno);
            else
                alert("Some error occur, please try again.");

        },
        failure: function (response) {
            $.unblockUI();
            alert('Unable to perform event. Please try again.');
        },
    })
}


function GetDueFeesMonthWise() {
    $("#StudentTbl").empty().hide();

    if ($("#Session").val() == 0 || $("#Session").val() == "") {
        alert("Please select Session");
        $("#Session").focus();
        return false;
    }
    else if ($("#TillMonth").val() == 0 || $("#ToMonth").val() == "") {
        alert("Please select Month");
        $("#ToMonth").focus();
        return false;
    }
    else if ($("#ClassID").val() == 0 || $("#ClassID").val() == "") {
        alert("Please select class.");
        $("#ClassID").focus();
        return false;
    }
    else if ($("#ddlSectionList").val() == 0 || $("#ddlSectionList").val() == "") {
        alert("Please select section.");
        $("#ddlSectionList").focus();
        return false;
    }
    BlockUI();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Fee/DueFeesMonthWise/',
        data: '{"ClassID":"' + $("#ClassID").val() + '","SectionID":"' + $("#ddlSectionList").val() + '","TillMonth":"' + $("#TillMonth").val() + '","Session":"' + $("#Session").val() + '"}',
        dataType: "json",
        success: function (data) {
            DisplayTotalDueFeeStudentList(data);
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}
function DisplayTotalDueFeeStudentList(record) {
    BlockUI();
    var html = '<table id="collectiontbllist" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        //html += '<th></th>';
        html += '<th>SNo</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '<th>Student Name</th>';
        html += '<th>Father Name</th>';
        html += '<th>Month</th>';
        html += '<th>School Fee</th>';
        html += '<th>Contact</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            //html += '<td><input type="checkbox" /></td>';
            html += '<td>' + (l + 1) + '</td>';
            html += '<td>' + record[l].Adminssionno + '</td>';
            html += '<td>' + record[l].RollNo + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].Bd_fathername + '</td>';
            html += '<td>' + record[l].UnPaidMonth + '</td>';
            html += '<td>' + record[l].PendingAmount + '</td>';
            html += '<td>' + record[l].Bd_contactno + '</td>';
            html += '</tr>';
        }
        html += '</tbody>';
        $("#StudentTbl").html(html).show();
        $('#collectiontbllist').DataTable({
            "scrollX": true,
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'excel',
                    title: function () {
                        return 'Due Fee List';
                    },
                    text: '<i class="fa fa-file-pdf-o"> Excel</i>',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: 'th:not(:last-child)'
                    }
                },
            ]
        });
    }
    else {
        alert("No record found.");
    }
    $.unblockUI();
}