
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


    $('#AnnualHoliday_Date').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $('#AnnualHoliday_Date').kendoTimePicker({
        format: 'dd-MMM-yyyy',
    });

    displayAnnualHolidayList();
});

var displayAnnualHolidayList = function () {
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
                    AnnualHolidayCategoryId: { type: "string" },
                    Date: { type: "date" },
                    Day: { type: "string" },
                    Type: { type: "string" },
                    Title: { type: "string" },
                    Description: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Id", dir: "asc" }
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
            field: "AnnualHolidayCategoryId",
            title: "Annual Holiday Category",
            filterable: true,
        },

        {
            field: "Date",
            title: "Date",
            filterable: true,
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
        },

        {
            field: "Day",
            title: "Day",
            filterable: true,
        },

        {
            field: "Type",
            title: "Type",
            filterable: true,
        },

        {
            field: "Title",
            title: "Title",
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
    if (validateAnnualHoliday()) {
        var jsonObject = {
            "Id": $('#AnnualHoliday_Id').val(),
            "AnnualHolidayCategoryId": $('#AnnualHolidayCategoryList').val(),
            "Date": $('#AnnualHoliday_Date').val(),
            "Day": $('#AnnualHolidayDayList').val(),
            "Type": $('#AnnualHolidayTypeList').val(),
            "Title": $('#AnnualHoliday_Title').val(),
            "Description": $('#AnnualHoliday_Description').val(),
            "IsActive": $('#AnnualHoliday_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ annualHoliday: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayAnnualHolidayList();
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
            $('#AnnualHoliday_Id').val(data.Id);
            $('#AnnualHolidayCategoryList').val(data.AnnualHolidayCategoryId);
            $('#AnnualHoliday_Date').data("kendoDatePicker").value(data.Date);
            $('#AnnualHolidayDayList').val(data.Day);
            $('#AnnualHolidayTypeList').val(data.Type);
            $('#AnnualHoliday_Title').val(data.Title);
            $('#AnnualHoliday_Description').val(data.Description);
            $('#AnnualHoliday_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this AnnualHoliday?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayAnnualHolidayList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateAnnualHoliday() {
    if ($('#AnnualHolidayCategoryList').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AnnualHolidayDayList').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AnnualHoliday_Date').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#AnnualHolidayTypeList').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#AnnualHoliday_Id').val("");
    $('#AnnualHolidayCategoryList').val("");
    $('#AnnualHoliday_Date').val("");
    $('#AnnualHolidayDayList').val("");
    $('#AnnualHolidayTypeList').val("");
    $('#AnnualHoliday_Title').val("");
    $('#AnnualHoliday_Description').val("");
    $('#AnnualHoliday_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}

