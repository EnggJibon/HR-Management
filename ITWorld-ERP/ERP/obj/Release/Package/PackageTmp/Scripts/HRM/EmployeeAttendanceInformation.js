var listURL;
var getURL;
var employeeId;
var getByEmployeeCodeURL;
var getByDateURL;

$(document).ready(function () {
    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    getByEmployeeCodeURL = $('#get-by-employeeCode-url').data('request-url');

    $('#EmployeeAttendanceInformation_Date').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $('#EmployeeAttendanceInformation_PunchInTime').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });
    $('#EmployeeAttendanceInformation_PunchOutTime').kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    $('#result').hide();
});

$("#btnSearch").click(function () {
    var employeeCode = $('#EmployeeAttendanceInformation_EmployeeCode').val().trim();
    var date = $('#EmployeeAttendanceInformation_Date').val().trim();
    displayEmployeeInfo(employeeCode);
    displayEmployeeInfowithDate(date)
});

function displayEmployeeInfo(employeeCode) {
    $.ajax({
        type: "POST",
        url: getByEmployeeCodeURL,
        data: { employeeCode: employeeCode,},
        cache: false,
        success: function (data) {
            setInformation(data);
        },
        error: function () {
            clearData();
        }
    });
}

function displayEmployeeInfowithDate(date) {
    $.ajax({
        type: "POST",
        url: getByDateURL,
        data: { date: date, },
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
            $('#EmployeeAttendanceInformation_Employee_EmployeeCode').val(data.Employee.EmployeeCode);
            $('#EmployeeAttendanceInformation_Employee_EmployeeName').val(data.Employee.EmployeeName);
            $('#EmployeeAttendanceInformation_Employee_DepartmentName').val(data.Employee.DepartmentName);
            $('#EmployeeAttendanceInformation_Employee_DesignationName').val(data.Employee.DesignationName);

            var employeeId = data.Employee.Id;
            var month = $('#EmployeeAttendanceInformation_Month').val();
            var year = $('#EmployeeAttendanceInformation_Year').val();
            var date = $('#EmployeeAttendanceInformation_Date').val();
            displayEmployeeAttendanceInformationList(employeeId, month, year, date);
            $('#result').show();
        } else {

        }
    }
}

var displayEmployeeAttendanceInformationList = function (employeeId, month, year, date) {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: listURL + "?employeeId=" + employeeId + "&month=" + month + "&year=" + year + "&date=" + date,
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
                    Date: { type: "date" },
                    PunchInTime: { type: "date" },
                    PunchOutTime: { type: "date" },
                }
            }
        },
        sort: [
        ],
        pageSize: 10,
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
        sortable: false,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        columns: [
        {
            field: "Date",
            title: "Date",
            filterable: false,
            sortable: false,
            format: "{0:dd MMMM, yyyy, dddd}"
        },
        {
            field: "PunchInTime",
            title: "Punch In Time",
            filterable: false,
            sortable: false,
            format: "{0:h:mm:ss tt}",
        },
        {
            field: "PunchOutTime",
            title: "Punch Out Time",
            filterable: false,
            sortable: false,
            format: "{0:h:mm:ss tt}",
        }
        ]
    });
};

function clearData() {
    $('#EmployeeAttendanceInformation_Employee_EmployeeCode').val("");
    $('#EmployeeAttendanceInformation_Month').val("");
    $('#EmployeeAttendanceInformation_Year').val("");
    $('#EmployeeAttendanceInformation_Date').val("");
}



$(document).ready(function () {

    $('#EmployeeAttendanceInformation_Date').data('kendoDatePicker').enable(false);
    $('#EmployeeAttendanceInformation_Month').attr('disabled', false);
    $('#EmployeeAttendanceInformation_Year').attr('disabled', false);

    $("#chkDate").change(function () {

        if ($("#chkDate").is(':checked')) {
            $('#EmployeeAttendanceInformation_Date').data('kendoDatePicker').enable(true);
            $('#EmployeeAttendanceInformation_Month').attr('disabled', true);
            $('#EmployeeAttendanceInformation_Year').attr('disabled', true);
        }
        else {
            $('#EmployeeAttendanceInformation_Date').data('kendoDatePicker').enable(false);
            $('#EmployeeAttendanceInformation_Month').attr('disabled', false);
            $('#EmployeeAttendanceInformation_Year').attr('disabled', false);
        }
    });
});
