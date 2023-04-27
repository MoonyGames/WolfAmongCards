using UnityEngine;
using TMPro;

public class ScoresCounter : MonoBehaviour
{
    public static ScoresCounter Instance { get; private set; } = null;

    [SerializeField]
    private TextMeshProUGUI _text;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _text.text = "Scores: " + PlayerPrefs.GetInt("Scores", 0).ToString();
    }

    public void AddScores(int count)
    {
        PlayerPrefs.SetInt("Scores", PlayerPrefs.GetInt("Scores", 0) + count);

        _text.text = "Scores: " + PlayerPrefs.GetInt("Scores", 0).ToString();
    }
}
