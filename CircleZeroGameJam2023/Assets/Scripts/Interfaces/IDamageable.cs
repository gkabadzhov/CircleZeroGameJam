using OTBG.Gameplay.Player.Combat.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OTBG.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(DamageData damageData);
    }
}
