var listURL;
var getURL;
var saveURL;
var deleteURL;
var screenListURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');
    screenListURL = $('#get-screen-list-url').data('request-url');

    $('#RoleWiseScreenPermission_ModuleId').change(function () {
        loadScreenDropdownList($(this).val());
    });

    displayRoleWiseScreenPermissionList();
});

function loadScreenDropdownList(moduleId) {
    $.ajax({
        type: "POST",
        url: screenListURL,
        data: JSON.stringify({ moduleId: moduleId }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        cache: false,
        success: function (data) {
            $('#RoleWiseScreenPermission_ScreenId').get(0).options.length = 0;
            $('#RoleWiseScreenPermission_ScreenId').get(0).options[0] = new Option("--Select--", "");
            $.map(data, function (item) {
                $('#RoleWiseScreenPermission_ScreenId').get(0).options[$('#RoleWiseScreenPermission_ScreenId').get(0).options.length] = new Option(item.Text, item.Value);
            });
        },
        error: function () {
            $('#RoleWiseScreenPermission_ScreenId').get(0).options.length = 0;
            $('#RoleWiseScreenPermission_ScreenId').get(0).options[0] = new Option('--Select--', 0);
        }
    });
}

var displayRoleWiseScreenPermissionList = function () {
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
                    ScreenId: { type: "long" },
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
            width: 60,
            field: "Id",
            title: "Id",
        },
        {
            field: "RoleName",
            title: "Role",
            filterable: true,
        },
        {
            field: "ScreenTitle",
            title: "Screen",
            filterable: true,
        },
        {
            field: "CanRead",
            title: "Can Read",
            filterable: false,
            width: 110,
            template: function (dataItem) {
                return dataItem.CanRead == "Yes" ? "Yes" : "No";
            }
        },
        {
            field: "CanCreate",
            title: "Can Create",
            filterable: false,
            width: 110,
            template: function (dataItem) {
                return dataItem.CanCreate == "Yes" ? "Yes" : "No";
            }
        },
        {
            field: "CanUpdate",
            title: "Can Update",
            filterable: false,
            width: 110,
            template: function (dataItem) {
                return dataItem.CanUpdate == "Yes" ? "Yes" : "No";
            }
        },
        {
            field: "CanDelete",
            title: "Can Delete",
            filterable: false,
            width: 110,
            template: function (dataItem) {
                return dataItem.CanDelete == "Yes" ? "Yes" : "No";
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
            width: 110,
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
    if (validateRoleWiseScreenPermission()) {

        var accessRight = $('#chkCanRead').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanCreate').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanUpdate').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanDelete').is(":checked") ? '1' : '0';

        var jsonObject = {
            "Id": $('#RoleWiseScreenPermission_Id').val(),
            "RoleId": $('#RoleWiseScreenPermission_RoleId').val(),
            "ScreenId": $('#RoleWiseScreenPermission_ScreenId').val(),
            "IsActive": $('#RoleWiseScreenPermission_IsActive').is(":checked"),
            "AccessRight": accessRight,
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ roleWiseScreenPermission: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayRoleWiseScreenPermissionList();
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
            $('#RoleWiseScreenPermission_Id').val(data.Id);
            $('#RoleWiseScreenPermission_RoleId').val(data.RoleId);
            $('#RoleWiseScreenPermission_ModuleId').val(data.ModuleId);
            //loadScreenDropdownList(data.ModuleId);
            //$('#RoleWiseScreenPermission_ScreenId').val(data.ScreenId);
            $('#RoleWiseScreenPermission_ScreenId').get(0).options[0] = new Option(data.ScreenTitle, data.ScreenId);
            $('#chkCanRead').prop('checked', data.CanRead == 'Yes' ? true : false);
            $('#chkCanCreate').prop('checked', data.CanCreate == 'Yes' ? true : false);
            $('#chkCanUpdate').prop('checked', data.CanUpdate == 'Yes' ? true : false);
            $('#chkCanDelete').prop('checked', data.CanDelete == 'Yes' ? true : false);
            $('#RoleWiseScreenPermission_IsActive').prop("checked", data.IsActive);
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
                displayRoleWiseScreenPermissionList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateRoleWiseScreenPermission() {
    if ($('#RoleWiseScreenPermission_RoleId').val() == "") {
        showMessage('Role is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#RoleWiseScreenPermission_ModuleId').val() == "") {
        showMessage('Module is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#RoleWiseScreenPermission_ScreenId').val() == "") {
        showMessage('Screen is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}


function clearData() {
    $('#RoleWiseScreenPermission_Id').val("");
    $('#RoleWiseScreenPermission_RoleId').val("");
    $('#RoleWiseScreenPermission_ScreenId').get(0).options[0] = new Option('--Select--', 0);
    $('#RoleWiseScreenPermission_ModuleId').val("");
    $('#chkCanRead').prop('checked', false);
    $('#chkCanCreate').prop('checked', false);
    $('#chkCanUpdate').prop('checked', false);
    $('#chkCanDelete').prop('checked', false);
    $('#RoleWiseScreenPermission_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}