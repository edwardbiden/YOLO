using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Career : MonoBehaviour {

	public Transform button;
	public GameObject buttonImage;
	public float countdown;
	public bool CareerFocus;
	public float careerCoolDown;

	public GameObject canvas;
	private Options options;
	private Display display;
	private Relationship relationship;
	public GameObject relationshipText;
	private Talk talk;
	
	void Start () 
	{
		careerCoolDown = 0;
		countdown = 0;
		button.GetComponent<Button>().interactable = true;
		buttonImage.SetActive(false);
		options = canvas.GetComponent<Options>();
		display = canvas.GetComponent<Display>();
		relationship = relationshipText.GetComponent<Relationship>();
		talk = canvas.GetComponent<Talk>();
		CareerFocus = false;
	}

	void Update ()
	{
		if ( display.age >= 18 && display.age <= 65)
		{	
			buttonImage.SetActive(true);
		}
		else
		{
			buttonImage.SetActive(false);
			return;
		}

		if ( careerCoolDown > 0 )
		{
			button.GetComponent<Button>().interactable = false;
		}
		else 
		{
			if ( CareerFocus == false && relationship.invest == false)
			{
				button.GetComponent<Button>().interactable = true;
			}
		}
	}

	public void Focus()
	{
		if (countdown >= 0)
		{
			display.aspectvalue[4] += 0.5f;
			button.GetComponent<Button>().interactable = false;
			relationship.buttonInactive();
			countdown--;
		}
		else
		{
			button.GetComponent<Button>().interactable = true;
			options.buttonActivate();
			CareerFocus = false;
			relationship.buttonActive();
			if (display.colorName.a > 0.1)
			{
				options.On();
			}
			if (display.inARelationship == true)
			{
				display.datingPanel.SetActive(true);
			}
		}
	}

	public void StartFocus () 
	{
		button.GetComponent<Button>().interactable = false;
		relationship.buttonInactive();
		countdown = display.jumpTime;
		options.buttonDisactivate();
		CareerFocus = true;
		talk.lastEvent = "Focus";
		talk.lastEventCount = 2;
		talk.Speak();
		display.FastForward();
	}

	public void buttonActive ()
	{
		button.GetComponent<Button>().interactable = true;
	}

	public void buttonInactive ()
	{
		button.GetComponent<Button>().interactable = false;
	}	
}
