appConfig.Reports = (function () {
    var obj = {};
    obj.configureTable = function (params) {
        $(function () {
            var table = $('#datatable').DataTable({
                responsive: true,
                ajax: function (data, callback, settings) {
                    $.ajax({
                        url: params.listUrl,
                        type: "GET",
                        dataType: "json",
                        success: function (response) {
                            callback(response);
                        },
                    });
                },
                columnDefs: [
                    {
                        targets: 0,
                        data: "region",
                    },
                    {
                        targets: 1,
                        data: "cases",
                        type: "numeric-comma"
                    },
                    {
                        targets: 2,
                        data: "deaths",
                        type: "numeric-comma"
                    },
                ],
            });
        });
    };
    return obj;
}());

$('#btnReports').click(function () {
    var region = $('#regions').val();

    if (region == 'Regions') {
        $('#datatable tr:eq(0) th:eq(0)').text("Region");
        $('#datatable').dataTable().fnDestroy();
        $('#btnXml').attr("href", 'Reports/ExportToXML');
        $('#btnJson').attr("href", 'Reports/ExportToJson');
        $('#btnCsv').attr("href", 'Reports/ExportToCsv');
        appConfig.Reports.configureTable({ listUrl: "Reports/ListReports" });
    }
    else {
        $('#btnXml').attr("href", 'Reports/ExportToXML/' + region);
        $('#btnJson').attr("href", 'Reports/ExportToJson/' + region);
        $('#btnCsv').attr("href", 'Reports/ExportToCsv/' + region);
        $('#datatable').dataTable().fnDestroy();
        $("#datatable").DataTable({
            dom: "fprtlip",
            ajax: {
                "url": "Reports/ListReportsByRegion",
                "contentType": "application/json",
                "data": {
                    "regionName": region
                }
            },
            order: [],
            searching: false,
            bInfo: false,
            paging: false,
            pageLength: 50,
            stateSave: false,
            processing: true,
            searchDelay: 500,
            serverSide: true,
            columnDefs: [
                {
                    targets: 0,
                    data: "province",
                },
                {
                    targets: 1,
                    data: "cases",
                    type: "numeric-comma"
                },
                {
                    targets: 2,
                    data: "deaths",
                    type: "numeric-comma"
                },
            ],
            initComplete: null
        });
        $('#datatable tr:eq(0) th:eq(0)').text("Province");
    }
});