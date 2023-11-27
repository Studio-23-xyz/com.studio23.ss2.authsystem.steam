using Studio23.SS2.AuthSystem.Data;
using UnityEngine;


namespace Studio23.SS2.AuthSystem.Core
{
    public class AuthenticationManager : MonoBehaviour
    {
        [SerializeField] private ProviderBase _providerBase;

        /// <summary>
        /// This method will check  user authentication for validating Digital Rights Managemment for the project
        /// </summary>
        public void Auth()
        {
            _providerBase?.Authenticate();
        }
    }
}

