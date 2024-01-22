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
        var materialID = rowData.find('#id').text();
        var materialCount = rowData.find('#count').text();
        var materialPrice = rowData.find('#price').text();
        var materialPriceOurPriceForSheet = rowData.find('#our-price-for-sheet').text();
        var materialPriceOurPriceForSqMeter = rowData.find('#our-price-for-sqmetre').text();
        var materialPriceDealerPriceForSheet = rowData.find('#dealer-price-for-sheet').text();
        var materialPriceDealerPriceForSqMeter = rowData.find('#dealer-price-for-sqmetre').text();
        var materialReserved = rowData.find('#reserved').text();
        var materialObj = {
            "id": materialID,
            "count": materialCount,
            "price": materialPrice,
            "OurPriceForSheet": materialPriceOurPriceForSheet,
            "OurPriceForSqMeter": materialPriceOurPriceForSqMeter,
            "DealerPriceForSheet": materialPriceDealerPriceForSheet,
            "DealerPriceForSqMeter": materialPriceDealerPriceForSqMeter,
            "reserved": materialReserved
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
            "<tr>" +
                "<th>"+
                    "ID"+
                "</th>"+
                "<th>"+
                    "Назва"+
                "</th>"+
                "<th>"+
                    "Товщина"+
                "</th>" +
                "<th>" +
                    "Розміри" +
                "</th>" +
                "<th>" +
                    "Колір" +
                "</th>" +
                "<th>"+
                    "Ціна"+
                "</th>"+
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
        let colors = "";
        if (material['colors'] && material['colors'].length > 0) {
            material['colors'].forEach(function (materialColor) {

                colors += `<span class="dot-color-show" style="background-color:${materialColor['colorHex']}"></span>`;
            });

        }
        var materialRow = "<tr id=" + material['name'] + "-" + material['thickness'] + ">" +
            "<td id=id>" +
            material['id'] +
            "</td>" +
            "<td id=name>" +
            material['name'] +
            "</td>" +
            "<td id=thickness>" +
            material['thickness'] +
            "</td>" +
            "<td id=size>" +
            material['size'] +
            "</td>" +
            "<td id=color>" +
            '<div class="color-cell">' +
            colors +
            '</div>' +
            "</td>" +
            "<td contenteditable id=price>" +
            material['price'] +
            "</td>" +
            "<td id=our-price-for-sheet  class=\"table-success\">" +
            material['ourPriceForSheet'] +
            "</td>" +
            "<td id=our-price-for-sqmetre>" +
            material['ourPriceForSqMetre'] +
            "</td>" +
            "<td id=dealer-price-for-sheet>" +
            material['dealerPriceForSheet'] +
            "</td>" +
            "<td id=dealer-price-for-sqmetre>" +
            material['dealerPriceForSqMetre'] +
            "</td>" +
            "<td id=count class=\"table-danger \" contenteditable>" +
            material['count'] +
            "</td>" +
            "<td contenteditable id=reserved>" +
            material['reserved'] +
            "</td>" +
            "<td>" +
            material['sold'] +
            "</td>" +
            "<td>" +
            "<a href=\"/Materials/Sell?id=" + material['id'] + "\">Продати</a> " +
            "<a href=\"/Materials/Edit?id=" + material['id'] + "\">Редагувати</a> " +
            "<a href=\"/Materials/Delete?id=" + material['id']+"\">Видалити</a>"+
            "</td>" +
            "</tr>";
        $(".material-table tbody").append(materialRow);

    });

});
$(document).on('input', '#price', function () {
    var editedPrice = $(this).text();

    var manufacturerInput = $('input#Manufacturer').val();
    var materialid = $(this).parent().find('td#id').text();
    connection.invoke("GetNewPrices", manufacturerInput, parseInt(materialid), editedPrice).catch(function (err) {
        return console.error(err.toString());
    })
});

function findTRWithText(text) {
    var foundRow = null;

    $('.material-table tr').each(function () {
        if ($(this).find('td#id:contains("' + text + '")').length > 0) {
            foundRow = this;
            return false; // Break out of the loop if a match is found
        }
    });

    return foundRow;
}

connection.on("UpdateMaterialPrices", function (id,
    OurPriceForSheet,
    OurPriceForSqMetre,
    DealerPriceForSheet,
    DealerPriceForSqMetre) {
    
    var materialTr = findTRWithText(id);
    console.log($(materialTr).find("td#our-price-for-sheet").text());
    $(materialTr).find("td#our-price-for-sheet").text(OurPriceForSheet);
    $(materialTr).find("td#our-price-for-sqmetre").text(OurPriceForSqMetre);
    $(materialTr).find("td#dealer-price-for-sheet").text(DealerPriceForSheet);
    $(materialTr).find("td#dealer-price-for-sqmetre").text(DealerPriceForSqMetre);

});