var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.ModuleZeroConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
$("#btnSearch").click(function () {
    var postDataValues = $("#searchText").val();
    $("#usersTable").jqGrid("setGridParam", {
        url: urls.Users + "GetUsers?searchString=" + postDataValues
    });
    $("#usersTable").jqGrid("setGridParam", { datatype: "json" }).trigger("reloadGrid");
});
window.refreshTable = function () {
    $("#usersTable").trigger('reloadGrid');
}
$("#usersTable").jqGrid({
    url: urls.Users + "GetUsers/",
    height: 'auto',
    datatype: "json",
    jsonReader: {
        root: 'Data',
        repeatitems: false
    },
    pager: '#pagination',
    emptyrecords: "No users in Unit",
    rowNum: 10,
    rowList: [10, 20, 30],
    autowidth: true,
    colNames: ['Actions', "Id", "User´s Full Name", "Roles", "Email", "Status", "Creation Time", "Last Login Time"],
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
            name: "RolesString",
            index: "RolesString",
            align: "center"
        },
        {
            name: "EmailAddress",
            index: "EmailAddress"
        },
        {
            name: "IsActiveString",
            index: "IsActiveString"
        },
        {
            name: "CreationTimeString",
            index: "CreationTimeString"
        },

        {
            name: "LastLoginTimeString",
            index: "LastLoginTimeString"
        }

    ],
    gridComplete: function () {
        var ids = jQuery("#usersTable").jqGrid('getDataIDs');
        for (var i = 0; i < ids.length; i++) {
            var cl = ids[i];
            var rowClientId = jQuery("#usersTable").jqGrid('getCell', cl, 'Id');
            var ca = "<button class='btn btn-xs btn-danger removeUser' data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-times'></i></button>";
            var ed = "<button class='btn btn-xs btn-primary editUser' data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-edit'></i></button>";
            jQuery("#usersTable").jqGrid('setRowData', ids[i], {
                act: ca + " " + ed
            });
        }
    }
});
$(window).on('resize.jqGrid', function () {
    $("#usersTable").jqGrid('setGridWidth', $("#tableContainer").width());
});
$(".create").click(function () {
    if (isGranted(zeroPermissions.ManageUsers_Create)) {
        modal.loadModal(urls.Users + "CreateUser", "");

    } else {
        abp.message.warn("You have no permissions to perform this operation");
    }
    
});
$("body").on("click", ".editUser", function () {
    if (isGranted(zeroPermissions.ManageUsers_Edit)) {
        var id = $(this).data("id");
        modal.loadModal(urls.Users + "EditUser/" + id, "");
    } else {
        abp.message.warn("You have no permissions to perform this operation");
    }
});
$("body").on("click", ".removeUser", function () {
    var id = $(this).data("id");
    abp.message.confirm(
        "User will be deleted!",
        "Are you sure?",
        function (isConfirmed) {
            if (isConfirmed) {
                deleteUser(id);
            }
        }
    );
});
function deleteUser(id) {
    abp.ui.setBusy($("#usersContainer"), abp.ajax({
        url: urls.Users + "DeleteUser/" + id
    }).done(function () {
        abp.notify.success(L("UserDeleted"));

        window.refreshTable();
    }));
}