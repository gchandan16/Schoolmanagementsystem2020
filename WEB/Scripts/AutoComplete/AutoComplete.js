var SHandeler;


$(document).ready(function () {
    SHandeler = new SearchHelper();
    SHandeler.BindEvents();
});


var SearchHelper = function () {
    this.studentDetails = $(".StudentNameAdmissionNo");
    this.studentDetailsIndiscipline = $(".StudentNameIndiscipline");
    this.studentDetailsExtraFee = $(".StudentNameExtraFee");
    this.StudentNameAdmissionNoRefund = $(".StudentNameAdmissionNoRefund");
}

SearchHelper.prototype.BindEvents = function () {
    var h = this;
    if (h.studentDetails.length != 0) {
        setStudentDetails();
        var URL = UrlBase + "/AutoComplete/GetStudentList"
        h.studentDetails.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'src': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            var result = item.Firstname + " (" + item.Adminssionno + ")";
                            return { label: result, value: result, id: item.Smid, Photo: item.studentimage, FirstName: item.Firstname, FatherName: item.Bd_fathername, AdmissionNo: item.Adminssionno, ClassName: item.ClassName, SectionName: item.SectionName, FeeGroup: item.FeeGroup, TPCost: item.TransportCost }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                })
            },
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                setStudentDetails(ui);
            }
        });
    }
    var h = this;
    if (h.studentDetailsIndiscipline.length != 0) {
        setStudentDetailsIndiscipline();
        var URL = UrlBase + "/AutoComplete/GetStudentList"
        h.studentDetailsIndiscipline.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'src': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            var result = item.Firstname + " (" + item.Adminssionno + ")";
                            return { label: result, value: result, id: item.Smid, Photo: item.studentimage, FirstName: item.Firstname, FatherName: item.Bd_fathername, AdmissionNo: item.Adminssionno, ClassName: item.ClassName, SectionName: item.SectionName, FeeGroup: item.FeeGroup, TPCost: item.TransportCost }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                })
            },
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                setStudentDetailsIndiscipline(ui);
            }
        });
    }
    var h = this;
    if (h.studentDetailsExtraFee.length != 0) {
        setStudentDetailsExtraFee();
        var URL = UrlBase + "/AutoComplete/GetStudentList"
        h.studentDetailsExtraFee.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'src': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            var result = item.Firstname + " (" + item.Adminssionno + ")";
                            return { label: result, value: result, id: item.Smid, Photo: item.studentimage, FirstName: item.Firstname, FatherName: item.Bd_fathername, AdmissionNo: item.Adminssionno, ClassName: item.ClassName, SectionName: item.SectionName, FeeGroup: item.FeeGroup, TPCost: item.TransportCost }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                })
            },
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                setStudentDetailsExtraFee(ui);
            }
        });
    }
    if (h.StudentNameAdmissionNoRefund.length != 0) {
        setStudentDetailsRefund();
        var URL = UrlBase + "/AutoComplete/GetStudentList"
        h.StudentNameAdmissionNoRefund.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'src': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            var result = item.Firstname + " (" + item.Adminssionno + ")";
                            return { label: result, value: result, id: item.Smid, Photo: item.studentimage, FirstName: item.Firstname, FatherName: item.Bd_fathername, AdmissionNo: item.Adminssionno, ClassName: item.ClassName, SectionName: item.SectionName, FeeGroup: item.FeeGroup, TPCost: item.TransportCost }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                })
            },
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                setStudentDetailsRefund(ui);
            }
        });
    }
}
function DynamicAuto() {
    var URL = UrlBase + "/AutoComplete/GetEmployeeList"
    if ($("#IsEmployee").val() == "true") {
        $(".EmployeeNameAutoDyn").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: URL,
                    data: "{ 'src': '" + request.term + "', maxResults: 10 }",
                    dataType: "json", type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data, function (item) {
                            var result = $.trim(item.FIRSTNAME) + " " + $.trim(item.LASTNAME) + " (" + item.EMPCODE + ")";
                            return { label: result, value: result, EMP_ID: item.EMP_ID, PermanentAddress: item.PermanentAddress, PresentAddress: item.PresentAddress, MOBILENUMBER: item.MOBILENUMBER, BIRTHDATE: item.BIRTHDATE, LASTNAME: item.LASTNAME, FIRSTNAME: item.FIRSTNAME, EMPCODE: item.EMPCODE }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                    }
                })
            },
            autoFocus: true,
            minLength: 1,
            select: function (event, ui) {
                setDetails(ui);
            }
        });
    }
    else {
        $(".EmployeeNameAutoDyn").autocomplete("destroy");
    }

}
