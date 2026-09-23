using System;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalWorld.Presentation
{
    public class ScreenTabs : MonoBehaviour
    {
        private const int WheelTab = 2;

        [SerializeField] private Button[] tabButtons;
        [SerializeField] private GameObject itemsScreen;
        [SerializeField] private GameObject wheelScreen;

        private static readonly Color SelectedTab = new(0.72f, 0.52f, 0.24f, 1f);
        private static readonly Color IdleTab = new(0.18f, 0.19f, 0.24f, 1f);

        public event Action<int> TabSelected;

        private void Start()
        {
            for (var i = 0; i < tabButtons.Length; i++)
            {
                var index = i;
                tabButtons[i].onClick.AddListener(() => Select(index));
            }

            Select(0);
        }

        public void SetTabsInteractable(bool interactable)
        {
            foreach (var button in tabButtons)
            {
                button.interactable = interactable;
            }
        }

        private void Select(int selected)
        {
            itemsScreen.SetActive(selected != WheelTab);
            wheelScreen.SetActive(selected == WheelTab);

            for (var i = 0; i < tabButtons.Length; i++)
            {
                tabButtons[i].image.color = i == selected ? SelectedTab : IdleTab;
            }

            TabSelected?.Invoke(selected);
        }
    }
}
