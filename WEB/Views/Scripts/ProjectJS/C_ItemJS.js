
var config = {
    '.itemchosen-select': { default_multiple_text: 'ALLddd' },
    '.itemchosen-select-deselect': { allow_single_deselect: true },
    '.itemchosen-select-no-single': { disable_search_threshold: 10 },
    '.itemchosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
    '.itemchosen-select-width': { width: "100%" }
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}

$('.itemchosen-select').on("chosen:ready", function () {
    debugger;
    OnChangeItemType();
});


$(document).ready(function () {
    OnChangeItemType();

    $('.itemchosen-select').on('change', function () {
        var id = "";
        if (id == "") {
            id = $(this).val();
        }
        else {
            id = id + "," + $(this).val();
        }

        $('#hdnSelectedItemGroup').val(id);
    });

});


function OnChangeItemType() {

    var selectedareaid = $('#C_SelectedItemType').val();
    $('.dvitem_choosen').hide();
    $('#dv_lbItem_' + selectedareaid).show();

}

//zone management
function GetAllItemById(url, rowid) {
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { rownumber: rowid },
        success: GetAllItemByIdSuccess,
        error: GetAllItemByIdFail
    });

}


function GetAllItemByIdSuccess(result) {
    debugger;
    FillAllItem(result.OBJLIST, result.ROWNUMBER);
}
function GetAllItemByIdFail(result) {
    console.log(result);
}

function FillAllItem(itemlist, rownumber) {

    var v = "";
    $.each(itemlist, function (i, obj) {

        v += "<option value=" + obj.AREAID + ">" + obj.AREANAME + "</option>";

    });

    $("#lbItem_" + rownumber).html(v);
    $('#lbItem_' + rownumber).trigger("chosen:updated");
}

