#pragma strict
#pragma implicit
#pragma downcast

// -----
//
// OrbitSim.js
//
// -----


// -----
// Property Declarations
// -----

// Public
var _BodyPrefab : GameObject;

var _BlueMaterial : Material;
var _GreyMaterial : Material;
var _OrangeMaterial : Material;
var _RedMaterial : Material;
var _TanMaterial : Material;
var _WhiteMaterial : Material;
var _YellowMaterial : Material;

// Private                         
private var _Bodies : GameObject[] = new GameObject[0];
private var _CamDistance : float = 1.0;
private var _COM : Vector3 = Vector3.zero;
private var _GravConstant : float = 6.673 * Mathf.Pow(10.0, -11.0); // Units: m3 kg-1 s-2
private var _Mass : float = 0.0;
private var _Name : String = "";
private var _SimTime : float = 0.0; // D (Days)
private var _Status : String = "Uninitialized...";

private var _DistanceConversion : float = 6.68458712 * Mathf.Pow(10.0, -12.0); // m -> AU (Astronomical Units)
private var _MassConversion : float = 1.67403241 * Mathf.Pow(10.0, -25.0); // kg -> E (Earth masses)
private var _TimeConversion : float = 1.15740741 * Mathf.Pow(10.0, -5.0); // s -> D (Days)

private var _InitialTotalEnergy : float = 0.0;
private var _CurrentTotalEnergy : float = 0.0;

private var _SimVersion : float = 1.0;


// -----
// Procedural Event Functions
// -----

function FixedUpdate () {
	
	// Step the simulation
	StepSimulation();
	
	// Update the sim time
	_SimTime += Time.deltaTime; //(Time.deltaTime * Time.timeScale) / _TimeConversion;
	
}


function Start () {
	
	// Convert the gravitational constant's units
	_GravConstant *= Mathf.Pow(_DistanceConversion, 3.0);
	_GravConstant /= _MassConversion;
	_GravConstant /= Mathf.Pow(_TimeConversion, 2.0);
	
	// Load the simulation data
	//LoadSimulation("XML/SolarSystem.xml");
	
	// Accelerate the physics system a bit for easier viewing... :)
	Time.timeScale = 50.0;
	
}


// -----
// Public Functions
// -----

function GetCamDistance () : float {
	return _CamDistance;
}
function GetCenterOfMass () : Vector3 {
	return _COM;
}
function GetCurrentTotalEnergy () : float {
	return _CurrentTotalEnergy;
}
function GetInitialTotalEnergy () : float {
	return _InitialTotalEnergy;
}
function GetName () : String {
	return _Name;
}
function GetSimTime () : float {
	return _SimTime;
}
function GetSimVersion () : float {
	return _SimVersion;
}

/*
function LoadSimulation (aURL : String) : IEnumerator {
	
	// Modify the URL for author-time testing if necessary
	if (Application.isEditor && (aURL.IndexOf("http://") == -1)) {
		aURL = "file://" + Application.dataPath + "/" + aURL;
	}
	
	// Retrieve the simulation XML file from the URL provided
	var tWWW : WWW = new WWW(aURL);
	yield tWWW;
	
	// Check for errors
	if (tWWW.error != null) {
		
		// Display an error message
		// TBD: how/when/where to show an error message?
		
	} else {
		
		// Parse the simulation XML data
		var tXMLParser : XMLParser = new XMLParser();
		var tSimulationData : Hashtable[] = tXMLParser.ParseString(tWWW.data);
		
		// Verify that it's a valid simulation to run
		if (tSimulationData.length > 0) {
			
			// Clear the current simulation
			ClearSimulation();
			
			// Build the new simulation
			BuildSimulation(tSimulationData);
			
		}
		
	}
	
}
*/

// -----
// Private Functions
// -----
/*
private function BuildSimulation (aSimulationData : Hashtable[]) : IEnumerator {
	
	// Pull the configuration data
	tConfigData = aSimulationData[0];
	
	// Update the cam distance and name
	_CamDistance = this.StringToFloat(tConfigData["camdistance"]);
	_Name = tConfigData["name"];
	
	yield;
	
	// Initialize the bodies array
	_Bodies = new GameObject[(aSimulationData.length - 1)];
	
	// Walk the simulation data hashtable array
	for (var i : int = 1; i < aSimulationData.length; i++) {
		
		// Initialize the current body's mass
		var tMass : float = StringToFloat(aSimulationData[i]["mass"]) * _MassConversion;
		
		// Update the system mass
		_Mass += tMass;
		
		// Initialize the current body's position (at point of semi-major axis)
		var tRadialDistance : float = StringToFloat(aSimulationData[i]["semimajoraxis"]) * _DistanceConversion;
		var tInclinationAngle : float = StringToFloat(aSimulationData[i]["inclination"]) * ((2.0 * Mathf.PI) / 360.0); // radians
		var tPositionX : float = tRadialDistance * Mathf.Cos(tInclinationAngle);
		var tPositionY : float = tRadialDistance * Mathf.Sin(tInclinationAngle);
		var tPosition : Vector3 = new Vector3(tPositionX, tPositionY, 0.0);
		
		// Initialize the current body's velocity
		var tVelocityZ : float = StringToFloat(aSimulationData[i]["avgspeed"]) * (_DistanceConversion / _TimeConversion);
		var tVelocity : Vector3 = new Vector3(0.0, 0.0, tVelocityZ);
		
		// Instantiate the body
		var tBody : GameObject = Instantiate(_BodyPrefab, tPosition, Quaternion.identity);
		switch (aSimulationData[i]["color"]) {
			case "blue": 
				tBody.renderer.material = _BlueMaterial;
				break;
			case "grey": 
				tBody.renderer.material = _GreyMaterial;
				break;
			case "orange": 
				tBody.renderer.material = _OrangeMaterial;
				break;
			case "red": 
				tBody.renderer.material = _RedMaterial;
				break;
			case "tan": 
				tBody.renderer.material = _TanMaterial;
				break;
			case "white": 
				tBody.renderer.material = _WhiteMaterial;
				break;
			case "yellow": 
				tBody.renderer.material = _YellowMaterial;
				var tPointLight : GameObject = CreateLight();
				tPointLight.transform.parent = tBody.transform;
				tPointLight.transform.localPosition = Vector3.zero;
				break;
			default:
				break;
		}
		
		// Initialize the body
		tBody.name = aSimulationData[i]["name"];
		tBody.transform.parent = transform;
		tBody.rigidbody.mass = tMass;
		tBody.rigidbody.velocity = tVelocity;
		tBody.transform.localScale = Vector3.one * StringToFloat(aSimulationData[i]["radius"]);
		
		// Add the body to the bodies array
		_Bodies[(i-1)] = tBody;
		
	}
	
}
*/

