using AnimalWorld.Collection;
using AnimalWorld.Inventory;
using AnimalWorld.Progression;
using TMPro;

namespace AnimalWorld.Presentation
{
    public class ItemsPresenter
    {
        private const int CollectionTab = 0;
        private const int InventoryTab = 1;

        private readonly ItemCatalog catalog;
        private readonly ScreenTabs screenTabs;
        private readonly ItemsView itemsView;
        private readonly WheelView wheelView;
        private readonly TextMeshProUGUI progressText;
        private readonly TextMeshProUGUI pointsText;

        private readonly InventoryService inventoryService = new();
        private readonly PointsService pointsService = new();

        public ItemsPresenter(ItemCatalog catalog, ScreenTabs screenTabs, ItemsView itemsView,
            WheelView wheelView, TextMeshProUGUI progressText, TextMeshProUGUI pointsText)
        {
            this.catalog = catalog;
            this.screenTabs = screenTabs;
            this.itemsView = itemsView;
            this.wheelView = wheelView;
            this.progressText = progressText;
            this.pointsText = pointsText;

            screenTabs.TabSelected += OnTabSelected;
            itemsView.UnlockRequested += Unlock;
            wheelView.SpinRequested += OnSpinRequested;
            wheelView.SpinFinished += OnSpinFinished;
            inventoryService.InventoryChanged += OnStateChanged;
            pointsService.BalanceChanged += OnStateChanged;
        }

        public void Initialize()
        {
            wheelView.ShowRewards(pointsService.Rewards);
            RefreshHeader();
        }

        private void OnTabSelected(int tab)
        {
            if (tab == CollectionTab)
            {
                itemsView.ShowItems(catalog.Items, inventoryService.IsOwned, CanAfford, true);
            }
            else if (tab == InventoryTab)
            {
                itemsView.ShowItems(inventoryService.OwnedItems, inventoryService.IsOwned, CanAfford, false);
            }
        }

        private void Unlock(Item item)
        {
            if (inventoryService.IsOwned(item) || !pointsService.TrySpend(item.UnlockCost))
            {
                return;
            }

            inventoryService.AddItem(item);
        }

        private void OnSpinRequested()
        {
            screenTabs.SetTabsInteractable(false);
            wheelView.PlaySpin(pointsService.RollRewardIndex());
        }

        private void OnSpinFinished(int segmentIndex)
        {
            pointsService.Add(pointsService.Rewards[segmentIndex]);
            screenTabs.SetTabsInteractable(true);
        }

        private bool CanAfford(Item item) => pointsService.CanAfford(item.UnlockCost);

        private void OnStateChanged()
        {
            itemsView.RefreshState(inventoryService.IsOwned, CanAfford);
            RefreshHeader();
        }

        private void RefreshHeader()
        {
            progressText.text = $"{inventoryService.OwnedItems.Count} / {catalog.Items.Count} collected";
            pointsText.text = $"{pointsService.Balance} pts";
        }
    }
}
