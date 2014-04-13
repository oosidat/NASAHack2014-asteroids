using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

	private Vector3 _COM = Vector3.zero;
	private float _GravConstant = 6.673f * Mathf.Pow(10.0f, -11.0f); // Units: m3 kg-1 s-2
	private float _Mass= 0.0f;
	private string _Name = "";
	private float _SimTime = 0.0f; // D (Days)
	
	private float _DistanceConversion = 6.68458712f * Mathf.Pow(10.0f, -12.0f); // m -> AU (Astronomical Units)
	private float _MassConversion = 1.67403241f * Mathf.Pow(10.0f, -25.0f); // kg -> E (Earth masses)
	private float _TimeConversion = 1.15740741f * Mathf.Pow(10.0f, -5.0f); // s -> D (Days)
	
	private float _InitialTotalEnergy = 0.0f;
	private float _CurrentTotalEnergy = 0.0f;
	
	private float _SimVersion = 1.0f;

	void Start () {

		// Convert the gravitational constant's units
		_GravConstant *= Mathf.Pow(_DistanceConversion, 3.0f);
		_GravConstant /= _MassConversion;
		_GravConstant /= Mathf.Pow(_TimeConversion, 2.0f);
		
		// Accelerate the physics system a bit for easier viewing... :)
		Time.timeScale = 50.0f;
	
	}

	void Update () {}

	void FixedUpdate () {
		
		// Step the simulation
		StepSimulation ();

		// Update the sim time
		_SimTime += Time.deltaTime;
	}

	void StepSimulation () {

		int i, j, k;
		
		// Initialize the kinetic and potential energies
		float tKineticEnergy = 0.0f;
		float tPotentialEnergy = 0.0f;

		GameObject[] _Bodies;

		_Bodies = GameObject.FindGameObjectsWithTag("asteroid");
		
		// Initialize the NxN matrix to store the gravitational forces between each body-pair
		Vector3[][] tForcesMatrix = new Vector3[_Bodies.Length][];
		
		// Initialize a matrix to store the total forces to apply on each body
		Vector3[] tTotalForcesArray = new Vector3[_Bodies.Length];
		
		// Walk each body in the system
		for (i = 0; i < _Bodies.Length; i++) {
			
			// Initialize this body's gravitational forces array and total force value
			Vector3[] tForcesArray = new Vector3[_Bodies.Length];
			Vector3 tTotalForce = Vector3.zero;
			
			// Update the kinetic energy
			tKineticEnergy += (0.5f * _Bodies[i].rigidbody.mass * _Bodies[i].rigidbody.velocity.sqrMagnitude);
			
			// Update the center of mass vector
			_COM += (_Bodies[i].rigidbody.mass * _Bodies[i].transform.position);

			// Walk each body-pair in the system
			for (j = 0; j < _Bodies.Length; j++) {
				
				// Check the body-pair indices
				if (i != j) {
					
					// Calculate the gravitational force between these two bodies
					Vector3 tSeparationVector = _Bodies[j].transform.position - _Bodies[i].transform.position;
					float tForceValue = (_GravConstant * _Bodies[i].rigidbody.mass * _Bodies[j].rigidbody.mass) / tSeparationVector.sqrMagnitude;
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
		_COM = (1.0f / _Mass) * _COM;
		
		// Apply the total force to each body in the system
		for (k = 0; k < _Bodies.Length; k++) {
			_Bodies[k].rigidbody.AddForce(tTotalForcesArray[k]);
		}
		
		// Store the current total energy
		_CurrentTotalEnergy = tKineticEnergy + tPotentialEnergy;
		
		// Store the total energy if this is a first step for the simulation
		if (_InitialTotalEnergy == 0.0f) { 
			_InitialTotalEnergy = tKineticEnergy + tPotentialEnergy;
		}
	}
}