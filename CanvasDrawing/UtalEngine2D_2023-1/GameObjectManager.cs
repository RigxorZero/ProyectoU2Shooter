
using System.Collections.Generic;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public static class GameObjectManager
    {
        public static List<GameObject> AllNewGameObjects = new List<GameObject>();
        public static List<GameObject> AllGameObjects = new List<GameObject>();
        public static List<GameObject> AllDeadGameObjects = new List<GameObject>();
        public static List<EmptyUpdatable> AllNewEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<EmptyUpdatable> AllEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<EmptyUpdatable> AllDeadEmptyUpdatables = new List<EmptyUpdatable>();
        public static List<UtalText> AllText = new List<UtalText>();
        public static List<UtalText> AllDeadText = new List<UtalText>();

        public static void Update()
        {
            foreach(GameObject go in AllNewGameObjects)
            {
                AllGameObjects.Add(go);
                go.Start();
            }
            AllNewGameObjects = new List<GameObject>();
            foreach(GameObject go in AllGameObjects)
            {
                go.Update();
            }
            foreach(EmptyUpdatable eu in AllEmptyUpdatables)
            {
                eu.Update();
            }
            
        }
        public static void DeadUpdate()
        {
            foreach (GameObject go in GameObjectManager.AllDeadGameObjects)
            {
                GameObjectManager.AllGameObjects.Remove(go);
                go.OnDestroy();
            }
            GameObjectManager.AllDeadGameObjects = new List<GameObject>();
            foreach(UtalText text in GameObjectManager.AllDeadText)
            {
                AllText.Remove(text);
            }
            AllDeadText = new List<UtalText>();
            foreach(EmptyUpdatable eu in AllDeadEmptyUpdatables)
            {
                AllEmptyUpdatables.Remove(eu);
            }
            AllDeadEmptyUpdatables = new List<EmptyUpdatable>();
        }
    }
}
