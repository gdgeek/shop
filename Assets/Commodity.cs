using UnityEngine;
using System.Collections;
using GDGeek;
public class Commodity : MonoBehaviour {

	// Use this for initialization
	void Start () {


	}
	Vector3 toScale(float x){
		float r = (x + 7.0f) / 14.0f;
		float c = Tween.easeInOutSine (0.0f, 1.0f, r*2.0f)+1.0f;
		return new Vector3(c,c,c);
	}
	// Update is called once per frame
	void Update () {
		if (this.transform.position.x > -7.0f && this.transform.position.x < 7.0f) {
			this.transform.localScale = toScale (this.transform.position.x);
		} else {
			this.transform.localScale = Vector3.one;
		}
	}
}
