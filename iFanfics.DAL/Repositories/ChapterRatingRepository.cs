﻿using iFanfics.DAL.EF;
using iFanfics.DAL.Entities;
using iFanfics.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace iFanfics.DAL.Repositories {
    class ChapterRatingRepository : Repository<ChapterRating, string>, IChapterRatingRepository {
        public ChapterRatingRepository(ApplicationContext db) : base(db) { }

        public bool CheckForGivenRating(string userId, string chapterId) {
            return _context.ChaptersRating.FirstOrDefault(a => a.ApplicationUserId.Equals(userId) && a.ChapterId.Equals(chapterId)) != null;
        }

        public IEnumerable<ChapterRating> GetChapterRatins(string id) {
            return _context.ChaptersRating.Where(a => a.ChapterId.Equals(id));
        }
    }
}