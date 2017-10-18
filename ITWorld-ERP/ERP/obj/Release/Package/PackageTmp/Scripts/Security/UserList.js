var listURL;
var detailsInfoURL;

$(document).ready(function () {
    listURL = $('#list-url').data('request-url');
    detailsInfoURL = $('#details-info-url').data('request-url');

    displayUserInformationList();
});

var displayUserInformationList = function () {
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
                    Username: { type: "string" },
                    EmployeeCode: { type: "string" },
                    EmployeeName: { type: "string" },
                    CategoryName: { type: "string" },
                    DepartmentName: { type: "string" },
                    DesignationName: { type: "string" },
                }
            }
        },
        sort: [
            { field: "Username", dir: "asc" }
        ],
        pageSize: 10,
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false
    });

    $("#grid").kendoGrid({
        selectable: "row",
        dataSource: dataSource,
        filterable: {
            extra: false
        },
        serverPaging: false,
        serverFiltering: false,
        serverSorting: false,
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
            width: 80,
            hidden: true
        },
        {
            field: "Username",
            title: "Username",
            width: 120
        },
        {
            field: "EmployeeCode",
            title: "Code",
            width: 80
        },
        {
            field: "EmployeeName",
            title: "Name",
            width: 180
        },
        {
            field: "CategoryName",
            title: "Category",
            width: 120
        },
        {
            field: "DepartmentName",
            title: "Department",
            width: 120
        },
        {
            field: "DesignationName",
            title: "Designation",
            width: 120
        },
        {
            field: "IsActive",
            title: "Status",
            width: 90,
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                }
        },
        {
            field: "Id",
            title: "Details",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"User Details\" class=\"column-highlight\" href='" + detailsInfoURL + "?uid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        }
        ]
    });
};