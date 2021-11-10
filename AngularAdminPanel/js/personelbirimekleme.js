var app = angular.module("sirketAdmin", []);
var controllerName = "personelbirimEklemeController";
var birimekleUrl = "/Manager/personelbirimEkleme";
var birimgetirUrl = "/Manager/birimgetir";
var birimSilUrl = "/Manager/birimSil";
var birimDuzenleUrl = "/Manager/birimDuzenle";
app.controller(controllerName, function ($scope, $http) {
    $scope.birimliste = [];
    $scope.personelBirimEkle = function (id) {
        var form = $("form.formPost");
        var formData = form.serializeArray();
        var gidenVeri = {};
        $.each(formData,
            function (i, v) {
                gidenVeri[v.name] = v.value;
            });
        var post = $http({
            method: "POST",
            url: birimekleUrl,
            data: gidenVeri,
            dataType:'json'
        });
        post.success(function (data, status) {
            if (data.Issucced == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                document.getElementById("form").reset();
                $scope.birimGetir();
            }
        });
    }
    $scope.birimGetir = function () {
        var get = $http({
            method: "GET",
            url: birimgetirUrl,
            data: {},
            dataType:'json'
        });
        get.success(function (data, status) {
            if (data.Issucced == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.birimliste = data.Result;
            }
        });
    }
    
    $scope.birimsil = function (id) {
        $scope.birim_no = id;

        var post = $http({
            method: "POST",
            url: birimSilUrl,
            data: { birim_no: $scope.birim_no },
            dataType:'json'
        });

        post.success(function (data, status) {
            if (data.Issucced == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.birimGetir();
            }
        });
    }

    $scope.birimduzenle = function (id) {
        $scope.birim_no = id;
        var post = $http({
            method: "POST",
            url: birimDuzenleUrl,
            data: { birim_no: $scope.birim_no },
            dataType:'json'
        });

        post.success(function (data, status) {
            if (data.Issucced == false) {
                console.log(data.Message);
            }
            else {
                $scope.no = data.birim_no;
                $scope.birim = data.birim_ad;
            }

        });
    }
    $scope.birimGetir();
});