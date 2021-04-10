var Users = function () {
    var handlebindUsers = function () {
        var api_url = 'https://localhost:44320/api/v1/Users/Get';
        $.ajax({
            url: api_url,
            type: 'GET',
            data: {},
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var str = "";
                var i = 0;
                $.each(data, function (key, value) {
                    str = str + "<tr><td>" + (i = parseInt(i) + 1) + "</td><td>" + value.firstName + "</td><td>" + value.lastName + "</td><td>" + value.address + '</td><td>' + value.age + '</td ><td>' + value.interests + '</td ></tr>';
                });
                $('#tblUsers').find("tbody").html(str);
                $('#tblUsers').DataTable({
                    "autoWidth": false,
                    "order": [[0, "asc"]]
                });
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    };
    var handleAddUser = function () {
        $("#form_users").validate();
        if ($("#form_users").valid()) {
            
            var _object = {
                "Id": 0,
                "FirstName": $("#FirstName").val(),
                "LastName": $("#LastName").val(),
                "Address": $("#Address").val(),
                "Age": parseInt($("#Age").val()),
                "Interests": $("#Interests").val(),
            }
            $.ajax({
                url: 'https://localhost:44320/api/v1/Users/Post',
                data: JSON.stringify(_object),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert(data.msg);
                    $("#form_users").trigger("reset");;
                }
            });
        }
    };
    return {
        bindUsers: function () {
            handlebindUsers();
        },
        addUser: function () {
            handleAddUser();
        },
    };
}();