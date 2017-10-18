﻿var listURL;
var getURL;
var saveURL;
var deleteURL;


$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    displayScreenOperationList();
});

var displayScreenOperationList = function () {
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
                    ScreenCode: { type: "string" },
                    ModuleId: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Name", dir: "asc" }
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
            field: "ScreenTitle",
            title: "Screen Title",
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
                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Edit</a>";
                    }
                }
        }
        ]
    });
};


$("#btnSave").click(function () {
    if (validateScreenOperation()) {
        var jsonObject = {
            "Id": $('#ScreenOperation_Id').val(),
            "Name": $('#ScreenOperation_Name').val(),
            "Description": $('#ScreenOperation_Description').val(),
            "ScreenId": $('#ScreenOperation_ScreenId').val(),
            "IsActive": $('#ScreenOperation_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ screenOperation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayScreenOperationList();
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
            $('#ScreenOperation_Id').val(data.Id);
            $('#ScreenOperation_Name').val(data.Name);
            $('#ScreenOperation_Description').val(data.Description);
            $('#ScreenOperation_ScreenId').val(data.ScreenId);
            $('#ScreenOperation_IsActive').prop("checked", data.IsActive);
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
                displayScreenOperationList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}


function validateScreenOperation() {
    if ($('#ScreenOperation_Name').val() == "") {
        showMessage('Screen Operation Name is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#ScreenOperation_Description').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#ScreenOperation_ScreenId').val() == "") {
        showMessage('Screen is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#ScreenOperation_Id').val("");
    $('#ScreenOperation_Name').val("");
    $('#ScreenOperation_Description').val("");
    $('#ScreenOperation_ScreenId').val("");
    $('#ScreenOperation_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}
