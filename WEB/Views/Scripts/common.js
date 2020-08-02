function isKeyValid(evt, validation) {
    var Status = false;
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode
    var validations = validation.split(",");

    for (var i = 0; i < validations.length; i++) {
        if (validations[i].split("-").length == 2) {
            if (!(charCode >= validations[i].split("-")[0] && charCode <= validations[i].split("-")[1]))
                Status = false;
            else
                Status = true;
        }
        else {
            if (charCode != validations[i])
                Status = false;
            else
                Status = true;
        }
        if (Status) break;
    }
    return Status;
}
function isKeyValidDecimal(evt, validation, ddg) {
    var Status = false;
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode
    var validations = validation.split(",");
    for (var i = 0; i < validations.length; i++) {
        if (validations[i].split("-").length == 2) {
            if (!(charCode >= validations[i].split("-")[0] && charCode <= validations[i].split("-")[1]))
                Status = false;
            else
                Status = true;
        }
        else {
            if (charCode == 46 && $("#" + (evt.target.id)).val().indexOf('.') > -1)
                Status = false;
            else if (charCode != validations[i])
                Status = false;
            else
                Status = true;
        }
        if (Status) break;
    }
    return Status;
}
function inputValidation() {
    var Status = false;
    $('.mandate').each(function () {
        if ($(this).is(":visible")) {
            var val = "";
            var pat = "";
            var minLength = true;
            if (this.type == "checkbox") {
                val = $(this).is(":checked") ? "checked" : "";
            }
            val = $.trim($("#" + $(this).attr('id')).val());
            pat = $(this).attr('pat');
            minLength = $(this).attr('minlength') != undefined ? parseInt($(this).attr('minlength')) <= val.length ? true : false : true;
            if (val == "" || !val.match(pat) || !minLength) {
                if ($(this).attr('msg') != undefined)
                    alert($(this).attr('msg'));
                else
                    alert("Value required.");
                $(this).focus();
                Status = false;
                return false;
            }
            else {
                Status = true;
            }
        }
    });
    return Status;
}
function getLoginStatus() {
    return false;
}