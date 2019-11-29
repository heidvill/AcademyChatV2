"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, date) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    //var encodedMsg = user + " says " + msg;
    //var row = document.createElement("tr");
    //var from = document.createElement("td");
    //var usermessage = document.createElement("td");
    //var time = document.createElement("td");

    ////täyttää viestin tiedot taulukon td-elementteihin
    //from.textContent = user;
    //usermessage.textContent = msg;
    //time.textContent = date;

    ////lisää td-elementit tr-elementtiin
    //row.appendChild(from);
    //row.appendChild(usermessage);
    //row.appendChild(time);
    ////lisää tr-elementin tbody elementtiin
    //document.getElementById("messagesList").appendChild(row);

    var outgoingdiv = document.getElementById("outgoing");
    var msgbox = document.createElement("div");
    msgbox.className = "outgoing_msg";


    var sent = document.createElement("div");
    sent.className = "sent_msg";
    var time = document.createElement("span");
    time.className = "time_date";
    time.textContent = date;
    var sender = document.createElement("span");
    sender.className = "time_date";
    sender.textContent = user;
    var text = document.createElement("p");
    text.textContent = message;

    sent.appendChild(time);
    sent.appendChild(sender);
    sent.appendChild(text);
    msgbox.appendChild(sent);
    outgoingdiv.appendChild(msgbox);



});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});