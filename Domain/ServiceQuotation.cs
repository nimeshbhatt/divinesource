using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ServiceQuotation : ModelBase
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        /// <summary>
        /// 0 = Not Replied
        /// 1 = Replied
        /// Service Related Fields
        public string IsReply { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceDescription { get; set; }

        public Guid? ServiceEngId { get; set; }
        [ForeignKey("ServiceEngId")]
        public virtual ServiceEngineer ServiceEngineer { get; set; }
        public DateTime? ServiceDate { get; set; }
        
        /// Quotation Related Fields
        public string QuotationTitle { get; set; }
        public string QuotationDesc { get; set; }


        /// <summary>
        /// 0 = Service
        /// 1 = Quotation
        /// </summary>
        public ServiceTypeEnum Type { get; set; }


        public virtual ICollection<ServiceAttachment> ServiceAttachments { get; set; }
    }
}
