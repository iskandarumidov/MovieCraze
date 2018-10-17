$(document).ready(function () {
    $('.roleCheckBox:checkbox').change(function () {
        var userName = $(this).attr("name");
        var role = $(this).attr("value");
        var isChecked = $(this).is(":checked");
        console.log("Change: " + userName + "//" + role + " to " + isChecked);
        sendAjax(userName, role, isChecked);
    });
});

function sendAjax(userName, role, isChecked) {
    $.post("/LocalRoles/UpdateUserRole",
        {
            UserName: userName,
            RoleName: role,
            IsChecked: isChecked
        },
        function (data, status) {
            alert("Data: " + data + "\nStatus: " + status);
        });
}