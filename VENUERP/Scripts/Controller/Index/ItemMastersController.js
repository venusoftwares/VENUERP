﻿var result = $('#ItemMastersController').val();

if (result == "ItemMastersController") {

    bindDatatable();
}


function bindDatatable() {

    datatable = $('#tblStudent').DataTable({
        "sAjaxSource": "/ItemMasters/GetData",
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
                "data": "HsnCode",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Brand",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Category",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Item",
                "autoWidth": true,
                "searchable": true
            },
            {
                "mRender": function (data, type, row) {
                    var linkEdit = '<a href="Edit?id=' + row.Id + '">Edit</a>';
                    return linkEdit;
                }
            },
            {
                "mRender": function (data, type, row) {
                    var linkView = '<a href="Details?id=' + row.Id + '">View</a>';
                    return linkView;
                }
            },
            {
                "mRender": function (data, type, row) {
                    var linkDelete = '<a href="Delete?id=' + row.Id + '">Delete</a>';
                    return linkDelete;
                }
            }
        ]
    });
}