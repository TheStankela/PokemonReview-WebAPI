using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiTest1.Dto;
using WebApiTest1.Interfaces;
using WebApiTest1.Models;

namespace WebApiTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewerRepository _reviewerRepository;

        public ReviewController(IMapper mapper, IReviewRepository reviewRepository, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet ("{id}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        public IActionResult GetReview(int id)
        {
            if (!_reviewRepository.ReviewExists(id))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet ("GetPokemonByReviewID/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        public IActionResult GetPokemonByReview(int reviewId) 
        { 
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_reviewRepository.GetPokemonByReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);

        }

        [HttpGet ("GetRevewerByReviewID/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        public IActionResult GetReviewerByReview(int reviewId)
        {
            if(!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var reviewer = _mapper.Map<ReviewerDto>(_reviewRepository.GetReviewerByReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromBody] ReviewDto reviewCreate, [FromQuery] int pokemonId, [FromQuery] int reviewerId)
        {
            if (reviewCreate == null)
                return BadRequest();

            var review = _reviewRepository.GetReviews()
                .Where(r => r.Title.Trim().ToUpper() == reviewCreate.Title.Trim().ToUpper()).SingleOrDefault();
            
            if (review != null)
            {
                ModelState.AddModelError("", "Review already exists.");
                return StatusCode(422, ModelState);
            }

            var reviewMap = _mapper.Map<Review>(reviewCreate);
            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokemonId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);

            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "Review successfully created.");
        }
        [HttpPut ("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview (int reviewId, [FromBody] ReviewDto review)
        {
            if (reviewId != review.Id)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound("Review does not exist.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(review);

            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Error while updating the review.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "Successfully updated the review.");

            
        }
        [HttpPut("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound("Review does not exist.");

            if (!ModelState.IsValid)
                return BadRequest();

            var reviewToDelete = _reviewRepository.GetReview(reviewId);

            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Error while updating the review.");
                return StatusCode(500, ModelState);
            }

            return StatusCode(200, "Successfully deleted the review.");
        }
    }
}
