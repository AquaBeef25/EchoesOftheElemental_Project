using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CompletionScreen : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private string nextSceneName = "Quest_2_Scene";

    private Panel completionPanel;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        CreateCompletionScreen();
        canvasGroup.alpha = 0;
        completionPanel.gameObject.SetActive(false);
    }

    private void CreateCompletionScreen()
    {
        GameObject panelObj = new GameObject("CompletionPanel");
        panelObj.transform.SetParent(canvas.transform, false);

        Image panelImage = panelObj.AddComponent<Image>();
        panelImage.color = new Color(0, 0, 0, 0.8f);

        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        canvasGroup = panelObj.AddComponent<CanvasGroup>();
        completionPanel = panelObj.AddComponent<Panel>();

        GameObject textObj = new GameObject("LevelText");
        textObj.transform.SetParent(panelObj.transform, false);

        TextMeshProUGUI levelText = textObj.AddComponent<TextMeshProUGUI>();
        levelText.text = "LEVEL 1 COMPLETED!";
        levelText.fontSize = 60;
        levelText.alignment = TextAlignmentOptions.Center;
        levelText.color = Color.yellow;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector2(0, 100);
        textRect.sizeDelta = new Vector2(800, 200);

        GameObject buttonObj = new GameObject("NextButton");
        buttonObj.transform.SetParent(panelObj.transform, false);

        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.7f, 0.2f);

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(0, -100);
        buttonRect.sizeDelta = new Vector2(300, 80);

        Button nextButton = buttonObj.AddComponent<Button>();
        nextButton.onClick.AddListener(GoToNextScene);

        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.SetParent(buttonObj.transform, false);

        TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();
        buttonText.text = "Next Scene";
        buttonText.fontSize = 40;
        buttonText.alignment = TextAlignmentOptions.Center;
        buttonText.color = Color.white;

        RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.offsetMin = Vector2.zero;
        buttonTextRect.offsetMax = Vector2.zero;
    }

    public void ShowCompletionScreen()
    {
        completionPanel.gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        Debug.Log("Level Completed!");
    }

    private void GoToNextScene()
    {
        Debug.Log("Button clicked! Loading next scene...");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        Debug.Log($"Current scene: {currentSceneIndex}, Next scene: {nextSceneIndex}");

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Loading scene {nextSceneIndex}");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes! Game finished!");
        }
    }
}

public class Panel : MonoBehaviour { }

