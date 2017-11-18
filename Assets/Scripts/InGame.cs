using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour {
	public GameObject sampleCocktailPrefab;
	public GameObject ourCocktailPrefab;
	public Transform sampleCocktailTransform;
	public Transform ourCocktailTransform;
	string newSampleCocktailName = "NewSampleCocktail";
	string newOurCocktailName = "NewOurCocktail";

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			StartNewRound ();
		}
	}

	public void StartNewRound(){
		Destroy (GameObject.Find(newSampleCocktailName));
		GameObject newSampleCocktail = Instantiate (sampleCocktailPrefab, Vector3.zero, Quaternion.identity, sampleCocktailTransform);
		newSampleCocktail.transform.localPosition = Vector3.zero;
		newSampleCocktail.name = newSampleCocktailName;

		Destroy (GameObject.Find(newOurCocktailName));
		GameObject newOurCocktail = Instantiate (ourCocktailPrefab, Vector3.zero, Quaternion.identity, ourCocktailTransform);
		newOurCocktail.transform.localPosition = Vector3.zero;
		newOurCocktail.name = newOurCocktailName;
	}
}
