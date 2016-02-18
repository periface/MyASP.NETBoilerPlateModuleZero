var modal = new window.publicModal();
var nameSpace = new window.globalConfigs(window.globalVariables.Localization.CmsModuleConstant);
var urls = window.globalVariables.Url;
var L = nameSpace.L;
function InfoSaved() {
    abp.message.success(L("InformationCreated"), L("Success"));
}
function InfoActivated() {
    abp.message.success(L("InformationActivated"), L("Success!"));
}
function InfoDeleted() {
    abp.notify.success(L("InformationDeleted"), L("Success"));
}
function ActivateInfo(infoId) {
    abp.ui.setBusy($("#infoWrapper"), abp.ajax({
        url: urls.SiteConfig + "SetInfoAsActive/" + infoId
    }).done(function () {
        InfoActivated();
        RebindInfo();
    }));
}
function DeleteInfo(infoId) {
    abp.ui.setBusy($("#infoWrapper"), abp.ajax({
        url: urls.SiteConfig + "DeleteInfo/" + infoId
    }).done(function () {
        InfoDeleted();
        RebindInfo();
    }));
}
function RebindInfo() {
    $("#infoWrapper").load(urls.SiteConfig + "SiteInfos");
}

$("#infoWrapper").on("click", ".activateButton", function () {
    var id = $(this).data("id");
    ActivateInfo(id);
});
$("#infoWrapper").on("click", ".deleteButton", function () {
    var id = $(this).data("id");
    abp.message.confirm(L("DeleteInfoMessage"), L("SureQuestion"), function (result) {
        if (result) {
            DeleteInfo(id);
        }
    });
});
$(".create").click(function () {
    modal.loadModal(urls.SiteConfig+"CreateInfo","");
});
$("#infoWrapper").on("click", ".editInfo", function () {
    var id = $(this).data("id");
    modal.loadModal(urls.SiteConfig+"EditInfo/" + id);
    
});
$("#infoWrapper").on("click", ".uploadIcon", function () {
    var id = $(this).data("id");
    modal.loadModal(urls.SiteConfig + "UploadImage?discriminator=Icon&id=" + id);
});
$("#infoWrapper").on("click", ".uploadLogo", function () {
    var id = $(this).data("id");
    modal.loadModal(urls.SiteConfig + "UploadImage?discriminator=Logo&id=" + id);
});