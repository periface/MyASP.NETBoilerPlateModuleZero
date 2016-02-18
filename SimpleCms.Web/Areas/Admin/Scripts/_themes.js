var nameSpace = new window.globalConfigs(window.globalVariables.Localization.CmsModuleConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
$(document).ready(function () {

    function rebindMyThemes(useMessage) {
        var container = $(".myThemesWrapper");
        container.load(urls.Themes + "MyThemes");
        if (useMessage) {
            abp.message.success(L("ThemeActive"));
        }
    };
    function rebindAllThemes() {
        var container = $(".allThemesWrapper");
        container.load(urls.getThemes + "AllThemes");
        abp.message.success(L("ThemeInLibrary"));
    }
    $(".myThemesWrapper").on("click", ".activeTheme", function () {
        var id = $(this).data("id");
        abp.ui.setBusy($(".themeWrapper-" + id), abp.ajax({
            url: urls.Themes + "ActivateTheme/" + id
        }).done(function () {
            rebindMyThemes(true);
        }));
    });
    $(".allThemesWrapper").on("click", ".getTheme", function () {
        var id = $(this).data("id");
        abp.ui.setBusy($(".themeWrapper-" + id), abp.ajax({
            url: url.Themes+ "GetTheme/" + id
        }).done(function () {
            rebindAllThemes();
            rebindMyThemes(false);
        }));
    });
});
