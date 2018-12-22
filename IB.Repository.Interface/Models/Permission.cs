using IB.Common.Helpers;

namespace IB.Repository.Interface.Models
{
    public class Permission : Entity<int>
    {
        public const int NameLength = 32;
        public const int DescriptionLength = 128;

        public string Name { get; set; }
        public string Description { get; private set; }

        public Permission SetDescription(string description)
        {
            Description = description.TrimToNull();
            return this;
        }
    }
}
