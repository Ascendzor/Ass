function leHandle(response) {
    console.log("entered leHandle(response)");
    console.log(response);
}

function main() {
    console.log("entered main()");
    var request = new XMLHttpRequest();

    request.open("POST", "testASP.aspx", true);

    request.onreadystatechange = function () {
        if (request.readyState == 4) {
            if (request.status == 200) {
                leHandle(request.responseText);
            }
        }
    };
    request.send("abcd");
}