
var listURL;
var getURL;
var getByEmployeeIdURL;
var getByEmployeeCodeURL;
var saveURL;
var deleteURL;
var employeeId = 0;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    getByEmployeeIdURL = $('#get-by-employeeId-url').data('request-url');
    getByEmployeeCodeURL = $('#get-by-employeeCode-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    $("#EmployeeLeaveRequest_RequestDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $("#EmployeeLeaveRequest_LeaveFrom").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $("#EmployeeLeaveRequest_LeaveTo").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $("#EmployeeLeaveRequest_AdjustmentDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    showEmployeeLeaveDetailsForNewEntry();

    var eid = getParameterByName('eid');
    if (eid.length > 0 && eid > 0) {
        employeeId = eid;
    }
    $('#EmployeeLeaveRequest_EmployeeId').val(employeeId);
    displayEmployeeInfoByEmployeeId(employeeId);

    $("#EmployeeLeaveRequest_LeaveFrom").change(function () {
        calculateDays();
    });

    $("#EmployeeLeaveRequest_LeaveTo").change(function () {
        calculateDays();
    });

    setAdjustmentIssues();
    $("#EmployeeLeaveRequest_IsAdjustment").change(function () {
        setAdjustmentIssues();
    });

    //DisableFields(false);
    hideEmployeeLeaveDetails();
});

var setAdjustmentIssues = function () {
    if ($('#EmployeeLeaveRequest_IsAdjustment').is(":checked")) {
        $('#EmployeeLeaveRequest_AdjustmentDate').data('kendoDatePicker').enable(true);
        $("#EmployeeLeaveRequest_AdjustmentDate").prop("readonly", true);
        //$("#EmployeeLeaveRequest_AdjustmentDate").attr('readonly', 'readonly');
    } else {
        $("#EmployeeLeaveRequest_AdjustmentDate").data("kendoDatePicker").value("");
        $('#EmployeeLeaveRequest_AdjustmentDate').data('kendoDatePicker').enable(false);
    }
};

var calculateDays = function () {
    var fromDate = $("#EmployeeLeaveRequest_LeaveFrom").data("kendoDatePicker").value(); //new Date($("#EmployeeLeaveRequest_LeaveFrom").val());
    var toDate = $("#EmployeeLeaveRequest_LeaveTo").data("kendoDatePicker").value();
    if (fromDate != undefined && toDate != undefined) {
        if (fromDate > toDate) {
            $('#EmployeeLeaveRequest_DaysCount').val("");
            alert("From date cannot be greater than to date");
        } else {
            //var daysCount = Math.floor(toDate.getDate() - fromDate.getDate());  // 86400000)
            var daysCount = Math.round((toDate - fromDate) / 1000 / 60 / 60 / 24);
            $('#EmployeeLeaveRequest_DaysCount').val(daysCount + 1);
        }
    }
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

var displayLeaveRequestList = function (employeeId) {
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
                    Id: { type: "long" },
                    RequestDate: { type: "date" },
                    LeaveFrom: { type: "date" },
                    LeaveTo: { type: "date" },
                }
            }
        },
        sort: [
            { field: "Id", dir: "asc" }
        ],
        pageSize: 20,
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false
    });

    $("#grid").kendoGrid({
        selectable: "row",
        dataSource: dataSource,
        filterable: {
            extra: true
        },
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false,
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
            filterable: false,
            sortable: false,
        },
        {
            field: "RequestDate",
            title: "Request Date",
            filterable: false,
            sortable: false,
            format: "{0:dd-MMM-yyyy}"
        },
        {
            field: "LeaveFrom",
            title: "From",
            filterable: false,
            sortable: false,
            format: "{0:dd-MMM-yyyy}"
        },
        {
            field: "LeaveTo",
            title: "To",
            filterable: false,
            sortable: false,
            format: "{0:dd-MMM-yyyy}"
        },
        {
            field: "IsHalfDay",
            title: "Half Day",
            filterable: false,
            sortable: false,
            template:
                function (dataItem) {
                    return dataItem.IsHalfDay ? "Yes" : "No";
                }
        },
        {
            field: "IsAdjustment",
            title: "Adjustment",
            filterable: false,
            sortable: false,
            template:
                function (dataItem) {
                    return dataItem.IsAdjustment ? "Yes" : "No";
                }
        },
        {
            field: "ApprovalStatusName",
            title: "Status",
            width: 120
        },
        {
            width: 60,
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
        },
        {
            width: 60,
            field: "Id",
            title: "Delete",
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null && dataItem.ApprovalStatusId != 3) {
                        return "<a class='btn-link' onclick='deleteItem(" + dataItem.Id + ")'>Delete</a>";
                    } else {
                        return "";
                    }
                }
        }
        ]
    });
};

