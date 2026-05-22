using UnityEngine;

public class Monument : MonoBehaviour
{
    public ElementType requiredElement;
    private bool isActivated = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate(ElementType elementCast)
    {
        if (isActivated) return;

        if (elementCast == requiredElement)
        {
            isActivated = true;
            Debug.Log($"Monument activated with {requiredElement}!");
            spriteRenderer.color = Color.green;
            CheckAllActivated();
        }
        else
        {
            Debug.Log($"Wrong! Need {requiredElement}, got {elementCast}");
            spriteRenderer.color = Color.red;
            Invoke("ResetColor", 0.5f);
        }
    }

    private void ResetColor()
    {
        switch (requiredElement)
        {
            case ElementType.Fire:
                spriteRenderer.color = new Color(1f, 0.3f, 0f);
                break;
            case ElementType.Water:
                spriteRenderer.color = new Color(0f, 0.3f, 1f);
                break;
            case ElementType.Earth:
                spriteRenderer.color = new Color(0.6f, 0.4f, 0.2f);
                break;
        }
    }

    private void CheckAllActivated()
    {
        Monument[] allMonuments = FindObjectsOfType<Monument>();

        foreach (Monument monument in allMonuments)
        {
            if (!monument.isActivated)
                return;
        }

        // ALL ACTIVATED!
        Debug.Log("ALL MONUMENTS ACTIVATED! LEVEL COMPLETE!");

        // FIND AND SHOW THE COMPLETION SCREEN
        CompletionScreen completionScreen = FindObjectOfType<CompletionScreen>();
        if (completionScreen != null)
        {
            completionScreen.ShowCompletionScreen();
            Debug.Log("Completion screen shown!");
        }
    }
}