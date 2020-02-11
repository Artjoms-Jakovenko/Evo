using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public enum LinearSliderOrientation
    {
        Horizontal,
        Vertical
    }

    public class LinearSlider // TODO make vertical too
    {
        public float offset = 0.0F;
        public float spacing = 0.0F;

        private GameObject sliderContent;
        private RectTransform sliderContentRectTransform;
        private LinearSliderOrientation linearSliderOrientation;


        private readonly List<GameObject> sliderElements = new List<GameObject>();
        private float sliderLength = 0.0F;

        public LinearSlider(GameObject sliderContent, LinearSliderOrientation linearSliderOrientation = LinearSliderOrientation.Horizontal)
        {
            this.sliderContent = sliderContent;
            sliderContentRectTransform = this.sliderContent.GetComponent<RectTransform>();
            this.linearSliderOrientation = linearSliderOrientation;
        }

        public void RenderSliderElements(List<GameObject> sliderObject) // TODO add single element without a list + move delete responsibility outside
        {
            DeleteSliderElements();

            sliderLength = offset;

            for (int i = 0; i < sliderObject.Count; i++)
            {
                RectTransform elementRectTransform = sliderObject[i].GetComponent<RectTransform>();

                if (linearSliderOrientation == LinearSliderOrientation.Horizontal)
                {
                    elementRectTransform.localPosition = new Vector3(sliderLength + elementRectTransform.rect.width / 2, 0.0F, 0.0F);
                    sliderLength += elementRectTransform.rect.width;
                }
                else if (linearSliderOrientation == LinearSliderOrientation.Vertical)
                {
                    elementRectTransform.localPosition = new Vector3(0.0F, -(sliderLength + elementRectTransform.rect.height / 2), 0.0F);
                    sliderLength += elementRectTransform.rect.height;
                }

                sliderElements.Add(sliderObject[i]);

                if(i < sliderObject.Count - 1) // Don't add spacing after last object
                {
                    sliderLength += spacing;
                }
            }

            sliderLength += offset;

            if (linearSliderOrientation == LinearSliderOrientation.Horizontal)
            {
                sliderContentRectTransform.sizeDelta = new Vector2(sliderLength, sliderContentRectTransform.rect.height);
            }
            else if (linearSliderOrientation == LinearSliderOrientation.Vertical)
            {
                sliderContentRectTransform.sizeDelta = new Vector2(sliderContentRectTransform.rect.width, sliderLength);
            }
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
