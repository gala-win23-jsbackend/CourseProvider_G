

namespace Data.Entities;

public class ContentEntity
{
    public string? Description { get; set; }
    public string[]? Includes { get; set; }
    public virtual List<ProgramDetailEntity>? ProgramDetails { get; set; }
}
