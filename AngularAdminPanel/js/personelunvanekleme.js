var app = angular.module("sirketAdmin", []);
var controllerName = "personelUnvanEklemeController";
var unvanekleUrl = "/Manager/personelUnvanEkleme";
var unvangetirUrl = "/Manager/unvangetir";
var unvanSilUrl = "/Manager/unvanSil";
var unvanDuzenleUrl = "/Manager/unvanDuzenle";
app.controller(controllerName, function ($scope, $http) {
    $scope.unvanliste = [];
        $scope.personelUnvanEkle = function (unvan_no) {
            var form = $("form.formPost");
            var formData = form.serializeArray();
            var gidenVeri = {};
            $.each(formData, function (i, v) {
                gidenVeri[v.name] = v.value;
            });
            var post = $http({
                method: "POST",
                url: unvanekleUrl,
                data: gidenVeri,
                dataType: 'json',
                headers: { "Content-Type": "application/json" }
            });

            post.success(function (data, status) {
                if (data.IsSucceed == false) {
                    console.log(data.Message);
                }
                else {
                    console.log(data.Message);
                    document.getElementById("form").reset();
                    $scope.unvanlist();
                }
            });
        }


        $scope.unvanlist = function () {
            
            var post = $http({
                method: "POST",
                url: unvangetirUrl,
                data: {},
                dataType: 'json',
                headers: { "Content-Type": "application/json" }
            });
            post.success(function (data,status) {
                if (data.IsSucceed == false) {
                    console.log(data.Message);
                }
                else {
                    console.log(data.Result);
                    $scope.unvanliste = data.Result;
                }

            });


        }

        $scope.unvansil = function (id) {
            $scope.unvan_no = id;

            var post = $http({
                method: "POST",
                url: unvanSilUrl,
                data: { unvan_no: $scope.unvan_no },
                dataType:'json'
            });
                post.success(function (data, status) {
                    if (data.IsSucceed == false) {
                        console.log(data.Message);
                    }
                    else {
                        console.log(data.Message);
                        $scope.unvanlist();
                    }
            });
        }

        $scope.unvanduzenle = function (id) {
            $scope.unvan_no = id;
            var post = $http({
                method: "POST",
                url: unvanDuzenleUrl,
                data: {unvan_no:$scope.unvan_no},
                dataType:'json'
            });

            post.success(function (data, status) {
                if (data.IsSucceed == false) {
                    console.log(data.Message);
                }
                else {
                    console.log(data.Message);
                    $scope.unvan = data.unvan_ad;
                    $scope.no = data.unvan_no;
                }

            });

        }

        
        $scope.unvanlist();


});