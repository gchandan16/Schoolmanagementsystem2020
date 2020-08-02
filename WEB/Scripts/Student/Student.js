function GenerateIDCard() {
    var ClassID = $("#ddlClassList").val();
    var SectionID = $("#ddlSectionList").val();
    var CardType = $("#ddlCardType").val();
    if (CardType == "0") {
        alert("Please select card type");
        return false;
    }
    else {
        BlockUI();
        $.ajax({
            url: UrlBase + '/Student/GenerateIDCard/',
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            cache: false,
            data: '{"ClassID":"' + ClassID + '","SectionID":"' + SectionID + '","CardType":"' + CardType + '"}',
            headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
            success: function (response) {
                $.unblockUI();
                var html = '<div class="box box-body">';
                html += '<div class="panel-heading">';
                html += '<input type="button" onclick="printIDCard(\'printDiv\')" value="Print" class="btn btn-success">';
                html += '</div>';
                html += '<div>' + response + '</div></div>';
                $(".divBox").html(html);
            },
            failure: function (response) {
                alert('Unable to perform event. Please try again.');
                $.unblockUI();
            },
        });
    }
}

function printIDCard(divID) {
    var contents = $("#" + divID).html();
    var frame1 = $('<iframe />');
    frame1[0].name = "frame1";
    frame1.css({ "position": "absolute", "top": "-1000000px" });
    $("body").append(frame1);
    var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
    frameDoc.document.open();
    //Create a new HTML document.
    frameDoc.document.write('<html><head><title>DIV Contents</title>');
    frameDoc.document.write('</head><body>');
    //Append the external CSS file.
    frameDoc.document.write('<link rel="stylesheet" href="http://www.igenussolution.com/Content/CSS/IDCard.css" type="text/css" />');
    //Append the DIV contents.
    frameDoc.document.write(contents);
    frameDoc.document.write('</body></html>');
    frameDoc.document.close();
    setTimeout(function () {
        window.frames["frame1"].focus();
        window.frames["frame1"].print();
        frame1.remove();
    }, 500);
}


