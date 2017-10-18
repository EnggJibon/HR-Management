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
        $('#EmployeeReferenceInformation_EmployeeId').val(employeeId);
        editItem(employeeId.trim());
        displayEmployeeReferenceInfoList(employeeId.trim());
    }
});



var displayEmployeeReferenceInfoList = function (employeeId) {
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
                    Id: { type: "string" },
                    Occupation: { type: "string" },
                    Designation: { type: "string" },
                    Organization: { type: "string" },
                    Mobile: { type: "string" },
                    Email: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Designation", dir: "asc" }
        ],
        pageSize: 10,
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
        height: 200,
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
         },


        {
            field: "Occupation",
            title: "Occupation",
            filterable: false,
        },
        {
            field: "Designation",
            title: "Designation",
            filterable: false,
        },
        {
            field: "Organization",
            title: "Organization",
            filterable: false,
        },
        {
            field: "Mobile",
            title: "Mobile",
            filterable: false,
        },
        {
            field: "Email",
            title: "Email",
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
    if (validateEmployeeReferenceInformation()) {
        var jsonObject = {
            "Id": $('#EmployeeReferenceInformation_Id').val(),
            "EmployeeId": employeeId,
            "Occupation": $('#EmployeeReferenceInformation_Occupation').val(),
            "Organization": $('#EmployeeReferenceInformation_Organization').val(),
            "Designation": $('#EmployeeReferenceInformation_Designation').val(),
            "Mobile": $('#EmployeeReferenceInformation_Mobile').val(),
            "Email": $('#EmployeeReferenceInformation_Email').val(),
            "IsActive": $('#EmployeeReferenceInformation_IsActive').is(":checked")
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ EmployeeReferenceInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data != null) {
                    displayEmployeeReferenceInfoList(data.EmployeeId);
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
            $('#EmployeeReferenceInformation_Id').val(data.Id);
            $('#EmployeeReferenceInformation_EmployeeId').val(data.EmployeeId);
            $('#EmployeeReferenceInformation_Occupation').val(data.Occupation);
            $('#EmployeeReferenceInformation_Organization').val(data.Organization);
            $('#EmployeeReferenceInformation_Designation').val(data.Designation);
            $('#EmployeeReferenceInformation_Mobile').val(data.Mobile);
            $('#EmployeeReferenceInformation_Email').val(data.Email);
            $('#EmployeeReferenceInformation_IsActive').prop("checked", data.IsActive);
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
                displayEmployeeReferenceInfoList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}



function validateEmployeeReferenceInformation() {
    if (employeeId.length == 0 || employeeId == 0) {
        showMessage('Employee selection is required.', 'warning', 'Warning!');
        return false;
    }
    if ($('#EmployeeReferenceInformation_Occupation').val() == "") {
        showMessage('Age is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeReferenceInformation_Organization').val() == "") {
        showMessage('Blood_Group is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeReferenceInformation_Mobile').val() == "") {
        showMessage('Weight is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#EmployeeReferenceInformation_Occupation').val("");
    $('#EmployeeReferenceInformation_Organization').val("");
    $('#EmployeeReferenceInformation_Designation').val("");
    $('#EmployeeReferenceInformation_Mobile').val("");
    $('#EmployeeReferenceInformation_Email').val("");
    $('#EmployeeReferenceInformation_IsActive').prop("checked", false);

}



