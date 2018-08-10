using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanBeSelected : MonoBehaviour {
	public bool isSelected;

	public void HighlightSelected()
	{
		if (isSelected == true)
		{
			Debug.Log (gameObject + "Ti pidor");
		}
	}
}
