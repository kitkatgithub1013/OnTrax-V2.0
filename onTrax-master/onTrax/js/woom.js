// Global variables to track production runs
var click_count = 0;
var jobs = 0;
var person = null;
var status = "checked-out";
var state = "stopped";

// Modal control
// When a modal option is selected, close the modal window
$('#employee-list input').on('change', function () {
    
    if ($('input[name=EmployeeID]:checked', '#employee-list').val() != null) {
        $("#employeeName").html($('input[name=employee-name]:checked', '#employee-list').val());
    }
});

//Check-in logic 
$("#form-check-in").click(function () {

    person = $('input[name=employee-name]:checked', '#employee-list').val();

    // Get the selected employee ID and the PIN typed into the text field 
    //Send PIN and ID to TechController for validation 
    EmployeeID = $("#Employee_EmployeeID").val();
    PIN = $("#PIN").val();
    $.get(
        {
            EmployeeID: EmployeeID,
            PIN: PIN
        }
    )
});

//Check-out logic
$("#checkout").click(function () {
    status = "checked-out";
    //Reset all current values 
    person = null;
    jobs = null;
    $('#employee-list').prop('selectedIndex', 0);
    $("#woomForm").hide();
    $("#completed").hide();
    $("#checkForm").fadeIn();
    resetSelections();
    resetIssues();
    resetStart();
    resetMessages();
});

/* Variables for DURATION CALCULATION */
var finalStart = null;
var finalStop = null;
var resume = null;
var duration = 0;
var paused = false;
var StartTime = null;  // Declared in global context so Stop function can access it

// Onclick logic for start button 
$("#start").click(function () {
    //Error messages to tell user which requried fields have not been selected
    if ($('input[name=product]:checked').val() == null && $('input[name=process]:checked').val() == null && $('input[name=batch]:checked').val() == null) {
        alert("Select Process, Product, and Batch")
    }
    else if ($('input[name=product]:checked').val() == null && $('input[name=process]:checked').val() == null) {
        alert("Select Process and Product")
    }
    else if ($('input[name=product]:checked').val() == null && $('input[name=batch]:checked').val() == null) {
        alert("Select Product and Batch")
    }
    else if ($('input[name=process]:checked').val() == null && $('input[name=batch]:checked').val() == null) {
        alert("Select Process and Batch")
    }
    else if ($('input[name=product]:checked').val() == null) {
        alert("Select Product")
    }
    else if ($('input[name=process]:checked').val() == null) {
        alert("Select Process")
    }
    else if ($('input[name=batch]:checked').val() == null) {
        alert("Select Batch")
    }
    else if ($('#Quantity').val() < 1) {
        alert("Quantity must be at least 1")
    }     
    else
    {
        // Process has started
        // Close Product & Process when clicking Start
        $("div[role='tabpanel']").removeClass("active show");
        $("a[role='tab']").removeClass("active show");
        $("#collapse").removeClass("disabled");
        $(this).toggleClass("bg-primary");
        $(this).toggleClass("bg-success");
        $("#stop").prop("disabled", false);
        $("#start h1").toggle();
        $("#completed").hide();
        $("#cancel").show();
        $("#cancel-msg").hide();
        $("#issue-form").hide();
        $("#flag").show();
        $('#stat-msg').show();
        $("#start-msg btn").toggle();
        $('#complete-msg').hide();
        $('#commands').show();
        // Set to "Resume" so it's displayed when paused
        $("#start-text").html("Resume");

        startText(); 
        //Set StartTime (finalStart) and calculate duration when paused
        //When start is first tapped and production is begun
        if (finalStart == null) {
            finalStart = Date.now();
        }
        //When pause is tapped the first time during production 
        else if (resume == null && !paused) {
            duration += Date.now() - finalStart;
            paused = true;
        }
        //When resume is tapped and timing resumes
        else if (paused) {
            resume = Date.now();
            paused = false
        }
        //When pause is tapped subsequent times (if the technician pauses 2 or more times)
        else if (!paused)
        {
            duration += Date.now() - resume
            paused = true
        }
    }

    //Assemble datetime string for StartTime 
    //Format should be YYYY:MM:dd hh:mm:ss

    if (StartTime == null) {
        StartTime = new Date();
        // Calculate StartTime for finalStart
        var DD = StartTime.getDate();
        var MM = StartTime.getMonth() + 1; //Janaury is 0
        var YYYY = StartTime.getFullYear();
        var hh = StartTime.getHours();
        var mm = StartTime.getMinutes();
        var ss = StartTime.getSeconds();
        //Values for month, day, hour, minute, and second must always be two characters long to avoid error in TechController
        if (DD < 10) {
            DD = '0' + DD
        }
        if (MM < 10) {
            MM = '0' + MM
        }
        if (mm < 10) {
            mm = '0' + mm;
        }
        if (ss < 10) {
            ss = '0' + ss;
            if (ss == 0) {
                ss = '00';
            }
        }
        if (hh < 10) {
            hh = '0' + hh;
            if (hh == 0) {
                hh = '00';
            }
        }
        StartTime = YYYY + '/' + MM + '/' + DD + ' ' + hh + ':' + mm + ':' + ss;   
    }

    if ($("#collapse").hasClass("disabled")) {
        $("#collapse").removeClass("disabled");
    }    
    if ($("#collapse").is(':visible')) {
        $("#collapse").hide();        
    }
});

