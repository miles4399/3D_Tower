using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] private Button _activateButton;

    public void TriggerButton()
    {
        _activateButton?.onClick.Invoke();
    }
}
