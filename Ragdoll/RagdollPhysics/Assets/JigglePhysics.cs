using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JigglePhysics : MonoBehaviour
{
    public float intensity, mass, stiffness, dampness;
    private Mesh Original, MeshClone;
    private MeshRenderer OriginalRenderer;
    private Vector3[] VertexArray;
    private GameObjectVertex[] GV;


    // Start is called before the first frame update
    void Start()
    {
        Original = GetComponent<MeshFilter>().sharedMesh;
        MeshClone = Instantiate(Original);
        GetComponent<MeshFilter>().sharedMesh = MeshClone;
        OriginalRenderer = GetComponent<MeshRenderer>();


        GV = new GameObjectVertex[MeshClone.vertices.Length];
        for (int i = 0; i < MeshClone.vertices.Length; i++) {
            GV[i] = new GameObjectVertex(i, transform.TransformPoint(MeshClone.vertices[i]));
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        VertexArray = Original.vertices;
        for (int i = 0; i < GV.Length; i++)
        {
            Vector3 target = transform.TransformPoint(VertexArray[GV[i].ID]);
            float _intensity = (1-(OriginalRenderer.bounds.max.y - target.y)/ OriginalRenderer.bounds.size.y) * intensity;
            GV[i].Shake(target, mass, stiffness, dampness);
            VertexArray[GV[i].ID] = Vector3.Lerp(VertexArray[GV[i].ID], target, _intensity);
        }
        MeshClone.vertices = VertexArray;
    }


    public class GameObjectVertex   {
        public int ID;
        public Vector3 position;
        public Vector3 velocity, force;


        public GameObjectVertex(int _ID, Vector3 _pos)
        {
            ID = _ID;
            position = _pos;
        }
        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - position) * s;
            velocity = (velocity + force / m) * d;
            position += velocity;
            if ((velocity + force + force/m).magnitude < 0.01f)
            {
                position = target;
            }
        }
    }


}
