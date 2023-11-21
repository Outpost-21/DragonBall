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
    public class CompProperties_UseEffectLearnScroll : CompProperties_UseEffectGiveAbility
    {
        public CompProperties_UseEffectLearnScroll()
        {
            compClass = typeof(Comp_UseEffectLearnScroll);
        }
    }
}
