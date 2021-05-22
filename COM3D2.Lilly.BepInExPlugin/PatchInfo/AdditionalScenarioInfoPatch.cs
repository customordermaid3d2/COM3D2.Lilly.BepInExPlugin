using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin.PatchInfo
{
    class AdditionalScenarioInfoPatch
    {
        /*
        /// <summary>
        /// 분석용
        /// </summary>
        /// <param name="label"></param>
        public void Init(string label)
        {
            this.m_MyPanel = base.GetComponent<UIPanel>();
            this.m_JumpLabel = label;
            base.StartCoroutine(this.Fade(true));
            ScenarioData[] addedScenario = GameMain.Instance.ScenarioSelectMgr.AddedScenario;
            if (addedScenario.Length <= 0)
            {
                UnityEngine.Debug.Log("昨日から追加されたシナリオはもうありません");
                UnityEngine.Object.Destroy(base.gameObject);
                return;
            }
            this.m_FewVer.SetActive(false);
            this.m_ManyVer.SetActive(false);
            int num = 0;
            foreach (ScenarioData scenarioData in addedScenario)
            {
                if (!scenarioData.IsFixedMaid)
                {
                    if (scenarioData.EventMaidCount > 0)
                    {
                        foreach (Maid maid in scenarioData.GetEventMaidList())
                        {
                            num++;
                        }
                    }
                    else
                    {
                        num++;
                    }
                }
                else if (!string.IsNullOrEmpty(scenarioData.Notification))
                {
                    num++;
                }
                else if (scenarioData.EventMaidCount == 1)
                {
                    if (scenarioData.GetEventMaid(0).status.heroineType == HeroineType.Sub)
                    {
                        num++;
                    }
                    else
                    {
                        num++;
                    }
                }
                else
                {
                    num++;
                }
            }
            GameObject gameObject = (num <= this.m_ManyModeCount) ? this.m_FewVer : this.m_ManyVer;
            gameObject.SetActive(true);
            this.m_UIScroll = gameObject.transform.Find("Contents").GetComponent<UIScrollView>();
            this.m_UIGrid = gameObject.transform.Find("Contents/Grid").GetComponent<UIGrid>();
            Action<string, string, string, string> action = delegate (string title, string chara, string titleTerm, string charaTerm)
            {
                GameObject gameObject2 = Utility.CreatePrefab(this.m_UIGrid.gameObject, "SceneScenarioSelect/Prefab/Additional", true);
                UILabel component = gameObject2.transform.Find("Title").GetComponent<UILabel>();
                UILabel component2 = gameObject2.transform.Find("Chara").GetComponent<UILabel>();
                component.text = title;
                component2.text = chara;
                Utility.SetLocalizeTerm(component, titleTerm, false);
                Utility.SetLocalizeTerm(component2, charaTerm, false);
            };
            foreach (ScenarioData scenarioData2 in addedScenario)
            {
                string notLineTitle = scenarioData2.NotLineTitle;
                string titleTerm2 = scenarioData2.TitleTerm;
                string notification = scenarioData2.Notification;
                string notificationTerm = scenarioData2.NotificationTerm;
                if (!scenarioData2.IsFixedMaid)
                {
                    if (scenarioData2.EventMaidCount > 0)
                    {
                        foreach (Maid maid2 in scenarioData2.GetEventMaidList())
                        {
                            if (string.IsNullOrEmpty(notification))
                            {
                                action(notLineTitle, maid2.status.callName, titleTerm2, string.Empty);
                            }
                            else
                            {
                                action(notLineTitle, notification, titleTerm2, notificationTerm);
                            }
                        }
                    }
                    else
                    {
                        action(notLineTitle, notification, titleTerm2, notificationTerm);
                    }
                }
                else if (!string.IsNullOrEmpty(notification))
                {
                    action(notLineTitle, notification, titleTerm2, notificationTerm);
                }
                else if (scenarioData2.EventMaidCount == 1)
                {
                    if (scenarioData2.GetEventMaid(0).status.heroineType == HeroineType.Sub)
                    {
                        action(notLineTitle, "NPC", titleTerm2, "SceneScenarioSelect/通知時表記/NPC");
                    }
                    else
                    {
                        action(notLineTitle, GameMain.Instance.ScenarioSelectMgr.GetConvertPersonal(scenarioData2.GetEventMaid(0)), titleTerm2, GameMain.Instance.ScenarioSelectMgr.GetConvertPersonalTerm(scenarioData2.GetEventMaid(0)));
                    }
                }
                else
                {
                    action(notLineTitle, this.m_IdolNotice, titleTerm2, "SceneScenarioSelect/通知時表記/" + this.m_IdolNotice);
                }
            }
            if (this.m_UIGrid)
            {
                this.m_UIGrid.repositionNow = true;
            }
            if (this.m_UIScroll)
            {
                this.m_UIScroll.ResetPosition();
            }
            EventDelegate.Add(this.m_MyButton.onClick, new EventDelegate.Callback(this.FadeOut));
        }
        */
    }
}
