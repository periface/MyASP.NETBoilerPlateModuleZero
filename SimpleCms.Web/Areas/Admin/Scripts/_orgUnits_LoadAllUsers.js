$("#btnSearch").click(function () {
    var postDataValues = $("#searchText").val();
    $("#allUsersTable").jqGrid("setGridParam", {
        url: "/Admin/OrgUnits/AllUsers?searchString=" + postDataValues,
    });
    $("#allUsersTable").jqGrid("setGridParam", { datatype: "json" }).trigger("reloadGrid");
});
//$("#searchText").on("keyup", function () {
//    var postDataValues = $("#searchText").val();
//    $("#allUsersTable").jqGrid("setGridParam", {
//        url: "/Admin/OrgUnits/AllUsers?searchString=" + postDataValues
//    });
//    $("#allUsersTable").jqGrid("setGridParam", { datatype: "json" }).trigger("reloadGrid");
//});
$("#allUsersTable").on("click", ".toUnit", function () {
    var id = $(this).data("id");
    var model = {
        IdUser: id,
        IdUnit: $("#orgUnit").val()
    }
    abp.ui.setBusy($("#tableAllContainer"), abp.ajax({
        url: "/Admin/OrgUnits/AddUserToUnit",
        data: JSON.stringify(model)
    }).done(function () {
        modal.close();
        window.refreshTable();
        window.updateTree();
        abp.notify.success("User added to unit!");
        //window.modal.unbind().modal();

    }));

});
window.populateTableWithAllUsers = function () {
    $("#allUsersTable").jqGrid({
        url: "/Admin/OrgUnits/AllUsers/",
        height: 'auto',
        datatype: "json",
        jsonReader: {
            root: 'Data',
            repeatitems: false
        },
        autowidth: true,
        caption: "Users in Unit",
        emptyrecords: "No users found",
        rowNum: 10,
        rowList: [10, 20, 30],
        pager: '#allUsersPagination',
        colNames: ['Actions', "Id", "User Name", "Email"],
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
        }],
        gridComplete: function () {
            var ids = jQuery("#allUsersTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var rowClientId = jQuery("#allUsersTable").jqGrid('getCell', cl, 'Id');
                var ca = "<button class='btn btn-xs btn-default toUnit'  data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-save'></i></button>";
                //ce = "<button class='btn btn-xs btn-default' onclick=\"jQuery('#jqgrid').restoreRow('"+cl+"');\"><i class='fa fa-times'></i></button>";
                //jQuery("#jqgrid").jqGrid('setRowData',ids[i],{act:be+se+ce});
                jQuery("#allUsersTable").jqGrid('setRowData', ids[i], {
                    act: ca
                });
            }
        }
    });
}
$(window).on('resize.jqGrid', function () {
    $("#allUsersTable").jqGrid('setGridWidth', $("#tableAllContainer").width());
});
$(document).ready(function () {
    window.populateTableWithAllUsers();
    //Fire the event to rezise the table
    setTimeout(function () {
        $('#allUsersTable').trigger('resize.jqGrid');
    }, 500);
});