using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebRailwayApp.Models
{
    public partial class Role
    {
        public Role()
        {
        }

        [Key]
        public int ID_Role { get; set; }
        public string NameOfRole { get; set; }

    }
}
