using System.Collections;
using UnityEngine;

namespace Test_task_gameShooting
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameObject bullet; // префаб снаряда
        [SerializeField]
        private float forse = 1; // как сильно запускаем снаряд 
        private bool ready_to_shoot = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) // выпуск снарядов
                StartCoroutine(Shot());
        }

        private IEnumerator Shot() 
        {
            if (ready_to_shoot) // что б не спамить, а стрелять с задержкой
            {
                ready_to_shoot = false;
                Ray ray = gameObject.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                GameObject newBullet = Instantiate(bullet, gameObject.transform.position, Quaternion.identity) as GameObject;
                newBullet.transform.GetComponent<Rigidbody>().AddForce(ray.direction * forse, ForceMode.Impulse);
                yield return new WaitForSeconds(1f);
                ready_to_shoot = true;
            }
        }
    }
}

