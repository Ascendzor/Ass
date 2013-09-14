/*
 * Keyboard navigation up and down table
 * Table rows must have incremeneting IDs
 * Enter invokes the selected elements onclick function
 */

// Global counter to identify what row is seleceted
var counter = 0;

/*
 * Function is invoked when a partial view is loaded onto the page
 * (from within the view html)
 */
function initialize() {
    $("#" + (counter = 0)).addClass("selectedRow");
}

/*
 * Remove selected class from the row being unselected
 * Add selected class to the row being selected
 * Parameter 'x' will equal 1 to go up, -1 to go down
 */
function changeStyle(x) {
    $("#" + counter).removeClass("selectedRow");
    counter += x;
    $("#" + counter).addClass("selectedRow");
}

/**
 * up key   = 38 = changeStyle() + scroll window
 * down key = 40 = changeStyle() + scroll window
 * enter    = 13 = call onclick function of selected row
 */
function handleKeyPressed(e) {

    switch(e.keyCode)
    {
        case 13:
            $("#" + counter).click();
            break;
        case 38:
            if (counter > 0) {
                changeStyle(-1);
                window.scrollBy(0, $("#" + counter).offsetHeight * -1);
            }
            e.preventDefault();
            break;
        case 40:
            if (document.getElementById(counter + 1) != null) {
                changeStyle(1);
                window.scrollBy(0, $("#" + counter).offsetHeight * 1);
            }
            e.preventDefault();
            break;
    }
}

document.onkeydown = handleKeyPressed;
