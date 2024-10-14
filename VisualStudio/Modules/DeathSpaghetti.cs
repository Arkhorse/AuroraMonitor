//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AuroraMonitor.Modules
//{
//	public enum DeathSpaghettiReturnReason
//	{
//		None,
//		Complete,
//		Burnt,
//		Failed
//	}

//	[RegisterTypeInIl2Cpp]
//	public class DeathSpaghetti : MonoBehaviour
//	{
//		/// <summary>
//		/// Handles the ability to deactivate "DeathSpaghetti" using bolt cutters
//		/// </summary>
//		public DeathSpaghetti() { }
//		public DeathSpaghetti(IntPtr pointer) : base(pointer) { }

//		public bool ForceAllow { get; private set; }

//		public List<GameObject>? FoundDeathSpaghetti { get; private set; }
//		public List<GameObject>? CutDeathSpaghetti { get; private set; }

//		public GearItem? BoltCutters { get; private set; }
//		public GameObject? TempObject { get; private set; }

//		public void Start()
//		{
//			CutDeathSpaghetti ??= new();
//			FoundDeathSpaghetti ??= new();
//		}

//		public void Awake()
//		{

//		}

//		public void FixedUpdate()
//		{

//		}

//		public void SetForcedAllow(bool allow) => ForceAllow = allow;

//		/// <summary>
//		/// Called when the player attempts to cut DeathSpaghetti
//		/// </summary>
//		/// <param name="go">The DeathSpaghetti object</param>
//		/// <returns>
//		/// <para><see cref="DeathSpaghettiReturnReason.Complete"/> if everything works</para>
//		/// <para><see cref="DeathSpaghettiReturnReason.Burnt"/> if the player attempts this while an aurora is active</para>
//		/// <para><see cref="DeathSpaghettiReturnReason.Failed"/> for any other reason</para>
//		/// </returns>
//		public DeathSpaghettiReturnReason OnBeginCutDeathSpaghetti(GameObject go)
//		{
//			bool PlayerHasBoltCutters = false;
//			bool AuroraActive = WeatherUtilities.IsAuroraFullyActive(GameManager.GetAuroraManager());

//			foreach (var item in GameManager.GetInventoryComponent().m_Items)
//			{
//				GearItem gi = item;
//				if (gi == null) continue;

//				if (!string.IsNullOrWhiteSpace(gi.name) && gi.name == "GEAR_BoltCutters")
//				{
//					Main.Logger.Log(FlaggedLoggingLevel.Debug, "OnBeginCutDeathSpaghetti(): Bolt Cutters have been found in the players inventory");

//					PlayerHasBoltCutters = true;
//					break;
//				}
//			}

//			if (ForceAllow)
//			{
//				Main.Logger.Log(FlaggedLoggingLevel.Debug, "OnBeginCutDeathSpaghetti(): ForceAllow has been triggered");

//				OnCutDeathSpaghetti(go);

//				return DeathSpaghettiReturnReason.Complete;
//			}

//			if (AuroraActive)
//			{
//				Main.Logger.Log(FlaggedLoggingLevel.Debug, "OnBeginCutDeathSpaghetti(): Aurora is active, attempting to cut Death Spaghetti will not work. Burns applied");

//				GameManager.GetBurnsElectricComponent().BurnsElectricStart(true);

//				return DeathSpaghettiReturnReason.Burnt;
//			}

//			if (PlayerHasBoltCutters)
//			{
//				Main.Logger.Log(FlaggedLoggingLevel.Debug, $"OnBeginCutDeathSpaghetti(): PlayerHasBoltCutters: {PlayerHasBoltCutters}");

//				OnCutDeathSpaghetti(go);

//				return DeathSpaghettiReturnReason.Complete;
//			}

//			Main.Logger.Log(FlaggedLoggingLevel.Debug, "Player does not have Bolt Cutters");

//			return DeathSpaghettiReturnReason.Failed;
//		}

//		public bool OnCutDeathSpaghetti(GameObject DeathSpaghetti)
//		{
//			InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(
//				Localization.Get("GAMEPLAY_DeathSpaghetti_Cut"),
//				3f,
//				0f,
//				0f,
//				"",
//				null,
//				false,
//				true,
//				null);

//			CutDeathSpaghetti?.Add(DeathSpaghetti);

//			GameObject.Destroy(DeathSpaghetti);
//			return true;
//		}

//		public bool CanCut(GameObject go)
//		{
//			if (IsDeathSpaghetti(go))
//			{
//				Main.Logger.Log(FlaggedLoggingLevel.Debug, $"DeathSpaghetti: GameObject is part of the \"ElectricalHazards\" group, {go.name}");
//				FoundDeathSpaghetti?.Add(go);
//			}

//			return false;
//		}

//		public bool IsDeathSpaghetti(GameObject go, string name = "ElectricalHazards")
//		{
//			if (go.transform.root.transform.gameObject.name == name)
//			{
//				if (go.name.Contains("WireElectrical") && go.name.EndsWith("Prefab")) return true;
//			}

//			return false;
//		}
//	}
//}
