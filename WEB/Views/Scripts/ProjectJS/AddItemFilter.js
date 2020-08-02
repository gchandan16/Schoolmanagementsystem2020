
var config = {
    '.partychosen-select': { data_placeholder: 'ALL' },
    '.partychosen-select-deselect': { allow_single_deselect: true },
    '.partychosen-select-no-single': { disable_search_threshold: 10 },
    '.partychosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
    '.partychosen-select-width': { width: "100%" }
}
for (var selector in config) {
    $(selector).chosen(config[selector]);
}

$('.partychosen-select').on("chosen:ready", function () {
    debugger;
    OnChangeItemType();
});

function OnChangeItemType() {
    debugger;
    var selectedareaid = $('#C_SelectedItemType').val();
    $('.dvparty_choosen').hide();
    $('#dv_lbItem_' + selectedareaid).show();

}
$(document).ready(function () {
    debugger;
    OnChangeItemType();
});

$('.partychosen-select').on('change', function () {
    var id = "";
    if (id == "") {
        id = $(this).val();
    }
    else {
        id = id + "," + $(this).val();
    }

    $('#hdnSelectedItemGroup').val(id);
}); 
