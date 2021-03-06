﻿/*
 * Copyright 2008 Adam Witczak, Jakub Tężycki, Kamil Sławiński, Tomasz Bilski, Emil Hornung, Michał Ziober
 *
 * This file is part of Wings Of Fury 2.
 * 
 * Freeware Licence Agreement
 * 
 * This licence agreement only applies to the free version of this software.
 * Terms and Conditions
 * 
 * BY DOWNLOADING, INSTALLING, USING, TRANSMITTING, DISTRIBUTING OR COPYING THIS SOFTWARE ("THE SOFTWARE"), YOU AGREE TO THE TERMS OF THIS AGREEMENT (INCLUDING THE SOFTWARE LICENCE AND DISCLAIMER OF WARRANTY) WITH WINGSOFFURY2.COM THE OWNER OF ALL RIGHTS IN RESPECT OF THE SOFTWARE.
 * 
 * PLEASE READ THIS DOCUMENT CAREFULLY BEFORE USING THE SOFTWARE.
 *  
 * IF YOU DO NOT AGREE TO ANY OF THE TERMS OF THIS LICENCE THEN DO NOT DOWNLOAD, INSTALL, USE, TRANSMIT, DISTRIBUTE OR COPY THE SOFTWARE.
 * 
 * THIS DOCUMENT CONSTITUES A LICENCE TO USE THE SOFTWARE ON THE TERMS AND CONDITIONS APPEARING BELOW.
 * 
 * The Software is licensed to you without charge for use only upon the terms of this licence, and WINGSOFFURY2.COM reserves all rights not expressly granted to you. WINGSOFFURY2.COM retains ownership of all copies of the Software.
 * 1. Licence
 * 
 * You may use the Software without charge.
 *  
 * You may distribute exact copies of the Software to anyone.
 * 2. Restrictions
 * 
 * WINGSOFFURY2.COM reserves the right to revoke the above distribution right at any time, for any or no reason.
 *  
 * YOU MAY NOT MODIFY, ADAPT, TRANSLATE, RENT, LEASE, LOAN, SELL, REQUEST DONATIONS OR CREATE DERIVATE WORKS BASED UPON THE SOFTWARE OR ANY PART THEREOF.
 * 
 * The Software contains trade secrets and to protect them you may not decompile, reverse engineer, disassemble or otherwise reduce the Software to a humanly perceivable form. You agree not to divulge, directly or indirectly, until such trade secrets cease to be confidential, for any reason not your own fault.
 * 3. Termination
 * 
 * This licence is effective until terminated. The Licence will terminate automatically without notice from WINGSOFFURY2.COM if you fail to comply with any provision of this Licence. Upon termination you must destroy the Software and all copies thereof. You may terminate this Licence at any time by destroying the Software and all copies thereof. Upon termination of this licence for any reason you shall continue to be bound by the provisions of Section 2 above. Termination will be without prejudice to any rights WINGSOFFURY2.COM may have as a result of this agreement.
 * 4. Disclaimer of Warranty, Limitation of Remedies
 * 
 * TO THE FULL EXTENT PERMITTED BY LAW, WINGSOFFURY2.COM HEREBY EXCLUDES ALL CONDITIONS AND WARRANTIES, WHETHER IMPOSED BY STATUTE OR BY OPERATION OF LAW OR OTHERWISE, NOT EXPRESSLY SET OUT HEREIN. THE SOFTWARE, AND ALL ACCOMPANYING FILES, DATA AND MATERIALS ARE DISTRIBUTED "AS IS" AND WITH NO WARRANTIES OF ANY KIND, WHETHER EXPRESS OR IMPLIED. WINGSOFFURY2.COM DOES NOT WARRANT, GUARANTEE OR MAKE ANY REPRESENTATIONS REGARDING THE USE, OR THE RESULTS OF THE USE, OF THE SOFTWARE WITH RESPECT TO ITS CORRECTNESS, ACCURACY, RELIABILITY, CURRENTNESS OR OTHERWISE. THE ENTIRE RISK OF USING THE SOFTWARE IS ASSUMED BY YOU. WINGSOFFURY2.COM MAKES NO EXPRESS OR IMPLIED WARRANTIES OR CONDITIONS INCLUDING, WITHOUT LIMITATION, THE WARRANTIES OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE WITH RESPECT TO THE SOFTWARE. NO ORAL OR WRITTEN INFORMATION OR ADVICE GIVEN BY WINGSOFFURY2.COM, IT'S DISTRIBUTORS, AGENTS OR EMPLOYEES SHALL CREATE A WARRANTY, AND YOU MAY NOT RELY ON ANY SUCH INFORMATION OR ADVICE.
 * 
 * IMPORTANT NOTE: Nothing in this Agreement is intended or shall be construed as excluding or modifying any statutory rights, warranties or conditions which by virtue of any national or state Fair Trading, Trade Practices or other such consumer legislation may not be modified or excluded. If permitted by such legislation, however, WINGSOFFURY2.COM' liability for any breach of any such warranty or condition shall be and is hereby limited to the supply of the Software licensed hereunder again as WINGSOFFURY2.COM at its sole discretion may determine to be necessary to correct the said breach.
 * 
 * IN NO EVENT SHALL WINGSOFFURY2.COM BE LIABLE FOR ANY SPECIAL, INCIDENTAL, INDIRECT OR CONSEQUENTIAL DAMAGES (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS INTERRUPTION, AND THE LOSS OF BUSINESS INFORMATION OR COMPUTER PROGRAMS), EVEN IF WINGSOFFURY2.COM OR ANY WINGSOFFURY2.COM REPRESENTATIVE HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. IN ADDITION, IN NO EVENT DOES WINGSOFFURY2.COM AUTHORISE YOU TO USE THE SOFTWARE IN SITUATIONS WHERE FAILURE OF THE SOFTWARE TO PERFORM CAN REASONABLY BE EXPECTED TO RESULT IN A PHYSICAL INJURY, OR IN LOSS OF LIFE. ANY SUCH USE BY YOU IS ENTIRELY AT YOUR OWN RISK, AND YOU AGREE TO HOLD WINGSOFFURY2.COM HARMLESS FROM ANY CLAIMS OR LOSSES RELATING TO SUCH UNAUTHORISED USE.
 * 5. General
 * 
 * All rights of any kind in the Software which are not expressly granted in this Agreement are entirely and exclusively reserved to and by WINGSOFFURY2.COM.
 * 
 * 
 */

