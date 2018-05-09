function NumberOnly(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) { return false; }
    return true;
}

function ArabicOnly(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode >= 1536 && charCode <= 1791) { return true; }
    if (charCode >= 48   && charCode <= 57  ) { return true; }
    if ((charCode >= 32 && charCode <= 47) || (charCode >= 58 && charCode <= 64) || (charCode >= 91 && charCode <= 96) || (charCode >= 123 && charCode <= 126)) { return true; }
    return false;
}

function EnglishOnly(e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if (charCode >= 32 && charCode <= 127) { return true; }
    return false;
}


//onkeypress="return ArabicOnly(event);"
function CheckEnglishOnly(field) {
    var sNewVal = "";
    var sFieldVal = field.value;

    for (var i = 0; i < sFieldVal.length; i++) {
        var ch = sFieldVal.charAt(i);
        var c = ch.charCodeAt(0);

        if (c < 0 || c > 255) { /* Discard */ } else { sNewVal += ch; }
    }

    field.value = sNewVal;
}
//onchange="CheckEnglishOnly(this);"

function CheckArabicOnly(field) {
    var sNewVal = "";
    var sFieldVal = field.value;

    for (var i = 0; i < sFieldVal.length; i++) {
        var ch = sFieldVal.charAt(i); ;
        var c = ch.charCodeAt(0);

        if (c < 1536 || c > 1791) { /* Discard */ } else { sNewVal += ch; }
    }

    field.value = sNewVal;
}
//onchange="CheckArabicOnly(this);"


function DefaultButton(e, buttonid) {

    var evt = e ? e : window.event;

    var bt = document.getElementById(buttonid);

    if (bt) {
        if (evt.keyCode == 13) {
            bt.click();
            return false;
        }
    }
}
