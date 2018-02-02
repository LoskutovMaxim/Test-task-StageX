using UnityEngine;

namespace Test_task_gameShooting
{
    public class ButtonSpawn : SpawnPanel
    {
        private Vector3 instPos = new Vector3(0, 2f, 6.54f); // позиция по центру на д платформой
        
        private void Start()
        {
            panel_List = transform.parent.gameObject;
        }
        public void ReturnTargetInWorld()
        {
            for (int i = 0; i < panel_List.transform.childCount; i++)
            {
                if (panel_List.transform.GetChild(i).gameObject == gameObject) // определяем индекс для последуйщей загрузки
                {
                    Instantiate(Resources.Load<GameObject>("Targets/Prefabs/" + HelperScript.allTarget_SpawnList[i].nameTarget), instPos, Quaternion.identity);
                    HelperScript.allTarget_SpawnList.RemoveAt(i); // и вычеркиваем из списка
                    break;
                }
            }
        }
    }
}

