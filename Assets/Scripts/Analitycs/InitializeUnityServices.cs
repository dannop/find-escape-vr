using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

public class InitializeUnityServices : MonoBehaviour
{
    public string environment = "development";

    async void Start()
    {
        try
        {
            var options = new InitializationOptions().SetEnvironmentName(environment);
            await UnityServices.InitializeAsync(options);
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }
}
