using FMODUnity;
using UnityEngine;
using UnityEngine.VFX;
using static DamageUtil;

public class TickingExplosion : TickingAttack
{
    [SerializeField]
    private float size;

    [SerializeField]
    private bool isRanged;

    [SerializeField]
    private bool damageOnStartup;

    [SerializeField]
    private VisualEffect effect;

    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private string soundEvent;

    [SerializeField]
    private bool inflictStun;

    [SerializeField] private GameObject spawnOnFinish;
    
    private bool initialising;

    protected override void Start()
    {
        base.Start();
        Vector3 s = visuals.transform.localScale;
        visuals.transform.localScale = Vector3.zero;
        LeanTween.scale(visuals, s, 0.5f).setEaseOutBack().setOvershoot(0.5f);
        initialising = true;
        if(damageOnStartup) {
            Explode();
        }
        initialising = false;
    }

    protected override bool Explode()
    {
        RuntimeManager.PlayOneShot(soundEvent, GetComponent<Transform>().position);

        Collider[] objects = Physics.OverlapSphere(transform.position, size / 2f);

        foreach (var obj in objects)
        {
            DealDamage(obj, playerId, damage, isRanged, inflictStun);
        }

        if (!initialising)
        {
            LeanTween.scale(visuals, Vector3.zero, 0.5f).setOnComplete(() => 
            {
                effect.SendEvent("Burst");
                Destroy(gameObject, 5f);
            }).setEaseInBack().setOvershoot(0.5f);

            if (spawnOnFinish != null) {
                Instantiate(spawnOnFinish, transform.position, transform.rotation);
            }
        }
        else
        {
            effect.SendEvent("Burst");
        }
        return true;
    }
}
