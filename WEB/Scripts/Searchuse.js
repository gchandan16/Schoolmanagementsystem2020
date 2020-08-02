/*----------------------Start Class and Section dependancy----------*/
function Getsectionbaseonclass(obj) {
    BlockUI();
    var classId = obj.value == "" ? 0 : obj.value;
    var checkUrl = UrlBase + '/Admin/GetsectionbaseonclassId';
   
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { classId: classId },
        success: GetsectionbaseonclassIdSuccess,
        error: GetsectionbaseonclassIdFail
    });

}

function GetsectionbaseonclassIdSuccess(result) {

    // $("#Adminssionno").val(result.frmno);
    FillSectionList(result.seclst);
    $.unblockUI();
    // $("#SecMid").val('').trigger("chosen:updated");

}

function GetsectionbaseonclassIdFail() {
    $.unblockUI();
}

function FillSectionList(SectionList) {
    debugger;
    var v = "<option value=\"0\">--Select--</option>";
    $.each(SectionList, function (i, sec) {

        v += "<option value=" + sec.Secmid + ">" + sec.SectionName + "</option>";
    });

    $("#SecMid").html(v).trigger("chosen:updated");
}
