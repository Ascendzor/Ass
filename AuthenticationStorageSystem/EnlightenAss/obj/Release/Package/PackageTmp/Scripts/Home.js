/* focus on search box on load */
$(document).ready(function () {
    $("#searchText").focus();
});

/* Search database without page reload */
function search() {
    //throttle
    delay(function () {
        $.ajax({
            url: '/Home/Search',
            data: { searchText: document.getElementById("searchText").value },
            dataType: 'html',
            error: function (xhr, status, error) {
                //do something about the error
                console.log("ERROR\nSomething went wrong with the search\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
                data = "Something went wrong with the search\n" + xhr + "\n" + status + "\n" + error;
            },
            success: function (data) {
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
        url: url,
        data: { id: item },
        dataType: 'html',
        error: function (xhr, status, error) {
            //do something about the error
            console.log("ERROR\nSomething went wrong with the request\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
            data = "Something went wrong with the request\n" + xhr + "\n" + status + "\n" + error;
        },
        success: function (data) {
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
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            //do something about the error
            console.log("ERROR\nSomething went wrong with the form submit\nXHR=" + xhr + "\nStatus=" + status + "\nError=" + error);
            data = "Something went wrong with the form submit\n" + xhr + "\n" + status + "\n" + error;
        },
        success: function (data) {
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


/* Hide or show entry table rows based off traffic light filter */
function filterByDevState(id) {
    if ($("#" + id).is(":checked"))
        $("." + id).show();
    else
        $("." + id).hide();
}
