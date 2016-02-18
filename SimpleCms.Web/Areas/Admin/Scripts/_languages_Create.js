$(document).ready(function () {
    function create(language) {
        abp.ui.setBusy($("#CreateForm"), abp.ajax({
            url: "/Admin/Languages/CreateLanguage",
            data: JSON.stringify(language)
        }).done(function () {
            modal.close();
            ReloadData();
        }));
    };
    $("#LanguageIconEdit").selectpicker({
        iconBase: "famfamfam-flag",
        tickIcon: "fa fa-check"
    });
    $("#LanguageNameEdit").selectpicker({
        iconBase: "famfamfam-flag",
        tickIcon: "fa fa-check"
    });
    $("#createLang").click(function() {
        var data = {};
        data.Name = $("#LanguageNameEdit").val();
        data.Icon = $("#LanguageIconEdit").val();
        create(data);
    });
});
