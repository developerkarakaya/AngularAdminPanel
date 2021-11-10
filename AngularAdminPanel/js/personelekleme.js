var app = angular.module("sirketAdmin", []);
var controllerName = "personelEklemeController";
var postUrl = "/Manager/personelEkleme";
var personellistUrl = "/Manager/personelListesi";
var personelSilUrl = "/Manager/personelSil";
var personelduzenleUrl = "/Manager/personelDuzenle";
var unvanUrl = "/Manager/personelUnvanGetir";
var personelBirimUrl = "/Manager/personelbirimgetir";
app.controller(controllerName, function ($scope, $http) {
    $scope.personelListe = [];
    $scope.unvanListe = [];
    $scope.personelEkle = function (id) {
        var form = $("form.formPost");
        var formData = form.serializeArray();//postform classlı formu serialize eder
        var gidenVeri = {};
        $.each(formData,
            function (i, v) {
                gidenVeri[v.name] = v.value;
            });
        var post = $http({
            method: "POST",
            url: postUrl,
            dataType: 'json',
            data: gidenVeri,
            headers: { "Content-Type": "application/json" }
        });
        post.success(function (data, status) {
            document.getElementById("form").reset();
            $scope.personellist();
        });
        post.error(function (data, status) {
            console.log("işlem başarısız");
        });
    }

    $scope.personellist = function () {
        var get = $http({
            method: "GET",
            url: personellistUrl,
            dataType: 'json',
            data: {},
            headers: { "Content-Type": "application/json" }
        });

        get.success(function (data, status) {
            $scope.personelListe = data.Result;
        });

        get.error(function (data, status) {
            console.log("Personel Listesi Çekme İşlemi Başarısız.");
        });
    }

    $scope.personelSil = function (personel_no) {

        $scope.gelenId = personel_no;
        var post = $http({
            method: "POST",
            url: personelSilUrl,
            dataType: 'json',
            data: {
                personel_no: $scope.gelenId
            },
            headers: { "Content-Type": "application/json" }

        });

        post.success(function (data, status) {
            console.log("Personel Silme İşlemi Başarılı.");
            $scope.personellist();
        });
        post.error(function (data, status) {
            console.log("Personel Silme İşlemi Başarısız.");
        });
    }


    $scope.personelDuzenle = function (personel_no) {
        $scope.personelId = personel_no;
        var post = $http({
            method: "POST",
            url: personelduzenleUrl,
            dataType: 'json',
            data: { personel_no: $scope.personelId },
            headers: { "Content-Type": "application/json" }
        });

        post.success(function (data, status) {
            $scope.id = data.personel_no;
            $scope.unvan = data.unvan;
            $scope.birim = data.birim;
            $scope.ad = data.ad;
            $scope.soyad= data.soyad;
            $scope.cinsiyet= data.cinsiyet;
            $scope.dogum_tarihi = data.dogum_tarihi;
            $scope.calisma_saati= data.calisma_saati;
            $scope.maas= data.maas;
            $scope.prim = data.prim;
            $scope.baslama_tarihi = data.baslama_tarihi;
        });
        post.error(function (data, status) {
            console.log("başarısız");
        });
    }


    $scope.personelUnvan = function () {
        var get = $http({
            method: "GET",
            url: unvanUrl,
            data: {},
            dataType: 'json',
        });

        get.success(function (data, status) {
            if (data.IsSucceed == false) {
                console.log(data.Message);
            }
            else {
                console.log(data.Result);
                $scope.unvanListe = data.Result;
            }
        });
    }

    $scope.personelBirim = function () {
        var post = $http({
            method: "POST",
            url: personelBirimUrl,
            data: {},
            dataType: 'json',

        });
        post.success(function (data, status) {
            if(data.IsSucceed==false) 
            {
                console.log(data.Message);
            }
            else {
                console.log(data.Message);
                $scope.birimliste = data.Result;
            }
            });
    }


    $scope.illergetir = function () {
        var get = $http({
            method: "GET",
            url: "https://raw.githubusercontent.com/volkansenturk/turkiye-iller-ilceler/master/il.json",
            data: {},
            dataType: 'json'
        }).then(function (response) {
            $scope.illerliste = response.data[2].data;
        });
    }

    $scope.personelBirim();
    $scope.personellist();
    $scope.personelUnvan();

    $scope.illergetir();
});