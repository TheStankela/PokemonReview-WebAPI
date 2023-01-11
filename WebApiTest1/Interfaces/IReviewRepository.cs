using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface IReviewRepository
    {
        public ICollection<Review> GetReviews();
        public Review GetReview(int id);
        public Reviewer GetReviewerByReview(int reviewId);
        public Pokemon GetPokemonByReview(int reviewId);
        public bool ReviewExists(int id);
        public bool CreateReview(Review review);
        public bool Save();
    }
}
