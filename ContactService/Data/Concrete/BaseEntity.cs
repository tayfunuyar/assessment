using System;
using System.ComponentModel.DataAnnotations;
using ContactService.Data.Abstract;

namespace ContactService.Data.Concrete
{
    public class BaseEntity : IEntity
    {
        [Key]
        [Required]
        public Guid Uuid { get; set; }
    }
}