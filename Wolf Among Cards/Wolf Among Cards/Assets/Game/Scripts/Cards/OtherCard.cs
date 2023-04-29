using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OtherCard : Card
{
    [SerializeField]
    private CardData[] _cardDatas;

    protected override void OnEnable()
    {
        _data = _cardDatas[Random.Range(0, _cardDatas.Length)];

        base.OnEnable();
    }

    protected override void OnMouseDown()
    {
        if (!_isFlipped && canBeChoosen)
        {
            _isFlipped = true;

            FlipAnimation();

            ScoresCounter.Instance.AddScores(_data.Cost);

            for (int i = 0; i < CardsPooler.Cards.Count; i++)
            {
                CardsPooler.Cards[i].DisappearAnimation();
            }

            CardsPooler.Instance.LooseScreen.SetActive(true);

            Invoke(nameof(LoadNextScene), 5f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
