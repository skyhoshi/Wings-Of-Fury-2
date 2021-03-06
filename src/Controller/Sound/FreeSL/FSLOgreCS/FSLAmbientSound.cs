using System;
using Mogre;
using Wof.Controller;

namespace FSLOgreCS
{
    /// <summary>
    /// Reprezentuje obiekt ktory nie zalezy od polozenia kamery
    /// </summary>
    public class FSLAmbientSound : FSLSoundObject
    {
    

        public FSLAmbientSound(string soundFile, string name, bool loop, bool streaming)
            : base(soundFile, name, loop, streaming)
        {
            FreeSL.fslSoundSetSourceRelative(_sound, true);
        }

        public FSLAmbientSound(string package, string soundFile, string name, bool loop)
            : base(package, soundFile, name, loop)
        {
            FreeSL.fslSoundSetSourceRelative(_sound, true);
        }

       

        public override void Update()
        { 
        	base.Update();
          //  if (_streaming)
            {
         
            }
           

            if(this.IsPlaying())
            {
            
             //   FreeSL.fslUpdate();
                //FreeSL.fslSleep(0.01f);
            }
        }
    }
}