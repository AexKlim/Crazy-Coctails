using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCocktail : MonoBehaviour {
	public GameObject liquidPrefab;
	public int minIngredientsNumber;
	public int maxIngredientsNumber;
	public float minIngredientAmountPercent;
	public Ingredient[] ingredients;
	public static float spriteMultiplier;

	void Awake(){
		spriteMultiplier = liquidPrefab.GetComponent<SpriteRenderer> ().sprite.texture.height / liquidPrefab.GetComponent<SpriteRenderer> ().sprite.pixelsPerUnit;
		int size = Random.Range (minIngredientsNumber, maxIngredientsNumber + 1);

		ingredients = new Ingredient[size];
		for (int i = 0; i < size; i++) {															
			ingredients [i] = new Ingredient ();
		}
		float sum = 0;
		for (int i = 0; i < size; i++) {
			ingredients [i].amount = Random.Range(minIngredientAmountPercent, 1f);
			sum += ingredients [i].amount;
		}
		for (int i = 0; i < size; i++) {
			ingredients [i].amount /= sum;
		}

		float startHeight = 0;
		for (int i = 0; i < size; i++) {
			ingredients [i].startHeight = startHeight * spriteMultiplier + 0.04f;
			ingredients [i].color = RandomColor ();
			GameObject liquid = Instantiate (liquidPrefab, Vector3.zero, Quaternion.identity, transform);
			liquid.transform.localPosition = new Vector3 (0, ingredients[i].startHeight, 0);
			liquid.transform.localScale =  new Vector3 (1, ingredients[i].amount, 0.5f);
			liquid.GetComponent<SpriteRenderer> ().color = ingredients[i].color;
			startHeight += ingredients [i].amount;
		}
	}

	Color RandomColor(){
		float red = 0, green = 0, blue = 0;
		int a = Random.Range (0, 3);
		switch (a) {
		case 0:
			red = Random.value;
			green = Random.Range(0.5f, 1f);
			blue = Random.value;
			break;
		case 1:
			red = Random.value;
			green = Random.value;
			blue = Random.Range(0.5f, 1f);
			break;
		case 2:
			red = Random.Range(0.5f, 1f);
			green = Random.value;
			blue = Random.value;
			break;
		}
		Color col = new Color(red, green, blue, 1f);
		return col;
	}
}
