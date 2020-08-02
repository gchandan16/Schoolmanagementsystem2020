


function FillStateList(statelist) {
    var v = "<option>--Select--</option>";
    $.each(statelist, function (i, state) {

        v += "<option value=" + state.STATE_ID + ">" + state.STATENAME + "</option>";

    });

    $("#SelectedStateId").html(v);
    $("#SelectedStateId").val('').trigger("chosen:updated");
}

function FillDistrictList(districtlist) {
    var v = "<option>--Select--</option>";
    $.each(districtlist, function (i, district) {

        v += "<option value=" + district.DISTRICT_ID + ">" + district.DISTRICTNAME + "</option>";
    });
    $("#SelectedDistrictId").html(v);
    $("#SelectedDistrictId").val('').trigger("chosen:updated");
}
function FillCityList(citylist) {
    var v = "<option>--Select--</option>";
    $.each(citylist, function (i, city) {

        v += "<option value=" + city.CITY_ID + ">" + city.CITYNAME + "</option>";

    });

    $("#SelectedCityId").html(v);
    $("#SelectedCityId").val('').trigger("chosen:updated");
}

function GetStateByCountry(CountryId, url) {
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: Number(CountryId) },
        success: GetStateByCountryIdSuccess,
        error: GetStateByCountryIdFail
    });

}


function GetStateByCountryIdSuccess(result) {
    FillStateList(result);
}
function GetStateByCountryIdFail(result) {
    console.log(result);
}

function GetDistrictByState(StateId, url) {
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: StateId },
        success: GetDistrictByStateIdSuccess,
        error: GetDistrictByStateIdFail
    });

}
function GetDistrictByStateIdSuccess(result) {
    FillDistrictList(result);
}

function GetDistrictByStateIdFail() {
}

function GetCityByDistrict(DistrictId, url) {
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: DistrictId },
        success: GetCityByDistrictIdSuccess,
        error: GetCityByDistrictIdFail
    });
}

function GetCityByDistrictIdFail() {

}

function GetCityByDistrictIdSuccess(result) {
    FillCityList(result);
}

function bindCityDDL(id, url) {
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(list) {
            $('#SelectedCityId').empty();
            var v = "<option value=''>--Select--</option>";
            $.each(list, function (i, item) {
                v += "<option value=" + item.City_Id + ">" + item.CITYNAME + "</option>";
            });
            $("#SelectedCityId").html(v);
        },
        error: function bindDDLFail() { }
    });
}

function bindZoneDDL(id, url) {
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(List) {
            $('#SelectedZoneId').empty();
            var v = "<option value=''>--Select--</option>";
            $.each(List, function (i, item) {
                v += "<option value=" + item.ZONE_ID + ">" + item.ZONENAME + "</option>";
            });
            $("#SelectedZoneId").append(v);
            $('#SelectedStateId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedTerritoryId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedDistrictId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedCityId').empty().append('<option value=0>--Select--</option>');
        },
        error: function bindDDLFail() {
            $('#SelectedZoneId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedStateId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedTerritoryId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedDistrictId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedCityId').empty().append('<option value=0>--Select--</option>');
        }
    });
}

function bindTerritoryDDL(id, url) {
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(List) {
            $('#SelectedTerritoryId').empty();
            var v = "<option value=''>--Select--</option>";
            $.each(List, function (i, item) {
                v += "<option value=" + item.TERRITORY_ID + ">" + item.TERRITORYNAME + "</option>";
            });
            $("#SelectedTerritoryId").append(v);
            $('#SelectedDistrictId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedCityId').empty().append('<option value=0>--Select--</option>');
        },
        error: function bindDDLFail() { }
    });
}

function bindStateDDL(id, url) {
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(List) {
            $("#SelectedStateId").empty();
            var v = "<option value=''>--Select--</option>";
            $.each(List, function (i, item) {
                v += "<option value=" + item.STATE_ID + ">" + item.STATENAME + "</option>";
            });
            $("#SelectedStateId").append(v);
            $('#SelectedTerritoryId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedDistrictId').empty().append('<option value=0>--Select--</option>');
            $('#SelectedCityId').empty().append('<option value=0>--Select--</option>');
        },
        error: function bindDDLFail() { }
    });
}

function bindDistrictDDL(id, url) {
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(List) {
            $("#SelectedDistrictId").empty();
            var v = "<option value=''>--Select--</option>";
            $.each(List, function (i, item) {
                v += "<option value=" + item.DISTRICT_ID + ">" + item.DISTRICTNAME + "</option>";
            });
            $("#SelectedDistrictId").html(v);
            $('#SelectedCityId').empty().append('<option value=0>--Select--</option>');
        },
        error: function bindDDLFail() { }
    });
}

