function addVehicleValidation() {
    if (inputValidation()) {
        if ($.trim($("#TotalSeats").val()) != "" && parseInt($.trim($("#TotalSeats").val())) == 0) {
            alert("Please enter total seats available.");
            $("#TotalSeats").focus();
            return false;
        }
        else if ($.trim($("#AllowedSeats").val()) != "" && parseInt($.trim($("#AllowedSeats").val())) == 0) {
            alert("Please enter allowed seats available to sit.");
            $("#AllowedSeats").focus();
            return false;
        }
        else {
            BlockUI();
        }
    }
    else {
        return false;
    }
}


function setDetails(ui) {
    $("#Name").val(ui.item.label);
    $("#EmployeeID").val(ui.item.EMP_ID);
    if (ui.item.BIRTHDATE != null) {
        var month_names = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var parsedDate = new Date(parseInt(ui.item.BIRTHDATE.substr(6)));
        var date = new Date(parsedDate);
        var day = (date.getDate() <= 9 ? "0" + date.getDate() : date.getDate());
        var month = (date.getMonth() + 1 <= 9 ? "0" + (date.getMonth() + 1) : (date.getMonth() + 1));
        var dateString = day + "/" + month_names[date.getMonth()] + "/" + date.getFullYear();

        $("#DOB").val(dateString);
    }
    $("#PermanentAddress").val(ui.item.PermanentAddress);
    $("#PresentAddress").val(ui.item.PresentAddress);
    $("#Phone").val(ui.item.MOBILENUMBER);
}

function GetVehicleDetails(d) {
    $("#LblDriveName").html('')
    $("#LblVehicleSpec").html('')
    $("#LblTotalSeats").html('')
    if (d.value > 0) {
        $.ajax({
            url: UrlBase + '/Transport/GetVehicleDetails/',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            cache: false,
            data: '{"VehicleID":"' + d.value + '"}',
            headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                $.unblockUI();
                if (response != null) {
                    $("#LblDriveName").html($.trim(response.Name))
                    if (response.VehicleDetail != null) {
                        $("#LblVehicleSpec").html($.trim(response.VehicleDetail.VehicleSpecification))
                        $("#LblTotalSeats").html($.trim(response.VehicleDetail.TotalSeats))
                    }
                }
            },
            failure: function (response) {
                alert('Unable to perform event. Please try again.');
                $.unblockUI();
            },
        })
    }
}

var TotalPoint = parseInt($.trim($("#TotalPoint").val()));
function AddPickDropPoint() {
    var str = "";
    str += '<div class="row" id= "cnt' + TotalPoint + '">';
    str += '<div class="col-lg-4">';
    str += '<input autocomplete="off" class="form-control mandate" id="PickDropPointLst_' + TotalPoint + '__PickDropPointName" maxlength="100" msg="Please enter pickup point." name="PickDropPointLst[' + TotalPoint + '].PickDropPointName" onkeypress="return isKeyValid(event,\'32,65-90,97-122\');" pat="^[A-Za-z ]*$" placeholder="Pickup Point" type="text" value="">';
    str += '</div>';
    str += '<div class="col-lg-2">';
    str += '<label for="pwd" class="redlabel"><input autocomplete="off" class="form-control mandate txtTimePicker" maxlength="10" msg="Please enter pickup time." id="PickDropPointLst_' + TotalPoint + '__PickupTime" name="PickDropPointLst[' + TotalPoint + '].PickupTime" type="text" value=""></label>';
    str += '</div>';
    str += '<div class="col-lg-2">';
    str += '<label for="pwd" class="redlabel"><input autocomplete="off" class="form-control mandate txtTimePicker" maxlength="10" msg="Please enter drop time." id="PickDropPointLst_' + TotalPoint + '__DropTime" name="PickDropPointLst[' + TotalPoint + '].DropTime" type="text" value=""></label>';
    str += '</div>';
    str += '<div class="col-lg-2">';
    str += '<input autocomplete="off" class="form-control mandate" id="PickDropPointLst_' + TotalPoint + '__Fee" name="PickDropPointLst[' + TotalPoint + '].Fee" onkeypress="return isKeyValid(event,\'32,48-57\');" pat="^[0-9]*$" type="text" value="">';
    str += '</div>';
    str += '<div class="col-lg-2">';
    str += '<label for="pwd" class="redlabel"><span onclick="return AddPickDropPoint();" style="cursor:pointer;">+</span></label>';
    if (parseInt(TotalPoint) > 0) {
        str += '<label for="pwd" class="redlabel"><span onclick="return RemovePickDropPoint(\'cnt' + TotalPoint + '\');" style="cursor:pointer;">&nbsp;&nbsp;-</span></label>';
    }
    str += '</div>';
    str += '<div class="col-lg-12"><br /></div>';
    str += '</div>';
    TotalPoint = TotalPoint + 1;
    $("#divPickDropDetails").append(str);
}

