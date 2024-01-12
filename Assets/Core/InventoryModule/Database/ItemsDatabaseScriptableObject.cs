using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "ItemsDatabase", menuName = "BlueGravity/ItemsDatabase", order = 1)]
public class ItemsDatabaseScriptableObject : ScriptableObject
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();
    public Dictionary<string, ItemData> itemsDictionary = new Dictionary<string, ItemData>();

    public string search;
    private string previousSearch;
    public List<ItemData> searchBar = new List<ItemData>();

    public void Awake()
    {
        itemsDictionary = items.ToDictionary(item => item.id, item => item);
    }

    public void OnValidate()
    {
        if (previousSearch != search)
        {
            searchBar = items.Where(item => (item.id == search) || item.name.Contains(search)).ToList();
            previousSearch = search;
        }

        itemsDictionary = items.ToDictionary(item => item.id, item => item);
    }
}
