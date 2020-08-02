using SHARED;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace WEB.Models
{
    public class CategoryModel: BaseModel
    {
        public int Catmid { get; set; }
        [Required(ErrorMessage = "Please enter Category name")]
        public string CatName { get; set; }
        public string ActionName { get; set; }
        public List<CategoryMaster> CategoryList { get; set; }
    }
}