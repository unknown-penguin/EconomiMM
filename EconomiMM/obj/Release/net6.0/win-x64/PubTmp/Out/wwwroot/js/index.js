"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/materialHub").build();
connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

$(document).on("click", ".material-type-table tr", function () {
    var materialName = $(this).attr("id");
    console.log("in");
    if ($(this).hasClass("unshown-material")) {
        $(this).removeClass("unshown-material")
        $(this).addClass("shown-material")
        connection.invoke("SendMaterial", materialName).catch(function (err) {
            return console.error(err.toString());
        })
    }
    else if ($(this).hasClass("shown-material")) {
        $(this).removeClass("shown-material")
        $(this).addClass("unshown-material")
        $("tr").remove("[id^='" + materialName + "-']");
    }

});
connection.on("RecievedMaterialInfo", function (materialDB) {
    console.log("out");
    console.log(materialDB);
    $("<tr id=\"" + materialDB[0]['name'] + "-list\">" +
        "<td colspan=\"3\">"+
            "<table class=\"table material-table\">"+
                "<thead>"+
                    "<tr>"+
                        "<th>"+
                            "Назва"+
                        "</th>"+
                        "<th>"+
                            "Товщина"+
                        "</th>"+
                        "<th>"+
                            "Кількість"+
                        "</th>"+
                        "<th>"+
                            "Ціна"+
                        "</th>"+
                        "<th>"+
                            "Зарезервовано"+
                        "</th>"+
                    "</tr>"+
                "</thead>"+
                "<tbody>"+
                "</tbody>"+
            "</table>"+
        "</td>"+
        "</tr>").insertAfter('#' + materialDB[0]['name']);
    

    materialDB.forEach(function (material) {
        $("<tr id=" + material['name'] + "-" + material['thickness'] + ">" +
            "<td>" +
                material['name'] +
            "</td>" +
            "<td>" +
                material['thickness'] +
            "</td>" +
            "<td>" +
                material['count'] +
            "</td>" +
            "<td>" +
                material['price'] +
            "</td>" +
            "<td>" +
                material['reserved'] +
            "</td>" +
          "</tr>").insertAfter("#" + materialDB[0]['name'] + "-list" + " tbody");
        
    });
   
});