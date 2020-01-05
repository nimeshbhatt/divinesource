using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class UploadDocumentImageAc : ModelBaseAc
    {
        public Guid? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UsersAc Users { get; set; }

        //Document can be PDF, WORD, EXCEL, IMAGE or any thing
        public string DocumentPath { get; set; }

        // 0 = Image required to show on client app
        // 1 = All the documents upload for particular client
        public string Type { get; set; }
        public string IsDelete { get; set; }
    }
}
