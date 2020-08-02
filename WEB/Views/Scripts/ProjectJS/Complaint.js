function GetProspectData(ProspectTypeId, CityId, checkUrl) {
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { ProspectTypeId: ProspectTypeId, CityId: CityId },//, str: str
        success: function (result) {
            debugger;
            ShowWaitingSymbol();
            if (ProspectTypeId == 1) {
                $("#DEALER_ID").empty();
                var v = "<option value=''>--Select--</option>";
                $('#DealerList').show();
                $('#RetailerList').hide();
                $('#DEALER_ID').chosen();
                $.each(result, function (i, data) {
                    v += "<option value=" + data.DEALER_ID + ">" + data.DEALERFULLNAME + "</option>";
                });
                $("#DEALER_ID").append(v);
                $("#DEALER_ID").trigger("chosen:updated");
            }
            else {
                $("#PROSPECT_ID").empty();
                var v = "<option value=''>--Select--</option>";
                $('#DealerList').hide();
                $('#RetailerList').show();

                if (result != null && result != undefined && result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        v += '<option value="' + result[i].PROSPECT_ID + '">' + result[i].PROSPECTFULLNAME + '</option>';

                    }
                    $('#PROSPECT_ID').append(v);
                    $('#PROSPECT_ID').trigger("chosen:updated");
                }
            }
            HideWaitingSymbol();
            
        },
        error: function (result) {
            console.log(result);
        }
    });
}

function FillComplaintReason(ComplaintTypeId, checkUrl) {
    debugger;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { ComplaintTypeId: ComplaintTypeId },//, str: str
        success: function (result) {
            ShowWaitingSymbol();
            $("select#CASETYPEID").empty();
            var v = "<option value=''>--Select--</option>";
            $.each(result, function (i, data) {
                v += "<option value=" + data.CASETYPEID + ">" + data.CaseTypeWithApprovalDays + "</option>";
            });
            $("select#CASETYPEID").append(v);
            $("select#CASETYPEID").trigger("chosen:updated");
            HideWaitingSymbol();
        },
        error: function (result) {
            console.log(result);
        }
    });
}

function FillpartByInvoiceId(InvoiceId, DealerId, CTYPEID, checkUrl, obj) {
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { InvoiceId: InvoiceId, DealerId: DealerId, CTYPEID: CTYPEID },//, str: str
        success: function (result) {
            ShowWaitingSymbol();
            $(obj).empty().trigger("chosen:updated");
            $("#HiddenPartId").empty();
            var v = "<option value=''>--Select--</option>";
            var hdn = "<option value=''>--Select--</option>";
            $.each(result, function (i, data) {
                v += "<option value=" + data.PART_ID + ">" + data.PARTFULLNAME + "</option>";
                hdn += '<option value="' + data.PART_ID + '">' + data.PARTFULLNAME + '</option>';
            });

            $(obj).append(v);
            $('#HiddenPartId').append(hdn);
            HideWaitingSymbol();

            //$("#PART_ID").append(v);
            $(obj).trigger("chosen:updated");
        },
        error: function (result) {
            console.log(result);
        }
    });
}

function GetFollowUpWithWEmpDetail(empid, followupid, checkUrl, id) {
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { EmployeeId: empid, FollowUpId: followupid },
        async: false,
        success: function (result) {
            $('#EmployeePhone_' + id[1]).val(result.EmpDetail.MOBILENUMBER);
            if (result.ManagerDetail != null) {
                //$('#FollowUpName_' + id[1]).val(result.ManagerDetail.FIRSTNAME + result.ManagerDetail.MIDDLENAME + result.ManagerDetail.LASTNAME);
                $('#FollowUpPhone_' + id[1]).val(result.ManagerDetail.MOBILENUMBER);
            }
        },
        error: function () {

        }
    });
}

