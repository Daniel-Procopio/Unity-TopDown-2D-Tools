using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Topdown2DTools : MonoBehaviour
{
    //Checks if the closest point on a gameObject's 2D collider is within inputDistance of the inputCheckedPosition
    public virtual bool GameObjectWithinDistance(float inputDistance, GameObject inputGameObject, Vector2 inputCheckedPosition) // Determines if the mouse hovered gameobject's collider's closest point is within distance of the player
    {
        Vector2 otherDistance;

        otherDistance = inputGameObject.GetComponent<Collider2D>().ClosestPoint(inputCheckedPosition); // returns the closest point between the collider and the player

        if ((otherDistance - inputCheckedPosition).sqrMagnitude <= inputDistance) // checks if the closest point is within Distance of the player's position
        {
            return true;
        }

        else
        {
            return false;
        }
    }


    /// <summary>
    /// Returns true if the two positions are within a certain distance of eachother
    /// sqrMagnitude is used for precision rather than sqrmagnitude due to diagonals being 2 and adjacents being 1 in sqrmagnitude
    /// </summary>
    public virtual bool Vector3WithinDistance(float distance, bool is2D, Vector3 checkedPosition, Vector3 playerPosRounded) //Checks if the input Vector3 is within distance of the player
    {
        float difference;
        distance *= distance;

        if (is2D)
        {
            difference = (((Vector2)checkedPosition - (Vector2)playerPosRounded).sqrMagnitude);
        }

        else
        {
            difference = ((checkedPosition - playerPosRounded).sqrMagnitude);
        }

        difference = Mathf.Abs(difference);

        if (difference <= distance)
        {
            //Debug.Log("Vector3s are within Distance of eachother");
            return true;
        }

        else
        {
            //Debug.Log("Vector3s not within distance of eachother");
            return false;
        }
    }


    //Converts radians to a Vector2
    public virtual static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }


    //converts degrees to a Vector2
    public virtual static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }


    /// Returns a random point in the space between two concentric circles.
    public virtual static Vector3 GetRandomPointBetweenTwoCircles(float minRadius, float maxRadius)
    {
        //Get a point on a unit circle (radius = 1) by normalizing a random point inside unit circle.
        Vector3 randomUnitPoint = Random.insideUnitCircle.normalized;
        //Now get a random point between the corresponding points on both the circles
        return GetRandomVector3Between(randomUnitPoint * minRadius, randomUnitPoint * maxRadius);
    }


    /// Returns a random vector3 between min and max. (Inclusive)
    public virtual static Vector3 GetRandomVector3Between(Vector3 min, Vector3 max)
    {
        return min + Random.Range(0f, 1f) * (max - min);
    }


    //returns calculates Vector3Int from Vector3 for grid
    public virtual Vector3Int CalculateGridPosition(Vector3 inputVector, Grid inputGrid)
    {
        return inputGrid.WorldToCell(inputVector);
    }


    //returns the center of grid position of a Vector3
    public virtual Vector3 ReturnVector3Center(Vector3 inputVector3, Grid inputGrid)
    {
        if (inputVector3 == null)
        {
            return Vector3.zero;
        }

        else
        {
            Vector3Int intVector;

            intVector = inputGrid.WorldToCell(inputVector3);

            return inputGrid.GetCellCenterWorld(intVector);
        }
    }


    //returns raycast hit target 2D at input position for the first object of layer checkedLayer
    public virtual RaycastHit2D PositionRayCast(Vector3 inputPosition, LayerMask checkedLayer) //raycast from mouse position to screen checking for whatever layers are passed in
    {
        return Physics2D.Raycast(inputPosition, Vector2.zero, Mathf.Infinity, checkedLayer); // test if mouse is hovering over any colliders belonging to checkedLayer
    }
}
