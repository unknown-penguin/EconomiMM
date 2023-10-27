$(document).ready(function () {

    var $select = $("#material-type-id");
    var $input = $("#material-type-name");
    var selectedText = $select.find("option:selected").text();
    $input.val(selectedText);

    $select.on("change", function () {
        var selectedText = $select.find("option:selected").text();

        $input.val(selectedText);
    });
});
