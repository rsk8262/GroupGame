using UnityEngine;

public class EnemyAI_SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource[] attackSounds;
    public AudioSource[] searchSounds;
    public AudioSource damagedSound;
    public float lastSoundTime;
    public void ApplySearchSound()
    {
        if (lastSoundTime > Time.time) return;
        searchSounds[Random.Range(0, searchSounds.Length)].Play();
        lastSoundTime = Time.time + Random.Range(4, 10);
    }
    public void ApplyAttackSound()
    {
        attackSounds[Random.Range(0, attackSounds.Length)].Play();
    }
    public void ApplyDamageSound()
    {
        damagedSound.Play();
    }
}
