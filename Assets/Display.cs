﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// counter for months of happiness in different relationships in death summary
// each aspect has own qualitative descriptors
// partner has own death time
// choice of how to date (internet dating / bars / etc.)
// career or something rather than wealth??
// accidental pregnancy

public class Display : MonoBehaviour {

	public int fastforward;
	public bool quickTime;
	public int age;
	private int startAge = 16;
	public int deathYear;
	public int deathMonth;
	public int jumpTime;

	public float time;
	public int count;
	public bool alive;
	public bool inARelationship;
	public int relationshipCount;

	public int childrenCount;
	public int marriageCount;
	public int divorceCount;
	public int pregnancyCount;
	public bool pregnant;

	public bool partnerAlive = false;
	public bool married = false;
	public string status;
	public bool playerIsMale;

	public Text ageText;
	public Text monthText;
	public Text yearText;
	public Text statusText;
	public Text nameText;
	public Text partnerAgeText;

	public Text childrenText;
	public Text durationText;
	public int durationMonths;
	public int durationYears;
	public int duration;
	private int fastForwardCount;

	public Text hotnessText;
	public Text intelligenceText;
	public Text kindnessText;
	public Text wealthText;
	public Text careerText;
	public Image careerProgress;

	private int monthCount;
	public int birthMonth;
	public int birthYear;
	private int year;

	private string[] months = {"January","Febrary","March","April","May","June","July","August","September","October","November","December"};
	private string[] aspectRate = {"Poor","Average","Good","Excellent"};
	public float[] aspectvalue = {0,0,0,0,0};
	public float[] maxaspectvalue = {0,0,0,0,0};
	public string[] aspectText = {"","","","",""};
	public string[] wealthRate = {"$","$$","$$$","$$$$","$$$$$"};
	
	public string[] careerStates = {"amateur","novice","apprentice","journeyman","professional","expert","guru"};
	public int[] careerThresholds = {20,40,60,80,100,120};
	public int careerFillMax;
	public int careerFillMin;
	public int careerStep;

	private Partner partner;
	private Options options;
	private Relationship relationship;
	private Talk talk;
	private Career career;
	private Obituary obituary;

	public GameObject canvas;
	public GameObject relationshipText;
	public GameObject datingPanel;
	public GameObject haveBabyButton;
	public GameObject getMarriedButton;
	public GameObject deathPanel;
	public GameObject birthPanel;
	public Button startButton;
	private int monthsSinceBirth;
	public bool canHaveBabies;
	public bool startTime;
	public float maxHappiness;

	public InputField inputName;
	public InputField inputMonth;
	public InputField inputYear;
	public Toggle maleToggle;

	public int matchChance = 50;
	public	Color colorName;
	
	public void Start () 
	{
		startTime = false;
		quickTime = false;
		startButton.interactable = false;
		maleToggle.isOn = true;
		playerIsMale = true;
		birthPanel.SetActive(true);
		datingPanel.SetActive(false);
		deathPanel.SetActive(false);
		deathYear = 40 + Random.Range(0, 60);
		deathMonth = Random.Range(0, 12);
		age = startAge;
		time = 0f;
		monthsSinceBirth = 0;
		alive = true;
		married = false;
		divorceCount = 0;
		monthCount = 2;
		status = "You are single";
		fastforward = 1;
		partner = canvas.GetComponent<Partner>();
		options = canvas.GetComponent<Options>();
		relationship = relationshipText.GetComponent<Relationship>();
		talk = canvas.GetComponent<Talk>();
		obituary = canvas.GetComponent<Obituary>();
		career = canvas.GetComponent<Career>();
		colorName = partner.nameText.color;
		colorName.a = 0;
		relationshipCount = 0;
		marriageCount = 0;
		childrenCount = 0;
		pregnant = false;
		birthMonth = 0;
		birthYear = 0;
		durationMonths = 0;
		durationYears = 0;
		fastForwardCount = 0;
		partner.exName = "";
		partner.exDuration = 0;
		maxHappiness = 50;
		jumpTime = 5;
	}
	

	void FixedUpdate()
	{
		if ( age == deathYear && deathMonth == monthCount)
		{
			if (alive == true)
			{
				Death();
				alive = false;
			}
		}	
		
		else
		{
			if ( startTime == true )
			{
				Tick();
				time += Time.deltaTime * fastforward;	
			}
		}
	}

