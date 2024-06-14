using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{
    public CinematicBar cinematicBar;
    public float targetSize = 50f; // Example target size for the bars
    public float transitionTime = 1f; // Example time for the transition

    private void Start()
    {
        // Show cinematic bars at the start of the scene
        cinematicBar.Show(targetSize, transitionTime);

        // Example: Hide the bars after 5 seconds
        StartCoroutine(HideBarsAfterDelay(5f));
    }

    private IEnumerator HideBarsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        cinematicBar.Hide(transitionTime);
    }
}
