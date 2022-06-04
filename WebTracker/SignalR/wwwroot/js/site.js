// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/server")
    .build();

connection.on("ReceiveMessage", (message) => {
    console.log(message)
});

connection.start().catch((err) => {
    return console.error(err.toString());
});

// get the click events data
// send data to the backend through socket
document.addEventListener('click', (event) => {

    connection.invoke("ReceiveMessage", "clicked").catch((err) => {
        return console.error(err.toString())
    });
});