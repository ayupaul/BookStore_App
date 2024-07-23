var connection = new signalR.HubConnectionBuilder().withUrl("/notificationsHub").build();
connection.on("RecieveMssg", function (title, message) {
    alert(message);
    console.log(message);
    toastr.info(message, title);
});
connection.start();