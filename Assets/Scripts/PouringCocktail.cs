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

	void Start () {
		currentSampleCocktail = GameObject.FindObjectOfType<RandomCocktail> ();
		size = currentSampleCocktail.ingredients.Length;
		ingredients = new Ingredient[size];
		for (int i = 0; i < size; i++) {															
			ingredients [i] = new Ingredient ();
		}
		ingredients [0].startHeight = 0.04f;
	}
	void Update () {
		if (currentIngredientNumber < size) {
			if (Input.GetMouseButtonDown (0)) {
				liquid = Instantiate (liquidPrefab, Vector3.zero, Quaternion.identity, transform);
				liquid.transform.localPosition = new Vector3 (0, ingredients [currentIngredientNumber].startHeight, 0);
				liquid.GetComponent<SpriteRenderer> ().color = currentSampleCocktail.ingredients [currentIngredientNumber].color;
				ingredients [currentIngredientNumber].color = currentSampleCocktail.ingredients [currentIngredientNumber].color;
			}
			if (Input.GetMouseButton (0)) {
				liquid.transform.localScale += new Vector3 (0, Time.deltaTime * pouringSpeed, 0);
			}
			if (Input.GetMouseButtonUp (0)) {
				ingredients [currentIngredientNumber].amount = liquid.transform.localScale.y;
				currentIngredientNumber++;
				if (currentIngredientNumber < size) {
					ingredients [currentIngredientNumber].startHeight = ingredients [currentIngredientNumber - 1].startHeight + ingredients [currentIngredientNumber - 1].amount * RandomCocktail.spriteMultiplier;
				}
			}
		} else {
			GameObject.Find ("GameManager").GetComponent<InGame> ().StartNewRound ();
		}
	}
}
