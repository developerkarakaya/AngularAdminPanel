using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularAdminPanel.Models;
using System.Security.Cryptography;
using System.Text;

namespace AngularAdminPanel.Controllers
{
    public class ManagerController : Controller
    {
        public static string hash = "samet";

        // GET: Manager


        #region personel ekleme silme guncelleme işlemleri
        [HttpPost]
        public JsonResult personelEkleme(personel personel)
        {
            ApiResponse<personel> response = new ApiResponse<personel>();
            response.IsSucceed = true;
            using (var db = new AngularAdminPanelEntities())
            {
                try
                {

                    if (personel.personel_no > 0)
                    {
                        var updatePerson = db.personel.FirstOrDefault(ss => ss.personel_no == personel.personel_no);
                        updatePerson.ad = personel.ad;
                        updatePerson.il = personel.il;
                        updatePerson.birim = personel.birim;
                        updatePerson.unvan = personel.unvan;
                        updatePerson.soyad = personel.soyad;
                        updatePerson.baslama_tarihi = personel.baslama_tarihi;
                        updatePerson.calisma_saati = personel.calisma_saati;
                        updatePerson.cinsiyet = personel.cinsiyet;
                        updatePerson.dogum_tarihi = personel.dogum_tarihi;
                        updatePerson.maas = personel.maas;
                        updatePerson.prim = personel.prim;
                        updatePerson.baslama_tarihi = personel.baslama_tarihi;
                        response.Result = updatePerson;
                        db.SaveChanges();

                    }
                    else
                    {
                        personel.baslama_tarihi = DateTime.Now.ToString();
                        db.personel.Add(personel);
                        db.SaveChanges();
                        response.Result = personel;
                    }
                    response.Message = Constant.SuccesMessage;

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    response.IsSucceed = false;
                    response.Message = Constant.ErrorMessage;// raise.ToString(); //"";
                }
            }
            return Json(response.Result);
        }


        [HttpGet]
        public JsonResult personelListesi()
        {
            ApiResponse<List<personel>> response = new ApiResponse<List<personel>>();
            using (var db = new AngularAdminPanelEntities())
            {
                var model = db.personel.OrderByDescending(ss => ss.personel_no).ToList();
                response.IsSucceed = true;
                response.Result = model;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult personelSil(int personel_no)
        {
            using (var db = new AngularAdminPanelEntities())
            {
                var deletePerson = db.personel.FirstOrDefault(ss => ss.personel_no == personel_no);
                db.personel.Remove(deletePerson);
                db.SaveChanges();
                return Json(personel_no);
            }
        }


        [HttpPost]
        public JsonResult personelDuzenle(int personel_no)
        {
            using (var db = new AngularAdminPanelEntities())
            {
                var updatePerson = db.personel.FirstOrDefault(ss => ss.personel_no == personel_no);
                return Json(updatePerson);
            }
        }


        [HttpGet]
        public JsonResult personelUnvanGetir()
        {
            ApiResponse<List<unvan>> response = new ApiResponse<List<unvan>>();
            using (var db = new AngularAdminPanelEntities())
            {
                var list = db.unvan.OrderByDescending(ss => ss.unvan_no).ToList();
                response.IsSucceed = true;
                response.Message = Constant.SuccesMessage;
                response.Result = list;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region personel ünvan ekleme silme guncelleme işlemleri


        [HttpPost]
        public JsonResult personelUnvanEkleme(unvan unvan)
        {
            ApiResponse<List<unvan>> response = new ApiResponse<List<unvan>>();
            try
            {

                using (var db = new AngularAdminPanelEntities())
                {


                    if (unvan.unvan_no > 0)
                    {
                        var updateUnvan = db.unvan.FirstOrDefault(ss => ss.unvan_no == unvan.unvan_no);
                        updateUnvan.unvan_ad = unvan.unvan_ad;
                        db.SaveChanges();
                        response.Message = Constant.SuccesMessage;
                        response.IsSucceed = true;
                        return Json(response);
                    }
                    else
                    {
                        db.unvan.Add(unvan);
                        db.SaveChanges();
                        response.IsSucceed = true;
                        response.Message = Constant.SuccesMessage;
                        return Json(response);
                    }
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                return Json(response);
            }
        }




        [HttpPost]
        public JsonResult unvangetir()
        {
            ApiResponse<List<unvan>> response = new ApiResponse<List<unvan>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var unvanliste = db.unvan.OrderByDescending(ss => ss.unvan_no).ToList();
                    response.Result = unvanliste;
                    response.IsSucceed = true;
                    response.Message = Constant.SuccesMessage;
                    return Json(response);
                }
            }
            catch (Exception)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }



        [HttpPost]

        public JsonResult unvanSil(int unvan_no)
        {
            ApiResponse<List<unvan>> response = new ApiResponse<List<unvan>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var deleteUnvan = db.unvan.FirstOrDefault(ss => ss.unvan_no == unvan_no);
                    db.unvan.Remove(deleteUnvan);
                    db.SaveChanges();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccesMessage;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                return Json(response);
            }
        }


        [HttpPost]

        public JsonResult unvanDuzenle(int unvan_no)
        {
            ApiResponse<unvan> response = new ApiResponse<unvan>();

            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var updateUnvan = db.unvan.FirstOrDefault(ss => ss.unvan_no == unvan_no);
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(updateUnvan);
                }
            }
            catch (Exception e)
            {
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(null);
            }
        }
        #endregion


