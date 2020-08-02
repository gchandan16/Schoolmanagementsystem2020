function showdiv() {
    debugger;
   
    if ($("#travelExpHDRMaster_fromDate").val() != "" && $("#travelExpHDRMaster_toDate").val() != "" && $("#travelExpHDRMaster_UserCode").val() != "") {

        ShowWaitingSymbol();

        $("#dvtravelexp").load('/Travel/_TravelExpenseDetail?userCode=' + $("#travelExpHDRMaster_UserCode").val() + '&fromDate=' + $("#travelExpHDRMaster_fromDate").val() + '&toDate=' + $("#travelExpHDRMaster_toDate").val());
        HideWaitingSymbol();
    }
    else {
        alert('Please enter all mandetory fields');

    }
}
function tab5open(input) {

    $("#" + $(input).attr("aria-controls")).show();
    $("#steps-uid-0-p-0").hide();
    $("#steps-uid-0-p-1").hide();
    $("#steps-uid-0-p-2").hide();
    $("#steps-uid-0-p-3").hide();
    $("#steps-uid-0-p-4").hide();

    $("#btnsubmit").hide();
    $("#mainul li").removeClass("current");
    $(input).parent().addClass("current");
}
function tab1open(input) {
    $("#" + $(input).attr("aria-controls")).show();
    $("#steps-uid-0-p-0").hide();
    $("#steps-uid-0-p-2").hide();
    $("#steps-uid-0-p-3").hide();
    $("#steps-uid-0-p-4").hide();
    $("#steps-uid-0-p-5").hide();
    $("#btnsubmit").hide();
    $("#mainul li").removeClass("current");
    $(input).parent().addClass("current");
}
function tab2open(input) {
    $("#" + $(input).attr("aria-controls")).show();
    $("#steps-uid-0-p-0").hide();
    $("#steps-uid-0-p-1").hide();
    $("#steps-uid-0-p-3").hide();
    $("#steps-uid-0-p-4").hide();
    $("#steps-uid-0-p-5").hide();
    $("#btnsubmit").hide();
    $("#mainul li").removeClass("current");
    $(input).parent().addClass("current");
}
function tab3open(input) {
    $("#" + $(input).attr("aria-controls")).show();
    $("#steps-uid-0-p-0").hide();
    $("#steps-uid-0-p-1").hide();
    $("#steps-uid-0-p-2").hide();
    $("#steps-uid-0-p-4").hide();
    $("#steps-uid-0-p-5").hide();
    $("#btnsubmit").hide();
    $("#mainul li").removeClass("current");
    $(input).parent().addClass("current");
}
function tab0open(input) {
    $("#" + $(input).attr("aria-controls")).show();
    $("#steps-uid-0-p-1").hide();
    $("#steps-uid-0-p-2").hide();
    $("#steps-uid-0-p-3").hide();
    $("#steps-uid-0-p-4").hide();
    $("#steps-uid-0-p-5").hide();
    $("#btnsubmit").hide();
    $("#mainul li").removeClass("current");
    $(input).parent().addClass("current");
}
function tab4open(input) {
   
    getSummary();
    var tfnamt = gettiffintotal();
    $("#total_tiffenxp").text(tfnamt.toFixed(2));
    var ldngamt = getlodgingtotal(ldngamt);
    $("#total_lodgingexp").text(ldngamt.toFixed(2));
    var othramt = getothertotal();
    $("#total_otherexp").text(othramt.toFixed(2))
    var trvlamt = gettravelTotal();
    $("#total_travelexp").text(trvlamt.toFixed(2));
    var fareamt = getfaretotal();
    $("#total_fareexp").text(fareamt.toFixed(2));
    var ttotalamt = tfnamt + ldngamt + othramt + trvlamt + fareamt;
    $("#total_totalexp").text(ttotalamt.toFixed(2));


    //$("#" + $(input).attr("aria-controls")).show();
    //$("#steps-uid-0-p-0").hide();
    //$("#steps-uid-0-p-1").hide();
    //$("#steps-uid-0-p-2").hide();
    //$("#steps-uid-0-p-3").hide();
    //$("#steps-uid-0-p-5").hide();
    $("#btnsubmit").show();

    //$("#mainul li").removeClass("current");
    //$(input).parent().addClass("current");
}
//**************Total expense start