//Stop button logic for entering data into DB 

$("#stop").click(function () {
    // UI display logic
    $('#complete-msg').show();

    finalStop = Date.now();
    if (resume == null) {
        duration += finalStop - finalStart;
    }
    else
    {
        duration += Date.now() - resume
    }

    // Calculate EndTime for finalStop
    // Assemble datetime string into format YYYY:MM:dd hh:mm:ss
    var EndTime = new Date();
    var DD = EndTime.getDate();
    var MM = EndTime.getMonth() + 1; //Janaury is 0
    var YYYY = EndTime.getFullYear();
    var hh = EndTime.getHours();
    var mm = EndTime.getMinutes();
    var ss = EndTime.getSeconds();

    //Values for month, day, hour, minute, and second must always be two characters long to avoid error in TechController
    if (DD < 10) {
        DD = '0' + DD
    }
    if (MM < 10) {
        MM = '0' + MM
    }
    if (mm < 10) {
        mm = '0' + mm;
        if (mm == 0) {
            mm = '00';
        }
    }
    if (ss < 10) {
        ss = '0' + ss;
        if (ss == 0) {
            ss = '00';
        }
    }
    if (hh < 10) {
        hh = '0' + hh;
        if (hh == 0) {
            hh = '00';
        }
    }
    EndTime = YYYY + '/' + MM + '/' + DD + ' ' + hh + ':' + mm + ':' + ss;   

    // Convert duration from miliseconds into minutes
    duration = duration / 60000
  
    // Increment job count for the day
    jobs = jobs + 1;
    $("#job-count").html(jobs);

    // Run stop functionality to clean up UI elements
    stop();

    // Builds variables based on form inputs for the controller
    BatchID = $('input[name=batch]:checked').val();
    ProductID = $('input[name=product]:checked').val();
    ProcessID = $('input[name=process]:checked').val();
    EmployeeID = $("#Employee_EmployeeID").val();
    Quantity = $('#Quantity').val();

    // Get array of all issues checked; if none are checked an empty array will be sent to TechController, where it will be ignored
    Issues = new Array();
    $('#issues input[type=checkbox]').each(function () {
        if ($(this).is(':checked')) {
            Issues.push($(this).val());
        }
    })

    // Posts to the controller - inserting values into the DB onClick of Stop
    $.post('/Production/CreateProduction',
        {
            BatchID: BatchID,
            ProductID: ProductID,
            ProcessID: ProcessID,
            EmployeeID: EmployeeID,
            Duration: duration,
            Quantity: Quantity,
            StartTime: StartTime,
            EndTime: EndTime,
            Issues: Issues
        });

    // Reset the variables for next prodcution run
    duration = 0
    finalStart = null
    finalStop = null
    resume = null
    $('#Quantity').val('0');
    StartTime = null;
    resetIssues();
});


// Display issue element when flag button is tapped
$("#flag").click(function(){

    $("#issue-form").fadeIn();
    $('#commands').hide();

});



// Hide and reset issues if Cancel Issues is tapped
$("#issue-cancel").click(function () {

    startText();

    $('input[name="issue_handle_bars"]').prop('checked', false);
    $("#issue-form").hide();
    $("#issue-confirm").show();
    $('#commands').show();

    resetIssues();
});

// Cancel production run and reset all fields; ask user if they are sure 
$("#cancel").click(function () {
    result = confirm('Are you sure you want to cancel this production run?')
    if (result) {

        stop();

        $("#completed").hide();
        $("#cancel-msg").show();
        $('#Quantity').val('0');
        duration = 0
        StartTime = null;
        resetIssues();
    }
});

//Update issue message and hide issue elements when Save Issues is tapped
$("#issue-submit").click(function () {

    $("#issue-form").hide();
    $("#issue-confirm").fadeIn();
    $('#commands').show();

    // If issue count = 0, we don't need to display count
    var n = $("#issues input:checked").length;
    if (n < 1) {
        resetIssues();
    }
});    

