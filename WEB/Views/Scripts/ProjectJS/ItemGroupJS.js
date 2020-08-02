var titem = $('#itemTbl').DataTable();

var len = 0;
function AddItemData(lennumber,url) {
    if (len == 0) {
        len = parseInt(lennumber);
    }
    var selectedareaid = $('#C_SelectedItemType').val();
    var selectedUserId = $('#selectedUserId').val();
    $.ajax({
        url: url,//'@Url.Action("GetAllItemDtlByFilterId", "Scheme")',
        type: "GET",
        dataType: "JSON",
        data: { UserAreaTypeId: selectedareaid, DataFor: "ITEM_GROUP", AreaId: $('#hdnSelectedItemGroup').val(), UserId: selectedUserId },
        async: false,
        success: AddItemDataSucceeded,
        error: AddItemDataFailed
    });
}

function AddItemDataSucceeded(result) {
    debugger;
    ShowWaitingSymbol();
    if (result.length == 0) {
        alert("Data not found");
    }
    var dealerids = [];
    titem.$('input[type="hidden"]').each(function () {
        dealerids.push(parseInt(this.value));
    });
    $.each(result, function (i, obj) {
        debugger;
        if (jQuery.inArray(obj.PART_ID, dealerids) == -1) {

            var cureenid = parseInt($('#itemloopcount').val()) + 1;
            titem.row.add([
                '<input type="checkbox" id="Itemcb_' + cureenid + '" name="Itemcb[]" >',
                obj.SAP_PartNumber + '<input type="hidden" id="PART_ID_' + cureenid + '" value="' + obj.PART_ID + '" >',
                obj.PartNumber,
                obj.PartDescription,
              //  obj.PartFamilyName,
                obj.SubcategoryName,
                obj.ProductCategoryName,
                obj.SegmentName,
                '<input type="text" id="MINQTY_' + cureenid + '" class="form-control" >'
            ]).node().id = 'ItemRow_' + cureenid;

            $('#itemloopcount').val(cureenid);
        }

    });
    titem.draw();
    HideWaitingSymbol();
}

function AddItemDataFailed(result) {

}



// Handle click on "Select all" control
$('#cballItem').on('click', function () {

    // Check/uncheck all checkboxes in the table
    var rows = titem.rows({ 'search': 'applied' }).nodes();
    $('input[type="checkbox"]', rows).prop('checked', this.checked);
    $('input[type="checkbox"]', rows).parent().parent().addClass('row_selected');
});
// Handle click on checkbox to set state of "Select all" control
$('#itemTbl tbody').on('change', 'input[type="checkbox"]', function () {
    debugger;
    var rowno = this.id.split('_')[1];
    // If checkbox is not checked
    if (!this.checked) {
        var el = $('#cballItem').get(0);
        // If "Select all" control is checked and has 'indeterminate' property
        if (el && el.checked && ('indeterminate' in el)) {
            // Set visual state of "Select all" control
            // as 'indeterminate'
            el.indeterminate = true;
        }
        $('#ItemRow_' + rowno).removeClass('row_selected');
    }
    else {
        $('#ItemRow_' + rowno).addClass('row_selected');
    }
});

$('#removeallItem').click(function () {
    debugger;
    titem.rows('.row_selected').remove().draw();
    $('#cballItem').prop("checked", false);
});

function CheckItemValidation() {
    debugger;
    var valid = false;
    var totalrows = titem.rows().count();
    var itemids = "";
    var partqty = "";
    if (totalrows > 0) {
        //get column 1 because we are hahnig hidden value
        titem.$('input[type="hidden"]').each(function () {
            itemids = itemids == "" ? this.value : itemids + "," + this.value;

        });
        titem.$('input[type="text"]').each(function () {
           
            partqty = partqty == "" ? (this.value == "" ? "0" : this.value) : partqty + "," + (this.value == "" ? "0" : this.value);

        });

        $('#ITEMIDARR').val(itemids);
        $('#ITEMQTYARR').val(partqty);
        valid = true;

    }
    else {
        alert("Please select at least one part");
    }

    return valid;
}



function IsItemGrpExistData(url) {
    debugger;
    var checkUrl = url;// '@Url.Action("IsItemGrpExist", "Scheme")';
    var IGname = $('#txtItemGroupName').val();

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: checkUrl,
        data: JSON.stringify({ ItemGroupName: IGname }),
        dataType: "json",
        success: function (data) {
            if (!data) {
                alert("Item Group Name already exists");
                $('#txtItemGroupName').val('');
            }
        },
        error: function (data) {

        }
    });

}