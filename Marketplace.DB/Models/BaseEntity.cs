using Marketplace.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DB.Models
{
    public class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
