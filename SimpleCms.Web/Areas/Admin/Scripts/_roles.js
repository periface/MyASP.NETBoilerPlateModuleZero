
var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.ModuleZeroConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
function RoleCreated() {
    abp.message.success(L("RoleCreated"), L("Success"));
}
function RoleDeleted() {
    abp.message.success(L("RoleDeleted"), L("Success"));
}
window.populateTableRoles = function () {
    $("#tableRoles").jqGrid({
        url: "/Admin/Roles/GetRoles/",
        //shrinkToFit: false,
        height: 'auto',
        datatype: "json",
        pager: '#paginationRoles',
        autowidth: true,
        emptyrecords: "No roles found",
        rowNum: 10,
        rowList: [10, 20, 30],
        colNames: ["", 'Actions', "Role Name", "Role Type", "Creation Time"],
        sortname: 'Id',
        colModel: [
            {
                name: "Id",
                index: "act",
                hidden: true
            },
            {
                name: 'act',
                index: 'act',
                sortable: false,
                align: "left"

            },
            {
                name: "DisplayName",
                index: "DisplayName",
                align: "center"
            },
            {
                name: "Type",
                index: "Type",
                align: "center"
            },
            {
                name: "CreationTimeString",
                index: "CreationTimeString",
                align: "center"

            }
        ],
        gridComplete: function () {
            var ids = jQuery("#tableRoles").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var rowClientId = jQuery("#tableRoles").jqGrid('getCell', cl, 'Id');
                var ca = "<button class='btn btn-xs btn-primary editRole'  data-id='" + rowClientId + "' data-original-title='Edit Role'><i class='fa fa-edit'></i></button>";
                var de = "<button class='btn btn-xs btn-danger deleteRole'  data-id='" + rowClientId + "' data-original-title='Delete Role'><i class='fa fa-times'></i></button>";
                //ce = "<button class='btn btn-xs btn-default' onclick=\"jQuery('#jqgrid').restoreRow('"+cl+"');\"><i class='fa fa-times'></i></button>";
                //jQuery("#jqgrid").jqGrid('setRowData',ids[i],{act:be+se+ce});
                jQuery("#tableRoles").jqGrid('setRowData', ids[i], {
                    act: de + " " + ca
                });
            }

        }
    });
}
$(document).ready(function () {
    var seeRole = function (roleName) {
        var errorFunc = function (response, status) {
            if (status === "error") {
                modal.close();
                abp.notify.error("Role not found!");
            }
        }
        modal.loadModal(urls.Roles + "EditRole?roleName=" + roleName,errorFunc);

    }
    var rolePetition = $("#rolePetition");
    if (rolePetition.val()) {
        seeRole(rolePetition.val());
    }
    window.populateTableRoles();




    $("#initUnSub").click(function() {
        abp.ajax({
            url: "/Admin/Layout/UnSubscribeToCreatedRole"
        }).done(function() {
            abp.message.success("Unsubscribed!");
        });
    });
    $("#initUnSubEdit").click(function() {
        abp.ajax({
            url: "/Admin/Layout/UnSubscribeToEditedRole"
        }).done(function() {
            abp.message.success("Unsubscribed!");
        });
    });
    $("#initUnSubDelete").click(function() {
        abp.ajax({
            url: "/Admin/Layout/UnSubscribeToDeletedRole"
        }).done(function () {
            abp.message.success("Unsubscribed!");
        });
    });
    $("#tableWidget").on("click", ".editRole", function () {
        if (isGranted(zeroPermissions.ManageRoles_Edit)) {

            var id = $(this).data("id");
            modal.loadModal(urls.Roles + "EditRole/" + id, "");
        } else {
            abp.message.warn("You have no permissions to perform this operation");
        }

    });
    $(".create").click(function () {
        if (isGranted(zeroPermissions.ManageRoles_Create)) {

            modal.loadModal(urls.Roles + "CreateRole", "");
        } else {
            abp.message.warn("You have no permissions to perform this operation");
        }
    });

    $("body").on("click", ".deleteRole", function () {
        if (isGranted(zeroPermissions.ManageRoles_Create)) {
            var id = $(this).data("id");
            abp.message.confirm(
                L("MessageDeleteQRole"),
                L("MessageDeleteQ"),
                function (isConfirmed) {
                    if (isConfirmed) {
                        var role = {
                            RoleId: id
                        }
                        //abp.message.success(localize("MessageDeleted", localizationConstant));
                        //deleteUnit(id);
                        window.removeRole(role);
                    }
                }
            );
        }
        else {
            abp.message.warn("You have no permissions to perform this operation");
        }
    });

});
window.refreshTable = function () {
    $("#tableRoles").trigger('reloadGrid');
}
$(window).on('resize.jqGrid', function () {
    $("#tableRoles").jqGrid('setGridWidth', $("#tableContainer").width());
});
window.removeRole = function (role) {

    abp.ui.setBusy($("body"), abp.ajax({
        url: "/Admin/Roles/DeleteRole",
        data: JSON.stringify(role)
    }).done(function () {
        abp.notify.success(L("RoleDeleted"));
        window.refreshTable();
    }).fail(function (data) {
        console.log("Failed");
        //abp.notify.error(data.message);
    }));
}