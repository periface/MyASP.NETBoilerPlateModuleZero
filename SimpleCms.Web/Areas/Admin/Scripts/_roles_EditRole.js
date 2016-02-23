$(document).ready(function () {
    $("#editRoleForm").on("submit", function (e) {
        e.preventDefault();
        var permissions = [];
        $(".checks input").each(function () {
            var name = $(this).attr('name');
            if ($(this).is(":checked")) {

                permissions.push({
                    DbName: name,
                    Granted: true

                });
            }
        });
        var newRole = {
            PermisionList: permissions,
            DisplayName: $("#DisplayName").val(),
            IsDefault: $("#IsDefault").is(":checked"),
            Id: $("#Id").val()
        }
        abp.ui.setBusy($("#myTabContent1"), abp.ajax({
            url: "/Admin/Roles/EditRole",
            data: JSON.stringify(newRole)
        }).done(function () {
            modal.close();
            abp.notify.success("Role Edited", "Success");
            window.refreshTable();
        }));
    });
});
