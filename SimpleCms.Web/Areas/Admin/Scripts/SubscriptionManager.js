(function ($) {
    //Basic usage
    //           -notification name                 -is not subscribed message                        -is subscribed message                                     -default selector    -misc text
    //<a data-name="CreatedRole" data-activeMessage="Subscribe to created role" data-inActiveMessage="Unsubscribe to created role" class="btn btn-xs btn-default notificationButton">Getting data...</a>
    //
    //Subscription service is in the layout controller
    //
    var subscriptionManager = function () {
        subscriptionManager.principalButton = $(".notificationButton");
        subscriptionManager.SubButton = $(".subscribe");
        subscriptionManager.UnSubButton = $(".unSubscribe");
        subscriptionManager.SubscribeUrl = "/Admin/Layout/Subscribe";
        subscriptionManager.UnSubscribeUrl = "/Admin/Layout/UnSubscribe";
        $("body").on("click", ".subscribe", function () {
            var serviceName = $(this).data("name");
            subscriptionManager.Subscribe(serviceName);
        });
        $("body").on("click", ".unSubscribe", function () {
            var serviceName = $(this).data("name");
            subscriptionManager.UnSubscribe(serviceName);
        });
        var initSubscriptionChecher = function () {
            $(".notificationButton").each(function () {
                var self = this;
                var serviceName = $(this).data("name");
                var subscribeMessage = $(this).data("activemessage");
                var unSubscribeMessage = $(this).data("inactivemessage");
                var data = {
                    serviceName: serviceName
                };
                abp.ajax({
                    url: "/Admin/Layout/IsSubscribed",
                    data: JSON.stringify(data)
                }).done(function (data) {
                    if (data.isSubscribed) {
                        $(self).removeClass("subscribe");
                        $(self).addClass("unSubscribe");
                        $(self).text(unSubscribeMessage);
                    } else {
                        $(self).removeClass("unSubscribe");
                        $(self).addClass("subscribe");
                        $(self).text(subscribeMessage);
                    }
                });
            });
        }
        subscriptionManager.Subscribe = function (subServiceName) {
            var data = {
                serviceName: subServiceName
            }
            abp.ajax({
                url: subscriptionManager.SubscribeUrl,
                data: JSON.stringify(data)
            }).done(function () {
                initSubscriptionChecher();
                abp.message.success("Subscribed!");
            });
        }
        subscriptionManager.UnSubscribe = function (subServiceName) {
            var data = {
                serviceName: subServiceName
            }
            abp.ajax({
                url: subscriptionManager.UnSubscribeUrl,
                data: JSON.stringify(data)
            }).done(function () {
                initSubscriptionChecher();
                abp.message.success("Unsubscribed!");
            });
        }
        initSubscriptionChecher();
        return subscriptionManager;
    }
    //Init instance - Start listening
    subscriptionManager();
})(jQuery);