	void Update () 
	{
		if ( partnerAlive == false )
		{
			colorName.a = 0f;
		}
		partner.nameText.color = colorName;
		partner.hotnessText.color = colorName;
		partner.intelligenceText.color = colorName;
		partner.kindnessText.color = colorName;
		partner.wealthText.color = colorName;
		partnerAgeText.color = colorName;
		partner.careerText.color = colorName;
		childrenText.text = "You have " + childrenCount + " children";
		statusText.text = status;

		if ( durationYears < 1 )
		{
			if ( married == true ) 
			{
				durationText.text = "Married for " + durationMonths + " months";
			}
			if ( inARelationship == false )
			{
				durationText.text = "Single for " + durationMonths + " months";
			}
			if ( inARelationship == true && married == false ) {
				durationText.text = "Dating for " + durationMonths + " months";	
			}	
		}
		else
		{
			if ( married == true ) 
			{
				durationText.text = "Married for " + durationYears + " years";
			}
			if ( inARelationship == false )
			{
				durationText.text = "Single for " + durationYears + " years";
			}
			if ( inARelationship == true && married == false ) {
				durationText.text = "Dating for " + durationYears + " years";	
			}	
		}
		
		TextUpdate( aspectvalue, aspectText, aspectRate);
		TextUpdate( partner.aspectvalue, partner.aspectText, partner.aspectRate);

		// career fill bar
		careerProgress.fillAmount = (aspectvalue[4] - careerFillMin) / (careerFillMax - careerFillMin);
		if ( aspectvalue[4] >= careerFillMax  && careerStep < (careerThresholds.Length - 1) )
		{
			careerFillMin = careerThresholds[careerStep];
			careerFillMax = careerThresholds[careerStep + 1];
			careerStep++;
			careerProgress.fillAmount = 0;
		}

		if ( birthPanel )
		{
			if ( inputName.text.Length > 0 && inputYear.text.Length > 3 && inputMonth.text.Length > 0)
			{
				nameText.text = inputName.text;
				birthYear = int.Parse(inputYear.text);
				birthMonth = int.Parse(inputMonth.text) - 1;
				if ( int.Parse(inputYear.text) > 0 && int.Parse(inputMonth.text) > 0 && int.Parse(inputMonth.text) <= 12 )
				{
					startButton.interactable = true;
				}
			}
			else {
				startButton.interactable = false;
			}
		}

		// update texts last
		hotnessText.text = aspectText[0];
		intelligenceText.text = aspectText[1];
		kindnessText.text = aspectText[2];
		wealthText.text = aspectText[3];
		careerText.text = aspectText[4];
		partner.hotnessText.text = partner.aspectText[0];
		partner.intelligenceText.text = partner.aspectText[1];
		partner.kindnessText.text = partner.aspectText[2];
		partner.wealthText.text = partner.aspectText[3];
		partner.careerText.text = partner.aspectText[4];
	}

