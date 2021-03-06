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

using System.Collections.Generic;
using Mogre;
using Wof.Controller;
using Wof.Misc;
using Wof.Model.Level.LevelTiles;
using Wof.Model.Level.LevelTiles.IslandTiles;
using Wof.View.Effects;
using Wof.View.VertexAnimation;

namespace Wof.View.TileViews
{
    public class MiddleIslandTileView : TileView, VertexAnimable
    {
        protected List<AnimationState> animableElements;


        public MiddleIslandTileView(LevelTile levelTile, IFrameWork framework)
            : base(levelTile, framework)
        {
            animableElements = new List<AnimationState>();
            // flagNode = null;
        }

        protected void initPalm(Vector3 position)
        {
            Entity palm;
            SceneNode palmNode;

            if (EngineConfig.LowDetails)
            {
                palm = sceneMgr.CreateEntity("Palm" + LevelView.PropCounter, "TwoSidedPlane.mesh");
                palm.SetMaterialName("FakePalmTree");
            }
            else
            {
                palm = sceneMgr.CreateEntity("Palm" + LevelView.PropCounter, "PalmTree.mesh");
            }
                
            palm.CastShadows = EngineConfig.ShadowsQuality > 0; 
            palmNode = installationNode.CreateChildSceneNode("PalmNode" + LevelView.PropCounter.ToString(), position);

            if (EngineConfig.LowDetails)
            {
                float angle = Math.RangeRandom(-Math.PI/5, Math.PI/5);

                palmNode.Yaw(Math.HALF_PI + angle);
                palmNode.Scale(0.5f, 1, 1);
                palmNode.Translate(new Vector3(0, 3, 0));
                palmNode.Pitch(-Math.HALF_PI);
                EffectsManager.Singleton.RectangularEffect(sceneMgr, installationNode, "PalmTop" + LevelView.PropCounter, EffectsManager.EffectType.PALMTOP1, position + new Vector3(0.0f, 4.5f, -0.0f), new Vector2(1.8f, 1.8f),
                                  Quaternion.IDENTITY, true).FirstNode.Yaw(angle);

            }
            else
            {
                palmNode.Rotate(Vector3.UNIT_Y, Math.RangeRandom(0.0f, Math.PI));
                palmNode.Scale(1, Math.RangeRandom(0.9f, 1.1f), 1);
                palmNode.Translate(new Vector3(0, -0.3f, 0));
            }
            palmNode.AttachObject(palm);
        }

        protected void initPalm2(Vector3 position)
        {
            Entity palm;
            SceneNode palmNode;

            if (EngineConfig.LowDetails)
            {
                palm = sceneMgr.CreateEntity("Palm" + LevelView.PropCounter.ToString(), "TwoSidedPlane.mesh");
                palm.SetMaterialName("FakePalmTree2");
            }
            else
                palm = sceneMgr.CreateEntity("Palm" + LevelView.PropCounter.ToString(), "PalmTree2.mesh");

            palm.CastShadows = EngineConfig.ShadowsQuality > 0; 
            palmNode = installationNode.CreateChildSceneNode("PalmNode" + LevelView.PropCounter.ToString(), position);

            if (EngineConfig.LowDetails)
            {
              
                float angle = Math.RangeRandom(-Math.PI/4, Math.PI/4);

                palmNode.Yaw(Math.HALF_PI + angle);
                palmNode.Scale(0.5f, 1, 1);
                palmNode.Translate(new Vector3(0, 3, 0));
                palmNode.Pitch(-Math.HALF_PI);
                EffectsManager.Singleton.RectangularEffect(sceneMgr, installationNode, "PalmTop" + LevelView.PropCounter, EffectsManager.EffectType.PALMTOP2, position + new Vector3(0.0f, 4.5f, -0.2f), new Vector2(2.5f, 2.5f),
                                Quaternion.IDENTITY, true).FirstNode.Yaw(angle);
            }
            else
            {
                palmNode.Rotate(Vector3.UNIT_Y, Math.RangeRandom(0.0f, Math.PI));
                palmNode.Scale(1, Math.RangeRandom(0.9f, 1.1f), 1);
                palmNode.Translate(new Vector3(0, -0.3f, 0));
            }

            palmNode.AttachObject(palm);
        }

