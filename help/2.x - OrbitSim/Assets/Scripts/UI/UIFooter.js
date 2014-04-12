#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// UIFooter.js
//
// -----


// -----
// Property Declarations
// -----

// Public
var _OrbitSim : OrbitSim;

// Private
private var _Enabled : boolean = true;
private var _FPS : float = 0.0;
private var _FPSCount : int = 0;
private var _FPSInterval : int = 10;
private var _FPSStart : float;


// -----
// Procedural Event Functions
// -----

function Start () {
	
	// Initialize the FPS start time
	_FPSStart = Time.time;
	
	_OrbitSim = GameObject.Find("Simulation").GetComponent(OrbitSim);
	
}

function Update () {
	
	// Increment the FPS count
	_FPSCount++;
	
	// Check against the interval and trap the fps as necessary
	if (_FPSCount == _FPSInterval) {
		
		// Compute the fps
		_FPS = (_FPSInterval / (Time.time - _FPSStart)) * Time.timeScale;
		
		// Reset the count and start variables
		_FPSCount = 0;
		_FPSStart = Time.time;
		
	}
	
}


// -----
// Public Functions
// -----

function Draw (aRect : Rect, aPadding : float) {
	
	// Set the separator width
	var tSeparatorWidth : float = 20.0;
	
	// Start the draw group
	GUI.BeginGroup(aRect);
	
		// Draw a background box
		var tBGRect : Rect = new Rect(0.0, 0.0, aRect.width, aRect.height);
		GUI.Box(tBGRect, "");
		
		// Declare the rect and string variables
		var tRect : Rect;
		var tString : String = "-na-";
		
		// Draw the enable/disable toggle
		tRect = new Rect(aPadding, aPadding, (aRect.height - (2.0 * aPadding)), (aRect.height - (2.0 * aPadding)));
		_Enabled = GUI.Toggle(tRect, _Enabled, "");
		
		// Draw the GUI if enabled
		if (_Enabled) {
			
			// Get the energy data and draw the energy display
			var tInitTE : float = _OrbitSim.GetInitialTotalEnergy();
			var tCurrTE : float = _OrbitSim.GetCurrentTotalEnergy();
			tString = GetEnergyString(tCurrTE, tInitTE);
			tRect = DrawValueDisplay(tRect, aPadding, "TE:", tString);
		
			// Get the simulation time and draw the simulation time display
			var tSimTime : float = _OrbitSim.GetSimTime();
			tString = (tSimTime / 365.2425).ToString("f4") + "Y";
			tRect = DrawValueDisplay(tRect, aPadding, "Time:", tString);
		
			// Draw the frame rate display
			tString = _FPS.ToString("f2");
			tRect = DrawValueDisplay(tRect, aPadding, "FPS:", tString);
		
		}
	
	// End the draw group
	GUI.EndGroup();
	
}


// -----
// Private Functions
// -----

private function DrawValueDisplay (aRect : Rect, aPadding : float, aLabel : String, aValue : String) : Rect {
	
	// Draw the label
	var tRect : Rect = new Rect((aRect.xMax + aPadding), aRect.yMin, (aLabel.length * 7.0), aRect.height);
	GUI.Label(tRect, aLabel);
	
	// Draw the value
	tRect = new Rect(tRect.xMax, aRect.yMin, (aValue.length * 7.0), aRect.height);
	GUI.Label(tRect, aValue);
	
	// Return the rect
	return tRect;
	
}

private function GetEnergyString (aCurrTE : float, aInitTE : float) : String {
	
	// Get the delta in total energy since the simulation started
	var tDeltaTE : float = ((aCurrTE / aInitTE) * 100.0) - 100.0;
	
	// Initialize the energy string
	var tString : String = "";
	
	// Add the energy value
	if (aCurrTE >= 0.0) { tString += "+"; }
	tString += aCurrTE.ToString("f10");
	
	// Add the delta percentage
	tString += "(";
	if (tDeltaTE >= 0.0) { tString += "+"; }
	tString += tDeltaTE.ToString("f6") + "%)";
	
	// Return the string
	return tString;
	
}


// -----
// TH