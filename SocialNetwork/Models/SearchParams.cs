using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class SearchParams
    {
        [StringLength(50,ErrorMessage = "Too large first name")]
        public string FirstName { get; set; } = "";
        [StringLength(50, ErrorMessage = "Too large last name")]
        public string LastName { get; set; } = "";


    }
}