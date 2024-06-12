using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public Transform FlashlightHold;

    void Update()
    {
        //AlignFlashlightWithCharacter();
    }

    void AlignFlashlightWithCharacter()
    {
        flashlight.transform.position = FlashlightHold.position; // Adjust the flashlight position slightly above the character
        flashlight.transform.rotation = FlashlightHold.rotation; // Align the flashlight direction with the character's front
    }
}
