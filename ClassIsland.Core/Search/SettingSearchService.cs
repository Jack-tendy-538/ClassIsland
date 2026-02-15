using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Services.Registry;

namespace ClassIsland.Core.Search;

public class SettingSearchService
{
    private readonly List<SettingSearchItem> _index = new();

    public SettingSearchService()
    {
        RebuildIndex();
        SettingsWindowRegistryService.Registered.CollectionChanged += OnRegisteredCollectionChanged;
    }

    private void RebuildIndex()
    {
        _index.Clear();
        foreach (var pageInfo in SettingsWindowRegistryService.Registered)
        {
            _index.Add(MapToSearchItem(pageInfo));
        }
    }

    private SettingSearchItem MapToSearchItem(SettingsPageInfo pageInfo)
    {
        return new SettingSearchItem
        {
            Id = pageInfo.Id,
            Title = pageInfo.Name,
            Description = string.Empty,
            IconGlyph = pageInfo.UnSelectedIconGlyph ?? string.Empty,
            GroupId = pageInfo.GroupId,
            Keywords = GenerateKeywords(pageInfo)
        };
    }

    private string GenerateKeywords(SettingsPageInfo pageInfo)
    {
        var words = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);
        if (!string.IsNullOrWhiteSpace(pageInfo.Name))
            words.Add(pageInfo.Name);
        if (!string.IsNullOrWhiteSpace(pageInfo.Id))
            words.Add(pageInfo.Id);
        if (!string.IsNullOrWhiteSpace(pageInfo.GroupId))
            words.Add(pageInfo.GroupId!);
        if (!string.IsNullOrWhiteSpace(pageInfo.UnSelectedIconGlyph))
            words.Add(pageInfo.UnSelectedIconGlyph);

        return string.Join(' ', words);
    }

    private void OnRegisteredCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (SettingsPageInfo newPage in e.NewItems)
            {
                _index.Add(MapToSearchItem(newPage));
            }
        }
        if (e.OldItems != null)
        {
            foreach (SettingsPageInfo oldPage in e.OldItems)
            {
                var item = _index.FirstOrDefault(x => x.Id == oldPage.Id);
                if (item != null)
                    _index.Remove(item);
            }
        }
    }

    public IEnumerable<SettingSearchItem> Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Enumerable.Empty<SettingSearchItem>();

        var q = query.ToLowerInvariant();
        return _index
            .Where(item =>
                (!string.IsNullOrEmpty(item.Title) && item.Title.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(item.Id) && item.Id.ToLowerInvariant().Contains(q)) ||
                (!string.IsNullOrEmpty(item.Keywords) && item.Keywords.ToLowerInvariant().Contains(q))
            )
            .OrderBy(item => item.Title)
            .ToList();
    }
}