	void TextUpdate (float[] values, string[] texts, string[] ratings)
	{
		for (int i = 0; i < 3; i++)
		{
			if ( values[i] < 4 )
			{
				texts[i] = ratings[0];
			}
			if ( values[i] < 7 && values[i] >= 4 )
			{
				texts[i] = ratings[1];
			}
			if ( values[i] < 9 && values[i] >= 7)
			{
				texts[i] = ratings[2];
			}
			if ( values[i] >= 9 )
			{
				texts[i] = ratings[3];
			}	
		}

		if ( values[3] < 3 )
		{
			texts[3] = wealthRate[0];
		}
		if ( values[3] < 4 && values[3] >= 3 )
		{
			texts[3] = wealthRate[1];
		}
		if ( values[3] < 6 && values[3] >= 4 )
		{
			texts[3] = wealthRate[2];
		}
		if ( values[3] < 8 && values[3] >= 6 )
		{
			texts[3] = wealthRate[3];
		}	
		if ( values[3] >= 8 )
		{
			texts[3] = wealthRate[4];
		}

		if ( values[4] < careerThresholds[0] )
		{
			texts[4] = careerStates[0];
		}
		if ( values[4] < careerThresholds[1] && values[4] >= careerThresholds[0] )
		{
			texts[4] = careerStates[1];
		}
		if ( values[4] < careerThresholds[2] && values[4] >= careerThresholds[1] )
		{
			texts[4] = careerStates[2];
		}
		if ( values[4] < careerThresholds[3] && values[4] >= careerThresholds[2] )
		{
			texts[4] = careerStates[3];
		}	
		if ( values[4] < careerThresholds[4] && values[4] >= careerThresholds[3] )
		{
			texts[4] = careerStates[4];
		}
		if ( values[4] < careerThresholds[5] && values[4] >= careerThresholds[4] )
		{
			texts[4] = careerStates[5];
		}
		if ( values[4] >= careerThresholds[5] )
		{
			texts[4] = careerStates[6];
		}
	}
	
	
	void Tick()
	{
	if (time > 1f)
		{
			time = 0f;
			if (monthCount < 11)
			{
				monthCount++;	
			}
			else
			{
				monthCount = 0;
			}

			if ( inARelationship == false )
			{
				colorName.a -= 0.1f;
			}

			if ( monthCount == birthMonth )
			{
				age++;
			}
			partner.partnerMonth++;
			if ( partner.partnerMonth == 12 )
			{
				partner.partnerMonth = 0;
				partner.partnerAge++;
			}

			talk.lastEventCount++;
			ageText.text = age.ToString("#");
			partnerAgeText.text = partner.partnerAge.ToString("#");
			monthText.text = months[monthCount];
			year = age + birthYear;
			yearText.text = year.ToString();
			
			partner.StatsUpdate( aspectvalue, maxaspectvalue, age , playerIsMale, childrenCount, divorceCount, true);
			partner.StatsUpdate( partner.aspectvalue, partner.maxaspectvalue, partner.partnerAge, !playerIsMale, partner.partnerChildren, partner.partnerDivorces, false);
			
			int r = Random.Range(0,100);
			if ( partnerAlive == false && r <= matchChance && monthsSinceBirth >= 3 && career.CareerFocus == false )
			{
				partnerAlive = true;
				colorName.a = 1f;
				Match();
			}

			if ( partnerAlive == true )
			{
				if ( colorName.a <= 0f )
				{
					BreakUp();
				}
			}

			if ( inARelationship == true )
			{
				relationship.UpdateRelationship();	
			}

			if ( durationMonths < 11 )
			{
				durationMonths++;	
			}
			if ( durationMonths >= 11 )
			{
				durationMonths = 0;
				durationYears++;
			}
			duration++;

			if ( pregnant == true ) {
				pregnancyCount--;
				if ( pregnancyCount == 0 )
				{
					childrenCount++;
					career.careerCoolDown = 6f;
					partner.partnerChildren++;
					talk.lastEvent = "hadchild";
				}
				if ( pregnancyCount <= -3 )
				{
					pregnant = false;
					pregnancyCount = 9;
				}
			}

			if ( relationship.playerHappiness > maxHappiness )
			{
				maxHappiness = relationship.playerHappiness;
			}

			monthsSinceBirth++;
			CanHaveBabies();
			talk.TextUpdate();
			talk.Speak();

			if (career.careerCoolDown >=0 )
			{
				career.careerCoolDown--;
			}
			if (career.CareerFocus == true)
			{
				career.Focus();
			}
			if (relationship.invest == true)
			{
				relationship.Invest();
			}

			if (quickTime == true) {
				if (fastForwardCount > 0) {
					fastForwardCount--;
				}
				else 
				{
					FastForward();
				}
			}
		}
	}

	void CanHaveBabies()
	{
		if ( playerIsMale == true && partner.partnerAge >= 40)
			{
				canHaveBabies = false;
				haveBabyButton.SetActive(false);
				return;	
			}
			if ( playerIsMale == false && age >= 40 )
			{
				canHaveBabies = false;
				haveBabyButton.SetActive(false);
				return;
			}
			else 
			{
				canHaveBabies = true;
			}
	}

