//namespace AuroraMonitor
//{
//    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ActivateWeatherSet), new Type[] { typeof(WeatherStage) })]
//    public class WeatherTransition_ActivateWeatherSet
//    {
//        public static void Postfix(WeatherTransition __instance, ref WeatherSet reqType)
//        {
//            MessageUtilities.Instance.ShowWeatherAlert(true);
//            Utilities.SavedStage = __instance.m_CurrentWeatherSet.m_CharacterizingType;

//            if (Settings.Instance.PRINTDEBUGLOG) Logger.Log("Activated message via: WeatherTransition.ActivateWeatherSet(WeatherSet reqType)");
//        }
//    }

//    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ActivateWeatherSet), new Type[] { typeof(string) })]
//    public class WeatherTransition_ActivateWeatherSet_custom
//    {
//        public static void Postfix(WeatherTransition __instance, ref string customTypeName)
//        {
//            MessageUtilities.Instance.ShowWeatherAlert(true);
//            Utilities.SavedStage = __instance.m_CurrentWeatherSet.m_CharacterizingType;

//            if (Settings.Instance.PRINTDEBUGLOG) Logger.Log("Activated message via: WeatherTransition.ActivateWeatherSet(tring customTypeName)");
//        }
//    }

//    [HarmonyPatch(typeof(WeatherTransition), nameof(WeatherTransition.ActivateWeatherSetAtFrac), new Type[] { typeof(WeatherStage), typeof(float) })]
//    public class WeatherTransition_ActivateWeatherSetAtFrac
//    {
//        public static void Postfix(WeatherTransition __instance, ref WeatherSet reqType, ref float startAtFrac)
//        {
//            MessageUtilities.Instance.ShowWeatherAlert(true);
//            Utilities.SavedStage = __instance.m_CurrentWeatherSet.m_CharacterizingType;

//            if (Settings.Instance.PRINTDEBUGLOG) Logger.Log("Activated message via: WeatherTransition.ActivateWeatherSetAtFrac(WeatherSet reqType, float startAtFrac)");
//        }
//    }
//}
