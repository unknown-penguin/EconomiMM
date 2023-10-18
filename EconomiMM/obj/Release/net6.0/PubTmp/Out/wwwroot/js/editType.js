"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/materialHub").build();

connection.start().then(function () {
    console.log('connected to hub');
    var materialName = $('.name ').find(".form-control").attr("value");
    connection.invoke("SendMaterial", materialName).catch(function (err) {
        return console.error(err.toString());
    })
}).catch(function (err) {
    return console.error(err.toString());
});
function getCountOfMaterials()
{
    return parseInt($('.material-table tr').length) - 1;
}

$(document).on("click", ".save-button input", function () {
    var materialsCount = getCountOfMaterials();
    for (let index = 1; index <= materialsCount; index++) {
        var rowData = $('.material-table tbody tr:nth-child(' + index + ')');
        var materialID = rowData.find('.id').text();
        var materialName = rowData.find('.name').text();
        var materialThickness = rowData.find('.thickness').text();
        var materialCount = rowData.find('.count').text();
        var materialPrice = rowData.find('.price').text();
        var materialReserved = rowData.find('.reserved').text();
        var materialSold = rowData.find('.sold').text();
        var materialObj = {
            "id": parseInt(materialID),
            "name": materialName,
            "thickness": parseFloat(materialThickness),
            "count": parseInt(materialCount),
            "price": parseInt(materialPrice),
            "reserved": parseInt(materialReserved),
            "sold": parseInt(materialSold),
        };
        connection.invoke("EditMaterial", materialObj).catch(function (err) {
            return console.error(err.toString());
        })

    }
});

connection.on("RecievedMaterialInfo", function (materialDB) {
    console.log("out");
    $("<table class=\"table material-table\">"+
        "<thead>"+
            "<tr id=Test>" +
                "<th>"+
                    "ID"+
                "</th>"+
                "<th>"+
                    "Назва"+
                "</th>"+
                "<th>"+
                    "Товщина"+
                "</th>"+
                "<th>"+
                    "Ціна"+
                "</th>"+
                "<th>" +
                    "Кількість" +
                "</th>" +
                "<th>"+
                    "Зарезервовано"+
                "</th>"+
                "<th>" +
                    "Продано" +
                "</th>" +
                "<th>" +
                "</th>" +
            "</tr>"+
        "</thead>"+
        "<tbody>" +
        "</tbody>"+
        "</table>").insertAfter('.manufacturer');


    materialDB.forEach(function (material) {
        var materialRow = "<tr id=" + material['name'] + "-" + material['thickness'] + ">" +
            "<td class=id>" +
            material['id'] +
            "</td>" +
            "<td class=name>" +
            material['name'] +
            "</td>" +
            "<td class=thickness>" +
            material['thickness'] +
            "</td>" +
            "<td class=price>" +
            material['price'] +
            "</td>" +
            "<td contenteditable class=count>" +
            material['count'] +
            "</td>" +
            "<td contenteditable class=reserved>" +
            material['reserved'] +
            "</td>" +
            "<td contenteditable class=sold>" +
            material['sold'] +
            "</td>" +
            "<td>" +
            "<a href=\"/Materials/Edit?id=" + material['id'] + "\">Редагувати</a> " +
            "<a href=\"/Materials/Delete?id=" + material['id']+"\">Видалити</a>"+
            "</td>" +
            "</tr>";
        $(".material-table tbody").append(materialRow);

    });

});