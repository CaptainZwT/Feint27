using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindController : MonoBehaviour {

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private GameObject currentKey;

    // Start is called before the first frame update
    void Start() {
        keys.Add("upButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upButton", "W")));
        keys.Add("leftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftButton", "A")));
        keys.Add("downButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downButton", "S")));
        keys.Add("rightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightButton", "D")));
        keys.Add("recallButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("recallButton", "B")));
        keys.Add("journalButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("journalButton", "J")));
        keys.Add("ability1Button", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ability1Button", "Alpha1")));
        keys.Add("ability2Button", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ability2Button", "Alpha2")));
        keys.Add("ability3Button", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ability3Button", "Alpha3")));

        foreach (var key in keys) {
            var gameobj = transform.parent.Find(key.Key).GetChild(0);

            if (key.Value.ToString().Contains("Alpha")) {
                gameobj.GetComponent<Text>().text = key.Value.ToString().Substring(5);
            }
            else {
                gameobj.GetComponent<Text>().text = key.Value.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
    
    void OnGUI() {
        if (currentKey != null) {
            Event e = Event.current;
            if (e.isKey) {
                keys[currentKey.name] = e.keyCode;
                if (e.keyCode.ToString().Contains("Alpha")) {
                    currentKey.GetComponent<Text>().text = e.keyCode.ToString().Substring(5);
                }
                else {
                    currentKey.GetComponent<Text>().text = e.keyCode.ToString();
                }
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked) {
        currentKey = clicked;
        currentKey.GetComponent<Text>().text = "Select New Key...";
    }

    public void SaveKeys() {
        foreach (var key in keys) {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }
}