function search() {
    $.ajax({
        url: '/Home/Search',
        data: { searchText: document.getElementById("searchText").value },
        dataType: 'html',
        success: function (data) {
            $('#partialSearchResults').html(data);
        }
    });
}