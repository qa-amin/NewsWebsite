using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace NewsManagement.Domain.VideoAgg
{
    public class Video : EntityBase<long>
    {
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string Poster { get; private set; }
        public DateTime? PublishDateTime { get; private set; }

        public Video(string title, string url, string poster, DateTime? publishDateTime)
        {
            Title = title;
            Url = url;
            Poster = poster;
            PublishDateTime = publishDateTime;
            
            
        }

        public void Edit(string title, string url, string poster, DateTime? publishDateTime)
        {
            Title = title;
            Url = url;
            Poster = poster;
            PublishDateTime = publishDateTime;
            UpdateDate = DateTime.Now;
        }

        public void Delete()
        {
            IsRemove = true;
            RemoveDate = DateTime.Now;
        }
    }


}
