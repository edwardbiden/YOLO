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
	private Talk talk;
	
	void Start () 
	{
		careerCoolDown = 0;
		countdown = 1;
		button.GetComponent<Button>().interactable = true;
		buttonImage.SetActive(false);
		options = canvas.GetComponent<Options>();
		display = canvas.GetComponent<Display>();
		talk = canvas.GetComponent<Talk>();
	}

	void Update ()
	{
		if ( display.age <= 18 )
		{
			buttonImage.SetActive(false);
			return;
		}
		else
		{
			buttonImage.SetActive(true);
		}

		if (button.GetComponent<Button>().IsInteractable() == false)
		{
			CareerFocus = true;
		}
		else 
		{
			CareerFocus = false;
		}

		if ( careerCoolDown > 0 )
		{
			buttonImage.SetActive(false);
		}
		else 
		{
			buttonImage.SetActive(true);
		}
	}

	void FixedUpdate()
	{
		if (button.GetComponent<Button>().IsInteractable() == false)
		{
			countdown -= Time.deltaTime;
		}
		if (countdown <= 0)
		{
			button.GetComponent<Button>().interactable = true;
			display.aspectvalue[4] += 1f;
			options.buttonActivate();
			if (display.colorName.a > 0.1)
			{
				CareerFocus = false;
				options.On();
			}
			if (display.inARelationship == true)
			{
				display.datingPanel.SetActive(true);
			}
			countdown = 2f;
		}
		if ( careerCoolDown > 0)
		{
			careerCoolDown -= Time.deltaTime;
		}
	}

	public void StartFocus () 
	{
		button.GetComponent<Button>().interactable = false;
		countdown = 2f;
		options.buttonDisactivate();
		talk.lastEvent = "Focus";
		talk.lastEventCount = 2;
		talk.Speak();
		display.FastForward();
	}
}
