using DomainValidator.Notifications;
using System;

namespace Shared.Entities
{
	public abstract class BaseEntity : Notifiable
	{
		public Guid Id { get; protected set; }

		public BaseEntity() { }

		public BaseEntity(Guid id) => Id = id;

		public abstract void Validate();
	}
}
