

using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UserCoursesEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = null!;
    public string CourseId { get; set; } = null!;
}
