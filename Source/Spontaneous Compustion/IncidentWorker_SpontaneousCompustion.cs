using RimWorld;
using System.Linq;
using Verse;

namespace Spontaneous_Compustion
{
    public class IncidentWorker_SpontaneousCompustion : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            return SpontaneousCompustionUtility.GetCombustables((Map)parms.target).Any();
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            if (!SpontaneousCompustionUtility.GetCombustables((Map)parms.target).TryRandomElement(out var result))
            {
                return false;
            }
            return SpontaneousCompustionUtility.TryCombust(result);
        }
    }
}
