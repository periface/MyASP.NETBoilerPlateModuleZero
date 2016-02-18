$(document).ready(function () {

    $("#editOrgUnit").on("submit", function (e) {
        e.preventDefault();
        var data = {
            Id: $("#Id").val(),
            TenantId: $("#TentantId").val(),
            DisplayName: $("#DisplayName").val()
        };
        console.log(data);
        abp.ui.setBusy($("#editOrgUnit"), abp.ajax({
            url: "/Admin/OrgUnits/EditUnit",
            data: JSON.stringify(data)
        }).done(function () {
            abp.message.success("Unit edited!", "Success");
            window.loadUsersInUnit(data.Id);
            window.updateTree();
        }));
    });
});

