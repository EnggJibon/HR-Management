var listURL;
var getURL;
var saveURL;
var employeeId;
var deleteURL;


$(document).ready(function () {
    $("#hdnIsInsert").val(true);
    listURL = $("#list-url").data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');




    employeeId = getParameterByName('eid');
    if (employeeId.length > 0 && employeeId > 0) {
        $('#EmployeeEducationalQualification_EmployeeId').val(employeeId);
        displayEmployeeEducationalQualificationList(employeeId.trim());
    }
});


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}



var displayEmployeeEducationalQualificationList = function (employeeId) {

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
                    EmployeeId: { type: "long" },
                    Certification: { type: "string" },
                    Subject: { type: "string" },
                    Institute: { type: "string" },
                    Board: { type: "string" },
                    Country: { type: "string" },
                    Duration: { type: "string" },
                    PassingYear: { type: "string" },
                    Division_Class_GPA: { type: "string" },
                    Marks: { type: "string" },
                    OutOf: { type: "string" }

                }
            }
        },

        sort: [
                { field: "Id", dir: "asc" }
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
        },
        {
            field: "Certification",
            title: "Certification",
            filterable: false,
        },
        {
            field: "Subject",
            title: "Subject",
            filterable: false,
        },
        {
            field: "Institute",
            title: "Institute",
            filterable: false,
        },
        {
            field: "Board",
            title: "Board",
            filterable: false,
        },
        //{
        //    field: "Country",
        //    title: "Country",
        //    filterable: false,
        //},

        //{
        //    field: "Duration",
        //    title: "Duration",
        //    filterable: false,
        //},

        {
            field: "PassingYear",
            title: "PassingYear",
            filterable: false,
        },

        {
            field: "Division_Class_GPA",
            title: "CGPA",
            filterable: false,
        },

        //{
        //    field: "Marks",
        //    title: "Marks",
        //    filterable: false,
        //},
        {
            field: "OutOf",
            title: "OutOf",
            filterable: false,
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
    if (validateEmployeeEducationalQualification()) {
        var jsonObject = {
            "Id": $('#EmployeeEducationalQualification_Id').val(),
            "EmployeeId": employeeId,
            "Certification": $('#EmployeeEducationalQualification_Certification').val(),
            "Subject": $('#EmployeeEducationalQualification_Subject').val(),
            "Institute": $('#EmployeeEducationalQualification_Institute').val(),
            "Board": $('#EmployeeEducationalQualification_Board').val(),
            "Country": $('#EmployeeEducationalQualification_Country').val(),
            "Duration": $('#EmployeeEducationalQualification_Duration').val(),
            "PassingYear": $('#EmployeeEducationalQualification_PassingYear').val(),
            "Division_Class_GPA": $('#EmployeeEducationalQualification_Division_Class_GPA').val(),
            "Marks": $('#EmployeeEducationalQualification_Marks').val(),
            "OutOf": $('#EmployeeEducationalQualification_OutOf').val(),
            "IsActive": $('#EmployeeEducationalQualification_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({employeeEducationalQualification: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function (data) {
                if (data != null) {
                    displayEmployeeEducationalQualificationList(data.EmployeeId);
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

            $('#EmployeeEducationalQualification_Id').val(data.Id);
            $('#EmployeeEducationalQualification_EmployeeId').val(data.EmployeeId);
            $('#EmployeeEducationalQualification_Certification').val(data.Certification);
            $('#EmployeeEducationalQualification_Subject').val(data.Subject);
            $('#EmployeeEducationalQualification_Institute').val(data.Institute);
            $('#EmployeeEducationalQualification_Board').val(data.Board);
            $('#EmployeeEducationalQualification_Country').val(data.Country);
            $('#EmployeeEducationalQualification_Duration').val(data.Duration);
            $('#EmployeeEducationalQualification_PassingYear').val(data.PassingYear);
            $('#EmployeeEducationalQualification_Division_Class_GPA').val(data.Division_Class_GPA);
            $('#EmployeeEducationalQualification_Marks').val(data.Position);
            $('#EmployeeEducationalQualification_OutOf').val(data.OutOf);
            $('#EmployeeEducationalQualification_IsActive').prop("checked", data.IsActive);
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
                displayEmployeeEducationalQualificationList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}






function validateEmployeeEducationalQualification() {

     if ($('#EmployeeEducationalQualification_Certification').val() == "") {
        showMessage('Organization is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_Board').val() == "") {
        showMessage('OrganizationType is required.', 'warning', 'Warning!');
        return false;
    }

    else if ($('#EmployeeEducationalQualification_Division_Class_GPA').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_Country').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_PassingYear').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_Subject').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_Institute').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#EmployeeEducationalQualification_OutOf').val() == "") {
        showMessage('Position is required.', 'warning', 'Warning!');
        return false;
    }

    return true;
}


function clearData() {

    $('#EmployeeEducationalQualification_Certification').val("");
    $('#EmployeeEducationalQualification_Subject').val("");
    $('#EmployeeEducationalQualification_Institute').val("");
    $('#EmployeeEducationalQualification_Board').val("");
    $('#EmployeeEducationalQualification_Country').val("");
    $('#EmployeeEducationalQualification_Duration').val("");
    $('#EmployeeEducationalQualification_PassingYear').val("");
    $('#EmployeeEducationalQualification_Division_Class_GPA').val("");
    $('#EmployeeEducationalQualification_Marks').val("");
    $('#EmployeeEducationalQualification_OutOf').val("");
    $('#EmployeeEducationalQualification_IsActive').prop("checked", false);

}


