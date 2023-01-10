﻿using WebApiTest1.Data;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Pokemon GetPokemonByReview(int reviewId)
        {
            return _dataContext.Reviews.Where(p => p.Id == reviewId).Select(p => p.Pokemon).FirstOrDefault();
        }

        public Reviewer GetReviewerByReview(int reviewId)
        {
            return _dataContext.Reviews.Where(r => r.Id == reviewId).Select(r => r.Reviewer).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.ToList();
        }

        public Review GetReview(int id)
        {
            return _dataContext.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(r => r.Id == id);
        }
    }
}