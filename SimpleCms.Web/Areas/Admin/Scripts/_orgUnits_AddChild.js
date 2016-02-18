$(function () {
    $("#addChildForm").on("submit", function (e) {
        e.preventDefault();
        var child = {
            ParentId: $("#ParentIdModal").val(),
            Name: $("#ModalName").val()
        };
        console.log("Func busy called");
        abp.ui.setBusy(window.modalContent, abp.ajax({
            url: "/Admin/OrgUnits/Create",
            data: JSON.stringify(child)
        }).done(function () {
            if (!modal) {
                var newmodal = new window.globalModal();
                newmodal.close();
            } else {
                modal.close();
            }
            window.updateTree();
            window.okNotify();
        }));
    });
});