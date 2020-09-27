# Unity TopDown 2D Tools
Tools used for some common situations in Topdown 2D Unity games
Free to use / extend for personal use

1. ClockManager.cs
   - Manager used to calculate AM/PM display time as well as military time using Time.deltatime to track change in time
   
2. RandomDirectionMovement.cs
   - Add script to a gameObject to add movement that goes in a random direction between two input degrees from 0 to 360
   - A perpendicular vector to the initially randomly choosen direction going either +/- 90 degrees to simulate an arc
   - Both Vectors are linearly interpolated for a smooth movement curve
   - Distance traveled is based on dropTime which is randomly calculated between a lower and upper bound
   - Distance traveled is also changed by dropSpeed, while arcRisingSpeed determines height of parabolic arc towards movement destination
   
3. SortingLayerSetter.cs
   - This will make it so a character can appear in front of a tree when below it, but behind it when walking behind it
   - Add script to a gameObject to change the sorting layer order of the Sprite Renderer based on the transform.y position
   - Must be a 2D TopDown project using x,y for movement
   - Objects with a lower transform.y will appear in front of objects with a higher transform.y
   
4. Topdown2DTools
   - GameObjectWithinDistance(): Checks if the closest point on a gameObject's 2D collider is within inputDistance of the inputCheckedPosition
   - Vector3WithinDistance(): Returns true if the two positions are within a certain distance of eachother
   - RadianToVector2(): Converts radians to a Vector2
   - DegreeToVector2(): Converts degrees to a Vector2
   - GetRandomVector3Between(): Returns a random Vector3 between min and max Vector3s (Inclusive)
   - CalculateGridPosition(): Calculates Vector3Int from Vector3 for a Grid
   - ReturnVector3Center(): returns the center of grid position of Vector3
   - PositionRayCast(): Raycasts at input position for the first object of layer checkedLayer
