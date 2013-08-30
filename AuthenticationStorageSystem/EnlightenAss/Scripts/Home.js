function search() {
    $.ajax({
        url: '/Home/Search',
        data: { searchText: document.getElementById("searchText").value },
        dataType: 'html',
        success: function (data) {
            $('#partialDiv').html(data);
        }
    });
}

function goBack() {
    history.back();
}

function requestSearch(element) {
	$.ajax({
		url: '/Search/Results',
		data: { searchText: document.getElementById(element).value },
		dataType: 'html',
		success: function (data) {
			$('#partialDiv').html(data);
		}
	});
}

function requestItem(item, url) {
	$.ajax({
		url: url,
		data: { id: item },
		dataType: 'html',
		success: function (data) {
			$('#partialDiv').html(data);
		}
	});
}