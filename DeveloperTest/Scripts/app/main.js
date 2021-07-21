var appConfig = (function () {
    var obj = {};
    function setutPlugins() {
        $.extend(true, $.fn.dataTable.defaults, {
            dom: "fprtlip",
            order: [],
            searching: false,
            bInfo: false,
            paging: false,
            pageLength: 50,
            stateSave: false,
            processing: true,
            searchDelay: 500,
            serverSide: true,
        });
    };
    obj.init = function () {
        setutPlugins();
    }
    return obj;

}());

$(function () {
    appConfig.init();
});