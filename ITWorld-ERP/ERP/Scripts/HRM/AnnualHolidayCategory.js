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

    displayAnnualHolidayCategoryList();
});

var displayAnnualHolidayCategoryList = function () {
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
                    Title: { type: "string" },
                    Description: { type: "string" },
                    Year: { type: "long" },
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
            hidden: true
        },
        {
            field: "Title",
            title: "Title",
            filterable: true,
            width: 200,
        },

        {
            field: "Year",
            title: "Year",
            filterable: false,
            width: 150,
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
    if (validateAnnualHolidayCategory()) {
        var jsonObject = {
            "Id": $('#AnnualHolidayCategory_Id').val(),
            "Title": $('#AnnualHolidayCategory_Title').val(),
            "Description": $('#AnnualHolidayCategory_Description').val(),
            "Year": $('#AnnualHolidayCategory_Year').val(),
            "IsActive": $('#AnnualHolidayCategory_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ AnnualHolidayCategory: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function () {
                displayAnnualHolidayCategoryList();
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
            $('#AnnualHolidayCategory_Id').val(data.Id);
            $('#AnnualHolidayCategory_Title').val(data.Title);
            $('#AnnualHolidayCategory_Description').val(data.Description);
            $('#AnnualHolidayCategory_Year').val(data.Year);
            $('#AnnualHolidayCategory_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this weekend category?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayAnnualHolidayCategoryList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateAnnualHolidayCategory() {
    if ($('#AnnualHolidayCategory_Title').val() == "") {
        showMessage('Title is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AnnualHolidayCategory_Description').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#AnnualHolidayCategory_Id').val("");
    $('#AnnualHolidayCategory_Title').val("");
    $('#AnnualHolidayCategory_Description').val("");
    $('#AnnualHolidayCategory_Year').val("");
    $('#AnnualHolidayCategory_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}
