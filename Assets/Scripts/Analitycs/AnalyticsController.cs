using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;

public class AnalyticsController : MonoBehaviour
{
    public void RecordCustomEvent()
    {
        Dictionary<string, object> parameters = new()
        {
            { "messsage", "Teste de entrada" }
        };
        AnalyticsService.Instance.CustomData("connect", parameters);
        AnalyticsService.Instance.Flush();
        Debug.Log("Enviou evento");
    }

    public void RegisterTest3()
    {
        Dictionary<string, object> parameters = new()
        {
            { "message", "Teste Pocoes finalizado" }
        };
        AnalyticsService.Instance.CustomData("testComplete", parameters);
        AnalyticsService.Instance.Flush();

        Debug.Log("Enviou Teste pocoes");
    }
}
