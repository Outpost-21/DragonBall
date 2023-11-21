using RimWorld;
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
    public class Comp_AbilityScroll : CompUsable
	{
		public AbilityDef Ability => parent.GetComp<Comp_UseEffectLearnScroll>().Props.ability;

		public override void PostExposeData()
		{
			base.PostExposeData();
		}

		public override void Initialize(CompProperties props)
		{
			base.Initialize(props);
		}

        public override string FloatMenuOptionLabel(Pawn pawn)
        {
            return string.Format(base.Props.useLabel, Ability.label);
        }

        public override bool AllowStackWith(Thing other)
		{
			if (!base.AllowStackWith(other))
			{
				return false;
			}
			Comp_AbilityScroll compScroll = other.TryGetComp<Comp_AbilityScroll>();
			if (compScroll == null || compScroll.Ability != Ability)
			{
				return false;
			}
			return true;
		}

		public override void PostSplitOff(Thing piece)
		{
			base.PostSplitOff(piece);
		}
	}
}