	public void Birth() 
	{
		for (int i = 0; i < 5; i++)
		{
			aspectvalue[i] = Random.Range(1, 11);
			maxaspectvalue[i] = 0;
		}

		aspectvalue[3] = Random.Range(1,4);
		aspectvalue[4] = 0;
		careerFillMax = careerThresholds[0];
		careerFillMin = 0;
		careerStep = 0;

		if ( maleToggle.isOn == true ) {
			playerIsMale = true;
		}
		else
		{
			playerIsMale = false;
		}
		birthPanel.SetActive(false);
		startTime = true;
		for ( int i = 0; i < 5; i++ )
		{
			partner.exvalue[i] = 0;
		}
		talk.Start();
	}

	void Match()
	{
		partner.Birth();
		options.On();
		relationship.playerHappiness = 50;
		relationship.partnerHappiness = 50;
		talk.lastEvent = "matched";
		talk.lastEventCount = 0;
		talk.ResetTalk();
		talk.Speak();
	}

	public void BreakUp()
	{
		// obituary
		if ( relationshipCount == 1)
		{
			obituary.soNames[0] = partner.partnerName;
			obituary.soLength[0] = duration;
			obituary.marriedSO[0] = married;
			obituary.soChildren[0] = childrenCount - obituary.previousChildren;
			Debug.Log( "breakUp1; Name: " + obituary.soNames[0] + ". Length: " + obituary.soLength[0] + ". Married: " + obituary.marriedSO[0] + ". Children: " + obituary.soChildren[0]);
		}
		if ( relationshipCount >= 2 && obituary.soLength[2] > obituary.soLength[1])
		{
			obituary.soNames[1] = obituary.soNames[2];
			obituary.soLength[1] = obituary.soLength[2];
			obituary.marriedSO[1] = obituary.marriedSO[2];
			obituary.soChildren[1] = childrenCount - obituary.previousChildren;
			Debug.Log( "breakUp2; Name: " + obituary.soNames[1] + ". Length: " + obituary.soLength[1] + ". Married: " + obituary.marriedSO[1] + ". Children: " + obituary.soChildren[1]);
		}
		if ( relationshipCount >= 2 )
		{
			obituary.soNames[2] = partner.partnerName;
			obituary.soLength[2] = duration;
			obituary.marriedSO[2] = married;
			obituary.soChildren[2] = childrenCount - obituary.previousChildren;
			Debug.Log( "breakUp3; Name: " + obituary.soNames[2] + ". Length: " + obituary.soLength[2] + ". Married: " + obituary.marriedSO[2] + ". Children: " + obituary.soChildren[2]);
		}


		if (maxHappiness > partner.exHappiness )
		{
			partner.CreateEx();
		}
		partnerAlive = false;
		options.Off();
		datingPanel.SetActive(false);
		status = "You are single";
		durationMonths = 0;
		durationYears = 0;
		duration = 0;
		if ( married == true )
		{
			talk.lastEvent = "divorce";
			divorceCount++;
		}
		if ( colorName.a <= 0 )
		{
			talk.lastEvent = "passed";
		}
		else 
		{
			talk.lastEvent = "breakup";
		}
		married = false;
		relationship.buttonImage.SetActive(false);
		relationship.partnerHappiness = 50;
		inARelationship = false;
		talk.lastEventCount = 0;
		talk.Speak();
		maxHappiness = 50;
	}

