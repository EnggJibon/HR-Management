
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

    displayLeaveRequestTypeList();
});

var displayLeaveRequestTypeList = function () {
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
                    Id: { type: "long", hidden: true },
                    RequestTypeName: { type: "string" },
                    Description: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Id", dir: "asc" }
        ],
        pageSize: 20,
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
        height: 350,
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
            filterable: true,
            hidden: true
        },

        {
            field: "RequestTypeName",
            title: "RequestTypeName",
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
    if (validateLeaveRequestType()) {
        var jsonObject = {
            "Id": $('#LeaveRequestType_Id').val(),
            "RequestTypeName": $('#LeaveRequestType_RequestTypeName').val(),
            "Description": $('#LeaveRequestType_Description').val(),
            "IsActive": $('#LeaveRequestType_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ LeaveRequestType: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function () {
                displayLeaveRequestTypeList();
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
            $('#LeaveRequestType_Id').val(data.Id);
            $('#LeaveRequestType_RequestTypeName').val(data.RequestTypeName);
            $('#LeaveRequestType_Description').val(data.Description);
            $('#LeaveRequestType_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this LeaveRequestType?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayLeaveRequestTypeList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateLeaveRequestType() {
    if ($('#LeaveRequestType_RequestTypeName').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }

    return true;
}

function clearData() {
    $('#LeaveRequestType_Id').val("");
    $('#LeaveRequestType_RequestTypeName').val("");
    $('#LeaveRequestType_Description').val("");
    $("#hdnIsInsert").val(true);
}
