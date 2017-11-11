using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(CameraRaycaster))]
public class cursorAffordance : MonoBehaviour {

	public Texture2D walkCursor = null;
	public Texture2D enemyCursor = null;
	public Texture2D debugCursor = null;
	public Texture2D blankCursor = null;
	public Vector2 hotSpot = new Vector2(0,0) ;

	[SerializeField] const int walkableLayerNumber = 8;
	[SerializeField] const int enemyLayerNumber = 9;
	CameraRaycaster cameraRaycaster;
	// Use this for initialization
	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
	}
	

	void OnLayerChanged (int newLayer) {
			
		//Cursor.SetCursor(walkCursor, hotSpot ,CursorMode.Auto);
			switch (newLayer)
			{
				case enemyLayerNumber:
					Cursor.SetCursor(enemyCursor,hotSpot,CursorMode.Auto);
					break;

				case walkableLayerNumber:
					Cursor.SetCursor(walkCursor,hotSpot,CursorMode.Auto);
					break;

				default:
					Cursor.SetCursor(debugCursor, hotSpot,CursorMode.Auto);
					break;

			};
		
	
	}
}
