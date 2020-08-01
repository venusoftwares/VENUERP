var result = $('#CustomerMastersController').val();

if (result == "CustomerMastersController") {

    bindDatatable();
}


function bindDatatable() {

    datatable = $('#tblStudent').DataTable({
        "sAjaxSource": "/CustomerMasters/GetData",
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
                "data": "Name",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Address",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "MobileNo",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "GSTNo",
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