using Flunt.Notifications;
using System;

namespace CadContato.Domain.Entities
{
    public abstract class Entity : Notifiable, IEquatable<Entity>
    {
        public Guid Id { get; private set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public bool Equals(Entity other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}
