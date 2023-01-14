using WebApiTest1.Models;

namespace WebApiTest1.Interfaces
{
    public interface IReviewerRepository
    {
        public ICollection<Reviewer> GetReviewers();
        public Reviewer GetReviewer(int id);

        public ICollection<Review> GetReviewsOfReviewer(int reviewerId);
        public bool ReviewerExists(int id);
        public bool CreateReviewer(Reviewer reviewer);
        public bool Save();

        public bool DeleteReviewer(Reviewer reviewer);
        public bool UpdateReviewer(Reviewer reviewer);
    }
}
