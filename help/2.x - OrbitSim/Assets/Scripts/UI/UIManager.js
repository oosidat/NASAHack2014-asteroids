#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// UIManager.js
//
// -----


// -----
// Property Declarations
// -----

// Public
var _Skin : GUISkin;

var _Titlebar : UITitlebar;
var _Cameras : UICameras;
var _Footer : UIFooter;


// -----
// Procedural Event Functions
// -----

function OnGUI () {
	
	// Set the GUI's skin
	GUI.skin = _Skin;
	
	// Set a few drawing properties
	var tPadding : float = 5.0;
	var tFooterHeight : float = 20.0;
	var tTitlebarHeight : float = 20.0;
	
	// Calculate the display dimensions
	var tDisplayHeight : float = Screen.height - (2.0 * tPadding);
	var tDisplayWidth : float = Screen.width - (2.0 * tPadding);
	
	// Calculate the various region rects
	var tTitlebarRect : Rect = new Rect(tPadding, tPadding, tDisplayWidth, tTitlebarHeight);
	var tFooterTop : float = Screen.height - (tFooterHeight + tPadding);
	var tFooterRect : Rect = new Rect(tPadding, tFooterTop, tDisplayWidth, tFooterHeight);
	var tCamerasTop : float = tTitlebarRect.yMax + tPadding;
	var tCamerasHeight : float = tFooterTop - tTitlebarRect.yMax - (2.0 * tPadding);
	var tCamerasRect : Rect = new Rect(tPadding, tCamerasTop, tDisplayWidth, tCamerasHeight);
	
	// Draw each region
	_Cameras.Draw(tCamerasRect, tPadding);
	_Titlebar.Draw(tTitlebarRect, tPadding);
	_Footer.Draw(tFooterRect, tPadding);
	
}


// -----
// TH