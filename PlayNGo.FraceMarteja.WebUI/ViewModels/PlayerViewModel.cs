using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlayNGo.FraceMarteja.WebUI.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        public bool IsEditing { get; set; } = false;
        public bool IsSelected { get; set; } = false;
    }
}