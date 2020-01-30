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

        private RectTransform parentRectTransform;

        private readonly List<GameObject> sliderElements = new List<GameObject>();

        protected void Initialize(GameObject leftArrow, GameObject rightArrow, LinearUiSpacing linearUiSpacing)
        {
            leftButton = leftArrow;
            rightButton = rightArrow;
            leftButton.GetComponent<Button>().onClick.AddListener(ScrollOneLeft);
            rightButton.GetComponent<Button>().onClick.AddListener(ScrollOneRight);
            this.linearUiSpacing = linearUiSpacing;
            parentRectTransform = gameObject.GetComponent<RectTransform>();
        }

        protected void RenderSliderElements() // TODO fix, creates more buttons than needed, when maxPlayer count is low
        {
            DeleteButtons();
            SetSliderButtonActivity();
            int partAmount = Mathf.Min(linearUiSpacing.partAmount, GetObjectCount());
            for (int i = 0; i < partAmount; i++)
            {
                GameObject element = GetObjectAt(currentPosition + i);
                //GameObjectUtility.InstantiateChild(element, gameObject, true); // TODO consider this
                RectTransform elementRectTransform = element.GetComponent<RectTransform>();
                float relativeStart = -(parentRectTransform.rect.width - elementRectTransform.rect.width) / 2;
                element.transform.localPosition = new Vector3(relativeStart + linearUiSpacing.GetNthPathPosition(i), 0.0F, 0.0F);
                sliderElements.Add(element);
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
            int maxElementCount = GetObjectCount();

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
        public abstract int GetObjectCount();
    }
}
