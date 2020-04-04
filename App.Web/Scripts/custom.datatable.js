/*data table initializer*/
function DataTableBinder(obj, url, rowsPerPage, isEnableAllRows, additionalPostdata, buttonsIn, isPaginationEnable) {

    var ln = [];
    var lnT = [];
    if (rowsPerPage != null) {
        jQuery.each(rowsPerPage, function () {
            ln.push(this);
            lnT.push(this);
        });
    }
    if (isEnableAllRows) {
        ln.push(-1); lnT.push("All");
    }



    obj.dataTable({
        aaSorting: [[0, "desc"]],
        bRetrieve: true,
        bJQueryUI: false,
        "bSort": false,
        "bFilter": false,
        "bInfo": isPaginationEnable,
        "bLengthChange": rowsPerPage == null ? false : true,
        "bPaginate": isPaginationEnable,
        "iDisplayLength": rowsPerPage == null ? 1000000 : rowsPerPage[0],
        "aLengthMenu": [ln, lnT],
        "sAjaxSource": url,
        "bProcessing": true,
        "bServerSide": true,
        "sPaginationType": "full_numbers",
        "sDom": "<'row-fluid'<'span7'l><'span5'f>r>t<'row-fluid'<'span5'i><'span7'p>>",
        "sPaginationType": "bootstrap",
        /*"aoColumnDefs": [
               {
                   "aTargets": [0],
                   "mData": null,
                   "mRender": function (data, type, full) {
                       alert(data);
                       alert(type);
                       alert(full);
                       return '<a href="#" onclick="alert(\'' + full[0] + '\');">Process</a>';
                   }
               }
        ],*/
        "fnServerData": function (sSource, aoData, fnCallback) {
            if (additionalPostdata != null) {
                jQuery.each(additionalPostdata, function (key, value) {
                    //aoData.push({ "name": key, "value": value });
                    aoData.push({ "name": key, "value": $("#" + value + "").val() });
                });
            }
            $.getJSON(sSource, aoData, function (json) {
                /* dynamic ajax button add */
                //alert('Total records found: ' + json.iTotalRecords);
                var iTotalRecords = $("#iTotalRecords");
                if (iTotalRecords != null) {
                    iTotalRecords.text(json.iTotalRecords);
                }
                if (buttonsIn != null && aoData != null) {    //if has button
                    var btnTxt = '';        //write button html                
                    var lnkTxt = '';        //buttton value
                    var icon = '';
                    var dlg = '';
                    var hdr = '';
                    var href = '';
                    var isAjaxButton = false;
                    var cls = '';
                    var isBtnVisible = true;
                    var isGroup = false;
                    //var indx = 0;

                    for (var i = 0, iLen = json.aaData.length ; i < iLen ; i++) {   //loop for every rows
                        //indx = 0;

                        jQuery.each(buttonsIn, function (key, value) {  //buttons array of columns
                            btnTxt = '';    //reser button html
                            isGroup = value;
                            //indx++;
                            if (isGroup) {    //if button group                                
                                //btnTxt = '<div class="btn-group pull-right"><a class="btn dropdown-toggle btn-info btn-mini" href="#" data-toggle="dropdown">Action<span class="caret"></span></a><ul class="dropdown-menu"><li>';
                                btnTxt = '<div class="btn-group"><a class="btn btn-info btn-mini" data-toggle="dropdown" href="#">Action</a><a class="btn btn-info btn-mini dropdown-toggle" data-toggle="dropdown" href="#"><span class="caret"></span></a><ul class="dropdown-menu">';
                            }
                            else {                                
                                btnTxt = '<div class="btn-group">';//<div class="btn-toolbar pull-right">
                            }

                            jQuery.each(json.aaData[i][key], function () {  // loop for multiple buttons at a column

                                jQuery.each(this, function (k, v) { // adding button properties K > key, V > value

                                    if (k == 'U') { href = (v == null ? '#' : v); }
                                    else if (k == 'T') { lnkTxt = (v == null ? '' : v); }
                                    else if (k == 'I') { icon = v;}
                                    else if (k == 'M') { cls = ' '+v; }
                                    else if (k == 'A') { isAjaxButton = (v == null ? true : v); }//if not set A then it is a Ajax Button
                                    else if (k == 'H') { hdr = v; }
                                    else if (k == 'V') { isBtnVisible = (v == null ? true : v); }
                                    else if (k == 'D') {
                                        if (v == 'mb') dlg = 'mb'; //mainbody
                                        else if (v == 'd1') dlg = 'd1'; //dialog1
                                        else if (v == 'd2') dlg = 'd2'; //dialog2
                                        else dlg = 'mb'; //if dialog but no dialod selected then it will main body                                            
                                    }
                                });
                                      
                                if (isBtnVisible) {
                                    //creating each button at this column
                                    if (isGroup) {
                                        btnTxt += '<li>';
                                    }

                                    if (isAjaxButton && href != '#') {
                                        btnTxt += '<a  data-ajax-method="GET" data-ajax-mode="replace" data-ajax-loading="#loading" data-ajax-failure="failure" data-ajax="true"';
                                    }
                                    else { btnTxt += '<a '; }

                                    btnTxt += ' href="' + href + '"' + cls;

                                    if (isAjaxButton && href != '#') {
                                        if (dlg == 'mb') btnTxt += ' data-ajax-update="#MainBody"';
                                        else if (dlg == 'd1') btnTxt += ' data-ajax-update="#dialog1Body" data-ajax-success="openDialog1(\'' + hdr + '\')"';
                                        else if (dlg == 'd2') btnTxt += ' data-ajax-update="#dialog2Body" data-ajax-success="openDialog2(\'' + hdr + '\')"';
                                    }

                                    //alert(icon);
                                    btnTxt += ' >' + lnkTxt + (icon == null ? '' : (' <i class="' + icon + '"></i>')) + '</a>';   //completing button

                                    if (isGroup) {
                                        btnTxt += '</li>';
                                    }
                                    else { btnTxt += ' '; }    //button separator
                                }
                            });

                            if (isGroup) {    //if button group
                                btnTxt += '</ul></div>';   //completing group button
                            }
                            else {
                                btnTxt += '</div>';//</div>
                            }

                            json.aaData[i][key] = btnTxt;   //replacing column data to button                            
                        });
                        //break;
                    }
                }

                fnCallback(json);
            });
        },
    });
}

function DataTableBinderClient(obj, rowsPerPage, isEnableAllRows) {

    var ln = [];
    var lnT = [];
    jQuery.each(rowsPerPage, function () {
        ln.push(this);
        lnT.push(this);
    });
    if (isEnableAllRows) {
        ln.push(-1); lnT.push("All");
    }
    obj.dataTable({
        bJQueryUI: false,
        "bSort": true,
        "bFilter": true,
        "iDisplayLength": rowsPerPage[0],
        "aLengthMenu": [ln, lnT],
        "sPaginationType": "full_numbers",
        "sDom": "<'row-fluid'<'span7'l><'span5'f>r>t<'row-fluid'<'span5'i><'span7'p>>",
        "sPaginationType": "bootstrap"
    });
}

function DataTableRedraw(obj) {
    obj.dataTable().fnDraw();
}



