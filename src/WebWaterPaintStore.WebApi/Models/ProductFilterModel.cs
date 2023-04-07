﻿namespace WebWaterPaintStore.WebApi.Models
{
    public class ProductFilterModel : PagingModel
    {
        public string? Keyword { get; set; }
        public string? CategorySlug { get; set; }
        public string? ProductSlug { get; set; }
    }
}
