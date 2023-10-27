"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/calculatorHub").build();

connection.start().then(function () {
    console.log('connected to hub');
}).catch(function (err) {
    return console.error(err.toString());
});

$(document).on('change', 'input#roundRadio', function () {
    if (this.checked) {
        $(".rectangle-sizes").addClass('disabled');
        $("#changable-span").text("Діаметр");
        $('#joint-length2').val("");
    }
});
$(document).on('change', 'input#rectangularRadio', function () {

    if (this.checked) {
        $(".rectangle-sizes").removeClass('disabled');
        $("#changable-span").text("Довжина 1");
    }
});

$(document).on('change', 'input#tapeRadio', function () {

    if (this.checked) {
        $("#liner-checking").removeClass('disabled');
    }
});
$(document).on('change', 'input#flangeRadio', function () {

    if (this.checked) {
        $("#liner-checking").addClass('disabled');
        $("#linerCheckbox").prop("checked", false);
        $(".addition").addClass('disabled');
    }
});

$(document).on("change", "#linerCheckbox", function () {

    if ($(this).is(":checked")) {
        $(".addition").removeClass('disabled');
        $(".liner-material-window").removeClass('disabled');
    } else {
        $(".addition").addClass('disabled');
        $(".liner-material-window").addClass('disabled');
    }
});


$(document).on("click", "a.toggle-btn", function () {

    var materialtypeId = $(this).attr("id");
    var parentDiv = $(this).closest(".joint-material");
    var contentToHide = parentDiv.find("#materialType-" + materialtypeId);
    contentToHide.toggle();

});
$(document).on("click", "button.copy-btn", function () {
    var length1 = $('#joint-length1').val();
    $('#joint-length2').val(length1);

});

function getSelectedMaterials(containerSelector) {
    var $container = $(containerSelector);
    var $checkedCheckboxes = $container.find('input[type="checkbox"]:checked');
    var selectedMaterials = [];
    $checkedCheckboxes.each(function () {
        var id = $(this).attr('id');
        id = id.replace("material-", "");
        selectedMaterials.push(id);
    });

    return selectedMaterials;
}

function appendSelectedMaterial(material, appendDiv) {

    if (!$(appendDiv).find('#' + material['type']['id']).length) {
        $(appendDiv).append(
            "<a id=" + material['type']['id'] + " class=\"toggle-btn link-dark\">" + material['type']['name'] + "</a>" +
            "<div id=\"materialType-" + material['type']['id'] + "\"></div>"
        );
    }

    if (!$(appendDiv).find('#materialType-' + material['type']['id']).find('#material-' + material['id']).length)//check if material not exist
    {
        var priceUnits = "грн/м^2"

        if (material.hasOwnProperty('partOfLiner') && material['partOfLiner'] == 0)
            priceUnits = "грн/м^3"

        $(appendDiv + " #materialType-" + material['type']['id']).append(
            "<div id=\"material-" + material['id'] + "\" class=\"input-group-sm d-flex flex-row selected-material\">" +
            "<input type=\"text\" class=\"form-control layer-count\" value = \"1\" aria-label=\"Small>" +
            "<label class=\"form-check-label\" for=" + material['id'] + ">" +
            "x " + material['name'] +
            "</label>" +
            "<label class=\"form-check-label mx-2\" for=\"material-" + material['id'] + "\">" +
            material['thickness'] + "мм"+
            "</label>" +
            "<label class=\"form-check-label mx-2\" for=\"material-" + material['id'] + "\">" +
            material['price'] + priceUnits+
                "</label>" +
            "<br>" +
            "</div>"
        );
    }
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
    selectedMaterials.forEach(function (material) {
        appendSelectedMaterial(material, ".joint-material")
    });


});

connection.on("addSelectedMaterialFlange", function (selectedMaterials) {


    selectedMaterials.forEach(function (material) {
        appendSelectedMaterial(material, ".flange-material")
    });




});

connection.on("addSelectedMaterialLiner", function (selectedMaterials) {
    selectedMaterials.forEach(function (material) {
        if (material['partOfLiner'] == 0)
            appendSelectedMaterial(material, ".liner-material-inner")
        else
            appendSelectedMaterial(material, ".liner-material-outer")
    });



});

