using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace NewsManagement.Domain.TagAgg
{
    public class Tag : EntityBase<long>
    {
       
        public string TagName { get; private set; }

        public Tag(string tagName)
        {
            TagName = tagName;
            CreationDate = DateTime.Now;
        }

        public void Edit(string tagName)
        {
            TagName = tagName;
            UpdateDate = DateTime.Now;
        }

        public void Delete()
        {
            IsRemove = true;
        }
    }
}
