using System;
using System.Collections.Generic;
using AnimalWorld.Collection;
using UnityEngine;

namespace AnimalWorld.Presentation
{
    public class ItemsView : MonoBehaviour
    {
        [SerializeField] private ItemCardView cardPrefab;
        [SerializeField] private RectTransform cardContainer;
        [SerializeField] private GameObject emptyMessage;

        private readonly List<ItemCardView> displayedCards = new();

        public event Action<Item> UnlockRequested;

        public void ShowItems(IReadOnlyList<Item> items, Func<Item, bool> isOwned,
            Func<Item, bool> canAfford, bool showUnlockButton)
        {
            ClearCards();
            emptyMessage.SetActive(items.Count == 0);

            foreach (var item in items)
            {
                if (item == null)
                {
                    continue;
                }

                var card = Instantiate(cardPrefab, cardContainer);
                card.Display(item, isOwned(item), canAfford(item), showUnlockButton);
                card.UnlockRequested += OnCardUnlockRequested;
                displayedCards.Add(card);
            }
        }

        public void RefreshState(Func<Item, bool> isOwned, Func<Item, bool> canAfford)
        {
            foreach (var card in displayedCards)
            {
                card.SetState(isOwned(card.Item), canAfford(card.Item));
            }
        }

        private void ClearCards()
        {
            foreach (var card in displayedCards)
            {
                card.UnlockRequested -= OnCardUnlockRequested;
                Destroy(card.gameObject);
            }

            displayedCards.Clear();
        }

        private void OnCardUnlockRequested(Item item) => UnlockRequested?.Invoke(item);
    }
}
