using UnityEngine;
using System.Xml.Linq;

namespace Test_task_gameShooting
{
    public class Target : MonoBehaviour // все что универсально для всех мишеней
    {
        [SerializeField]
        public string nameTarget; // имя (универсально для подгружения связаных файлов и префаба)
        [SerializeField]
        public int points_for_reward; // сколько поинтов дает фигура

        protected HelperScript helper;
        protected AudioSource audioS;

        protected virtual void Awake()
        {
            audioS = FindObjectOfType<AudioSource>();
            helper = FindObjectOfType<HelperScript>();
        }

        protected virtual void Start()
        {
            helper.allTarget_InstObjList.Add(this); // при поевление объекта в мире, записываем его в список созданных объектов
            helper.Save();
        }

        protected virtual void Update()
        {
            if (transform.position.z < -10)
            {
                OnDes();
                helper.Save();
                Destroy(gameObject);
                // тут можно отнимать очки ели цель была не подстреляна, а упала самостоятельно
            }
        }

        protected virtual void OnDes()
        {
            helper.allTarget_InstObjList.Remove(this); // при уничтожении вычеркиваем из списка
        }

        // для сохранения
        public XElement GetElement() 
        {
            XAttribute posX = new XAttribute("posX", transform.position.x);
            XAttribute posY = new XAttribute("posY", transform.position.y);
            XAttribute posZ = new XAttribute("posZ", transform.position.z);

            XAttribute rotX = new XAttribute("rotX", transform.rotation.x);
            XAttribute rotY = new XAttribute("rotY", transform.rotation.y);
            XAttribute rotZ = new XAttribute("rotZ", transform.rotation.z);

            XElement element = new XElement("inst", nameTarget, posX, posY, posZ, rotX, rotY, rotZ);
            return element;
        }
        public XElement GetTarget_Spawn()
        {
            XAttribute point = new XAttribute("point", points_for_reward);
            XElement element = new XElement("spawn", nameTarget, point);
            return element;
        }
        //

        public void MyDestroy() // для очищения сцены перед загрузкой
        {
            OnDes();
            Destroy(gameObject);
        }
    }
}

