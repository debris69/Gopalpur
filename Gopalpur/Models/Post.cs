using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

    public class Post
    {
        [Display(Name="Post Title :")]
        public string post_title { get; set; }
        
        [AllowHtml][Display(Name="Post Content:")]
        public string post { get; set; }

        [Display(Name = "Category :")]
        public string catid { get; set; }

        [Display(Name = "Sub-Category Id:")]
        public string subcatid { get; set; }

        [Display(Name = "Active")]
        public choice active { get; set; }

        [Display(Name = "Entry By")]
        public string entryby { get; set; }

        [Display(Name = "Modify By")]
        public string modifyby { get; set; }

        public int flag { get; set; }
    }

    public enum choice{
        Y,
        N,
    }