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
    counter = parseInt($("td:first").parent("tr:first").attr('id'));        // Get first table row
    if (isNaN(counter)) counter = 0;                                        // If empty table
    $("#" + (counter)).addClass("selectedRow");
}

/*
 * Remove selected class from the row being unselected
 * Add selected class to the row being selected
 * Parameter 'x' will equal 1 to go up, -1 to go down
 */
function changeStyle(x, oldSelected) {
    $("#" + oldSelected).removeClass("selectedRow");
    counter += x;
    $("#" + counter).addClass("selectedRow");
}

/**
 * up key   = 38 = changeStyle() + scroll window
 * down key = 40 = changeStyle() + scroll window
 * enter    = 13 = call onclick function of selected row
 */
function handleKeyPressed(e) {
    var oldSelected = counter;

    switch (e.keyCode) {
        case 13:
        case 39:
            $("#" + counter).click();
            break;

        case 38:
            if (counter > 0) {
                var tempCounter = counter;                                  // Store counter incase reach null row
                while ($('#' + (counter - 1)).css('display') == 'none') {   // Check if next row is hidden
                    counter--;                                              // If hidden skip it
                    if (document.getElementById(counter - 1) == null) {     // If next row is null revert back to old row
                        counter = tempCounter + 1;                          // and break
                        break;
                    }
                }
                changeStyle(-1, oldSelected);

                // Only scroll up when selected element is near top of page
                if ($("#" + counter).offset().top - $(window).scrollTop() < (window.innerHeight / 4))
                    window.scrollBy(0, document.getElementById(counter).offsetHeight * -1);
                scrollIntoView();
                
            }
            e.preventDefault();
            break;

        case 40:
            if (document.getElementById(counter + 1) != null) {             // Store counter incase reach null row
                var tempCounter = counter;                                  // Check if next row is hidden
                while ($('#' + (counter+1)).css('display') == 'none') {     // If hidden skip it
                    counter++;                                              // If next row is null revert back to old row
                    if (document.getElementById(counter + 1) == null) {     // and break
                        counter = tempCounter-1;
                        break;
                    }
                }
                changeStyle(+1, oldSelected);
                
                // Only scroll down when selected element is near bottom of page
                if ($("#" + counter).offset().top - $(window).scrollTop() > (window.innerHeight - (window.innerHeight / 4)))
                    window.scrollBy(0, document.getElementById(counter).offsetHeight * 1);
                scrollIntoView();
            }
            e.preventDefault();
            break;

        case 37:
            console.log("yes");
            $("#upDirButton").click();
            break;
    }
}

/**
 * If selected element is outside of view scroll to it
 */
function scrollIntoView() {
    var docViewTop = $(window).scrollTop();
    var docViewBottom = docViewTop + $(window).height();

    var offset = $("#" + counter).offset().top;

    if (!((offset <= docViewBottom) && (offset >= docViewTop)))
        $(window).scrollTop($("#" + counter).position().top - (window.innerHeight / 2));
}


document.onkeydown = handleKeyPressed;
