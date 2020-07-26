var result = $('#IQuotationMastersController').val();

if (result == "IQuotationMastersController") {

    bindDatatable();
}


function bindDatatable() {

    datatable = $('#tblStudent').DataTable({
        "sAjaxSource": "/IQuotationMasters/GetData",
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
                    var linkEdit = '<a href="Print?id=' + row.Id + '">Print</a>';
                    return linkEdit;
                },
                "bSortable": false
            },
            {
                "mRender": function (data, type, row) {
                    var linkView = '<a href="SendMail?id=' + row.Id + '">SendMail</a>';
                    return linkView;
                },
                "bSortable": false
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
                    var linkDelete = '<a href="Delete?id=' + row.Id + '">Delete</a>';
                    return linkDelete;
                },
                "bSortable": false
            }
        ]
    });
}