﻿namespace AuroraMonitor.Utilities
{
    public class SceneUtilities
    {
        public static bool IsSceneEmpty(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.Contains("Empty", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static bool IsSceneBoot(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.Contains("Boot", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool IsSceneMenu(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Used to check if the current scene is a base scene (Zone or Region)
        /// </summary>
        /// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
        /// <returns></returns>
        public static bool IsSceneBase(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            bool RegionOrZone = sceneName.Contains("Region", StringComparison.InvariantCultureIgnoreCase) || sceneName.Contains("Zone", StringComparison.InvariantCultureIgnoreCase);

            return RegionOrZone;
        }

        public static bool IsSceneSandbox(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.Contains("SANDBOX", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool IsSceneDLC01(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.Contains("DLC01", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool IsSceneDarkWalker(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName.Contains("DARKWALKER", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Used to check if the current scene is an additive scene, like sandbox or DLC scenes added to the base scene
        /// </summary>
        /// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
        /// <returns></returns>
        public static bool IsSceneAdditive(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (IsSceneSandbox(sceneName) || IsSceneDLC01(sceneName) || IsSceneDarkWalker(sceneName))
            {
                return false;
            }

            return true;
        }

        public static bool IsScenePlayable(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (IsSceneEmpty(sceneName) || IsSceneBoot(sceneName) || IsSceneMenu(sceneName))
            {
                return false;
            }

            return true;
        }

        public static bool IsSceneIndoorEnvironment(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName != null && !IsSceneEmpty(sceneName) && !IsSceneBoot(sceneName) && !IsSceneMenu(sceneName))
            {
                if (GameManager.GetWeatherComponent().IsIndoorEnvironment())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsSceneIndoor(string? sceneName = null)
        {
            sceneName ??= GameManager.m_ActiveScene;

            if (sceneName != null && !IsSceneEmpty(sceneName) && !IsSceneBoot(sceneName) && !IsSceneMenu(sceneName))
            {
                if (!IsSceneIndoorEnvironment(sceneName) && GameManager.GetWeatherComponent().IsIndoorScene()) return true;
            }

            return false;
        }

        public static bool IsValidSceneForWeather(string sceneName, bool IndoorOverride)
        {
            bool one    = (IsSceneBase(sceneName));
            bool two    = !(IsSceneAdditive(sceneName));
            bool three  = GameManager.GetWeatherComponent().IsIndoorScene();
            bool four   = three && IndoorOverride;

            return one && two && (three || four);
        }
    }
}
