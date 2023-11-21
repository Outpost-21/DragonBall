﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using VFECore.Abilities;
using AbilityDef = VFECore.Abilities.AbilityDef;

namespace DragonBall
{
    public class Comp_UseEffectLearnScroll : CompUseEffect_GiveAbility
	{
		public override bool CanBeUsedBy(Pawn p, out string failReason)
		{
			if (!base.CanBeUsedBy(p, out failReason))
			{
				return false;
			}
			Hediff_KiUser kiHediff = p.health.hediffSet.GetFirstHediffOfDef(DragonBallDefOf.DragonBall_KiUser) as Hediff_KiUser;
			if (kiHediff == null)
			{
				failReason = "DragonBall.IncapableOfKiTechniques".Translate();
				return false;
			}
			if (kiHediff.level < Props.ability.requiredHediff.minimumLevel)
            {
                failReason = "DragonBall.PowerLevelTooLow".Translate();
                return false;
			}
			CompAbilities comp = p.GetComp<CompAbilities>();
			if (comp != null && comp.HasAbility(Props.ability))
			{
				failReason = "DragonBall.AlreadyKnowKiTechnique".Translate();
				return false;
			}
			failReason = null;
			return true;
		}
	}
}
