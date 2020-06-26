using System.Globalization;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class StatsController : MonoBehaviour
    {
        public TextMeshProUGUI KilledRegular;
        public TextMeshProUGUI KilledDiver;
        public TextMeshProUGUI KilledTank;

        public TextMeshProUGUI PerksLife;
        public TextMeshProUGUI PerksEnergyBlast;

        public TextMeshProUGUI DamagePlayer;
        public TextMeshProUGUI DamagePlanet;

        public BasePanel MainMenuPanel;

        public BasePanel StatsPanel;

        void Awake()
        {
            StatsPanel.SetOnOpen(OnOpen);
        }

        private void OnOpen(GameObject obj)
        {
            KilledRegular.text = GameStats.KilledRegular.ToString();
            KilledDiver.text = GameStats.KilledDiver.ToString();
            KilledTank.text = GameStats.KilledTank.ToString();

            PerksLife.text = GameStats.PerkHealth.ToString();
            PerksEnergyBlast.text = GameStats.PerkEnergyBlast.ToString();
        
            DamagePlayer.text = GameStats.DamagePlayer.ToString(CultureInfo.InvariantCulture);
            DamagePlanet.text = GameStats.DamagePlanet.ToString(CultureInfo.InvariantCulture);

        }

        public void ButtonClick_ClosePanel()
        {
            StatsPanel.ClosePanel();
            MainMenuPanel.OpenPanel();
        }
        
    }
}
