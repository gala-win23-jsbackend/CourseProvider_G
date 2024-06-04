

namespace Data.Models;

public class ContentUpdateRequest
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public virtual List<ProgramDetailUpdateRequest>? ProgramDetails { get; set; }
}
