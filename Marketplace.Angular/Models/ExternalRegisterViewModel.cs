using System;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.Angular.Models
{
    public class ExternalRegisterViewModel
    {
        public string UserName { get; set; }
        public string ReturnUrl { get; set; }
    }
}
