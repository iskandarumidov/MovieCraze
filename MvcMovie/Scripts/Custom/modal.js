$(document).ready(function () {
    $('#showModalUserBtn').click(function () {
        //$("#myModal").attr("data-url", "/LocalRoles/Create");
        $("#myModal").data("url", "/LocalRoles/Create");
        var url = $('#myModal').data('url');
        
        

        $.get(url, function (data) {
            $('#modalBody').html(data);

            $('#myModal').modal('show');
        });
    });

    $('#showModalRoleBtn').click(function () {
        //$("#myModal").attr("data-url", "/LocalRoles/Create");
        $("#myModal").data("url", "/LocalRoles/CreateRole");
        var url = $('#myModal').data('url');



        $.get(url, function (data) {
            $('#modalBody').html(data);

            $('#myModal').modal('show');
        });
    });
});