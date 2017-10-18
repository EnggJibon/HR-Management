var listURL;
var getURL;
var saveURL;
var deleteURL;

$(document).ready(function () {
    $("#hdnIsInsert").val(true);

    listURL = $('#list-url').data('request-url');
    getURL = $('#get-url').data('request-url');
    saveURL = $('#save-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');

    displayAddressList();
});

var displayAddressList = function () {
    var dataSource = new kendo.data.DataSource({
        type: "aspnetmvc-ajax",
        transport: {
            read: {
                url: listURL,
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
                    Id: { type: "int" },
                    Address1: { type: "string" },
                    Address2: { type: "string" },
                    CountryId: { type: "int" },
                    City: { type: "string" },
                    State: { type: "string" },
                    PostalCode: { type: "int" },
                    Status: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Address1", dir: "asc" }
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
            field: "Address1",
            title: "Address2",
            filterable: true,
        },
        {
            field: "Address2",
            title: "Address2",
            filterable: true,
        },
        {
            field: "CountryId",
            title: "CountryId",
            filterable: true,
        },
        {
            field: "City",
            title: "City",
            filterable: true,
        },
         {
             field: "State",
             title: "State",
             filterable: true,
         },
         {
             field: "PostalCode",
             title: "PostalCode",
             filterable: true,
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
                function(dataItem) {
                    if (typeof dataItem.Id != "string") {
                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Edit</a> | <a class='btn-link-delete' onclick='deleteItem(" + dataItem.Id + ")'>Delete</a>";
                    }
                }
        }
        ]
    });
};

$("#btnSave").click(function () {
    if (validateAddress()) {
        var jsonObject = {
           "Id": $('#Address_Id').val(),
            "Address1": $('#Address_Address1').val(),
            "Address2": $('#Address_Address2').val(),
            "CountryId": $('#Address_CountryId').val(),
            "City": $('#Address_City').val(),
            "State": $('#Address_State').val(),
            "Postalcode": $('#Address_PostalCode').val(),
            "IsActive": $('#Address_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ address: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayAddressList();
                clearData();
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
            $('#Address_Id').val(data.Id);
            $('#Address_Address1').val(data.Address1);
            $('#Address_Address2').val(data.Address2);
            $('#Address_CountryId').val(data.CountryId);
            $('#Address_City').val(data.City);
            $('#Address_State').val(data.State);
            $('#Address_PostalCode').val(data.PostalCode);
            $('#Address_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this address?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayAddressList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateAddress() {
    if ($('#Address_Address1').val() == "") {
        showMessage('Address1 is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#Address_Address2').val() == "") {
        showMessage('Address2 is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#Address_Id').val("");
    $('#Address_Address1').val("");
    $('#Address_Address2').val("");
    $('#Address_CountryId').val("");
    $('#Address_City').val("");
    $('#Address_State').val("");
    $('#Address_PostalCode').val("");
    $('#Address_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}
