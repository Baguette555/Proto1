using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroFinalBoss : MonoBehaviour
{
    public NewControls newControls;
    IEnumerator coroutine;

    void Start()
    {
        StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        // Disable ce input system ou la mort
        newControls.speed = 0f;
        newControls.jumpForce = 0f;
        newControls.canDash = false;
        newControls.canFlip = false;
        yield return new WaitForSeconds(3);
        // R�active les inputs apr�s cin�matique
        newControls.speed = 8f;
        newControls.jumpForce = 9f;
        newControls.canDash = true;
        newControls.canFlip = true;
    }

}
