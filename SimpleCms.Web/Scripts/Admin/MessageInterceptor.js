var reloadNotifications = function () {
    $("#notifications").empty();
    abp.ajax({
        url: "/Admin/Layout/Notifications"
    }).done(function (d) {
        var count = d.length;
        if (count <= 0) {
            $("#notifications").append("<li><a  class='checkAll'>Not a single notification...</a></li>");
        }
        $.each(d, function (i, userNotification) {
            if (userNotification.notification.data.type === 'Abp.Notifications.LocalizableMessageNotificationData') {
                var localizedText = abp.localization.localize(
                    userNotification.notification.data.message.name,
                    userNotification.notification.data.message.sourceName
                );

                $.each(userNotification.notification.data.properties, function (key, value) {
                    localizedText = localizedText.replace('{' + key + '}', value);
                });

                if (userNotification.notification.notificationName === "CreatedRole") {
                    $("#notifications").append("<li><a data-id='" + userNotification.id + "' data-href='/Admin/Roles?role=" + userNotification.notification.data.properties.uniqueRoleName + "' class='notification'>" + localizedText + "</a></li>");
                }
                if (userNotification.notification.notificationName === "EditedRole") {
                    $("#notifications").append("<li><a data-id='" + userNotification.id + "' data-href='/Admin/Roles?role=" + userNotification.notification.data.properties.uniqueRoleName + "' class='notification'>" + localizedText + "</a></li>");
                }
                if (userNotification.notification.notificationName === "DeletedRole") {
                    $("#notifications").append("<li><a data-id='" + userNotification.id + "' class='notification'>" + localizedText + "</a></li>");
                }
            }
        });
        $("#notifications").append("<li><hr style='margin:0;'></li>");
        $("#notifications").append("<li><a  class='checkAll'>Clear</a></li>");

        $("#notifications").append("<li><a  class='seeAll'>See all</a></li>");
    });
}
$("body").on("click", ".checkAll", function () {
    abp.ajax({
        url: "/Admin/Layout/MarkAllAsReaded"
    }).done(function () {
        reloadNotifications();
    });
});
$(document).ready(function () {
    reloadNotifications();
});
abp.event.on('abp.notifications.received', function (userNotification) {
    abp.notifications.showUiNotifyForUserNotification(userNotification);
    //if (userNotification.notification.data.type === 'Abp.Notifications.LocalizableMessageNotificationData') {
    //    var localizedText = abp.localization.localize(
    //        userNotification.notification.data.message.name,
    //        userNotification.notification.data.message.sourceName
    //    );

    //    $.each(userNotification.notification.data.properties, function (key, value) {
    //        localizedText = localizedText.replace('{' + key + '}', value);
    //    });
    //    abp.notify.success(localizedText);
    //}
    reloadNotifications();
});
$("body").on("click", ".notification", function () {
    var id = $(this).data("id");
    var url = $(this).data("href");
    abp.ajax({
        url: "/Admin/Layout/MarkAsReaded/" + id
    }).done(function () {
        window.location.href = url;
    });
});