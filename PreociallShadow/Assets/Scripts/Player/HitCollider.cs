using UnityEditor;
using UnityEngine;

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
            Player someBobody = other.GetComponent<Player>();

            if (someBobody != null && someBobody != Owner)
            {
                HitCollider somebodyHitCollider = someBobody.GetComponent<HitCollider>();

                if (somebodyHitCollider != null)
                {
                    switch (somebodyHitCollider.PunchName)
                    {
                        case PunchName.CressentKick:

                            someBobody.CressentKickHit();
                            break;

                        case PunchName.HookPunch:

                            someBobody.HookPunchHit();
                            break;

                        case PunchName.LeftHandPunch:

                            someBobody.LeftHandPunchHit();
                            break;

                        case PunchName.RightHandPunch:

                            someBobody.RightHandPunchHit();
                            break;

                        case PunchName.LegPunch:

                            someBobody.LegPunchHit();
                            break;
                    }
                }
            }
        }
    }
}
