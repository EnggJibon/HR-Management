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

    displayRoleWiseOperationPermissionList();
});

var displayRoleWiseOperationPermissionList = function () {
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
                    RoleId: { type: "long" },
                    ScreenOperationId: { type: "long" },
                    HaveAccess: { type: "byte" },
                }
            }
        },
        sort: [
            { field: "RoleId", dir: "asc" }
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
        },
        {
            field: "RoleName",
            title: "Role",
            filterable: true,
        },
        {
            field: "ScreenOperationName",
            title: "Screen Operation Name",
            filterable: false,
        },
        {
            field: "HaveAccess",
            title: "Have Access",
            filterable: false,
            template: function (dataItem) {
                return dataItem.HaveAccess ? "Active" : "Inactive";
            }
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
    if (validateRoleWiseOperationPermission()) {

        var jsonObject = {
            "Id": $('#RoleWiseOperationPermission_Id').val(),
            "RoleId": $('#RoleWiseOperationPermission_RoleId').val(),
            "ScreenOperationId": $('#RoleWiseOperationPermission_ScreenOperationId').val(),
            "HaveAccess": $('#RoleWiseOperationPermission_HaveAccess').is(":checked"),
            "IsActive": $('#RoleWiseOperationPermission_IsActive').is(":checked"),
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ roleWiseOperationPermission: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayRoleWiseOperationPermissionList();
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
            $('#RoleWiseOperationPermission_Id').val(data.Id);
            $('#RoleWiseOperationPermission_RoleId').val(data.RoleId);
            $('#RoleWiseOperationPermission_ScreenOperationId').val(data.ScreenOperationId);
            $('#RoleWiseOperationPermission_HaveAccess').prop("checked", data.HaveAccess);
            $('#RoleWiseOperationPermission_IsActive').prop("checked", data.IsActive);
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
                displayRoleWiseOperationPermissionList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}


function validateRoleWiseOperationPermission() {
    if ($('#RoleWiseOperationPermission_RoleId').val() == "") {
        showMessage('Role is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#RoleWiseOperationPermission_ScreenOperationId').val() == "") {
        showMessage('Screen Operation Name is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#RoleWiseOperationPermission_Id').val("");
    $('#RoleWiseOperationPermission_RoleId').val("");
    $('#RoleWiseOperationPermission_ScreenOperationId').val("");
    $('#RoleWiseOperationPermission_HaveAccess').prop("checked", false);
    $('#RoleWiseOperationPermission_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}