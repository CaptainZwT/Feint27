
using Assets.Scripts.Information.Quests;
using System.Collections.Generic;

// This will hold a list of all quests held or completed by the player.
// It manages to feed into the UI for Journal

namespace Assets.Scripts.CharacterComponents.Parts.Journal
{
    class QuestLog
    {
        private List<Quest> quests;

        /// <summary>
        /// Initializes quests with a empty list of Quests.
        /// </summary>
        public QuestLog()
        {
            quests = new List<Quest>();
        }
    }
}
