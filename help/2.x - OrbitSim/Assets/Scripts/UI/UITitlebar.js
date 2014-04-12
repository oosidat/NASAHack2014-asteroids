#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// UITitlebar.js
//
// -----


// -----
// Property Declarations
// -----

// Public
var _OrbitSim : OrbitSim;


// -----
// Public Functions
// -----

function Draw (aRect : Rect, aPadding : float) {
	
	// Pull the simulation name and version information
	var tSimName : String = _OrbitSim.GetName();
	var tSimVersion : float = _OrbitSim.GetSimVersion();
	
	// Start the draw group
	GUI.BeginGroup(aRect);
	
		// Draw a background box
		var tBGRect : Rect = new Rect(0.0, 0.0, aRect.width, aRect.height);
		GUI.Box(tBGRect, "");
		
		// Draw the title and version labels
		var tLabelRect : Rect = new Rect(aPadding, 0.0, (aRect.width - (2.0 * aPadding)), aRect.height);
		GUI.Label(tLabelRect, "OrbitSim : " + tSimName, "label_left");
		GUI.Label(tLabelRect, ("v" + tSimVersion.ToString("f1")), "label_right");
	
	// End the draw group
	GUI.EndGroup();
	
}


// -----
// TH