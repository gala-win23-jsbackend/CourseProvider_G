

namespace Data.Models;

public class ContentCreateRequest
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public virtual List<ProgramDetailCreateRequest>? ProgramDetails { get; set; }
}
