// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function doTestService() {
    var url = $("#url").val();
    var method = $("#method").val();
    var jwtToken = getJwtToken();
    //console.log(token);
    var jqxhr = $.ajax({
        beforeSend: function (xhr, settings) { xhr.setRequestHeader('Authorization', 'Bearer ' + jwtToken); },
        method: method,
        url: url,
        xhrFields: {
            withCredentials: true
        },
        success: function (result) {
            //debugger;
            $("#result").html(JSON.stringify(result));
        },
        error: function (response, status, error) {
            //if (response.status === 401) {// && response.headers.has('Token-Expired')
            //    debugger;
            //    doRefreshToken(function (refreshResponse) {
            //        if (refreshResponse.status === 200) {
            //            doTestService();//repeat the original request
            //        } else {
            //            //return response; //failed to refresh so return original 401 response
            //            $("#result").html(JSON.stringify(refreshResponse.status + ':' + refreshResponse.statusText));
            //            return response;
            //        }

            //    });

            //} else { //status is not 401 and/or there's no Token-Expired header
            //    // return response; //return the original 401 response

            //    $("#result").html(JSON.stringify(response.status + ':' + response.statusText));
            //}
        }
    })
    //this section is executed when the server responds with error
    jqxhr.fail(function (response, status, textStatus) {
        //debugger;
        if (response.status === 401) {// && jqxhr.getResponseHeader('Token-Expired')) {
            doRefreshToken(function (refreshResponse) {
                if (refreshResponse.status === 200) {
                    doTestService();//repeat the original request
                } else {//status is not 401 and/or there's no Token-Expired header
                    //return response; //failed to refresh so return original 401 response
                    $("#result").html(JSON.stringify(refreshResponse.status + ':' + refreshResponse.statusText));
                    return response;
                }

            });
        } else {
            $("#result").html(JSON.stringify(response.status + ':' + response.statusText));
        }
    })
}
function doRefreshToken(callback) {

    var jwtToken = getJwtToken();
    var refreshToken = getRefreshToken();

    $.ajax({
        method: 'Get',
        url: '/account/RefreshToken?token=' + jwtToken + '&refreshToken=' + refreshToken,
        success: function (result) {
            displayToken();
            if (typeof callback === "function") {
                callback(result)
            }
        },
        error: function (response, status, error) {
            displayToken();
            if (typeof callback === "function") {

                callback(response)
            }
        }
    })
}

function displayToken() {
    $("#token").html(getCookie("token"))
    $("#refreshtoken").html(getCookie("refresh-token"))
}

function getJwtToken() {
    return getCookie("token");
    //return localStorage.getItem('token');
}

function getRefreshToken() {
    return getCookie("refresh-token")
    //return localStorage.getItem('refreshToken');
}

function saveJwtToken(token) {
    localStorage.setItem('token', token);
}

function saveRefreshToken(refreshToken) {
    localStorage.setItem('refreshToken', refreshToken);
}