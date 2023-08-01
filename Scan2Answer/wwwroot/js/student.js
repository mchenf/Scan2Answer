"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/teacherHub").build();

var btnReadyState = 0;
var btnSubmit = document.getElementById("btnSubmit");

connection.start().then(function () {
    connection.invoke("ReportIn").catch(function (err) {
        return console.error(err.toString());
    });
    btnReadyState = btnReadyState | 1;
    if (btnReadyState === 3) {
        btnSubmit.disabled = false;
    }
}).catch(function (err) {
    return console.error(err.toString());
});



function submitAnswer() {
    var fdata = new FormData(document.forms[0]);
    var answerId = fdata.get("opts") | 0;
    console.info("Answer submitted..." + answerId);
    connection.invoke("Answer", answerId)
    btnSubmit.disabled = true;
    btnReadyState = btnReadyState & ~1 //disable submit for this student
    var radioBtns = document.querySelectorAll("label");
    for (var i = 0; i < radioBtns.length; i++) {
        radioBtns[i].disabled = true;
    }
}


// Happens when the option label is clicked
function rdbtnChecked() {
    console.info("radio button is clicked...")
    btnReadyState = btnReadyState | 2;
    if (btnReadyState === 3) {
        btnSubmit.disabled = false;
    }
    var fdata = new FormData(document.forms[0]);
    var answerId = fdata.get("opts") | 0;
    console.info("radio button clicked...");
    console.info("Answered: " + answerId);

    var radioBtns = document.querySelectorAll("label");
    for (var i = 0; i < radioBtns.length; i++) {
        radioBtns[i].classList.add("bg-light");
        radioBtns[i].classList.remove("bg-primary");
        radioBtns[i].classList.add("text-black");
        radioBtns[i].classList.remove("text-white");
    }
    radioBtns[answerId].classList.remove("bg-light");
    radioBtns[answerId].classList.remove("text-black");
    radioBtns[answerId].classList.add("bg-primary");
    radioBtns[answerId].classList.add("text-white");


}