$("#btnShowDetails").click(function () {
    displayEmployeeInfoByEmployeeCode($('#EmployeeLeaveRequest_Employee_EmployeeCode').val().trim());
});

$("#btnSave").click(function () {
    if (validateLeaveRequest()) {
        var jsonObject = {
            "Id": $('#EmployeeLeaveRequest_Id').val(),
            "EmployeeId": $('#EmployeeLeaveRequest_EmployeeId').val(),
            "RequestDate": $('#EmployeeLeaveRequest_RequestDate').val(),
            "LeaveTypeId": $('#EmployeeLeaveRequest_LeaveTypeId').val(),
            "IsHalfDay": $('#EmployeeLeaveRequest_IsHalfDay').is(":checked"),
            "LeaveFrom": $('#EmployeeLeaveRequest_LeaveFrom').val(),
            "LeaveTo": $('#EmployeeLeaveRequest_LeaveTo').val(),
            "Purpose": $('#EmployeeLeaveRequest_Purpose').val(),
            "DaysCount": $('#EmployeeLeaveRequest_DaysCount').val(),
            "IsAdjustment": $('#EmployeeLeaveRequest_IsAdjustment').is(":checked"),
            "AdjustmentDate": $('#EmployeeLeaveRequest_AdjustmentDate').val(),
            "ApprovalStatusId": $('#EmployeeLeaveRequest_ApprovalStatusId').val(),
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employeeLeaveRequest: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data.Success) {
                    displayLeaveRequestList($('#EmployeeLeaveRequest_EmployeeId').val());
                    clearData();
                    hideEmployeeLeaveDetails();
                    showMessage('Saved successfully.', 'success', 'Success!');
                } else {
                    showMessage(data.Message, 'warning', 'Warning!');
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
            $('#EmployeeLeaveRequest_Id').val(data.Id);
            $('#EmployeeLeaveRequest_EmployeeId').val(data.EmployeeId);
            $("#EmployeeLeaveRequest_RequestDate").data("kendoDatePicker").value(data.RequestDate);
            $('#EmployeeLeaveRequest_LeaveTypeId').val(data.LeaveTypeId);
            $('#EmployeeLeaveRequest_IsHalfDay').prop("checked", data.IsHalfDay);
            $("#EmployeeLeaveRequest_LeaveFrom").data("kendoDatePicker").value(data.LeaveFrom);
            $("#EmployeeLeaveRequest_LeaveTo").data("kendoDatePicker").value(data.LeaveTo);
            $('#EmployeeLeaveRequest_Purpose').val(data.Purpose);
            $('#EmployeeLeaveRequest_DaysCount').val(data.DaysCount);
            $('#EmployeeLeaveRequest_IsAdjustment').prop("checked", data.IsAdjustment);
            $("#EmployeeLeaveRequest_AdjustmentDate").data("kendoDatePicker").value(data.AdjustmentDate);
            $('#EmployeeLeaveRequest_ApprovalStatusId').val(data.ApprovalStatusId);
            $("#hdnIsInsert").val(false);
            $("#employeeLeaveDetails").show();

            if (data.ApprovalStatusId == 3) {
                DisableFields(true);
            } else {
                DisableFields(false);
            }
        },
        error: function () {
            clearData();
        }
    });
}

function displayEmployeeInfoByEmployeeId(employeeId) {
    $.ajax({
        type: "POST",
        url: getByEmployeeIdURL,
        data: { employeeId: employeeId },
        cache: false,
        success: function (data) {
            setInformation(data);
            displayLeaveRequestList(employeeId);
        },
        error: function () {
            $('#EmployeeLeaveRequest_Employee_EmployeeName').val("");
            $('#EmployeeLeaveRequest_Employee_DepartmentName').val("");
            $('#EmployeeLeaveRequest_Employee_DesignationName').val("");
        }
    });
}

function displayEmployeeInfoByEmployeeCode(employeeCode) {
    $.ajax({
        type: "POST",
        url: getByEmployeeCodeURL,
        data: { employeeCode: employeeCode },
        cache: false,
        success: function (data) {
            setInformation(data);
            displayLeaveRequestList(data.Id);
        },
        error: function () {
            $('#EmployeeLeaveRequest_Employee_EmployeeName').val("");
            $('#EmployeeLeaveRequest_Employee_DepartmentName').val("");
            $('#EmployeeLeaveRequest_Employee_DesignationName').val("");
        }
    });
}

