using System.Collections.Generic;
using UnityEngine;

namespace AnimalWorld.Collection
{
    [CreateAssetMenu(
        menuName = "Animal World/Collection/Item Catalog",
        fileName = "ItemCatalog")]
    public class ItemCatalog : ScriptableObject
    {
        [SerializeField] private List<Item> items = new();

        public IReadOnlyList<Item> Items => items;

#if UNITY_EDITOR
        private void OnValidate()
        {
            var usedIds = new HashSet<string>();
            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item == null)
                {
                    Debug.LogWarning($"{name}: empty slot at index {i}.", this);
                    continue;
                }

                if (!usedIds.Add(item.Id))
                {
                    Debug.LogWarning($"{name}: duplicate item id '{item.Id}' at index {i}.", this);
                }
            }
        }
#endif
    }
}