        #region personel birim ekleme silme guncelleme işlemleri

        [HttpPost]
        public JsonResult personelbirimEkleme(birim birim)
        {
            ApiResponse<List<birim>> response = new ApiResponse<List<birim>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    if (birim.birim_no > 0)
                    {
                        var updateBirim = db.birim.FirstOrDefault(ss => ss.birim_no == birim.birim_no);
                        updateBirim.birim_ad = birim.birim_ad;
                        db.SaveChanges();
                        response.Message = Constant.SuccesMessage;
                        response.IsSucceed = true;
                        return Json(updateBirim);
                    }
                    else
                    {
                        db.birim.Add(birim);
                        db.SaveChanges();
                        response.IsSucceed = true;
                        response.Message = Constant.SuccesMessage;
                        return Json(response);
                    }
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
            }
            return Json(response);
        }


        [HttpGet]
        public JsonResult birimgetir()
        {
            ApiResponse<List<birim>> response = new ApiResponse<List<birim>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var list = db.birim.OrderByDescending(ss => ss.birim_no).ToList();
                    response.Result = list;
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]

        public JsonResult birimSil(int birim_no)
        {
            ApiResponse<List<birim>> response = new ApiResponse<List<birim>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var deleteBirim = db.birim.FirstOrDefault(ss => ss.birim_no == birim_no);
                    db.birim.Remove(deleteBirim);
                    db.SaveChanges();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccesMessage;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                return Json(response);
            }
        }


        [HttpPost]

        public JsonResult birimDuzenle(int birim_no)
        {
            ApiResponse<List<birim>> response = new ApiResponse<List<birim>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var updatebirim = db.birim.FirstOrDefault(ss => ss.birim_no == birim_no);
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(updatebirim);
                }

            }
            catch (Exception e)
            {
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                return Json(null);
            }
        }

        [HttpPost]

        public JsonResult personelbirimgetir()
        {
            ApiResponse<List<birim>> response = new ApiResponse<List<birim>>();

            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var list = db.birim.OrderByDescending(ss => ss.birim_no).ToList();
                    response.Message = Constant.SuccesMessage;
                    response.Result = list;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }
        #endregion



        #region personel kullanıcı kayıt ve  kullanıcı giriş işlemleri



        public JsonResult register(User user)
        {
            ApiResponse<List<User>> response = new ApiResponse<List<User>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    user.UserPasswordText = user.UserPassword;
                    user.UserPassword = MD5Olustur(user.UserPassword);
                    db.User.Add(user);
                    db.SaveChanges();
                    response.IsSucceed = true;
                    response.Message = Constant.SuccesMessage;
                    response.Result = null;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;
                return Json(response);
            }
        }

        public JsonResult login(string UserEmail, string UserPassword)
        {
            ApiResponse<List<User>> response = new ApiResponse<List<User>>();

            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var user = db.User.Where(ss => ss.UserEmail == UserEmail).FirstOrDefault();
                    if (user != null)
                    {

                        var UserPasswordCozulu = MD5Olustur(UserPassword);
                        if (user.UserEmail == UserEmail && user.UserPassword == UserPasswordCozulu)
                        {

                            Session["userName"] = user.UserName;
                            response.Message = Constant.SuccesMessage;
                            response.IsSucceed = true;
                            response.Result = null;
                            return Json(response);
                        }
                    }
                    else
                    {
                        response.Message = Constant.ErrorMessage;
                        response.IsSucceed = false;
                        response.Result = null;
                        return Json(response);
                    }

                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                        validationErrors.Entry.Entity.ToString(),
                        validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                response.IsSucceed = false;
                response.Message = Constant.ErrorMessage;// raise.ToString(); //"";
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult kullanicigetir()
        {
            ApiResponse<List<User>> response = new ApiResponse<List<User>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var userliste = db.User.OrderByDescending(ss => ss.UserId).ToList();
                    response.Result = userliste;
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }


        [HttpPost]
        public JsonResult kullaniciRolEkle(Role rol)
        {
            ApiResponse<List<Role>> response = new ApiResponse<List<Role>>();
            try
            {

                using (var db = new AngularAdminPanelEntities())
                {
                    if (rol.RolId > 0)
                    {
                        var updateRole = db.Role.FirstOrDefault(ss => ss.RolId == rol.RolId);
                        updateRole.RolName = rol.RolName;
                        db.SaveChanges();
                        response.Result = null;
                        response.Message = Constant.SuccesMessage;
                        response.IsSucceed = true;
                        return Json(response);
                    }
                    else
                    {
                        db.Role.Add(rol);
                        db.SaveChanges();
                        response.Result = null;
                        response.Message = Constant.SuccesMessage;
                        response.IsSucceed = true;
                        return Json(response);


                    }
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }


        [HttpPost]
        public JsonResult rolgetir()
        {
            ApiResponse<List<Role>> response = new ApiResponse<List<Role>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var rollist = db.Role.OrderByDescending(ss => ss.RolId).ToList();
                    response.Result = rollist;
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }



        [HttpPost]

        public JsonResult rolsil(int RolId)
        {

            ApiResponse<List<Role>> response = new ApiResponse<List<Role>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var deleteRole = db.Role.FirstOrDefault(ss => ss.RolId == RolId);
                    db.Role.Remove(deleteRole);
                    db.SaveChanges();
                    response.Message = Constant.SuccesMessage;
                    response.Result = null;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                response.IsSucceed = false;
                return Json(response);
            }
        }


        [HttpPost]
        public JsonResult rolduzenle(int RolId)
        {
            ApiResponse<Role> response = new ApiResponse<Role>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var updatedRole = db.Role.FirstOrDefault(ss => ss.RolId == RolId);
                    response.Result = updatedRole;
                    response.Message = Constant.SuccesMessage;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Result = null;
                response.Message = Constant.ErrorMessage;
                response.IsSucceed = false;
                return Json(response);
            }
        }


        [HttpPost]


        public JsonResult kullaniciSil(int UserId)
        {

            ApiResponse<List<Role>> response = new ApiResponse<List<Role>>();
            try
            {
                using (var db = new AngularAdminPanelEntities())
                {
                    var User = db.User.FirstOrDefault(ss => ss.UserId == UserId);
                    db.User.Remove(User);
                    db.SaveChanges();
                    response.Message = Constant.SuccesMessage;
                    response.Result = null;
                    response.IsSucceed = true;
                    return Json(response);
                }
            }
            catch (Exception e)
            {
                response.Message = Constant.ErrorMessage;
                response.Result = null;
                response.IsSucceed = false;
                return Json(response);
            }
        }
    
        public static string MD5SifreCoz(string UserPassword)
        {

            byte[] data = Convert.FromBase64String(UserPassword);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }

        public static string MD5Olustur(string text)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }
        #endregion
    }
}