using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Talk : MonoBehaviour {

	public Text Comment;

	private Display display;
	private Relationship relationship;
	private Partner partner;
	public GameObject canvas;

	public string lastEvent;
	public int lastEventCount;
	public int lastTextCount;
	public int textCoolDown;
	public int usedTextMax;
	public int oneOff;
	public int oneOffValue;
	public int oneOffValueCareer;
	public int[] usedText;

	private bool retiredText;
	private bool infertileText;

	// Use this for initialization
	public void Start () {
		display = canvas.GetComponent<Display>();
		relationship = canvas.GetComponent<Relationship>();
		partner = canvas.GetComponent<Partner>();
		
		lastEvent = "";
		lastEventCount = 3;
		lastTextCount = 0;
		usedTextMax = 100;
		usedText = new int[100];
		oneOffValue = 2;
		oneOffValueCareer = 4;

		retiredText = true;
		infertileText = true;

		Comment.text = "Teenage angst ...";
		textCoolDown = 0;
		for (int i = 0; i < usedText.Length; i++)
		{
			usedText[i] = 0;
		}
	}

	public void TextUpdate()
	{
		lastEventCount++;
		lastTextCount++;
		for ( int i = 0; i < usedText.Length; i++)
		{
			if ( usedText[i] > 0 )
			{
				usedText[i]--;
			}
		}
	}

	public void ResetTalk()
	{
		for (int i = 0; i < usedText.Length; i++)
		{
			usedText[i] = 0;
		}
	}

	public void Speak()
	{
		// Events		
		if ( lastEvent == "divorce" && lastEventCount < 3 )
		{
			Comment.text = partner.partnerName + " didn't take the divorce well.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "divorce" && lastEventCount < 6  && lastEventCount >=3 )
		{
			Comment.text = "That divorce will cost you.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "breakup" && lastEventCount < 3 )
		{
			Comment.text = partner.partnerName + " didn't take the breakup well.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "passed" && lastEventCount < 3 )
		{
			Comment.text = partner.partnerName + " lost interest.";
			lastTextCount = 0;
			return;
		}	
		if ( lastEvent == "divorced" )
		{
			Comment.text = partner.partnerName + " divorced you.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "dumped" && lastEventCount < 3 )
		{
			Comment.text = partner.partnerName + " dumped you.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "married" )
		{
			Comment.text = partner.partnerName + " said YES!";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "spurned" )
		{
			Comment.text = partner.partnerName + " wasn't keen on getting married.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "matched" && lastEventCount < 3 )
		{
			Comment.text = partner.partnerName + " seems nice.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "hadchild" )
		{
			Comment.text = "You had a baby!";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "Focus" && lastEventCount < 3 )
		{
			Comment.text = "Your career is improving.";
			lastTextCount = 0;
			return;
		}
		if ( lastEvent == "Invest" && lastEventCount < 3)
		{
			Comment.text = partner.partnerName + " appreciated that.";
			lastTextCount = 0;
			return;
		}
		if ( display.age >= 65 && lastEventCount < 3 && retiredText == true )
		{
			Comment.text = "You decide it's time to retire.";
			lastTextCount = 0;
			retiredText = false;
			return;
		}
		if ( display.playerIsMale == true && partner.partnerAge >= display.infertileAge && lastTextCount > 3 && infertileText == true )
		{
			Comment.text = partner.partnerName + " is too old to have children now.";
			lastTextCount = 0;
			infertileText = false;
			return;
		}
		if ( display.playerIsMale == false && display.age >= display.infertileAge && lastTextCount > 3 && infertileText == true )
		{
			Comment.text = "You are too old to have children now.";
			lastTextCount = 0;
			infertileText = false;
			return;
		}

		// Career worries
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < 10 && display.age > 25 && lastTextCount > 3 && oneOff < oneOffValueCareer ) 
		{
			Comment.text = "Aren't you a little old to be a student?";
			lastTextCount = 0;
			return;
		}
		if ( display.aspectvalue[3] < 25 && display.age > 28 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "Aren't you a little old to be an intern?";
			lastTextCount = 0;
			return;
		}
		if ( display.aspectvalue[3] < 25 && display.age > 30 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "You think it's fine to try a few different jobs before committing to a career.";
			lastTextCount = 0;
			return;
		}
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < 50 && display.age > 30 && display.age < 40 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "You're over 30 and your career still hasn't taken off";
			lastTextCount = 0;
			return;
		}
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < 50 && display.age >= 40 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "You always thought you would achieve more before you were 40.";
			lastTextCount = 0;
			return;
		}
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < (display.age - 16) * 2.7f && display.age < 65 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "You're career isn't going as well as you would like it to.";
			lastTextCount = 0;
			return;
		}
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < (display.age - 16) * 2f && display.age < 65 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "Your friends think your career is a joke.";
			lastTextCount = 0;
			return;
		}
		oneOff = Random.Range(0,100);
		if ( display.aspectvalue[3] < (display.age - 16) * 2.7f && display.age < 30 && lastTextCount > 3 && oneOff < oneOffValueCareer )
		{
			Comment.text = "Your parents think you should get a real job.";
			lastTextCount = 0;
			return;
		}


		// Doubting partner

		if ( display.inARelationship == true )
		{
			oneOff = Random.Range(0,100);
			if ( partner.maxaspectvalue[0] > partner.aspectvalue[0] + 1 && usedText[0] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "You notice " + partner.partnerName + " has some new grey hairs";
				usedText[0] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( partner.maxaspectvalue[0] > partner.aspectvalue[0] + 2 && usedText[1] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "Was " + partner.partnerName + " always so wrinkled?";
				usedText[1] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[0] > partner.aspectvalue[0] && usedText[2] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "You could probably be with someone hotter than " + partner.partnerName;
				usedText[2] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[0] > partner.aspectvalue[0] && usedText[3] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "If only " + partner.partnerName + " was a bit sexier";
				usedText[3] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[0] > partner.aspectvalue[0] && usedText[10] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "You flirt with a beautiful colleague at work";
				usedText[10] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[0] > partner.aspectvalue[0] && usedText[11] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "You think of other people during sex";
				usedText[11] = usedTextMax;
				lastTextCount = 0;
				return;
			}

			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[4] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " made a stupid comment over dinner";
				usedText[4] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[5] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " doesn't seem to understand what you are talking about";
				usedText[5] = usedTextMax;
				lastTextCount = 0;
				return;
			}

			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[6] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " said something mean to you";
				usedText[6] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[7] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = "Maybe you should be with someone nicer than " + partner.partnerName;
				usedText[7] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[8] == 0 && lastTextCount >= 3 && oneOff < oneOffValue && display.age < 50)
			{
				Comment.text = "Your mother doesn't like " + partner.partnerName;
				usedText[8] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( display.aspectvalue[1] > partner.aspectvalue[1] && usedText[9] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " never seems to listen";
				usedText[9] = usedTextMax;
				lastTextCount = 0;
				return;
			}

			// Comparison to ex
			oneOff = Random.Range(0,100);
			if ( partner.aspectvalue[0] < partner.exvalue[0] && usedText[13] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.exName + " was way hotter than " + partner.partnerName;
				usedText[13] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( partner.aspectvalue[0] < partner.exvalue[0] && usedText[17] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.exName + " was so sexy compared to " + partner.partnerName;
				usedText[17] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( partner.aspectvalue[1] < partner.exvalue[1] && usedText[14] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " isn't as smart as " + partner.exName;
				usedText[14] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			oneOff = Random.Range(0,100);
			if ( partner.aspectvalue[1] < partner.exvalue[1] && usedText[15] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " isn't as kind as " + partner.exName;
				usedText[15] = usedTextMax;
				lastTextCount = 0;
				return;
			}
			if ( partner.aspectvalue[2] < partner.exvalue[2] && usedText[16] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
			{
				Comment.text = partner.partnerName + " isn't as well off as " + partner.exName;
				usedText[16] = usedTextMax;
				lastTextCount = 0;
				return;
			}
		}


		// oneOffs
		oneOff = Random.Range(0,100);
		if ( display.inARelationship == false && partner.exDuration > 0 && usedText[12] == 0 && lastTextCount >= 3 && oneOff < oneOffValue)
		{
			Comment.text = "You think about " + partner.exName + " a lot";
			usedText[12] = usedTextMax;
			lastTextCount = 0;
			return;
		}

		// general status
		oneOff = Random.Range(0,100);
		if ( display.durationMonths >= 3 && display.colorName.a <= 0.7f && lastTextCount >= 3 && display.age <= 25 && oneOff < 50 && lastTextCount >= 3)
		{
			Comment.text = "You are horny.";
			lastTextCount = 0;
			return;
		}
		if ( display.durationMonths >= 3 && display.colorName.a <= 0.7f && lastTextCount >= 3 && lastTextCount >= 3)
		{
			Comment.text = "You are lonely.";
			lastTextCount = 0;
			return;
		}

		if ( display.inARelationship == true && lastTextCount >= 3)
		{
			Comment.text = relationship.states[relationship.playerRating] + partner.partnerName + relationship.partnerStates[relationship.partnerRating];	
			return;
		}
	}
}
