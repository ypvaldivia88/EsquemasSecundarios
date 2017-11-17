InitDataTables();

function InitDataTables() {
    var handleDataTableButtons = function () {

        if ($("#datatable-buttons").length) {
            $("#datatable-buttons").DataTable({
                dom: "Bfrtip",
                buttons: [
                    {
                        extend: "copy", className: "btn-sm", label: "Copiar"
                    },
                    {
                        extend: "csv", className: "btn-sm", label: "CSV"
                    },
                    {
                        extend: "excel", className: "btn-sm", label: "Excel"
                    },
                    {
                        extend: "pdfHtml5", className: "btn-sm", label: "PDF"
                    },
                    {
                        extend: "print", className: "btn-sm", label: "Imprimir"
                    },
                ],
                responsive: true
            });
        }
    };

    TableManageButtons = function () {
        "use strict";
        return {
            init: function () {
                handleDataTableButtons();
            }
        };
    }();

    $('#datatable').dataTable();

    $('#datatable-keytable').DataTable({
        keys: true
    });

    $('#datatable-responsive').DataTable();

    $('#datatable-scroller').DataTable({
        ajax: "js/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });

    var table = $('#datatable-fixed-header').DataTable({
        fixedHeader: true
    });

    TableManageButtons.init();
}