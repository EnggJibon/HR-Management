var requestListURL;
var listLeaveBalanceURL;
var getURL;
var approveURL;
var employeeId = 0;
var getByEmployeeIdURL;

$(document).ready(function () {
    requestListURL = $('#request-list-url').data('request-url');
    listLeaveBalanceURL = $('#list-leave-balance-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    approveURL = $('#approve-url').data('request-url');
    getByEmployeeIdURL = $('#get-by-employeeId-url').data('request-url');

    $("#EmployeeLeaveRequest_RequestDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#EmployeeLeaveRequest_LeaveFrom").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#EmployeeLeaveRequest_LeaveTo").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#EmployeeLeaveRequest_AdjustmentDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $('#EmployeeLeaveRequest_RequestDate').data('kendoDatePicker').enable(false);
    $('#EmployeeLeaveRequest_LeaveFrom').data('kendoDatePicker').enable(false);
    $('#EmployeeLeaveRequest_LeaveTo').data('kendoDatePicker').enable(false);
    $('#EmployeeLeaveRequest_AdjustmentDate').data('kendoDatePicker').enable(false);

    $("#incoming-leave-request :input").attr("disabled", true);
    //$('#EmployeeLeaveRequest_ApprovalStatusId').attr("disabled", true);
    //$('#requestDetail').css('display', 'none');
    $('#requestDetail').hide();

    var eid = getParameterByName('eid');
    if (eid.length > 0 && eid > 0) {
        employeeId = eid;
    }

    $('#EmployeeLeaveRequest_EmployeeId').val(employeeId);
    displayIncomingLeaveRequestList(employeeId);
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

var displayIncomingLeaveRequestList = function () {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: requestListURL,
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
            width: 150,
            field: "Id",
            title: "Select",
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (typeof dataItem.Id != "string") {
                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Details</a>";
                    }
                }
        }
        ]
    });
};

function recommend() {
    $('#EmployeeLeaveRequest_ApprovalStatusId').val(2);
    save();
};
function approve() {
    $('#EmployeeLeaveRequest_ApprovalStatusId').val(3);
    save();
};
function reject() {
    $('#EmployeeLeaveRequest_ApprovalStatusId').val(4);
    save();
};

function save() {
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
            url: approveURL,
            data: JSON.stringify({ employeeLeaveRequest: jsonObject }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data.Success) {
                    displayIncomingLeaveRequestList();
                    $('#requestDetail').hide();
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
};

function editItem(id) {
    $.ajax({
        type: "POST",
        url: getURL,
        data: { id: id },
        cache: false,
        success: function (data) {
            $('#requestDetail').show();
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
            displayEmployeeInfoByEmployeeId(data.EmployeeId);
            displayLeaveBalanceList(data.EmployeeId);
        },
        error: function () {
        }
    });
}

function validateLeaveRequest() {
    if ($('#EmployeeLeaveRequest_EmployeeId').val() == "") {
        showMessage('EmployeeID is required.', 'warning', 'Warning!');
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
        showMessage('Days count is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_IsAdjustment').val() == true && $('#EmployeeLeaveRequest_AdjustmentDate').val() == "") {
        showMessage('Adjustment date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveRequest_ApprovalStatusId').val() == "") {
        showMessage('Approval status selection is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

var displayLeaveBalanceList = function (employeeId) {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: listLeaveBalanceURL + "?employeeId=" + employeeId,
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
                    LeaveTypeId: { type: "long" },
                    LeaveTypeName: { type: "string" },
                    AllocatedDays: { type: "int" },
                    EnjoyedDays: { type: "int" },
                    RemainingDays: { type: "int" },
                }
            }
        },
        sort: [
            { field: "Id", dir: "asc" }
        ],
        pageSize: 5,
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false
    });

    $("#leaveBalance").kendoGrid({
        selectable: "row",
        dataSource: dataSource,
        filterable: {
            extra: true
        },
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false,
        height: 150,
        groupable: false,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
        {
            field: "LeaveTypeId",
            title: "Leave Type Id",
            filterable: false,
            sortable: false,
            hidden: true
        },
        {
            field: "LeaveTypeName",
            title: "Leave Type",
            filterable: false,
            sortable: false,
        },
        {
            field: "AllocatedDays",
            title: "Allocated Days",
            filterable: false,
            sortable: false,
        },
        {
            field: "EnjoyedDays",
            title: "Enjoyed",
            filterable: false,
            sortable: false,
        },
        {
            field: "RemainingDays",
            title: "Remaining",
            filterable: false,
            sortable: false,
        }
        ]
    });
};

function displayEmployeeInfoByEmployeeId(employeeId) {
    $.ajax({
        type: "POST",
        url: getByEmployeeIdURL,
        data: { employeeId: employeeId },
        cache: false,
        success: function (data) {
            setInformation(data);
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


