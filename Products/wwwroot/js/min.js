var gid;

function update1(id) {

    $.ajax({
        url: "/Home/GetData",
        type: 'POST',
        data: { id: id },
        success: function (result) {
            console.table(result)
        }
    });
    $('#update').modal('show');
}
function Test() {
    console.log("w");
}
function delMassage(id) {
    $('#conform').modal('show');
    gid = id;
}
function confirm() {
    window.location.href = "Home/Delete?product=" + gid;
}



