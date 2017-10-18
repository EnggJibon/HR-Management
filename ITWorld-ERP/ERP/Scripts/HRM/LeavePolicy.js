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

    $("#LeavePolicy_EffectiveDate").kendoDatePicker({
        format: 'dd-MMM-yyyy',
    });

    displayLeavePolicyList();
});

var displayLeavePolicyList = function () {
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
                    Id: { type: "long", hidden: true },
                    PolicyName: { type: "string" },
                    Description: { type: "string" },
                    EffectiveDate:{type:"date"},
                }
            }
        },
        sort: [
            { field: "PolicyName", dir: "asc" }
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
            hidden: true
        },
        {
            field: "PolicyName",
            title: "PolicyName",
            filterable: true,
        },
        {
            field: "Description",
            title: "Description",
            filterable: false,
        },
        {
            field: "EffectiveDate",
            title: "Effective Date",
            filterable: false,
            //format: "{0:MMM-dd-yyyy hh:mm:ss tt}",
            format: "{0:MMM-dd-yyyy}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
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

$("#btnSave").click(function () {
    if (validateLeavePolicy()) {
        var jsonObject = {
            "Id": $('#LeavePolicy_Id').val(),
            "PolicyName": $('#LeavePolicy_PolicyName').val(),
            "Description": $('#LeavePolicy_Description').val(),
            "EffectiveDate": $('#LeavePolicy_EffectiveDate').val(),
            "IsActive": $('#LeavePolicy_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ leavePolicy: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            headers: {
                'RequestVerificationToken': $('#tokenHeader').val() //'@TokenHeaderValue()'
            },
            success: function () {
                displayLeavePolicyList();
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
            $('#LeavePolicy_Id').val(data.Id);
            $('#LeavePolicy_PolicyName').val(data.PolicyName);
            $('#LeavePolicy_Description').val(data.Description);
            $("#LeavePolicy_EffectiveDate").data("kendoDatePicker").value(data.EffectiveDate);
            $('#LeavePolicy_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this weekend category?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayLeavePolicyList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}

function validateLeavePolicy() {
    if ($('#LeavePolicy_PolicyName').val() == "") {
        showMessage('PolicyName is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#LeavePolicy_Description').val() == "") {
        showMessage('Description is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#LeavePolicy_Id').val("");
    $('#LeavePolicy_PolicyName').val("");
    $('#LeavePolicy_Description').val("");
    $('#LeavePolicy_EffectiveDate').val("");
    $('#LeavePolicy_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}