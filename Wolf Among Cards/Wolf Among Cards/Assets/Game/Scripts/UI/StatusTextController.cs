using UnityEngine;
using TMPro;

public class StatusTextController : MonoBehaviour
{
    public static StatusTextController Instance { get; private set; } = null;

    [SerializeField]
    private TextMeshProUGUI _text;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void ChangeStatus(string status) { _text.text = status; }
}
