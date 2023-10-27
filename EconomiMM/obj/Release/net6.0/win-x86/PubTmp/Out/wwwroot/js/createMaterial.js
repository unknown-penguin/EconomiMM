"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/materialHub").build();

connection.start().then(function () {
    console.log('connected to hub');
    connection.invoke("GetAllMaterialTypes").catch(function (err) {
        return console.error(err.toString());
    })
}).catch(function (err) {
    return console.error(err.toString());
});



connection.on("RecievedMaterialTypeInfo", function (materialTypeDB) {
    console.log("out");


    materialTypeDB.forEach(function (materialType) {
        $('#name-select').append($('<option>', {
            value: materialType['name'],
            text: materialType['name']
        }));

    });

});