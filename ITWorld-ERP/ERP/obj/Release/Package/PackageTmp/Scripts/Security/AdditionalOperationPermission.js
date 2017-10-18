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

    $("#AdditionalOperationPermission_StartDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $("#AdditionalOperationPermission_EndDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    displayAdditionalOperationPermissionList();
});


var displayAdditionalOperationPermissionList = function () {
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
                    UserId: { type: "long" },
                    ScreenOperationId: { type: "long" },
                    StartDate: { type: "date" },
                    EndDate: { type: "date" },
                }
            }
        },
        sort: [
            { field: "UserId", dir: "asc" }
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
            field: "Username",
            title: "User",
            filterable: true,
        },
        {
            field: "ScreenOperationName",
            title: "Screen Operation",
            filterable: false,
        },
        {
            field: "StartDate",
            title: "Start Date",
            filterable: false,
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
        },
        {
            field: "EndDate",
            title: "End Date",
            filterable: false,
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
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
    if (validateAdditionalOperationPermission()) {
        var jsonObject = {
            "Id": $('#AdditionalOperationPermission_Id').val(),
            "UserId": $('#AdditionalOperationPermission_UserId').val(),
            "ScreenOperationId": $('#AdditionalOperationPermission_ScreenOperationId').val(),
            "StartDate": $('#AdditionalOperationPermission_StartDate').val(),
            "EndDate": $('#AdditionalOperationPermission_EndDate').val(),
            "IsActive": $('#AdditionalOperationPermission_IsActive').is(":checked"),
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ additionalOperationPermission: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayAdditionalOperationPermissionList();
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
            $('#AdditionalOperationPermission_Id').val(data.Id);
            $('#AdditionalOperationPermission_UserId').val(data.UserId);
            $('#AdditionalOperationPermission_ScreenOperationId').val(data.ScreenOperationId);
            $("#AdditionalOperationPermission_StartDate").data("kendoDatePicker").value(data.StartDate);
            $("#AdditionalOperationPermission_EndDate").data("kendoDatePicker").value(data.EndDate);
            $('#AdditionalOperationPermission_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}



function deleteItem(id) {
    if (confirm("Are you sure you want to delete this role?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayAdditionalOperationPermissionList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}


function validateAdditionalOperationPermission() {
    if ($('#AdditionalOperationPermission_UserId').val() == "") {
        showMessage('User is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalOperationPermission_ScreenOperationId').val() == "") {
        showMessage('Screen Operation Title is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalOperationPermission_StartDate').val() == "") {
        showMessage('Start Date Operation Title is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalOperationPermission_EndDate').val() == "") {
        showMessage('End Date Operation Title is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#AdditionalOperationPermission_Id').val("");
    $('#AdditionalOperationPermission_UserId').val("");
    $('#AdditionalOperationPermission_ScreenOperationId').val("");
    $('#AdditionalOperationPermission_StartDate').val("");
    $('#AdditionalOperationPermission_EndDate').val("");
    $('#RoleWiseScreenPermission_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}