function RemovePickDropPoint(divId) {
    $("#" + divId).remove();
}

var TransportDetails = null;
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
            url: UrlBase + '/Transport/GetSectionList/',
            data: '{"ClassID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                if (data.length > 0) {
                    for (var l = 0; l < data.length; l++) {
                        $("#ddlSectionList").append('<option value="' + data[l].Secmid + '">' + data[l].SectionName + '</option>');
                    }

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
            url: UrlBase + '/Transport/GetStudentList/',
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
            url: UrlBase + '/Transport/GetStudentList/',
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
            url: UrlBase + '/Transport/GetStudentList/',
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
    var html = '';
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
            html += '<td><input type="button" value="Apply" class="btn btn-primary" onclick="return ApplyTransport(\'' + record[l].Smid + '\',\'' + record[l].TPCostID + '\');" /></td>';
            html += '</tr>';
        }
        html += '<tbody>';
        $("#StudentTbl").html(html)
    }
    else {
        alert("No Student Registered.");
    }
    $.unblockUI();
}
function ApplyTransport(studentid, tpcostid) {
    TransportDetails = null;
    flag = 0;
    $("#hdnTPCodeID").val(tpcostid);
    $("#hdnStudentID").val(studentid);
    $("#ddlRoute").empty().append('<option value="0">--Select--</option>');
    $("#ddlPickDropPoint").empty().append('<option value="0">--Select--</option>');
    $("#LblPickUpTime").html('');
    $("#LblDropTime").html('');
    $(".MonthFee").val('');
    $("#divFeeDetails").hide();
    BlockUI();
    if (tpcostid != 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Transport/GetAssignTransport/',
            data: '{"TPCostID":"' + tpcostid + '"}',
            dataType: "json",
            async: true,
            success: function (TPData) {
                TransportDetails = TPData;
            },
            error: function (data) {
            }
        });
    }
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Transport/GetRouteList/',
        dataType: "json",
        success: function (data) {
            BindRouteList(data);
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });

}
function BindRouteList(RouteData) {
    $("#ddlRoute").empty().append('<option value="0">--Select--</option>');
    $("#ddlPickDropPoint").empty().append('<option value="0">--Select--</option>');
    $("#LblPickUpTime").html('');
    $("#LblDropTime").html('');
    $(".MonthFee").val('');
    $("#divFeeDetails").hide();
    if (RouteData != null && RouteData.length > 0) {
        for (var i = 0; i < RouteData.length; i++) {
            $("#ddlRoute").append('<option value="' + RouteData[i].RouteID + '">' + RouteData[i].RouteName + '</option>');
        }
        //$("#popupDiv").show();
        $('#popupDiv').modal('show');
        if ($("#hdnTPCodeID").val() != 0) {
            $('#ddlRoute').val(TransportDetails.RouteID).trigger('change');
        }
    }
    else {
        alert("No Route details found.");
    }
}
function getPickDropPointList(d) {
    $("#ddlPickDropPoint").empty().append('<option value="0">--Select--</option>');
    $("#LblPickUpTime").html('');
    $("#LblDropTime").html('');
    $(".MonthFee").val('');
    $("#divFeeDetails").hide();
    if (d.value != 0) {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Transport/GetPickDropRouteList/',
            data: '{"RouteID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                BindPickDropPointList(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function BindPickDropPointList(PickDropData) {
    $("#LblPickUpTime").html('');
    $("#LblDropTime").html('');
    $(".MonthFee").val('');
    $("#divFeeDetails").hide();
    if (PickDropData != null && PickDropData.length > 0) {
        for (var i = 0; i < PickDropData.length; i++) {
            $("#ddlPickDropPoint").append('<option value="' + PickDropData[i].PickDropID + "_" + PickDropData[i].PickupTime + "_" + PickDropData[i].DropTime + "_" + PickDropData[i].Fee + '">' + PickDropData[i].PickDropPointName + '</option>');
        }
        //$("#popupDiv").show();
        $('#popupDiv').modal('show');
        if ($("#hdnTPCodeID").val() != 0) {
            var PKID = 0;
            $('#ddlPickDropPoint > option').each(function () {
                if ($(this).val().indexOf(TransportDetails.PickDropPointID + "_") == 0 && $(this).val().indexOf("_" + TransportDetails.Fee) > 0) {
                    PKID = $(this).val();
                }
            });
            $('#ddlPickDropPoint').val(PKID).trigger('change');
        }
    }
    else {
        alert("No Pick and drop point found.");
    }
}
function DisplayPickDropPointDetails(PickDropPointDetail) {
    var details = PickDropPointDetail.value;
    $("#LblPickUpTime").html('');
    $("#LblDropTime").html('');
    $(".MonthFee").val('');
    $("#divFeeDetails").hide();

    if (details != 0 && details.split('_').length == 4) {
        var PickupTime = details.split('_')[1];
        var DropTime = details.split('_')[2];
        var TPCost = details.split('_')[3];
        var TPCost = details.split('_')[3];
        $("#LblPickUpTime").html(PickupTime);
        $("#LblDropTime").html(DropTime);
        if ($("#hdnTPCodeID").val() != 0 && flag == 0) {
            $("#hdnFee").val(TransportDetails.Fee);
            $("#txtApr").val(TransportDetails.Apr);
            $("#txtMay").val(TransportDetails.May);
            $("#txtJun").val(TransportDetails.Jun);
            $("#txtJul").val(TransportDetails.Jul);
            $("#txtAug").val(TransportDetails.Aug);
            $("#txtSep").val(TransportDetails.Sep);
            $("#txtOct").val(TransportDetails.Oct);
            $("#txtNov").val(TransportDetails.Nov);
            $("#txtDec").val(TransportDetails.Dec);
            $("#txtJan").val(TransportDetails.Jan);
            $("#txtFeb").val(TransportDetails.Feb);
            $("#txtMar").val(TransportDetails.Mar);
            flag = 1;
        }
        else {
            $(".MonthFee").val(TPCost);
        }
        $("#divFeeDetails").show();
    }
}
function AssignTransport() {

    var transportDetail = {
        "TPCostID": $("#hdnTPCodeID").val() != 0 ? $("#hdnTPCodeID").val() : 0,
        "StudentID": $("#hdnStudentID").val(),
        "RouteID": $("#ddlRoute").val(),
        "PickDropPointID": $("#ddlPickDropPoint").val().split('_')[0],
        "Fee": $("#hdnFee").val() == "" ? "0" : $("#hdnFee").val(),
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
        url: UrlBase + '/Transport/AssignTransport/',
        data: JSON.stringify(transportDetail),
        headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
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


function SearchAllocationRepost() {
    BlockUI();
    var SearchBy = $("#ddlSearchBy").val();
    var ClassID = $("#ddlClassList").val();
    var SectionID = $("#ddlSectionList").val();
    var AdmissionNo = $("#txtAdmissionNumber").val();
    var RouteID = $("#ddlRouteList").val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: UrlBase + '/Transport/SearchAllocationRepost/',
        data: '{"SearchBy":"' + SearchBy + '","ClassID":"' + ClassID + '","SectionID":"' + SectionID + '","AdmissionNo":"' + AdmissionNo + '","RouteID":"' + RouteID + '"}',
        headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
        dataType: "json",
        success: function (data) {
            DisplayStudentListForAllocationReport(data);
            $.unblockUI();
        },
        error: function (data) {
            $.unblockUI();
        }
    });
}
function DisplayStudentListForAllocationReport(record) {
    BlockUI();
    $(".divBox").empty();
    var html = '<div class="box">';
    html = '<div class="card card_ps">';
    html = '<div class="card card-default">';
    html = '<div class="box-body">';
    html = '<table id="StudentTbl" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
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
    html += '<th>RouteName(Ponit)</th>';
    html += '<th>Vehicle No</th>';
    html += '<th>Fee</th>';
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
        html += '<td>' + record[l].RouteName + "(" + record[l].PickDropPointName + ")" + '</td>';
        html += '<td>' + record[l].VehicleNumber + '</td>';
        html += '<td>' + record[l].TPFee + '</td>';
        html += '</tr>';
    }
    html += '</tbody>';
    html += '</table>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    html += '</div>';
    $(".divBox").html(html)

    $.unblockUI();
    $('#StudentTbl').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                title: function () {
                    return "Student List";
                },
                orientation: 'landscape',
                pageSize: 'A0',
                text: '<i class="fa fa-file-pdf-o"> PDF</i>',
                titleAttr: 'PDF'
            },
            {
                extend: 'excel',
                title: function () {
                    return "Student List";
                },
                text: '<i class="fa fa-file-pdf-o"> Excel</i>',

            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':visible'
                },
                title: function () {
                    return "Student List";
                },
                text: '<i class="fa fa-file-pdf-o"> Print</i>',
                className: 'btn btn-default',
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (win) {
                    $(win.document.body)
                        .css('font-size', '10pt')
                        .prepend(
                            '<img src="http://datatables.net/media/images/logo-fade.png" style="position:absolute; top:0; left:0;" />'
                        );

                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ]
    });
}