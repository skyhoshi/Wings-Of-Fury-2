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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using MOIS;
using Mogre;
using BetaGUI;
using Wof.Controller.Input.KeyboardAndJoystick;
using Wof.Languages;
using Wof.Misc;

namespace Wof.Controller.Screens
{
	/// <summary>
	/// Description of JoystickChangerHelper.
	/// </summary>
	public class JoystickChangerHelper : AbstractChangerHelper
	{
	
		#region implemented abstract members of AbstractChangerHelper

		protected override void OnChangeButtonAddedDo(Button b)
		{			
			if (onChangeButtonAdded != null) {
				onChangeButtonAdded(b);
			}
			
		}

		#endregion
		protected IList<JoyStick> joysticks;
		
		protected JoyStick currentJoystick;		
	
		protected TextInput joystickDeadzoneTi;
		protected TextInput joystickSensitivityTi;
		
		protected IList<uint> horizontalAxesButtonIDs = new List<uint>(), verticalAxesButtonIDs  = new List<uint>();
	
		public delegate void OnControlsChanged();		
		public event OnControlsChanged onControlsChanged;
		
		public delegate void OnControlsCaptureStarted();		
		public event OnControlsCaptureStarted onControlsCaptureStarted;
		
		public delegate void OnControlsCaptureEnded();		
		public event OnControlsCaptureEnded onControlsCaptureEnded;
		
		public delegate void OnChangeButtonAdded(Button button);		
		public event OnChangeButtonAdded onChangeButtonAdded;
		
		
		public void Clear() {
			horizontalAxesButtonIDs.Clear();
			verticalAxesButtonIDs.Clear();
			identifiers.Clear();
			lastButtonId = 0;
		}
				
		
		
		public JoystickChangerHelper(Keyboard keyboard, IList<JoyStick> joysticks, MenuScreen parent)  : base(keyboard, parent) {
			
			this.joysticks = joysticks;
			UpdateCurrentJoystick();
			onControlsCaptureStarted += ControlsChangerHelper_onControlsCaptureStarted;
			onControlsCaptureEnded += ControlsChangerHelper_onControlsCaptureEnded;
			
		}
		
		
				
		public void UpdateCurrentJoystick() {
		
			if(FrameWorkStaticHelper.GetNumberOfAvailableJoysticks() >0){
				currentJoystick = joysticks[FrameWorkStaticHelper.GetCurrentJoystickIndex()];
			}
		
		}
		
		protected void ActivateJoysticks()
		{
			if(currentJoystick != null)
			{
				currentJoystick.ButtonPressed+=joystick_ButtonPressed;
				currentJoystick.ButtonReleased+=joystick_ButtonReleased;
			}
		}
		
	
		
		protected void DisableJoysticks()
		{
			if(currentJoystick != null)
			{
				currentJoystick.ButtonPressed-=joystick_ButtonPressed;
				currentJoystick.ButtonReleased-=joystick_ButtonReleased;
			}
			
		}
		
		bool joystick_ButtonPressed(JoyStickEvent arg, int button)		
		{
			return true;
		}
		
