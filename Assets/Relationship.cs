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
	public float playerChemistry;
	public float partnerChemistry;
	public float chemistryMin;
	public float chemistryMax;

	public int babyThreshold;
	public int marriageThreshold;
	public float investEffectPlayer;
	public float investEffectPartner;
	public float investmentEffect;
	public float investmentDecay;
	public float happinessGap;

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
		investEffectPlayer = 0;
		investmentEffect = 0.25f;
		investmentDecay = 18f;
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
		career.buttonInactive();
		talk.lastEvent = "Invest";
		investEffectPlayer += investmentEffect;
		talk.lastEventCount = 2;
		talk.Speak();
		display.FastForward();
	}

	public void UpdateRelationship () {
		// calculate playerHappiness
		playerHappiness = Mood(display.aspectvalue, partner.aspectvalue, playerHappiness, playerChemistry, investEffectPlayer, investEffectPartner);
		partnerHappiness = Mood(partner.aspectvalue, display.aspectvalue, partnerHappiness, partnerChemistry, investEffectPartner, investEffectPlayer);

		// lower happiness drags you down
		happinessGap = (Mathf.Max (partnerHappiness, playerHappiness) - Mathf.Min (partnerHappiness, playerHappiness)) / 10;
		if (playerHappiness > partnerHappiness)
		{
			playerHappiness -= happinessGap;
		}
		if (partnerHappiness > playerHappiness)
		{
			partnerHappiness -= happinessGap;
		}

		playerRating = Rating(playerHappiness, playerRating);
		partnerRating = Rating(partnerHappiness, partnerRating);

		investEffectPartner -= investEffectPartner / investmentDecay;
		investEffectPlayer -= investEffectPlayer / investmentDecay;

		if ( playerHappiness >= babyThreshold && partnerHappiness >= babyThreshold && display.pregnant == false && display.canHaveBabies == true)
		{
			display.haveBabyButton.SetActive(true);
		}

		if ( playerHappiness >= (marriageThreshold - 20) && display.married == false )
		{
			display.getMarriedButton.SetActive(true);
		}
	}

	public float Mood( float[] myvalues, float[] yourvalues, float myHappiness, float myChemistry, float myInvestment, float yourInvestment) 
	{
		delta = 0;
		for ( int a = 0; a < 3; a++ )
		{
			delta += myvalues[a] - yourvalues[a];	
		}
		// delta is difference in attributes, deltaDenominator is "allowed difference"
		myHappiness -= (delta / deltaDenominator);

		// novelty effect
		if ( display.durationYears < 1 && display.durationMonths <=3 )
		{
			myHappiness += loveBonus;
		}
		if ( display.durationYears < 2 )
		{
			myHappiness += contentmentBonus;
		}

		// Chemistry - random modifier
		float chemChange = Random.Range (0,10f);
		if ( chemChange < 2 )
		{
			myChemistry = Random.Range( chemistryMin, chemistryMax );
		}
		myHappiness += myChemistry;

		// relationship investment
		if ( myInvestment <= 0) 
		{
			myInvestment = 0;
		}
		myHappiness += myInvestment + yourInvestment;

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
