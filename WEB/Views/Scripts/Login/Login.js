$(document).ready(function () {
    $(".loginbtnvld").click(function () {
        var a = $("#UserName").val(), b = $("#Password").val(); if ("" == a) return alert("Please enter user name"), !1; if ("" == b) return alert("Please enter Password"),
          !1; var c = CryptoJS.enc.Utf8.parse("8080808080808080"), d = CryptoJS.enc.Utf8.parse("8080808080808080"),
            e = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(b), c, { keySize: 16, iv: d, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 }); $("#Password").val(e);
        BlockUI();
    })
});