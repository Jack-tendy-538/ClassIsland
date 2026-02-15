namespace ClassIsland.Core.Search;

public class SettingSearchItem
{
    public string Id { get; init; } = "";
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
    public string IconGlyph { get; init; } = "";
    public string? GroupId { get; init; }
    public string Keywords { get; init; } = "";
}
