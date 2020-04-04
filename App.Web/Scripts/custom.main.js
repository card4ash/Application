/*variable declaration*/
var keepSessionAliveUrl = null;
//var totalRecordCount = 0;

/*general modal functions*/
function openDialog1(title) {
    $('#dialog1Label').html(title);
    $('#dialog1').modal('show');
}
function openDialog2(title) {
    $('#dialog1Labe2').html(title);
    $("#dialog2").modal('show');
}

function closeDialog1() {
    $("#dialog1").modal('hide');
}

function closeDialog2() {
    $("#dialog2").modal('hide');
}

/*error modal functions*/
function failure() {
    $('#errorModal').modal('show');
}

function failureClose() {
    $('#errorModal').modal('hide');
}

var ValidatateForm = function (formId) {
    $.validator.unobtrusive.parse("#" + formId);
}

/*timer for heartbit (keep alive session)*/
function KeepSessionAlive(heartBitUrl) {
    keepSessionAliveUrl = heartBitUrl;
    setInterval("runHeartBit()", 115000);
}

function runHeartBit() {
    $.ajax({
        type: "POST",
        url: keepSessionAliveUrl,
        success: function (data) {
        },
        error: function () {
            $('#disconnectedModal').modal('show');
        }
    });
}

function LoadDropDownList(objDDLId, url, optionalLabel, objDataTableToReload) {
    var objDDL = $("#" + objDDLId);
    objDDL.empty();
    if (optionalLabel != null) {
        objDDL.append($('<option/>', {
            value: "",
            text: optionalLabel
        }));
    }
    $.ajax({
        cache: false,
        url: url,
        type: "GET",
        beforeSend: function () {
        },
        complete: function () {
        },
        success: function (data) {

            $.each(data, function (index, itemData) {
                objDDL.append($('<option/>', {
                    value: data[index]["Value"],
                    text: data[index]["Text"]
                }));
            });

            if (objDataTableToReload != null) {
                DataTableRedraw(objDataTableToReload);
            }
        },
        error: function (x, e) {
            //alert('error');
        }
    }); //end ajax call
}

function LoadDropDownListAdv(objDDLId, url, optionalLabel, objDataTableToReload, valueField, textField) {
    var objDDL = $("#" + objDDLId);
    objDDL.empty();
    if (optionalLabel != null) {
        objDDL.append($('<option/>', {
            value: "",
            text: optionalLabel
        }));
    }
    $.ajax({
        cache: false,
        url: url,
        type: "GET",
        beforeSend: function () {
        },
        complete: function () {
        },
        success: function (data) {

            $.each(data, function (index, itemData) {
                objDDL.append($('<option/>', {
                    value: data[index][valueField],
                    text: data[index][textField]
                }));
            });

            if (objDataTableToReload != null) {
                DataTableRedraw(objDataTableToReload);
            }
        },
        error: function (x, e) {
            //alert('error');
        }
    }); //end ajax call
}

function LoadDropDownListWithCall(objDDLId, url, optionalLabel, functionCall) {
    var firstId = 0;
    var objDDL = $("#" + objDDLId);
    objDDL.empty();

    if (optionalLabel != null) {
        objDDL.append($('<option/>', {
            value: "",
            text: optionalLabel
        }));
    }

    $.ajax({
        cache: false,
        url: url,
        type: "GET",
        beforeSend: function () {
        },
        complete: function () {
        },
        success: function (data) {


            $.each(data, function (index, itemData) {
                if (index == 0) {
                    firstId = data[0]["Value"];
                }
                objDDL.append($('<option/>', {
                    value: data[index]["Value"],
                    text: data[index]["Text"]
                }));
            });

            if (functionCall != null) {
                window[functionCall](firstId);//.apply(null, Array.prototype.slice.call(arguments, 1));
            }
        },
        error: function (x, e) {
            //alert('error');
        }

    }); //end ajax call    
}

function AjaxPost(url, data) {

    return $.ajax({
        cache: false,
        async: true,
        url: url,
        type: "GET",
        contentType: 'application/json;charset=utf-8',
        data: data,
        beforeSend: function () {
            $("#loading").show();
        },
        success: function (result) {
            $("#loading").hide();
            //this.Callback(result);
        },
        error: function (x, e) {
            $("#loading").hide();
            alert('error at calling' + url);
        }
    });
}


function HideDDLWithSingleValue(ddlId) {
    var ddl = $('#' + ddlId);
    var count = ddl.children('option').length;
    if (count <= 1) {
        ddl.hide();
    }
}

Date.prototype.format = function (format) //author: meizz
{
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1,
          (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            if (k == "M+") {
                format = format.replace(RegExp.$1, months[o[k] - 1]);
            }
            else {
                format = format.replace(RegExp.$1,
                  RegExp.$1.length == 1 ? o[k] :
                    ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
    }
    return format;
}

function FormatJsonDate(value, dateFormat) {
    var dte = eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
    return dte.format(dateFormat);
}

function DefaultDate(dte, dateFormat) {
    return dte.format(dateFormat);
}

function DateFormartConverter(dateFormat) {
    dateFormat = dateFormat.replace('MMM', 'M');
    dateFormat = dateFormat.replace('mm', 'ii');
    return dateFormat;
}

function DatePicker(objId, dateFormat) {
    $('#' + objId).datepicker({
        format: DateFormartConverter(dateFormat)
        , autoclose: true
    });

}

function DatePickerWithDisable(objId, dateFormat, startDate, isDisableFuture) {
    if (isDisableFuture) {
        $('#' + objId).datepicker({
            format: DateFormartConverter(dateFormat)
            , autoclose: true
            , endDate: '+0d'
            , startDate: new Date(startDate)
        });
    }
    else {
        $('#' + objId).datepicker({
            format: DateFormartConverter(dateFormat)
           , autoclose: true           
           , startDate: new Date(startDate)
        });
    }
}

function FormatDecimal(value) {
    if (Math.round(value) !== value) {
        value = value.toFixed(1);
    }

    return value;
}