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

    displayLeaveTypeList();
});

var displayLeaveTypeList = function () {
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
                    Id: { type: "int" },
                    LeavePolicyId: { type: "int" },
                    Name: { type: "string" },
                    NumberOfDays: { type: "int" },
                    Description: { type: "string" },
                }
            }
        },
        sort: [
            { field: "LeavePolicyId", dir: "asc" }
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
            field: "LeavePolicyId",
            title: "LeavePolicyId",
            filterable: true,
        },
        {
            field: "Name",
            title: "Name",
            filterable: true,
        },
        {
            field: "NumberOfDays",
            title: "NumberOfDays",
            filterable: false,
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
    if (validateLeaveType()) {
        var jsonObject = {
            "Id": $('#LeaveType_Id').val(),
            "LeavePolicyId": $('#LeavePolicyList').val(),
            "Name": $('#LeaveType_Name').val(),
            "NumberOfDays": $('#LeaveType_NumberOfDays').val(),
            "Description": $('#LeaveType_Description').val(),
            "IsActive": $('#LeaveType_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ leaveType: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayLeaveTypeList();
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
            $('#LeaveType_Id').val(data.Id);
            $('#LeaveType_LeavePolicyId').val(data.LeavePolicyId);
            $('#LeaveType_Name').val(data.Name);
            $('#LeaveType_NumberOfDays').val(data.NumberOfDays);
            $('#LeaveType_Description').val(data.Description);
            $('#LeaveType_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this leaveType?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayLeaveTypeList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateLeaveType() {
    if ($('#LeaveType_LeavePolicyId').val() == "") {
        showMessage('LeavePolicyId is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#LeaveType_Name').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#LeaveType_Id').val("");
    $('#LeaveType_LeavePolicyId').val("");
    $('#LeaveType_Name').val("");
    $('#LeaveType_NumberOfDays').val("");
    $('#LeaveType_Description').val("");
    $('#Address_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}