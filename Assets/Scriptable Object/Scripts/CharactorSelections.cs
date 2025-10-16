using UnityEngine;

public class CharactorSelections : MonoBehaviour
{
    public static CharactorSelections instance;
    public CharactorScriptableObject charactorData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("Duplicate CharactorSelections instance found. Destroying the new one.");
            Destroy(gameObject);
        }
    }

    public static CharactorScriptableObject GetData()
    {
        return instance.charactorData;
    }

    public void SelectCharactor(CharactorScriptableObject charactor)
    {
        charactorData = charactor;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
