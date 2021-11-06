using UnityEngine;

public class LocalizationDontDestroy : MonoBehaviour
{
    private string _localizationTypeStore = "ENG";

    public void SetLocalizationTypeStore(string type)
    {
        _localizationTypeStore = type;
    }

    public string GetLocalizationType()
    {
        return _localizationTypeStore;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
