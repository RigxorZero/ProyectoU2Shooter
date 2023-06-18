
using System.Collections.Generic;


namespace CanvasDrawing.UtalEngine2D_2023_1.Physics
{
    public static class PhysicsEngine
    {
        public static List<Rigidbody> allNewRigidbodies = new List<Rigidbody>();
        public static List<Rigidbody> allRigidbodies = new List<Rigidbody>();
        public static List<Rigidbody> allDeadRigidbodies = new List<Rigidbody>();
        public static void Destroy(Rigidbody rigidbody)
        {
            allDeadRigidbodies.Add(rigidbody);
        }
        public static void Update()
        {
            foreach(Rigidbody rb in allNewRigidbodies)
            {
                allRigidbodies.Add(rb);
            }
            allNewRigidbodies = new List<Rigidbody>();
            for(int i=0; i<allRigidbodies.Count; i++)
            {
                for(int j=i+1; j < allRigidbodies.Count; j++)
                {
                    if(allRigidbodies[i].CheckCollision(allRigidbodies[j])){
                        //Console.WriteLine(allRigidbodies[i].transform.position);
                        //Console.WriteLine(allRigidbodies[j].transform.position);
                    }
                }
            }
            foreach(Rigidbody rb in allDeadRigidbodies)
            {
                allRigidbodies.Remove(rb);
            }
            allDeadRigidbodies = new List<Rigidbody>();

            foreach(Rigidbody rb in allRigidbodies)
            {
                rb.lastPos = rb.transform.position;
            }
        }
    }
}