function FillEmployeeDetail(DeptId, checkUrl, id) {
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { DepartmentId: DeptId },
        async: false,
        success: function (result) {
            $("#EmpId_" + id[1]).empty();
            //$("#FollowUpName_" + id[1]).empty();
            var v = "<option value=0>--Select--</option>";
            $.each(result, function (i, empdetail) {
                if (empdetail.MIDDLENAME == null) {
                    empdetail.MIDDLENAME = " ";
                }
                v += "<option value=" + empdetail.EMP_ID + ">" + empdetail.DISPLAYEMPNAME + "</option>";
                //v += "<option value=" + empdetail.EMP_ID + ">" + empdetail.FIRSTNAME + empdetail.MIDDLENAME + empdetail.LASTNAME + "</option>";

            });
            $("#EmpId_" + id[1]).append(v);
            $("#FollowUpName_" + id[1]).append(v);
            $("#EmpId_" + id[1]).trigger("chosen:updated");
            //$("#FollowUpName_" + id[1]).trigger("chosen:updated");
        },
        error: function () {

        }
    });
}

//function FillDealerList(CityId, checkUrl) {
//    $.ajax({
//        url: checkUrl,
//        type: "GET",
//        dataType: "JSON",
//        data: { CityId: CityId },
//        async: false,
//        success: function (result) {
//            $.each(result, function (i, data) {
//                $("#DEALER_ID").append("<option value=" + data.DEALER_ID + ">" + data.DEALERFULLNAME + "</option>");
//            });

//            //$("#PART_ID").append(v);
//            $("#DEALER_ID").trigger("chosen:updated");
//        },
//        error: function () {

//        }
//    });
//}

function GetAllComplaintType(complaintUrl) {
    var PType = $('#PROSPECTTYPE_ID').val();
    $.ajax({
        url: complaintUrl,
        type: "GET",
        data: { ProspectTypeId: PType },
        dataType: "JSON",
        async: false,
        success: function (result) {
            $("#CTYPEID").empty();
            var v = "<option value=0>--Select--</option>";
            $.each(result, function (i, data) {
              v +=  "<option value=" + data.CTYPEID + ">" + data.CTYPENAME + "</option>";
            });

            $("#CTYPEID").append(v);
        },
        error: function () {

        }
    });
}



function RemoveRowFromTable() {
    debugger;
    valid = true;

    var mtable = document.getElementById("complainttbl");
    var mrowCount = document.getElementById("ComplaintDTLCount").value;
    var currentpartvalue = "";

    for (var i = 2; i <= mrowCount; i++) {

        $('#Complaint_' + i).remove();

    }
}

function ValidateFileType(obj) {
    var valid = true;
    var file = $(obj).val();
    var filenameAttr = $(obj).attr('name');
    var fileSize = 0;
    //var file1 = $(obj)[0].files[0];
    var fileType = "";
    var validImageTypes = ["image/gif", "image/jpeg", "image/png"];

    $($(obj)[0].files).each(function () {
        fileType = $(this)[0].type;
        if ($.inArray(fileType, validImageTypes) < 0) {
            alert("Please upload image type file only");
            $(obj).val("");
            valid = false;
            return valid;
        }
    });
    if (valid) {
        if (parseInt($(obj).get(0).files.length) > 2) {
            alert("You can only upload a maximum of 2 files");
            $(obj).val("");
            valid = false;
            return valid;
        }
        else {
            $($(obj)[0].files).each(function () {
                fileSize += $(this)[0].size;
            });
            if (fileSize / 1024 / 1024 > 2) {
                alert("Please upload file upto 2 mb only");
                $(obj).val("");
                valid = false;
                return valid;
            }
            else {
                return valid;
            }
            
        }
    }
    return valid;
}

function checkAllchekBoxs(obj) {
    var status = obj.checked;
    debugger;
    $('input[type="checkbox"]').each(function () {
        this.checked = status;
    });
}

function removeCheckedrow() {
    if ($('#complainttbl tbody tr').length > 1) {
        $('#complainttbl tbody').find('tr').each(function () {
            if ($(this).find('td:first').find('input[type="checkbox"]').is(':checked')) {
                $(this).remove();
            }
        });
        $('#ComplaintDTLCount').prop('value', $('#complainttbl tbody tr').length);
    }
    else {
        alert("Atleast one row is required");
    }
}



function CheckDuplicatePart(obj) {
    var currentpartvalue = $(obj).val();
    $('#complainttbl').find('tbody tr').each(function (i, tr) {
        if ($(this).find('#PARTID_' + (i + 1)).val() == currentpartvalue && $(this).find('#PARTID_' + (i + 1)).attr('id') != $(obj).attr('id')) {
            alert('Part already selected');
            $(obj).val('').trigger("chosen:updated");
            $(obj).focus();
        }
    });
}