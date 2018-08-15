using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanBeSelected : MonoBehaviour
{

    private bool _isSelected;

    public bool isSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            if (_isSelected)
            {
                HighlightSelected();
            }
            else
            {
                DarkLightSelected();
            }
        }
    }

    public DialogueMessage myDialogue;

    private void HighlightSelected()
    {
        this.GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }

    private void DarkLightSelected()
    {
        this.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

}