function setInformation(data) {
    if (data != null) {
        $('#EmployeeLeaveRequest_EmployeeId').val(data.Id);
        $('#EmployeeLeaveRequest_Employee_EmployeeCode').val(data.EmployeeCode);
        $('#EmployeeLeaveRequest_Employee_EmployeeName').val(data.EmployeeName);
        $('#EmployeeLeaveRequest_Employee_DepartmentName').val(data.DepartmentName);
        $('#EmployeeLeaveRequest_Employee_DesignationName').val(data.DesignationName);
    }
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this leave request?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                displayLeaveRequestList($('#EmployeeLeaveRequest_EmployeeId').val());
                clearData();
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateLeaveRequest() {
    if ($('#EmployeeLeaveRequest_EmployeeId').val() == "") {
        showMessage('Employee Id is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_RequestDate').val() == "") {
        showMessage('Request date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_LeaveTypeId').val() == "") {
        showMessage('Leave type selection is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_LeaveFrom').val() == "") {
        showMessage('From date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_LeaveTo').val() == "") {
        showMessage('To date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_Purpose').val() == "") {
        showMessage('Purpose is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_DaysCount').val() == "") {
        showMessage('Day selection is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_IsAdjustment').val() == true && $('#EmployeeLeaveRequest_AdjustmentDate').val() == "") {
        showMessage('Adjustment date is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#EmployeeLeaveRequest_Id').val("");
    //$('#EmployeeLeaveRequest_EmployeeId').val("");
    $('#EmployeeLeaveRequest_LeaveTypeId').val("");
    $('#EmployeeLeaveRequest_IsHalfDay').prop("checked", false);
    $("#EmployeeLeaveRequest_LeaveFrom").data("kendoDatePicker").value("");
    $("#EmployeeLeaveRequest_LeaveTo").data("kendoDatePicker").value("");
    $('#EmployeeLeaveRequest_Purpose').val("");
    $('#EmployeeLeaveRequest_DaysCount').val("");
    $('#EmployeeLeaveRequest_IsAdjustment').prop("checked", false);
    $('#EmployeeLeaveRequest_AdjustmentDate').val("");
    $("#hdnIsInsert").val(true);
}

function showEmployeeLeaveDetailsForNewEntry() {
    $("#employeeLeaveDetails").show();

    clearData();

    $("#EmployeeLeaveRequest_RequestDate").data("kendoDatePicker").value(new Date());
    $('#EmployeeLeaveRequest_ApprovalStatusId').val(1);

    $('#EmployeeLeaveRequest_RequestDate').data('kendoDatePicker').enable(false);
    $('#EmployeeLeaveRequest_LeaveFrom').prop('readonly', true);
    $('#EmployeeLeaveRequest_LeaveTo').prop('readonly', true);
    $('#EmployeeLeaveRequest_AdjustmentDate').prop('readonly', true);
}

function hideEmployeeLeaveDetails() {
    $("#employeeLeaveDetails").hide();
}

function DisableFields(isDisable) {
    $("#employeeLeaveDetails").prop('disabled', isDisable);
    $("#EmployeeLeaveRequest_LeaveTypeId").prop('disabled', isDisable);
    $("#EmployeeLeaveRequest_IsHalfDay").prop('disabled', isDisable);
    $('#EmployeeLeaveRequest_LeaveFrom').data('kendoDatePicker').enable(!isDisable);
    $('#EmployeeLeaveRequest_LeaveTo').data('kendoDatePicker').enable(!isDisable);

    $("#EmployeeLeaveRequest_Purpose").prop('disabled', isDisable);
    $("#EmployeeLeaveRequest_IsAdjustment").prop('disabled', isDisable);
    $("#EmployeeLeaveRequest_AdjustmentDate").prop("disabled", isDisable);
    $("#btnSave").prop('disabled', isDisable);
    $("#btnReset").prop('disabled', isDisable);

    $('#EmployeeLeaveRequest_LeaveFrom').prop('readonly', true);
    $('#EmployeeLeaveRequest_LeaveTo').prop('readonly', true);
    $('#EmployeeLeaveRequest_AdjustmentDate').prop('readonly', true);
}