//Get Summary
function getSummary() {
 
    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
 
    
    $("#finalexp tbody").empty();
    while (sdt <= enddt) {
        var fare_amt = 0.00;
        var trvl_amt = 0.00;
        var tiffn_amt = 0.00;
        var lodging_amt = 0.00;
        var ather_amt = 0.00;
        var total_amt = 0.00;
        var date = sdt;

        $('#fareexp tbody tr').each(function (i, tr) {
            var frdt =formatDate($(tr).find("td:nth-child(3) input[type=text]").val());
            if (frdt != '' && frdt == formatDate(date.toString())) {
            if ($(tr).find("td:nth-child(8) input[type=text]").val() != '') {
                fare_amt = parseFloat(fare_amt) + parseFloat($(tr).find("td:nth-child(8) input[type=text]").val());
                }
            }
        });

        for (var j = 1; j <= parseInt($("#Noofcount").val()); j++) {
            $('#trvltbl' + j + ' tbody tr').each(function (i, tr) {
                var trvldate = formatDate($(tr).find("td:nth-child(2) input[type=text]").val());
                if (trvldate != '' && trvldate == formatDate(date.toString())) {
                    trvl_amt = parseFloat(trvl_amt) + parseFloat($(tr).find("td:nth-child(7) input[type=text]").val());
                }
            });

        }

        $('#tiffinexp tbody tr').each(function (i, tr) {
            var tfndate = formatDate($(tr).find("td:nth-child(2) input[type=text]").val());
            if (tfndate != '' && tfndate == formatDate(date.toString())) {
                if ($(tr).find("td:nth-child(3) input[type=text]").val() != "") {
                    tiffn_amt = parseFloat(tiffn_amt) + parseFloat($(tr).find("td:nth-child(3) input[type=text]").val());
                }
            }
        });

        $('#lodegingexp tbody tr').each(function (i, tr) {
            var lodgdate = formatDate($(tr).find("td:nth-child(3) input[type=text]").val());
            if (lodgdate != '' && lodgdate == formatDate(date.toString())) {
                if ($(tr).find("td:nth-child(9) input[type=text]").val() != '') {
                    lodging_amt = parseFloat(lodging_amt) + parseFloat($(tr).find("td:nth-child(9) input[type=text]").val());
                }
            }
        });

        $('#otherexp tbody tr').each(function (i, tr) {
            var othrdate = formatDate($(tr).find("td:nth-child(2) input[type=text]").val());
            if (othrdate != "" && othrdate == formatDate(date.toString())) {
            if ($(tr).find("td:nth-child(3) input[type=text]").val() != '') {
                ather_amt = parseFloat(ather_amt) + parseFloat($(tr).find("td:nth-child(3) input[type=text]").val());
                }
            }
        });

        total_amt = parseFloat(fare_amt) + parseFloat(trvl_amt) + parseFloat(tiffn_amt) + parseFloat(lodging_amt) + parseFloat (ather_amt);
        
        if (fare_amt != 0.00 || trvl_amt != 0.00 || tiffn_amt != 0.00 || lodging_amt != 0.00 || ather_amt != 0.00) {
            $("#finalexp tbody").append('<tr><td>' + formatDate(date.toString()) + '</td>' +
                '<td>' + fare_amt.toFixed(2) + '</td>' +
                '<td>' + trvl_amt.toFixed(2) + '</td>' +
                '<td>' + tiffn_amt.toFixed(2) + '</td>' +
                '<td>' + lodging_amt.toFixed(2) + '</td>' +
                '<td>' + ather_amt.toFixed(2)+ '</td>' +
                '<td>' + total_amt.toFixed(2) + '</td>' +
                '</tr>');
        }
         
        sdt.setDate(sdt.getDate() + 1);
         
    }

}
//end summary
function gettiffintotal() {
    var tfntotal = 0.00;   
    $('#tiffinexp tbody tr').each(function (i, tr) {
        if($(tr).find("td:nth-child(3) input[type=text]").val() != '') {
            tfntotal = parseFloat(tfntotal) + parseFloat($(tr).find("td:nth-child(3) input[type=text]").val());
        }
    });
    return tfntotal;
}
function getlodgingtotal() {
    var ldngtotal = 0.00;
    $('#lodegingexp tbody tr').each(function (i, tr) {
        if ($(tr).find("td:nth-child(9) input[type=text]").val() != '') {
            ldngtotal = parseFloat(ldngtotal) + parseFloat($(tr).find("td:nth-child(9) input[type=text]").val());
        }
    });
    return ldngtotal;
}
function getothertotal() {
    var othertotal = 0.00;
    $('#otherexp tbody tr').each(function (i, tr) {
        if ($(tr).find("td:nth-child(3) input[type=text]").val() != '') {
            othertotal = parseFloat(othertotal) + parseFloat($(tr).find("td:nth-child(3) input[type=text]").val());
        }
    });
    return othertotal;
}
function gettravelTotal() {

    var trvlsum = 0.00;
    for (var j = 1; j <= parseInt($("#Noofcount").val()); j++) {
        $('#trvltbl' + j + ' tbody tr').each(function (i, tr) {
            if ($(tr).find("td:nth-child(7) input[type=text]").val() != '') {
                trvlsum = parseFloat(trvlsum) + parseFloat($(tr).find("td:nth-child(7) input[type=text]").val());
            }
        });

    }
    return trvlsum;
}
function getfaretotal() {
    var faretotal = 0.00;
    $('#fareexp tbody tr').each(function (i, tr) {
        if ($(tr).find("td:nth-child(8) input[type=text]").val() != '') {
            faretotal = parseFloat(faretotal) + parseFloat($(tr).find("td:nth-child(8) input[type=text]").val());
        }
    });
    return faretotal;
}
function suminline(input) {
    //alert($(input).attr("data-id"));
    var id = $(input).attr("data-id");
    var amt = $("#lodgingamt_" + id).val() == "" ? 0 : $("#lodgingamt_" + id).val();
    var daamt = $("#daAmt_" + id).val() == "" ? 0 : $("#daAmt_" + id).val();
    var sum = parseFloat(amt) + parseFloat(daamt)
    $("#totalamt_" + id).val(sum.toFixed(2));
}
//**********************END
function deleteRow(input) {
    try {
        var rowid = $(input).attr("data-name");//"otherrw" + Id;
        var tblname = $(input).attr("data-tbl");

        $("#" + rowid).remove();
        var table = document.getElementById(tblname).getElementsByTagName('tbody')[0];
        var rowCount = table.rows.length;

        var newserialnumber = 1;
        for (var i = 1; i < rowCount; i++) {
            newserialnumber = newserialnumber + 1;
            var row = table.rows[i];
            var serialtd = row.cells[0];
            serialtd.innerHTML = newserialnumber;
        }
        serialnumber = newserialnumber;
    }
    catch (e) {
        alert(e);
    }
}
//***********Travel Expense Start******************
function addrowdata(input) {

    var i = $(input).attr("id");

    var nm = "trvltbl" + i;
    var j = $("#trvltbl" + i + " > tbody > tr").length + 1;

    var str = "";
    if (j > 1) {
        str = '<a data-tbl="' + nm + '" data-name="' + nm + j + '" class="btn btn-sm btn-secondary  pt-top " onclick="return deleteRow(this);"><i class="fa fa-trash bg_red"></i></a>';
    }
    $("#subrow" + i).show();
    $("#trvltbl" + i + " tbody").append(
        '<tr id="' + nm + j + '">' + '<td align="center">' + j + '</td>' +
        '<td align="center"><input type="text" id="localconvdate_' + j + '" name="localconvdate_' + j + '" class="form-control DatePicker" onchange="TravelDateValidatin(this)" readonly="readonly"/></td>' +
        '<td align="center"><input type="text" id="fromSource_' + j + '" name="fromSource_' + j + '" class="form-control"/></td>' +
        '<td align="center"><input type="text" id="toDestination_' + j + '" name="toDestination_' + j + '" class="form-control"/></td>' +
        '<td align="center"><input type="text" id="totalDistance_' + j + '" name="totalDistance_' + j + '" class="form-control numberinput"/></td>' +
        '<td align="center"><select id="travelMode_' + j + '" name="travelMode_' + j + '" class="form-control">' + $("#HiddenTravelModeId").html() + '</select ></td > ' +
        '<td align="center"><input type="text" id="travleCharge_' + j + '" name="travleCharge_' + j + '" class="form-control numberinput"/></td>' +
        '<td align="center"><input type="text" id="travleRemark_' + j + '" name="travleRemark_' + j + '" class="form-control"/></td>' +
        '<td align="center">' + str + '</td></tr>'
    )

    $('.DatePicker').datepicker({
        format: 'dd/M/yyyy',
        endDate: new Date()

    });
}
//***********Travel Expense End********************
//***********Tiffin Expense Start******************
function AddTiffinRow() {


    var j = $("#tiffinexp > tbody > tr").length + 1;


    $("#tiffinexp tbody").append(
        '<tr id="tiffinrw_' + j + '">' + '<td align="center">' + j + '</td>' +
        '<td align="center"><input type="text" id="tiffindate_' + j + '" name="tiffindate_' + j + '" class="form-control DatePicker" readonly="readonly" onchange="TiffinDateValidation(this)"/></td>' +
        '<td align="center"><input type="text" id="tiffinamount_' + j + '" name="tiffinamount_' + j + '" class="form-control numberinput" /></td>' +
        '<td align="center"><input type="text" id="tiffinremark_' + j + '" name="tiffinremark_' + j + '" class="form-control" /></td>' +
        '<td align="center"><a data-tbl="tiffinexp" data-name="tiffinrw_' + j + '" class="btn btn-sm btn-secondary  pt-top " onclick="return deleteRow(this);"><i class="fa fa-trash bg_red"></i></a></td></tr>'
    )

    $('.DatePicker').datepicker({
        format: 'dd/M/yyyy',
        endDate: new Date()
    });
}
//*********End Tiffin Expense*********************
//********Other Expense Start************************
function AddotherRow() {
    var j = $("#otherexp > tbody > tr").length + 1;


    $("#otherexp tbody").append(
        '<tr id="otherrw_' + j + '">' + '<td align="center">' + j + '</td>' +
        '<td align="center"><input type="text" id="otherdate_' + j + '" name="tiffindate_' + j + '" class="form-control DatePicker" onchange="OtherDateValidation(this)" readonly="readonly"/></td>' +
        '<td align="center"><input type="text" id="otheramount_' + j + '" name="tiffinamount_' + j + '" class="form-control numberinput" /></td>' +
        '<td align="center"><input type="text" id="otherremark_' + j + '" name="tiffinremark_' + j + '" class="form-control" /></td>' +
        '<td align="center"><a data-tbl="otherexp" data-name="otherrw_' + j + '" class="btn btn-sm btn-secondary  pt-top " onclick="return deleteRow(this);"><i class="fa fa-trash bg_red"></i></a></td></tr>'
    )

    $('.DatePicker').datepicker({
        format: 'dd/M/yyyy',
        endDate: new Date()
    });
}
//******End Other Expense****************************
//******Lodging Expense Start***********************
function addLodgingrow() {


    var j = $("#lodegingexp > tbody > tr").length + 1;


    $("#lodegingexp tbody").append(
        '<tr id="lodgingrw' + j + '">' + '<td align="center">' + j + '</td>' +
        '<td align="center"><select id="lodgingCity_' + j + '"  name="lodgingCity_' + j + '" class="form-control">' + $("#HiddenlodgingCityId").html() + '</select ></td>' +
        '<td align="center"><input type="text" id="lodgingfromdate_' + j + '" name="lodgingfromdate_' + j + '" data-todate="lodgingtodate_' + j + '" class="form-control DatePicker" onchange="LodgingFromDateValidatin(this)" readonly="readonly"/></td>' +
        '<td align="center"><input type="text" id="lodgingtodate_' + j + '" name="lodgingtodate_' + j + '" data-fromdate="lodgingfromdate_' + j + '" class="form-control DatePicker" onchange="LodgingToDateValidatin(this)" readonly="readonly"/></td>' +
        '<td align="center"><input type="text" id="nightspent_' + j + '" name="nightspent_' + j + '" class="form-control numberinput"/></td>' +
        '<td align="center"><input type="text" id="lodgingamt_' + j + '" name="lodgingamt_' + j + '" data-id="' + j + '" onblur="suminline(this)" class="form-control numberinput"/></td>' +
        '<td align="center"><input type="text" id="daAmt_' + j + '" name="daAmt_' + j + '" data-id="' + j + '" onblur="suminline(this)" class="form-control numberinput"/></td>' +
        '<td align="center"><input type="text" id="lodgingremark_' + j + '" name="lodgingremark_' + j + '" class="form-control"/></td>' +
        '<td align="center"><input type="text" id="totalamt_' + j + '" name="totalamt_' + j + '" class="form-control numberinput" readonly="readonly"/></td>' +
        '<td align="center"><a data-tbl="lodegingexp" data-name="lodgingrw' + j + '" class="btn btn-sm btn-secondary  pt-top " onclick="return deleteRow(this);"><i class="fa fa-trash bg_red"></i></a></td></tr>'
    )

    $('.DatePicker').datepicker({
        format: 'dd/M/yyyy',
        endDate: new Date()

    });
}
//*****End Lodging Expense
//*****Final Expense Start*****************
//***********FARE EXP STARTtrvlmode_
function addFarerow() {


    var q = $("#fareexp > tbody > tr").length + 1;

    $("#fareexp tbody").append(
        '<tr id="fareexprw_' + q + '">' + '<td align="center">' + q + '</td>' +
        '<td align="center"><select id="fromstation_' + q + '" data-todate="arrivaldate_' + q + '" name="fromstation_' + q + '" class="form-control">' + $("#HiddenCityId").html() + '</select ></td>' +
        '<td align="center"><input type="text" id="depdate_' + q + '" name="depdate_' + q + '" class="form-control DatePicker" onchange="LodgingFromDateValidatin(this)" readonly="readonly"/></td>' +
        '<td align="center"><select id="tostation_' + q + '" name="tostation_' + q + '" class="form-control">' + $("#HiddenCityId").html() + '</select ></td>' +
        '<td align="center"><input type="text" id="arrivaldate_' + q + '" data-fromdate="depdate_' + q + '" name="arrivaldate_' + q + '" class="form-control DatePicker" onchange="LodgingFromDateValidatin(this)" readonly="readonly"/></td>' +
        '<td align="center"><select id="trvlmode_' + q + '" name="trvlmode_' + q + '" class="form-control">' + $("#HiddenTrvlModeId").html() + '</select ></td>' +
        '<td align="center"><input type="text" id="ticketno_' + q + '" name="ticketno_' + q + '" class="form-control"/></td>' +
        '<td align="center"><input type="text" id="fareamt_' + q + '" name="fareamt_' + q + '" class="form-control numberinput"/></td>' +
        '<td align="center"><input type="text" id="fareexpremark_' + q + '" name="fareexpremark_' + q + '" class="form-control"/></td>' +
        '<td align="center"><a data-tbl="fareexp" data-name="fareexprw_' + q + '" class="btn btn-sm btn-secondary  pt-top " onclick="return deleteRow(this);"><i class="fa fa-trash bg_red"></i></a></td></tr>'
    )

    $('.DatePicker').datepicker({
        format: 'dd/M/yyyy',
        endDate: new Date()

    });
}
//***********END FARE EXP
//**********Date Validation start
function TiffinDateValidation(input) {

    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
    var cdate = new Date(formatDate($(input).val()));
    if (Date.parse(cdate) <= Date.parse(enddt) && Date.parse(cdate) >= Date.parse(sdt)) {

        var count = 0;
        $('#tiffinexp tbody tr').each(function (i, tr) {
            if ($(tr).find("td:nth-child(2) input[type=text]").val() != '') {
                var d1 = $(input).val();
                var d2 = $(tr).find("td:nth-child(2) input[type=text]").val();

                if (d1 == d2) {

                    count = count + 1;

                }
            }
        });
        if (count > 1) {
            alert('This date already selected');
            $(input).val('');
        }
    }
    else {
        alert('Please select date between from and to date')
        $(input).val('');
    }

}
function OtherDateValidation(input) {
    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
    var cdate = new Date(formatDate($(input).val()));
    if (Date.parse(cdate) <= Date.parse(enddt) && Date.parse(cdate) >= Date.parse(sdt)) {
        var count = 0;
        $('#otherexp tbody tr').each(function (i, tr) {
            if ($(tr).find("td:nth-child(2) input[type=text]").val() != '') {
                var d1 = $(input).val();
                var d2 = $(tr).find("td:nth-child(2) input[type=text]").val();
                if (d1 == d2) {
                    count = count + 1;
                }
            }
        });
        if (count > 1) {
            alert('This date already selected');
            $(input).val('');
        }
    }
    else {
        alert('Please select date between from and to date')
        $(input).val('');
    }
}
function TravelDateValidatin(input) {
    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
    var cdate = new Date(formatDate($(input).val()));
    if (Date.parse(cdate) <= Date.parse(enddt) && Date.parse(cdate) >= Date.parse(sdt)) {
    }
    else {
        alert('Please select date between from and to date')
        $(input).val('');
    }

}
function LodgingFromDateValidatin(input) {
    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
    var cdate = new Date(formatDate($(input).val()));

    if (Date.parse(cdate) <= Date.parse(enddt) && Date.parse(cdate) >= Date.parse(sdt)) {

        $("#" + $(input).attr('data-todate')).val('');
    }
    else {
        alert('Please select date between from and to date')
        $(input).val('');
        $("#" + $(input).attr('data-todate')).val('');
    }

}
function LodgingToDateValidatin(input) {
    var sdt = new Date(formatDate($("#travelExpHDRMaster_fromDate").val()));
    var enddt = new Date(formatDate($("#travelExpHDRMaster_toDate").val()));
    var cdate = new Date(formatDate($(input).val()));


    if ($("#" + $(input).attr('data-fromdate')).val() != "") {
        if (Date.parse(cdate) <= Date.parse(enddt) && Date.parse(cdate) >= Date.parse(sdt)) {
            var fdate = new Date($("#" + $(input).attr('data-fromdate')).val());
            var ndate = new Date($(input).val());
            if (Date.parse(ndate) < Date.parse(fdate)) {
                alert("To date Always grater than from date");
                $(input).val('');

            }

        }
        else {
            alert('Please select date between from and to date')
            $(input).val('');
            $("#" + $(input).attr('data-todate')).val('');
        }
    }
    else {
        alert('Please select from date')
        $(input).val('');
    }

}
//**********Date Validation End
///*********Save Travel Expense
function savetraveldetails(input) {    
    //********Travle fare expense
    var trvlarr = new Array();
    var k = 0;
    var cnt = 0;
     
    debugger;
    for (var j = 1; j <= parseInt($("#Noofcount").val()); j++) {
        // trvltbl


        var dtlid = $('#trvltbl' + j).attr("data-dsrdtlid");
        $('#trvltbl' + j + ' tbody tr').each(function (i, tr) {

            trvlarr[k] = {
                traveldate: ($(tr).find("td:nth-child(2) input[type=text]")).val(),
                source: ($(tr).find("td:nth-child(3) input[type=text]")).val(),
                destination: ($(tr).find("td:nth-child(4) input[type=text]")).val(),
                distance: ($(tr).find("td:nth-child(5) input[type=text]")).val(),
                travelMode: ($(tr).find("td:nth-child(6) option:selected")).val(),
                plandtlid: parseInt(dtlid),
                charges: ($(tr).find("td:nth-child(7) input[type=text]")).val(),
                remarks: ($(tr).find("td:nth-child(8) input[type=text]")).val(),
            }


            if ($(tr).find("td:nth-child(2) input[type=text]").val() != "" && $(tr).find("td:nth-child(3) input[type=text]").val() != "" && $(tr).find("td:nth-child(4) input[type=text]").val() != "" && $(tr).find("td:nth-child(5) input[type=text]").val() != "" && $(tr).find("td:nth-child(6) option:selected").val() != "" && $(tr).find("td:nth-child(7) input[type=text]").val() != "") {
                cnt++;
            }
            k++;

        });

    }
    //***********END
    $('#XmltravelDetails').val(JSON.stringify(trvlarr));
    //*******save tiffin detail 
    var tiffinarr = new Array();
    $('#tiffinexp tbody tr').each(function (i, tr) {
        tiffinarr[i] = {
            tiffindate: ($(tr).find("td:nth-child(2) input[type=text]")).val(),
            tiffinamount: ($(tr).find("td:nth-child(3) input[type=text]")).val(),
            tiffinremark: ($(tr).find("td:nth-child(4) input[type=text]")).val(),
        }

    });
    $('#XmltiffinDetails').val(JSON.stringify(tiffinarr));
    //*******save tiffin detail end
    //*******save lodegingexp detail
    var lodegingarr = new Array();
    $('#lodegingexp tbody tr').each(function (i, tr) {

        lodegingarr[i] = {
             
            visitedcity: ($(tr).find("td:nth-child(2) option:selected")).val(),
            lodgingfromdate: ($(tr).find("td:nth-child(3) input[type=text]")).val(),
            lodgingtodate: ($(tr).find("td:nth-child(4) input[type=text]")).val(),
            nightspent: ($(tr).find("td:nth-child(5) input[type=text]")).val(),
            lodgingamt: ($(tr).find("td:nth-child(6) input[type=text]")).val(),
            daAmt: ($(tr).find("td:nth-child(7) input[type=text]")).val(),
            lodgingremark: ($(tr).find("td:nth-child(8) input[type=text]")).val(),
            totalamt: ($(tr).find("td:nth-child(9) input[type=text]")).val(),
        }

    });
    $('#XmllodgingDetails').val(JSON.stringify(lodegingarr));
    //*******save lodegingexp detail end

    //*******save otherexp detail
    var otherarr = new Array();
    $('#otherexp tbody tr').each(function (i, tr) {

        otherarr[i] = {
            otherdate: ($(tr).find("td:nth-child(2) input[type=text]")).val(),
            otheramount: ($(tr).find("td:nth-child(3) input[type=text]")).val(),
            otherremark: ($(tr).find("td:nth-child(4) input[type=text]")).val(),

        }

    });
    $('#XmlotherDetails').val(JSON.stringify(otherarr));
    //*******save otherexp detail end

    //*******save FAREexp detail
    var fareexparr = new Array();
    $('#fareexp tbody tr').each(function (i, tr) {

        fareexparr[i] = {
            fromstation: ($(tr).find("td:nth-child(2) option:selected")).val(),
            depdate: ($(tr).find("td:nth-child(3) input[type=text]")).val(),
            tostation: ($(tr).find("td:nth-child(4) option:selected")).val(),
            arrivaldate: ($(tr).find("td:nth-child(5) input[type=text]")).val(),
            trvlmode: ($(tr).find("td:nth-child(6) option:selected")).val(),
            ticketno: ($(tr).find("td:nth-child(7) input[type=text]")).val(),
            fare: ($(tr).find("td:nth-child(8) input[type=text]")).val(),
            remark: ($(tr).find("td:nth-child(9) input[type=text]")).val(),
        }

    });
    $('#XmlFareDetails').val(JSON.stringify(fareexparr));
    //*******save FAREexp detail end

    //*******Get Total all type expense
    //var finalarr = new Array();
    //$('#finalexp tbody tr').each(function (i, tr) {

    //    finalarr[i] = {
    //        totaltravelexp: ($(tr).find("td:nth-child(2) label")).text(),
    //        totaltiffenxp: ($(tr).find("td:nth-child(3) label")).text(),
    //        totallodgingexp: ($(tr).find("td:nth-child(4) label")).text(),
    //        totalotherexp: ($(tr).find("td:nth-child(5) label")).text(),
    //        totalexp: ($(tr).find("td:nth-child(6) label")).text(),

    //    }

    //});
    //$('#XmlfinalDetails').val(JSON.stringify(finalarr));

    //var fareval = $("#AllowtravelExpNormedVal_FareVal").val() == "" ? 0.00 : parseFloat($("#AllowtravelExpNormedVal_FareVal").val());
    //var lodging = $("#AllowtravelExpNormedVal_Lodging").val() == "" ? 0.00 : parseFloat($("#AllowtravelExpNormedVal_Lodging").val());
    //var convence = $("#AllowtravelExpNormedVal_Convence").val() == "" ? 0.00 : parseFloat($("#AllowtravelExpNormedVal_Convence").val());
    //var tiffin = $("#AllowtravelExpNormedVal_Tiffin").val() == "" ? 0.00 : parseFloat($("#AllowtravelExpNormedVal_Tiffin").val());
    //var other = $("#AllowtravelExpNormedVal_Other").val() == "" ? 0.00 : parseFloat($("#AllowtravelExpNormedVal_Other").val());

    //alert(fareval);
    //alert(lodging);
    //alert(convence);
    //alert(tiffin);
    //alert(other)

    //$("#NormModel_FareVal").val(fareval);
    //$("#NormModel_Lodging").val(lodging);
    //$("#NormModel_Convence").val(convence);
    //$("#NormModel_Tiffin").val(tiffin);
    //$("#NormModel_Other").val(other);
    //*******save otherexp detail end
    $("#travelExpHDRMaster_total_Amount").val($("#total_totalexp").text());

    if (k == cnt) {
        $("#travelExpHDRMaster_status").val($(input).attr("data-val"));
        $("#travelExpHDRMaster_mode").val($(input).attr("data-mode"));
        $("#btnsaveupdate").click();
    }
    else {
        alert("Please fill travel expense completely");
        $("#steps-uid-0-t-0").click();
    }
}
///***********END *****************

