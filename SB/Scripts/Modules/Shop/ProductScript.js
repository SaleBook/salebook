function InitData() {

    var dd = new DropDown($('#menu'));
    dd.setValue("menuProduct");
    dd.initEvents();

    // jquery event
   
    $("#btnProductFacbook").click(function () {
        btnProductFacbook_click();
    });

    $("#btnSDSync").click(function () {
        btnSDSync_click();
    });

    $("#btnSDCancel").click(function () {
        btnSDCancel_click();
    });

    $("#chkDSAll").change(function () {
        chkDSAll_change();
    });
    
}

function btnProductFacbook_click() {
    $('#divSysfbDialog').bPopup();
}

function btnSDSync_click() {
    
}

function btnSDCancel_click() {
    $("#divSysfbDialog").bPopup().close();
}

function chkDSAll_change() {
    CheckAll('#chkDSAll', 'chkDS');
}

function CheckAll(chk, group) {
    if ($(chk).is(':checked')) {
        $(':input[id*=' + group + ']').each(function () {
            $(this).prop('checked', true); //Command For Jquery 1.6+
        });
    }
    else {
        $(':input[id*=' + group + ']').each(function () {
            $(this).prop('checked', false);
        });
    }
}

function parseISO8601(date_str) {
    var time = ('' + date_str).replace(/-/g, "/").replace(/[TZ]/g, " ");
    var date = new Date(time);
    return date;
}

window.fbAsyncInit = function () {
    FB.init({
        appId: '498664966918205',
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true  // parse XFBML
    });

    FB.Event.subscribe('auth.authResponseChange', function (response) {
        // Here we specify what we do with the response anytime this event occurs. 
        if (response.status === 'connected') {
            // prepare data fb
            $.post('GetPageData', function (data) {
                var pageId = data.FanPageID;
                var AlbumIdList = new Array();
                var AlbumNameList = new Array();
                var AlbumDateList = new Array();

                FB.api(
                        "/" + pageId + "/albums",
                        function (response) {
                            if (response && !response.error) {
                                var data = response.data;
                                for (var i = 0; i < data.length; i++) {
                                    var date = parseISO8601(data[i].updated_time);
                                    AlbumDateList[i] = date;
                                    AlbumIdList[i] = data[i].id;
                                    AlbumNameList[i] = data[i].name;
                                    for (var j = i; j > 0; j--) {
                                        if (AlbumDateList[j] > AlbumDateList[j - 1]) {

                                            var tmp1 = AlbumDateList[j];
                                            AlbumDateList[j] = AlbumDateList[j - 1];
                                            AlbumDateList[j - 1] = tmp1;

                                            var tmp2 = AlbumIdList[j];
                                            AlbumIdList[j] = AlbumIdList[j - 1];
                                            AlbumIdList[j - 1] = tmp2;

                                            var tmp3 = AlbumNameList[j];
                                            AlbumNameList[j] = AlbumNameList[j - 1];
                                            AlbumNameList[j - 1] = tmp3;

                                        }
                                    }
                                }

                                // set html
                                for (var i = 1; i <= AlbumIdList.length; i++) {
                                    var html = "<div><input type='checkbox' id='chkDS" + AlbumIdList[i] + "' value='" + AlbumIdList[i] + "' />" + AlbumNameList[i] + "</div>";
                                    if (i % 3 == 1) {
                                        $('#divDSMain1').append(html);
                                    }
                                    else if (i % 3 == 2) {
                                        $('#divDSMain2').append(html);
                                    }
                                    else if (i % 3 == 0) {
                                        $('#divDSMain3').append(html);
                                    }
                                }
                            }
                        }
                );
            });

        }
    });

};