		bool joystick_ButtonReleased(JoyStickEvent arg, int button)
		{			
			if(button.Equals(KeyMap.Instance.JoystickEscape)){
				CloseControlChangeWindow();
				return false;
			}
						
			String langKey = GetLanguageKeyById(currentKeyId);
			
			// check for conflicts
			bool noconflict = true;
			string property = null; 
			
			// keys
			if(langKey.Equals(LanguageKey.Engine)){
				property = "JoystickEngine";
			}			
			if(langKey.Equals(LanguageKey.Gear)){
				property = "JoystickGear";							
			}
			if(langKey.Equals(LanguageKey.Gun)){				
				property = "JoystickGun";			
			}
			if(langKey.Equals(LanguageKey.Bombs)){
				property = "JoystickRocket";
			}
			if(langKey.Equals(LanguageKey.Spin)){
				property = "JoystickSpin";
			}
			if(langKey.Equals(LanguageKey.Camera)){
				property = "JoystickCamera";
			}			
			if(langKey.Equals(LanguageKey.BulletTimeEffect)){
				property = "JoystickBulletiTimeEffect";
			}
			if(langKey.Equals(LanguageKey.Back)){
				property = "JoystickEscape";
			}
			if(langKey.Equals(LanguageKey.OK)){
				property = "JoystickEnter";
			}
			
			if(property == null) return false;
			
			String[] exceptions;
			
			
			if(!langKey.Equals(LanguageKey.OK) && !langKey.Equals(LanguageKey.Back) ) {
				exceptions = new String[]{"JoystickEscape", "JoystickEnter"};
			}else {
				exceptions = new String[]{"JoystickEscape", "JoystickEnter"};
			}
			
			
			if(!langKey.Equals(LanguageKey.Pitch) && !langKey.Equals(LanguageKey.AccelerateBreakTurn)) {
				KeyMap.ClearOtherControlsWithSameKey(property, button, TypeOfControl.Joystick, exceptions);
				KeyMap.UpdateProperty(property, button, TypeOfControl.Joystick);
						
			}
			
		
			
			CloseControlChangeWindow();
			
			// notify parent to refresh
			if(onControlsChanged != null){
				onControlsChanged();
			}
			return true;
		}
		
		
		protected void ActivateKeyboard()
		{
			keyboard.KeyPressed+= keyboard_KeyPressed;
			keyboard.KeyReleased += keyboard_KeyReleased;			
		}
		
		protected void DisableKeyboard()
		{
			keyboard.KeyPressed-= keyboard_KeyPressed;
			keyboard.KeyReleased -= keyboard_KeyReleased;
		}
		
		bool keyboard_KeyPressed(KeyEvent arg)
		{
			return true;
		}

		bool keyboard_KeyReleased(KeyEvent arg)
		{
			if(arg.key.Equals(KeyCode.KC_ESCAPE)){
				CloseControlChangeWindow();
				return false;
			}
			
			return false;
		}
		
		
		void ControlsChangerHelper_onControlsCaptureStarted()
		{
			capturingKeys = true;
		}

		void ControlsChangerHelper_onControlsCaptureEnded()
		{
			capturingKeys = false;
		}
		
		
		protected override void DisplayControlChangeWindow(uint id)
		{
			 if(onControlsCaptureStarted != null){
				onControlsCaptureStarted();
			 }
		//	Console.WriteLine("DisplayControlChangeWindow");
			  ActivateJoysticks();
			  ActivateKeyboard();
			  string caption = "";
			  currentKeyId = id;
			 
			
			  caption += LanguageResources.GetString( GetLanguageKeyById(id));
			
			  caption += " = ... ";			  
			  //LanguageResources.GetString(LanguageKey.Pause);
			  
			  
              float width = ViewHelper.MeasureText(parentGui.mFont, caption, parentGui.mFontSize);
			  
			  
              controlChangeWindow = parentGui.createWindow(new Vector4(parentGuiWindow.x + (parentGuiWindow.w - width)/2.0f,parentGuiWindow.y+ parentGuiWindow.h/4,width,parentGuiWindow.h/2),
                                          "bgui.window", (int) wt.NONE, caption);
			  
			  if( GetLanguageKeyById(id).Equals(LanguageKey.Pitch)) {
			  	  controlChangeWindow.createStaticText(new Vector4(parentGui.mFontSize, parentGui.mFontSize, controlChangeWindow.w, parentGui.mFontSize ), "Step 1...");
					
			  }
			  
			  controlChangeWindow.show();
        
		}
		
