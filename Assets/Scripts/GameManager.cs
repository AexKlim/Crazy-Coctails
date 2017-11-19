using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject sampleCocktailPrefab;
	public GameObject pouringCocktailPrefab;
	public Transform sampleCocktailTransform;
	public Transform pouringCocktailTransform;
	public Text totalDifferenceText;
	public Text mark;
	public int perfectMark;
	public int greatMark;
	public int goodMark;
	string newSampleCocktailName = "NewSampleCocktail";
	string newPouringCocktailName = "NewOurCocktail";

	RandomCocktail sampleCocktail;
	PouringCocktail pouringCocktail;

	void Start () {
		StartNewRound ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			StartNewRound ();
		}
	}

	public void StartNewRound(){
		Destroy (GameObject.Find(newSampleCocktailName));
		GameObject sampleCocktailGO = Instantiate (sampleCocktailPrefab, Vector3.zero, Quaternion.identity, sampleCocktailTransform);
		sampleCocktailGO.transform.localPosition = Vector3.zero;
		sampleCocktailGO.name = newSampleCocktailName;
		sampleCocktail = sampleCocktailGO.GetComponent<RandomCocktail> ();
		Destroy (GameObject.Find(newPouringCocktailName));
		GameObject pouringCocktailGO = Instantiate (pouringCocktailPrefab, Vector3.zero, Quaternion.identity, pouringCocktailTransform);
		pouringCocktailGO.transform.localPosition = Vector3.zero;
		pouringCocktailGO.name = newPouringCocktailName;
		pouringCocktail = pouringCocktailGO.GetComponent<PouringCocktail> ();
	}

	public IEnumerator CalculateSimilarity(){
		float sum = 0;
		for (int i = 0; i < sampleCocktail.ingredients.Length; i++) {
			float difference = Mathf.Abs (sampleCocktail.ingredients [i].amount -  pouringCocktail.ingredients [i].amount) * 100;
			sum += difference;
		}
		int intSum = Mathf.RoundToInt (sum);
		totalDifferenceText.text = "TOTAL INGREDIENTS DIFFERENCE = " +intSum + " %";
		totalDifferenceText.enabled = true;
		if (intSum <= perfectMark)
			mark.text = "PERFECT";
		else if (intSum <= greatMark)
			mark.text = "GREAT";
		else if(intSum <= goodMark)
			mark.text = "GOOD";
		else 
			mark.text = "BAD";
		mark.enabled = true;

		yield return new WaitForSeconds (1.5f);
		totalDifferenceText.enabled = false;
		mark.enabled = false;
	}
}