private function ClearSimulation () {
	
	// Look for and destroy any existing bodies
	if (_Bodies.length > 0) {
	  for (var i : int = 0; i < _Bodies.length; i++) {
	  	Destroy(_Bodies[i]);
	  }
	  _Bodies = null;
	}
	_Bodies = new GameObject[0];
	
	// Reset the sim time
	_SimTime = 0.0;

	// Clear the name and reset the energy data
	_Name = "";
	_CurrentTotalEnergy = 0.0;
	_InitialTotalEnergy = 0.0;
	
	// Clear the center of mass and total mass
	_COM = Vector3.zero;
	_Mass = 0.0;
	
}

private function CreateLight () : GameObject {
	
	// Create a new game object with the light component
	var tLightGO : GameObject = new GameObject("Light");
	tLightGO.AddComponent(Light);
	tLightGO.light.color = Color.white;
	tLightGO.light.type = LightType.Point;
	return tLightGO;
	
}

private function StepSimulation () {
	
	// Initialize the kinetic and potential energies
	var tKineticEnergy : float = 0.0;
	var tPotentialEnergy : float = 0.0;
	
	// Initialize the NxN matrix to store the gravitational forces between each body-pair
	var tForcesMatrix : Array[] = new Array[_Bodies.length];
	
	// Initialize a matrix to store the total forces to apply on each body
	var tTotalForcesArray : Vector3[] = new Vector3[_Bodies.length];
			
	// Walk each body in the system
	for (var i : int = 0; i < _Bodies.length; i++) {
		
		// Initialize this body's gravitational forces array and total force value
		var tForcesArray : Vector3[] = new Vector3[_Bodies.length];
		var tTotalForce : Vector3 = Vector3.zero;
		
		// Update the kinetic energy
		tKineticEnergy += (0.5 * _Bodies[i].rigidbody.mass * _Bodies[i].rigidbody.velocity.sqrMagnitude);
		
		// Update the center of mass vector
		_COM += (_Bodies[i].rigidbody.mass * _Bodies[i].transform.position);
		
		// Walk each body-pair in the system
		for (var j : int = 0; j < _Bodies.length; j++) {
			
			// Check the body-pair indices
			if (i != j) {
				
				// Calculate the gravitational force between these two bodies
				var tSeparationVector : Vector3 = _Bodies[j].transform.position - _Bodies[i].transform.position;
				var tForceValue : float = (_GravConstant * _Bodies[i].rigidbody.mass * _Bodies[j].rigidbody.mass) / tSeparationVector.sqrMagnitude;
				tForcesArray[j] = tSeparationVector.normalized * tForceValue;
				
				// Update the potential energy if necessary
				if (i < j) { tPotentialEnergy -= (tForceValue * tSeparationVector.magnitude); }
				
			} else {
				
				// Use a zero vector, no self gravitation
				tForcesArray[j] = Vector3.zero;
				
			}
			
			// Increment the total force to apply
			tTotalForce += tForcesArray[j];
				
		}
		
		// Add this body's forces array to the matrix and total force to the array
		tForcesMatrix[i] = tForcesArray;
		tTotalForcesArray[i] = tTotalForce;
		
	}
	
	// Finalize the center of mass vector
	_COM = (1.0 / _Mass) * _COM;
	
	// Apply the total force to each body in the system
	for (var k : int = 0; k < _Bodies.length; k++) {
		_Bodies[k].rigidbody.AddForce(tTotalForcesArray[k]);
	}
	
	// Store the current total energy
	_CurrentTotalEnergy = tKineticEnergy + tPotentialEnergy;
	
	// Store the total energy if this is a first step for the simulation
	if (_InitialTotalEnergy == 0.0) { 
		_InitialTotalEnergy = tKineticEnergy + tPotentialEnergy;
	}
	
}


private function StringToFloat (aString : String) : float {
	
	// Split string into value and power values
	var tData : Array = aString.Split("e"[0]);
	var tValue : float = parseFloat(tData[0].ToString());
	var tPower : float = 0.0;
	if (tData.length > 1) { tPower = parseFloat(tData[1].ToString()); } 
	
	// Return the value
	return (tValue * Mathf.Pow(10.0, tPower));
}



// -----
// TH