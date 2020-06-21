using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GameStats
    {
        public static int KilledRegular
        {
            get => LocalStorage.GetInt("KilledRegular");
            set => LocalStorage.SetInt("KilledRegular",value);
        }

        public static int KilledDiver
        {
            get => LocalStorage.GetInt("KilledDiver");
            set => LocalStorage.SetInt("KilledDiver",value);
        }

        public static int KilledTank
        {
            get => LocalStorage.GetInt("KilledTank");
            set => LocalStorage.SetInt("KilledTank",value);
        }

        public static int PerkHealth
        {
            get => LocalStorage.GetInt("PerkHealth");
            set => LocalStorage.SetInt("PerkHealth",value);
        }

        public static int PerkEnergyBlast
        {
            get => LocalStorage.GetInt("PerkEnergyBlast");
            set => LocalStorage.SetInt("PerkEnergyBlast",value);
        }

        public static float DamagePlayer
        {
            get => LocalStorage.GetFloat("DamagePlayer");
            set => LocalStorage.SetFloat("DamagePlayer",value);
        }

        public static float DamagePlanet
        {
            get => LocalStorage.GetFloat("DamagePlanet");
            set => LocalStorage.SetFloat("DamagePlanet",value);
        }



    }
}
