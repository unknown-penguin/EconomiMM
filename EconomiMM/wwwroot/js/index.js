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
    $("<tr id=\"" + materialDB[0]['name'] + "-list\">" +
        "<td colspan=\"3\">" +
        "<table class=\"table material-table\">" +
        "<thead>" +
        "<tr>" +
        "<th>" +
        "Назва" +
        "</th>" +
        "<th>" +
        "Товщина" +
        "</th>" +
        "<th>" +
        "Розміри" +
        "</th>" +
        "<th>" +
        "Колір" +
        "</th>" +
        "<th>" +
        "  Ціна  ." +
        "</th>" +
        "<th class=\"table-success\">" +
        "Наша ціна за лист" +
        "</th>" +
        "<th>" +
        "Наша ціна за м2" +
        "</th>" +
        "<th>" +
        "Дилерська ціна за лист" +
        "</th>" +
        "<th>" +
        "Дилерська ціна за м2" +
        "</th>" +
        "<th class=\"table-danger\">" +
        "Кількість" +
        "</th>" +
        "<th>" +
        "Зарезервовано" +
        "</th>" +
        "<th>" +
        "Продаж" +
        "</th>" +
        "</tr>" +
        "</thead>" +
        "<tbody>" +
        "</tbody>" +
        "</table>" +
        "</td>" +
        "</tr>").insertAfter('#' + materialDB[0]['name']);


    materialDB.forEach(function (material) {
        let colors = "";
        if (material['colors'] && material['colors'].length > 0) {
            material['colors'].forEach(function (materialColor) {

                colors += `<span class="dot-color-show" style="background-color:${materialColor['colorHex']}"></span>`;
            });

        }

        $("<tr id=" + material['name'] + "-" + material['thickness'] + ">" +
            "<td>" +
            material['name'] +
            "</td>" +
            "<td>" +
            material['thickness'] + " мм" +
            "</td>" +
            "<td>" +
            (material['size'] != null ? material['size'] : "Не задано") +
            "</td>" +
            "<td>" +
            '<div class="color-cell">' +
            colors +
            '</div>' +
            "</td>" +
            "<td>" +
            material['price'] + " €"+
            "</td>" +
            "<td class=\"table-success\">" +
            material['ourPriceForSheet'] + " грн" +
            "</td>" +
            "<td>" +
            material['ourPriceForSqMetre'] + " грн" +
            "</td>" +
            "<td>" +
            material['dealerPriceForSheet'] + " грн" +
            "</td>" +
            "<td>" +
            material['dealerPriceForSqMetre'] + " грн" +
            "</td>" +
            "<td class=\"table-danger\">" +
            material['count'] + " шт" +
            "</td>" +
            "<td>" +
            material['reserved'] +
            "</td>" +
            "<td>" +
            "<a href=\"/Materials/Sell?id=" + material['id'] + "\">Продати</a> " +
            "</td>" +
            "</tr>").appendTo("#" + materialDB[0]['name'] + "-list" + " tbody");

    });

});


$(document).on("click", ".material-table th", function () {
    var table = $(this).parents('table').eq(0)
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()))
    this.asc = !this.asc
    if (!this.asc) { rows = rows.reverse() }
    for (var i = 0; i < rows.length; i++) { table.append(rows[i]) }
})
function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index), valB = getCellValue(b, index)
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.toString().localeCompare(valB)
    }
}
function getCellValue(row, index) { return $(row).children('td').eq(index).text() }