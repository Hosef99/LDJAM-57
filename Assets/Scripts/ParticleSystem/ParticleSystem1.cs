using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Particle {
    public Vector3 position;
    public float size;
    public float lifeTime;
    public float age;
    public float t;
    public float opacity;
    public Vector3 velocity;

    public Particle(ParticleProperty p) {
        this.age = 0;
        this.position = p.position;
        this.size = p.size;
        this.lifeTime = p.lifeTime;
        this.velocity = p.velocity;
    }

    public void update() {
        this.age += Time.deltaTime;
        this.t = Mathf.InverseLerp(0, lifeTime, age);
        this.opacity = Mathf.Lerp(0.3f, 0f, t);
        position += velocity * Time.deltaTime;
    }
}

struct ParticleProperty {
    public Vector3 position;
    public float size;
    public float lifeTime;
    public Vector3 velocity;
}

public class ParticleSystem1 : MonoBehaviour {

    public int n = 5;
    public float size = 1.0f / 40.0f;
    public float lifeTime = 0.3f;
    [Range(0.01f, 0.5f)]
    public float interval = 0.035f;
    public float areaSize = 0.2f;
    public float speed = 4.5f;
    public Vector2 position;
    public bool isEnabled;

    public float y;
    public Vector2 area;

    List<Particle> particlesList;
    List<Vector3> vertices = new List<Vector3>(); 
    List<int> triangles = new List<int>();
    List<Vector2> uv0 = new List<Vector2>();
    Mesh mesh;
    ParticleProperty p;
    float cooldown;
    Transform playerTransform;

    void Start() {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        particlesList = new List<Particle>();
        mesh = new Mesh();  
        mesh.MarkDynamic();
        GetComponent<MeshFilter>().mesh = mesh;

        p.position = new Vector3(0, 0, 0);
    }

    void Update() {
        position = new Vector2(playerTransform.position.x, playerTransform.position.y) + new Vector2(0, y);

        if (isEnabled) {
            createParticles();
        }
        cooldown -= Time.deltaTime;

        for (int i = 0; i < particlesList.Count; i++) {
            particlesList[i].update();
        }
        particlesQueueToMesh();
    }

    public void createParticles() {
        if (cooldown <= 0) {
            for (int i = 0; i < n; i++) {
                p.size = size;
                p.lifeTime = Random.Range(Mathf.Max(0, lifeTime - 0.1f), lifeTime + 0.1f);
                p.position = new Vector3(Random.Range(-area.x / 2.0f + position.x, area.x / 2.0f + position.x), Random.Range(-area.y / 2.0f + position.y, area.y / 2.0f + position.y), 0);
                p.velocity = (p.position - new Vector3(position.x, position.y, 0)).normalized * Random.Range(speed / 2, speed);
                createParticle(p);
            } 
            
            cooldown = interval;
        }
    }

    private void createParticle(ParticleProperty p) {
        Particle particle = new Particle(p);
        particlesList.Add(particle);
    }

    void particlesQueueToMesh() {
        mesh.Clear();
        vertices.Clear();
        triangles.Clear();
        uv0.Clear();

        for (int i = 0; i < particlesList.Count; i++) {
            vertices.Add(new Vector3(particlesList[i].size / 2 + particlesList[i].position.x, particlesList[i].size / 2 + particlesList[i].position.y, particlesList[i].position.z));
            vertices.Add(new Vector3(particlesList[i].size / 2 + particlesList[i].position.x, -particlesList[i].size / 2 + particlesList[i].position.y, particlesList[i].position.z));
            vertices.Add(new Vector3(-particlesList[i].size / 2 + particlesList[i].position.x, -particlesList[i].size / 2 + particlesList[i].position.y, particlesList[i].position.z));
            vertices.Add(new Vector3(-particlesList[i].size / 2 + particlesList[i].position.x, particlesList[i].size / 2 + particlesList[i].position.y, particlesList[i].position.z));

            triangles.Add(3 + i * 4);
            triangles.Add(0 + i * 4);
            triangles.Add(1 + i * 4);
            triangles.Add(3 + i * 4);
            triangles.Add(1 + i * 4);
            triangles.Add(2 + i * 4);

            for (int j = 0; j < 4; j++) {
                uv0.Add(new Vector2(particlesList[i].opacity, 0));
            }          
        }

        for (int i = particlesList.Count - 1; i >= 0; i--) {
            if (particlesList[i].age >= particlesList[i].lifeTime) {
                particlesList.RemoveAt(i);
            }
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.SetUVs(0, uv0);
        mesh.RecalculateNormals();
    }
}
