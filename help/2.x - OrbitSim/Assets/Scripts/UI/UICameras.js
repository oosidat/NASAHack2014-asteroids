#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// UICameras.js
//
// -----


// -----
// Property Declarations
// -----

// Public
var _OrbitSim : OrbitSim;

var _CamGroup : Camera;
var _FrontCamera : Camera;
var _LeftCamera : Camera;
var _TopCamera : Camera;
var _UserCamera : Camera;


function Start () {
	
	// Have the user camera look at the origin
	_UserCamera.transform.LookAt(Vector3.zero, Vector3.up);
	
}

function Update () {
	
	// Move the cameras along with the system's center of mass
	transform.position = _OrbitSim.GetCenterOfMass();
	var tDistance : float = _OrbitSim.GetCamDistance();
	_CamGroup.transform.localScale = new Vector3(tDistance, tDistance, tDistance);
	
}


// -----
// Public Functions
// -----

function Draw (aRect : Rect, aPadding : float) {
	
	// Start the draw group
	GUI.BeginGroup(aRect);
		
		// Determine the various camera display rects
		var tCamWidth : float = (aRect.width - aPadding) / 2.0;
		var tCamHeight : float = (aRect.height - aPadding) / 2.0;
		var tULRect : Rect = new Rect(0.0, 0.0, tCamWidth, tCamHeight);
		var tURRect : Rect = new Rect((tCamWidth + aPadding), 0.0, tCamWidth, tCamHeight);
		var tLLRect : Rect = new Rect(0.0, (tCamHeight + aPadding), tCamWidth, tCamHeight);
		var tLRRect : Rect = new Rect((tCamWidth + aPadding), (tCamHeight + aPadding), tCamWidth, tCamHeight);
		
		// Draw each camera display
		DrawCameraDisplay(tULRect, aPadding, _UserCamera);
		DrawCameraDisplay(tURRect, aPadding, _TopCamera);
		DrawCameraDisplay(tLLRect, aPadding, _FrontCamera);
		DrawCameraDisplay(tLRRect, aPadding, _LeftCamera);
	
	// End the draw group
	GUI.EndGroup();
	
}


// -----
// Private Functions
// -----

private function DrawCameraDisplay (aRect : Rect, aPadding : float, aCamera : Camera) {
	
	// Draw the display label
	GUI.Box(aRect, "", "box_inverted");
	var tRect : Rect = new Rect((aRect.xMin + aPadding), (aRect.yMin + aPadding), aRect.width, 10.0);
	GUI.Label(tRect, aCamera.name, "label_left");
		
	// Get the bottom left position
	var tBottomLeft : Vector2 = GUIUtility.GUIToScreenPoint(new Vector2(aRect.xMin, aRect.yMax));
	
	// Convert to bottom-left based unit coordinates
	var tLeft : float = tBottomLeft.x / Screen.width;
	var tBottom : float = 1.0 - (tBottomLeft.y / Screen.height);
	var tWidth : float = aRect.width / Screen.width;
	var tHeight : float = aRect.height / Screen.height;
	
	// Set the camera's rect
	aCamera.rect = new Rect(tLeft, tBottom, tWidth, tHeight);
	
}

/*
private function StringToFloat (aString : String) : float {
	
	// Split string into value and power values
	var tData : Array = aString.Split("e"[0]);
	var tValue : float = parseFloat(tData[0]);
	var tPower : float = 0.0;
	if (tData.length > 1) { tPower = parseFloat(tData[1]); } 
	
	// Return the value
	return (tValue * Mathf.Pow(10.0, tPower));
	
}
*/

// -----
// TH