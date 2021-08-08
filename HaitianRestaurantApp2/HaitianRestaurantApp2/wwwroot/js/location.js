$(document).ready(function () {
    $('#menuEmployee').DataTable({
        initComplete: function () {
            this.api().columns([2]).every(function () {
                var column = this;
                var select = $('<select class="class="form-control form-control-sm"><option value="">All Categories</option></select>')
                    .appendTo('#categoryDropdown')
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
});