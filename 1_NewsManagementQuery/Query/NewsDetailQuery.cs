using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1_NewsManagementQuery.Contracts.NewsDetail;
using Microsoft.VisualBasic.CompilerServices;
using NewsManagement.Domain.CommentAgg;
using NewsManagement.Domain.NewsAgg;

namespace _1_NewsManagementQuery.Query
{
    public class NewsDetailQuery : INewsDetailQuery
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICommentRepository _commentRepository;

        public NewsDetailQuery(INewsRepository newsRepository, ICommentRepository commentRepository)
        {
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
        }

        public NewsDetailQueryModel GetDetail(long newsId, long? userId)
        {
            var news = _newsRepository.GetNewsById(newsId, userId);
            var newsComments = _commentRepository.getNewsComments(newsId);
            var nextAndPreviousNews = _newsRepository.GetNextAndPreviousNews(news.PublishDateTime, userId);


            var newsDetailsQueryModel = new NewsDetailQueryModel(news, newsComments, null, nextAndPreviousNews);
            return newsDetailsQueryModel;
        }
    }
}
