using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Smile
: MonoBehaviour {

	public Sprite mood0;
	public Sprite mood1;
	public Sprite mood2;
	public Sprite mood3;
	public Sprite mood4;
	public Sprite mood5;
	public Sprite mood6;
	public Sprite mood7;
	public Sprite mood8;
	public Sprite mood9;

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
		Moods (player, relationship.playerHappiness);
		Moods (partner, relationship.partnerHappiness);

		tempColor = partner.color;
		tempColor.a = display.colorName.a;
		partner.color = tempColor;
	}

	void Moods(Image mood, float happiness)
	{
		if ( happiness < 5)
		{
			mood.sprite = mood0;
		}
		if ( happiness >= 5 && happiness < 17 )
		{
			mood.sprite = mood1;
		}
		if ( happiness >= 17 && happiness < 28 )
		{
			mood.sprite = mood2;
		}
		if ( happiness >= 28 && happiness < 39 )
		{
			mood.sprite = mood3;
		}
		if ( happiness >= 39 && happiness < 50 )
		{
			mood.sprite = mood4;
		}
		if ( happiness >= 50 && happiness < 61 )
		{
			mood.sprite = mood5;
		}
		if ( happiness >= 61 && happiness < 72 )
		{
			mood.sprite = mood6;
		}
		if ( happiness >= 72 && happiness < 83 )
		{
			mood.sprite = mood7;
		}
		if ( happiness >= 83 && happiness < 95 )
		{
			mood.sprite = mood8;
		}
		if ( happiness >= 95 )
		{
			mood.sprite = mood9;
		}
	}
}
