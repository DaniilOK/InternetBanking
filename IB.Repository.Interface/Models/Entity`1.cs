using System;

namespace IB.Repository.Interface.Models
{
    public class Entity<TKey> : Entity, IEquatable<Entity<TKey>> where TKey : struct 
    {
        public TKey Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Entity<TKey>)obj);
        }

        public bool Equals(Entity<TKey> other)
        {
            if (other is null)
            {
                return false;
            }

            return ReferenceEquals(this, other) || Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Id.GetHashCode();
        }
    }
}
