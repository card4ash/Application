
function RenderImageGrid(obj, url, postData) {

    obj.html(''); //clear data
    
    var text = '';
    var href = '';
    var bynary = '';
    var hdr = '';
    var isAjaxButton = false;
    var btnTxt = '';
    var dlg = '';
    var cls = '';
    var txtCls = '';
    var grp = '';
    var child = '';
    var noImg = 'iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAABUUlEQVR4nO2UMW7DMAxFc/+jcOToUbMuwEvwCurEQFXsoojgPBT9wxsSO8TTk6JHZo7/zIMWoFEAWoBGAWgBGgWgBWgUgBagUQBagEYBaAEaBaAFaBSAFqBRAFqARgFoARoFoAVoFIAWoNkO0FobZjbMbBzHcfqOmQ13R2feEqBEM3P03oeZjd776Tu/lb1j5m0B3P1yhzJzRMRzJ2dZd39ZZGttayYSoI5oCdUi1kDu/k22FlG/nZ+9OxMLcHVc63NEnMrOi5yP+M5M7ASsu1rP5mO9ytb76/c7Mz8eYP2/lmzt1MrZPbAe852ZHw9Qt3FEXN7YZ7tVv+u9P0NExNZMJEBm/nhhXcnOO7ce83dnYgH+OgpAC9AoAC1AowC0AI0C0AI0CkAL0CgALUCjALQAjQLQAjQKQAvQKAAtQKMAtACNAtACNApAC9AoAC1A8wXUOCBTu25bVgAAAABJRU5ErkJggg==';

    $.ajax({
        cache: false,
        url: url,
        data: postData,
        type: "GET",
        beforeSend: function () {
            $("#loading").show();
        },
        complete: function () {
        },
        success: function (data) {
            $("#loading").hide();
            $.each(data, function (index, groupData) {

                grp = '';
                child = '';
                color = '';

                jQuery.each(this, function (k, v) {
                    if (k == 'N') {
                        grp = v;
                    }
                    else if (k == 'C') {
                        txtCls = v;
                    }
                    else if (k == 'D') {
                        $.each(v, function (index, images) {
                            text = '';
                            href = '';
                            bynary = '';
                            dlg = '';
                            cls = '';
                            hdr = '';
                            id = '';
                            btnTxt = '';

                            jQuery.each(this, function (kk, vv) {
                                btnTxt = '';
                                if (kk == 'T') {
                                    text = vv;
                                }
                                else if (kk == 'U') {
                                    {
                                        //href = ' href="' + vv + '"';
                                        href = vv;
                                    }
                                }
                                else if (kk == 'B') {
                                    bynary = vv;
                                }
                                else if (kk == 'C') { cls = ' class="' + vv + '"'; }
                                else if (kk == 'H') {
                                    hdr = vv;
                                }
                                else if (kk == 'I') {
                                    id = vv;
                                }
                            });

                            btnTxt += '<a href="#" name="' + text + '" id="' + id + '" url="' + href + '" role="button" ' + cls + ' data-toggle="modal">';

                            bynary = bynary == '' || bynary == null ? noImg : bynary;
                            child += '<div class="imgGallery">' + btnTxt + '<img class="media-object" data-src="holder.js/64x64" alt="' + text + '" src="data:image/png;base64,' + bynary + '" /><div class="media-body ' + txtCls + '">' + text + '</div></a></div>';
                        });
                    }
                });

                obj.append('<div class="imgGalleryGrp ' + txtCls + '" style="text-align:center;"><h2 class="media-heading" style=" padding-top:15px;">' + grp + '</h2></div>');
                obj.append(child);
            });
        },
        error: function (x, e) {
            alert('error');
        }
    }); //end ajax call
}



