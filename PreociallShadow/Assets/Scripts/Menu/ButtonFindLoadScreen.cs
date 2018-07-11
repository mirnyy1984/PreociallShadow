using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    class ButtonFindLoadScreen : MonoBehaviour
    {
        public void LoadScene(string scene)
        {
            LoadingScreenManager.Instance.LoadLevelNoFade(scene);
        }
    }
}
