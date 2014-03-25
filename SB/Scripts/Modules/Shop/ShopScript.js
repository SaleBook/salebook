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
            var fbdata = new Array();
            var pageidList = new Array();
            var pageNameList = new Array();
            var pageUrlList = new Array();
            var pageImageList = new Array();

            fbdata[0] = response.authResponse.userID;
            FB.api('/me', function (data) {
                fbdata[1] = data.name;
                fbdata[2] = data.link;
                SetLoginByFacebook(fbdata[0], fbdata[1], fbdata[2], '');
            })

            var count1 = 0;
            var count2 = 0;

            FB.api('/me/accounts', function (response) {
                var data = response['data'];
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        pageidList[i] = data[i].id;
                        var str = "<div id='divPage" + i + "' class='page-list' >" +
                                   "<img id='pageImg" + i + "' alt='' width='70' height='70' />" +
                                   "<a id='pageName" + i + "' ></a>" +
                                   "<input id='pageLogin" + i + "' type='button' value='เข้าสู่ระบบ'/>" +
                                   "</div>";
                        $('#divPageList').html(str);
                        $('#divPage' + i).hide();

                        var count1 = i;
                        FB.api('/' + pageidList[count1], function (response) {
                            pageNameList[count1] = response.name;
                            pageUrlList[count1] = response.link;
                            $('#pageName' + count1).text(pageNameList[count1]);
                            $('#pageName' + count1).attr('href', pageUrlList[count1]);

                            var count2 = count1;
                            FB.api('/' + pageidList[count2] + '/picture',
                                {
                                    "redirect": false,
                                    "height": "200",
                                    "type": "normal",
                                    "width": "200"
                                },
                                function (response) {
                                    if (response && !response.error) {
                                        var data = response.data
                                        pageImageList[count2] = data.url;
                                        $('#pageImg' + count2).attr('src', pageImageList[count2]);
                                    }

                                    $('#pageLogin' + count2).click(function () {
                                        SetPageData(pageidList[count2], pageNameList[count2], pageUrlList[count2], pageImageList[count2]);
                                    });
                                    $('#divPage' + count2).show();
                                }
                            );

                        });


                    }
                }
                else {
                    var str = "<div" +
                              "<lable>ไม่พบข้อมูล page</lable>" +
                              "</div>";
                    $('#divPageList').html(str);
                }

            });

            $('#linkLogout').show();
        } else if (response.status === 'not_authorized') {
            $('#linkLogout').hide();
            //            FB.login();
        } else {
            $('#linkLogout').hide();
            //            FB.login();
        }
    });

};

function SetLoginByFacebook(id, name, url, image) {
    $.post('Account/SetLoginByFacebook', { id: id, name: name, url: url, image: image });
}

function SetPageData(id, name, url, image) {
    $.post('Shop/SetPageData', { id: id, name: name, url: url, image: image }, function (data) {

        if (data != "") {
            window.location = data
        }
        else {
            alert("ระบบเกิดข้อผิดพลาด");
        }
    });
}

function SetLogout() {
    FB.logout(function () { document.location.reload(); });
    $.post('Account/SetLogout');
}

