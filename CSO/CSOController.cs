using DisableCSO.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.PubSubSystem.Core;
using Kingmaker.Code.UI.MVVM.VM.Fade;
using Kingmaker;
using Kingmaker.Code.UI.MVVM.View.Common.PC;
using Kingmaker.AreaLogic.Cutscenes.Commands;
using UnityEngine;

namespace DisableCSO.CSO
{
    internal class CSOController : IModEventHandler, IAreaLoadingStagesHandler
    {
        public int Priority => 100;

        public void HandleModDisable()
        {
            EventBus.Unsubscribe(this);
            SetCSO();
        }

        public void HandleModEnable()
        {
            EventBus.Subscribe(this);
            SetCSO();
        }

        public void OnAreaLoadingComplete() => SetCSO();

        public void OnAreaScenesLoaded() { }

        public static void SetCSO()
        {
            var common = (CommonPCView) Game.Instance.RootUiContext.m_CommonView;

            if (common == null)
                return;

            var overlay = common.transform.Find("FadeCanvas/FadeScreenView/CutscenesOverlay");

            if (overlay == null) 
                return;

            foreach (Transform transform in overlay.transform)
                transform.gameObject.SetActive(!Main.ModEntry.Enabled || Main.Settings.LetterBox && (transform.name == "TopLBX" || transform.name == "BotLBX"));
        }
    }
}
