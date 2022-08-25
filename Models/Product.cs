﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopThoiTrang.Models
{
    [Table("Products")]
    public class Product
    {

        [Key]
        public int Id { get; set; }

        public int CatId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Slug { get; set; }

        public string Detail { get; set; }

        [Required]
        public string MetaKey { get; set; }

        [Required]
        public string Metadesc { get; set; }

        public string Img { get; set; }

        public int Number { get; set; }

        public double Price { get; set; }

        public double Pricesale { get; set; }

        public int? Created_By { get; set; }

        public DateTime? Created_At { get; set; }

        public int? Updated_By { get; set; }

        public DateTime? Updated_At { get; set; }

        public int Status { get; set; }
    }
}