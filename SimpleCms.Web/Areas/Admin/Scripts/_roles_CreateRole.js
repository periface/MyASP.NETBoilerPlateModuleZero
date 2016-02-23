$(document).ready(function () {
    
    $("#createRoleForm").on("submit", function (e) {
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
            IsDefault: $("#IsDefault").is(":checked")
        }
        abp.ui.setBusy($("#myTabContent1"), abp.ajax({
            url: "/Admin/Roles/CreateRole",
            data: JSON.stringify(newRole)
        }).done(function () {
            modal.close();
            abp.notify.success("Role Created", "Success");
            window.refreshTable();
        }));
    });
});