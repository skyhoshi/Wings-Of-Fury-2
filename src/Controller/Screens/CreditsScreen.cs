/*
 * Copyright 2008 Adam Witczak, Jakub T�ycki, Kamil S�awi�ski, Tomasz Bilski, Emil Hornung, Micha� Ziober
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
using System.Collections.Generic;
using BetaGUI;
using Mogre;
using Wof.Languages;


namespace Wof.Controller.Screens
{
    internal class CreditsScreen : ScrollingScreen
    {
        #region Private Fields

        /// <summary>
        /// Format wyswietlanych napisow
        /// </summary>
        private const string NamesFormat = @"{0}:";

        /// <summary>
        /// Wyswietlany tekst
        /// </summary>
        private string[] names = {  EngineConfig.C_GAME_NAME, 
                                    String.Empty,
                                    String.Format(NamesFormat, LanguageResources.GetString(LanguageKey.CoreTeam)),
                                    "Adam Witczak",
                                    String.Empty,
                                    String.Format(NamesFormat, LanguageResources.GetString(LanguageKey.SupportTeam)),
                                    "Kamil Slawinski",  
                                    "Michal Ziober",
                                    "Jakub Tezycki",
                                    "Emil Hornung",
                                    "Tomasz Bilski",
                                    String.Empty, 
                                    String.Format(NamesFormat, LanguageResources.GetString(LanguageKey.Graphics)),
                                    "Adam Witczak",
                                    String.Empty,
                                    String.Format(NamesFormat, LanguageResources.GetString(LanguageKey.Sound)),
                                    "DJ Metalhead",
                                    "Jakub Tezycki",
                                    String.Empty,
                                    String.Format(@"WOF2 {0}:", LanguageResources.GetString(LanguageKey.CommunityTranslations)),
                                    "Makis", 
                                    "Joao Andre Colaco Caine da Silva", 
                                    "Beauty",
                                    "RedFox2879",
                                    "Jack Flushell",
                                    "Paszmina",
                                    "Sebastien Ligez",
                                    "Sychospy",
                                    "tboc.razor",
                                    "Marcelo Eiras",
									"Nico",
									"Tung Nguyen",
									"mental-hacker",
									"Renn Li",
                                    String.Empty,
                                    LanguageResources.GetString(LanguageKey.CommunitySupport),
                                    "Marcelo Eiras",
                                    "Sandy Siegel",
                                    "Jonathan Rae",
                                    "Winnow Driscoll",
                                    "Detlef Spengler",
                                    "Mr. B.",
                                    "Chompson",
                                    "k3fir",
                                    String.Empty,
                                    LanguageResources.GetString(LanguageKey.WinnerOfFirstDogfight),
                                    "Jack Flushell",
                                    String.Empty,
                                    LanguageResources.GetString(LanguageKey.SpecialThanksToSteveWaldo),
                                    LanguageResources.GetString(LanguageKey.CreatorOfOriginalWingsOfFury),
                                    String.Format(@"{0} 2015", LanguageResources.GetString(LanguageKey.Poland)) 
                                };

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameEventListener"></param>
        /// <param name="framework"></param>
        /// <param name="viewport"></param>
        /// <param name="camera"></param>
        /// <param name="startFromBottom"></param>
        /// <param name="speed"></param>
        public CreditsScreen(GameEventListener gameEventListener,
                              IFrameWork framework, Viewport viewport, Camera camera, bool startFromBottom, float speed) :
                                 base(gameEventListener, framework, viewport, camera, startFromBottom, speed)
        {
          //  this.speed = 145;
        
        }

        protected override int getBackButtonIndex()
        {
            return 0;
        }

        protected override List<Button> buildButtons()
        {        	
            List<Button> ret = new List<Button>();
            ret.Add(guiWindow.createButton(new Vector4(20, (this.messages.Count + 2) * GetTextVSpacing(), Viewport.ActualWidth / 2, GetTextVSpacing()),
                                           "bgui.button", LanguageResources.GetString(LanguageKey.OK), cc));
            return ret;
        }

        protected override List<PositionedMessage> buildMessages()
        {
            
            List<PositionedMessage> ret = new List<PositionedMessage>();
            ret.Add(
            new PositionedMessage(20, 2*GetTextVSpacing(), Viewport.ActualWidth / 2, GetTextVSpacing(),
                                 LanguageResources.GetString(LanguageKey.Credits)));

            foreach (string s in names)
            {
                ret.Add(new PositionedMessage(20, GetTextVSpacing(), Viewport.ActualWidth / 2, GetTextVSpacing(), s));
            }
            return ret;
        }
         
        #region BetaGUIListener Members

        public override void onButtonPress(Button referer)
        {
            if (referer == buttons[backButtonIndex])
            {
                PlayClickSound();
                gameEventListener.GotoStartScreen(referer);
            }
        }

        #endregion
    }
}