
using Assets.Scripts.CharacterComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class UIController
    {
        // HUD variables
        private GameObject Status;
        private GameObject Journal;
        private GameObject Dialogue;

        // HUD Components
        private Slider Healthbar, Manabar;
        private Text CreditsHUD, ArmorHUD, SpellPowerHUD, CDRHUD, AlignmentHUD;
        private Image PAHUD, A1HUD, A2HUD, A3HUD;

        // Character variable
        private Player plyr;

        // variables
        private int journalTab;

        // constructor
        public UIController(GameObject _status, GameObject _journal, GameObject _dialogue, Player _plyr)
        {
            // initializing HUD variables
            Status = _status;
            Journal = _journal;
            Dialogue = _dialogue;

            // initializing HUD components
            Healthbar = _status.transform.Find("Healthbar").GetComponent<Slider>();
            Manabar = _status.transform.Find("Manabar").GetComponent<Slider>();

            CreditsHUD = _journal.transform.Find("Stats").GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
            ArmorHUD = _journal.transform.Find("Stats").GetChild(1).GetChild(1).gameObject.GetComponent<Text>();
            SpellPowerHUD = _journal.transform.Find("Stats").GetChild(2).GetChild(1).gameObject.GetComponent<Text>();
            CDRHUD = _journal.transform.Find("Stats").GetChild(3).GetChild(1).gameObject.GetComponent<Text>();
            AlignmentHUD = _journal.transform.Find("Stats").GetChild(4).GetChild(1).gameObject.GetComponent<Text>();

            PAHUD = _status.transform.GetChild(0).GetComponent<Image>();
            A1HUD = _status.transform.GetChild(1).GetComponent<Image>();
            A2HUD = _status.transform.GetChild(2).GetComponent<Image>();
            A3HUD = _status.transform.GetChild(3).GetComponent<Image>();

            // Initializing character
            plyr = _plyr;
        }

        public void HandleUI()
        {
            UpdateUI();

            // activations b
            if (Input.GetButtonDown("OpenJournal"))
            {
                Journal.SetActive(!Journal.activeSelf);
            }

            // updates
        }

        // Helper functions
        private void UpdateUI()
        {
            // health/mana
            Healthbar.value = plyr.health / plyr.maxHealth;
            Manabar.value = plyr.mana / plyr.maxMana;

            // stats
            UpdateStatUI();

            // inv
            UpdateInventoryUI();
        }

        private void UpdateStatUI()
        {
            if (plyr.credits >= 1000)
            {
                CreditsHUD.text = (plyr.credits / 1000).ToString() + "k";
            }
            else
            {
                CreditsHUD.text = plyr.credits.ToString();
            }

            ArmorHUD.text = plyr.armor.ToString();
            SpellPowerHUD.text = plyr.spellPower.ToString();
            CDRHUD.text = plyr.cooldownReduction.ToString() + "%";
            AlignmentHUD.text = plyr.alignment.ToString();
        }

        private void UpdateInventoryUI()
        {

        }

        private void UpdatePouchUI()
        {
            if (plyr.pouch.spells.Count > 3)
            {
                PAHUD.sprite = Resources.Load<Sprite>("Sprites/spells/blue_" + plyr.pouch.spells[0].ID);
                A1HUD.sprite = Resources.Load<Sprite>("Sprites/spells/blue_" + plyr.pouch.spells[1].ID);
                A2HUD.sprite = Resources.Load<Sprite>("Sprites/spells/blue_" + plyr.pouch.spells[2].ID);
                A3HUD.sprite = Resources.Load<Sprite>("Sprites/spells/blue_" + plyr.pouch.spells[3].ID);
            }
        }
    }
}
