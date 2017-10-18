
var listURL;
var getURL;
var saveURL;
var deleteURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    displayDepartmentList();
});

var displayDepartmentList = function () {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: listURL,
                type: "POST"
            }
        },
        schema: {
            data: function (data) {
                return data.Data;
            },
            total: function (data) {
                return data.Total;
            },
            model: {
                fields: {
                    Id: { type: "long" },
                    Name: { type: "string" },
                    Description: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Name", dir: "asc" }
        ],
        pageSize: 10,
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true
    });

    $("#grid").kendoGrid({
        selectable: "row",
        dataSource: dataSource,
        filterable: {
            extra: true
        },
        serverPaging: true,
        serverFiltering: true,
        serverSorting: true,
        height: 200,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
        {
            field: "Id",
            title: "Id",
        },
        {
            field: "Name",
            title: "Name",
            filterable: true,
        },
        {
            field: "Description",
            title: "Description",
            filterable: false,
        },
        {
            field: "IsActive",
            title: "Status",
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                }
        },
        {
            width: 150,
            field: "Id",
            title: "Select",
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (typeof dataItem.Id != "string") {
                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Edit</a> | <a class='btn-link-delete' onclick='deleteItem(" + dataItem.Id + ")'>Delete</a>";
                    }
                }
        }
        ]
    });
};

$("#btnSave").click(function () {
    if (validateDepartment()) {
        var jsonObject = {
            "Id": $('#Department_Id').val(),
            "Name": $('#Department_Name').val(),
            "Description": $('#Department_Description').val(),
            "IsActive": $('#Department_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ department: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayDepartmentList();
                clearData();
                showMessage('Saved successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
});

function editItem(id) {
    $.ajax({
        type: "POST",
        url: getURL,
        data: { id: id },
        cache: false,
        success: function (data) {
            $('#Department_Id').val(data.Id);
            $('#Department_Name').val(data.Name);
            $('#Department_Description').val(data.Description);
            $('#Department_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this department?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayDepartmentList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateDepartment() {
    if ($('#Department_Name').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#Department_Description').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#Department_Id').val("");
    $('#Department_Name').val("");
    $('#Department_Description').val("");
    $('#Department_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}

