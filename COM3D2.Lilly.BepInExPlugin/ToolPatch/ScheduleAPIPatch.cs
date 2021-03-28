using HarmonyLib;
using MaidStatus;
using PlayerStatus;
using Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wf;

namespace COM3D2.Lilly.Plugin
{
    class ScheduleAPIPatch
    {
		// ScheduleAPI

		// public static bool VisibleNightWork(int workId, Maid maid = null, bool checkFinish = true)
		// public static bool EnableNightWork(int workId, Maid maid = null, bool calledTargetCheck = true, bool withMaid = true)
		// public static bool EnableNoonWork(int workId, Maid maid = null)
		/// <summary>
		/// 스케즐 버튼 듸우기?
		/// 모든 밤시중이 활성화 되버려서 이방법 위험
		/// ScheduleCSVData.GetNightTravelWorkId:夜の旅行仕事IDが発見できませんでした。
		/// 발생
		/// </summary>
		/// <param name="__result"></param>
		/// <param name="workId"></param>
		/// <param name="maid"></param> null 잇을수 있음
		/// <returns></returns>
		[HarmonyPatch(typeof(ScheduleAPI), "VisibleNightWork") ]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        [HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
        public static void VisibleNightWork(out bool __result, int workId, Maid maid)
        {
			//if (SceneFreeModeSelectManager.IsFreeMode)
            {
				__result= true;
				/*
				MyLog.LogMessage("VisibleNightWork:" + SceneFreeModeSelectManager.IsFreeMode
				, workId
				, ScheduleCSVData.AllData[workId].name
				, maid != null ? MaidUtill.GetMaidFullNale(maid) : "");
				//return false;
				*/
			}
			//return true; // SceneFreeModeSelectManager.IsFreeMode;
        }		
		
		/// <summary>
		/// 스케줄 등록시
		/// </summary>
		/// <param name="__result"></param>
		/// <param name="workId"></param>
		/// <param name="maid"></param>
		/// <returns></returns>
        [HarmonyPatch(typeof(ScheduleAPI), "EnableNightWork")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
		[HarmonyPostfix]//HarmonyPostfix ,HarmonyPrefix
		public static void EnableNightWork(out bool __result, int workId, Maid maid)
        {
			//if (SceneFreeModeSelectManager.IsFreeMode)
			{
				__result = true;
				/*
				if (Lilly.isLogOnOffAll)
					MyLog.LogMessage("EnableNightWork:" + SceneFreeModeSelectManager.IsFreeMode
				, workId
				, ScheduleCSVData.AllData[workId].name
				, maid != null ? MaidUtill.GetMaidFullNale(maid) : "");
				//return false;
				*/
			}
			//return true;
			/*
			ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[workId];
			foreach (KeyValuePair<int, int> keyValuePair in yotogi.condSkill)
			{
				if (!maid.status.yotogiSkill.Contains(keyValuePair.Key))
				{
					__result= false;
					//return false;
					//break;
				}
			}

			*/

			//return false; // SceneFreeModeSelectManager.IsFreeMode;
        }
		        
		//[HarmonyPatch(typeof(ScheduleAPI), "EnableNoonWork")]
        //[HarmonyPrefix]//HarmonyPostfix ,HarmonyPrefix
        public static void EnableNoonWork( out bool __result, int workId, Maid maid)
        {
            __result = true;
			/*
			if (Lilly.isLogOnOffAll)
				MyLog.LogMessage("EnableNoonWork:" + SceneFreeModeSelectManager.IsFreeMode
				, workId
				, ScheduleCSVData.AllData[workId].name
				, maid != null ? MaidUtill.GetMaidFullNale(maid) : "" );
			*/
			//return false; // SceneFreeModeSelectManager.IsFreeMode;
        }

		/// <summary>
		/// 분석용
		/// </summary>
		/// <param name="workId"></param>
		/// <param name="maid"></param>
		/// <param name="checkFinish"></param>
		/// <returns></returns>
		public static bool VisibleNightWork(int workId, Maid maid , bool checkFinish )
		{
			ScheduleCSVData.Yotogi yotogi = ScheduleCSVData.YotogiData[workId];
			switch (yotogi.yotogiType)
			{
				case ScheduleCSVData.YotogiType.Vip:
				case ScheduleCSVData.YotogiType.VipCall:
					{
						NightWorkState nightWorksState = GameMain.Instance.CharacterMgr.status.GetNightWorksState(workId);
						//if (nightWorksState == null)
						{
							//return false;
						}
						//nightWorksState.finish = true;
						//if (checkFinish && nightWorksState.finish)
						{
							//if (DailyMgr.IsLegacy)
							{
								//return false;
							}
							//if (GameMain.Instance.CharacterMgr.status.clubGrade < 5)
							{
								//return false;
							}
							//ScheduleCSVData.vipFullOpenDay = 0;
							//if (GameMain.Instance.CharacterMgr.status.days < ScheduleCSVData.vipFullOpenDay)
							{
								//return false;
							}
						}
						break;
					}
				case ScheduleCSVData.YotogiType.Travel:
					//return false;
					break;
				case ScheduleCSVData.YotogiType.EasyYotogi:
					{
						//if (yotogi.easyYotogi == null)
						{
							//return false;
						}
						//int trophyId = yotogi.easyYotogi.trophyId;
						//if (!GameMain.Instance.CharacterMgr.status.IsHaveTrophy(trophyId))
						{
							//return false;
						}
						break;
					}
			}
			if (yotogi.condPackage.Count > 0)
			{
				for (int i = 0; i < ScheduleCSVData.YotogiData[workId].condPackage.Count; i++)
				{
					if (!PluginData.IsEnabled(ScheduleCSVData.YotogiData[workId].condPackage[i]))
					{
						return false;
					}
				}
			}
			if (yotogi.condManVisibleFlag1.Count > 0)
			{
				for (int j = 0; j < yotogi.condManVisibleFlag1.Count; j++)
				{
					if (GameMain.Instance.CharacterMgr.status.GetFlag(yotogi.condManVisibleFlag1[j]) < 1)
					{
						return false;
					}
				}
			}
			if (maid != null)
			{
				if (ScheduleCSVData.YotogiData[workId].condMainChara && !maid.status.mainChara)
				{
					return false;
				}
				if (yotogi.condPersonal.Count > 0)
				{
					bool flag = false;
					for (int k = 0; k < yotogi.condPersonal.Count; k++)
					{
						if (maid.status.personal.id == yotogi.condPersonal[k])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (yotogi.subMaidUnipueName != string.Empty)
				{
					if (maid.status.heroineType != HeroineType.Sub)
					{
						return false;
					}
					if (yotogi.subMaidUnipueName != maid.status.subCharaData.uniqueName)
					{
						return false;
					}
				}
				else if (maid.status.heroineType == HeroineType.Sub)
				{
					return false;
				}
			}
			return true;
		}



	}
}
