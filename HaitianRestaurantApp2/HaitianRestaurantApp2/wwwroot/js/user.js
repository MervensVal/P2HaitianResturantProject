var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $(`#tblData`).DataTable({
        "ajax": {
            "url":"/Admin/User/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "phoneNumber", "width": "20%" },
            { "data": "role", "width": "20%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        // user is currently locked
                        return `
                            <div class="text-center">
                                <a onclick = LockorUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-lock-open"></i> Unlock
                                </a>
                            </div>
                        `;
                    } else
                    {
                        // user is currently unlocked
                        return `
                            <div class="text-center">
                                <a onclick = LockorUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="fas fa-lock"></i> Lock
                                </a>
                            </div>
                        `;
                    }
                }, "width": "20%"
            }
        ]
    })
}

function LockorUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}

/*
 *
AJAX is a technique for creating fast and dynamic web pages. 
AJAX allows web pages to be updated asynchronously by exchanging 
small amounts of data with the server behind the scenes. 
This means that it is possible to update parts of a web page, 
without reloading the whole page.

AJAX calls are one method to load personalized content separately 
from the rest of the HTML document


AJAX uses both a browser built-in XMLHttpRequest object to get 
data from the web server and JavaScript and HTML DOM to display 
that content to the user. Despite the name “AJAX” these calls can 
also transport data as plain text or JSON instead of XML.

AJAX calls use a JavaScript snippet to load dynamic content. 
As a basic example you could configure a page counter that 
changed each time a page is reloaded by programming a snippet
that is loaded after the main content:
 * */