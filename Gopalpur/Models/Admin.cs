using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


    public class Admin
    {
        [DisplayName("ID:")]
        public string id { get; set; }

        [DisplayName("Password:")]
        public string pwd { get; set; }
    }

