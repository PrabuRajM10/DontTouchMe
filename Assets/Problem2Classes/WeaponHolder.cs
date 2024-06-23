using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Problem2
{
    public enum WeaponId
    {
        PS1,
        PS2,
        AR1,
        AR2,
        Sng1,
        Lmg1,
        SG1
    }
    [CreateAssetMenu(menuName = "WeaponsHolder", fileName = "Create/WeaponHolder", order = 0)]
    public class WeaponHolder : ScriptableObject
    {
        [SerializeField] private List<Weapon> weaponList = new List<Weapon>();

        public Weapon GetWeaponByWeaponId(WeaponId weaponId)
        {
            return weaponList.FirstOrDefault(weapon => weapon.WeaponId == weaponId);
        }

    }
}