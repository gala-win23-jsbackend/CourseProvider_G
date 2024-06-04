

using Data.Entities;
using Data.Models;
using Data.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Data.GraphQL.Mutations;

public class CourseMutation(ICourseService courseService)
{
    private readonly ICourseService _courseService = courseService;

    [GraphQLName("createCourse")]
    public async Task<Course> CreateCourseAsync(CourseCreateRequest input)
    {
        return await _courseService.CreateCourseAsync(input);
    }

    [GraphQLName("updateCourse")]
    public async Task<Course> UpdateCourseAsync(CourseUpdateRequest input)
    {
        return await _courseService.UpdateCourseAsync(input);
    }

    [GraphQLName("deleteCourse")]
    public async Task<bool> DeleteCourseAsync(string id)
    {
        return await _courseService.DeleteCourseAsync(id);
    }

    [GraphQLName("saveUserCourse")]
    public async Task<UserCoursesEntity> CreateUserCourse(CreateUserCourse input)
    {
        return await _courseService.CreateUserCourse(input);
    }

    [GraphQLName("deleteUserCourse")]
    public async Task<bool> DeleteUserCourse(UserCourses userCourse)
    {
        return await _courseService.DeleteUserCourse(userCourse);
    }

    [GraphQLName("deleteAllUserCourses")]
    public async Task<bool> DeleteAllUserCourses(string userId)
    {
        return await _courseService.DeleteAllUserCourses(userId);
    }
}
