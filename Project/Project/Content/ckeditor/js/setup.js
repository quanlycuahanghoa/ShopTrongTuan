$(document).ready(function () {
    // data-tables
    CKEDITOR.replace("txtAddDescriptionProductCategoryCkeditor");
    CKEDITOR.replace("txtEditDescriptionProductCategoryCkeditor"); 
    $('#btnAddEmageProductCategory').click(function () {
        var fider = new CKFinder();

        fider.selectActionFunction = function (fileUrl) {
            $("#ImageAddProductCategory").append("<img src='" + fileUrl +"' style='width: 100%; height: 100%; ' />")
            $('#txtAddImageProductCategory').val(fileUrl);
        }
        fider.popup();
    })
});