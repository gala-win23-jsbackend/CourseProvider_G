
using Data.Contexts;
using Data.Entities;
using Data.Factories;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class CourseService(IDbContextFactory<DataContext> contextFactory) : ICourseService
{
    private readonly IDbContextFactory<DataContext> _contextFactory = contextFactory;

    public async Task<Course> CreateCourseAsync(CourseCreateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();

        var courseEntity = CourseFactory.CreateCourse(request);
        context.Courses.Add(courseEntity);
        await context.SaveChangesAsync();

        await context.DisposeAsync();
        return CourseFactory.CreateToModel(courseEntity);

    }


    public async Task<bool> DeleteCourseAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntity = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (courseEntity == null) return false;

        context.Courses.Remove(courseEntity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<Course> GetCourseByIdAsync(string id)
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntity = await context.Courses.FirstOrDefaultAsync(c => c.Id == id);
        if (courseEntity == null)
        {
            await context.DisposeAsync();
            return null!;
        }
        else
        {
            var course = CourseFactory.CreateToModel(courseEntity);
            await context.DisposeAsync();
            return course;
        }

    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        await using var context = _contextFactory.CreateDbContext();
        var courseEntities = await context.Courses.ToListAsync();
        return courseEntities.Select(CourseFactory.CreateToModel);

    }

    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest request)
    {
        await using var context = _contextFactory.CreateDbContext();
        var existingCourse = await context.Courses
        .Include(c => c.Authors)
        .Include(c => c.Prices)
        .Include(c => c.Content)
        .ThenInclude(content => content!.ProgramDetails)
        .FirstOrDefaultAsync(c => c.Id == request.Id);
        if (existingCourse == null)
            return null!;

        existingCourse.Id = request.Id;
        existingCourse.ImageUri = request.ImageUri;
        existingCourse.ImageHeaderUri = request.ImageHeaderUri;
        existingCourse.IsBestseller = request.IsBestseller;
        existingCourse.IsDigital = request.IsDigital;
        existingCourse.Categories = request.Categories;
        existingCourse.Title = request.Title;
        existingCourse.Ingress = request.Ingress;
        existingCourse.StarRating = request.StarRating;
        existingCourse.Reviews = request.Reviews;
        existingCourse.Likes = request.Likes;
        existingCourse.LikesInProcent = request.LikesInProcent;
        existingCourse.Hours = request.Hours;

        // Update Authors
        if (existingCourse.Authors != null)
        {
            context.RemoveRange(existingCourse.Authors);
            existingCourse.Authors.Clear();
        }
        if (request.Authors != null)
        {
            foreach (var author in request.Authors)
            {
                existingCourse.Authors!.Add(new AuthorEntity { Name = author.Name, AuthorImg = author.AuthorImg });
            }
        }

        // Update Prices
        if (existingCourse.Prices == null)
        {
            existingCourse.Prices = new PriceEntity();
        }
        existingCourse.Prices.Currency = request.Prices!.Currency;
        existingCourse.Prices.Price = request.Prices.Price;
        existingCourse.Prices.Discount = request.Prices.Discount;

        // Update Content
        if (existingCourse.Content == null)
        {
            existingCourse.Content = new ContentEntity();
        }
        existingCourse.Content.Description = request.Content!.Description;
        existingCourse.Content.Includes = request.Content.Includes;

        // Update ProgramDetails
        if (existingCourse.Content.ProgramDetails != null)
        {
            context.RemoveRange(existingCourse.Content.ProgramDetails);
            existingCourse.Content.ProgramDetails.Clear();
        }
        if (request.Content.ProgramDetails != null)
        {
            foreach (var programDetail in request.Content.ProgramDetails)
            {
                existingCourse.Content.ProgramDetails!.Add(new ProgramDetailEntity
                {
                    Id = programDetail.Id,
                    Title = programDetail.Title,
                    Description = programDetail.Description
                });
            }
        }

        //context.Entry(existingCourse).CurrentValues.SetValues(updatedCourseEntity);

        await context.SaveChangesAsync();
        await context.DisposeAsync();
        return CourseFactory.CreateToModel(existingCourse);
    }

    public async Task<UserCoursesEntity> CreateUserCourse(CreateUserCourse request)
    {
        try
        {
            await using var context = _contextFactory.CreateDbContext();

            var userCourse = new UserCoursesEntity
            {
                CourseId = request.CourseId,
                UserId = request.UserId
            };
            context.UserCourses.Add(userCourse);
            var courseEntity = await context.Courses.FirstOrDefaultAsync(c => c.Id == userCourse.CourseId);
            if (courseEntity == null)
            {
                courseEntity!.IsBookmarked = true;
            }
            await context.SaveChangesAsync();
            return userCourse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<bool> DeleteUserCourse(UserCourses userCourse)
    {
        try
        {
            await using var context = _contextFactory.CreateDbContext();
            var existingUserCourse = await context.UserCourses.FirstOrDefaultAsync(u => u.UserId == userCourse.UserId && u.CourseId == userCourse.CourseId);
            if (existingUserCourse == null) return false;

            context.UserCourses.Remove(existingUserCourse);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAllUserCourses(string userId)
    {
        try
        {
            await using var context = _contextFactory.CreateDbContext();
            var userCourses = await context.UserCourses.Where(u => u.UserId == userId).ToListAsync();
            if (userCourses.Count == 0)
                return false;
            foreach (var userCourse in userCourses)
            {
                context.UserCourses.Remove(userCourse);
            };
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<string>> GetUserCourseIds(string userId)
    {
        try
        {
            await using var context = _contextFactory.CreateDbContext();
            var courseIds = await context.UserCourses
                .Where(u => u.UserId == userId)
                .Select(u => u.CourseId)
                .ToListAsync();

            return courseIds;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}
public interface ICourseService
{
    Task<Course> CreateCourseAsync(CourseCreateRequest request);

    Task<Course> UpdateCourseAsync(CourseUpdateRequest request);

    Task<bool> DeleteCourseAsync(string id);

    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<Course> GetCourseByIdAsync(string id);
    Task<UserCoursesEntity> CreateUserCourse(CreateUserCourse request);
    Task<IEnumerable<string>> GetUserCourseIds(string userId);
    Task<bool> DeleteUserCourse(UserCourses userCourse);
    Task<bool> DeleteAllUserCourses(string userId);
}
