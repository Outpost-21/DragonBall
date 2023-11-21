using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace DragonBall
{
    [DefOf]
    public static class DragonBallDefOf
    {
        static DragonBallDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(DragonBallDefOf));
        }

        public static HediffDef DragonBall_KiUser;

        public static StatDef DragonBall_KiAbilityCostMultiplier;
        public static StatDef DragonBall_KiEnergyMax;
        public static StatDef DragonBall_KiEnergyRecoveryRate;
    }
}
