﻿namespace IMS.CoderePlaytech.CajaCodereWebApi.Models
{
    #region Using

    using IMS.CoderePlaytech.Domain.Models;

    #endregion

    public class Session
    {
        public UserViewModel user { get; set; }
        public string token { get; set; }
    }
}
