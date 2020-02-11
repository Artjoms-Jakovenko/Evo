using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LinearSlider // TODO make vertical too
    {
        public float offset = 0.0F;
        public float spacing = 0.0F;

        private GameObject sliderContent;
        private RectTransform parentRectTransform;


        private readonly List<GameObject> sliderElements = new List<GameObject>();
        private float sliderLength = 0.0F;

        public LinearSlider(GameObject sliderContent)
        {
            this.sliderContent = sliderContent;
            parentRectTransform = this.sliderContent.GetComponent<RectTransform>();
        }

        public void RenderSliderElements(List<GameObject> sliderObject) // TODO add single element without a list + move delete responsibility outside
        {
            DeleteSliderElements();

            sliderLength = offset;

            for (int i = 0; i < sliderObject.Count; i++)
            {
                RectTransform elementRectTransform = sliderObject[i].GetComponent<RectTransform>();
                sliderObject[i].transform.localPosition = new Vector3(sliderLength + elementRectTransform.rect.width / 2, 0.0F, 0.0F);
                sliderElements.Add(sliderObject[i]);

                if(i < sliderObject.Count - 1) // Don't add spacing after last object
                {
                    sliderLength += spacing;
                }
                sliderLength += elementRectTransform.rect.width;
            }

            sliderLength += offset;
            parentRectTransform.sizeDelta = new Vector2(sliderLength, parentRectTransform.rect.height);
        }

        private void DeleteSliderElements()
        {
            foreach (GameObject gameObject in sliderElements)
            {
                GameObject.Destroy(gameObject);
            }
            sliderElements.Clear();
        }
    }
}
