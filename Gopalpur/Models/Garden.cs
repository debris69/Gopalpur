using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;





    public class Garden

    {
        
       
        [DisplayName("ImageID")]
        public int ImageID { get; set; }

        [DisplayName("Title")]
        public String Title { get; set; }

        [DisplayName("ImageText")]
        public String ImageText { get; set; }

        [DisplayName("TextHeading")]
        public String TextHeading { get; set; }

        //[DisplayName("Upload File")]

        
        

        public String ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }



         
    }
