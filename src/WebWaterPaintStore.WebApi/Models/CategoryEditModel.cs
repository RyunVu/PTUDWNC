﻿namespace WebWaterPaintStore.WebApi.Models
{
    public class CategoryEditModel
    {
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public bool Actived { get; set; }
    }
}
