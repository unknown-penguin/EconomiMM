"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/calculatorHub").build();

connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});


$(document).on("click", ".toggle-btn", function () {
    var materialtypeId = $(this).attr("id");
    var parentDiv = $(this).closest(".expansionjoint-material");
    var contentToHide = parentDiv.find("#materialType-" + materialtypeId);
    contentToHide.toggle();

});

function getSelectedMaterials(containerSelector) {
    var $container = $(containerSelector);
    var $checkedCheckboxes = $container.find('input[type="checkbox"]:checked');
    var selectedMaterials = [];
    $checkedCheckboxes.each(function () {
        var id = $(this).attr('id');
        selectedMaterials.push(id);
    });

    return selectedMaterials;
}

function appendSelectedMaterial(material, appendDiv) {

    if (!$(appendDiv + ' #' + material['type']['id']).length)//check if type not exist
    {
        $(appendDiv).append(
            "<a id =" + material['type']['id'] + " class= \"toggle-btn link-dark\" >" + material['type']['name'] + "</a >" +
            "<div id = \"materialType-" + material['type']['id'] + "\"></div>");
    }
    $(appendDiv + " #materialType-" + material['type']['id']).append(
        "<div>" +
        "<input class= \"form-check-input\" type = \"checkbox\" value = \"\" id = " + material['normilized_name'] + " >" +
        "<label class=\"form-check-label\" for=" + material['normilized_name'] + ">" +
        material['name'] +
        "</label>" +
        "<br>" +
        "</div>"
    );




    return selectedMaterials;
}

$(document).on("click", "#add-joint-material-button", function () {

    var $container = $('.joint-material-modal');
    connection.invoke("SelectMaterialsJoint", getSelectedMaterials($container));

});

$(document).on("click", "#add-flange-material-button", function () {

    var $container = $('.flange-material-modal');
    connection.invoke("SelectMaterialsFlange", getSelectedMaterials($container));

});

$(document).on("click", "#add-liner-material-button", function () {

    var $container = $('.liner-material-modal');
    connection.invoke("SelectMaterialsLiner", getSelectedMaterials($container));

});

connection.on("addSelectedMaterialJoint", function (selectedMaterials) {
    console.log(selectedMaterials);

    selectedMaterials.forEach(function (material) {
        appendSelectedMaterial(material, ".joint-material")
    });


});

connection.on("addSelectedMaterialFlange", function (selectedMaterials) {
    console.log(selectedMaterials);

    selectedMaterials.forEach(function (material) {
        appendSelectedMaterial(material, ".flange-material")
    });
    



});

connection.on("addSelectedMaterialLiner", function (selectedMaterials) {
    console.log(selectedMaterials);

    selectedMaterials.forEach(function (material) {
        if (material['partOfLiner'] == 0)
            appendSelectedMaterial(material, ".liner-material-inner")
        else
            appendSelectedMaterial(material, ".liner-material-outer")
    });



});

//$(document).on("click", ".calc-btn", function () {

//});