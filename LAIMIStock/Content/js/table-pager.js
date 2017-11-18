var page;
function pager(isLoading) {
    if (isLoading === "Starting") {
        page = 0;
    }

    $('#page span').text(page + 1);
    $('tr').hide();
    $('tr:first,tr:gt(' + page * 20 + '):lt(20)').show();
}
function prevPage() {
    if (page > 0) {
        page--;
        pager("Drawing");
    }
};

function nextPage() {
    var table = document.getElementById("myTable");
    var rowCount = table.rows.length;
    if (page < rowCount / 20 - 1) {
        page++;
        pager("Drawing");
    }
};