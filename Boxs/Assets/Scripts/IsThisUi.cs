using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class IsThisUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private MainKawashima mainkawashima;
	private bool isUi;
	private GameObject main;
	// Use this for initialization
	void Start () {

		main = GameObject.Find ("Main");
		mainkawashima = main.GetComponent<MainKawashima>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnPointerEnter( PointerEventData eventData){
		isUi = true;
		mainkawashima.isUi = true;

	}
	
	public void OnPointerExit( PointerEventData eventData){
		isUi = false;
		mainkawashima.isUi = false;

	}

}
