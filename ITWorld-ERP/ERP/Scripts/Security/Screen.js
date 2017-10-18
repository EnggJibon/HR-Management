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

    displayScreenList();
});

var displayScreenList = function () {
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
                    Id: { type: "long" },
                    ScreenCode: { type: "string" },
                    ModuleId: { type: "long" },
                    Title: { type: "string" },
                }
            }
        },
        sort: [
            { field: "ScreenCode", dir: "asc" }
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
            hidden: true
        },
        {
            width: 140,
            field: "ScreenCode",
            title: "Screen Code",
            filterable: true,
        },
        {
            field: "ModuleName",
            title: "Module Name",
            width: 150,
            filterable: false,
        },
        {
            field: "ScreenTitle",
            title: "Title",
            width: 250,
            filterable: true,
        },
        {
            field: "Type",
            title: "Type",
            width: 80,
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.Type == 'S' ? "Screen" : "Report";
                }
        },
        {
            field: "URL",
            title: "URL",
            width: 250,
            filterable: false,
        },
        {
            field: "IsActive",
            title: "Status",
            width: 100,
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                }
        },
        {
            width: 100,
            field: "Id",
            title: "Select",
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (typeof dataItem.Id != "string") {
                        return "<a class='btn-link' onclick='editItem(" + dataItem.Id + ")'>Edit</a>";
                        //| <a class='btn-link-delete' onclick='deleteItem(" + dataItem.Id + ")'>Delete</a>
                    }
                }
        }
        ]
    });
};

$("#btnSave").click(function () {
    if (validateScreen()) {
        var jsonObject = {
            "Id": $('#Screen_Id').val(),
            "ScreenCode": $('#Screen_ScreenCode').val(),
            "ModuleId": $('#ModuleList').val(),
            "Title": $('#Screen_Title').val(),
            "Type": $('#ScreenTypeList').val(),
            "URL": $('#Screen_URL').val(),
            "IsActive": $('#Screen_IsActive').is(":checked")
        };

        $.ajax({
            type: "POST",
            url: saveURL,
            data: JSON.stringify({ screen: jsonObject, isInsert: $("#hdnIsInsert").val() }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            cache: false,
            success: function () {
                displayScreenList();
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
            $('#Screen_Id').val(data.Id);
            $('#Screen_ScreenCode').val(data.ScreenCode);
            $('#ModuleList').val(data.ModuleId);
            $('#Screen_Title').val(data.Title);
            $('#ScreenTypeList').val(data.Type);
            $('#Screen_URL').val(data.URL);
            $('#Screen_IsActive').prop("checked", data.IsActive);
            $("#hdnIsInsert").val(false);
        },
        error: function () {
            showMessage('Connection error.', 'error', 'Error!');
        }
    });
}

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this role?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayScreenList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}


function validateScreen() {
    if ($('#Screen_ScreenCode').val() == "") {
        showMessage('Screen Code is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#Screen_ScreenCode').val().length > 5) {
        showMessage('Screen Code will be maxuimun 5 letter/digit.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#ModuleList').val() == "") {
        showMessage('Module is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#Screen_Title').val() == "") {
        showMessage('Title is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#ScreenTypeList').val() == "") {
        showMessage('Type is required.', 'warning', 'Warning!');
        return false;
    }
    else if ($('#Screen_URL').val() == "") {
        showMessage('URL is required.', 'warning', 'Warning!');
        return false;
    }
    return true;
}

function clearData() {
    $('#Screen_Id').val("");
    $('#Screen_ScreenCode').val("");
    $('#ModuleList').val("");
    $('#Screen_Title').val("");
    $('#Screen_Type').val("");
    $('#Screen_URL').val("");
    $('#Screen_IsActive').prop("checked", false);
    $("#hdnIsInsert").val(true);
}

