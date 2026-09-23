using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimalWorld.Presentation
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private RectTransform wheel;
        [SerializeField] private TextMeshProUGUI[] segmentLabels;
        [SerializeField] private Button spinButton;
        [SerializeField] private TextMeshProUGUI resultText;

        private const float SpinDuration = 1.5f;
        private const float SpinTurns = 4f;

        private float wheelAngle;

        public event Action SpinRequested;
        public event Action<int> SpinFinished;

        private void Awake()
        {
            spinButton.onClick.AddListener(() => SpinRequested?.Invoke());
        }

        public void ShowRewards(IReadOnlyList<int> rewards)
        {
            for (var i = 0; i < segmentLabels.Length; i++)
            {
                segmentLabels[i].text = rewards[i].ToString();
            }

            resultText.text = "Spin to earn points";
        }

        public void PlaySpin(int segmentIndex)
        {
            StartCoroutine(SpinRoutine(segmentIndex));
        }

        private IEnumerator SpinRoutine(int segmentIndex)
        {
            spinButton.interactable = false;
            resultText.text = "Spinning...";

            var segmentAngle = 360f / segmentLabels.Length;
            var start = wheelAngle;
            var target = start + SpinTurns * 360f + Mathf.Repeat(segmentIndex * segmentAngle - start, 360f);

            for (var elapsed = 0f; elapsed < SpinDuration; elapsed += Time.deltaTime)
            {
                var progress = 1f - Mathf.Pow(1f - elapsed / SpinDuration, 3f);
                wheel.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(start, target, progress));
                yield return null;
            }

            wheelAngle = Mathf.Repeat(target, 360f);
            wheel.localRotation = Quaternion.Euler(0f, 0f, wheelAngle);

            resultText.text = $"+{segmentLabels[segmentIndex].text} points";
            spinButton.interactable = true;
            SpinFinished?.Invoke(segmentIndex);
        }
    }
}
