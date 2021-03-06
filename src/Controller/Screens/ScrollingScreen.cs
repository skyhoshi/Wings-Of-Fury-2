using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BetaGUI;
using Mogre;
using Wof.Languages;
using FontManager=Mogre.FontManager;

namespace Wof.Controller.Screens
{
    /// <summary>
    /// Abstrakcyjna klasa reprezentuj�ca automatycznie przewijaj�cy si� screen w menu
    /// <author>Adam Witczak</author>
    /// </summary>
    abstract class ScrollingScreen : AbstractScreen, BetaGUIListener
    {
        protected bool startFromBottom = true;
        
        /// <summary>
        /// Margines na dole ekranu
        /// </summary>
        protected float bottomMargin = 10.0f;
        public float BottomMargin
        {
            get { return bottomMargin; }
        }

        protected float speed = 15.0f;
        /// <summary>
        /// Szybko�� przewijania element�w wyra�ona w px na sek.
        /// </summary>
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        protected bool enabled = false;
        public bool Enabled
        {
            set { enabled = value; }
            get { return enabled; }
        }

        public bool OutOfBounds
        {
            get { return outOfBounds; }
        }

        protected List<PositionedMessage> messages;
        private List<OverlayContainer> messageOverlays;


        protected Callback cc;
        protected Window guiWindow;

        public ScrollingScreen(GameEventListener gameEventListener, IFrameWork framework, Viewport viewport,
                               Camera camera, bool startFromBottom, float speed) : base(gameEventListener, framework, viewport, camera)
        {
            messages = new List<PositionedMessage>();
            messageOverlays = new List<OverlayContainer>();
            this.startFromBottom = startFromBottom;
            this.speed = speed;
                      
        }

        public override void CleanUp(Boolean justMenu)
        {
            base.CleanUp(justMenu);
        }

        protected override void CreateGUI()
        {
            mGui = new GUI(Wof.Languages.FontManager.CurrentFont, fontSize);
            createMouse();
            string message = "";
           
           
            guiWindow = mGui.createWindow(new Vector4(0, 0, Viewport.ActualWidth, Viewport.ActualHeight),
                                          "bgui.window", (int)wt.NONE, message);
            cc = new Callback(this);
            messages = buildMessages();
            List<Button> temp = buildButtons();

            initButtons(temp.Count, (int)getBackButtonIndex());
            buttons = temp.ToArray();
            
            
            float y = 0;
            
            if (startFromBottom)
            {
                y += guiWindow.h;
                foreach (Button b in buttons)
                {
                    b.Translate(new Vector2(0, y));
                }
                
            }
            OverlayContainer container;
            foreach(PositionedMessage m in messages)
            {
                 container = guiWindow.createStaticText(new Vector4(m.X, y, m.Width, m.Height), m.Message);
                 if(m.ColourTop != null)
                 {
                     SetOverlayColor(container, m.ColourTop, m.ColourBottom);
                 }
                 messageOverlays.Add(container);
                 y += m.YSpace;
            }
                     
            selectButton(getBackButtonIndex());
            guiWindow.show();
            enabled = true;
        }

        protected abstract List<Button> buildButtons();

        /// <summary>
        /// Zwraca index przycisku 'back'. Je�li nie ma przycisku 'back' powinno zwr�ci� -1
        /// </summary>
        /// <returns></returns>
        protected abstract int getBackButtonIndex();

        /// <summary>
        /// Buduje list� obiekt�w PositionedMessage - wy�wietlanych tekst�w
        /// </summary>
        /// <returns></returns>
        protected abstract List<PositionedMessage> buildMessages();
        

        protected virtual void Translate(float timeSinceLastFrame, bool reverse)
        {
           
            float newTop;
            float maxY = float.MinValue;
           // float minY = float.MaxValue;
            float step = (speed * timeSinceLastFrame) * (Viewport.ActualHeight / 1050.0f); // normalizacja do szybkosci scrollowania na ekranie 1680/1050
            if (reverse)
            {
                step *= -1;
            }

            foreach (OverlayContainer o in messageOverlays)
            {
                float top = StringConverter.ParseReal(o.GetParameter("top"));
                newTop = (top - step);
                o.SetParameter("top", StringConverter.ToString(newTop));

                if (newTop + StringConverter.ParseReal(o.GetParameter("height")) + bottomMargin > maxY)
                {
                    maxY = newTop + StringConverter.ParseReal(o.GetParameter("height")) + bottomMargin;
                }
            }

            for (int j = 0; j < buttons.Length; j++)
            {
                Button b = buttons[j];
                if (b.Y + b.h < 0 && j != backButtonIndex)
                {
                    buttons[j].killButton();
                    continue;
                }
                b.Translate(new Vector2(0, -step));
                if (b.Y + b.h > maxY + bottomMargin)
                {
                    maxY = b.Y + b.h + bottomMargin;
                }
            }

            // stop condition
            if (maxY < guiWindow.h)
            {
                enabled = false;
                outOfBounds = true;
            }
            
        }

        private bool outOfBounds = false;
        /// <summary>
        /// Przewija wszystkie elementy
        /// </summary>
        /// <param name="evt"></param>
        public override void FrameStarted(FrameEvent evt)
        {
           
           
            base.FrameStarted(evt);
            if (!Enabled) return;
            Translate(evt.timeSinceLastFrame, false);

        }

        #region BetaGUIListener Members

        public abstract void onButtonPress(Button referer);
        

        #endregion
    }
}
