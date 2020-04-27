
namespace Assets.Scripts.CharacterComponents.Parts.Journal
{
    class Journal
    {
        private QuestLog _questlog;
        private Pouch _pouch;

        /// <summary>
        /// Initializes a Journal with an empty QuestLog and Pouch.
        /// </summary>
        public Journal()
        {
            _questlog = new QuestLog();
            _pouch = new Pouch();
        }
    }
}
