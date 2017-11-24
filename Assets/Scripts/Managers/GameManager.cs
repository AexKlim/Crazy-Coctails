using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Manager {
	public GameObject sampleCocktailPrefab;
	public GameObject pouringCocktailPrefab;
	public Transform sampleCocktailTransform;
	public Transform pouringCocktailTransform;
	public Text totalDifferenceText;
	public Text markText;
	public Text pointsText;
	public GameObject gameOverMenu;
	public GameObject gameInterface;
	public GameObject backgroundLightening;
	public Text totalScoreText;
	public Text maxScoreText;
	public int perfectMark;
	public int perfectPoints;
	public int greatMark;
	public int greatPoints;
	public int goodMark;
	public int goodPoints;
	string newSampleCocktailName = "NewSampleCocktail";
	string newPouringCocktailName = "NewOurCocktail";

	RandomCocktail sampleCocktail;
	PouringCocktail pouringCocktail;
	int points;
	int maxScore;

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
		if (intSum <= perfectMark) {
			markText.text = "PERFECT";
			points += perfectPoints;
		} else if (intSum <= greatMark) {
			markText.text = "GREAT";
			points += greatPoints;
		} else if (intSum <= goodMark) {
			markText.text = "GOOD";
			points += goodPoints;
		} else {
			markText.text = "BAD";
			GameOver ();
		}
		markText.enabled = true;
		pointsText.text = points.ToString();
		yield return new WaitForSeconds (1.5f);
		totalDifferenceText.enabled = false;
		markText.enabled = false;
		StartNewRound ();
	}

	public void GameOver(){
		Time.timeScale = 0;
		gameOverMenu.SetActive (true);
		gameInterface.SetActive (false);
		backgroundLightening.SetActive (true);
		if (points > maxScore) {
			maxScore = points;
			maxScoreText.text = maxScore.ToString ();
		}
		totalScoreText.text = points.ToString();
	}

	public void Restart(){
		Time.timeScale = 1f;
		points = 0;
		pointsText.text = points.ToString ();
		gameOverMenu.SetActive (false);
		gameInterface.SetActive (true);
		backgroundLightening.SetActive (false);
		StopAllCoroutines ();
		StartNewRound ();
		totalDifferenceText.enabled = false;
		markText.enabled = false;
	}
}
