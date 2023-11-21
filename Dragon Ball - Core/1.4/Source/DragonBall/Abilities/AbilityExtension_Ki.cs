using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using VFECore.Abilities;
using Ability = VFECore.Abilities.Ability;
using AbilityDef = VFECore.Abilities.AbilityDef;

namespace DragonBall
{
    public class AbilityExtension_Ki : AbilityExtension_AbilityMod
    {
        public int experience = 0;

        public float energyUsed = 0f;
        public float GetEnergyUsedByPawn(Pawn pawn) => energyUsed * pawn.GetStatValue(DragonBallDefOf.DragonBall_KiAbilityCostMultiplier);

        public override bool IsEnabledForPawn(Ability ability, out string reason)
        {
            reason = "DragonBall.AbilityDisableReasonEnergyLack".Translate();
            return ability.Hediff != null && ((Hediff_KiUser)ability.Hediff).SufficientEnergyPresent(GetEnergyUsedByPawn(ability.pawn));
        }

        public override void Cast(GlobalTargetInfo[] targets, Ability ability)
        {
            base.Cast(targets, ability);
            Hediff_KiUser kiUser = (Hediff_KiUser)ability.pawn.health.hediffSet.GetFirstHediffOfDef(DragonBallDefOf.DragonBall_KiUser);
            kiUser.UseEnergy(GetEnergyUsedByPawn(ability.pawn));
            kiUser.ExpGain(experience);
        }

        public override string GetDescription(Ability ability) =>
            $"{"DragonBall.AbilityStatsKiEnergy".Translate()}: {GetEnergyUsedByPawn(ability.pawn)}".Colorize(Color.cyan);
    }
}
