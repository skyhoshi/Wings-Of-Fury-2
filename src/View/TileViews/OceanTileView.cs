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
using Mogre;
using Wof.Controller;
using Wof.Misc;
using Wof.Model.Level.LevelTiles;
using Wof.View.Effects;
using Wof.View.VertexAnimation;
using Math=System.Math;

namespace Wof.View.TileViews
{
    /// <summary>
    /// Nie wiem dlaczego ale moze reprezentowac r�wniez endislandtileview
    /// </summary>
    internal class OceanTileView : TileView, Animable
    {
        SceneNode[] floatingNodes;
        public OceanTileView(LevelTile levelTile, IFrameWork framework)
            : base(levelTile, framework)
        {
            floatingNodes = new SceneNode[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        protected void initRocks(Vector3 position)
        {
            SceneNode rockNode;
            Entity rock;
            rockNode = installationNode.CreateChildSceneNode("RockNode" + LevelView.PropCounter, position);
            rock = sceneMgr.CreateEntity("Rock" + LevelView.PropCounter, "Rock.mesh");
            rockNode.AttachObject(rock);
            rockNode.Yaw(Mogre.Math.RangeRandom(0, Mogre.Math.TWO_PI));
        }
        
        
        protected void initSmallIsland(Vector3 position, String mesh)
        {
            SceneNode smallIslandNode;
            Entity island;
            smallIslandNode = installationNode.CreateChildSceneNode("SmallIslandNode" + LevelView.PropCounter, position);
            island = sceneMgr.CreateEntity("SmallIsland" + LevelView.PropCounter, mesh);
            smallIslandNode.AttachObject(island);
            smallIslandNode.Yaw(Mogre.Math.RangeRandom(0, Mogre.Math.TWO_PI));
        }
        
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        protected void initBigRocks(Vector3 position)
        {
            SceneNode rockNode;
            Entity rock;
            rockNode = installationNode.CreateChildSceneNode("BigRockNode" + LevelView.PropCounter, position);
            rock = sceneMgr.CreateEntity("BigRock" + LevelView.PropCounter, "BigRock.mesh");
            rockNode.AttachObject(rock);
            //rockNode.Yaw(Mogre.Math.RangeRandom(0, Mogre.Math.TWO_PI));
            rockNode.Yaw(Mogre.Math.RangeRandom(-1.5f , 1.5f) + Mogre.Math.PI);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="barrelCount">Ilo�� beczek. Max = 4</param>
        protected void initBarrels(Vector3 position, int barrelCount)
        {
            List<SceneNode> floatingNodesL = new List<SceneNode>();
            floatingNodes = new SceneNode[barrelCount];
            Entity entity;
            barrelCount = Math.Min(barrelCount, 4);
            for (int i = 0; i < barrelCount; i++)
            {
                floatingNodesL.Insert(i, installationNode.CreateChildSceneNode("BarrelNode" + LevelView.PropCounter.ToString(), position));
                entity = sceneMgr.CreateEntity("Barrel" + LevelView.PropCounter.ToString(), "Barrel.mesh");
                floatingNodesL[i].AttachObject(entity);

                if (barrelCount == 4)
                {
                    if (i == 1) floatingNodesL[i].Translate(-0.5f, -0.05f, 1.3f);
                    else if (i == 2) floatingNodesL[i].Translate(1.6f, -0.05f, -0.1f);
                    else if (i == 3) floatingNodesL[i].Translate(0.7f, -0.05f, -1.0f);
                }
                else
                {
                    if (i == 1) floatingNodesL[i].Translate(0.3f, -0.05f, 2.5f);
                    else if (i == 2) floatingNodesL[i].Translate(-0.5f, -0.05f, -2.0f);
                   
                }
                floatingNodesL[i].Pitch(new Radian(Mogre.Math.RangeRandom(-Mogre.Math.HALF_PI, Mogre.Math.HALF_PI)));
                floatingNodesL[i].SetScale(Mogre.Math.RangeRandom(1.2f, 1.5f), Mogre.Math.RangeRandom(1.2f, 1.5f), Mogre.Math.RangeRandom(1.2f, 1.5f));
            }

            SceneNode planks = installationNode.CreateChildSceneNode("PlanksNode" + LevelView.PropCounter.ToString(), position + new Vector3(0,0,5));
            planks.SetScale(2, 2, 2);
            entity = sceneMgr.CreateEntity("Planks" + LevelView.PropCounter.ToString(), "Planks.mesh");
            planks.AttachObject(entity);

            floatingNodesL.Add(planks);

            floatingNodes = floatingNodesL.ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tileCMVIndex"></param>
        /// <param name="compositeModelTilesNumber"></param>
        public override void initOnScene(SceneNode parentNode, int tileCMVIndex, int compositeModelTilesNumber)
        {
            base.initOnScene(parentNode, tileCMVIndex, compositeModelTilesNumber);

            /*base.initOnScene(parentNode, tileCMVIndex, compositeModelTilesNumber);
            String nameSuffix = tileID.ToString();

            float positionOnIsland = -getRelativePosition(parentNode, LevelTile);*/

            /*if (levelTile is BarrelTile)
            {
                installationNode =
                    parentNode.CreateChildSceneNode("Barrels" + nameSuffix, new Vector3(0, 0.1f, positionOnIsland - 5.0f));
            */

            float test = getRelativePosition(parentNode, LevelTile);

            if (LevelTile is OceanTile)
            {
                int variant = LevelTile.Variant;
                if(variant >= 0)
                {

                    //installationNode = parentNode.CreateChildSceneNode("OceanNode" + tileID, new Vector3(-getRelativePosition(parentNode, LevelTile), 0, 0));
                    installationNode = parentNode.CreateChildSceneNode("OceanNode" + tileID, new Vector3(getRelativePosition(parentNode, LevelTile)+5.0f, 0, 0));
                }

                switch (variant)
                {
                    case 0:
                        break;
                    case 1:
                        initRocks(new Vector3(0, 0, 0));
                        break;
                    case 2:
                        initRocks(new Vector3(0, 0, Mogre.Math.RangeRandom(-100, 100)));
                        break;

                    case 3:
                        initBigRocks(new Vector3(0, 0, 0));
                        break;

                    case 4:
                        initBigRocks(new Vector3(0, 0, Mogre.Math.RangeRandom(-100, 100)));
                        break;

                    case 5:
                        initBarrels(new Vector3(0, 0, Mogre.Math.RangeRandom(-3, 3)), 2);
                        break;
                        
                    case 6:
                        initSmallIsland(new Vector3(0, -3, Mogre.Math.RangeRandom(-20, 20)), "Island1.mesh");
                        break;
                        
                     case 7:
                        initSmallIsland(new Vector3(0, -3, Mogre.Math.RangeRandom(-20, 20)), "Island3.mesh");
                        break;
                        
                    case 8:
                        initSmallIsland(new Vector3(0, -3, Mogre.Math.RangeRandom(-20, 20)), "Island4.mesh");
                        break;   
                }
            }
        }

        public void updateTime(float timeSinceLastFrame)
        {
            if(EngineConfig.UseHydrax)
            {
                // animuj beczki
                foreach (SceneNode node in floatingNodes)
                {
                    
                    Vector3 worldPos = node._getDerivedPosition();
                    float newY = HydraxManager.Singleton.GetHydrax().GetHeight(worldPos);

                    float diff = (newY - worldPos.y);
                    node.Translate(0,diff,0);
                  
                }
               
            }
           
        }
    }
}