var result = $('#CashPaymentController').val();

if (result == "CashPaymentController") {

    bindDatatable();
}


function bindDatatable() {

    datatable = $('#tblStudent').DataTable({
        "sAjaxSource": "/CashPayment/GetData",
        "bServerSide": true,
        "bProcessing": true,
        "bSearchable": true,
        "order": [[1, 'asc']],
        "language": {
            "emptyTable": "No record found.",
            "processing":
                '<i class="fa fa-spinner fa-spin fa-3x fa-fw" style="color:#2a2b2b;"></i><span class="sr-only">Loading...</span> '
        },
        "columns": [

            {
                "data": "No",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Date",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Amount",
                "autoWidth": true,
                "searchable": true
            },
            {
                "mRender": function (data, type, row) {
                    var linkEdit = '<a href="Edit?id=' + row.Id + '">Edit</a>';
                    return linkEdit;
                },
                "bSortable": false
            },
            
            {
                "mRender": function (data, type, row) {
                    var linkView = '<a href="Details?id=' + row.Id + '">View</a>';
                    return linkView;
                },
                "bSortable": false
            },
            {
                "mRender": function (data, type, row) {
                    var linkDelete = '<a href="Delete?id=' + row.Id + '">Delete</a>';
                    return linkDelete;
                },
                "bSortable": false
            }
        ]
    });
}