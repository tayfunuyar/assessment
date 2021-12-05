using System;
using System.ComponentModel.DataAnnotations;

namespace ReportService.Data.Abstract
{

    public interface IEntity
    { 
        public Guid Uuid { get; set; }
    }
}