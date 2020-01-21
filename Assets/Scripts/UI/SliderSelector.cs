using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public abstract class SliderSelector : MonoBehaviour
    {
        private GameObject leftButton;
        private GameObject rightButton;
        private LinearUiSpacing linearUiSpacing;
        private int currentPosition = 0;
        private int maxElementCount;

        private List<GameObject> sliderElements = new List<GameObject>();

        protected SliderSelector(GameObject leftArrow, GameObject rightArrow, LinearUiSpacing linearUiSpacing, int maxElementCount)
        {
            leftButton = leftArrow;
            rightButton = rightArrow;
            leftButton.GetComponent<Button>().onClick.AddListener(ScrollOneLeft);
            rightButton.GetComponent<Button>().onClick.AddListener(ScrollOneRight);
            this.linearUiSpacing = linearUiSpacing;
            this.maxElementCount = maxElementCount;
        }

        private void RenderSliderElements()
        {
            DeleteButtons();
            SetSliderButtonActivity();
            for (int i = 0; i < linearUiSpacing.partAmount; i++)
            {
                GetObjectAt(currentPosition + i);
            }
        }

        private void ScrollOneRight()
        {
            currentPosition++;
            RenderSliderElements();
        }

        private void ScrollOneLeft()
        {
            currentPosition--;
            RenderSliderElements();
        }

        private void SetSliderButtonActivity()
        {
            if (currentPosition <= 0)
            {
                leftButton.SetActive(false);
            }
            else
            {
                leftButton.SetActive(true);
            }

            if (currentPosition + linearUiSpacing.partAmount >= maxElementCount)
            {
                rightButton.SetActive(false);
            }
            else
            {
                rightButton.SetActive(true);
            }
        }

        private void DeleteButtons()
        {
            foreach (GameObject gameObject in sliderElements)
            {
                GameObject.Destroy(gameObject);
            }
            sliderElements.Clear();
        }

        public abstract GameObject GetObjectAt(int position);
    }
}
