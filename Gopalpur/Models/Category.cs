using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class Category
{

    [Display(Name = "Category")]
    public string catname { get; set; }

    [Display(Name = "Controller:")]
    public string controller { get; set; }

    [Display(Name = "Action")]
    public string action { get; set; }

    [Display(Name = "Active")]
    public string active { get; set; }

    [Display(Name = "Entery By")]
    public string entryby { get; set; }

    [Display(Name = "Modify By")]
    public string modifyby { get; set; }

    [Display(Name = "Select the Category for update")]
    public string updatecat { get; set; }

    [Display(Name = "Enter New Category")]
    public string newcat { get; set; }

    [Display(Name = "Active")]
    public string newActive { get; set; }
}