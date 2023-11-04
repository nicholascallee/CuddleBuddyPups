$(document).ready(function () {
    var url = window.location.search;

    if (url.includes("in review")) {
        loadDataTable("in review");
    }
    else {
        if (url.includes("approved")) {
            loadDataTable("approved");
        }
        else {
            if (url.includes("InProgress")) {
                loadDataTable("InProgress");
            }
            else {
                if (url.includes("Finalized")) {
                    loadDataTable("Finalized");
                }
                else {
                    loadDataTable("all");
                }
            }
        }
    }



});


function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/application/getall?status=' + status },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'dog', "width": "15%" },
            { data: 'name', "width": "10%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'applicationStatus', "width": "10%" },
            { data: 'paymentStatus', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/application/details?applicationId=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i></a>
                    </div>`
                },
                "width": "10%"
            }
        ]
    });
}