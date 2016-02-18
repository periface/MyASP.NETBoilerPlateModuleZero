$("#createUserForm").on("submit", function (e) {
    e.preventDefault();
    var roles = [];
    $(".checks input").each(function () {
        var name = $(this).attr('name');
        if ($(this).is(":checked")) {
            roles.push({
                RoleName: name,
                Granted: true
            });
        }
    });
    var data = {
        Name: $("#Name").val(),
        Surname: $("#Surname").val(),
        Email: $("#Email").val(),
        ShouldChangePassword: $("#ShouldChangePassword").is(":checked"),
        CreateRandomPassword: $("#CreateRandomPassword").is(":checked"),
        SendActivationEmail: $("#SendActivationEmail").is(":checked"),
        PasswordInput: $("#PasswordInput").val(),
        ConfirmPassword: $("#ConfirmPassword").val(),
        IsActive: $("#IsActive").is(":checked"),
        Roles: roles,
        UserName: $("#UserName").val()
    }
    abp.ui.setBusy(this, abp.ajax({
        url: "/Admin/Users/CreateUser",
        data: JSON.stringify(data)
    }).done(function () {
        window.refreshTable();
        modal.close();
        abp.notify.success("User created.");
    }));
});
function CheckSize() {
    var input = document.getElementById("image");
    var file = input.files[0];
    if (file.size > 250000) {
        $(".fileMsg").fadeIn();
        console.log(file.size);
        $(".btnCreate").attr("disabled", "disabled");
        return false;
    } else {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#preview").attr("src", e.target.result);
        }
        reader.readAsDataURL(file);
        $(".fileMsg").fadeOut();
        $(".btnCreate").removeAttr("disabled");
        return true;
    }
}

$("#CreateRandomPassword").change(function () {
    if ($(this).is(":checked")) {
        $("#ps").fadeOut();
    } else {
        $("#ps").fadeIn();
    }
});