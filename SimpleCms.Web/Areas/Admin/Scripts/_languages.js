var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.ModuleZeroConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
$(".create").click(function () {
    modal.loadModal(urls.Languages + "CreateLanguage");
    //window.modalContent.load("/Admin/Languages/CreateLanguage", function () {
    //    window.launchModal();
    //});
});
$("body").on("click", ".edit", function() {
    var id = $(this).data("id");
    modal.loadModal(urls.Languages + "EditLanguage/" + id);

});
$(document).ready(function () {
    LoadData();
});
function ReloadData() {
    $("#langsTable").trigger('reloadGrid');
}
function LoadData() {
    $("#langsTable").jqGrid({
        url: urls.Languages + "LoadLanguages/",
        height: 'auto',
        datatype: "json",
        jsonReader: {
            root: 'Data',
            repeatitems: false
        },
        pager: '#pagination',
        emptyrecords: "No languages",
        rowNum: 10,
        rowList: [10, 20, 30],
        autowidth: true,
        colNames: ['Actions', "Id","Type","","", "Name", "Creation Time"],
        colModel: [

            {
                name: 'act',
                index: 'act',
                sortable: false,
                align: "center"

            }
            ,
            {
                name: "Id",
                index: "Id",
                hidden: true
            },
            {
                name: "State",
                index: "State",
                align: "center"
            },
            {
                name: "IsHost",
                index: "IsHost",
                hidden: true
            },
            {
                name: "Name",
                index: "Name",
                hidden: true
            },
            {
                name: "GetFullInfo",
                index: "GetFullInfo",
                align: "center"

            },
            {
                name: "CreationDate",
                index: "CreationDate",
                align: "center"
            }

        ],
        gridComplete: function () {
            var ids = jQuery("#langsTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var cl = ids[i];
                var rowClientId = jQuery("#langsTable").jqGrid('getCell', cl, 'Id');
                var rowClientName = jQuery("#langsTable").jqGrid('getCell', cl, 'Name');
                var isHost = jQuery("#langsTable").jqGrid('getCell', cl, 'IsHost');
                var ca = "", ed = "";
                if (isHost === "false") {
                    ca = "<button class='btn btn-xs btn-danger removeLanguage' data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-times'></i></button>";
                    ed = "<button class='btn btn-xs btn-primary edit' data-id='" + rowClientId + "' data-original-title='Cancel'><i class='fa fa-edit'></i></button>";
                }
                var edText = "<a class='btn btn-xs btn-default editText' href='/Admin/Languages/EditLanguageText?langName="+ rowClientName +"&isHost="+isHost+"' title='Edit Texts'><i class='fa fa-book'></i></a>";
                //ce = "<button class='btn btn-xs btn-default' onclick=\"jQuery('#jqgrid').restoreRow('"+cl+"');\"><i class='fa fa-times'></i></button>";
                //jQuery("#jqgrid").jqGrid('setRowData',ids[i],{act:be+se+ce});
                jQuery("#langsTable").jqGrid('setRowData', ids[i], {
                    act: ca + " " + ed + " " +edText
                });
            }
            //setTimeout(function () {
            //    $('#usersTable').trigger('resize.jqGrid');
            //}, 1000);
        }
    });
}