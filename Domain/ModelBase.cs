using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class ModelBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
