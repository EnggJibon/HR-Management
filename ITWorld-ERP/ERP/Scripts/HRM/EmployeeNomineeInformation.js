var listURL;
var getURL;
var saveURL;
var deleteURL;
var employeeId;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    employeeId = getParameterByName('eid');
    if (employeeId.length > 0 && employeeId > 0) {
        $('#EmployeeNomineeInformation_EmployeeId').val(employeeId);
        editItem(employeeId.trim());
    }
    displayEmployeeNomineeInformationList(employeeId.trim());
});


var displayEmployeeNomineeInformationList = function () {
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
                    Name: { type: "string" },
                    Relation: { type: "string" },
                    Occupation: { type: "string" },
                    Organization: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Id", dir: "asc" }
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
            field: "Name",
            title: "Name",
            filterable: false,
        },
        {
            field: "Relation",
            title: "Relation",
            filterable: false,
        },
        {
            field: "Occupation",
            title: "Occupation",
            filterable: false,
        },
        {
            field: "Organization",
            title: "Organization",
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



function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}



$("#btnSave").click(function () {
    if (validateEmployeeNomineeInformation()) {
        var jsonObject = {
            "Id": $('#EmployeeNomineeInformation_Id').val(),
            "EmployeeId": employeeId,
            "Relation": $('#EmployeeNomineeInformation_Relation').val(),
            "Name": $('#EmployeeNomineeInformation_Name').val(),
            "Occupation": $('#EmployeeNomineeInformation_Occupation').val(),
            "Organization": $('#EmployeeNomineeInformation_Organization').val(),
            "NationalId": $('#EmployeeNomineeInformation_NationalId').val(),
            "Phone": $('#EmployeeNomineeInformation_Phone').val(),
            "Mobile": $('#EmployeeNomineeInformation_Mobile').val(),
            "Email": $('#EmployeeNomineeInformation_Email').val(),
            "Passport": $('#EmployeeNomineeInformation_Passport').val(),
            "AddressId": $('#EmployeeNomineeInformation_AddressId').val(),
            "BenefitName": $('#EmployeeNomineeInformation_BenefitName').val(),
            "Percentage": $('#EmployeeNomineeInformation_Percentage').val(),
            "IsActive": $('#EmployeeNomineeInformation_IsActive').is(":checked")
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ EmployeeNomineeInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data != null) {
                    displayEmployeeNomineeInformationList(data.EmployeeId);
                    clearData();
                    $("#hdnIsInsert").val(true);
                    alert('Saved successfully.');
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
            $('#EmployeeNomineeInformation_Id').val(data.Id);
            $('#EmployeeNomineeInformation_EmployeeId').val(data.EmployeeId);
            $('#EmployeeNomineeInformation_Occupation').val(data.Occupation);
            $('#EmployeeNomineeInformation_Organization').val(data.Organization);
            $('#EmployeeNomineeInformation_Relation').val(data.Relation);
            $('#EmployeeNomineeInformation_Name').val(data.Name);
            $('#EmployeeNomineeInformation_NationalId').val(data.NationalId);
            $('#EmployeeNomineeInformation_Phone').val(data.Phone);
            $('#EmployeeNomineeInformation_Mobile').val(data.Mobile);
            $('#EmployeeNomineeInformation_Email').val(data.Email);
            $('#EmployeeNomineeInformation_Passport').val(data.Passport);
            $('#EmployeeNomineeInformation_AddressId').val(data.AddressId);
            $('#EmployeeNomineeInformation_BenefitName').val(data.BenefitName);
            $('#EmployeeNomineeInformation_Percentage').val(data.Percentage);
            $('#EmployeeNomineeInformation_IsActive').prop("checked", data.IsActive);
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
                displayEmployeeNomineeInformationList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}



function validateEmployeeNomineeInformation() {
    if (employeeId.length == 0 || employeeId == 0) {
        showMessage('Employee selection is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeNomineeInformation_Name').val() == "") {
        showMessage('Age is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeNomineeInformation_NationalId').val() == "") {
        showMessage('Blood_Group is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeNomineeInformation_Relation').val() == "") {
        showMessage('Weight is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#EmployeeNomineeInformation_Occupation').val("");
    $('#EmployeeNomineeInformation_Organization').val("");
    $('#EmployeeNomineeInformation_Phone').val("");
    $('#EmployeeNomineeInformation_Mobile').val("");
    $('#EmployeeNomineeInformation_Email').val("");
    $('#EmployeeNomineeInformation_Name').val("");
    $('#EmployeeNomineeInformation_Relation').val("");
    $('#EmployeeNomineeInformation_NationalId').val("");
    $('#EmployeeNomineeInformation_Passport').val("");
    $('#EmployeeNomineeInformation_AddressId').val("");
    $('#EmployeeNomineeInformation_BenefitName').val("");
    $('#EmployeeNomineeInformation_Percentage').val("");
    $('#EmployeeNomineeInformation_IsActive').prop("checked", false);
    
}



