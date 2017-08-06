/*
This script was developed by Jupp Otto + modified by Adriana Vecc for flying.
. It's free to use and there are no restrictions in modification.
If there are any questions you can send me an Email: juppotto3@gmail.com

This script moves your player automatically in the direction they are looking at. You can 
activate the autowalk function by pull the cardboard trigger.

This script can easally be configured in the Unity Inspector. 

How to get started with this script?: 
0. haven't the Google VR SDK yet, follow this guide https://developers.google.com/vr/unity/get-started
1. Import the exampple package downloaded in step 0 (GoogleVRForUnity.unitypackage).
2. Load the GVRDemo scene.
3. Attach a Rigidbody to the "Player" GameObject.
4. Freeze X, Y and Z Rotation of the Rigidbody in the inspector. 
5. Attach a Capsule Collider to the "Player" GameObject.
6. Attach the Autofly script to the "Player" GameObject.
7. Configure the Script in the insector for example: 
      - walk when triggered   = true 
      - speed                 = 3  
8. Start the scene and have fun! 
*/

using UnityEngine;
using System.Collections;

public class Autowalk : MonoBehaviour
{

    // This variable determinates if the player will move or not 
    private bool isFlying = false;

    Transform mainCamera = null;

    //This is the variable for the player speed
    [Tooltip("With this speed the player will move.")]
    public float speed;

    [Tooltip("Activate this checkbox if the player shall move when the Cardboard trigger is pulled.")]
    public bool FlyWhenTriggered;

    [Tooltip("Activate this Checkbox if you want to freeze the y-coordiante for the player. " +
             "For example in the case of you have no collider attached to your CardboardMain-GameObject" +
             "and you want to stay in a fixed level.")]
    public bool freezeYPosition;

    [Tooltip("This is the fixed y-coordinate.")]
    public float yOffset;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void Update()
    {
      // Fly when the Cardboard Trigger is used 
	if (FlyWhenTriggered && !isFlying && GvrViewer.Instance.Triggered)
        {
            isFlying = true;
        }
      else if (FlyWhenTriggered && isFlying && GvrViewer.Instance.Triggered)
        {
            isFlying = false;
        }

      if (isFlying)
        {
		Vector3 direction = new Vector3(mainCamera.transform.forward.x, mainCamera.transform.forward.y, mainCamera.transform.forward.z).normalized * speed * Time.deltaTime;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, -transform.rotation.eulerAngles.y, 0));
		transform.Translate(rotation * direction);
        }

       if (freezeYPosition)
        {
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }
    }
}
