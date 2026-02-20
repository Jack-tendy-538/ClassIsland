namespace ClassIsland.Core.Search;
/// <summary>
/// 搜索设置项
/// </summary>
public class SettingSearchItem
{
    // <summary>
    // 设置项ID
    // </summary>
    /// <summary>
    /// 设置项ID
    /// </summary>
    public string Id { get; init; } = "";
    /// <summary>
    /// 设置项标题
    /// </summary>
    public string Title { get; init; } = "";
    /// <summary>
    /// 设置项描述
    /// </summary>
    public string Description { get; init; } = "";
    /// <summary> 设置项图标字体
    /// </summary>
    public string IconGlyph { get; init; } = "";
    /// <summary>
    ///  设置项所属分组ID
    /// </summary>
    public string? GroupId { get; init; }
    /// <summary> 设置项关键词
    /// </summary>
    public string Keywords { get; init; } = "";
}
