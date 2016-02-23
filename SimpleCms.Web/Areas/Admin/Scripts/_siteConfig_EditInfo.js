$(document).ready(function () {

    $("#editInfoForm").on("submit", function (e) {
        e.preventDefault();

        var newInfo = {
            SiteTitle: $("#SiteTitle").val(),
            SiteSlogan: $("#SiteSlogan").val(),
            SiteDescription: $("#SiteDescription").val(),
            IsActive: $("#IsActive").is(":checked"),
            Id: $("#Id").val()
        }
        abp.ui.setBusy($(modal), abp.ajax({
            url: urls.SiteConfig + "EditInfo",
            data: JSON.stringify(newInfo)

        }).done(function () {
            modal.close();
            InfoSaved();
            RebindInfo();
        }));
    });
});