		protected override void CloseControlChangeWindow()
		{		
			DisableJoysticks();
			DisableKeyboard();
		//	controlChangeWindow.hide();		
			parentGui.killWindow(controlChangeWindow);			
			controlChangeWindow = null;
			if(onControlsCaptureEnded != null){
				onControlsCaptureEnded();
			}
		}
		/*
		void joystickHorizontalAxisNoTi_onValueChanged(string delta)
		{
			bool error = false;
			try {
				bool recreate = false;
			    // horizontal
				var val = joystickHorizontalAxisNoTi.getValue();
				
				if(joystickHorizontalAxisNoTi.getValue().Length == 0 || joystickHorizontalAxisNoTi.getValue().Equals(joystickVerticalAxisNoTi.getValue())) {
					throw new Exception("Same values");
				}
				if(Int32.Parse(val) != KeyMap.Instance.JoystickHorizontalAxisNo) {
					recreate = true;
				}
				KeyMap.Instance.JoystickHorizontalAxisNo = Int32.Parse(val);
				KeyMap.Instance.Value = KeyMap.Instance.Value;	
				if(recreate) {
					parent.RecreateGUI();
				}
				return;
			}
			catch(Exception fe) {
				error = true;
			}
			
			if(error) {
				joystickHorizontalAxisNoTi.error();
			}else {
				joystickHorizontalAxisNoTi.noerror();
			}
			
		}
		
		void joystickVerticalAxisNoTi_onValueChanged(string delta)
		{
			bool error = false;
			try {
				bool recreate = false;
			    // horizontal
				var val = joystickVerticalAxisNoTi.getValue();
				
				if(joystickVerticalAxisNoTi.getValue().Length == 0 || joystickHorizontalAxisNoTi.getValue().Equals(joystickVerticalAxisNoTi.getValue())) {
					throw new Exception("Same values");
				}
				if(Int32.Parse(val) != KeyMap.Instance.JoystickVerticalAxisNo) {
					recreate = true;
				}
				KeyMap.Instance.JoystickVerticalAxisNo = Int32.Parse(val);
				KeyMap.Instance.Value = KeyMap.Instance.Value;	
				if(recreate) {
					parent.RecreateGUI();
				}
				return;
			}
			catch(Exception fe) {
				error = true;
			}
			
			if(error) {
				joystickVerticalAxisNoTi.error();
			}else {
				joystickVerticalAxisNoTi.noerror();
			}
			
		}*/
		
		void joystickDeadzoneTi_onValueChanged(string delta)
		{
			bool error = false;
			try {
				var val = joystickDeadzoneTi.getValue();					 
				KeyMap.Instance.JoystickDeadZone = Double.Parse("0."+Int32.Parse(val), ci);
				KeyMap.Instance.Value = KeyMap.Instance.Value;			
					
			}
			catch(FormatException fe) {
				error = true;			
			}
			if(error) {
				joystickDeadzoneTi.error();
			}else {
				joystickDeadzoneTi.noerror();
			}
			
		}
		
		readonly CultureInfo ci = new CultureInfo("en-US");
		
		void joystickSensitivityTi_onValueChanged(string delta)
		{
			
			bool error = false;
			try {
				
				var val = joystickSensitivityTi.getValue();			
				
				double newVal = Double.Parse(val, ci);				
				KeyMap.Instance.JoystickSensivity = newVal;	
				if(newVal > 3.0 || newVal < 0.01) {
					throw new FormatException();
				}			
				KeyMap.Instance.Value = KeyMap.Instance.Value;
								
			}
			catch(FormatException fe) {
				error = true;				
			}
			
			if(error) {
				joystickSensitivityTi.error();
			}else {
				joystickSensitivityTi.noerror();
			}
			
		}
		
		
			
	

		
		#region BetaGUIListener implementation
		public override void onButtonPress(Button referer){
		
			// wylacz obsluge klawiatury w oknie powyzej
			// pokaz okno, czekaj na guzik
			if(capturingKeys) {
				return;
			}
			
			if(horizontalAxesButtonIDs.Contains(referer.id)) {
				// change horizontal axis no
				var newAxis = horizontalAxesButtonIDs.IndexOf(referer.id);
				if(newAxis.Equals(KeyMap.Instance.JoystickHorizontalAxisNo)) {
					return;
				}	
				if(newAxis.Equals(KeyMap.Instance.JoystickVerticalAxisNo)) {
					return;
				}
				
				KeyMap.Instance.JoystickHorizontalAxisNo = newAxis;		
				parent.RecreateGUI();
				
				return;
			}
			
			if(verticalAxesButtonIDs.Contains(referer.id)) {
				// change horizontal axis no
				var newAxis = verticalAxesButtonIDs.IndexOf(referer.id);
				if(newAxis.Equals(KeyMap.Instance.JoystickHorizontalAxisNo)) {
					return;
				}	
				if(newAxis.Equals(KeyMap.Instance.JoystickVerticalAxisNo)) {
					return;
				}				
				KeyMap.Instance.JoystickVerticalAxisNo = newAxis;				
				parent.RecreateGUI();
				return;
			}
			
			// change of other controls
		
			DisplayControlChangeWindow(referer.id);
			
		}
		#endregion		
		
			
			
