using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Relationship : MonoBehaviour {

	public GameObject canvas;
	private Partner partner;
	private Display display;
	private Talk talk;
	private Career career;
	private Options options;
	public float relationshipStrength;
	public float playerHappiness;
	public float partnerHappiness;
	public Transform button;
	public GameObject buttonImage;

	public int playerRating;
	public int partnerRating;
	public float countdown;

	public string[] partnerStates = {" looks annoyed."," seems distant."," appears content."," seems very happy."," clearly adores you."};
	public int[] partnerThresholds = {0,20,40,60,80};

	public string[] states = {"You hate your partner. ","Things could be better. ","You get by ok. ","You are happy. ","You are in love. "};
	public int[] stateThresholds = {0,20,40,60,80};

	private float delta;
	public float deltaDenominator;
	public int relationshipMin;
	public int relationshipMax;
	public int loveBonus;
	public int contentmentBonus;
	public bool invest;

	public int babyThreshold;
	public int marriageThreshold;

	void Start () {
		partner = canvas.GetComponent<Partner>();
		display = canvas.GetComponent<Display>();
		options = canvas.GetComponent<Options>();
		talk = canvas.GetComponent<Talk>();
		career = canvas.GetComponent<Career>();
		relationshipStrength = 50;
		playerHappiness = 50;
		partnerHappiness = 50;
		buttonImage.SetActive(false);
		button.GetComponent<Button>().interactable = true;
		countdown = 0;
		invest = false;
	}
	
	void FixedUpdate()
	{
		if ( countdown > 0)
		{
			countdown -= Time.deltaTime;
		}
		if ( countdown <= 0 && button.GetComponent<Button>().IsInteractable() == false)
		{
			button.GetComponent<Button>().interactable = true;
			options.buttonActivate();
			invest = false;
		}
	}

	void Update()
	{
		if ( partnerHappiness <= 10 )
		{
			display.Dumped();
		}
	}

	public void StartInvest () 
	{
		button.GetComponent<Button>().interactable = false;
		career.careerCoolDown = 2f;
		countdown = 2f;
		options.buttonDisactivate();
		invest = true;
		talk.lastEvent = "Invest";
		talk.lastEventCount = 2;
		talk.Speak();
		display.FastForward();
	}

	public void UpdateRelationship () {
		// calculate playerHappiness
		playerHappiness = Mood(display.aspectvalue, partner.aspectvalue, playerHappiness);
		playerRating = Rating(playerHappiness, playerRating);

		partnerHappiness = Mood(partner.aspectvalue, display.aspectvalue, partnerHappiness);
		partnerRating = Rating(partnerHappiness, partnerRating);

		if ( playerHappiness >= babyThreshold && partnerHappiness >= babyThreshold && display.pregnant == false && display.canHaveBabies == true)
		{
			display.haveBabyButton.SetActive(true);
		}

		if ( playerHappiness >= (marriageThreshold - 20) && display.married == false )
		{
			display.getMarriedButton.SetActive(true);
		}
		Debug.Log("player happiness: " + playerHappiness + "; partner happiness: " + partnerHappiness);
	}

	public float Mood( float[] myvalues, float[] yourvalues, float myHappiness) 
	{
		delta = 0;
		for ( int a = 0; a < 4; a++ )
		{
			delta += myvalues[a] - yourvalues[a];	
		}
		// balance is way off
		myHappiness += Random.Range(relationshipMin - (delta / deltaDenominator), relationshipMax - (delta / deltaDenominator));

		// novelty effect
		if ( display.durationYears < 1 && display.durationMonths <=3 )
		{
			myHappiness += loveBonus;
		}
		if ( display.durationYears < 2 )
		{
			myHappiness += contentmentBonus;
		}

		if ( invest == true )
		{
			myHappiness += 1;
		}
		if ( myHappiness >= 100 )
		{
			myHappiness = 100;
		}
		return myHappiness;
	}

	public int Rating (float myHappiness, int myRating)
	{
		for ( int i = 0; i < stateThresholds.Length ; i++ )
		{
			if ( myHappiness >= stateThresholds[i] )
			{
				myRating = i;
			}
		}
		return myRating;
	}
}
