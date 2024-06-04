

namespace Data.Models;

public class Course
{
    public string? Id { get; set; }
    public string? ImageUri { get; set; }
    public string? ImageHeaderUri { get; set; }
    public bool IsBestseller { get; set; }
    public bool IsDigital { get; set; }
    public string[]? Categories { get; set; }
    public string? Title { get; set; }
    public string? Ingress { get; set; }
    public decimal StarRating { get; set; }
    public string? Reviews { get; set; }
    public string? Likes { get; set; }

    public bool? IsBookmarked { get; set; }
    public string? LikesInProcent { get; set; }
    public string? Hours { get; set; }
    public virtual List<Author>? Authors { get; set; }
    public virtual Prices? Prices { get; set; }
    public virtual Content? Content { get; set; }
}



