using UnityEngine;
using UnityEngine.UI;

public class SkillTreeZoom : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float zoomSpeed = 0.1f; // Adjust this value to control the zoom speed

    private void Update()
    {
        // Get the scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new scale factor
        float newScale = scrollRect.content.localScale.x + scrollInput * zoomSpeed;

        // Clamp the scale factor to a desired range (if needed)
        newScale = Mathf.Clamp(newScale, 0.5f, 2.0f); // Adjust the range as needed

        // Apply the new scale factor to the content
        scrollRect.content.localScale = new Vector3(newScale, newScale, 1f);
    }
}
