"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/teacherHub").build();

var reported = 0;
var submitted = 0;

connection.on("ReportedIn", function () {
    reported++;
    document.getElementById("StudentsOnline").textContent = reported;
});

connection.on("AnswerProvided", function (answerId) {
    submitted++;
    document.getElementById("StudentsAnswered").textContent = submitted;
    var pbar = document.querySelector("div#pbar_" + answerId);
    pbar.ariaValueNow = (pbar.ariaValueNow | 0) + 1
    var allPbars = document.querySelectorAll("div.progress-bar");
    for (var i = 0; i < allPbars.length; i++) {
        var pa = (allPbars[i].ariaValueNow / submitted * 100) | 0;
        allPbars[i].style.width = "" + pa + "%"
    }
})

connection.start()