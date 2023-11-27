using UnityEngine;

namespace Studio23.SS2.AuthSystem.Data
{
    public abstract class ProviderBase : ScriptableObject
    {
        public abstract void Authenticate();
    }

}