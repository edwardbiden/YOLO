using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {

	public GameObject instructions;
	public GameObject startMenu;

	public void Start () {
		instructions.SetActive(false);
		startMenu.SetActive(true);
	}

	public void ShowInstructions () {
		instructions.SetActive(true);
		startMenu.SetActive(false);
	}

	public void BackButton () {
		instructions.SetActive(false);
		startMenu.SetActive(true);
	}

	public void Startgame () {
		instructions.SetActive(false);
		startMenu.SetActive(false);
	}
}
