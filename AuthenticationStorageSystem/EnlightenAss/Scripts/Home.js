﻿/* Search database without page reload */
function search() {
    delay(function () {
        $('body').css('cursor', 'progress');
        $.ajax({
            url: '/Home/Search',
            data: { searchText: document.getElementById("searchText").value },
            dataType: 'html',
            error: function (xhr, status, error) {
                //do something about the error
                $('body').css('cursor', 'default');
            },
            success: function (data) {
                $('#partialDiv').html(data);
                $('body').css('cursor', 'default');
            }
        });
    }, 200);
}

function goBack() {
    history.back();
}

/* Request partial view and display it without page reload */
function requestItem(item, url) {
    $('body').css('cursor', 'progress');

    $.ajax({
        url: url,
        data: { id: item },
        dataType: 'html',
        error: function (xhr, status, error) {
            //do something about the error
            $('body').css('cursor', 'default');
        },
        success: function (data) {
            $('#partialDiv').html(data);
            $('body').css('cursor', 'default');
        }
    });
}

/* Submit form without page reload */
function ajaxSubmitForm(btnClicked) {
    var $form = $(btnClicked).parents('form');
    $('body').css('cursor', 'progress');

    $.ajax({
        type: "POST",
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            //do something about the error
            $('body').css('cursor', 'default');
            alert("ajaxSubmitForm errored ", xhr, "\n", status, "\n", error);
        },
        success: function (data) {
            $('#partialDiv').html(data);
            $('body').css('cursor', 'default');
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

