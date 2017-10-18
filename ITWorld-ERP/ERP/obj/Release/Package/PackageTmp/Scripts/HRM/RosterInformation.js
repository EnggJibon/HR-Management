var listURL;
var getURL;
var saveURL;
var deleteURL;


//$(".datePicker").kendoDatePicker({
//    format: "dd/MM/yyyy",
//    value: new Date(),
//    animation: {
//        close: {
//            effects: "fadeOut zoom:out",
//            duration: 300
//        },
//        open: {
//            effects: "fadeIn zoom:in",
//            duration: 300
//        }
//    }
//});


$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    $("#RosterInformation_StartTime").kendoTimePicker({
        interval: 15
    });
    $("#RosterInformation_EndTime").kendoTimePicker({
        interval: 15
    });

    displayRosterInformationList();
});

var displayRosterInformationList = function () {
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
                    StartTime: { type: "string" },
                    EndTime: { type: "string" },
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
            field: "StartTime",
            title: "StartTime",
            type: "string",
            //format: "{HH:mm tt}",
            filterable: false,
        },
        {
            field: "EndTime",
            title: "EndTime",
            type: "string",
            //format: "{HH:mm tt}",
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
    if (validateRosterInformation()) {
        var jsonObject = {
            "Id": $('#RosterInformation_Id').val(),
            "Name": $('#RosterInformation_Name').val(),
            "StartTime": $("#RosterInformation_StartTime").val(),
            "EndTime": $("#RosterInformation_EndTime").val(),
            "Description": $('#RosterInformation_Description').val(),
            "IsActive": $('#RosterInformation_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ rosterInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayRosterInformationList();
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
            $('#RosterInformation_Id').val(data.Id);
            $('#RosterInformation_Name').val(data.Name);
            $("#RosterInformation_StartTime").data("kendoTimePicker").value(data.StartTime);
            $("#RosterInformation_EndTime").data("kendoTimePicker").value(data.EndTime);
            $('#RosterInformation_Description').val(data.Description);
            $('#RosterInformation_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this Roster Schedule?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayRosterInformationList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateRosterInformation() {
    if ($('#RosterInformation_Name').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#RosterInformation_StartTime').val() == "") {
        showMessage('StartTime is required.', 'warning', 'Warning!');
        return false;
    }

    else if ($('#RosterInformation_EndTime').val() == "") {
        showMessage('EndTime is required.', 'warning', 'Warning!');
        return false;
    }

    return true;
}

function clearData() {
    $('#RosterInformation_Id').val("");
    $('#RosterInformation_Name').val("");
    $('#RosterInformation_StartTime').val("");
    $('#RosterInformation_EndTime').val("");
    $('#RosterInformation_Description').val("");
    $('#RosterInformation_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}
