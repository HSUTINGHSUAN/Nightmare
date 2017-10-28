using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;//射擊給敵人的傷害
    public float timeBetweenBullets = 0.15f;//間隔時間
    public float range = 100f;//攻擊範圍


    float timer;//紀錄上次攻擊的時間
    Ray shootRay = new Ray();
    RaycastHit shootHit;//被打到的物件
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;//特效播放的間隔時間

    //變數宣告

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");//敵人的圖層
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;//60fps frame per second = 1/60 = 0.016666

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)//判斷玩家有無按下Fire1 // Edit > project setting > input可以看的到控制
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects () //特效消失
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;//雷射線打開
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;//設定射線起始位置 //看程式碼是掛在誰身上 //因為此程式碼是掛在槍管上，所以起始位置是在槍管上
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else//都沒有打到，位置設在最大的攻擊距離
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
