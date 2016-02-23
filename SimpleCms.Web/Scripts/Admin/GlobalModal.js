//El Peri!
var globalModal = function (container, wrapper) {
    var instance = {};
    globalModal.container = container || $("#modalContent");
    globalModal.wrappElement = wrapper || $("#modal");
    globalModal.setContainer = function (containerSelector) {
        globalModal.container = containerSelector;
    };
    globalModal.execute = function () {
        if (globalModal.sourceUrl) {
            globalModal.container.load(globalModal.sourceUrl, globalModal.callBack);
            globalModal.wrappElement.modal("show");
        } else {
            console.error("No source url defined!");

        }
    };
    globalModal.excecuteWithData = function(data) {
        if (globalModal.sourceUrl) {
            globalModal.container.load(globalModal.sourceUrl, data, globalModal.callback);
            globalModal.wrappElement.modal("show");
        } else {
            console.error("No source url defined!");
        }
    }
    globalModal.close = function () {
        globalModal.wrappElement.modal("hide");
    };
    globalModal.loadModal = function (sourceUrl, callback) {
        var noCallBack = function () {
            console.log("No callback defined...");
        }
        globalModal.sourceUrl = sourceUrl;
        globalModal.callBack = callback || noCallBack;
        globalModal.execute();
    };
    globalModal.loadModalWithData = function(sourceUrl,data,callback) {
        var noCallBack = function () {
            console.log("No callback defined...");
        }
        globalModal.sourceUrl = sourceUrl;
        globalModal.callBack = callback || noCallBack;
        globalModal.excecuteWithData(data);
    }
    instance.loadModal = globalModal.loadModal;
    instance.loadModalWithData = globalModal.loadModalWithData;
    instance.setContainer = globalModal.setContainer;
    instance.close = globalModal.close;
    return instance;
};
window.publicModal = globalModal;
