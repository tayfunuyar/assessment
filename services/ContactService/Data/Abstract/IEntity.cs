using System;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Data.Abstract
{

    public interface IEntity
    { 
        public Guid Uuid { get; set; }
    }
}