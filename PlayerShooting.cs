using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        // Turns off shooting effect when not shooting
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        // Stop particle effect from previous shot, then start for current shot
        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        
        // 0 is the barrel of the gun, transform needs to be found
        gunLine.SetPosition (0, transform.position);

        // Start the raycast at the gun end aka origin
        shootRay.origin = transform.position;

        // The raycast direction is the direection the gun is pointed
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            // If you hit something, connect to it's EnemyHealth script
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            
            // If the EnemyHealth script isn't null (ie. you hit a wall or something that
            // doesn't have that script, take health away from enemy
            if(enemyHealth != null)
            {
                // Subtract from enemy health and instantiate effect at point it was shot
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        // Player doesn't hit anything, draw a line
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
