﻿using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models

{
    public class ProductDto
    {
        [Key]
        public int ProductId { get; set; }

		public ProductDto()
		{
			Count = 1;
		}

		[Required]
        public string Name { get; set; }

        [Range(1, 1000)]
        public double Price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
		[Range(1, 100)]
		public int Count { get; set; }
	}
}
