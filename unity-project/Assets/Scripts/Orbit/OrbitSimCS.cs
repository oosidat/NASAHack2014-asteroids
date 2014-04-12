using UnityEngine;
using System.Collections;

// speial thanks to HiggyB for the awesome OrbitSim.js
// http://forum.unity3d.com/members/1408-HiggyB

public class OrbitSimCS : MonoBehaviour {

	void Start () {

		// Convert the gravitational constant's units
		_GravConstant *= Mathf.Pow(_DistanceConversion, 3.0);
		_GravConstant /= _MassConversion;
		_GravConstant /= Mathf.Pow(_TimeConversion, 2.0);
		
		// Accelerate the physics system a bit for easier viewing... :)
		Time.timeScale = 50.0;
	}
	
	// Update is called once per frame
	void Update () {}

	void FixedUpdate () {
		
		// Step the simulation
		StepSimulation();
		
		// Update the sim time
		_SimTime += Time.deltaTime;
	}
	
	void StepSimulation () {

		int k = 0;
		int i = 0;
		int j = 0;

		// Initialize the kinetic and potential energies
		float tKineticEnergy = 0.0;
		float tPotentialEnergy = 0.0;

		// Initialize the NxN matrix to store the gravitational forces between each body-pair
		Array tForcesMatrix = new Array[_Bodies.length];

		// Initialize a matrix to store the total forces to apply on each body
		Vector3[] tTotalForcesArray = new Vector3[_Bodies.length];

		// Walk each body in the system
		for (i = 0; i < _Bodies.length; i++) {

				// Initialize this body's gravitational forces array and total force value
				Vector3[] tForcesArray = new Vector3[_Bodies.length];
				Vector3 tTotalForce = Vector3.zero;

				// Update the kinetic energy
				tKineticEnergy += (0.5 * _Bodies [i].rigidbody.mass * _Bodies [i].rigidbody.velocity.sqrMagnitude);

				// Update the center of mass vector
				_COM += (_Bodies [i].rigidbody.mass * _Bodies [i].transform.position);

				// Walk each body-pair in the system
				for (j = 0; j < _Bodies.length; j++) {
		
						// Check the body-pair indices
						if (i != j) {
			
								// Calculate the gravitational force between these two bodies
								Vector3 tSeparationVector = _Bodies [j].transform.position - _Bodies [i].transform.position;
								float tForceValue = (_GravConstant * _Bodies [i].rigidbody.mass * _Bodies [j].rigidbody.mass) / tSeparationVector.sqrMagnitude;
								tForcesArray [j] = tSeparationVector.normalized * tForceValue;
			
								// Update the potential energy if necessary
								if (i < j) {
										tPotentialEnergy -= (tForceValue * tSeparationVector.magnitude);
								}
			
						} else {
			
								// Use a zero vector, no self gravitation
								tForcesArray [j] = Vector3.zero;
						}
		
						// Increment the total force to apply
						tTotalForce += tForcesArray [j];
				}

				// Add this body's forces array to the matrix and total force to the array
				tForcesMatrix [i] = tForcesArray;
				tTotalForcesArray [i] = tTotalForce;
		}

		// Finalize the center of mass vector
		_COM = (1.0 / _Mass) * _COM;

		// Apply the total force to each body in the system
		for (k = 0; k < _Bodies.length; k++) {
				_Bodies [k].rigidbody.AddForce (tTotalForcesArray [k]);
		}

		// Store the current total energy
		_CurrentTotalEnergy = tKineticEnergy + tPotentialEnergy;

		// Store the total energy if this is a first step for the simulation
		if (_InitialTotalEnergy == 0.0) { 
				_InitialTotalEnergy = tKineticEnergy + tPotentialEnergy;
		}
	}
}
