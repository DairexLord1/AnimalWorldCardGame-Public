using AnimalWorld.Collection;
using TMPro;
using UnityEngine;

namespace AnimalWorld.Presentation
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private ItemCatalog catalog;
        [SerializeField] private ScreenTabs screenTabs;
        [SerializeField] private ItemsView itemsView;
        [SerializeField] private WheelView wheelView;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private TextMeshProUGUI pointsText;

        private ItemsPresenter presenter;

        private void Awake()
        {
            presenter = new ItemsPresenter(catalog, screenTabs, itemsView, wheelView, progressText, pointsText);
        }

        private void Start()
        {
            presenter.Initialize();
        }
    }
}
