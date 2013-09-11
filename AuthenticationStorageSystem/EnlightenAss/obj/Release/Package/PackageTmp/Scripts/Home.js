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
                $('body').css('cursor', 'default');
            },
            success: function (data) {
                $('#partialDiv').html(data);
                //onPrtialLoad();
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
            $("#searchText").focus();
            $('#partialDiv').html(data);
            //onPrtialLoad()
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
            //onPrtialLoad()
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

/* Toggle manage mode (hide/unhide buttons) */
function toggleManage() {
    if (!$('#manageToggle').is(":checked")) {
        console.log("on-off");
        $('#editLabel').text("Edit Mode off");
        $('.editButton').css({ 'display': 'none' });
    } else {
        console.log("off-on");
        $('#editLabel').text("Edit Mode on");
        $('.editButton').css({ 'display': '' });
    }
    
}

/* When partial view is loaded check whether edit mode is enabled */
function onPrtialLoad() {
    if (!$('#manageToggle').is(":checked")) {
        $('.editButton').css({ 'display': 'none' });
    } else {
        $('.editButton').css({ 'display': '' });
    }
}
