// JavaScript source code


var counter = 0;
    function initialize() {
        counter = 0;
        if(returnId(0)!= null)
            changeStyle(0);
    }

    

function returnId(tempCounter) {
    return document.getElementById(String(tempCounter));
}



function changeStyle(x) {
    var temp = returnId(counter);
    temp.bgColor = "white";
    
    counter = counter + x;
    var temp2 = returnId(counter);

    temp2.bgColor = "#0099FF";
    

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
            e.preventDefault();
            break;
        case 40://downkey
            if (returnId(String(counter + 1)) != null) {
                changeStyle(1);
                
            }
            
            e.preventDefault();
            break;
    }
    var element = returnId(counter);
}

function mousedOver(newCounter) {
    changeStyle(newCounter - counter);
    printElement(counter);
}

function printElement(element) {
    console.log(element);
    console.log(element.id);
        
}

window.onload = initialize;
document.onkeydown = handleKeyPressed;
