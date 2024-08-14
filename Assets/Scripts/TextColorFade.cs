using UnityEngine;
using TMPro;

public class TextColorFade : MonoBehaviour
{
    public TMP_Text textElement;
    public float fadeSpeed = 1f; // Speed of the fade effect in seconds

    private bool fadeIn = true;
    private float currentAlpha = 0f;


    private void Update()
    {
        // Gradually change the alpha value of the text
        ChangeTextAlpha();

        // Reverse the fade direction when the alpha reaches 0 or 255
        if (currentAlpha <= 0f)
        {
            fadeIn = true;
        }
        else if (currentAlpha >= 255f)
        {
            fadeIn = false;
        }
    }

    private void ChangeTextAlpha()
    {
        // Calculate the new alpha value based on the fade direction and speed
        if (fadeIn)
        {
            currentAlpha += Time.deltaTime * fadeSpeed * 255f;
        }
        else
        {
            currentAlpha -= Time.deltaTime * fadeSpeed * 255f;
        }

        // Clamp the alpha value between 0 and 255
        currentAlpha = Mathf.Clamp(currentAlpha, 0f, 255f);

        // Update the text color with the new alpha value
        Color newColor = textElement.color;
        newColor.a = currentAlpha / 255f;
        textElement.color = newColor;
    }
}