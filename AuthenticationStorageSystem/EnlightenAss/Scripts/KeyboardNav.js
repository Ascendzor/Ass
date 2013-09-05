// JavaScript source code


var counter = 0;
    function initialize() {
        counter = 0;
        var initalSelection = returnId(counter);
        if (initalSelection != null)
            initalSelection.bgColor = "blue";

    }

    

function returnId(tempCounter) {
    return document.getElementById(String(tempCounter));
}



function changeStyle(x) {
    var temp = returnId(counter);
    temp.bgColor = "white";
    console.log(counter);
    counter = counter + x;
    var temp2 = returnId(counter);
    temp2.bgColor = "blue";
    

}

function handleKeyPressed(e) {
    //up key =38
    //down key=40
    //enter =13
    //key.Code

    switch(e.keyCode)
    {
        case 13://enter
            var loadElement = returnId(String(counter));
            if (loadElement != null) {
                loadElement.click();
            }
            break;
        case 38://up key
            if (counter > 0) {
                
                changeStyle(-1);
            }
            break;
        case 40://downkey
            if (returnId(String(counter + 1)) != null) {
                changeStyle(1);
            }
            break;
    }
    var element = returnId(counter);
}

function printElement(element) {
    console.log(element);
    console.log(element.id);
        
}

window.onload = initialize;
document.onkeydown = handleKeyPressed;
