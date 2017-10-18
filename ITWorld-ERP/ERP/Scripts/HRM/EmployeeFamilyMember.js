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
        $('#EmployeeFamilyMember_EmployeeId').val(employeeId);
        editItem(employeeId.trim());
        displayEmployeeFamilyMemberList(employeeId.trim());
    }

});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}




var displayEmployeeFamilyMemberList = function (employeeId) {
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
                    Relation: { type: "date" },
                    Name: { type: "date" },
                    Qualification: { type: "string" },
                    Occupation: { type: "string" },
                    Organization: { type: "string" },
                    JobLocation: { type: "string" },
                    NationalId: { type: "string" },
                    Phone: { type: "string" },
                    Mobile: { type: "string" },
                    Email: { type: "string" },
                    Passport: { type: "string" },
                    AddressId: { type: "string" },
                    Designation: { type: "string" }
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
            filterable: true,
            hidden:true
         
        },

        {
            field: "Name",
            title: "Name",
            filterable: true,
            width: 200
        },
        {
            field: "Relation",
            title: "Relation",
            filterable: true,
            width: 120
        },

        {
            field: "Qualification",
            title: "Qualification",
            filterable: false,
            width: 120
        },
        //{
        //    field: "Occupation",
        //    title: "Occupation",
        //    filterable: false,
        //    width: 250
        //},
        {
            field: "Organization",
            title: "Organization",
            filterable: true,
            width: 250
        },
        {
            field: "Designation",
            title: "Designation",
            filterable: false,
            width: 120
        },
        {
            field: "JobLocation",
            title: "JobLocation",
            filterable: false,
            width: 150
        },
        {
            field: "NationalId",
            title: "National Id",
            filterable: false,
            width: 100
        },
        {
            field: "Phone",
            title: "Phone",
            filterable: false,
            width: 100
        },

        {
            field: "Email",
            title: "Phone",
            filterable: false,
            width: 200
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
    if (validateEmployeeFamilyMember()) {
        var jsonObject = {
            "Id": $('#EmployeeFamilyMember_Id').val(),
            "EmployeeId": employeeId,
            "Relation": $('#EmployeeFamilyMember_Relation').val(),
            "Name": $('#EmployeeFamilyMember_Name').val(),
            "Qualification": $('#EmployeeFamilyMember_Qualification').val(),
            "Occupation": $('#EmployeeFamilyMember_Occupation').val(),
            "Organization": $('#EmployeeFamilyMember_Organization').val(),
            "Designation": $('#EmployeeFamilyMember_Designation').val(),
            "JobLocation": $('#EmployeeFamilyMember_JobLocation').val(),
            "NationalId": $('#EmployeeFamilyMember_NationalId').val(),
            "Phone": $('#EmployeeFamilyMember_Phone').val(),
            "Mobile": $('#EmployeeFamilyMember_Mobile').val(),
            "Email": $('#EmployeeFamilyMember_Email').val(),
            "Passport": $('#EmployeeFamilyMember_Passport').val(),
            "AddressId": $('#EmployeeFamilyMember_AddressId').val(),
            "IsActive": $('#EmployeeFamilyMember_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employeeFamilyMember: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data != null) {
                    displayEmployeeFamilyMemberList(data.EmployeeId);
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
            $('#EmployeeFamilyMember_Id').val(data.Id);
            $('#EmployeeFamilyMember_EmployeeId').val(data.EmployeeId);
            $('#EmployeeFamilyMember_Relation').val(data.Relation);
            $('#EmployeeFamilyMember_Name').val(data.Name);
            $('#EmployeeFamilyMember_Qualification').val(data.Qualification);
            $('#EmployeeFamilyMember_Occupation').val(data.Occupation);
            $('#EmployeeFamilyMember_Organization').val(data.Organization);
            $('#EmployeeFamilyMember_Designation').val(data.Designation);
            $('#EmployeeFamilyMember_JobLocation').val(data.JobLocation);
            $('#EmployeeFamilyMember_NationalId').val(data.NationalId);
            $('#EmployeeFamilyMember_Phone').val(data.Phone);
            $('#EmployeeFamilyMember_Mobile').val(data.Mobile);
            $('#EmployeeFamilyMember_Email').val(data.Email);
            $('#EmployeeFamilyMember_Passport').val(data.Passport);
            $('#EmployeeFamilyMember_AddressId').val(data.AddressId);
            $('#EmployeeFamilyMember_IsActive').prop("checked", data.IsActive);
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
                displayEmployeeFamilyMemberList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}




function validateEmployeeFamilyMember() {
    if ($('#EmployeeFamilyMember_Name').val() == "") {
        showMessage('Name is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeFamilyMember_Relation').val() == "") {
        showMessage('Relation is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeFamilyMember_NationalId').val() == "") {
        showMessage('NationalId is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeFamilyMember_Mobile').val() == "") {
        showMessage('Mobile is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {

    $('#EmployeeFamilyMember_Relation').val("");
    $('#EmployeeFamilyMember_Name').val("");
    $('#EmployeeFamilyMember_Qualification').val("");
    $('#EmployeeFamilyMember_Occupation').val("");
    $('#EmployeeFamilyMember_Organization').val("");
    $('#EmployeeFamilyMember_Designation').val("");
    $('#EmployeeFamilyMember_JobLocation').val("");
    $('#EmployeeFamilyMember_NationalId').val("");
    $('#EmployeeFamilyMember_Phone').val("");
    $('#EmployeeFamilyMember_Mobile').val("");
    $('#EmployeeFamilyMember_Email').val("");
    $('#EmployeeFamilyMember_Passportc').val("");
    $('#EmployeeFamilyMember_AddressId').val("");
    $('#EmployeeFamilyMember_IsActive').prop("checked", false);

}

