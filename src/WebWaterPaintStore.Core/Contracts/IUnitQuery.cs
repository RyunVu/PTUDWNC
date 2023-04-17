namespace WebWaterPaintStore.Core.Contracts
{
    public interface IUnitQuery
    {
        public int? ProductId { get; set; }
        public string ProductSlug { get; set; }
        public string UnitTag { get; set; }
        public bool Actived { get; set; }
        public bool NotActived { get; set; }
        public string Keyword { get; set; }
    }
}