using System;
using System.Drawing;
using BetaGUI;
using Mogre;
using Wof.Controller.Screens;
using Wof.Languages;
using Wof.Model.Level.Effects;
using Wof.View.NodeAnimation;

namespace Wof.Controller.EffectBars
{
	internal class AltitudeBar : IDisposable
    {
        private const string ImageBar = @"altitudeBar.PNG";
        private const string ImageBarBg = @"bulletTimeBarBg.PNG";
        private float _height = 25.0f;
        private float _width = 75f;
        
        private Window _bar;
        private Window _bar2;
        
        private GUI _gui1;
        private GUI _gui2;
        
        private OverlayContainer _barOverConta;
        
       
        private OverlayContainer _barOverContaBg;
        private OverlayContainer _text;
        
        private PointF _startPoint = Point.Empty;
        
        private static Timer blinkDelay;

        private ColourValue _colour1 = new ColourValue(0.68f, 0.77f, 0.41f);
        private ColourValue _colour2 = new ColourValue(0.6f,0.1f,0.1f);
        
        /// <summary>
        /// Kiedy zaczyna konczyc sie efekt
        /// </summary>
        private float _threshold = 0.08f;
        
        private bool thresholdCrossed = false;

        public AltitudeBar(float fontSize, Viewport viewport, float width, float height)
        {
             blinkDelay = new Timer();
        	_height = height;
        	_width = width;
        
            //pozycja paska
            _startPoint = new PointF(_width * 1.15f + viewport.ActualWidth / 3.0f , viewport.ActualHeight * 1.020f);
              
            float min = _width / 150.0f;
         
            _gui1 = new GUI(Wof.Languages.FontManager.CurrentFont, (uint)fontSize, "AltitudeBarGUI1");
            _gui1.SetZOrder(100);
            
            _bar = _gui1.createWindow(new Vector4(_startPoint.X - min, _startPoint.Y - min, _width+ 2*min, _height+ 2*min), String.Empty, (int)wt.NONE, String.Empty);
            _barOverContaBg = _bar.createStaticImage(new Vector4(0, 0, _width + 2*min, _height + 2*min), ImageBarBg, 0);
           
            
            _gui2 = new GUI(Wof.Languages.FontManager.CurrentFont, (uint)fontSize, "AltitudeBarGUI2");
            _gui2.SetZOrder(110);
            
            _bar2 = _gui2.createWindow(new Vector4(_startPoint.X - min, _startPoint.Y - min, _width+ 2*min, _height+ 2*min), String.Empty, (int)wt.NONE, String.Empty);
            _barOverConta = _bar2.createStaticImage(new Vector4(min, min, _width, _height), ImageBar, 10);
            
              
            uint oldFontSize = _gui2.mFontSize;
            _gui2.mFontSize = (uint)(oldFontSize * 0.55f);           
            _text = _bar2.createStaticText(new Vector4(min, - 9.0f *min, _width, _height * 0.90f), LanguageResources.GetString(LanguageKey.Altitude), _colour1);
            _gui2.mFontSize = oldFontSize;
            
        }

        public void Update(int time)
        {
           
            ModelEffectsManager.Instance.UpdateEffect(time, EffectType.AltitudeEffect);
            float width = ModelEffectsManager.Instance.GetEffectLevel(EffectType.AltitudeEffect) * _width;
            //_barOverConta.SetPosition(_startPoint.X, _startPoint.Y - (_heigth - h));
           
            
            _barOverConta.SetDimensions(width, _height);
            _barOverConta.Show();
            
            // todo: timer 
            if(width < _width * _threshold)
            {
            	thresholdCrossed = true;
            	if(blinkDelay.Milliseconds > 100)
            	{            
            		BetaGUI.Window.ChangeContainerColour(_text, _colour2);
            		if(_text.IsVisible)
	            	{
	            		_text.Hide();
	            	} else
	            	{
	            		_text.Show();
	            	}
            		blinkDelay.Reset();
            	}
            	
            } else
            { 
            
            	if(thresholdCrossed)
            	{
            	    blinkDelay.Reset();            		  			
            		BetaGUI.Window.ChangeContainerColour(_text, _colour1);
            		if(!_text.IsVisible) _text.Show();
            		thresholdCrossed = false;      
            		
            	}
            	
            }
            
        }
        
        public void Dispose()
        {
        	_gui1.killGUI();
        	_gui1 = null;
        	
        	_gui2.killGUI();
        	_gui2 = null;
        	
        	blinkDelay = null;
        	
        }
    }
}