function RenderImageGridRent(obj, url, postData) {

    obj.html(''); //clear data

    var text = '';
    var lnkText = '';
    var href = '';
    var bynary = '';
    var hdr = '';
    var isAjaxButton = false;
    var btnTxt = '';
    var dlg = '';
    var cls = '';
    var txtCls = '';
    var grp = '';
    var child = '';
    var noImg = 'iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAABUUlEQVR4nO2UMW7DMAxFc/+jcOToUbMuwEvwCurEQFXsoojgPBT9wxsSO8TTk6JHZo7/zIMWoFEAWoBGAWgBGgWgBWgUgBagUQBagEYBaAEaBaAFaBSAFqBRAFqARgFoARoFoAVoFIAWoNkO0FobZjbMbBzHcfqOmQ13R2feEqBEM3P03oeZjd776Tu/lb1j5m0B3P1yhzJzRMRzJ2dZd39ZZGttayYSoI5oCdUi1kDu/k22FlG/nZ+9OxMLcHVc63NEnMrOi5yP+M5M7ASsu1rP5mO9ytb76/c7Mz8eYP2/lmzt1MrZPbAe852ZHw9Qt3FEXN7YZ7tVv+u9P0NExNZMJEBm/nhhXcnOO7ce83dnYgH+OgpAC9AoAC1AowC0AI0C0AI0CkAL0CgALUCjALQAjQLQAjQKQAvQKAAtQKMAtACNAtACNApAC9AoAC1A8wXUOCBTu25bVgAAAABJRU5ErkJggg==';
    var date;
    var prop;
    var propName;
    var mainClass = '';
    var nameFor = '';
    var uomId;

    $.ajax({
        cache: false,
        url: url,
        data: postData,
        type: "GET",
        beforeSend: function () {
            $("#loading").show();
        },
        complete: function () {
        },
        success: function (data) {
            $("#loading").hide();
            $.each(data, function (index, groupData) {

                grp = '';
                child = '';
                color = '';

                jQuery.each(this, function (k, v) {
                    if (k == 'N') {
                        grp = v;
                    }
                    else if (k == 'C') {
                        txtCls = v;
                    }
                    else if (k == 'D') {
                        $.each(v, function (index, images) {
                            text = '';
                            href = '';
                            bynary = '';
                            dlg = '';
                            mainClass = '';
                            hdr = '';                            
                            btnTxt = '';
                            href = '';
                            hdr = '';

                            jQuery.each(this, function (kk, vv) {
                                btnTxt = '';

                                if (kk == 'T') {
                                    text = vv;
                                }
                                else if (kk == 'B') {
                                    bynary = vv;
                                }
                                else if (kk == 'C') {
                                    mainClass = vv;
                                }
                                else if (kk == 'U') {
                                    href = vv;
                                }
                                else if (kk == 'H') {
                                    hdr = vv;
                                }                                
                                else if (kk == 'L') {

                                    /*btnTxt += '<table class="table table-striped">';
                                    jQuery.each(this, function (i1, v1) {

                                        lnkText = '';                                        
                                        cls = '';
                                        date = '';
                                        prop = '';
                                        id = '';
                                        nameFor = '';

                                        $.each(v1, function (kkk, vvv) {

                                            if (kkk == 'T') {
                                                lnkText = vvv;
                                            }                                           
                                            else if (kkk == 'C') {
                                                cls = ' class=" ' + vvv + '"';
                                            }
                                            else if (kkk == 'D') {
                                                date = vvv;
                                            }
                                            else if (kkk == 'P') {
                                                prop = vvv;
                                            }
                                            else if (kkk == 'M') {
                                                propName = vvv;
                                            }
                                            else if (kkk == 'N') {
                                                nameFor = vvv;
                                            }
                                            else if (kkk == 'I') {
                                                id = vvv;
                                            }
                                            else if (kkk == 'U') {
                                                uomId = vvv;
                                            }
                                        });

                                        btnTxt += '<tr><td><input name="' + id + '" prodname="' + nameFor + '" type="checkbox" ' + cls + ' date="' + date + '" prop="' + prop + '" propname="' + propName + '" uom="' + uomId + '"/></td><td><small>' + lnkText + '</small></td></tr>'; //<a name="' + hdr + '" id="' + id + '" url="' + href + '" role="button" ' + cls + ' data-toggle="modal" href="#">' + lnkText + '</a>
                                        
                                    });

                                    btnTxt += '</table>';*/
                                }                                
                            });
                                                        
                            //bynary = bynary == '' || bynary == null ? noImg : bynary;
                            //btnTxt += '<tr><td colspan="2"><a name="' + hdr + '" id="' + id + '" url="' + href + '" class="btn btn-mini btn-info ' + mainClass + ' " href="#"><i class="icon icon-white icon-ok"></i></a></td></tr></table>';
                            //child += '<div class="imgGallery"><img class="media-object" data-src="holder.js/64x64" alt="' + text + '" src="data:image/png;base64,' + bynary + '" /><div class="media-body ' + txtCls + '">' + text + '</div><div class="media-body">' + btnTxt + '</div></div>';

                            btnTxt += '<a href="#" name="' + text + '" id="' + id + '" url="' + href + '" role="button" ' + cls + ' data-toggle="modal">';

                            bynary = bynary == '' || bynary == null ? noImg : bynary;
                            child += '<div class="imgGallery">' + btnTxt + '<img class="media-object" data-src="holder.js/64x64" alt="' + text + '" src="data:image/png;base64,' + bynary + '" /><div class="media-body ' + txtCls + '">' + text + '</div></a></div>';

                        });
                    }
                });

                obj.append('<div class="imgGalleryGrp ' + txtCls + '" style="text-align:center;"><h2 class="media-heading" style=" padding-top:15px;">' + grp + '</h2></div>');
                obj.append(child);                
            });

            obj.append('<div class="span8"><a name="' + hdr + '" url="' + href + '" class="btn btn-large btn-warning ' + mainClass + ' " href="#">Booking</a></div>');
        },
        error: function (x, e) {
            alert('error');
        }
    }); //end ajax call
}