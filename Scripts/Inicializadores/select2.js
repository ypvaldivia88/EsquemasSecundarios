InitSelect2();


function InitSelect2() {
    //<!-- Select2 -->
    $(".select2_single").select2({
        placeholder: "Seleccione un elemento de la lista",
        allowClear: true
    });
    $(".select2_group").select2({});
    $(".select2_multiple").select2({
        placeholder: "Seleccione uno o varios elementos de la lista",
        allowClear: true
    });
}