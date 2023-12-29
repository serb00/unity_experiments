using Newtonsoft.Json;
using UnityEngine;

public class serializer : MonoBehaviour
{
    void Start()
    {
        Movement obj = GetComponent<Movement>();
        Debug.Log(obj.angularSpeed);
        string json = JsonConvert.SerializeObject(obj);
        Debug.Log(json);
    }
}