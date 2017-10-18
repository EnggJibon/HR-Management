var listURL;
var getURL;
var saveURL;
var employeeId;
var deleteURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);
    listURL = $("#list-url").data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    
    $('#EmployeeEmploymentHistory_StartDate').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $('#EmployeeEmploymentHistory_EndDate').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    employeeId = getParameterByName('eid');
    if (employeeId.length > 0 && employeeId > 0) {
        $('#EmployeeEmploymentHistory_EmployeeId').val(employeeId);
        displayEmployeeEmploymentHistoryList(employeeId.trim());
    }
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

var displayEmployeeEmploymentHistoryList = function (employeeId) {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: listURL + "?employeeId=" + employeeId,
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
                    EmployeeId: { type: "long" },
                    StartDate: { type: "date" },
                    EndDate: { type: "date" },
                    Organization: { type: "string" },
                    OrganizationType: { type: "string" },
                    Position: { type: "string" },
                    Responsibility: { type: "string" }
                }
            }
        },
        sort: [
            {
                field: "Id", dir: "asc"
            }
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
            width: 60,
            hidden: true
        },
        {
            field: "StartDate",
            title: "Start Date",
            filterable: false,
            format: "{0:dd-MMM-yyyy}",
            width: 120
        },
        {
            field: "EndDate",
            title: "End Date",
            filterable: false,
            format: "{0:dd-MMM-yyyy}",
            width: 120
        },
        {
            field: "Organization",
            title: "Organization",
            filterable: false,
            width: 300
        },
        {
            field: "Position",
            title: "Position",
            filterable: false,
            width: 250
        },
        {
            field: "IsActive",
            title: "Status",
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                },
            width: 100
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
    if (validateEmployeeEmploymentHistory()) {
        var jsonObject = {
            "Id": $('#EmployeeEmploymentHistory_Id').val(),
            "EmployeeId": employeeId,
            "StartDate": $('#EmployeeEmploymentHistory_StartDate').val(),
            "EndDate": $('#EmployeeEmploymentHistory_EndDate').val(),
            "Organization": $('#EmployeeEmploymentHistory_Organization').val(),
            "OrganizationType": $('#EmployeeEmploymentHistory_OrganizationType').val(),
            "Position": $('#EmployeeEmploymentHistory_Position').val(),
            "Responsibility": $('#EmployeeEmploymentHistory_Responsibility').val(),
            "IsActive": $('#EmployeeEmploymentHistory_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employeeEmploymentHistory: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data != null) {
                    displayEmployeeEmploymentHistoryList(data.EmployeeId);
                    clearData();
                    $("#hdnIsInsert").val(true);
                    showMessage('Saved successfully.', 'success', 'Success!');
                }
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
            $('#EmployeeEmploymentHistory_Id').val(data.Id);
            $('#EmployeeEmploymentHistory_EmployeeId').val(data.EmployeeId);
            $('#EmployeeEmploymentHistory_StartDate').data("kendoDatePicker").value(data.StartDate);
            $('#EmployeeEmploymentHistory_EndDate').data("kendoDatePicker").value(data.EndDate);
            $('#EmployeeEmploymentHistory_Organization').val(data.Organization);
            $('#EmployeeEmploymentHistory_OrganizationType').val(data.OrganizationType);
            $('#EmployeeEmploymentHistory_Position').val(data.Position);
            $('#EmployeeEmploymentHistory_Responsibility').val(data.Responsibility);
            $('#EmployeeEmploymentHistory_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);            
        },
        error: function () {
            clearData();
        }
    });
}



function deleteItem(id) {
    if (confirm("Are you sure you want to delete this department?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayEmployeeEmploymentHistoryList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateEmployeeEmploymentHistory() {
    if ($('#EmployeeEmploymentHistory_EmployeeId').val() == "") {
        showMessage('Employee Id is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEmploymentHistory_Organization').val() == "") {
        showMessage('Organization is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEmploymentHistory_OrganizationType').val() == "") {
        showMessage('OrganizationType is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEmploymentHistory_Position').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#EmployeeEmploymentHistory_StartDate').val("");
    $('#EmployeeEmploymentHistory_EndDate').val("");
    $('#EmployeeEmploymentHistory_Organization').val("");
    $('#EmployeeEmploymentHistory_OrganizationType').val("");
    $('#EmployeeEmploymentHistory_Position').val("");
    $('#EmployeeEmploymentHistory_Responsibility').val("");
    $('#EmployeeEmploymentHistory_IsActive').prop("checked", false);
}


