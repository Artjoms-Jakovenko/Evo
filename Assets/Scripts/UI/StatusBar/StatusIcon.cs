using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    private Image actionIcon;
    private BehaviourSystem behaviourSystem;

    private void Awake()
    {
        actionIcon = GetComponent<Image>();
        behaviourSystem = GetComponentInParent<BehaviourSystem>();
    }

    private void OnEnable()
    {
        behaviourSystem.OnActionChanged += SetStatusIcon;
    }

    private void OnDisable()
    {
        behaviourSystem.OnActionChanged -= SetStatusIcon;
    }

    public void SetStatusIcon(ActionEnum action)
    {
        actionIcon.sprite = Resources.Load<Sprite>(UiData.actionIconDescription[action].iconPath);
    }
}
