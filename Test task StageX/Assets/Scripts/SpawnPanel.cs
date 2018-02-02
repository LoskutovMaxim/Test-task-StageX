using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Test_task_gameShooting
{
    public class SpawnPanel : MonoBehaviour
    {
        public GameObject panel_List, panel_exemplarList_prefab; // для списка Spawn

        protected HelperScript helper;
        private void Awake()
        {
            helper = FindObjectOfType<HelperScript>();
        }

        public void CriateTarget() // добавляем новую случейную ячейку списка Spawn
        {
            string nameX = "CubeB";
            int rnd = Random.Range(0, 4);
            if (rnd == 1) nameX = "CubeG";
            else if (rnd == 2) nameX = "SphereR";
            else if (rnd == 3) nameX = "SphereY";
            HelperScript.allTarget_SpawnList.Add(Resources.Load<GameObject>("Targets/Prefabs/" + nameX).GetComponent<Target>());
            helper.Save();
        }

        public void UpdateSpawnItem() // для обновления меню
        {
            transform.GetChild(0).GetComponent<Text>().text = "Game points\n" + HelperScript.myPoints;

            if (HelperScript.allTarget_SpawnList.Count > panel_List.transform.childCount) // если по факту меньше обектов чем должно быть
            {
                for (int i = 0; i < HelperScript.allTarget_SpawnList.Count; i++)
                {
                    if (i < panel_List.transform.childCount) // сначала обновляем уже созданные
                    {
                        panel_List.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Targets/Image/" + HelperScript.allTarget_SpawnList[i].nameTarget);
                        panel_List.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = HelperScript.allTarget_SpawnList[i].nameTarget + " / " + HelperScript.allTarget_SpawnList[i].points_for_reward;
                    }
                    else // и досоздаем едостоющее
                    {
                        GameObject exemplarList = Instantiate(panel_exemplarList_prefab) as GameObject;
                        exemplarList.transform.SetParent(panel_List.transform);
                        exemplarList.transform.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        exemplarList.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Targets/Image/" + HelperScript.allTarget_SpawnList[i].nameTarget);
                        exemplarList.transform.GetChild(1).GetComponent<Text>().text = HelperScript.allTarget_SpawnList[i].nameTarget + " / " + HelperScript.allTarget_SpawnList[i].points_for_reward;
                    }
                }
            }
            else // на оборот все
            {
                for (int i = 0; i < panel_List.transform.childCount; i++)
                {
                    if (i < HelperScript.allTarget_SpawnList.Count)
                    {
                        panel_List.transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Targets/Image/" + HelperScript.allTarget_SpawnList[i].nameTarget);
                        panel_List.transform.GetChild(i).transform.GetChild(1).GetComponent<Text>().text = HelperScript.allTarget_SpawnList[i].nameTarget + " / " + HelperScript.allTarget_SpawnList[i].points_for_reward;
                    }
                    else
                        Destroy(panel_List.transform.GetChild(i).gameObject);
                }
            }
        }

        public void ResetAll() // button
        {
            foreach (Target targ in helper.allTarget_InstObjList.ToList())
                targ.MyDestroy();
            HelperScript.allTarget_SpawnList.Clear();
            HelperScript.myPoints = 0;

            helper.Save();
        }

        public void Exit() // button
        {
            Application.Quit();
        }
    }
}

