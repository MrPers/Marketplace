using System;

namespace Marketplace.Contracts.Models
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}