        protected void initFlag(Vector3 position)
        {
            SceneNode flagNode;
            Entity japanFlag = sceneMgr.CreateEntity("JapanFlag" + LevelView.PropCounter.ToString(), "JapanFlag.mesh");
            flagNode =
                installationNode.CreateChildSceneNode("JapanFlagNode" + LevelView.PropCounter.ToString(), position);
            AnimationState japanFlagState;

            flagNode.Rotate(Vector3.UNIT_Y, Math.PI);
            flagNode.AttachObject(japanFlag);

            japanFlagState = japanFlag.GetAnimationState("idle");
            japanFlagState.Enabled = true;
            japanFlagState.Loop = true;

            animableElements.Add(japanFlagState);
        }

       


        public override void initOnScene(SceneNode parentNode, int tileCMVIndex, int compositeModelTilesNumber)
        {
            base.initOnScene(parentNode, tileCMVIndex, compositeModelTilesNumber);

            if (LevelTile is MiddleIslandTile)
            {
               
                installationNode =
                    parentNode.CreateChildSceneNode("Middle" + tileID.ToString(), new Vector3(0, 0, -getRelativePosition(parentNode, LevelTile)));

                int variant = ((IslandTile) LevelTile).Variant;

                switch (variant)
                {
                    case 0:
                        break;

                    // 3 palmy
                    case 1:
                        initPalm2(new Vector3(-1, 0, -6.5f));
                        initPalm(new Vector3(0.5f, 0, -5.6f));
                        initPalm(new Vector3(-1, 0, -4.4f));
                        break;

                    // 4 palmy
                    case 2:
                        initPalm(new Vector3(-1, 0, -6 ));
                        initPalm2(new Vector3(1, 0, -5 ));
                        initPalm(new Vector3(1, 0, -7 ));
                        initPalm2(new Vector3(-1, 0, -4));
                        break;

                    // flaga
                    case 3:
                        initFlag(new Vector3(0, 0, -4.5f));
                        break;

                    // uniesione 3 palmy
                    case 12:
                        initPalm2(new Vector3(-1, 7.5f, -7.0f));
                        initPalm(new Vector3(0.5f, 7.5f, -5f));
                        initPalm(new Vector3(-1, 7.5f, -4.0f));
                        break;

                    // uniesione 4 palmy
                    case 13:
                        initPalm(new Vector3(-1, 7.5f, -6));
                        initPalm2(new Vector3(1, 7.5f, -5));
                        initPalm(new Vector3(1, 7.5f, -6.5f));
                        initPalm2(new Vector3(-1, 7.5f, -4));
                        break;

                    // uniesiona flaga
                    case 14:
                        initFlag(new Vector3(0, 6.5f, -4.5f));
                        break;


                }
            }
        }

        #region VertexAnimation

        public virtual void updateTime(float timeSinceLastFrame)
        {
            int count = animableElements.Count;
            for (int i = 0; i < count; i++)
            {
                if (!animableElements[i].HasEnded)
                {
                    animableElements[i].AddTime(timeSinceLastFrame);
                }
            }
        }

        public void rewind()
        {
            int count = animableElements.Count;
            for (int i = 0; i < count; i++)
            {
                animableElements[i].TimePosition = 0.0f;
            }
        }

        public void enableAnimation()
        {
            int count = animableElements.Count;
            for (int i = 0; i < count; i++)
            {
                animableElements[i].Enabled = true;
            }
        }

        public void disableAnimation()
        {
            int count = animableElements.Count;
            for (int i = 0; i < count; i++)
            {
                animableElements[i].Enabled = false;
            }
        }

        #endregion
    }
}