var getEmployeesURL;
var getURL;
var saveURL;
var deleteURL;
var userListURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    getEmployeesURL = $('#getEmployees-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');
    userListURL = $('#user-list-url').data('request-url');

    $("#UserInformation_LastPasswordChangedDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });
    $("#UserInformation_LastLockedDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });
    $('#UserInformation_LastPasswordChangedDate').data('kendoDatePicker').enable(false);
    $('#UserInformation_LastLockedDate').data('kendoDatePicker').enable(false);

    var userId = getParameterByName('uid');
    if (userId.length > 0 && userId > 0) {
        editItem(userId.trim());
    }
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$("#UserInformation_Employee_CategoryId").change(function () {
    loadEmployees($(this).val(), 0, false);
});

function loadEmployees(employeeCategoryId, selectedValue, disabled) {
    var employeeDropdown = $('#UserInformation_EmployeeId');
    employeeDropdown.prop('disabled', 'disabled');
    employeeDropdown.empty();
    var firstItem = '<option value="">--Select--</option>';
    employeeDropdown.append(firstItem);

    if (employeeCategoryId > 0) {
        $.ajax({
            type: "POST",
            url: getEmployeesURL,
            data: { categoryId: employeeCategoryId },
            cache: false,
            success: function (data) {
                if (data.length > 0) {
                    employeeDropdown.removeAttr("disabled");
                    $.each(data, function (i, employee) {
                        var item = '<option value="' + employee.Id + '">' + employee.EmployeeCode + '</option>';
                        employeeDropdown.append(item);
                    });
                    if (selectedValue > 0) {
                        $('#UserInformation_EmployeeId').val(selectedValue);
                    }
                    if (disabled) {
                        employeeDropdown.prop('disabled', 'disabled');
                    } else {
                        employeeDropdown.removeAttr("disabled");
                    }
                }
            },
            error: function () {
                showMessage('Employee information loading failed.', 'error', 'Error!');
            }
        });
    }
}

$("#btnSave").click(function () {
    if (validateUserInformation()) {
        var jsonObject = {
            "Id": $('#UserInformation_Id').val(),
            "UserTypeId": $('#UserInformation_UserTypeId').val(),
            "RoleId": $('#UserInformation_RoleId').val(),
            "EmployeeId": $('#UserInformation_EmployeeId').val(),
            "Username": $('#UserInformation_Username').val(),
            "Password": $('#UserInformation_Password').val(),
            "ConfirmPassword": $('#UserInformation_ConfirmPassword').val(),
            "PasswordAgeLimit": $('#UserInformation_PasswordAgeLimit').val(),
            "WrongPasswordTryLimit": $('#UserInformation_WrongPasswordTryLimit').val(),
            "IsPasswordChanged": $('#UserInformation_IsPasswordChanged').is(":checked"),
            "IsLocked": $('#UserInformation_IsLocked').is(":checked"),
            "IsSuperAdmin": $('#UserInformation_IsSuperAdmin').is(":checked"),
            "IsActive": $('#UserInformation_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ userInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                clearData();
                window.location = userListURL;
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
            $('#UserInformation_Id').val(data.Id);
            $('#UserInformation_UserTypeId').val(data.UserTypeId);
            $('#UserInformation_RoleId').val(data.RoleId);
            if (data.EmployeeId > 0) {
                $('#UserInformation_Employee_CategoryId').val(data.CategoryId);
                loadEmployees(data.CategoryId, data.EmployeeId, true);
            }
            $('#UserInformation_Username').val(data.Username);
            $('#UserInformation_Password').val("********************");
            $('#UserInformation_ConfirmPassword').val("********************");
            $('#UserInformation_PasswordAgeLimit').val(data.PasswordAgeLimit);
            $('#UserInformation_WrongPasswordTryLimit').val(data.WrongPasswordTryLimit);
            $("#UserInformation_LastPasswordChangedDate").data("kendoDatePicker").value(data.LastPasswordChangedDate);
            $("#UserInformation_LastLockedDate").data("kendoDatePicker").value(data.LastLockedDate);
            $('#UserInformation_IsPasswordChanged').prop("checked", data.IsPasswordChanged);
            $('#UserInformation_IsLocked').prop("checked", data.IsLocked);
            $('#UserInformation_IsSuperAdmin').prop("checked", data.IsSuperAdmin);
            $('#UserInformation_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);

            $("#UserInformation_Username").prop("disabled", true);
            $('#UserInformation_Password').prop("disabled", true);
            $('#UserInformation_ConfirmPassword').prop("disabled", true);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this user information?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateUserInformation() {
    if ($('#UserInformation_UserTypeId').val() == "") {
        showMessage('User type selection is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_RoleId').val() == "") {
        showMessage('Role selection is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_EmployeeId').val() == "") {
        showMessage('Employee selection is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_Username').val() == "") {
        showMessage('Username is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_Password').val() == "") {
        showMessage('Password is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_ConfirmPassword').val() == "") {
        showMessage('Confirm Password is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_Password').val() != $('#UserInformation_ConfirmPassword').val()) {
        showMessage('Password does not match.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_PasswordAgeLimit').val() == "") {
        showMessage('Password age limit is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#UserInformation_WrongPasswordTryLimit').val() == "") {
        showMessage('Wrong password try limit is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#UserInformation_Id').val("");
    $('#UserInformation_UserTypeId').val("");
    $('#UserInformation_RoleId').val("");
    $('#UserInformation_Employee_CategoryId').val("");
    $('#UserInformation_EmployeeId').val("");
    $('#UserInformation_Username').val("");
    $('#UserInformation_Password').val("");
    $('#UserInformation_ConfirmPassword').val("");
    $('#UserInformation_PasswordAgeLimit').val("");
    $('#UserInformation_WrongPasswordTryLimit').val("");
    $('#UserInformation_IsPasswordChanged').prop("checked", false);
    $('#UserInformation_LastPasswordChangedDate').val("");
    $('#UserInformation_IsLocked').prop("checked", false);
    $('#UserInformation_LastLockedDate').val("");
    $('#UserInformation_IsSuperAdmin').prop("checked", false);
    $('#UserInformation_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}



