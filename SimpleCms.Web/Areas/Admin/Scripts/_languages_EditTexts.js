//El Peri!
var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.ModuleZeroConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
var rebindTable = function (langBase, langTarget, source) {
    $("#langTextsTable").jqGrid("setGridParam", {
        url: urls.Languages + "GetLanguagesForEdit?langTarget=" + langTarget + "&langBase=" + langBase + "&langSource=" + source
    });
    $("#langTextsTable").jqGrid("setGridParam", { datatype: "json" }).trigger("reloadGrid");
}
var onlyReloadTable = function() {
    $("#langTextsTable").trigger("reloadGrid");
}
$(document).ready(function () {
    //Fun fun fun fun!!
    var workObject = function (baseSelector, targetSelector, selectedLang, sourceSelector, url) {
        var outPutMembers = {};
        //Private members
        workObject.selectBase = baseSelector || $("#base");
        workObject.selectTarget = targetSelector || $("#target");
        workObject.selectSource = sourceSelector || $("#source");
        workObject.selectedLang = selectedLang || $("#selectedLang").val();
        workObject.populateUrl = function () {
            var resourceUrl = urls.Languages + "GetTenancyLanguages/";
            if (url) {
                resourceUrl = urls.Languages + url;
            }
            return resourceUrl;
        }
        workObject.LoadEditTextForm = function (requestObject) {
            modal.loadModalWithData(urls.Languages + "EditText", requestObject);
        }
        workObject.populateBase = function () {
            if (workObject.selectBase && workObject.selectTarget) {

                abp.ajax({
                    url: workObject.populateUrl(url)

                }).done(function (data) {
                    console.log(data);
                    $.each(data.languages, function (i, language) {
                        workObject.selectBase.append("<option value=" + language.name + ">" + language.getFullInfo + "</option>");
                    });
                });
            } else {
                console.error("No selectors defined!");
            }
        }
        workObject.populateTarget = function () {
            console.log(workObject.selectedLang);
            if (workObject.selectBase && workObject.selectTarget) {
                abp.ajax({
                    url: workObject.populateUrl(url) + "?activeLang=" + workObject.selectedLang
                }).done(function (data) {
                    console.log(data);
                    $.each(data.languages, function (i, language) {
                        workObject.selectTarget.append("<option value=" + language.name + ">" + language.getFullInfo + "</option>");
                    });
                });
            } else {
                console.error("No selectors defined!");
            }
        }

        //Define public members
        outPutMembers.populateBase = workObject.populateBase;
        outPutMembers.populateTarget = workObject.populateTarget;
        outPutMembers.selectBase = workObject.selectBase;
        outPutMembers.selectTarget = workObject.selectTarget;
        outPutMembers.selectSource = workObject.selectSource;
        outPutMembers.LoadEditTextForm = workObject.LoadEditTextForm;

        return outPutMembers;
    };
    
    var instance = new workObject();
    instance.populateBase();
    instance.populateTarget();
    instance.selectBase.change(function () {
        var langBase = $(this).val();
        var langTarget = instance.selectTarget.val();
        var source = instance.selectSource.val();
        rebindTable(langBase, langTarget, source);
    });
    instance.selectTarget.change(function () {
        var langTarget = $(this).val();
        var langBase = instance.selectBase.val();
        var source = instance.selectSource.val();
        rebindTable(langBase, langTarget, source);
    });
    instance.selectSource.change(function () {
        var langTarget = instance.selectTarget.val();;
        var langBase = instance.selectBase.val();
        var source = $(this).val();;
        rebindTable(langBase, langTarget, source);
    });
    $("#langTextContainer").on("click",".editLanguageText", function() {
        var key = $(this).data("key");
        var source = $(this).data("source");
        var lang = $(this).data("target");
        var base = $(this).data("base");
        var data = {
            SourceName:source,
            Key:key,
            BaseLanguage:base,
            TargetLanguage: lang
        }
        instance.LoadEditTextForm(data);
    });
    var populateTable = function (langBase, langTarget) {
        if (!langBase) {
            langBase = "en";
        }
        if (!langTarget) {
            langTarget = $("#selectedLang").val();
        }
        var source = $("#source").val();
        $("#langTextsTable").jqGrid({
            url: urls.Languages + "GetLanguagesForEdit?langTarget=" + langTarget + "&langBase=" + langBase + "&langSource=" + source,
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
            colNames: ["Id", "Key", "Base Value", "Target Value", 'Actions'],
            colModel: [
                {
                    name: "Id",
                    index: "Id",
                    hidden: true
                },
                {
                    name: "Key",
                    index: "Key",
                    align: "center"

                },
                {
                    name: "BaseValue",
                    index: "BaseValue",
                    align: "center"
                },
                {
                    name: "TargetValue",
                    index: "TargetValue",
                    align: "center"
                },
                {
                    name: "act",
                    index: "act",
                    sortable: false,
                    align: "center"

                }
            ],
            gridComplete: function () {
                var ids = jQuery("#langTextsTable").jqGrid('getDataIDs');
                for (var i = 0; i < ids.length; i++) {
                    var cl = ids[i];
                    var rowKey = jQuery("#langTextsTable").jqGrid('getCell', cl, 'Key');
                    var lang = instance.selectTarget.val() || $("#selectedLang").val();
                    var src = instance.selectSource.val();
                    var base = instance.selectBase.val();
                    var ed = "<button class='btn btn-xs btn-primary editLanguageText' data-key="+rowKey+" data-source='" + src + "' data-base='" + base + "' data-target='" +lang + "' data-original-title='Cancel'><i class='fa fa-edit'></i></button>";
                    jQuery("#langTextsTable").jqGrid('setRowData', ids[i], {
                        act: ed
                    });
                }
            }
        });
    }
    populateTable();
});