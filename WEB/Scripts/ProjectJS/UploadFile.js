function checkFileValidation(fileid) {
    debugger;
    var file = $('#' + fileid).val();
    var fileSize = 0;
    if (file != "") {
        fileSize = GetFileSize('fileField');
        if (file == '' || file == null) {
            valMessage.innerHTML = "Please select excel file ";
            return false;
        }
        else if (!(/\.(xlsx|xls)$/i).test(file)) {
            $(file).val('');
            alert("Please select excel file only");
            return false;

        }
        else if (fileSize > 8.0) {
            alert("Please select excel file upto 8 mb only");
            return false;


        }
        else {

            $.blockUI();
            return true;
        }
    }
    else {
        alert("Please select file");
        return false;
    }

}

function GetFileSize(fileField) {

    try {
        var fileSize = 0;
        //for IE
        if (navigator.userAgent.match(/msie/i)) {

            //before making an object of ActiveXObject, 
            //please make sure ActiveX is enabled in your IE browser
            //var objFSO = new ActiveXObject("Scripting.FileSystemObject"); 

            //var filePath = $("#"+fileField)[0].value;
            //var objFile = objFSO.getFile(filePath);
            //fileSize = objFile.size; 
            //size in kb
            //fileSize = fileSize / 1048576; 
            //size in mb
            fileSize = fileSize;
        }
        //for FF, Safari, Opeara and Others
        else {
            fileSize = $("#" + fileField)[0].files[0].size //size in kb
            fileSize = fileSize / 1048576; //size in mb 
        }
        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}
