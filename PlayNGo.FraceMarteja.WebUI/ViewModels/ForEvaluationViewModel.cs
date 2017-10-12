using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayNGo.FraceMarteja.WebUI.ViewModels
{
    public class ForEvaluationViewModel
    {
        [Required]
        public int Hand_Id { get; set; }
        [Required]
        public List<int> Player_Ids { get; set; }
    }
}