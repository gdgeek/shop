using UnityEngine;
using System.Collections;
using GDGeek;

public class Shop : MonoBehaviour {
	private Commodity[] commodities_ = null;
	public GameObject _offset = null;
	private float offset_ = 0.0f;
	private FSM fsm_ = new FSM();
	public int _select = 0;
	private const float Jianju_ = 15.0f;
	private State getNormal(){
		StateWithEventMap normal = new StateWithEventMap (); 	
		normal.addAction ("swipe_start", "touch");
		normal.onStart += delegate {
	//		Debug.Log("normal");
		};
		return normal;
	}
	private State getTouch(){
		StateWithEventMap touch = new StateWithEventMap (); 	
		touch.addAction ("swipe_end", "select");
		touch.onStart += delegate {
			
		//	Debug.Log("touch");
		};
		touch.onOver += delegate {
			_select = -(int)(_offset.transform.position.x/Jianju_ -0.5f) ;
			Debug.Log(_select);
		};

		return touch;
	}
	private State getSelect(){
		StateWithEventMap select = TaskState.Create (delegate {
			TweenTask tt = new TweenTask(delegate() {
				TweenLocalPosition tlp = TweenLocalPosition.Begin(this._offset, 0.3f, new Vector3(-_select * Jianju_, 0, 0) );

				return tlp;
			});
			//TweenLocalPosition();
			/*
			Task task = new Task();
			task.init = delegate {
				Debug.Log(_select);	
			};
			return task;*/
			return tt;
		}, fsm_, "normal");	

		select.onStart += delegate {
//			Debug.Log("select");
		};
		return select;
	}
	void Awake(){
		fsm_.addState ("normal", getNormal());
		fsm_.addState ("touch", getTouch());
		fsm_.addState ("select", getSelect ());
		fsm_.init ("normal");
	
	}
	private void layout(){
		for (int i = 0; i < commodities_.Length; ++i) {
			var pos = new Vector3 (i*Jianju_, 0, 0);
			commodities_ [i].transform.localPosition = pos;
		}
		
	}
	private Vector3 handlePosition(Vector2 delta){
		offset_ += delta.x;
		return new Vector3(offset_, 0,0);
	}
	// Use this for initialization
	void Start () {
		commodities_ = this.gameObject.GetComponentsInChildren<Commodity> ();
		//Debug.Log (commodities_.Length);
		this.layout ();
		EasyTouch.On_SwipeStart += delegate(Gesture gesture) {
			fsm_.post("swipe_start");
		};
		EasyTouch.On_SwipeEnd += delegate(Gesture gesture) {
			fsm_.post("swipe_end");
		};
		EasyTouch.On_Swipe += delegate(Gesture gesture) {
			_offset.transform.localPosition = handlePosition(gesture.deltaPosition);

		};
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
