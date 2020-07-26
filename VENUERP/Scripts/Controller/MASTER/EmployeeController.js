var result = $('#EmployeeController').val();

if (result == "EmployeeController")
{
    bindDatatable();
}
 

function bindDatatable() {

    datatable = $('#tblStudent').DataTable({
        "sAjaxSource": "/Employee/GetData",
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
                "data": "Position",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Location",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "Age",
                "autoWidth": true,
                "searchable": true
            }, {
                "data": "StartDateString",
                "autoWidth": true,
                "searchable": true
            }, {
                "data": "Salary",
                "autoWidth": true,
                "searchable": true
            },
        ]
    });
}