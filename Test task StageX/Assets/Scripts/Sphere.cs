using System.Collections;
using UnityEngine;

namespace Test_task_gameShooting
{
    public class Sphere : Target
    {
        public void OnTriggerEnter(Collider other)
        {
            transform.GetComponent<Animator>().SetBool("destroy", true); // анимация исчезновения 
            audioS.Play(); // доуп-звук
            StartCoroutine(Hitting_Sphere(other.gameObject)); // триггер только на пульке, так что при попадании запускаем процесс разрушения объекта
        }

        public IEnumerator Hitting_Sphere(GameObject bullet)
        {
            OnDes(); // выводи объект из списка
            HelperScript.myPoints += points_for_reward; // добавляем заданое количество point-оф
            helper.Save();
            yield return new WaitForSeconds(4f);
            Destroy(bullet);
            Destroy(gameObject);
        }
    }
}

