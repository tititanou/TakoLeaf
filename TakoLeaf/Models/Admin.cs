﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakoLeaf.Models
{
    public class Admin
    {
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(40)")]
        public string Pwd { get; set; }
    }
}
