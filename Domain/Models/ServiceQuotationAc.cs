using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class ServiceQuotationAc : ModelBaseAc
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UsersAc Users { get; set; }

        /// <summary>
        /// 0 = Not Replied
        /// 1 = Replied
        /// Service Related Fields
        public string IsReply { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string ServiceTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string ServiceDescription { get; set; }

        public Guid? ServiceEngId { get; set; }
        [ForeignKey("ServiceEngId")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Service engineer selection required")]
        public virtual ServiceEngineerAc ServiceEngineer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date selection is required")]
        public DateTime? ServiceDate { get; set; }
        
        /// Quotation Related Fields
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
        public string QuotationTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        public string QuotationDesc { get; set; }


        /// <summary>
        /// 0 = Service
        /// 1 = Quotation
        /// </summary>
        public string Type { get; set; }

        public virtual ICollection<ServiceAttachmentAc> ServiceAttachments { get; set; }
    }
}
