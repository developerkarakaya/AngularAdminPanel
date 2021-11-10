using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    [Serializable]
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public string SonucKodu { get; set; }
        public bool IsSucceed { get; set; }
    }
