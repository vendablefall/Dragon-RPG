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
	CameraRaycaster cameraRaycaster;
	// Use this for initialization
	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.layerChangeObservers += OnDelegateCalled;
	}
	

	void OnDelegateCalled (Layer newLayer) {
			
		//Cursor.SetCursor(walkCursor, hotSpot ,CursorMode.Auto);
			switch (newLayer)
			{
				case Layer.Enemy:
					Cursor.SetCursor(enemyCursor,hotSpot,CursorMode.Auto);
					break;

				case Layer.Walkable:
					Cursor.SetCursor(walkCursor,hotSpot,CursorMode.Auto);
					break;

				case Layer.RaycastEndStop:
					Cursor.SetCursor(debugCursor, hotSpot,CursorMode.Auto);
					break;

			};
		
	
	}
}
