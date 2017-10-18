var listURL;
var deleteURL;
var detailsInfoURL;
var employeeLeaveInformationURL;
var employeeEmploymentHistoryURL;
var employeeEducationalQualificationURL;
var employeeReferenceInformationURL;
var employeeNomineeInformationURL;
var employeefamilyInformationURL;
var employeeHealthInformationURL;

$(document).ready(function () {
    listURL = $('#list-url').data('request-url');
    deleteURL = $('#delete-url').data('request-url');
    detailsInfoURL = $('#details-info-url').data('request-url');
    employeeLeaveInformationURL = $('#employeeLeaveInformationURL').data('request-url');
    employeeEmploymentHistoryURL = $('#employeeEmploymentHistoryURL').data('request-url');
    employeeEducationalQualificationURL = $('#employeeEducationalQualificationURL').data('request-url');
    employeeReferenceInformationURL = $('#employeeReferenceInformationURL').data('request-url');
    employeeNomineeInformationURL = $('#employeeNomineeInformationURL').data('request-url');
    employeefamilyInformationURL = $('#employeeFamilyInformationURL').data('request-url');
    employeeHealthInformationURL = $('#employeeHealthInformationURL').data('request-url');
    displayEmployeeList();
});

var displayEmployeeList = function () {
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
                    EmployeeCode: { type: "string" },
                    EmployeeName: { type: "string" },
                    CategoryName: { type: "string" },
                    DepartmentName: { type: "string" },
                    DesignationName: { type: "string" },
                    JoiningDate: { type: "date" },
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
            field: "Id",
            title: "Id",
            width: 80
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
            field: "Id",
            title: "Details",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Employee Details\" class=\"column-highlight\" href='" + detailsInfoURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "Id",
            title: "Leave Info",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Leave Information\" class=\"column-highlight\" href='" + employeeLeaveInformationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "Id",
            title: "Experience",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                 function (dataItem) {
                     if (dataItem.Id != null) {
                         return "<span><a title=\"Employment History\" class=\"column-highlight\" href='" + employeeEmploymentHistoryURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                     }
                 }
        },
        {
            field: "Id",
            title: "Qualification",
            width: 100,
            sortable: false,
            filterable: false,
            template:
                  function (dataItem) {
                      if (dataItem.Id != null) {
                          return "<span><a title=\"Educational Qualification\" class=\"column-highlight\" href='" + employeeEducationalQualificationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                      }
                  }
        },
        {
            field: "Id",
            title: "Ref. Info",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Reference Information\" class=\"column-highlight\" href='" + employeeReferenceInformationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "Id",
            title: "Nominee",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Nominee Information\" class=\"column-highlight\" href='" + employeeNomineeInformationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "Id",
            title: "Family",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Family Information\" class=\"column-highlight\" href='" + employeefamilyInformationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "Id",
            title: "Health Info",
            width: 90,
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<span><a title=\"Health Information\" class=\"column-highlight\" href='" + employeeHealthInformationURL + "?eid=" + dataItem.Id + "'><i class='glyphicon glyphicon-edit'></i></a></span>";
                    }
                }
        },
        {
            field: "JoiningDate",
            title: "Joining Date",
            width: 120,
            filterable: false,
            format: "{0:dd-MMM-yyyy}", //"{0:MMM-dd-yyyy hh:mm:ss tt}",
            parseFormats: ["yyyy-MM-dd'T'HH:mm:ss.zz"]
        },
        {
            field: "IsActive",
            title: "Status",
            width: 60,
            filterable: false,
            template:
                function (dataItem) {
                    return dataItem.IsActive ? "Active" : "Inactive";
                }
        },
        {
            width: 150,
            field: "Id",
            title: "Delete",
            sortable: false,
            filterable: false,
            template:
                function (dataItem) {
                    if (dataItem.Id != null) {
                        return "<a class='btn-link-delete' onclick='deleteItem(" + dataItem.Id + ")'>Delete</a>";
                    }
                }
        }
        ]
    });
};

function deleteItem(id) {
    if (confirm("Are you sure you want to delete this Employee Details Information?")) {
        $.ajax({
            type: "POST",
            url: deleteURL,
            data: { id: id },
            cache: false,
            success: function () {
                clearData();
                displayEmployeeList();
                $("#myModal").modal('hide');
                showMessage('Deleted successfully.', 'success', 'Success!');
            },
            error: function () {
                showMessage('Connection error.', 'error', 'Error!');
            }
        });
    }
}