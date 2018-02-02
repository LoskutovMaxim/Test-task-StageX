using System.Collections;
using UnityEngine;

namespace Test_task_gameShooting
{
    public class Splinters : MonoBehaviour
    {
        private float rnd;

        void Start() // задаем случайное число, что б частички исчезали с разной задержкой
        {
            rnd = Random.Range(2, 4f);
            StartCoroutine(Dest());
        }

        IEnumerator Dest()
        {
            yield return new WaitForSeconds(rnd); // <<==
            Destroy(gameObject);
        }
    }
}

