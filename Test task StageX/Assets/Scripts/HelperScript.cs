using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.IO;

namespace Test_task_gameShooting
{
    public class HelperScript : MonoBehaviour
    {
        private string path_SaveDoc; // путь расположения Save-файла
        public static List<Target> allTarget_SpawnList = new List<Target>(); // для списка объектов готовых на spawn
        public List<Target> allTarget_InstObjList = new List<Target>(); // все уже созданые объекты 
        public static int myPoints = 0;
        private SpawnPanel spawn_panel;

        private void Awake()
        {
            path_SaveDoc = Application.persistentDataPath + "/save.xml";
            spawn_panel = FindObjectOfType<SpawnPanel>();
            Debug.Log("File is saved by: " + path_SaveDoc);
            if (File.Exists(path_SaveDoc)) Load(); // если Save-файл есть, грузим его
            else Save(); // else создадим Save-файл
        }
        public void Save()
        {
            XElement save = new XElement("save");

            foreach (Target obj in allTarget_InstObjList)
                save.Add(obj.GetElement());
            foreach (Target obj in allTarget_SpawnList)
                save.Add(obj.GetTarget_Spawn());
            save.AddFirst(new XElement("points", myPoints));

            XDocument saveDoc = new XDocument(save);

            File.WriteAllText(path_SaveDoc, saveDoc.ToString());
            //Debug.Log("Save complite!");

            spawn_panel.UpdateSpawnItem(); // при каждом сохранении обновляем меню
        }
        public void Load()
        {
            XElement save = null;
            if (File.Exists(path_SaveDoc))
                save = XDocument.Parse(File.ReadAllText(path_SaveDoc)).Element("save");

            if (save == null)
            {
                Debug.Log("Save not found...");
                return;
            }

            LoadObject(save);

            spawn_panel.UpdateSpawnItem();
        }
        private void LoadObject(XElement save)
        {
            foreach (Target targ in allTarget_InstObjList) 
                targ.MyDestroy();

            allTarget_SpawnList.Clear();

            foreach (XElement inst in save.Elements("inst"))
            {
                Vector3 position = Vector3.zero;
                position.x = float.Parse(inst.Attribute("posX").Value);
                position.y = float.Parse(inst.Attribute("posY").Value);
                position.z = float.Parse(inst.Attribute("posZ").Value);

                Quaternion rotation = Quaternion.identity;
                rotation.x = float.Parse(inst.Attribute("rotX").Value);
                rotation.y = float.Parse(inst.Attribute("rotY").Value);
                rotation.z = float.Parse(inst.Attribute("rotZ").Value);

                Instantiate(Resources.Load<GameObject>("Targets/Prefabs/" + inst.Value), position, rotation);
            }

            foreach (XElement spawn in save.Elements("spawn"))
                allTarget_SpawnList.Add(Resources.Load<GameObject>("Targets/Prefabs/" + spawn.Value).GetComponent<Target>());

            myPoints = int.Parse(save.Element("points").Value);
        }
    }
}