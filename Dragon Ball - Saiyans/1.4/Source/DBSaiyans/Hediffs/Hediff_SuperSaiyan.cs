using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace DBSaiyans
{
    public class Hediff_SuperSaiyan : HediffWithComps
    {
        public Color? original;

        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            TurnHair();
            pawn.drawer.renderer.graphics.ResolveAllGraphics();
        }

        public void TurnHair()
        {
            if (original == null)
            {
                original = pawn.story.HairColor;
            }
            pawn.story.HairColor = Color.yellow;
        }

        public override void PostRemoved()
        {
            base.PostRemoved();
            RestoreHair();
            pawn.drawer.renderer.graphics.ResolveAllGraphics();
        }

        public void RestoreHair()
        {
            if (original != null)
            {
                pawn.story.HairColor = (Color)original;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref original, "original");
        }
    }
}
