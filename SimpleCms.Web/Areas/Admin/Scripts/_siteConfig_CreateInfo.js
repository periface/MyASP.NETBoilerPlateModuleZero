$(document).ready(function () {

    $("#createInfoForm").on("submit", function (e) {
        e.preventDefault();
        var newInfo = {
            SiteTitle: $("#SiteTitle").val(),
            SiteSlogan: $("#SiteSlogan").val(),
            SiteDescription: $("#SiteDescription").val(),
            IsActive: $("#IsActive").is(":checked")
        }
        abp.ui.setBusy($(modal), abp.ajax({
            url: urls.SiteConfig+"CreateInfo",
            data: JSON.stringify(newInfo)

        }).done(function () {
            modal.close();
            InfoSaved();
            RebindInfo();
        }));
    });
});
