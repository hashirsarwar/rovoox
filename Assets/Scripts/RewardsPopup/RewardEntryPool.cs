using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardEntryPool
{
    private RewardEntryView _prefab;
    private Transform _parent;

    private List<RewardEntryView> _pooledEntries = new();

    public RewardEntryPool(RewardEntryView prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public RewardEntryView Get()
    {
        foreach (var entry in _pooledEntries.Where(e => !e.gameObject.activeSelf))
        {
            entry.gameObject.SetActive(true);
            entry.transform.SetAsLastSibling();
            return entry;
        }

        return CreateNewEntry();
    }

    public void ResetPool()
    {
        foreach (var entry in _pooledEntries)
            entry.gameObject.SetActive(false);
    }
    
    private RewardEntryView CreateNewEntry()
    {
        var entry = Object.Instantiate(_prefab, _parent);
        _pooledEntries.Add(entry);
        entry.gameObject.SetActive(true);
        return entry;
    }
}
