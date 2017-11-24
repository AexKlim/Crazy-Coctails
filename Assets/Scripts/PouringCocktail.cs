using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringCocktail : MonoBehaviour {
	public GameObject liquidPrefab;
	public float pouringSpeed;
	public Ingredient[] ingredients;

	RandomCocktail currentSampleCocktail;
	int currentIngredientNumber = 0;
	GameObject liquid;
	int size;
	GameManager gameManger;
	float totalHeight;
	bool similarityCalculated;
	bool roundOver;

	void Start () {
		gameManger = FindObjectOfType<GameManager> ();
		currentSampleCocktail = FindObjectOfType<RandomCocktail> ();
		size = currentSampleCocktail.ingredients.Length;
		ingredients = new Ingredient[size];
		for (int i = 0; i < size; i++) {															
			ingredients [i] = new Ingredient ();
		}
		ingredients [0].startHeight = 0.04f;
	}
	void Update () {
		if (roundOver)
			return;
		if (currentIngredientNumber < size) {
			if (Input.GetMouseButtonDown (0)) {
				liquid = Instantiate (liquidPrefab, Vector3.zero, Quaternion.identity, transform);
				liquid.transform.localPosition = new Vector3 (0, ingredients [currentIngredientNumber].startHeight, 0);
				liquid.GetComponent<SpriteRenderer> ().color = currentSampleCocktail.ingredients [currentIngredientNumber].color;
				ingredients[currentIngredientNumber].color = currentSampleCocktail.ingredients [currentIngredientNumber].color;
			}
			if (liquid != null) {
				if (Input.GetMouseButton (0)) {
					liquid.transform.localScale += new Vector3 (0, Time.deltaTime * pouringSpeed, 0);
					ingredients [currentIngredientNumber].amount = liquid.transform.localScale.y;
					if (ingredients [currentIngredientNumber].amount * RandomCocktail.spriteMultiplier + ingredients [currentIngredientNumber].startHeight > 2f) {
						liquid.transform.localScale = new Vector3 (1f, (2f - ingredients [currentIngredientNumber].startHeight) / RandomCocktail.spriteMultiplier, 1f);
						ingredients [currentIngredientNumber].amount = liquid.transform.localScale.y;

						gameManger.StartCoroutine ("CalculateSimilarity");
						//gameManger.Invoke ("StartNewRound", 2f);
						similarityCalculated = true;
						roundOver = true;
					}	
				}
				if (Input.GetMouseButtonUp (0)) {
					currentIngredientNumber++;
					if (currentIngredientNumber < size) {
						ingredients [currentIngredientNumber].startHeight = ingredients [currentIngredientNumber - 1].startHeight + ingredients [currentIngredientNumber - 1].amount * RandomCocktail.spriteMultiplier;
					}
				}
			}
		} else {
			if (!similarityCalculated) {
				gameManger.StartCoroutine ("CalculateSimilarity");
				//gameManger.Invoke("StartNewRound", 2f);
				similarityCalculated = true;
				roundOver = true;
			}
		}
	}
}
