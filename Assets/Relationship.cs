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
	public float loveBonus;
	public float contentmentBonus;
	public bool invest;
	private float playerChemistry;
	public float partnerChemistry;
	public float chemistryMin;
	public float chemistryMax;

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
	
	public void Invest()
	{
		if ( countdown >= 0)
		{
			countdown --;
		}
		if ( countdown < 0 && button.GetComponent<Button>().IsInteractable() == false)
		{
			button.GetComponent<Button>().interactable = true;
			options.buttonActivate();
			career.buttonActive();
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
		countdown = display.jumpTime;
		options.buttonDisactivate();
		invest = true;
		career.buttonInactive();
		talk.lastEvent = "Invest";
		talk.lastEventCount = 2;
		talk.Speak();
		display.FastForward();
	}

	public void UpdateRelationship () {
		// calculate playerHappiness
		playerHappiness = Mood(display.aspectvalue, partner.aspectvalue, playerHappiness, playerChemistry);
		playerRating = Rating(playerHappiness, playerRating);
		Debug.Log ("player: " + playerHappiness);

		partnerHappiness = Mood(partner.aspectvalue, display.aspectvalue, partnerHappiness, partnerChemistry);
		partnerRating = Rating(partnerHappiness, partnerRating);
		Debug.Log ("partner: " + partnerHappiness);

		if ( playerHappiness >= babyThreshold && partnerHappiness >= babyThreshold && display.pregnant == false && display.canHaveBabies == true)
		{
			display.haveBabyButton.SetActive(true);
		}

		if ( playerHappiness >= (marriageThreshold - 20) && display.married == false )
		{
			display.getMarriedButton.SetActive(true);
		}
	}

	public float Mood( float[] myvalues, float[] yourvalues, float myHappiness, float myChemistry) 
	{
		delta = 0;
		for ( int a = 0; a < 3; a++ )
		{
			delta += myvalues[a] - yourvalues[a];	
		}
		// balance is way off
		Debug.Log ("myHappiness: " + myHappiness);
		myHappiness -= (delta / deltaDenominator);
		Debug.Log ("delta / denom: " + (-delta / deltaDenominator));

		// novelty effect
		if ( display.durationYears < 1 && display.durationMonths <=3 )
		{
			myHappiness += loveBonus;
			Debug.Log ("lovebonus: " + loveBonus);
		}
		if ( display.durationYears < 2 )
		{
			myHappiness += contentmentBonus;
			Debug.Log ("contentmentbonus: " + contentmentBonus);
		}

		// Chemistry - random modifier
		float chemChange = Random.Range (0,10f);
		if ( chemChange < 2 )
		{
			myChemistry = Random.Range( chemistryMin, chemistryMax );
		}
		myHappiness += myChemistry;
		Debug.Log ("myChemistry: " + myChemistry);
		Debug.Log ("myHappiness: " + myHappiness);

		if ( invest == true || partner.invest == 0)
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

	public void buttonActive()
	{
		button.GetComponent<Button>().interactable = true;
	}

	public void buttonInactive()
	{
		button.GetComponent<Button>().interactable = false;
	}
}
