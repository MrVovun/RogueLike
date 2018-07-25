using UnityEngine;
using System.Collections;

public class JsonFileReader {

	public static string JsonLoader(string path){
		string jsonFilePath = path.Replace(".json", "");
		TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
		return loadedJsonFile.text;
	}

}
