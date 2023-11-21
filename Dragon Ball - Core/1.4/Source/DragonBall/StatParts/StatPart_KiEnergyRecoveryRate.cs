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
    public class StatPart_KiEnergyRecoveryRate : StatPart
    {
        public const float baseValue = 0.01f;
        public const float perLevel = 0.01f;

        public override void TransformValue(StatRequest req, ref float val)
        {
            Pawn pawn = req.Pawn;
            if (pawn != null)
            {
                Hediff_KiUser hediff = (Hediff_KiUser)pawn.health.hediffSet.GetFirstHediffOfDef(DragonBallDefOf.DragonBall_KiUser);
                if (hediff != null)
                {
                    val = baseValue + (hediff.level * perLevel);
                }
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            Pawn pawn = req.Thing as Pawn;
            if (pawn != null)
            {
                Hediff_KiUser hediff = (Hediff_KiUser)pawn.health.hediffSet.GetFirstHediffOfDef(DragonBallDefOf.DragonBall_KiUser);
                if (hediff != null)
                {
                    StringBuilder report = new StringBuilder();
                    report.Append("DragonBall.KiRecoveryBase_StatReport".Translate() + ": " + baseValue);
                    report.Append("\n" + "DragonBall.KiRecoveryLevel_StatReport".Translate() + ": +" + (hediff.level * perLevel));
                    return report.ToString();
                }
            }
            return null;
        }
    }
}
