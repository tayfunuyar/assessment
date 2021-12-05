using System;
using System.ComponentModel.DataAnnotations;
using ReportService.Data.Abstract;

namespace ReportService.Data.Concrete
{
    public class BaseEntity : IEntity
    {
        [Key]
        [Required]
        public Guid Uuid { get; set; }
    }
}