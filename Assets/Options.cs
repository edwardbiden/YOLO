using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options : MonoBehaviour {

	public GameObject panel;
	private Display display;
	private Career career;
	private Relationship relationship;
	private Obituary obituary;
	public GameObject canvas;
	public GameObject relationshipText;

	public Transform DateButton;
	public Transform HaveBaby;
	public Transform GetMarried;

	void Start()
	{
		display = canvas.GetComponent<Display>();
		career = canvas.GetComponent<Career>();
		relationship = relationshipText.GetComponent<Relationship>();
		obituary = canvas.GetComponent<Obituary>();
		panel.SetActive(false);
	}

	public void On()
	{
		if ( career.CareerFocus == false)
		{
			panel.SetActive(true);
		}
	}

	public void Off()
	{
		panel.SetActive(false);
	}

	public void Date() 	
	{
		Off();
		display.status = "You are dating";
		display.datingPanel.SetActive(true);
		display.relationshipCount++;
		display.colorName.a = 1f;
		display.inARelationship = true;
		display.durationMonths = 0;
		display.durationYears = 0;
		display.duration = 0;
		relationship.buttonImage.SetActive(true);
		obituary.previousChildren = display.childrenCount;
	}

	public void Reject()
	{
		display.partnerAlive = false;
		display.inARelationship = false;
		Off();
		display.status = "You are dead";
	}

	public void buttonDisactivate()
	{
		DateButton.GetComponent<Button>().interactable = false;
		HaveBaby.GetComponent<Button>().interactable = false;
		GetMarried.GetComponent<Button>().interactable = false;
	}

	public void buttonActivate()
	{
		DateButton.GetComponent<Button>().interactable = true;
		HaveBaby.GetComponent<Button>().interactable = true;
		GetMarried.GetComponent<Button>().interactable = true;
	}

}
