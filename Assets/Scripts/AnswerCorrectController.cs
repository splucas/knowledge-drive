using System.Collections;
using UnityEngine;

public class AnswerCorrectController : MonoBehaviour, IResetable
{

    public Transform ParticlesTform;

    bool moveParticles = false;
    private ParticleSystem system;
    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[50];

    Vector3 particleTarget;


    

    void Start()
    {
        system = ParticlesTform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveParticles)
        {

            int count = system.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                Vector3 v1 = (particles[i].position);

                particles[i].position = Vector3.MoveTowards(v1, particleTarget, Time.deltaTime * 20.0f);
                
            }

            system.SetParticles(particles, count);
        }
        
    }


    public void ShowCorrect(Transform targetLoc, Vector3 PartclePos)
    {
        particleTarget = targetLoc.position;
        ParticlesTform.position = PartclePos;
        StartCoroutine(ShowNextCorrect(targetLoc));
    }

    IEnumerator ShowNextCorrect(Transform targetTrans)
    {
        yield return new WaitForSeconds(1.0f);
        moveParticles = true;

        yield return new WaitForSeconds(.5f);

        SpriteRenderer spriteRenderer = targetTrans.GetComponent<SpriteRenderer>();

        while(spriteRenderer.color.a < 1.0)
        {
            Color col = spriteRenderer.color;
            col.a += Time.deltaTime;
            spriteRenderer.color = col;
            yield return new WaitForEndOfFrame();
        }

    }

    public void Reset()
    {
        Destroy(gameObject);
    }
}
