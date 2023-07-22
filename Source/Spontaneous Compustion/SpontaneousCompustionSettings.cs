using RimWorld;
using UnityEngine;
using Verse;

namespace Spontaneous_Compustion
{
    public class SpontaneousCompustionSettings : ModSettings
    {
        public static bool letterFire = true;
        public static bool letterExplode = false;
        public override void ExposeData()
        {
            Scribe_Values.Look(ref letterFire, "letterFire");
            Scribe_Values.Look(ref letterExplode, "letterExplode");
            base.ExposeData();
        }
    }
    public class SpontaneousCompustionMod : Mod
    {
        public SpontaneousCompustionMod(ModContentPack content) : base(content)
        {
            GetSettings<SpontaneousCompustionSettings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Label("BBLK_SpontaneousCompustion_Info".Translate());
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCompustionLabelLetterFire".Translate(), ref SpontaneousCompustionSettings.letterFire, "BBLK_SpontaneousCompustionLetterToolTipFire".Translate());
            listingStandard.CheckboxLabeled("BBLK_SpontaneousCompustionLabelLetterExlode".Translate(), ref SpontaneousCompustionSettings.letterExplode, "BBLK_SpontaneousCompustionLetterToolTipExlode".Translate());
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory() => "BBLK_SpontaneousCompustion_Settings".Translate();
    }
}
