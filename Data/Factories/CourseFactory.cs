

using Data.Entities;
using Data.Models;


namespace Data.Factories;

public static class CourseFactory
{
    public static CourseEntity CreateCourse(CourseCreateRequest request)
    {
        return new CourseEntity
        {
            Id = Guid.NewGuid().ToString(),
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            IsBestseller = request.IsBestseller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Ingress = request.Ingress,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            Likes = request.Likes,
            LikesInProcent = request.LikesInProcent,
            Hours = request.Hours,
            Authors = request.Authors?.Select(author => new AuthorEntity
            {
                Name = author.Name,
                AuthorImg = author.AuthorImg
            }).ToList(),
            Prices = request.Prices == null ? null : new PriceEntity
            {
                Discount = request.Prices.Discount,
                Price = request.Prices.Price,
                Currency = request.Prices.Currency
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                ProgramDetails = request.Content.ProgramDetails?.Select((programDetail, index) => new ProgramDetailEntity
                {
                    Id = index + 1, 
                    Title = programDetail.Title,
                    Description = programDetail.Description
                }).ToList()
            }
        };
    }

    public static CourseEntity UpdateCourse(CourseUpdateRequest request)
    {
        return new CourseEntity
        {
            Id = request.Id,
            ImageUri = request.ImageUri,
            ImageHeaderUri = request.ImageHeaderUri,
            IsBestseller = request.IsBestseller,
            IsDigital = request.IsDigital,
            Categories = request.Categories,
            Title = request.Title,
            Ingress = request.Ingress,
            StarRating = request.StarRating,
            Reviews = request.Reviews,
            Likes = request.Likes,
            LikesInProcent = request.LikesInProcent,
            Hours = request.Hours,
            Authors = request.Authors?.Select(author => new AuthorEntity
            {
                Name = author.Name,
                AuthorImg = author.AuthorImg
            }).ToList(),
            Prices = request.Prices == null ? null : new PriceEntity
            {
                Discount = request.Prices.Discount,
                Price = request.Prices.Price,
                Currency = request.Prices.Currency
            },
            Content = request.Content == null ? null : new ContentEntity
            {
                Description = request.Content.Description,
                Includes = request.Content.Includes,
                ProgramDetails = request.Content.ProgramDetails?.Select(programDetail => new ProgramDetailEntity
                {
                    Id = programDetail.Id,
                    Title = programDetail.Title,
                    Description = programDetail.Description
                }).ToList()
            }
        };
    }

    public static Course CreateToModel(CourseEntity entity)
    {
        return new Course
        {
            Id = entity.Id,
            ImageUri = entity.ImageUri,
            ImageHeaderUri = entity.ImageHeaderUri,
            IsBestseller = entity.IsBestseller,
            IsDigital = entity.IsDigital,
            Categories = entity.Categories,
            Title = entity.Title,
            Ingress = entity.Ingress,
            StarRating = entity.StarRating,
            Reviews = entity.Reviews,
            IsBookmarked = entity.IsBookmarked ?? null,
            Likes = entity.Likes,
            LikesInProcent = entity.LikesInProcent,
            Hours = entity.Hours,
            Authors = entity.Authors?.Select(author => new Author
            {
                Name = author.Name,
                AuthorImg = author.AuthorImg
            }).ToList(),
            Prices = entity.Prices == null ? null : new Prices
            {
                Discount = entity.Prices.Discount,
                Price = entity.Prices.Price,
                Currency = entity.Prices.Currency
            },
            Content = entity.Content == null ? null : new Content
            {
                Description = entity.Content.Description,
                Includes = entity.Content.Includes,
                ProgramDetails = entity.Content.ProgramDetails?.Select(programDetail => new ProgramDetail
                {
                    Id = programDetail.Id,
                    Title = programDetail.Title,
                    Description = programDetail.Description
                }).ToList()
            }
        };
    }
}
