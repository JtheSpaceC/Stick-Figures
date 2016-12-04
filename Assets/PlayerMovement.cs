using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class PlayerMovement : MonoBehaviour {

	public Transform leftFoot;
	public Transform rightFoot;
	public Transform pelvis;
	public LineRenderer leftLeg;
	public LineRenderer rightLeg;

	Vector2 bodyPosition;

	public float legLength = 1;

	bool drawLeftKnee;
	bool drawRightKnee;

	Vector2 pelvisToFootDir;
	Vector2 perpendicularVector;
	Vector2 halfWayPoint;
	float perpendicularLength = 0;
	float a;
	//b would be the unknown value
	float c;
	Vector2 kneePosition;


	void LateUpdate () 
	{
		//MOVE BODY
		//move the whole body to average of feet positions
		bodyPosition = transform.position;
		bodyPosition.x = (leftFoot.position.x + rightFoot.position.x)/2;
		transform.position = bodyPosition;

		//FEET MOVEMENT
		//reset knees
		drawLeftKnee = false;
		drawRightKnee = false;

		//Figure out where feet are
		if(Vector2.Distance(leftFoot.position, pelvis.position) > legLength) //if the foot is further away than it can be
		{
			leftFoot.position = pelvis.position + (leftFoot.position - pelvis.position).normalized * legLength;
		}
		//else put in a knee joint
		else
		{
			drawLeftKnee = true;
		}

		if(Vector2.Distance(rightFoot.position, pelvis.position) > legLength) //if the foot is further away than it can be
		{
			rightFoot.position = pelvis.position + (rightFoot.position - pelvis.position).normalized * legLength;
		}
		//else put in a knee joint
		else 
		{
			drawRightKnee = true;	
		}

		//Draw legs
		if(!drawLeftKnee)
		{
			leftLeg.numPositions = 2;
			leftLeg.SetPosition(0, pelvis.position);
			leftLeg.SetPosition(1, leftFoot.position);
		}
		else
		{
			leftLeg.numPositions = 3;
			leftLeg.SetPosition(0, pelvis.position);
			leftLeg.SetPosition(2, leftFoot.position);

			pelvisToFootDir = leftFoot.position - pelvis.position;
			perpendicularVector = new Vector2(-pelvisToFootDir.y, pelvisToFootDir.x); //from -y,x for perpendicular lines
			halfWayPoint = (pelvis.position + leftFoot.position)/2;

			//a2 = b2 + c2. a is half the remaining distance. b is unknown. c is distance to halfway point
			a = legLength/2;
			c = Vector2.Distance(halfWayPoint, pelvis.position);

			// b2 = a2 - c2.  The root of b is perpendicularLength
			perpendicularLength = Mathf.Sqrt( Mathf.Pow(a, 2) - Mathf.Pow(c, 2) );

			kneePosition = halfWayPoint + (perpendicularVector.normalized * perpendicularLength);

			leftLeg.SetPosition(1, kneePosition);
		}

		if(!drawRightKnee)
		{
			rightLeg.numPositions = 2;
			rightLeg.SetPosition(0, pelvis.position);
			rightLeg.SetPosition(1, rightFoot.position);
		}
		else
		{
			rightLeg.numPositions = 3;
			rightLeg.SetPosition(0, pelvis.position);
			rightLeg.SetPosition(2, rightFoot.position);

			pelvisToFootDir = rightFoot.position - pelvis.position;
			perpendicularVector = new Vector2(-pelvisToFootDir.y, pelvisToFootDir.x); //from -y,x for perpendicular lines
			halfWayPoint = (pelvis.position + rightFoot.position)/2;

			Debug.DrawLine(pelvis.position, halfWayPoint, Color.red);

			//a2 = b2 + c2. a is half the remaining distance. b is unknown. c is distance to halfway point
			a = legLength/2;
			c = Vector2.Distance(halfWayPoint, pelvis.position);

			// b2 = a2 - c2.  The root of b is perpendicularLength
			perpendicularLength = Mathf.Sqrt( Mathf.Pow(a, 2) - Mathf.Pow(c, 2) );

			kneePosition = halfWayPoint + (perpendicularVector.normalized * perpendicularLength);

			Debug.DrawLine(pelvis.position, kneePosition, Color.green);

			rightLeg.SetPosition(1, kneePosition);
		}
	}


}
