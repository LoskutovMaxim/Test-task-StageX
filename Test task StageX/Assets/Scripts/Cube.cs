using System.Collections;
using UnityEngine;

namespace Test_task_gameShooting
{
    public class Cube : Target
    {
        public void OnTriggerEnter(Collider other)
        {
            StartCoroutine(Hitting_Cube(other.gameObject)); // триггер только на пульке, так что при попадании запускаем процесс разрушения объекта
        }

        public IEnumerator Hitting_Cube(GameObject bullet)
        {
            OnDes(); // выводи объект из списка
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetComponent<Rigidbody>().isKinematic = true; // что бы оболочка префаба не мешала
            transform.GetChild(0).gameObject.SetActive(true);
            audioS.Play(); // доуп-звук
            yield return new WaitForSeconds(0.01f);
            transform.GetChild(1).gameObject.SetActive(false); // основное тело префаба отключаем немного позже, что б оскольки разлетались в стороны 
            HelperScript.myPoints += points_for_reward; // добавляем заданое количество point-оф
            helper.Save(); 
            yield return new WaitForSeconds(4f);
            Destroy(bullet);
            Destroy(gameObject);
        }
    }
}

