using UnityEngine;

namespace Test_task_gameShooting
{
    public class Bullet : MonoBehaviour
    {
        void Update()
        {
            if (transform.position.y < -10f) // что бы не нахламлялись
                Destroy(gameObject);
        }
    }
}

