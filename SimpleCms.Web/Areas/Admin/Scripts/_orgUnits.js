var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.ModuleZeroConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
var colNames = function () {
    return [L("Actions"), "Id", L("UserName"), L("Email")];
}
window.okNotify = function () {
    abp.notify.success(L("OrgUnits_UnitSaved"));
}
$(document).ready(function () {
    $("#usersTable").jqGrid({
        colNames: colNames(),
        height: 'auto',
        datatype: "json",
        pager: '#pagination',
        autowidth: true,
        colModel: [
            {
                name: 'act',
                index: 'act',
                sortable: false,
                align: "center"

            },
            {
                name: "Id",
                index: "Id",
                align: "center",
                hidden: true
            },
            {
                name: "FullName",
                index: "FullName",
                align: "center"

            },
            {
                name: "EmailAddress",
                index: "EmailAddress"
            }
        ],
        gridComplete: function () {
            var ids = jQuery("#usersTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var rowClientId = jQuery("#usersTable").jqGrid('getCell', cl, 'Id');
                var ca = "<button class='btn btn-xs btn-default removeFromUnit' data-unit=" + id + " data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-times'></i></button>";
                //ce = "<button class='btn btn-xs btn-default' onclick=\"jQuery('#jqgrid').restoreRow('"+cl+"');\"><i class='fa fa-times'></i></button>";
                //jQuery("#jqgrid").jqGrid('setRowData',ids[i],{act:be+se+ce});
                jQuery("#usersTable").jqGrid('setRowData', ids[i], {
                    act: ca
                });
            }
            //setTimeout(function () {
            //    $('#usersTable').trigger('resize.jqGrid');
            //}, 1000);
        }
    });
});
window.refreshTable = function () {
    $("#usersTable").trigger('reloadGrid');
}
var editArea = $("#optionsContainer");
var bindTree = function () {
    $('#nestable3').nestable();
}
window.loadUsersInUnit = function (id) {
    editArea.load(urls.OrgUnits + "EditUnit/" + id, function () {
        editArea.fadeIn();
    });
    $("#btnSec").text("");

    $("#btnSec").append(
        "    <button class='btn btn-xs btn-primary addMembers' data-id='" + id + "'>"
        + L("OrgUnits_AddMember")
        + "</button><br /><br />");

    $("#users").text("");
    window.populateTable(id);
}
var updateTree = function () {
    $("#tree").load(urls.OrgUnits + "TreeView", function () {
        bindTree();
    });
}
window.populateTable = function (id) {
    $("#usersTable").jqGrid('GridUnload');
    $("#usersTable").jqGrid({
        url: urls.OrgUnits + "UsersFromUnit/" + id,
        height: 'auto',
        datatype: "json",

        pager: '#pagination',
        emptyrecords: "No users in Unit",
        rowNum: 10,
        rowList: [10, 20, 30],
        autowidth: true,
        colNames: colNames(),
        sortname: 'Id',
        colModel: [
            {
                name: 'act',
                index: 'act',
                sortable: false,
                align: "center"

            },
            {
                name: "Id",
                index: "Id",
                align: "center",
                hidden: true
            },
            {
                name: "FullName",
                index: "FullName",
                align: "center"

            },
            {
                name: "EmailAddress",
                index: "EmailAddress"
            }
        ],
        gridComplete: function () {
            var ids = jQuery("#usersTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var rowClientId = jQuery("#usersTable").jqGrid('getCell', cl, 'Id');
                var ca = "<button class='btn btn-xs btn-default removeFromUnit' data-unit=" + id + " data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-times'></i></button>";
                //ce = "<button class='btn btn-xs btn-default' onclick=\"jQuery('#jqgrid').restoreRow('"+cl+"');\"><i class='fa fa-times'></i></button>";
                //jQuery("#jqgrid").jqGrid('setRowData',ids[i],{act:be+se+ce});
                jQuery("#usersTable").jqGrid('setRowData', ids[i], {
                    act: ca
                });
            }
            //setTimeout(function () {
            //    $('#usersTable').trigger('resize.jqGrid');
            //}, 1000);
        }
    });

};
$(window).on('resize.jqGrid', function () {
    $("#usersTable").jqGrid('setGridWidth', $("#tableContainer").width());
});
window.removeFromUnit = function (id, unitId) {
    if (isGranted(zeroPermissions.ManageUnits_Modify)) {
        var data = {
            IdMember: id,
            IdOrganizationSection: unitId
        }
        abp.ui.setBusy($("#usersTable"), abp.ajax({
            url: urls.OrgUnits + "RemoveFromUnit",
            datatype: "json",
            data: JSON.stringify(data)
        }).done(function () {
            window.refreshTable();
            updateTree();
            abp.notify.success(L("Removed"));
        }));
    } else {
        abp.message.warn("You have no permissions for this operation");
    }
}