// Tab option selected, proceed to next tab 
// UI flow of menu bar as fields are selected
$('#options-tabContent .selector').click(function () {
    // next tab's index
    // true index is 0, first child index is 1 so we need to +2
    var nextIndex = $(this).parent().parent().index() + 2;
    $('#options-tab li:nth-child(' + nextIndex + ' ) a').tab("show");
});

// Collapse all tabs on click of active tab
$("#options-tab").click(function(){
    if ($("#collapse").is(':hidden') && !$("#collapse").hasClass("disabled")) {
        $("#collapse").fadeIn();
    }
});

// Collapse all tabs when Collapse button is tapped
$("#collapse").on("click", function () {
    $("div[role='tabpanel']").removeClass("active show");
    $("a[role='tab']").removeClass("active show");
    $(this).fadeOut();
});

// Change background color and start button text when Start is tapped
function startText(){
    if ($('#pause-text').is(':visible')) {
        $("#pause-msg").hide();
        $("#start-msg").show();
        $("body").css('background-image', 'inherit');
        state = "started";
    }
    else {
        pauseTracker();
    }
}

// Stop function cleans up UI elements when stopped
function stop() {

    $("#completed").show();
    $("#cancel").hide();
    $("#flag").hide();
    $("#issue-form").hide();
    $("#start-msg").hide();
    $("#pause-msg").hide();
    $("#cancel-msg").hide();
    $("#issue-confirm").hide();
    $("body").css('background-image', 'inherit');
    $("#stop").prop("disabled", true);

    // Activate Process Tab
    /*$("#options-process-tab").tab("show");*/
    $("#options-quantity-tab").tab("show");
    // Remove collapse button if it is visible

    if ($("#collapse").is(':visible')) {
        $("#collapse").hide();        
    }

    // Make sure the options cannot be collapsed when "stopped"
    if (!$("#collapse").hasClass("disabled")) {
        $("#collapse").addClass("disabled");
    }
    resetStart();
    state = "stopped";
}
//TODO: find out what this does
function disableStart() {
    if ($('#product-list').val() != null && $('#process-list').val() != null) {
        $("#start").prop("disabled", false);
    }
}

// Update text of Save/Cancel issue buttons depending on how many issues are selected
var countChecked = function () {
    var n = $("#issues input:checked").length;
    $("#issue-submit").text("Save " + n + (n == 1 ? " Issue" : " Issues"));
    $("#issue-cancel").text("Cancel " + n + (n == 1 ? " Issue" : " Issues"));
    $("#issue-confirm").text(n + (n == 1 ? " Issue" : " Issues") + " Flagged");
};

// Reset issues (uncheck all) when Cancel Issues is tapped
$("#issue-cancel").click(function () {
    startText();
    resetIssues();
});

// Uncheck all issues and set issue label to inactive 
function resetIssues() {
    $("#issues .selector").prop('checked', false);
    $("#issues label").removeClass("ui-state-active");
    $("#issue-confirm").hide();
    countChecked();
}

// Reset all field selections (uncheck all values)
function resetSelections() {
    $('input[name="process"]').prop('checked', false);
    $('input[name="product"]').prop('checked', false);
    $('input[name="batch"]').prop('checked', false);
    $('input[name="batch"]').button("refresh");
    $('input[name="product"]').button("refresh");
    $('input[name="process"]').button("refresh");
}

//  Hide alert message element 
function resetMessages() {
    $("#messages .alert").hide();
}

// Reset start button 
// Change text back to Start from Pause and change color to green (bg-success) from blue (bg-primary)
function resetStart() {
    $("#start").addClass("bg-success");
    $("#start").removeClass("bg-primary");
    $("#pause-text").hide();
    $("#start-text").show();
    $("#start-text").html("Start");
}

// Function for when Pause is tapped; change background and bottom message text to indicate that tracker is paused
function pauseTracker() {
    $("#pause-msg").show();
    $("#start-msg").hide();
    $("body").css("background-image", "linear-gradient(rgb(217, 219, 220), rgb(255, 255, 255), rgb(217, 219, 220))");
    state = "paused";
}

// When save button is clicked, get value of all text boxes and send them to controlled in a dictionary 
$("#save-std-duration").click(function () {
    var standardsDict = new Object();
    $('.input').each(function () {
        standardsDict[$(this).attr('id')] = $(this).val();
    });

    // Posts to the controller - send dictionary to controller for updating of rows in DB 
    $.post('/Admin/Standards/Edit',
        {
            standardsDict: standardsDict
        });

    location.reload();
});