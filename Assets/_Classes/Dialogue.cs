using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
	public string speaker;
	public List<string> question = new List<string>();
	public List<string> answer = new List<string>();
}