function bindDealerDDL(id, url) {
    //debugger;
    var ID = id
    var checkUrl = url;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { id: ID },
        success: function bindDDLSuccess(List) {
            var v = "<option value=''>--Select--</option>";
            $.each(List, function (i, item) {
                v += "<option value=" + item.DEALER_ID + ">" + item.DEALERNAME + "</option>";
            });
            $("#SelectedDealerId").html(v);
        },
        error: function bindDDLFail() { }
    });
}

function bindUserAreasDealersDDL(UserId, url) {
    //debugger;
    var userId = UserId
    var checkUrl = url;

    //debugger;
    $.ajax({
        url: checkUrl,
        type: "GET",
        dataType: "JSON",
        data: { userId: userId},

        success: bindDDLSuccess1,
        error: function bindDDLFail() { }
    });
}

function bindDDLSuccess1(List) {
    debugger
    var v = "<option value=''>--Select Dealer--</option>";
    $.each(List, function (i, item) {
        v += "<option value=" + item.DEALER_ID + ">" + item.DEALERCODE + " (" + item.DEALERNAME + ")</option>";
    });
    $("#SelectedDealer_ID").html(v);
}
function bindPartyGroup(countryId, zoneId, territoryId, stateId, districtId, cityId, PartyId, partyGrpHdrId, url) {

    $.ajax({
        url: url,
        type: "GET",
        dataType: "JSON",
        data: { countryId: countryId, zoneId: zoneId, territoryId: territoryId, stateId: stateId, districtId: districtId, cityId: cityId, partyId: PartyId, partyGrpHdrId: partyGrpHdrId },
        success: function (result) {
            //$('#ItemList tbody').remove();
            //var data = Json.Parse(result);
            //var PartyDetailCount = result.length;
            //var valid = true;
            
            if ($('#dealerTbl tbody tr').hasClass('odd')) {
                $('#dealerTbl tbody').find('tr.odd').remove();
            }
            var len = $('#dealerTbl tbody tr').length;
            if ($('#dealerTbl tbody').length == 0) {
                var tbody = "<tbody><input type='hidden' id='PartyDetailCount' name = 'PartyDetailCount' />";
            }
            else {
                tbody = $('#dealerTbl tbody');
            }
            $.each(result, function (i, data) {
                //var PartNumber = $(this).find('td').eq(1).text().trim();

                if ($('#dealerTbl tbody tr td:contains(' + data.DEALERNAME + ')').length == 0) {
                    tbody += "<tr><td><input type='checkbox' name='tblChekboxes' id='check_" + data.DEALER_ID + "' /><input type='hidden' id=DealerId_" + (len + 1) + " value=" + data.DEALER_ID + " name = DealerId_" + (len + 1) + " /></td>" +
                        "<td>" + data.DEALERCODE + "</td><td>" + data.DEALERNAME + "</td><td>" + data.CityName +
                        "</td></tr>";
                    len = len + 1;
                }
                len = len + 1;
            });
            $('#dealerTbl').append(tbody);
            $('#PartyDetailCount').attr('value', Number($('#dealerTbl tbody tr').length));
          
        },
        error: function () {
        }
    });
}


function FillStateListIntoMomentLeft(statelist) {
    var v = "";
    $.each(statelist, function (i, state) {
        v += "<li class=\"list-group-item\"><input type=\"hidden\" value=\"" + state.STATE_ID + "\" name=\"menu_" + state.STATE_ID + "\">" + state.STATENAME + "</li>"
    });

    $("#momentlistLeft").html(v);
}

function FillStateListIntoMomentRight(statelist) {
    var v = "";
    $.each(statelist, function (i, state) {
        v += "<li class=\"list-group-item\"><input type=\"hidden\" value=\"" + state.STATE_ID + "\" name=\"menu_" + state.STATE_ID + "\">" + state.ZoneStateName + "</li>"
    });

    $("#momentlistRight").html(v);
}

function FillDistrictListIntoMomentLeft(DistrictList) {
    var v = "";
    $.each(DistrictList, function (i, district) {
        v += "<li class=\"list-group-item\"><input type=\"hidden\" value=\"" + district.DISTRICT_ID + "\" name=\"menu_" + district.DISTRICT_ID + "\">" + district.DISTRICTNAME + "</li>"
    });

    $("#momentlistLeft").html(v);
}

function FillDISTRICTListIntoMomentRight(TerritoryDistrictList) {
    var v = "";
    $.each(TerritoryDistrictList, function (i, territorydistrict) {
        v += "<li class=\"list-group-item\"><input type=\"hidden\" value=\"" + territorydistrict.DISTRICT_ID + "\" name=\"menu_" + territorydistrict.DISTRICT_ID + "\">" + territorydistrict.TerritoryDistrictName + "</li>"
    });

    $("#momentlistRight").html(v);
}