function GetSectionLists(d) {

    $("#ddlSectionList").empty().append('<option value="0">--Select--</option>');
    if (d.value != "0" && d.value != "") {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Student/GetSectionList/',
            data: '{"ClassID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                FillSectionLists(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function FillSectionLists(SectionList) {
    var v = "<option value=\"0\">--Select--</option>";
    $.each(SectionList, function (i, sec) {
        v += "<option value=" + sec.Secmid + ">" + sec.SectionName + "</option>";
    });
    $("#SectionID").html(v);
        //.trigger("chosen:updated");
}

function ListSectionStudents() {
    $("#StudentTbl").empty();
    if ($("#SectionID").val() != 0) {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Transport/GetStudentList/',
            data: '{"ClassID":"' + $("#ClassID").val() + '","SectionID":"' + $("#SectionID").val() + '","ApplicationNo":""}',
            dataType: "json",
            success: function (data) {
                DisplayStudentLists(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}

function DisplayStudentLists(record) {
    BlockUI();
    $("#StudentTbl").empty();
    $(".details").hide();
    var html = '<table id="roletbl" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        html += '<th>SNo</th>';
        html += '<th>Student Name</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            html += '<td>' + parseInt(l + 1) + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].Adminssionno + '</td>';
            html += '<td><input name="StudentList[' + l + '].Smid" id="StudentList_' + l + '__Smid" type="hidden" value="' + record[l].Smid + '"><input name="StudentList[' + l + '].RollNo" id="StudentList_' + l + '__Check" type="text" value="' + record[l].RollNo + '" ></td>';
            html += '</tr>';
        }
        html += '</tbody>';
        $("#StudentTbl").html(html);
        $(".details").show();
    }
    else {
        alert("No Student Registered.");
    }
    $.unblockUI();
}

function GetPrevSectionList(d) {
    EmptyStudentList()
    $("#ddlPrevSectionList").empty().append('<option value="0">--Select--</option>');
    if (d.value != "0" && d.value != "") {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Student/GetSectionList/',
            data: '{"ClassID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                var v = "<option value=\"0\">--Select--</option>";
                $.each(data, function (i, sec) {
                    v += "<option value=" + sec.Secmid + ">" + sec.SectionName + "</option>";
                });
                $("#ddlPrevSectionList").html(v);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function GetNextSectionList(d) {
    $("#ddlNextSectionList").empty().append('<option value="0">--Select--</option>');
    if (d.value != "0" && d.value != "") {
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Student/GetSectionList/',
            data: '{"ClassID":"' + d.value + '"}',
            dataType: "json",
            success: function (data) {
                var v = "<option value=\"0\">--Select--</option>";
                $.each(data, function (i, sec) {
                    v += "<option value=" + sec.Secmid + ">" + sec.SectionName + "</option>";
                });
                $("#ddlNextSectionList").html(v);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function ListSectionStudentsToPromote() {
    if ($("#PrevFinancialYearID").val() == "" || $("#PrevFinancialYearID").val() == "0") {
        alert("Please select financial year.");
        $("#PrevFinancialYearID").focus();
        return false;
    }
    else if ($("#PrevClassID").val() == "" || $("#PrevClassID").val() == "0") {
        alert("Please select class.");
        $("#PrevClassID").focus();
        return false;
    }
    else if ($("#ddlPrevSectionList").val() == "" || $("#ddlPrevSectionList").val() == "0") {
        alert("Please select section.");
        $("#ddlPrevSectionList").focus();
        return false;
    }
    else {
        EmptyStudentList();
        BlockUI();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Student/GetStudentListToPromote/',
            data: '{"ClassID":"' + $("#PrevClassID").val() + '","SectionID":"' + $("#ddlPrevSectionList").val() + '","FinancialYearID":"' + $("#PrevFinancialYearID").val() + '"}',
            dataType: "json",
            success: function (data) {
                DisplayStudentListsToPromote(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }
}
function DisplayStudentListsToPromote(record) {
    BlockUI();
    EmptyStudentList();
    //$(".details").hide();
    var html = '<table id="collectiontbllist" class="table table-striped table-bordered dt-responsive nowrap" style="width: 100%">';
    if (record.length > 0) {
        html += '<thead class="thead-lights">';
        html += '<tr>';
        html += '<th>SNo</th>';
        html += '<th>Select</th>';
        html += '<th>Admission No</th>';
        html += '<th>Roll No</th>';
        html += '<th>Student Name</th>';
        html += '<th>Class</th>';
        html += '<th>Section</th>';
        html += '<th>Father Name</th>';
        html += '<th>Contact N.</th>';
        html += '<th>Address</th>';
        html += '</thead>';
        html += '<tbody>';
        for (var l = 0; l < record.length; l++) {
            html += '<tr>';
            html += '<td>' + parseInt(l + 1) + '</td>';
            if (record[l].StudentStatus == "N") {
                html += '<td><input type="checkbox" id="StudentStatus" name="StudentStatus" value="' + record[l].Adminssionno + '" /></td>';
            }
            else {
                html += '<td>&nbsp;</td>';
            }

            html += '<td>' + record[l].Adminssionno + '</td>';

            html += '<td>' + record[l].RollNo + '</td>';
            html += '<td>' + record[l].Firstname + '</td>';
            html += '<td>' + record[l].ClassName + '</td>';
            html += '<td>' + record[l].SectionName + '</td>';
            html += '<td>' + record[l].Bd_fathername + '</td>';
            html += '<td>' + record[l].Bd_contactno + '</td>';
            html += '<td>' + record[l].Bd_address1 + ' ' + record[l].Bd_address2 + '</td>';
            //html += '<td><input name="StudentList[' + l + '].Smid" id="StudentList_' + l + '__Smid" type="hidden" value="' + record[l].Smid + '"><input name="StudentList[' + l + '].RollNo" id="StudentList_' + l + '__Check" type="text" value="' + record[l].RollNo + '" ></td>';
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
                        return 'Promote Sheet';
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
        alert("No Student Registered.");
    }
    $.unblockUI();
}
function PromoteStudent() {
    if ($("#NextFinancialYearID").val() == "" || $("#NextFinancialYearID").val() == "0") {
        alert("Please select financial year.");
        $("#NextFinancialYearID").focus();
        return false;
    }
    else if ($("#NextClassID").val() == "" || $("#NextClassID").val() == "0") {
        alert("Please select class.");
        $("#NextClassID").focus();
        return false;
    }
    else if ($("#ddlNextSectionList").val() == "" || $("#ddlNextSectionList").val() == "0") {
        alert("Please select section.");
        $("#ddlNextSectionList").focus();
        return false;
    }
    else if ($("input[name='StudentStatus']:checked").length == 0) {
        alert("Please select student.");
        return false;
    }
    else if ($("#PrevFinancialYearID").val() == $("#NextFinancialYearID").val()) {
        alert("Please change financial year.");
        $("#NextFinancialYearID").focus();
        return false;
    }
    else {
        BlockUI();
        var allVals = new Array();
        $("input[name='StudentStatus']:checked").each(function () {
            allVals.push($(this).val());
        });
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: UrlBase + '/Student/PromoteStudent/',
            data: JSON.stringify({ 'AdmissionNo': allVals, 'PrevFYID': $("#PrevFinancialYearID").val(), 'NextFYID': $("#NextFinancialYearID").val(), 'ClassID': $("#NextClassID").val(), 'SectionID': $("#ddlNextSectionList").val() }),
            headers: { "__RequestVerificationToken": $(':input[name="__RequestVerificationToken"]').val() },
            dataType: "json",
            success: function (data) {
                alert(data);
                $.unblockUI();
            },
            error: function (data) {
                $.unblockUI();
            }
        });
    }

}
function EmptyStudentList() {
    $("#StudentTbl").empty().hide();
}