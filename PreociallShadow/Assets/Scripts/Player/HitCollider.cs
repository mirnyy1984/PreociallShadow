﻿using UnityEngine;

namespace Assets.Scripts.Player
{
    public class HitCollider : MonoBehaviour
    {
        public Player Owner;
        public PlayerBodyPart BodyPart;
        public PunchName PunchName;
        public float Damage;

        private void OnTriggerEnter(Collider other)
        {
            if (Owner.IsAtacking)
            {
                Player someBobody = other.GetComponent<Player>();

                if (someBobody != null && someBobody != Owner)
                {
                   someBobody.Heart(PunchName);
                }
            }
        }
    }
}
