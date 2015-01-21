﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class DocumentDao : TreeDao
    {
        public virtual Guid? DocumentParentId
        {
            get { return DocumentParent == null ? (Guid?) null : DocumentParent.Id; }
        }

        public virtual DocumentDao DocumentParent { get; set; }

        public virtual Guid DocumentTypeId
        {
            get { return DocumentType.Id; }
        }

        public virtual TreeDao DocumentType { get; set; }

        public virtual Guid DocumentStateId
        {
            get { return DocumentState.Id; }
        }

        public virtual TreeDao DocumentState { get; set; }

        public virtual Guid DocumentUserId
        {
            get { return DocumentUser.Id; }
        }

        public virtual UserDao DocumentUser { get; set; }
        
        public virtual string Notes { get; set; }
    }
}
