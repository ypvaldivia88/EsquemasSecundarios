$('#dtexport').DataTable({
    dom: 'Bfrtip',
    buttons: [
        'copy',
        'excel',
        'pdf',
        'print'
    ],
    language: {
        buttons: {
            copy: 'Copiar',
            copyTitle: 'Copiado al portapapeles',
            copySuccess: {
                _: '%d líneas copiadas',
                1: '1 ligne copiée'
            },
            print: 'Imprimir'
        }
    }
});