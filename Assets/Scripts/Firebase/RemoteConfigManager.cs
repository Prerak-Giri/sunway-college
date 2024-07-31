using System;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using NaughtyAttributes;
using UnityEngine;

namespace Firebase
{
    public class RemoteConfigManager : MonoBehaviour
    {
        void Start()
        {
            FirebaseController.OnFirebaseInitialized += OnFirebaseInitialized;
        }


        [Dropdown("Keys")] public string ToFetchKey;
        
        
        private List<string> Keys { get { return new List<string>() { "TestInteger", "TestString", "TestBool", "TestJson" }; } }
        
        private void OnFirebaseInitialized()
        {
            FetchRemoteConfig(() =>
            {
                Debug.Log("Remote Config Fetch Success");
            });
        }
        
        
        public void FetchRemoteConfig(Action OnValuesFetched)
        {
            Debug.Log("Fetching data...");
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            remoteConfig.FetchAsync(System.TimeSpan.Zero).ContinueWithOnMainThread(
                previousTask=>
                {
                    if (!previousTask.IsCompleted)
                    {
                        Debug.LogError($"{nameof(remoteConfig.FetchAsync)} incomplete: Status '{previousTask.Status}'");
                        return;
                    }
                    ActivateValues(OnValuesFetched);
                });
        }
        
        
        private void ActivateValues(System.Action onFetchAndActivateSuccessful)
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if(info.LastFetchStatus == LastFetchStatus.Success)
            {
                remoteConfig.ActivateAsync().ContinueWithOnMainThread(
                    previousTask =>
                    {
                        Debug.Log($"Remote data loaded and ready (last fetch time {info.FetchTime}).");
                        onFetchAndActivateSuccessful();
                    });
            }
        }

        [Button]
        public void FetchValueUI()
        {
            var value = FetchValue(ToFetchKey);
            if (String.IsNullOrEmpty(value.ToString()))
            {
                Debug.LogError("The key cannot be found");
                return;
            }
            Debug.Log("Fetched value is " + value);
        }
        
        
        private object FetchValue(string key)
        {
            ConfigValue configValue = FirebaseRemoteConfig.DefaultInstance.GetValue(key);

            object obj;
            if (bool.TryParse(configValue.StringValue, out bool boolResult))
            {
                obj = boolResult;
            }
            else if (int.TryParse(configValue.StringValue, out int intResult))
            {
                obj = intResult;
            }
            else if (double.TryParse(configValue.StringValue, out double doubleResult))
            {
                obj = doubleResult;
            }
            else
            {
                obj = configValue.StringValue;
            }

            return obj;
        }


        
    }
}

public class TestClass
{
    public string name;
    public int age;
}
