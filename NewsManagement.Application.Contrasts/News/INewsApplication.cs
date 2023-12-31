﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace NewsManagement.Application.Contrasts.News
{
    public interface INewsApplication
    {
        OperationResult Create(CreateNews command );
        OperationResult Edit(EditNews command );
        OperationResult Delete(long id  );
        (List<NewsViewModel>, long) Search(NewsSearchModel searchModel);
        EditNews GetDetails(long id);

        long[] CategoryIds(long newsId);

        string GetTagNames(long NewsId);

        void RemoveNewsTagRecords(long newsId);
        void RemoveNewsNewsCategoryRecords(long newsId);

        NewsViewModel NumberOfLikeAndDisLike(long newsId);

        long GetPublished();

        long CountNews();

        long CountFuturePublish();

        long CountNewsUnPublished();

        long NumberOfVisit(DateTime startDateTimeMiladi, DateTime endDateTimeMiladi);
    }
}
