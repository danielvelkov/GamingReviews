﻿using GamingReviews.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingReviews.Models
{
    class ReviewsRepository:Repository<Reviews>, IReviewsRepository
    {
        public ReviewsRepository(GameNewsLetterContext context) : base(context) { }

        public GameNewsLetterContext PlutoContext
        {
            get { return Context as GameNewsLetterContext; }
        }

        // implement the methods from the used interface


    }
}
