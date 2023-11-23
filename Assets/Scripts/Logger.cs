using UnityEngine;
using TMPro;

public class Logger : MonoBehaviour
{
    public static Logger Instance { get; private set; }

    public GameObject logPrefab;
    public Transform logContainer;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Transform child in logContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void Log(string message)
    {
        CreateLogEntry($"Log: {message}");
    }

    public void LogError(string message)
    {
        CreateLogEntry($"LogError: {message}");
    }

    private void CreateLogEntry(string text)
    {
        var logEntry = Instantiate(logPrefab, logContainer);
        logEntry.GetComponent<TextMeshProUGUI>().text = text;
    }
}
