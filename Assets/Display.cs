using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Display : MonoBehaviour {

	public int fastforward;
	public bool quickTime;
	public int age;
	private int startAge;
	public int deathYear;
	public int deathMonth;
	public int jumpTime;
	public bool impendingDeath;
	public Transform pausebutton;
	public Transform playbutton;
	public Transform fastbutton;
	public Text InvestIn;
	private bool gowing;
	private bool glowUp;

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
	private int nameCount;
	private string playerName;
	public InputField inputNameText;
	public InputField monthInput;
	public InputField yearInput;

	public Text childrenText;
	public Text durationText;
	public int durationMonths;
	public int durationYears;
	public int duration;
	private int fastForwardCount;

	public Text hotnessText;
	public Text personalityText;
	public Text wealthText;
	public Text careerText;
	public Image careerProgress;
	public Image careerProgressPartner;

	private int monthCount;
	public int birthMonth;
	public int birthYear;
	private int year;
	public int infertileAge;

	private string[] months = {"January","February","March","April","May","June","July","August","September","October","November","December"};
	public float[] aspectvalue = {0,0,0,0};
	public float[] maxaspectvalue = {0,0,0,0};
	public string[] aspectText = {"","","",""};

	private int careerFillMax;
	private int careerFillMin;
	private int careerStep;
	public int careerFillMaxPartner;
	public int careerFillMinPartner;
	public int careerStepPartner;

	private Partner partner;
	private Options options;
	private Relationship relationship;
	private Talk talk;
	private Career career;
	private Obituary obituary;
	private TextUpdate textupdate;

	public GameObject canvas;
	public GameObject datingPanel;
	public GameObject haveBabyButton;
	public GameObject getMarriedButton;
	public GameObject deathPanel;
	public GameObject birthPanel;
	public Button startButton;
	public int monthsSinceBirth;
	public bool canHaveBabies;
	public bool startTime;
	public float maxHappiness;

	public InputField inputName;
	public InputField inputMonth;
	public InputField inputYear;
	public Toggle maleToggle;

	public int matchChance = 50;
	public	Color colorName;
	public Image PartnerCareer;
	public Image PartnerFill;
	public Image Deathfade;
	public Image Birthfade;
	public Image Glowfade;
	private float FadeInCycle;
	private bool FadeIn;

	private int deathfadeCount;

	private Color tempColor;
	private Color deathColor;
	private Color birthColor;
	private Color glowColor;
	public float birthTime;
	private bool fastTimeOn;
	private bool glowing;
	
	public void Start () 
	{
		pausebutton.GetComponent<Button>().interactable = false;
		playbutton.GetComponent<Button>().interactable = true;
		fastbutton.GetComponent<Button>().interactable = true;
		infertileAge = Random.Range (37,45);
		glowing = true;
		glowUp = true;
		glowColor = Glowfade.color;
		glowColor.a = 0;
		FadeInCycle = 0;
		FadeIn = false;
		startTime = false;
		quickTime = false;
		startButton.interactable = false;
		maleToggle.isOn = true;
		playerIsMale = true;
		birthPanel.SetActive(true);
		deathfadeCount = 0;
		datingPanel.SetActive(false);
		deathPanel.SetActive(false);
		deathYear = 50 + Random.Range(0, 20) + Random.Range(0, 20) + Random.Range(0, 20);
		deathMonth = Random.Range(1, 12);
		startAge = 16;
		age = startAge;
		time = 0f;
		monthsSinceBirth = 0;
		alive = true;
		married = false;
		divorceCount = 0;
		monthCount = 2;
		status = "You are single";
		fastforward = 0;
		partner = canvas.GetComponent<Partner>();
		options = canvas.GetComponent<Options>();
		relationship = canvas.GetComponent<Relationship>();
		textupdate = canvas.GetComponent<TextUpdate>();
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
		fastTimeOn = false;
		partner.exName = "";
		partner.exDuration = 0;
		maxHappiness = 50;
		jumpTime = 5;
		deathColor = Deathfade.color;
		birthColor = Birthfade.color;
		impendingDeath = false;
		relationship.playerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
		relationship.playernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;

		for ( int i = 0; i < 3; i++ )
		{
			obituary.soNames[i] = "";
			obituary.soLength[i] = 0;
			obituary.marriedSO[i] = false;
			obituary.soChildren[i] = 0;
		}


		if ( Random.Range (0,101) > 50)
		{
			nameCount = Random.Range(0,partner.maleNames.Length);
			playerName = partner.maleNames[nameCount];
		}
		else
		{
			nameCount = Random.Range(0,partner.femaleNames.Length);
			playerName = partner.femaleNames[nameCount];
		}
		inputNameText.text = playerName;
		monthInput.text = Random.Range (1,13).ToString();
		yearInput.text = Random.Range (1950,1995).ToString();
		impendingDeath = false;
		hotnessText.text = aspectText[0];
		personalityText.text = aspectText[1];
		wealthText.text = aspectText[2];
		careerText.text = aspectText[3];
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
				if (time > 1f)
				{
					Tick();
					time = 0f;
				}
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
		partner.personalityText.color = colorName;
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
		
		textupdate.Process( aspectvalue, aspectText);
		textupdate.Process( partner.aspectvalue, partner.aspectText);

		// career fill bar
		careerProgress.fillAmount = (aspectvalue[3] - careerFillMin) / (careerFillMax - careerFillMin);
		if ( aspectvalue[3] >= careerFillMax  && careerStep < (textupdate.careerThresholds.Length - 1) )
		{
			careerFillMin = textupdate.careerThresholds[careerStep];
			careerFillMax = textupdate.careerThresholds[careerStep + 1];
			careerStep++;
			careerProgress.fillAmount = 0;
		}
		careerProgressPartner.fillAmount = (partner.aspectvalue[3] - careerFillMinPartner) / (careerFillMaxPartner - careerFillMinPartner);
		if ( partner.aspectvalue[3] >= careerFillMaxPartner  && careerStepPartner < (textupdate.careerThresholds.Length - 1) )
		{
			careerFillMinPartner = textupdate.careerThresholds[careerStepPartner];
			careerFillMaxPartner = textupdate.careerThresholds[careerStepPartner + 1];
			careerStepPartner++;
			careerProgressPartner.fillAmount = 0;
		}
		PartnerCareer.color = colorName;
		tempColor = PartnerFill.color;
		tempColor.a = colorName.a;
 		PartnerFill.color = tempColor;

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
			else 
			{
				startButton.interactable = false;
			}
		}

		// birthfade in
		if ( FadeIn == true && FadeInCycle < 1 )
		{
			birthColor.a += Time.deltaTime * birthTime;
			Birthfade.color = birthColor;
			FadeInCycle = birthColor.a;
		}
		if ( FadeIn == true && FadeInCycle >= 1 && FadeInCycle < 2 )
		{
			birthPanel.SetActive(false);
			birthColor.a -= Time.deltaTime * birthTime;
			Birthfade.color = birthColor;
			FadeInCycle = 2 - birthColor.a;
		}
		if ( FadeIn == true && FadeInCycle >= 2)
		{
			startTime = true;
			FadeIn = false;
		}

		if ( relationship.playerHappiness - relationship.pastplayerhappiness > 0 && impendingDeath == false )
		{
			relationship.playerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = true;
			relationship.playernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.playerpositiveemitter.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(relationship.playerHappiness - relationship.pastplayerhappiness));
		}
		if ( relationship.playerHappiness - relationship.pastplayerhappiness < 0 && impendingDeath == false )
		{
			relationship.playerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.playernegativeemitter.GetComponent<ParticleSystem>().enableEmission = true;
			relationship.playernegativeemitter.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(relationship.pastplayerhappiness - relationship.playerHappiness));
		}
		if ( inARelationship == true && relationship.partnerHappiness - relationship.pastpartnerhappiness > 0 && impendingDeath == false )
		{
			relationship.partnerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = true;
			relationship.partnernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.partnerpositiveemitter.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(relationship.partnerHappiness - relationship.pastpartnerhappiness));
		}
		if ( inARelationship == true && relationship.partnerHappiness - relationship.pastpartnerhappiness < 0 && impendingDeath == false )
		{
			relationship.partnerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.partnernegativeemitter.GetComponent<ParticleSystem>().enableEmission = true;
			relationship.partnernegativeemitter.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(relationship.pastpartnerhappiness - relationship.partnerHappiness));
		}
		if ( inARelationship == false )
		{
			relationship.partnerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.partnernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;
		}
		if ( impendingDeath == true )
		{
			relationship.partnerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.partnernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.playerpositiveemitter.GetComponent<ParticleSystem>().enableEmission = false;
			relationship.playernegativeemitter.GetComponent<ParticleSystem>().enableEmission = false;
		}

		relationship.pastpartnerhappiness = relationship.partnerHappiness;
		relationship.pastplayerhappiness = relationship.playerHappiness;

		if (glowing == true)
		{
			Glow ();
		}

		if ( career.buttonImage.activeSelf || relationship.buttonImage.activeSelf)
		{
			InvestIn.text = "Invest in";
		}
		else
		{
			InvestIn.text = "";
		}


		// update texts last
		hotnessText.text = aspectText[0];
		personalityText.text = aspectText[1];
		wealthText.text = aspectText[2];
		careerText.text = aspectText[3];
		partner.hotnessText.text = partner.aspectText[0];
		partner.personalityText.text = partner.aspectText[1];
		partner.wealthText.text = partner.aspectText[2];
		partner.careerText.text = partner.aspectText[3];
	}

	
	void Tick()
	{
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

		partner.StatsUpdate( aspectvalue, maxaspectvalue, age , playerIsMale, childrenCount, divorceCount, true);
		if ( inARelationship == true )
		{
			relationship.UpdateRelationship();
			partner.StatsUpdate( partner.aspectvalue, partner.maxaspectvalue, partner.partnerAge, !playerIsMale, partner.partnerChildren, partner.partnerDivorces, false);
		}
		if ( inARelationship == false ) 
		{
			if ( relationship.playerHappiness > 50 )
			{
				relationship.playerHappiness -= 0.5f;
			}
			if ( relationship.playerHappiness < 50 )
			{
				relationship.playerHappiness += 0.5f;
			}

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

		if ( duration > partner.exDuration )
		{
			partner.WipeEx();
		}

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
		if (relationship.countdown >= 0)
		{
			relationship.Invest();
		}

		if (quickTime == true) 
		{
			if ( fastForwardCount > 0) 
			{
				fastForwardCount--;
			}
			if ( fastForwardCount == 0 )
			{
				fastforward = 1;
				quickTime = false;
			}
		}

		if ( age + 1 == deathYear && monthCount == deathMonth && impendingDeath == false) 
		{
			impendingDeath = true;
			deathfadeCount = 0;
		}
		if ( impendingDeath == true )
		{
			deathfadeCount--;
		}
		if ( impendingDeath == true && deathfadeCount <= 0 )
		{
			deathColor.a += 1.0f / 12.0f;
			Deathfade.color = deathColor;
		}

		ageText.text = age.ToString("#");
		partnerAgeText.text = partner.partnerAge.ToString("#");
		monthText.text = months[monthCount];
		year = age + birthYear;
		yearText.text = year.ToString();
		Debug.Log ("player: " + relationship.playerHappiness + ". partner: " + relationship.partnerHappiness);
	}

	void CanHaveBabies()
	{
		if ( playerIsMale == true && partner.partnerAge >= infertileAge)
			{
				canHaveBabies = false;
				haveBabyButton.SetActive(false);
				return;	
			}
		if ( playerIsMale == false && age >= infertileAge )
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
		for (int i = 0; i < 4; i++)
		{
			aspectvalue[i] = Random.Range(1, 11);
			maxaspectvalue[i] = 0;
		}

		aspectvalue[2] = Random.Range(1,4);
		aspectvalue[3] = 0;
		careerFillMax = textupdate.careerThresholds[0];
		careerFillMin = 0;
		careerStep = 0;
		relationship.investEffectPlayer = 0;
		relationship.investEffectPartner = 0;

		if ( maleToggle.isOn == true ) {
			playerIsMale = true;
		}
		else
		{
			playerIsMale = false;
		}

		FadeIn = true;

		for ( int i = 0; i < 4; i++ )
		{
			partner.exvalue[i] = 0;
		}
		talk.Start();
	}

	void Match()
	{
		partner.Birth();
		options.On();
		relationship.partnerHappiness = Random.Range(35f,65f);
		talk.lastEvent = "matched";
		talk.lastEventCount = 0;
		talk.ResetTalk();
		talk.Speak();
		relationship.investEffectPlayer = 0;
		relationship.investEffectPartner = 0;
		haveBabyButton.SetActive(false);
		getMarriedButton.SetActive(false);
		relationship.pastpartnerhappiness = relationship.partnerHappiness;
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
		float moodswing = (relationship.playerHappiness - 50) / 2;
		relationship.playerHappiness = moodswing + 50;
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
		relationship.investEffectPlayer = 0;
		relationship.investEffectPartner = 0;
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
		float moodswing = 100 - relationship.playerHappiness;
		relationship.playerHappiness = Mathf.Max (Mathf.Min (moodswing, 80f), 10f);
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
		relationship.investEffectPlayer = 0;
		relationship.investEffectPartner = 0;
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
		deathColor.a = 0f;
		Deathfade.color = deathColor;
	}

	public void HaveBaby()
	{
		pregnancyCount = 9;
		pregnant = true;
		haveBabyButton.SetActive(false);
	}

	public void GetMarried()
	{
		if ( relationship.partnerHappiness + Random.Range (0,30) >= relationship.marriageThreshold  )
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
			relationship.playerHappiness -= 40;
			if (relationship.playerHappiness <= 10)
			{
				relationship.playerHappiness = 10;
			}
			relationship.partnerHappiness -= 10;
			getMarriedButton.SetActive(false);
			talk.lastEvent = "spurned";
			talk.lastEventCount = 0;
			talk.Speak();
		}
	}

	public void FastForward()
	{
		if ( fastTimeOn == false )
		{
			quickTime = true;
			fastforward = jumpTime;
			fastForwardCount = 5;
		}
	}

	public void FastTime()
	{
		fastforward = jumpTime;
		fastForwardCount = -1;
		fastTimeOn = true;
		pausebutton.GetComponent<Button>().interactable = true;
		playbutton.GetComponent<Button>().interactable = true;
		fastbutton.GetComponent<Button>().interactable = false;
		glowing = false;
		glowColor.a = 0;
		Glowfade.color = glowColor;
	}

	public void NormalTime()
	{
		fastforward = 1;
		fastForwardCount = 0;
		fastTimeOn = false;
		pausebutton.GetComponent<Button>().interactable = true;
		playbutton.GetComponent<Button>().interactable = false;
		fastbutton.GetComponent<Button>().interactable = true;
		glowing = false;
		glowColor.a = 0;
		Glowfade.color = glowColor;
	}

	public void PauseTime()
	{
		fastforward = 0;
		fastForwardCount = 0;
		fastTimeOn = false;
		pausebutton.GetComponent<Button>().interactable = false;
		playbutton.GetComponent<Button>().interactable = true;
		fastbutton.GetComponent<Button>().interactable = true;
	}

	public void Glow()
	{
		if ( glowColor.a >= 1)
		{
			glowUp = false;
		}
		if ( glowColor.a <= 0 )
		{
			glowUp = true;
		}

		if (glowUp == true)
		{
			glowColor.a += Time.deltaTime * 3;
			Glowfade.color = glowColor;
		}
		if (glowUp == false)
		{
			glowColor.a -= Time.deltaTime;
			Glowfade.color = glowColor;
		}
	}
}
