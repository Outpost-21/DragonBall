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
    public class Hediff_KiUser : Hediff_Abilities
    {
        public const float lvlConstant = 0.1f;
        public const float lvlPower = 2f;

        public float experienceInt = 0f;
        public float experienceLastTick = -1f;
        public int levelInt = 0;

        public float kiEnergy;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref kiEnergy, "kiEnergy");
            Scribe_Values.Look(ref experienceInt, "experienceInt");
            Scribe_Values.Look(ref experienceLastTick, "experienceLastTick");
            Scribe_Values.Look(ref levelInt, "levelInt");
        }

        public float PercentToNextLevel => Mathf.Clamp(((experienceInt - ExpForCurrentLevel) / TotalExpForNextLevel), 0f, 1f);

        public float ExpForNextLevel => TotalExpForNextLevel - ExpForCurrentLevel;

        public float TotalExpForNextLevel => ExpForGivenLevel(levelInt + 1);

        public float ExpForCurrentLevel => ExpForGivenLevel(levelInt);

        public int PowerLevel => (level * 1000) + (Mathf.RoundToInt(Mathf.Lerp(0, 999, PercentToNextLevel)));

        public float ExpForGivenLevel(int lvl)
        {
            if (lvl < 1) { return 0f; }
            return Mathf.Pow(lvl / lvlConstant, lvlPower);
        }

        public int LevelFromGivenExp(float exp)
        {
            if (exp < ExpForGivenLevel(1)) { return 1; }
            return Mathf.FloorToInt(lvlConstant * Mathf.Pow(exp, 1 / lvlPower));
        }

        public override void Tick()
        {
            base.Tick();
            kiEnergy += pawn.GetStatValue(DragonBallDefOf.DragonBall_KiEnergyRecoveryRate) / 60f;
            kiEnergy = Mathf.Min(kiEnergy, pawn.GetStatValue(DragonBallDefOf.DragonBall_KiEnergyMax));
            if (experienceLastTick != experienceInt) { CalcLevel(); }
            experienceLastTick = experienceInt;
        }

        public override string Label => "DragonBall.KiUser".Translate(PowerLevel);

        public override void PostAdd(DamageInfo? dinfo)
        {
            base.PostAdd(dinfo);
            kiEnergy = pawn.GetStatValue(DragonBallDefOf.DragonBall_KiEnergyMax);
        }

        public void UseEnergy(float energyUsed)
        {
            kiEnergy -= energyUsed;
            kiEnergy = Mathf.Min(kiEnergy, pawn.GetStatValue(DragonBallDefOf.DragonBall_KiEnergyMax));
        }

        public bool SufficientEnergyPresent(float energyWanted)
        {
            return kiEnergy >= energyWanted;
        }

        public void CalcLevel()
        {
            levelInt = LevelFromGivenExp(experienceInt);
            if (level != levelInt) { SetLevelTo(levelInt); }
        }

        public void ExpGain(float xp) { experienceInt += xp; }

        public override void GiveRandomAbilityAtLevel(int? forLevel = null)
        {
            if (forLevel > 1) { return; }
            GiveRandomAbilityAtLevelForClass(forLevel);
        }

        public override IEnumerable<Gizmo> DrawGizmos()
        {
            yield return new Gizmo_KiEnergyStatus
            {
                kiHediff = this
            };
        }

        public void GiveRandomAbilityAtLevelForClass(int? forLevel = null)
        {
            if (giveRandomAbilities)
            {
                CompAbilities comp = pawn.GetComp<CompAbilities>();
                List<AbilityDef> abilities = GetAbilitiesForLevel(comp, forLevel ?? 0).ToList();
                if (!abilities.NullOrEmpty()) { comp.GiveAbility(abilities.RandomElement()); }
            }
        }

        public IEnumerable<AbilityDef> GetAbilitiesForLevel(CompAbilities comp, int forLevel = 0)
        {
            foreach(AbilityDef ab in DefDatabase<AbilityDef>.AllDefs)
            {
                if (ab.HasModExtension<AbilityExtension_Ki>() && !comp.LearnedAbilities.Any(a => a.def == ab) && ab.requiredHediff.minimumLevel <= level)
                {
                    yield return ab;
                }
            }
            yield break;
        }
    }
}
