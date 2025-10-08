using UnityEngine;

namespace TowerDefense.Enemies
{
    public class Creep : Enemy
    {
        protected override void Update()
        {
            base.Update();
            
            if (_hitPoints > 0)
                Move();
        }

        protected override void Move()
        {
            transform.LookAt(_targetPosition, Vector3.up);
        
            var direction = _targetPosition - transform.position;
            transform.position += direction.normalized * (_speed * Time.deltaTime);
        }
    }
}
