var tparty = $('#dealerTbl').DataTable();

var len = 0;
function AddPartyData(lennum, url) {
    debugger;
    if (len == 0) {
        len = parseInt(lennum);
    }
    var selectedareaid = $('#C_SelectedAreaType').val();
    var selectedUserId = $('#selectedUserId').val();
    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        data: { UserAreaTypeId: selectedareaid, DataFor: "PARTY_GROUP", AreaId: $('#hdnSelectedUserArea').val(), UserId: selectedUserId },
        async: false,
        success: AddPartyDataSucceeded,
        error: AddPartyDataFailed
    });
}

function AddPartyDataSucceeded(result) {
    debugger;
    ShowWaitingSymbol();
    if (result.length == 0) {
        alert("Data not found");
    }
    var dealerids = [];
    tparty.$('input[type="hidden"]').each(function () {
        dealerids.push(parseInt(this.value));
    });
    $.each(result, function (i, obj) {
        if (jQuery.inArray(obj.DEALER_ID, dealerids) == -1) {

            var cureenid = parseInt($('#partyloopcount').val()) + 1;
            tparty.row.add([
                '<input type="checkbox" id="Partycb_' + cureenid + '" name="Partycb[]" >',
                obj.DEALERCODE + '<input type="hidden" id="DEALER_ID' + cureenid + '" value="' + obj.DEALER_ID + '" >',
                obj.DEALERNAME,
                obj.CITYNAME,
                obj.STATENAME,
                obj.ZONENAME
            ]).node().id = 'PartyRow_' + cureenid;

            $('#partyloopcount').val(cureenid);
        }

    });
    tparty.draw();
    HideWaitingSymbol();
}

function AddPartyDataFailed(result) {

}

// Handle click on "Select all" control
$('#cballParty').on('click', function () {

    // Check/uncheck all checkboxes in the table
    var rows = tparty.rows({ 'search': 'applied' }).nodes();
    $('input[type="checkbox"]', rows).prop('checked', this.checked);
    $('input[type="checkbox"]', rows).parent().parent().addClass('row_selected');
});
// Handle click on checkbox to set state of "Select all" control
$('#dealerTbl tbody').on('change', 'input[type="checkbox"]', function () {
    debugger;
    var rowno = this.id.split('_')[1];
    // If checkbox is not checked
    if (!this.checked) {
        var el = $('#cballParty').get(0);
        // If "Select all" control is checked and has 'indeterminate' property
        if (el && el.checked && ('indeterminate' in el)) {
            // Set visual state of "Select all" control
            // as 'indeterminate'
            el.indeterminate = true;
        }
        $('#PartyRow_' + rowno).removeClass('row_selected');
    }
    else {
        $('#PartyRow_' + rowno).addClass('row_selected');
    }
});

$('#removeallParty').click(function () {
    debugger;
    tparty.rows('.row_selected').remove().draw();
    $('#cballParty').prop("checked", false);
});

function CheckPartyValidation() {
    debugger;
    var valid = false;
    var totalrows = tparty.rows().count();
    var dealerids = "";
    if (totalrows > 0) {
        //get column 1 because we are hahnig hidden value
        tparty.$('input[type="hidden"]').each(function () {
            dealerids = dealerids == "" ? this.value : dealerids + "," + this.value;

        });
        $('#DEALERIDARR').val(dealerids);
        valid = true;

    }
    else {
        alert("Please select at least one dealer");
    }

    return valid;
}



function IsPartyGrpExistValid(url) {
    debugger;
    var checkUrl = url;
    var PartyGrpName = $('#PartyGrpName').val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: checkUrl,
        data: JSON.stringify({ PartyGrpName: PartyGrpName }),
        dataType: "json",
        success: function (data) {
            if (!data) {
                alert("Party Group Name already exists");
                $('#PartyGrpName').val('');
            }
        },
        error: function (data) {

        }
    });

}