		private string getButtonImageName(int joybutton){
			
			if(joybutton == -1) return "keyUnknown.png";
			return "key"+(joybutton+1)+".png";
		}
		int AddControlLine(string languageKey, int button, string additionalComment, Window guiWindow, GUI mGui, int left, int top, float width, uint fontSize, int y, int h, int leftOrg)
		{
			return AddControlLine(languageKey, button, additionalComment, guiWindow,mGui, left, top, width, fontSize, y, h, leftOrg, false);
		}
		int AddControlLine(string languageKey, int button, string additionalComment, Window guiWindow, GUI mGui, int left, int top, float width, uint fontSize, int y, int h, int leftOrg, bool noButton)
		{
			OverlayContainer c;
			float spaceSize = ViewHelper.MeasureText(mGui.mFont, " ", mGui.mFontSize);
			float imgSize = fontSize;
			float imgVDiff = -Mogre.Math.Abs(imgSize - fontSize) * 0.5f;
			
			var pos = new Vector4(left, top + y, width, h);
			var controlTxt = LanguageResources.GetString(languageKey) + ": "+additionalComment;
			c = guiWindow.createStaticText(pos, controlTxt);
			float controlTxtWidth = ViewHelper.MeasureText(mGui.mFont, controlTxt, mGui.mFontSize);
			guiWindow.createStaticImage(new Vector4(pos.x + controlTxtWidth, pos.y + imgVDiff, imgSize, imgSize), getButtonImageName(button));
			if(!noButton){
				AddChangeButton(new Vector2(leftOrg, pos.y), fontSize, languageKey);
			}
			return y;
		}

		
		

		
		public override int AddControlsInfoToGui(Window guiWindow, GUI mGui, int left, int top, int initialTopSpacing, float width, float textVSpacing, uint fontSize)
        {
        	
			var joystick = FrameWorkStaticHelper.GetCurrentJoystick(joysticks);
        	int y = initialTopSpacing;
        	int h = (int)textVSpacing;
            uint oldFontSize = mGui.mFontSize;
			float spaceSize = ViewHelper.MeasureText(mGui.mFont, " ", mGui.mFontSize);
            int leftOrg = left;
            Button b;
            // przesuniecie o szerokosc guzika (fontsize) + fontSize
            left += (int)(fontSize + fontSize);
            Vector4 pos;
        
  			Setup(mGui, guiWindow);          
            OverlayContainer c;
            y += (int)(h*1);
                  
            mGui.mFontSize = fontSize;
            y = AddControlLine(LanguageKey.OK, KeyMap.Instance.JoystickEnter, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
            
            
            y += (int)(h*1); 
            y = AddControlLine(LanguageKey.Back, KeyMap.Instance.JoystickEscape, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
            
          
            y += (int)(h*1);  
            y = AddControlLine(LanguageKey.Engine, KeyMap.Instance.JoystickEngine, " (" + LanguageResources.GetString(LanguageKey.Hold) + ")", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
            
            y += (int)(h*1);  
            y = AddControlLine(LanguageKey.Gear, KeyMap.Instance.JoystickGear, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
             
            y += (int)(h*1);
            y = AddControlLine(LanguageKey.Gun, KeyMap.Instance.JoystickGun, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
            
        	y += (int)(h*1);
            y = AddControlLine(LanguageKey.Bombs, KeyMap.Instance.JoystickRocket, "/" + LanguageResources.GetString(LanguageKey.Rockets)+ ": ", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
           
            y += (int)(h*1);
            y = AddControlLine(LanguageKey.Spin, KeyMap.Instance.JoystickSpin, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
           
            y += (int)(h*1);
            y = AddControlLine(LanguageKey.Camera, KeyMap.Instance.JoystickCamera, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
            
            y += (int)(h*1);
            y = AddControlLine(LanguageKey.BulletTimeEffect, KeyMap.Instance.JoystickBulletiTimeEffect, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg);
           
           
            y += (int)(h*1);
            
            y = AddControlLine(LanguageKey.RearmEndMission, KeyMap.Instance.JoystickRocket, "", guiWindow, mGui, left, top, width, fontSize, y, h, leftOrg, true);
           
            y += (int)(h*1);
            pos = new Vector4(left, top + y, width, h);
       
		// horizontal axis            
            string txt;
            float controlTxtWidth;
           
            	
            txt = LanguageResources.GetString(LanguageKey.ChooseHorizontalAxisNo)+":";
            controlTxtWidth = ViewHelper.MeasureText(mGui.mFont, txt, mGui.mFontSize);
			
            guiWindow.createStaticText(pos, txt); 
            pos.x += controlTxtWidth + spaceSize ;
          //  pos.y -= mGui.mFontSize
            pos.z = spaceSize*2;
            
            for(int i = 0; i < FrameWorkStaticHelper.GetCurrentJoystick(joysticks).JoyStickState.AxisCount; i++) {
            	horizontalAxesButtonIDs.Add(++lastButtonId);
            	var material = (KeyMap.Instance.JoystickHorizontalAxisNo != i) ? "bgui.button" : "bgui.selected.button";
            	
            	OnChangeButtonAddedDo(guiWindow.createButton(pos, material, i.ToString(), new Callback(this), lastButtonId));
			 	pos.x +=  pos.z ;            	
            }
            
            y += (int)(h*1);
            pos = new Vector4(left, top + y, width, h);
            
        // vertical axis  
            //FrameWorkStaticHelper.GetCurrentJoystick(joysticks).JoyStickState.VectorCount
            txt = LanguageResources.GetString(LanguageKey.ChooseVerticalAxisNo)+":";
            controlTxtWidth = ViewHelper.MeasureText(mGui.mFont, txt, mGui.mFontSize);
			
            guiWindow.createStaticText(pos, txt); 
            pos.x += controlTxtWidth + spaceSize ;
          //  pos.y -= mGui.mFontSize
            pos.z = spaceSize*2;
            
            for(int i = 0; i < FrameWorkStaticHelper.GetCurrentJoystick(joysticks).JoyStickState.AxisCount; i++) {
            	verticalAxesButtonIDs.Add(++lastButtonId);
            	var material = (KeyMap.Instance.JoystickVerticalAxisNo != i) ? "bgui.button" : "bgui.selected.button";
            	
            	OnChangeButtonAddedDo(guiWindow.createButton(pos, material, i.ToString(), new Callback(this), lastButtonId));
			 	pos.x += pos.z ;            	
            }
                       
            y += (int)(h*1);
            pos = new Vector4(left, top + y, width, h);
            
            
        // deadzone           
            txt = LanguageResources.GetString(LanguageKey.JoystickDeadzone)+" 0.";
            controlTxtWidth = ViewHelper.MeasureText(mGui.mFont, txt, mGui.mFontSize);
			
            guiWindow.createStaticText(pos, txt); 
            pos.x += controlTxtWidth;
            pos.z = spaceSize*2;
            joystickDeadzoneTi = guiWindow.createTextInput(pos, "bgui.textinput",KeyMap.Instance.JoystickDeadZone.ToString(ci).Substring(2).PadRight(2, '0')  , 2 );
            joystickDeadzoneTi.Validator = new NumberValidator();
            joystickDeadzoneTi.onValueChanged += joystickDeadzoneTi_onValueChanged;
              
		
            y += (int)(h*1);
            pos = new Vector4(left, top + y, width, h);
        // sensitivity
            txt = LanguageResources.GetString(LanguageKey.JoystickSensitivity)+ " (0.01 - 3.00):";
            controlTxtWidth = ViewHelper.MeasureText(mGui.mFont, txt, mGui.mFontSize);
			
            guiWindow.createStaticText(pos, txt); 
            pos.x += controlTxtWidth+ spaceSize;
            pos.z = spaceSize*3;
            joystickSensitivityTi = guiWindow.createTextInput(pos, "bgui.textinput", KeyMap.Instance.JoystickSensivity.ToString(ci), 4 );
            joystickSensitivityTi.Validator = new NumberAndDotValidator();
            joystickSensitivityTi.onValueChanged += joystickSensitivityTi_onValueChanged;
              
          
            return y+top;
        }
			
		
		   
		
	
	}
}
