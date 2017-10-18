var getURL;
var saveURL;
var saveURLPersonalInfo;
var listURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    saveURLPersonalInfo = $('#save-url-personal-info').data('request-url');
    listURL = $('#list-url').data('request-url');

    $("#Employee_AppointmentLetterIssueDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#Employee_AppointmentLetterJoinDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#Employee_JoiningDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#Employee_JobConfirmationDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#Employee_PersonalInformation_BirthDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    }).attr("readonly", true);

    $("#Employee_Mobile").intlTelInput({
        preferredCountries: ["us", "bd"]
    });

    $("#Employee_PersonalInformation_Mobile").intlTelInput({
        preferredCountries: ["us", "bd"]
    });

    $("#countries").msDropdown();

    var employeeId = getParameterByName('eid');
    if (employeeId.length > 0 && employeeId > 0) {
        editItem(employeeId.trim());
    }
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

$("#btnSave").click(function () {
    if (validateEmployee()) {
        var jsonObject = {
            "Id": $('#Employee_Id').val(),
            "EmployeeCode": $('#Employee_EmployeeCode').val(),
            "CategoryId": $('#EmployeeCategoryList').val(),
            "DepartmentId": $('#DepartmentList').val(),
            "DesignationId": $('#DesignationList').val(),
            "JobLocation": $('#Employee_JobLocation').val(),
            "WorkLocation": $('#Employee_WorkLocation').val(),
            "SalaryLocation": $('#Employee_SalaryLocation').val(),
            "Mobile": $("#Employee_Mobile").intlTelInput().val(),
            //"Mobile": $('#Employee_Mobile').val(),
            "Email": $('#Employee_Email').val(),
            "AppointmentLetterNo": $('#Employee_AppointmentLetterNo').val(),
            "AppointmentLetterIssueDate": $('#Employee_AppointmentLetterIssueDate').val(),
            "AppointmentLetterJoinDate": $('#Employee_AppointmentLetterJoinDate').val(),
            "JoiningLetterNo": $('#Employee_JoiningLetterNo').val(),
            "Name": $('#Employee_Name').val(),
            "JoiningDate": $('#Employee_JoiningDate').val(),
            "ProbationaryPeriodInDays": $('#Employee_ProbationaryPeriodInDays').val(),
            "JobConfirmationDate": $('#Employee_JobConfirmationDate').val(),
            "SupervisorId": $('#Employee_SupervisorId').val(),
            "ApproverId": $('#Employee_ApproverId').val(),
            "RosterId": $('#Employee_RosterId').val(),
            "IsActive": $('#Employee_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employee: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                location.href = listURL;
                clearData();
                showMessage('Saved successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
});

$("#btnSavePersonalInfo").click(function () {
    if (validatePersonalInfo()) {
        var jsonObject = {
            "Id": $('#Employee_PersonalInformation_Id').val(),
            "EmployeeId": $('#Employee_Id').val(),
            "Title": $('#Employee_PersonalInformation_Title').val(),
            "FirstName": $('#Employee_PersonalInformation_FirstName').val(),
            "MiddleName": $('#Employee_PersonalInformation_MiddleName').val(),
            "LastName": $('#Employee_PersonalInformation_LastName').val(),
            //"BirthDate": $("#Employee_PersonalInformation_BirthDate").data("kendoDatePicker").value(),
            "BirthDate": $('#Employee_PersonalInformation_BirthDate').val(),
            "Gender": $('#Employee_PersonalInformation_Gender').val(),
            "MaritalStatus": $('#Employee_PersonalInformation_MaritalStatus').val(),
            "Religion": $('#Employee_PersonalInformation_Religion').val(),
            "Nationality": $('#Employee_PersonalInformation_Nationality').val(),
            "NationalId": $('#Employee_PersonalInformation_NationalId').val(),
            "Phone": $('#Employee_PersonalInformation_Phone').val(),
            "Mobile": $("#Employee_PersonalInformation_Mobile").intlTelInput().val(),
            //$('#Employee_PersonalInformation_Mobile').val(),
            "Email": $('#Employee_PersonalInformation_Email').val(),
            "Passport": $('#Employee_PersonalInformation_Passport').val(),
            "MotherLanguage": $('#Employee_PersonalInformation_MotherLanguage').val(),
            "AddressId": $('#Employee_PersonalInformation_AddressId').val(),
            "IsActive": $('#Employee_PersonalInformation_IsActive').is(":checked"),

            "Address.Id": $('#Employee_PersonalInformation_Address_Id').val(),
            "Address.Address1": $('#Employee_PersonalInformation_Address_Address1').val(),
            "Address.Address2": $('#Employee_PersonalInformation_Address_Address2').val(),
            "Address.CountryId": $('#countries option:selected').val(),
            //$('#Employee_PersonalInformation_Address_CountryId').val(),
            "Address.State": $('#Employee_PersonalInformation_Address_State').val(),
            "Address.City": $('#Employee_PersonalInformation_Address_City').val(),
            "Address.PostalCode": $('#Employee_PersonalInformation_Address_PostalCode').val(),
        };

        $.ajax({
            type: "POST",
            url: saveURLPersonalInfo,
            data: JSON.stringify({ personalInformation: jsonObject, isInsert: $("#hdnIsInsertPersonalInfo").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                location.href = listURL;
                clearPersonalInfoData();
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
            showEmployeeData(data);
            showPersonalInfoData(data);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

var showEmployeeData = function (data) {
    $('#Employee_Id').val(data.Id);
    $('#Employee_EmployeeCode').val(data.EmployeeCode);
    $('#Employee_PersonalInformation_EmployeeCode').val(data.EmployeeCode);
    $('#EmployeeCategoryList').val(data.CategoryId);
    $('#DepartmentList').val(data.DepartmentId);
    $('#DesignationList').val(data.DesignationId);
    $('#Employee_JobLocation').val(data.JobLocation);
    $('#Employee_WorkLocation').val(data.WorkLocation);
    $('#Employee_SalaryLocation').val(data.SalaryLocation);
    $("#Employee_Mobile").intlTelInput("setNumber", data.Mobile);
    $('#Employee_Email').val(data.Email);
    $('#Employee_AppointmentLetterNo').val(data.AppointmentLetterNo);

    //var appointmentLetterIssueDate = new Date(parseInt(data.AppointmentLetterIssueDate.substr(6)));
    //$('#Employee_AppointmentLetterIssueDate').val(appointmentLetterIssueDate.toDateString('dd-M-yyyy'));

    //var appointmentLetterJoinDate = new Date(parseInt(data.AppointmentLetterJoinDate.substr(6)));
    //$('#Employee_AppointmentLetterJoinDate').val(appointmentLetterJoinDate.toDateString('dd-M-yyyy'));

    $("#Employee_AppointmentLetterIssueDate").data("kendoDatePicker").value(data.AppointmentLetterIssueDate);
    $("#Employee_AppointmentLetterJoinDate").data("kendoDatePicker").value(data.AppointmentLetterJoinDate);

    $('#Employee_JoiningLetterNo').val(data.JoiningLetterNo);

    //var joiningDate = new Date(parseInt(data.JoiningDate.substr(6)));
    //$('#Employee_JoiningDate').val(joiningDate.toDateString('dd-M-yyyy'));
    $("#Employee_JoiningDate").data("kendoDatePicker").value(data.JoiningDate);

    $('#Employee_ProbationaryPeriodInDays').val(data.ProbationaryPeriodInDays);

    //var jobConfirmationDate = new Date(parseInt(data.JobConfirmationDate.substr(6)));
    //$('#Employee_JobConfirmationDate').val(jobConfirmationDate.toDateString('dd-M-yyyy'));
    $("#Employee_JobConfirmationDate").data("kendoDatePicker").value(data.JobConfirmationDate);

    $('#Employee_SupervisorId').val(data.SupervisorId);
    $('#Employee_ApproverId').val(data.ApproverId);
    $('#Employee_RosterId').val(data.RosterId);

    $('#Employee_IsActive').prop("checked", data.IsActive);
    $("#hdnIsInsert").val(false);
};

var showPersonalInfoData = function (data) {
    $('#Employee_PersonalInformation_Id').val(data.PersonalInformation.Id);
    $('#Employee_PersonalInformation_EmployeeId').val(data.PersonalInformation.EmployeeId);
    $('#Employee_PersonalInformation_Title').val(data.PersonalInformation.Title);
    $('#Employee_PersonalInformation_FirstName').val(data.PersonalInformation.FirstName);
    $('#Employee_PersonalInformation_MiddleName').val(data.PersonalInformation.MiddleName);
    $('#Employee_PersonalInformation_LastName').val(data.PersonalInformation.LastName);
    $("#Employee_PersonalInformation_BirthDate").data("kendoDatePicker").value(data.PersonalInformation.BirthDate);
    $('#Employee_PersonalInformation_Gender').val(data.PersonalInformation.Gender);
    $('#Employee_PersonalInformation_MaritalStatus').val(data.PersonalInformation.MaritalStatus);
    $('#Employee_PersonalInformation_Religion').val(data.PersonalInformation.Religion);
    $('#Employee_PersonalInformation_Nationality').val(data.PersonalInformation.Nationality);
    $('#Employee_PersonalInformation_NationalId').val(data.PersonalInformation.NationalId);
    $('#Employee_PersonalInformation_Phone').val(data.PersonalInformation.Phone);
    $("#Employee_PersonalInformation_Mobile").intlTelInput("setNumber", data.PersonalInformation.Mobile);
    //$('#Employee_PersonalInformation_Mobile').val(data.PersonalInformation.Mobile);
    $('#Employee_PersonalInformation_Email').val(data.PersonalInformation.Email);
    $('#Employee_PersonalInformation_Passport').val(data.PersonalInformation.Passport);
    $('#Employee_PersonalInformation_MotherLanguage').val(data.PersonalInformation.MotherLanguage);
    $('#Employee_PersonalInformation_IsActive').prop("checked", data.PersonalInformation.IsActive);

    if (data.PersonalInformation.Address != null) {
        $('#Employee_PersonalInformation_Address_Id').val(data.PersonalInformation.Address.Id);
        $('#Employee_PersonalInformation_Address_Address1').val(data.PersonalInformation.Address.Address1);
        $('#Employee_PersonalInformation_Address_Address2').val(data.PersonalInformation.Address.Address2);
        $("#countries").msDropdown().data("dd").set("value", data.PersonalInformation.Address.CountryId);
        //$('#Employee_PersonalInformation_Address_CountryId').val(data.PersonalInformation.Address.CountryId);
        $('#Employee_PersonalInformation_Address_State').val(data.PersonalInformation.Address.State);
        $('#Employee_PersonalInformation_Address_City').val(data.PersonalInformation.Address.City);
        $('#Employee_PersonalInformation_Address_PostalCode').val(data.PersonalInformation.Address.PostalCode);
    }
    $("#hdnIsInsertPersonalInfo").val(false);
};

function validateEmployee() {
    if ($('#Employee_EmployeeCode').val() == "") {
        showMessage('Employee code is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeCategoryList').val() == "") {
        showMessage('Employee Category is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#DepartmentList').val() == "") {
        showMessage('Department is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#DesignationList').val() == "") {
        showMessage('Designation is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_Mobile').val() == "") {
        showMessage('Mobile is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_Email').val() == "") {
        showMessage('Email is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_JobLocation').val() == "") {
        showMessage('Job Location is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_WorkLocation').val() == "") {
        showMessage('Work Location is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_SalaryLocation').val() == "") {
        showMessage('Salary Location is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_AppointmentLetterNo').val() == "") {
        showMessage('Appointment Letter No is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_JoiningLetterNo').val() == "") {
        showMessage('Joining Letter No is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_JobConfirmationDate').val() == "") {
        showMessage('Confirmation Date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_SupervisorId').val() == "") {
        showMessage('Supervisor Id is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_ApproverId').val() == "") {
        showMessage('Approver Id is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_RosterId').val() == "") {
        showMessage('Roster Id is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function validatePersonalInfo() {
    if ($('#Employee_PersonalInformation_EmployeeCode').val() == "") {
        showMessage('Employee code is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Title').val() == "") {
        showMessage('Title is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_FirstName').val() == "") {
        showMessage('First Name is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_MiddleName').val() == "") {
        showMessage('Middle Name is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_LastName').val() == "") {
        showMessage('Last Name is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_BirthDate').val() == "") {
        showMessage('Birth Date is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Religion').val() == "") {
        showMessage('Religion is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Gender').val() == "") {
        showMessage('Gender is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_MaritalStatus').val() == "") {
        showMessage('Marital Status is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Nationality').val() == "") {
        showMessage('Nationality is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_NationalId').val() == "") {
        showMessage('National Id is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Mobile').val() == "") {
        showMessage('Mobile is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_MotherLanguage').val() == "") {
        showMessage('Mother Language is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Address_Address1').val() == "") {
        showMessage('Present Address is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#Employee_PersonalInformation_Address_Address2').val() == "") {
        showMessage('Permanent Address is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#countries').val() == "") {
        showMessage('Country is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#Employee_EmployeeCode').val("");
    $('#EmployeeCategoryList').val("");
    $('#DepartmentList').val("");
    $('#DesignationList').val("");
    $('#Employee_JobLocation').val("");
    $('#Employee_WorkLocation').val("");
    $('#Employee_SalaryLocation').val("");
    $('#Employee_WorkLocation').val("");
    $('#Employee_Mobile').val("");
    $('#Employee_Email').val("");
    $('#Employee_AppointmentLetterNo').val("");
    $('#Employee_AppointmentLetterIssueDate').val("");
    $('#Employee_AppointmentLetterJoinDate').val("");
    $('#Employee_JoiningLetterNo').val("");
    $('#Employee_JoiningDate').val("");
    $('#Employee_ProbationaryPeriodInDays').val("");
    $('#Employee_JobConfirmationDate').val("");
    $('#Employee_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}

function clearPersonalInfoData() {
    $('#Employee_PersonalInformation_Id').val("");
    $('#Employee_PersonalInformation_EmployeeId').val("");
    $('#Employee_PersonalInformation_Title').val("");
    $('#Employee_PersonalInformation_FirstName').val("");
    $('#Employee_PersonalInformation_MiddleName').val("");
    $('#Employee_PersonalInformation_LastName').val("");
    $('#Employee_PersonalInformation_BirthDate').val("");
    $('#Employee_PersonalInformation_Gender').val("");
    $('#Employee_PersonalInformation_MaritalStatus').val("");
    $('#Employee_PersonalInformation_Religion').val("");
    $('#Employee_PersonalInformation_Nationality').val("");
    $('#Employee_PersonalInformation_NationalId').val("");
    $('#Employee_PersonalInformation_Phone').val("");
    $('#Employee_PersonalInformation_Mobile').val("");
    $('#Employee_PersonalInformation_Email').val("");
    $('#Employee_PersonalInformation_Passport').val("");
    $('#Employee_PersonalInformation_MotherLanguage').val("");
    $('#Employee_PersonalInformation_AddressId').val("");
    $('#Employee_PersonalInformation_IsActive').prop("checked", false);

    $('#Employee_PersonalInformation_Address_Address1').val("");
    $('#Employee_PersonalInformation_Address_Address2').val("");
    $('#Employee_PersonalInformation_Address_CountryId').val("");
    $('#Employee_PersonalInformation_Address_State').val("");
    $('#Employee_PersonalInformation_Address_City').val("");
    $('#Employee_PersonalInformation_Address_PostalCode').val("");
    $("#hdnIsInsertPersonalInfo").val(true);
}
