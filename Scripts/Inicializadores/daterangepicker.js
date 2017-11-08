$(document).ready(function () {
    //datepicker
    $('.date-picker').daterangepicker({
        autoUpdateInput: false,
        singleDatePicker: true,
        calender_style: "picker_4",
        timePicker: true,
        timePickerIncrement: 15,
        locale: {
            format: 'DD/MM/YYYY h:mm A',
            applyLabel: "Aplicar",
            cancelLabel: "Cancelar",
            fromLabel: "Desde",
            toLabel: "Hasta",
            customRangeLabel: "Personalizar",
            daysOfWeek: [
                "Do",
                "Lu",
                "Ma",
                "Mi",
                "Ju",
                "Vi",
                "Sa"
            ],
            monthNames: [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            ],
            firstDay: 1
        }
    }, function (chosen_date) {
        $('.date-picker').val(chosen_date.format('DD/MM/YYYY h:mm A'));
    });
});
