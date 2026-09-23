using System;
using AnimalWorld.Collection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalWorld.Presentation
{
    public class ItemCardView : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI rarityText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI detailsText;
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private Button unlockButton;
        [SerializeField] private TextMeshProUGUI unlockButtonText;

        private static readonly Color OwnedStatus = new(0.55f, 0.78f, 0.52f, 1f);
        private static readonly Color AffordableStatus = new(0.85f, 0.72f, 0.38f, 1f);
        private static readonly Color LockedStatus = new(0.68f, 0.44f, 0.44f, 1f);
        private static readonly Color LockedIcon = new(0.42f, 0.44f, 0.5f, 1f);

        private Item item;

        public event Action<Item> UnlockRequested;

        public Item Item => item;

        private void Awake()
        {
            unlockButton.onClick.AddListener(() => UnlockRequested?.Invoke(item));
        }

        public void Display(Item itemToShow, bool isOwned, bool canAfford, bool showUnlockButton)
        {
            item = itemToShow;
            iconImage.sprite = item.Icon;
            nameText.text = item.DisplayName;
            rarityText.text = item.Rarity.ToString();
            descriptionText.text = item.Description;
            detailsText.text = item.Details;
            detailsText.gameObject.SetActive(!string.IsNullOrEmpty(item.Details));
            unlockButton.gameObject.SetActive(showUnlockButton);
            SetState(isOwned, canAfford);
        }

        public void SetState(bool isOwned, bool canAfford)
        {
            if (isOwned)
            {
                statusText.text = "Collected";
                statusText.color = OwnedStatus;
            }
            else if (canAfford)
            {
                statusText.text = $"Unlock for {item.UnlockCost} pts";
                statusText.color = AffordableStatus;
            }
            else
            {
                statusText.text = $"Need {item.UnlockCost} pts";
                statusText.color = LockedStatus;
            }

            iconImage.color = isOwned ? Color.white : LockedIcon;
            unlockButtonText.text = isOwned ? "Owned" : "Unlock";
            unlockButton.interactable = !isOwned && canAfford;
        }
    }
}
