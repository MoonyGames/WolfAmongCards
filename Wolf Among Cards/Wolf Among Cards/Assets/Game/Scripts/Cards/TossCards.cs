using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardsPooler))]
public class TossCards : MonoBehaviour
{
    public static TossCards Instance { get; private set; } = null;

    [SerializeField]
    private float _iterationsCount, _timeBetweenIterations;

    [SerializeField]
    private float _timeMultiplier = 2f;

    private List<int> _cardsIndexes = new List<int>();

    private void Awake()
    {
        Time.timeScale = _timeMultiplier;

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        CardsPooler.OnGenerationEnd += StartTossByTimeAndCount;
    }

    private void UpdateLength()
    {
        for(int i = 0; i < CardsPooler.Cards.Count; i++)
            _cardsIndexes.Add(i);
    }

    public void Toss()
    {
        UpdateLength();

        for (int y = 0; y < CardsPooler.Cards.Count; y++)
        {
            int randomNumber = Random.Range(0, _cardsIndexes.Count);

            CardsPooler.Cards[y].MoveTo(CardsPooler.Cards[_cardsIndexes[randomNumber]].transform.position);

            _cardsIndexes.RemoveAt(randomNumber);
        }
    }

    private IEnumerator TossByTimeAndCount()
    {
        StatusTextController.Instance.ChangeStatus("Ready...");

        yield return new WaitForSeconds(Card.timeToSee + 1f);

        StatusTextController.Instance.ChangeStatus("Tossing...");

        for (int i = 0; i < _iterationsCount; i++)
        {
            Toss();

            yield return new WaitForSeconds(_timeBetweenIterations);
        }

        StatusTextController.Instance.ChangeStatus("Choose!");

        Card.canBeChoosen = true;

        _cardsIndexes.Clear();
    }

    private void StartTossByTimeAndCount() { StartCoroutine(TossByTimeAndCount()); }

    private void OnDisable()
    {
        CardsPooler.OnGenerationEnd -= StartTossByTimeAndCount;
    }
}
