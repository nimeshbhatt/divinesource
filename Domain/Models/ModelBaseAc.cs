using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class ModelBaseAc
    {
        [Key]
        public Guid Id { get; set; }
    }
}
