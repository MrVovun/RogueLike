using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueMessage{
	public string dialogueMessage;
	public string branch;

	//for crutches use only
	public DialogueMessage(string pMsg,string pBranch)
	{
		dialogueMessage = pMsg;
		branch = pBranch;
	}
}