var listLeaveBalanceURL;
var getByEmployeeIdURL;
var getByEmployeeCodeURL;
var saveURL;
//var deleteURL;
var employeeId = 0;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listLeaveBalanceURL = $('#list-leave-balance-url').data('request-url');
    getByEmployeeIdURL = $('#get-by-employeeId-url').data('request-url');
    getByEmployeeCodeURL = $('#get-by-employeeCode-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    //deleteURL = $('#delete-url').data('request-url');

    var eid = getParameterByName('eid');
    if (eid.length > 0 && eid > 0) {
        employeeId = eid;
    }

    $('#EmployeeLeaveInformation_EmployeeId').val(employeeId);
    editItem(employeeId);

    //employeeId = getParameterByName('eid');
    //if (employeeId.length > 0 && employeeId > 0) {
    //    $('#EmployeeLeaveInformation_EmployeeId').val(employeeId);
    //    editItem(employeeId.trim());
    //}
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
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

$("#btnShowDetails").click(function () {
    displayEmployeeInfo($('#EmployeeLeaveInformation_Employee_EmployeeCode').val().trim());    
});

$("#btnSave").click(function () {
    if (validateEmployeeLeaveInformation()) {
        var jsonObject = {
            "Id": $('#EmployeeLeaveInformation_Id').val(),
            "EmployeeId": $('#EmployeeLeaveInformation_EmployeeId').val(),
            "AnnualHolidayCategoryId": $('#EmployeeLeaveInformation_AnnualHolidayCategoryId').val(),
            "WeekendCategoryId": $('#EmployeeLeaveInformation_WeekendCategoryId').val(),
            "LeavePolicyId": $('#EmployeeLeaveInformation_LeavePolicyId').val(),
            "IsActive": $('#EmployeeLeaveInformation_IsActive').is(":checked")
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employeeLeaveInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data != null) {
                    $('#EmployeeLeaveInformation_Id').val(data.Id);
                    $("#hdnIsInsert").val(false);
                    alert('Saved successfully.');
                }
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
});

function editItem(employeeId) {
    $.ajax({
        type: "POST",
        url: getByEmployeeIdURL,
        data: { id: employeeId },
        cache: false,
        success: function (data) {
            setInformation(data);
        },
        error: function () {
            clearData();
        }
    });
}

function displayEmployeeInfo(employeeCode) {
    $.ajax({
        type: "POST",
        url: getByEmployeeCodeURL,
        data: { employeeCode: employeeCode },
        cache: false,
        success: function (data) {
            setInformation(data);
        },
        error: function () {
            clearData();
        }
    });
}

function setInformation(data) {
    if (data != null) {
        if (data.Employee != null) {
            $('#EmployeeLeaveInformation_Employee_EmployeeCode').val(data.Employee.EmployeeCode);
            $('#EmployeeLeaveInformation_Employee_EmployeeName').val(data.Employee.EmployeeName);
            $('#EmployeeLeaveInformation_Employee_DepartmentName').val(data.Employee.DepartmentName);
            $('#EmployeeLeaveInformation_Employee_DesignationName').val(data.Employee.DesignationName);
        }
        $('#EmployeeLeaveInformation_Id').val(data.Id);
        $('#EmployeeLeaveInformation_EmployeeId').val(data.EmployeeId);
        $('#EmployeeLeaveInformation_AnnualHolidayCategoryId').val(data.AnnualHolidayCategoryId);
        $('#EmployeeLeaveInformation_WeekendCategoryId').val(data.WeekendCategoryId);
        $('#EmployeeLeaveInformation_LeavePolicyId').val(data.LeavePolicyId);
        $('#EmployeeLeaveInformation_IsActive').prop("checked", data.IsActive);
        if (data.Id > 0) {
            $("#hdnIsInsert").val(false);
        } else {
            $("#hdnIsInsert").val(true);
        }
        displayLeaveBalanceList(data.EmployeeId);
    }
}

function validateEmployeeLeaveInformation() {
    if (employeeId.length == 0 || employeeId == 0) {
        showMessage('Employee selection is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeLeaveInformation_AnnualHolidayCategoryId').val() == "") {
        showMessage('Annual holiday category selection is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeLeaveInformation_WeekendCategoryId').val() == "") {
        showMessage('Weekend category selection is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeLeaveInformation_LeavePolicyId').val() == "") {
        showMessage('Leave policy selection is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#EmployeeLeaveInformation_Employee_EmployeeName').val("");
    $('#EmployeeLeaveInformation_Employee_DepartmentName').val("");
    $('#EmployeeLeaveInformation_Employee_DesignationName').val("");

    $('#EmployeeLeaveInformation_AnnualHolidayCategoryId').val("");
    $('#EmployeeLeaveInformation_WeekendCategoryId').val("");
    $('#EmployeeLeaveInformation_LeavePolicyId').val("");
    $('#EmployeeLeaveInformation_IsActive').prop("checked", false);
}



