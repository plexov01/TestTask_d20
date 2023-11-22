namespace TestTask_d20.Feautures.ObjectCreator
{
    using UnityEngine;
    
    /// <summary>
    /// Создатель для объектов на сцене
    /// </summary>
    public class ObjectCreator : MonoBehaviour
    {
        /// <summary>
        /// Создать объект
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject CreateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(prefab, position, rotation, parent);
        }
    }

}
