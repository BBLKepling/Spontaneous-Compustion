using RimWorld;
using System.Collections.Generic;
using Verse;

namespace Spontaneous_Compustion
{
    public static class SpontaneousCompustionUtility
    {
        public static IEnumerable<Thing> GetCombustables(Map map)
        {
            if (!SpontaneousCompustionSettings.letterFire && !SpontaneousCompustionSettings.letterExplode)
            {
                yield break;
            }
            List<Thing> combustables = map.listerThings.AllThings;
            for (int i = 0; i < combustables.Count; i++)
            {
                if (CanEverCombust(combustables[i]))
                {
                    yield return combustables[i];
                }
            }
        }
        public static bool TryCombust(Thing culprit)
        {
            if (SpontaneousCompustionSettings.letterFire && SpontaneousCompustionSettings.letterExplode)
            {
                GenExplosion.DoExplosion(
                    radius: 5.9f,
                    center: culprit.Position,
                    map: culprit.Map,
                    damType: DamageDefOf.Flame,
                    instigator: culprit);
                Find.LetterStack.ReceiveLetter("BBLK_SpontaneousCompustion_LetterLabel".Translate(), "BBLK_SpontaneousCompustion_LetterText".Translate(culprit.Label, culprit.Named("CULPRIT")), LetterDefOf.NegativeEvent, new TargetInfo(culprit.Position, culprit.Map));
                return true;
            }
            if (SpontaneousCompustionSettings.letterFire)
            {
                if (culprit.CanEverAttachFire())
                {
                    culprit.TryAttachFire(Rand.Range(0.5f, 1f));
                }
                else
                {
                    FireUtility.TryStartFireIn(culprit.Position, culprit.Map, Rand.Range(0.5f, 1f));
                }
                Find.LetterStack.ReceiveLetter("BBLK_SpontaneousCompustion_LetterLabel".Translate(), "BBLK_SpontaneousCompustion_LetterText".Translate(culprit.Label, culprit.Named("CULPRIT")), LetterDefOf.NegativeEvent, new TargetInfo(culprit.Position, culprit.Map));
                return true;
            }
            if (SpontaneousCompustionSettings.letterExplode)
            {
                GenExplosion.DoExplosion(
                    radius: 3.9f,
                    center: culprit.Position,
                    map: culprit.Map,
                    damType: DamageDefOf.Bomb,
                    instigator: culprit);
                Find.LetterStack.ReceiveLetter("BBLK_SpontaneousCompustion_LetterLabel".Translate(), "BBLK_SpontaneousCompustion_LetterText".Translate(culprit.Label, culprit.Named("CULPRIT")), LetterDefOf.NegativeEvent, new TargetInfo(culprit.Position, culprit.Map));
                return true;
            }
            return false;
        }
        public static bool CanEverCombust(Thing t)
        {
            if (t.Destroyed) return false;
            if (!t.FlammableNow) return false;
            return true;
        }
    }
}
