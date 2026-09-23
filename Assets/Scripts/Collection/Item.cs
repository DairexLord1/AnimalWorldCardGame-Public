using UnityEngine;

namespace AnimalWorld.Collection
{
    [CreateAssetMenu(
        menuName = "Animal World/Collection/Item Definition",
        fileName = "Item_New")]
    public class Item : ScriptableObject
    {
        [Header("Identity")]
        [SerializeField] private string id;
        [SerializeField] private string displayName;
        [SerializeField, TextArea(2, 5)] private string description;

        [Header("Presentation")]
        [SerializeField] private Sprite icon;
        [SerializeField] private ItemRarity rarity = ItemRarity.Common;

        [Header("Unlocking")]
        [SerializeField, Min(0)] private int unlockCost;

        public string Id => id;
        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;
        public ItemRarity Rarity => rarity;
        public int UnlockCost => unlockCost;

        public virtual string Details => null;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = name;
            }
        }
#endif
    }
}
