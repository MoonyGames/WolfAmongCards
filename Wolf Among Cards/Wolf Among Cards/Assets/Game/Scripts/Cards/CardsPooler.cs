using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsPooler : MonoBehaviour
{
    public static bool IsGenerating { get; set; } = false;

    public static int Level;

    private List<GameObject> _pooledObjects;

    [SerializeField]
    private GameObject _objectsToPool, _wolfCard;
    [SerializeField]
    private int _amountToPool;

    [SerializeField]
    private Transform _poolParent;

    [SerializeField]
    private Transform _triangleParent;

    private bool _wolfIsGenerated = false;

    private void Awake() { Level = 3; }

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

        _wolfCard = Instantiate(_wolfCard, _poolParent);
        _wolfCard.SetActive(false);

        GenerateTriangle(Level);
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

    private void GenerateTriangle(int level)
    {
        Vector3 newPosition;

        for(int i = 0; i < 1 + level; i++)
        {
            newPosition = new Vector3(-5f * i, 0f, -10f * i);

            for (int y = 0; y < i; y++)
            {
                Vector3 delta = new Vector3(10f, 0f, 0f);

                int randomNumber = Random.Range(0, 2);

                if (!_wolfIsGenerated && randomNumber == 0)
                {
                    _wolfCard.transform.position = newPosition + delta;
                    _wolfCard.transform.SetParent(_triangleParent);
                    _wolfCard.SetActive(true);

                    TossCards.instance.Cards.Add(_wolfCard.GetComponent<Card>());

                    _wolfIsGenerated = true;

                    newPosition = _wolfCard.transform.position;
                }

                else
                {
                    GameObject otherCard = GetPooledObject();
                    otherCard.transform.position = newPosition + delta;
                    otherCard.transform.SetParent(_triangleParent);
                    otherCard.SetActive(true);

                    TossCards.instance.Cards.Add(otherCard.GetComponent<Card>());

                    newPosition = otherCard.transform.position;
                }
            }
        }

        _triangleParent.position = new Vector3(-5f, 0f, 5 * Level);

        TossCards.instance.Toss(3);
    }
}
