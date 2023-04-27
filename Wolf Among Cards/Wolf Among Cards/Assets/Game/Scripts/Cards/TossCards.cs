using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossCards : MonoBehaviour
{
    public static TossCards instance = null;

    [HideInInspector]
    public List<Card> Cards = new List<Card>();

    [SerializeField]
    private float _iterationsCount, _timeBetweenIterations;

    private List<int> _cardsIndexes = new List<int>();

    private void Awake()
    {
        instance = this;

        CardsPooler.OnGenerationEnd += StartTossByTimeAndCount;
    }

    private void UpdateLength()
    {
        for(int i = 0; i < Cards.Count; i++)
            _cardsIndexes.Add(i);
    }

    public void Toss()
    {
        UpdateLength();

        for (int y = 0; y < Cards.Count; y++)
        {
            int randomNumber = Random.Range(0, _cardsIndexes.Count);

            Cards[y].MoveTo(Cards[_cardsIndexes[randomNumber]].transform.position);

            _cardsIndexes.RemoveAt(randomNumber);
        }
    }

    private IEnumerator TossByTimeAndCount()
    {
        yield return new WaitForSeconds(Card.timeToSee + 1f);

        for(int i = 0; i < _iterationsCount; i++)
        {
            Toss();

            yield return new WaitForSeconds(_timeBetweenIterations);
        }

        Cards.Clear();
        _cardsIndexes.Clear();
    }

    private void StartTossByTimeAndCount() { StartCoroutine(TossByTimeAndCount()); }

    private void OnDisable()
    {
        CardsPooler.OnGenerationEnd -= StartTossByTimeAndCount;
    }
}
