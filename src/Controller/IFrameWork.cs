using System;
using System.Collections.Generic;
using Mogre;
using MOIS;
using Plane=Wof.Model.Level.Planes.Plane;

namespace Wof.Controller
{
    public interface IFrameWork
    {
        Camera Camera { get; }
        Camera MinimapCamera { get; }
        Camera MinimapNoseCamera { get; }
       
        Viewport Viewport { get; }
        Viewport MinimapViewport { get; }
        Viewport OverlayViewport { get; }
        void Go();

 		Keyboard InputKeyboard
        {
            get;
        }

        SceneManager SceneMgr
        {
            get; set;
        }

        SceneManager MinimapMgr
        {
            get; set;
        }

        SceneManager OverlayMgr
        {
            get;
            set;
        }

        CameraListenerBase CameraListener { get;  }

        void ChooseSceneManager();
  
      
   
        void HandleCameraInput(Keyboard inputKeyboard, Mouse inputMouse, IList<JoyStick> inputJoysticks, FrameEvent evt, Camera camera,
                               Camera minimapCamera, Plane playerPlane);

        void TakeScreenshot(string fileName);
        string UpdateStats();
       

      
        
       

        void SetCompositorEnabled(FrameWorkForm.CompositorTypes type, bool enabled);
     
    }
}