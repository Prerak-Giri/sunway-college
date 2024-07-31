using System;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Firebase
{
    public class FirebaseController : MonoBehaviour
    {
        private FirebaseApp myApp;
        public static event Action OnFirebaseInitialized;
        void Awake()
        {
            InitFirebase();
        }

        void InitFirebase()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    myApp = Firebase.FirebaseApp.DefaultInstance;
                    Debug.Log("<color=green>Firebase Init Success</color>");
                    OnFirebaseInitialized?.Invoke();
                } else {
                    UnityEngine.Debug.LogError("Firebase Init Failed");
                }
            });
        }

        public void LogEvent(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }
        
        public void LogEvent(string eventName,Parameter parameter)
        {
            FirebaseAnalytics.LogEvent(eventName,parameter);
        }

        
    }
}
