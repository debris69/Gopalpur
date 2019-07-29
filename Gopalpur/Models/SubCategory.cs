using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


    public class SubCategory
    {
        [Display(Name = "SubID")]
        public string SubID { get; set; }
        [Display(Name = "")]
        public string cat_id { get; set; }
        [Display(Name = "Category Name")]
        public string cat_name { get; set; }
        [Display(Name = "Sub-Category Name ")]
        public string SubCategoryName { get; set; }
        public bool subNameInUse { get; set; }
        [Display(Name = "Controller")]
        public string controller { get; set; }
        [Display(Name = "Action")]
        public string action { get; set; }
        [Display(Name = "Active")]
        public string active { get; set; }
        [Display(Name = "Entry By")]
        public string entry_by { get; set; }
        [Display(Name = "Entry Date")]
        public string entry_date { get; set; }
        [Display(Name = "Modify By")]
        public string modify_by { get; set; }
        [Display(Name = "Modify Date")]
        public string modify_date { get; set; }

    }

