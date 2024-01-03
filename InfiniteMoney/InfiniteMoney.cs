using JaLoader;
using System.Collections;
using UnityEngine;

namespace InfiniteMoney
{
    public class InfiniteMoney : Mod
    {
        public override string ModID => "InfiniteMoney"; // The mod's ID. Try making it as unique as possible, to avoid conflitcting IDs.
        public override string ModName => "Infinite Money"; // The mod's name. This is shown in the mods list. Does not need to be unique.
        public override string ModAuthor => "Leaxx"; // The mod's author (you). Also shown in the mods list.
        public override string ModDescription => "Adds semi-infinite money, works by resetting your money to 9999$ after every transaction."; // The mod's description. This is also shown in the mods list, upon clicking on "More Info".
        public override string ModVersion => "1.0"; // The mod's version. Also shown in the mods list. If your mod is open-source on GitHub, make sure that you're using the same format as your release tags (for example, 1.0.0)
        public override string GitHubLink => "https://github.com/Jalopy-Mods/InfiniteMoney"; // If your mod is open-source on GitHub, you can link it here to allow for automatic update-checking in-game. It compares the current ModVersion with the tag of the latest release (ex. 1.0.0 compared with 1.0.1)
        public override WhenToInit WhenToInit => WhenToInit.InGame; // When should the mod's OnEnable/Awake/Start/Update functions be called?

        public override bool UseAssets => false; // Does your mod use custom assetbundles?

        public override void SettingsDeclaration() // Declare all of your per-user settings here
        {
            base.SettingsDeclaration();

            InstantiateSettings();

            AddToggle("EnableInfMoney", "Enable the mod", true);

            EventsManager.Instance.OnTransaction += OnTransaction;
        }

        public override void Start() // Default Unity Start() function
        {
            base.Start();

            StartCoroutine(SetMoneyDelay());
        }

        public void AddOnTransaction()
        {
            EventsManager.Instance.OnTransaction += OnTransaction;
        }

        public void RemoveOnTransaction() 
        {
            EventsManager.Instance.OnTransaction -= OnTransaction;
        }

        public void OnTransaction()
        {
            if(GetToggleValue("EnableInfMoney") == false)
                return;

            //Console.Instance.Log("on transaction called");
            StartCoroutine(SetMoneyDelay());
        }

        private void SetMoney()
        {
            FindObjectOfType<WalletC>().TotalWealth = 9999;
            FindObjectOfType<WalletC>().UpdateWealth();
        }

        private IEnumerator SetMoneyDelay()
        {
            yield return new WaitForSeconds(2);
            SetMoney();
        }
    }
}
