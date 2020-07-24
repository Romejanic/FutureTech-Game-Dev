using UnityEngine;

[AddComponentMenu("Game Dev Workshop/Disappear")]
public class Disappear : MonoBehaviour
{
   
    public void DoDisappear()
    {
        Destroy(gameObject);
    }

}
