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

    $("#AdditionalScreenPermission_StartDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $("#AdditionalScreenPermission_EndDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $('#AdditionalScreenPermission_ModuleId').change(function () {
        loadScreenDropdownList($(this).val());
    });

    displayAdditionalScreenPermissionList();
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
            $('#AdditionalScreenPermission_ScreenId').get(0).options.length = 0;
            $('#AdditionalScreenPermission_ScreenId').get(0).options[0] = new Option("--Select--", "");
            $.map(data, function (item) {
                $('#AdditionalScreenPermission_ScreenId').get(0).options[$('#AdditionalScreenPermission_ScreenId').get(0).options.length] = new Option(item.Text, item.Value);
            });
        },
        error: function () {
            alert("Error");
        }
    });
}

var displayAdditionalScreenPermissionList = function () {
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
                    ScreenId: { type: "long" },
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
            width: 50,
            field: "Id",
            title: "Id",
        },
        {
            width: 180,
            field: "UserName",
            title: "User",
            filterable: true,
        },
        {
            width: 180,
            field: "ScreenTitle",
            title: "Screen",
            filterable: false,
        },
        {
            width: 120,
            field: "StartDate",
            title: "Start Date",
            filterable: false,
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
        },
        {
            width: 120,
            field: "EndDate",
            title: "End Date",
            filterable: false,
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
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
            width: 100,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                }
        },
        {
            width: 100,
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
    if (validateAdditionalScreenPermission()) {

        var accessRight = $('#chkCanRead').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanCreate').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanUpdate').is(":checked") ? '1' : '0';
        accessRight += $('#chkCanDelete').is(":checked") ? '1' : '0';

        var jsonObject = {
            "Id": $('#AdditionalScreenPermission_Id').val(),
            "UserId": $('#AdditionalScreenPermission_UserId').val(),
            "ScreenId": $('#AdditionalScreenPermission_ScreenId').val(),
            "StartDate": $('#AdditionalScreenPermission_StartDate').val(),
            "EndDate": $('#AdditionalScreenPermission_EndDate').val(),
            "IsActive": $('#AdditionalScreenPermission_IsActive').is(":checked"),
            "AccessRight": accessRight,
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ additionalScreenPermission: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayAdditionalScreenPermissionList();
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
            $('#AdditionalScreenPermission_Id').val(data.Id);
            $('#AdditionalScreenPermission_UserId').val(data.UserId);
            $('#AdditionalScreenPermission_ModuleId').val(data.ModuleId);
            $("#AdditionalScreenPermission_StartDate").data("kendoDatePicker").value(data.StartDate);
            $("#AdditionalScreenPermission_EndDate").data("kendoDatePicker").value(data.EndDate);
            $('#AdditionalScreenPermission_IsActive').prop("checked", data.IsActive);
            $('#AdditionalScreenPermission_ScreenId').get(0).options[0] = new Option(data.ScreenTitle, data.ScreenId);
            $('#chkCanRead').prop('checked', data.CanRead == 'Yes' ? true : false);
            $('#chkCanCreate').prop('checked', data.CanCreate == 'Yes' ? true : false);
            $('#chkCanUpdate').prop('checked', data.CanUpdate == 'Yes' ? true : false);
            $('#chkCanDelete').prop('checked', data.CanDelete == 'Yes' ? true : false);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}



function validateAdditionalScreenPermission() {
    if ($('#AdditionalScreenPermission_UserId').val() == "") {
        showMessage('User is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalScreenPermission_ScreenId').val() == "") {
        showMessage('Screen is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalScreenPermission_ModuleId').val() == "") {
        showMessage('Module is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalScreenPermission_StartDate').val() == "") {
        showMessage('Start Date Operation Title is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AdditionalScreenPermission_EndDate').val() == "") {
        showMessage('End Date Operation Title is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}


function deleteItem(id) {
    if (confirm("Are you sure you want to delete this Screen Permission?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayAdditionalScreenPermissionList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function clearData() {
    $('#AdditionalScreenPermission_Id').val("");
    $('#AdditionalScreenPermission_UserId').val("");
    $('#AdditionalScreenPermission_ModuleId').val("");
    $('#AdditionalScreenPermission_ScreenId').get(0).options[0] = new Option('--Select--', 0);
    $('#AdditionalScreenPermission_StartDate').val("");
    $('#AdditionalScreenPermission_EndDate').val("");
    $('#chkCanRead').prop('checked', false);
    $('#chkCanCreate').prop('checked', false);
    $('#chkCanUpdate').prop('checked', false);
    $('#chkCanDelete').prop('checked', false);
    $('#AdditionalScreenPermission_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}