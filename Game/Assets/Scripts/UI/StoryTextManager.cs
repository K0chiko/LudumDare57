using System.Collections.Generic;
using UnityEngine;

public class StoryTextManager : MonoBehaviour
{
    public static StoryTextManager Instance;

    public TextAsset storyTextJSON;
    public string currentLanguage = "ru"; // или "en"

    private Dictionary<string, StoryEntry> storyTexts;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        LoadTexts();
    }

    void LoadTexts()
    {
        storyTexts = new Dictionary<string, StoryEntry>();

        var wrapper = JsonUtility.FromJson<Wrapper>(storyTextJSON.text);
        foreach (var entry in wrapper.items)
        {
            storyTexts.Add(entry.key, entry.value);
        }
    }

    public string GetText(string key)
    {
        if (storyTexts.TryGetValue(key, out StoryEntry entry))
        {
            return currentLanguage == "en" ? entry.en : entry.ru;
        }

        return $"[Text missing: {key}]";
    }

    // Вспомогательные структуры — можно оставить внизу

    [System.Serializable]
    public class StoryEntry
    {
        public string en;
        public string ru;
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<StoryEntryItem> items;
    }

    [System.Serializable]
    private class StoryEntryItem
    {
        public string key;
        public StoryEntry value;
    }
}