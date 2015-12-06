using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smile
: MonoBehaviour {

	public Sprite pained;
	public Sprite depressed;
	public Sprite sad;
	public Sprite basic;
	public Sprite content;
	public Sprite blushing;
	public Sprite hearteyes;

	public Image player;
	public Image partner;
	private Color tempColor;

	public GameObject canvas;
	private Display display;
	private Relationship relationship;

	void Start () {
		display = canvas.GetComponent<Display>();
		relationship = canvas.GetComponent<Relationship>();
	}
	
	void Update () {
		Moods(player, relationship.playerHappiness);
		Moods (partner, relationship.partnerHappiness);

		tempColor = partner.color;
		tempColor.a = display.colorName.a;
		partner.color = tempColor;
	}

	void Moods(Image mood, float happiness)
	{
		if ( happiness < 15)
		{
			mood.sprite = pained;
		}
		if ( happiness >= 15 && happiness < 30 )
		{
			mood.sprite = depressed;
		}
		if ( happiness >= 30 && happiness < 45 )
		{
			mood.sprite = sad;
		}
		if ( happiness >= 45 && happiness < 65 )
		{
			mood.sprite = basic;
		}
		if ( happiness >= 65 && happiness < 80 )
		{
			mood.sprite = content;
		}
		if ( happiness >= 80 && happiness < 95 )
		{
			mood.sprite = blushing;
		}
		if ( happiness >= 95 )
		{
			mood.sprite = hearteyes;
		}
	}
}
