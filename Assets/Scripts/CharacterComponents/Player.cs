  using Assets.Scripts.CharacterComponents.Parts;
using Assets.Scripts.CharacterComponents.Parts.Journal;
using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Spells;
using UnityEngine;

namespace Assets.Scripts.CharacterComponents
{
    class Player : Character
    {
        // initializing UI placeholders
        [Header("UI Holders")]

        [SerializeField] private GameObject StatusHUD;
        [SerializeField] private GameObject JournalHUD;
        [SerializeField] private GameObject DialogueHUD;

        // initializing variables
        private Journal journal;
        private Equipment equipment;

        public UIController ui_c;

        public new void Awake()
        {
            // initializing character controller
            char_control = GetComponent<CharacterControl>();

            ui_c = new UIController(StatusHUD, JournalHUD, DialogueHUD, this);

            pouch = new Pouch();
            pouch.spells.Add(new Spell("19", "Lizard Eye", 0f));
            pouch.spells.Add(new Spell("03", "Sonar", 0.3f));
            pouch.spells.Add(new Spell("05", "Wave", 2f));
            pouch.spells.Add(new Spell("08", "Tree of Life", 4f));
        }

        public void Update()
        {
            // Operations
            ui_c.HandleUI();
        }
    }
}
