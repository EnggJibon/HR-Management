var listURL;
var getURL;
var saveURL;
//var deleteURL;
var employeeId;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    //deleteURL = $('#delete-url').data('request-url');

    employeeId = getParameterByName('eid');
    if (employeeId.length > 0 && employeeId > 0) {
        $('#EmployeeHealthInformation_EmployeeId').val(employeeId);
        editItem(employeeId.trim());
    }
    //employeeHealthInformationList(employeeId.trim());
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

//var employeeHealthInformationList = function (employeeId) {
//    var dataSource = new kendo.data.DataSource({
//        type: "aspnetmvc-ajax",
//        transport: {
//            read: {
//                url: listURL + "?employeeId=" + employeeId,
//                type: "POST"
//            }
//        },
//        schema: {
//            data: function (data) {
//                return data.Data;
//            },
//            total: function (data) {
//                return data.Total;
//            },
//            model: {
//                fields: {
//                    Id: { type: "long" },
//                    EmployeeId: { type: "long" },
//                    Age: { type: "string" },
//                    Weight: { type: "string" },
//                    Height: { type: "string" },
//                    Blood_Group: { type: "string" },
//                    Disease: { type: "string" },
//                    Identified_Health_Symbol: { type: "string" },
//                    Disease_Status: { type: "string" },
//                    Disease_Note: { type: "string" },
//                }
//            }
//        },
//        sort: [
//            { field: "Id", dir: "asc" }
//        ],
//        pageSize: 10,
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true
//    });

//    $("#grid").kendoGrid({
//        selectable: "row",
//        dataSource: dataSource,
//        filterable: {
//            extra: true
//        },
//        serverPaging: true,
//        serverFiltering: true,
//        serverSorting: true,
//        height: 200,
//        groupable: false,
//        sortable: true,
//        pageable: {
//            refresh: true,
//            pageSizes: true,
//            buttonCount: 5
//        },
//        columns: [
//        {
//            field: "Id",
//            title: "Id",
//        },


//        {
//            field: "Age",
//            title: "Age",
//            filterable: true,
//        },
//        {
//            field: "Weight",
//            title: "Weight",
//            filterable: false,
//        },
//        {
//            field: "Height",
//            title: "Height",
//            filterable: false,
//        },
//        {
//            field: "Blood_Group",
//            title: "Blood Group",
//            filterable: false,
//        },
//        {
//            field: "Disease",
//            title: "Disease",
//            filterable: false,
//        },
//        {
//            field: "Identified_Health_Symbol",
//            title: "Health Symbol",
//            filterable: false,
//        },

//        {
//            field: "Disease_Status",
//            title: "Disease Status",
//            filterable: false,
//        },

//        {
//            field: "Disease_Note",
//            title: "Disease Note",
//            filterable: false,
//        },
//        {
//            field: "IsActive",
//            title: "Status",
//            filterable: false,
//            template:
//                function (dataItem) {
//                    return dataItem.IsActive ? "Active" : "Inactive";
//                },
//            width: 100
//        },
//        {
//            width: 150,
//            field: "Id",
//            title: "Select",
//            sortable: false,
//            filterable: false,
//            template:
//                function (dataItem) {
//                    if (typeof dataItem.Id != "string") {
//                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Edit</a>";
//                    }
//                }
//        }
//        ]
//    });
//};

$("#btnSave").click(function () {
    if (validateEmployeeHealthInformation()) {
        var jsonObject = {
            "Id": $('#EmployeeHealthInformation_Id').val(),
            "EmployeeId": employeeId,
            "Age": $('#EmployeeHealthInformation_Age').val(),
            "Height": $('#EmployeeHealthInformation_Height').val(),
            "Weight": $('#EmployeeHealthInformation_Weight').val(),
            "BloodGroup": $('#EmployeeHealthInformation_BloodGroup').val(),
            "Disease": $('#EmployeeHealthInformation_Disease').val(),
            "DiseaseNote": $('#EmployeeHealthInformation_DiseaseNote').val(),
            "DiseaseStatus": $('#EmployeeHealthInformation_DiseaseStatus').val(),
            "IdentifiedHealthSymbol": $('#EmployeeHealthInformation_IdentifiedHealthSymbol').val(),
            "IsActive": $('#EmployeeHealthInformation_IsActive').is(":checked")
        };
        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ employeeHealthInformation: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function (data) {
                if (data != null) {
                    //employeeHealthInformationList(data.EmployeeId);
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
            $('#EmployeeHealthInformation_Id').val(data.Id);
            $('#EmployeeHealthInformation_EmployeeId').val(data.EmployeeId);
            $('#EmployeeHealthInformation_Age').val(data.Age);
            $('#EmployeeHealthInformation_Height').val(data.Height);
            $('#EmployeeHealthInformation_Weight').val(data.Weight); 
            $('#EmployeeHealthInformation_BloodGroup').val(data.BloodGroup);
            $('#EmployeeHealthInformation_Disease').val(data.Disease);
            $('#EmployeeHealthInformation_DiseaseNote').val(data.DiseaseNote);
            $('#EmployeeHealthInformation_DiseaseStatus').val(data.DiseaseStatus);
            $('#EmployeeHealthInformation_IdentifiedHealthSymbol').val(data.IdentifiedHealthSymbol);
            $('#EmployeeHealthInformation_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            clearData();

        }
    });
}


function validateEmployeeHealthInformation() {

    if ($('#EmployeeHealthInformation_Age').val() == "") {
        showMessage('Age is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeHealthInformation_Blood_Group').val() == "") {
        showMessage('Blood_Group is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeHealthInformation_Weight').val() == "") {
        showMessage('Weight is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeHealthInformation_Height').val() == "") {
        showMessage('Height is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {

    $('#EmployeeHealthInformation_Age').val("");
    $('#EmployeeHealthInformation_Height').val("");
    $('#EmployeeHealthInformation_Weight').val("");
    $('#EmployeeHealthInformation_BloodGroup').val("");
    $('#EmployeeHealthInformation_Disease').val("");
    $('#EmployeeHealthInformation_IdentifiedHealth_Symbol').val("");
    $('#EmployeeHealthInformation_DiseaseStatus').val("");
    $('#EmployeeHealthInformation_DiseaseNote').val("");
    $('#EmployeeHealthInformation_IsActive').prop("checked", false);

}



