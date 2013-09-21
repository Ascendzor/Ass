﻿/* focus on search box on load */
$(document).ready(function () {
    $("#searchText").focus();
});

/* Search database without page reload */
function search() {

    delay(function () {                             // delay to throttle number of database queries
        $.ajax({
            url: '/Home/Search',
            data: { searchText: document.getElementById("searchText").value },
            dataType: 'html',
            error: function (xhr, status, error) {
                //do something about the error
                console.log("ERROR\nSomething went wrong with the search\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
                data = "Something went wrong with the search\n" + xhr + "\n" + status + "\n" + error;
            },
            success: function (data) {              // on success replace the html inside partialDiv with the returned partial view
                $('#partialDiv').html(data);
            }
        });
    }, 200);
}

function goBack() {
    history.back();
}

/* Request partial view and display it without page reload */
function requestItem(item, url) {

    $.ajax({
        url: url,                                   // url to controller action
        data: { id: item },                         // id of the client/project/index selected
        dataType: 'html',
        error: function (xhr, status, error) {
            //do something about the error
            console.log("ERROR\nSomething went wrong with the request\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
            data = "Something went wrong with the request\n" + xhr + "\n" + status + "\n" + error;
        },
        success: function (data) {                  // on success replace the html inside partialDiv with the returned partial view
            $("#searchText").focus();
            $('#partialDiv').html(data);
        }
    });
}

/* Submit form without page reload */
function ajaxSubmitForm(btnClicked) {
    var $form = $(btnClicked).parents('form');

    $.ajax({
        type: "POST",
        url: $form.attr('action'),                  // the forms action which will map to a controller method
        data: $form.serialize(),
        error: function (xhr, status, error) {
            //do something about the error 
            console.log("ERROR\nSomething went wrong with the form submit\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
            data = "Something went wrong with the form submit\n" + xhr + "\n" + status + "\n" + error;
        },
        success: function (data) {                  // on success replace the html inside partialDiv with the returned partial view
            $('#partialDiv').html(data);
        }
    });
}

/* Function to throttle the ajax calls to database when typing in search box */
var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();


/**
 * Hide or show entry table rows based off traffic light checkboxes
 * Switch traffic light image to light on and off 
 * id = checkbox element id, testing, staging and development
 * Table rows for entries class name = testing or staging or development
 */
function filterByDevState(id) {
    if ($("#" + id).is(":checked")) {
        $("." + id).show();
        $("#" + id + "LightOn").show();
        $("#" + id + "LightOff").hide();
    }
    else {
        $("." + id).hide();
        $("#" + id + "LightOn").hide();
        $("#" + id + "LightOff").show();
    }
}