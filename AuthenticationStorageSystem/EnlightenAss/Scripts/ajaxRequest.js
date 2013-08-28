function ajaxRequest(url, data, success) {
    $.ajax({
        url: url,
        data: data,
        dataType: 'html',
        success: success
    });
}