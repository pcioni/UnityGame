using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InfoDisplay : MonoBehaviour {
	
	UIReq target;

	Text Name;
	Text TimeToNext;
	Text Description;
	Text Flavor;

	GameObject FlavorPanel;
	GameObject DescPanel;
	GameObject NamePanel;
	GameObject TimePanel;

	private void RendererAll( bool val ) {
		Name.enabled = val;
		TimeToNext.enabled = val;
		Description.enabled = val;
		Flavor.enabled = val;
		FlavorPanel.SetActive( val );
		DescPanel.SetActive( val );
		NamePanel.SetActive( val );
		TimePanel.SetActive( val );
	}

	// Use this for initialization
	void Start () {
		target = null;

		Name = transform.Find("FactoryName").gameObject.GetComponent<Text>();
		TimeToNext = transform.Find("TimeUntilNext").gameObject.GetComponent<Text>();
		Description = transform.Find("Description").gameObject.GetComponent<Text>();
		Flavor = transform.Find("FlavorText").gameObject.GetComponent<Text>();

		FlavorPanel = transform.Find("FlavorPanel").gameObject;
		DescPanel = transform.Find("DescPanel").gameObject;
		NamePanel = transform.Find("NamePanel").gameObject;
		TimePanel = transform.Find("TimePanel").gameObject;

		RendererAll( false );
	}
	
	// Update is called once per frame
	void Update () {
		if ( target == null )
			return;

		double tt = target.GetTimeToNext();
		if ( double.IsInfinity( tt ) ) {
			TimeToNext.text = "Time Until Next: Never";
		} else {
			TimeToNext.text = "Time Until Next: " + tt.ToString();
		}

		if ( target.FactoryName != Name.text )
			Name.text = target.FactoryName;
		if ( target.Description != Description.text )
			Description.text = target.Description;
		if ( target.FlavorText != Flavor.text )
			Flavor.text = target.FlavorText;
	}

	public void Enable( UIReq obj ) {
		if ( obj == null ) {
			RendererAll( false );
		} else {
			target = obj;
			RendererAll( true );
		}
	}
}