	public void Dumped()
	{
		// obituary
		if ( relationshipCount == 1)
		{
			obituary.soNames[0] = partner.partnerName;
			obituary.soLength[0] = duration;
			obituary.marriedSO[0] = married;
			obituary.soChildren[0] = childrenCount - obituary.previousChildren;
			Debug.Log( "Dumped1; Name: " + obituary.soNames[0] + ". Length: " + obituary.soLength[0] + ". Married: " + obituary.marriedSO[0] + ". Children: " + obituary.soChildren[0]);
		}
		if ( relationshipCount >= 2 && obituary.soLength[2] > obituary.soLength[1])
		{
			obituary.soNames[1] = obituary.soNames[2];
			obituary.soLength[1] = obituary.soLength[2];
			obituary.marriedSO[1] = obituary.marriedSO[2];
			obituary.soChildren[1] = childrenCount - obituary.previousChildren;
			Debug.Log( "Dumped2; Name: " + obituary.soNames[1] + ". Length: " + obituary.soLength[1] + ". Married: " + obituary.marriedSO[1] + ". Children: " + obituary.soChildren[1]);
		}
		if ( relationshipCount >= 2 )
		{
			obituary.soNames[2] = partner.partnerName;
			obituary.soLength[2] = duration;
			obituary.marriedSO[2] = married;
			obituary.soChildren[2] = childrenCount - obituary.previousChildren;
			Debug.Log( "Dumped3; Name: " + obituary.soNames[1] + ". Length: " + obituary.soLength[1] + ". Married: " + obituary.marriedSO[1] + ". Children: " + obituary.soChildren[1]);
		}

		if (partner.exDuration <= duration )
		{
			partner.CreateEx();
		}
		partnerAlive = false;
		options.Off();
		datingPanel.SetActive(false);
		status = "You are single";
		durationMonths = 0;
		durationYears = 0;
		duration = 0;
		if ( married == true )
		{
			talk.lastEvent = "divorced";
		}
		else 
		{
			talk.lastEvent = "dumped";	
		}
		married = false;
		relationship.partnerHappiness = 50;
		relationship.buttonImage.SetActive(false);
		inARelationship = false;
		talk.lastEventCount = 0;
		talk.Speak();
	}

	public void GenderToggle()
	{
		if ( playerIsMale == true )
		{
			playerIsMale = false;
		}
		else 
		{
			playerIsMale = true;
		}
	}

	void Death() 
	{
		if ( relationshipCount == 1)
		{
			obituary.soNames[0] = partner.partnerName;
			obituary.soLength[0] = duration;
			obituary.marriedSO[0] = married;
			obituary.soChildren[0] = childrenCount - obituary.previousChildren;
			Debug.Log( "Death1; Name: " + obituary.soNames[0] + ". Length: " + obituary.soLength[0] + ". Married: " + obituary.marriedSO[0] + ". Children: " + obituary.soChildren[0]);
		}
		if ( relationshipCount >= 2 && obituary.soLength[2] > obituary.soLength[1])
		{
			obituary.soNames[1] = obituary.soNames[2];
			obituary.soLength[1] = obituary.soLength[2];
			obituary.marriedSO[1] = obituary.marriedSO[2];
			obituary.soChildren[1] = childrenCount - obituary.previousChildren;
			Debug.Log( "Death2; Name: " + obituary.soNames[1] + ". Length: " + obituary.soLength[1] + ". Married: " + obituary.marriedSO[1] + ". Children: " + obituary.soChildren[1]);
		}
		if ( relationshipCount >= 2 )
		{
			obituary.soNames[2] = partner.partnerName;
			obituary.soLength[2] = duration;
			obituary.marriedSO[2] = married;
			obituary.soChildren[2] = childrenCount - obituary.previousChildren;
			Debug.Log( "Death3; Name: " + obituary.soNames[2] + ". Length: " + obituary.soLength[2] + ". Married: " + obituary.marriedSO[2] + ". Children: " + obituary.soChildren[2]);
		}

		status = "You are dead";
		statusText.text = status;
		deathPanel.SetActive(true);
		obituary.Write ();
		BreakUp();
	}

	public void HaveBaby()
	{
		pregnancyCount = 9;
		pregnant = true;
		haveBabyButton.SetActive(false);
	}

	public void GetMarried()
	{
		if ( relationship.partnerHappiness >= relationship.marriageThreshold )
		{
			relationship.playerHappiness += 10;
			relationship.partnerHappiness += 10;
			married = true;
			marriageCount++;
			getMarriedButton.SetActive(false);
			durationMonths = 0;
			durationYears = 0;
			status = "You are married";
			talk.lastEvent = "married";
			talk.lastEventCount = 0;
			talk.Speak();
			career.careerCoolDown = 3f;
			obituary.Relationship();
		}
		else
		{
			relationship.playerHappiness -= 10;
			relationship.partnerHappiness -= 10;
			getMarriedButton.SetActive(false);
			talk.lastEvent = "spurned";
			talk.lastEventCount = 0;
			talk.Speak();
		}
	}

	public void FastForward()
	{
		if ( quickTime == false )
		{
			quickTime = true;
			fastforward = jumpTime;
			fastForwardCount = 5;
		}
		else
		{
			quickTime = false;
			fastforward = 1;
		}
	}
}
