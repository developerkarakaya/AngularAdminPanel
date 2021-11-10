var app = angular.module("sirketAdmin", []);
var controllerName = "loginController";
var registerUrl = "/Manager/register";
var loginUrl = "/Manager/login";
var kullanicigetirUrl = "/Manager/kullanicigetir";
var rolEkleUrl = "/Manager/kullaniciRolEkle";
var rolgetirUrl = "/Manager/rolgetir";
var rolsilUrl = "/Manager/rolsil";
var rolduzenleUrl = "/Manager/rolduzenle";
var kullanicisilUrl = "/Manager/kullaniciSil";
app.controller(controllerName, function ($scope, $http) {
    $scope.IserrorMessage = false;
    $scope.IssuccessMessage = false;
    $scope.register = function () {
        var form = $("form.postForm");
        var formData = form.serializeArray();
        var gidenVeri = {};
        $.each(formData,function (i,v) {
            gidenVeri[v.name] = v.value;
        });
        var post = $http({
            method: "POST",
            url: registerUrl,
            data: gidenVeri,
            dataType: 'json'
        });
        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
                $scope.errorMessage = "Kullanıcı kaydı yapılırken hata oluştu. Lütfen tekrar deneyiniz.";
                $scope.IserrorMessage = true;
            }
            else {
                console.log(data.Message);
                document.getElementById("formRegister").reset();
                $scope.successMessage = "Kullanıcı kaydı başarılı bir şekilde gerçekleşti.";
                $scope.IssuccessMessage = true;
            }
        });
    }
    $scope.login = function () {
        $scope.UserEmail = $("#email").val();
        $scope.UserPassword = $("#password").val();


        var post = $http({
            method: "POST",
            url: loginUrl,
            data: {
                UserEmail: $scope.UserEmail,
                UserPassword:$scope.UserPassword
            },
            dataType:'json'
        });
        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
                $scope.errorMessage = "Kullanıcı girişi yapılırken hata oluştu. Lütfen tekrar deneyiniz.";
                $scope.IserrorMessage = true;
            }
            else {
                console.log(data.Message);
                document.getElementById("formLogin").reset();
                window.location.href = "/Admin/Index";
            }
        });

    }

    $scope.kullaniciRolEkle = function (id) {
        $scope.rollistesi=[];
        var form = $("form.postform");
        var formData = form.serializeArray();
        var gidenVeri = {};

        $.each(formData, function (i, v) {
            gidenVeri[v.name] = v.value;
        });

        var post = $http({
            method: "POST",
            url: rolEkleUrl,
            data: gidenVeri,
            dataType:'json'
        });

        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);

            }
            else {
                console.log(data.Message);
                document.getElementById("form").reset();
                $scope.kullaniciRolGetir();
            }
        });

    }

    $scope.kullaniciRolGetir = function () {
        $scope.rollistesi=[];
        var post = $http({
            method: "POST",
            url: rolgetirUrl,
            data: {},
            dataType:'json'
        });

        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.rollistesi = data.Result;
            }
        });
        post.error(function (data, status) {
            console.log(data);
        });
    }

    $scope.kullanicilistegetir = function () {
        var post = $http({
            method: "POST",
            url: kullanicigetirUrl,
            data: {},
            dataType: 'json'
        });
        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.kullaniciListe = data.Result;
            }
        });
        post.error(function (data, status) {
            console.log(data);
        });
    }
    if ($('#PageName').val()=="Rol") {
        $scope.kullaniciRolGetir();
    }
    else if ($('#PageName').val() == "Kullanici") {
        $scope.kullanicilistegetir();
    }


    $scope.rolSil = function (id) {
        $scope.RolId = id;
        var post = $http({
            method: "POST",
            url: rolsilUrl,
            data: { RolId: $scope.RolId },
            dataType:'json'
        });

        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.kullaniciRolGetir();
            }
        });
    }

    $scope.rolDuzenle = function (id) {
        $scope.RolId = id;

        var post = $http({
            method: "POST",
            url: rolduzenleUrl,
            data: { RolId: $scope.RolId },
            dataType:'json'
        });
        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.RolName = data.Result.RolName;
                $scope.RolId = data.Result.RolId;
            }

        });




    }

    $scope.kullaniciSil = function (id) {
        $scope.UserId = id;
        var post = $http({
            method: "POST",
            url: kullanicisilUrl,
            data: { UserId: $scope.UserId },
            dataType: 'json'
        });

        post.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.kullanicilistegetir();
            }
        });
    }

  


});