using NewsManagement.Application.Contrasts.News;
using NewsManagement.Domain.CommentAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_NewsManagementQuery.Contracts.NewsDetail
{
    public class NewsDetailQueryModel
    {
        public NewsDetailQueryModel(NewsViewModel news, List<Comment> comments, List<NewsViewModel> newsRelated, List<NewsViewModel> nextAndPreviousNews)
        {
            News = news;
            Comments = comments;
            NewsRelated = newsRelated;
            NextAndPreviousNews = nextAndPreviousNews;
        }
        public NewsViewModel News { get; set; }
        public List<Comment> Comments { get; set; }
        public List<NewsViewModel> NewsRelated { get; set; }
        public List<NewsViewModel> NextAndPreviousNews { get; set; }
    }
}
