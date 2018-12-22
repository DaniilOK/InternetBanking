namespace IB.Data.EF
{
    public sealed class UnitOfWorkOptions
    {
        public string ConnectionString { get; set; }
        public int? CommandTimeout { get; set; }
    }
}