$(function () {
    $("body").on("click", ".addMembers", function () {

        var id = $(this).data("id");
        var callBack = function () {
            $('#modal').unbind().modal();
        }
        if (isGranted(zeroPermissions.ManageUsers_ViewUsers)) {
            modal.loadModal(urls.OrgUnits + "LoadAllUsers/" + id, callBack);

        } else {
            abp.message.warn("You have no permissions to perform this operation");
        }
        //window.modalContent.load("LoadAllUsers/" + id, function () {

        //});
    });
    var deleteUnit = function (id) {
        if (isGranted(zeroPermissions.ManageUnits_Delete)) {
            var model = {
                Id: id
            }
            console.log(model);
            abp.ui.setBusy($("body"), abp.ajax({
                url: urls.OrgUnits + "DeleteUnit",
                data: JSON.stringify(model),
                contentType: "application/json"
            }).done(function () {
                abp.notify.success(L("Removed"));

                updateTree();

            }));
        } else {
            abp.message.warn("You have no permissions to perform this operation");
        }
    }
    $("body").on("click", ".delete", function () {
        var id = $(this).data("id");

        console.log(abp.message);
        abp.message.confirm(
            L("OrgUnits_AdviseDeleteUnit"),
            L("OrgUnits_ConfirmQuestion"),
            function (isConfirmed) {
                if (isConfirmed) {
                    deleteUnit(id);
                }
            }
        );
    });
    $("body").on("click", ".usersInUnit", function () {

        var id = $(this).data("id");
        window.loadUsersInUnit(id);

    });
    $("body").on("click", ".removeFromUnit", function () {
        if (isGranted(zeroPermissions.ManageUnits_Modify)) {
            var id = $(this).data("id");
            var unitId = $(this).data("unit");
            abp.message.confirm(
                L("OrgUnits_AdviseRemoveUser"),
                 L("OrgUnits_ConfirmQuestion"),
                function (isConfirmed) {
                    if (isConfirmed) {
                        removeFromUnit(id, unitId);
                    }
                }
            );
        }
        else {
            abp.message.warn("You have no permissions to perform this operation");
        }
    });
    $("body").on("click", ".addChild", function () {
        if (isGranted(zeroPermissions.ManageUnits_Create)) {
            var id = $(this).data("id");
            modal.loadModal(urls.OrgUnits + "AddChild/" + id, "");
        } else {
            abp.message.warn("You have no permissions for this operation");
        }
    });
    var updateOutput = function (e) {
        if (isGranted(zeroPermissions.ManageUnits_Modify)) {
            console.log(e);
            var list = e.length ? e : $(e.target),
                output = list.data('output');
            if (window.JSON) {
                var model = window.JSON.stringify(list.nestable('serialize'));
                console.log(model); //, null, 2));
                abp.ajax({
                    url: urls.OrgUnits + "ChangeOrderOfUnits",
                    data: model,
                    contentType: "application/json"
                }).done(function () {
                    updateTree();
                });
            } else {
                output.val('JSON browser support required.');
            }
        }
        else {
            abp.message.warn("You have no permissions to perform this operation");
            updateTree();
        }

    };
    $(document).ready(function () {

        pageSetUp();

        bindTree();
    });
    $("body").on("change", "#nestable3", updateOutput);
    var guardarUnidad = function (unidad) {
        if (isGranted(zeroPermissions.ManageUnits_Create)) {
            abp.ui.setBusy($("body"), abp.ajax({
                url: urls.OrgUnits + 'Create',
                data: JSON.stringify(unidad)
            }).done(function () {
                var message = L("OrgUnits_UnitSaved");
                abp.message.success(message + unidad.Name);
                updateTree();
            }));
        }
        else {
            abp.message.warn("You have no permissions to perform this operation");
        }
    }
    $("#newOrgUnit").on("submit", function (e) {
        e.preventDefault();
        var name = $("#Name").val();
        var parentId = $("#ParentId").val();
        var newOrganization = {
            Name: name,
            ParentId: parentId
        };
        guardarUnidad(newOrganization);
    });
    $("body").on("click", ".convertToRoot", function () {
        if (isGranted(zeroPermissions.ManageUnits_Modify)) {
            var id = $(this).data("id");
            var data = {
                Id: id
            }
            abp.message.confirm(
                L("OrgUnits_ConfirmQuestion"),
                L("OrgUnits_ConfirmQuestion_Root"),
                function (isConfirmed) {
                    if (isConfirmed) {
                        abp.ui.setBusy($("#nestable3"), abp.ajax({
                            url: urls.OrgUnits + "ConvertToRoot",
                            data: JSON.stringify(data)
                        }).done(function () {
                            window.updateTree();
                        }));
                    }
                }
            );
        }
        else {
            abp.message.warn("You have no permissions to perform this operation");
        }

    });
});