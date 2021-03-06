using SweetMoleHouse.MarioForever.Scripts.Constants;
using UnityEngine;

namespace SweetMoleHouse.MarioForever.Scripts.Enemy.Goomba
{
    public class GoombaDeathHandler : MonoBehaviour
    {
        [SerializeField] private GameObject goombaPhoto;

        private DamageReceiver dr;
        private void Start()
        {
            dr = GetComponent<DamageReceiver>();
            dr.OnGenerateCorpse += OnDeath;
            
            goombaPhoto = Instantiate(goombaPhoto, dr.Host.parent);
            goombaPhoto.SetActive(false);
        }

        private ActionResult OnDeath(EnumDamageType type)
        {
            if (type != EnumDamageType.STOMP) return ActionResult.PASS;
            
            goombaPhoto.SetActive(true);
            goombaPhoto.transform.position = dr.Host.position;
            if (goombaPhoto.TryGetComponent(out Corpse corpse))
            {
                corpse.AcceptBody(dr.Renderer);
            }
            
            return ActionResult.CANCEL;
        }
    }
}
