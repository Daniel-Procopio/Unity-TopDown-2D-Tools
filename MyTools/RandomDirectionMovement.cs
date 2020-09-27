using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires RigidBody2D on object, set to Kinematic with simulated physics

//This simulates an up and down arcing movement towards a randomized drop direction
//An arcVector moves perpendicular to the initial direction in order to simulate a rise and fall 
//The arcVector determines perpendicular angle based on the angle of the initial dropDirectionVector, 
//Set the dropTimeDelay to delay movement
//Set lower and upper bound to randomize how far and long movement goes
//Set lower and upper bound of directionDegrees to determine which direction object can move
//Set dropSpeed and arcRisingSpeed
public class RandomDirectionMovement: MonoBehaviour
{
    private Rigidbody2D pickerRb;

    private bool currentlyMoving = false;
    private bool isRising = false;

    public float dropTimeDelay; //delay before movement begins

    public float dropTime; //randomized time it will travel through the air, determines how far something actually goes
    public float randomDropTimeLowerBound; //random drop time between lower and upper bound
    public float randomDropTimeUpperBound;

    private float timeSinceStarted;
    private float percentMovementComplete;

    public float dropDirectionDegrees; //degrees used to determine drop direction vector
    public int dropDirectionDegreesLowerBound; //can drop between lower and upper bound degrees
    public int dropDirectionDegreesUpperBound;

    private Vector2 rbCurrentVelocity;
    private Vector2 dropDirectionVector;

    public float dropSpeed; //speed at which dropped items travel
    private Vector2 arcVector; //Perpendicular to the drop direction to simulate an arc in movement
    public float arcRisingSpeed; //speed at which it arcs up and back down


    /// <summary>
    /// On Start we initialize our item picker
    /// </summary>
    public virtual void Start()
    {
        Invoke("StartDropping", dropTimeDelay);
    }

    
    //ran each physics update frame
    private void FixedUpdate()
    {
        if (currentlyMoving)
        {
            timeSinceStarted = Time.time;
            percentMovementComplete = timeSinceStarted / (dropTime / 2); //set to half the dropTime to determine how long it takes to reach apex of arc

            if (isRising == true) //first half of arc of movement
            {
                if (percentMovementComplete < 1) //not finished rising
                {
                    rbCurrentVelocity = Vector2.Lerp(dropDirectionVector, (dropDirectionVector + arcVector), percentMovementComplete);
                    pickerRb.velocity = rbCurrentVelocity;
                }

                else //finished rising
                {
                    isRising = false;
                }
            }

            else //second half of arc of movement
            {
                if (percentMovementComplete < 1) //falling
                {
                    rbCurrentVelocity = Vector2.Lerp(dropDirectionVector, (dropDirectionVector - arcVector), percentMovementComplete);
                    pickerRb.velocity = rbCurrentVelocity;
                }

                else //Completed the drop
                {
                    pickerRb.velocity = Vector2.zero;
                }
            }
        }
    }


    //dropDirectionDegrees determines direction of arcVector 
    public void StartDropping()
    {
        pickerRb = transform.GetComponent<Rigidbody2D>();

        dropDirectionDegrees = UnityEngine.Random.Range(dropDirectionDegreesLowerBound, dropDirectionDegreesUpperBound);

        dropTime = UnityEngine.Random.Range(randomDropTimeLowerBound, randomDropTimeUpperBound); //How long it will drop randomized between two values

        dropDirectionVector = DegreeToVector2(dropDirectionDegrees) * dropSpeed;

        //if trajectory on right side of player arcVector + 90f degrees
        if ((dropDirectionDegrees >= 0 && dropDirectionDegrees <= 90) || (dropDirectionDegrees > 270 && dropDirectionDegrees < 360))
        {
            arcVector = DegreeToVector2(dropDirectionDegrees + 90f) * arcRisingSpeed;
        }

        else //if trajectory on left side of player arcVector - 90f degrees
        {
            arcVector = DegreeToVector2(dropDirectionDegrees - 90f) * arcRisingSpeed;
        }

        currentlyMoving = true;
        isRising = true;
    }


    //converts radians to direction Vector2 
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    //converts degrees to direction Vector2
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