function getUsedMaterials(containerSelector) {
    var $container = $(containerSelector);
    var $selectedMaterial = $container.find('div[id^="material-"]');
    var usedMaterials = [];
    $selectedMaterial.each(function () {
        var id = $(this).attr('id');
        id = id.replace("material-", "");
        var count = $(this).find(".layer-count").val();

        var selMaterialObj =
        {
            'id': parseInt(id),
            'countOfLayers': parseInt(count)
        }
        usedMaterials.push(selMaterialObj);
    });

    return usedMaterials;
}

function parseSetting(inputId) {
    var settingValue = $(inputId).val();
    if (settingValue == "") {
        return "0";
    }
    else {
        return settingValue;
    }

}
function parseShapeRadio() {
    var roundRadio = $('#roundRadio');
    var rectangleRadio = $('#rectangularRadio');
    if (roundRadio.is(':checked')) {
        return roundRadio.val();
    }
    else if (rectangleRadio.is(':checked')) {
        return rectangleRadio.val();
    }
    else return -1;
}

function parseTypeRadio() {
    var roundRadio = $('#flangeRadio');
    var rectangleRadio = $('#tapeRadio');
    if (roundRadio.is(':checked')) {
        return roundRadio.val();
    }
    else if (rectangleRadio.is(':checked')) {
        return rectangleRadio.val();
    }
    else
        return -1;
}
function parseLinerCheckbox() {
    var checbox = $('#linerCheckbox');

    if (checbox.is(':checked')) {
        return 1;
    }
    else {
        return 0;
    }
}
function getSettings() {

    var orderNumber = parseSetting("#order-number");
    var orderTemperature = parseSetting("#order-temperature");
    var jointLength1 = parseSetting("#joint-length1");
    var jointLength2 = parseSetting("#joint-length2");
    var jointWidth = parseSetting("#joint-width");
    var flangeWidth = parseSetting("#flange-width");
    var materialKoef = parseSetting("#koef");
    var silicone = parseSetting("#silicone");
    var mainPartPrice = parseSetting("#main-part-price");
    var linerLenght1 = parseSetting("#liner-length1");
    var linerWidth = parseSetting("#liner-width");
    var linerHeight = parseSetting("#liner-height");
    var linerBindingWidth = parseSetting("#liner-binding-width");
    var linerPrice = parseSetting("#liner-price");
    var jointShape = parseShapeRadio();
    var jointType = parseTypeRadio();
    var withLiner = parseLinerCheckbox();


    var obj = {
        'orderNumber': orderNumber,
        'temperature': orderTemperature,
        //'mainPartLength1': parseFloat(jointLength1),
        'mainPartLength2': parseFloat(jointLength2) / 1000,
        'mainPartWidth': parseFloat(jointWidth) / 1000,
        'flangeWidth': parseFloat(flangeWidth) / 1000,
        'koef': parseFloat(materialKoef),
        'siliconeTubes': parseInt(silicone),
        'mainPartWorkPrice': parseFloat(mainPartPrice),
        'linerPartLength': parseFloat(linerLenght1) / 1000,
        'linerPartWidth': parseFloat(linerWidth) / 1000,
        'linerPartHeight': parseFloat(linerHeight) / 1000,
        'linerPartBindingWidth': parseFloat(linerBindingWidth) / 1000,
        'linerPartWorkPrice': parseFloat(linerPrice) / 1000,
        'jointShape': parseInt(jointShape),
        'jointType': parseInt(jointType),
        'withLiner': Boolean(withLiner)
    };
    //not tested
    if (obj.jointShape === 0) {
        obj.mainPartDiameter = parseFloat(jointLength1) / 1000;
    }
    else if (obj.jointShape == 1) {
        obj.mainPartLength1 = parseFloat(jointLength1) / 1000;
    }
    return obj;
}

connection.on("ShowResult", function (t) {
    $(".total-price-input").val(t);


});

$(document).on('click', '.calc-btn', function () {
    var jointUsedMaterials = getUsedMaterials('.joint-material');
    var flangeUsedMaterials = getUsedMaterials('.flange-material');
    var linerUsedMaterials = getUsedMaterials('.liner-material');

    connection.invoke("CalculatePrice", getSettings(), jointUsedMaterials, flangeUsedMaterials, linerUsedMaterials);
});
$(document).on('click', '.save-btn', function () {
    var jointUsedMaterials = getUsedMaterials('.joint-material');
    var flangeUsedMaterials = getUsedMaterials('.flange-material');
    var linerUsedMaterials = getUsedMaterials('.liner-material');
    //create checker if nothing is filled
    connection.invoke("SaveOrder", getSettings(), jointUsedMaterials, flangeUsedMaterials, linerUsedMaterials);
});