$(document).ready(function () {
    function checkSize() {
        var input = document.getElementById("image");
        var file = input.files[0];
        if (file.size > 250000) {
            $(".fileMsg").fadeIn();
            console.log(file.size);
            $(".btnCreate").attr("disabled", "disabled");
            return false;
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#preview").attr("src", e.target.result);
            }
            reader.readAsDataURL(file);
            $(".fileMsg").fadeOut();
            $(".btnCreate").removeAttr("disabled");
            return true;
        }
    }
    $("#image").on("change", function (e) {
        $(".fileMsgSuccess").fadeOut();
        if (checkSize()) {
            var form = $("#imageSubmit");
            form.submit();
        }
    });
    $("#imageSubmit").on("submit", function (e) {
        e.preventDefault();
        var data = new FormData(this);
        var discriminator = $("#discriminator").val();
        var id = $("#IdConfig").val();
        abp.ui.setBusy($("#container"), abp.ajax({
            type: "POST",
            url: urls.SiteConfig + "UploadImageToInfo/?discriminator=" + discriminator + "&id=" + id,
            data: data,
            contentType: false,
            cache: false,
            processData: false
        }).done(function () {
            $(".fileMsgSuccess").fadeIn();
        }));
    });
});
