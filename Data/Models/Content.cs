

namespace Data.Models;

public class Content
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public virtual List<ProgramDetail>? ProgramDetails { get; set; }
}
