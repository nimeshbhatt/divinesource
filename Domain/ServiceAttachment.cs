using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ServiceAttachment : ModelBase
    {
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual ServiceQuotation ServiceQuotation { get; set; }

        public string ServiceAttachPath { get; set; }
    }
}
