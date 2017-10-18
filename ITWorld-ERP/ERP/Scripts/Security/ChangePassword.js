var getByEmployeeCodeURL;
var saveURL;

$(document).ready(function () {
    getByEmployeeCodeURL = $('#get-by-employeeCode-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    displayEmployeeInfoByEmployeeCode("");
    $('#divPassword').hide();
});

$("#btnSave").click(function () {
    if (validateUserInformation()) {
        var jsonObject = {
            "Id": $('#UserInformation_Id').val(),
            "Username": $('#UserInformation_Username').val(),
            "OldPassword": $('#UserInformation_OldPassword').val(),
            "NewPassword": $('#UserInformation_NewPassword').val(),
            "ConfirmPassword": $('#UserInformation_ConfirmPassword').val(),
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ userInformation: jsonObject }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                clearData();
                showMessage('Saved successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
});

function validateUserInformation() {
    if ($('#UserInformation_Username').val() == "") {
        showMessage('Username is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_OldPassword').val() == "") {
        showMessage('Old password is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_NewPassword').val() == "") {
        showMessage('New Password is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_ConfirmPassword').val() == "") {
        showMessage('Confirm Password is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#UserInformation_Id').val("");
    $('#UserInformation_EmployeeId').val("");
    $('#UserInformation_Username').val("");
    $('#UserInformation_OldPassword').val("");
    $('#UserInformation_NewPassword').val("");
    $('#UserInformation_ConfirmPassword').val("");
}

$("#btnShowDetails").click(function () {
    displayEmployeeInfoByEmployeeCode($('#UserInformation_Employee_EmployeeCode').val().trim());
});

function displayEmployeeInfoByEmployeeCode(employeeCode) {
    $.ajax({
        type: "POST",
        url: getByEmployeeCodeURL,
        data: { employeeCode: employeeCode },
        cache: false,
        success: function (data) {
            setInformation(data);
        },
        error: function () {
            $('#UserInformation.Id').val("");
            $('#UserInformation_Employee_EmployeeId').val("");
            $('#UserInformation_Employee_EmployeeCode').val("");
            $('#UserInformation_Employee_EmployeeName').val("");
            $('#UserInformation_Employee_DepartmentName').val("");
            $('#UserInformation_Employee_DesignationName').val("");
        }
    });
}

function setInformation(data) {
    if (data == null) {
        showMessage('Employee information is not found.', 'warning', 'Warning!');
    } else {
        if (data.Id == null || data.Id <= 0) {
            $('#divPassword').hide();
            $('#UserInformation_Employee_EmployeeCode').prop('disabled', 'disabled');
            $('#btnShowDetails').hide();
            showMessage('User information is not found.', 'warning', 'Warning!');
        } else {
            $('#divPassword').show();
            //if (data.UserTypeId == 4) {
            //    $('#UserInformation_Employee_EmployeeCode').prop('disabled', 'disabled');
            //    $('#btnShowDetails').hide();
            //} else {
            //    $('#UserInformation_Employee_EmployeeCode').prop('disabled', false);
            //    $('#btnShowDetails').show();
            //}
            $('#UserInformation.Id').val(data.Id);
        }
        $('#UserInformation_Employee_EmployeeId').val(data.Employee.Id);
        $('#UserInformation_Employee_EmployeeCode').val(data.Employee.EmployeeCode);
        $('#UserInformation_Employee_EmployeeName').val(data.Employee.EmployeeName);
        $('#UserInformation_Employee_DepartmentName').val(data.Employee.DepartmentName);
        $('#UserInformation_Employee_DesignationName').val(data.Employee.DesignationName);
    }
}


