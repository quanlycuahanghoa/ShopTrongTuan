
function SeachUser() {
    $.ajax({
        url: "/User/Seach",
        data: {
            Page: 1,
            FullName: $("#txtSeachNameUser").val(),
            UserName: $("#txtSeachUserName").val(),
            Phone: $("#txtSeachPhoneUser").val(),
            Group: $("#txtSeachGroupUser").val(),
        },
        success: function (result) {
            $("#Table_User").html(result);
        }
    });
}

function GetListGroupUser() {
    $.ajax({
        url: "/GroupUser/GetListGroup",
        success: function (result) {
            $.each(result, function (i, result) {
                $('#txtSeachGroupUser').append($('<option>', {
                    value: result.ID,
                    text: result.GroupName
                }));
            });

            $.each(result, function (i, result) {
                $('#SelectAddGroupIdUser').append($('<option>', {
                    value: result.ID,
                    text: result.GroupName
                }));
            });

            $.each(result, function (i, result) {
                $('#txtEditGroupUser').append($('<option>', {
                    value: result.ID,
                    text: result.GroupName
                }));
            });

        }
    });
}



function AddUser() {

    $.ajax({
        url: "/User/AddUser",
        data: $("#FormAdd_User").serialize(),
        success: function (result) {
            if (result) {
                $("#Modal_AdđUser").hide();
                swal("Thông Báo!", "Thêm mới thành công", "success");
            }
            else {

            }
            SeachUser();
        }
    });
}

function EditUser() {

    $.ajax({
        url: "/User/EditUser",
        data: $("#Form_EditUser").serialize(),
        success: function (result) {
            if (result) {
                $("#Modal_EditUser").hide();
                swal("Thông Báo!", "Cập nhập thành công tài khoản", "success");
            }
            else {

            }
            SeachUser();
        }
    });
}


function GetUserById(Id) {
    $.ajax({
        url: "/User/GetUserById",
        data: { Id: Id },
        success: function (result) {
            $("#txtEditIdUser").val(result.ID);
            $("#txtEditNameUser").val(result.Name);
            $("#txtEditUserName").val(result.UserName);
            $("#txtEditEmail").val(result.Email);
            $("#txtEditPhone").val(result.Phone);
            $("#txtEditGroupUser").val(result.GroupId);
            if (result.Status == true) {
                $('#txtEditStatusUser').val('true').prop('selected', true);
            }
            else {
                $('#txtEditStatusUser').val('false').prop('selected', true);
            }
            $("#Modal_EditUser").show();
        }
    });
}


var IDDelete = 0;
function ShowFromDeleteUser(Id) {
    IDDelete = Id;
    $("#Delete_Users").show();
}

function DelUser() {
    $.ajax({
        url: "/User/DeleteUser",
        data: { Id: IDDelete },
        success: function (result) {
            if (result == 1) {
                $("#Delete_Users").hide();
                swal("Thông Báo!", "Xóa tài khoản thành công", "success");
            }

            SeachUser();
        }
    });
}


//==> đăng nhập tài khoản 
function LoginAdmin() {
    var userName = $("#txtUserName").val();
    var passWord = $("#txtPassword").val();
    $.ajax({
        url: "/User/LoginAction",
        data: { UserName: userName, Password: passWord },
        type: "POST",
        success: function (result) {
            if (result == 1) {
                window.location.href = "/Home/Index";
            }
            else {

            }

        },
        error: function (errr) {

        }
    });
}


// phan đình kiên /3/3/2019
// tìm kiếm loại sản phẩm 
function SeachProductCategory() {
    $.ajax({
        url: "/ProductCategory/Seach",
        data: {
            Page: 1,
            ProductCategoryName: $("#txtSeachNameProductCategory").val(),
            GroupCategoryId: $("#txtSeachGroupProductCategory").val()
        },
        success: function (result) {
            $("#Table_ProductCategory").html(result);
        }
    });
}

function getGropProductCategory() {
    $.ajax({
        url: "/ProductCategory/GetSelectProductCategory",
        success: function (result) {
            $.each(result, function (i, result) {
                $('.txtSeachGroupProductCategory').append($('<option>', {
                    value: result.ID,
                    text: result.Name
                }));

            });
        }
    });
}

function AddProductCategory() {

   
    var value = CKEDITOR.instances['txtAddDescriptionProductCategoryCkeditor'].getData();
    $('#txtAddDescriptionProductCategory').val(value); 
    
    $.ajax({
        type:"POST",
        url: "/ProductCategory/AddProductCategory",
        data: $("#FormAdd_ProductCategory").serialize(),

        success: function (result) {
            var data = $("#noidung").val(); 
            swal("Thông Báo!", data, "success");
            if (result==1) {
                ///window.location = '/ProductCategory/index'; 
                swal("Thông Báo!", "Thêm mới thành công", "success");
                alert("Thêm mới thành công"); 
            }
            else {

            }
            SeachProductCategory();
        }
    });
}



var IDDeleteProductCategory;
function ShowFromDeletePrductCategory(Id) {
    IDDeleteProductCategory = Id;
    $("#Delete_ProductCategory").show();
}

function DelProductCategory() {
    
    $.ajax({
        url: "/ProductCategory/DeleteProductCategory",
        //type: "post",
        data: { ProductCategoryId: IDDeleteProductCategory },
        success: function (result) {
            if (result == 1) {
                $("#Delete_ProductCategory").hide();
                swal("Thông Báo!", "Xóa tài Thành công loại Sản phẩm", "success");
            }
            SeachProductCategory();
        }
    });
}
$(document).ready(function () {
    getGropProductCategory();
    GetListGroupUser();
});