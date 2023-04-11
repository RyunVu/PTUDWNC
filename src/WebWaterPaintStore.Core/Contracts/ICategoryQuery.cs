namespace WebWaterPaintStore.Core.Contracts
{
    public interface ICategoryQuery
    {
        public string Keyword { get; set; }
        public bool Actived { get; set; }
    }
}
