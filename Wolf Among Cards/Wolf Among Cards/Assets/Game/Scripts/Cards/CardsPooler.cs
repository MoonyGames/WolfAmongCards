using System.Collections.Generic;
using UnityEngine;

public class CardsPooler : MonoBehaviour
{
    public static CardsPooler Instance { get; private set; } = null;

    public int Level { get; set; }

    private List<GameObject> _pooledObjects;

    public static List<Card> Cards = new List<Card>();

    [SerializeField]
    private GameObject _objectsToPool, _wolfCard;
    [SerializeField]
    private int _amountToPool;

    [SerializeField]
    private Transform _poolParent;

    [SerializeField]
    private Transform _triangleParent;

    private bool _wolfIsGenerated = false;

    public delegate void GenerationEnd();
    public static event GenerationEnd OnGenerationEnd;

    private void Awake()
    {
        Level = 1;

        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < _amountToPool; i++)
        {
            tmp = Instantiate(_objectsToPool, _poolParent);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);
        }

        GenerateTriangle();
    }

    private GameObject GetPooledObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
                return _pooledObjects[i];
        }

        return null;
    }

    public void GenerateTriangle()
    {
        Cards.Clear();
        Vector3 newPosition;
        _triangleParent.position = Vector3.zero;

        _wolfIsGenerated = false;

        _wolfCard = Instantiate(_wolfCard, _poolParent);
        _wolfCard.SetActive(false);

        for (int i = 0; i < 1 + Level; i++)
        {
            newPosition = new Vector3(-5f * i, 0f, -10f * i);

            for (int y = 0; y < i; y++)
            {
                Vector3 delta = new Vector3(10f, 0f, 0f);

                int randomNumber = Random.Range(0, 2);

                if (!_wolfIsGenerated && y == 0)
                {
                    _wolfCard.transform.position = newPosition + delta;
                    _wolfCard.transform.SetParent(_triangleParent);
                    _wolfCard.SetActive(true);

                    Cards.Add(_wolfCard.GetComponent<Card>());

                    _wolfIsGenerated = true;

                    newPosition = _wolfCard.transform.position;
                }

                else
                {
                    GameObject otherCard = GetPooledObject();
                    otherCard.transform.position = newPosition + delta;
                    otherCard.transform.SetParent(_triangleParent);
                    otherCard.SetActive(true);

                    Cards.Add(otherCard.GetComponent<Card>());

                    newPosition = otherCard.transform.position;
                }
            }
        }

        _triangleParent.position = new Vector3(-5f, 0f, 5 * Level);

        OnGenerationEnd?.Invoke();
    }

    public void DeactivateCards()
    {
        for(int i = 0; i < Cards.Count; i++)
        {
            Cards[i].transform.SetParent(_poolParent);
            Cards[i].DisappearAnimation();
        }

        GenerateTriangle();
    }
}
