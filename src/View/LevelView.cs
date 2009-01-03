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
using Wof.Model.Configuration;
using Wof.Model.Level;
using Wof.Model.Level.Common;
using Wof.Model.Level.LevelTiles;
using Wof.Model.Level.LevelTiles.AircraftCarrierTiles;
using Wof.Model.Level.LevelTiles.IslandTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.EnemyInstallationTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.ExplosiveObjects;
using Wof.Model.Level.LevelTiles.Watercraft;
using Wof.Model.Level.Planes;
using Wof.Model.Level.Troops;
using Wof.Model.Level.Weapon;
using Wof.View.Effects;
using Wof.View.NodeAnimation;
using Wof.View.TileViews;
using Wof.View.VertexAnimation;
using Math=Mogre.Math;
using Plane=Wof.Model.Level.Planes.Plane;

namespace Wof.View
{
    /// <summary>
    /// Klasa reprezentuj�ca poziom gry w warstwie View
    /// <author>Adam Witczak, Kamil S�awi�ski</author>
    /// </summary>
    internal class LevelView
    {
        public int CurrentCameraHolderIndex
        {
            get { return currentCameraHolderIndex; }
        }

        private int currentCameraHolderIndex = 0;

        public SceneNode CurrentCameraHolder
        {
            get
            {
                if (currentCameraHolderIndex >= 0 && currentCameraHolderIndex < cameraHolders.Count)
                {
                    return cameraHolders[currentCameraHolderIndex];
                }
                return null;
            }
        }

        private List<SceneNode> cameraHolders;

        public List<SceneNode> CameraHolders
        {
            get { return cameraHolders; }
        }


        public const int oceanSize = 10000;

        private static readonly float tileWidth = 10.0f;

        public static float TileWidth
        {
            get { return tileWidth; }
        }

        //Fields
        private static float currentTilePositionOnScene;

        public static float CurrentTilePositionOnScene
        {
            get { return currentTilePositionOnScene; }
        }

        /// <summary>
        /// Licznik obiektow dodatkowych - drzew, flag
        /// uzywany do nadawania unikalnych nazw w Ogre
        /// </summary>
        private static int propCounter = 1;

        private static bool isNightScene = false;

        public static bool IsNightScene
        {
            get { return isNightScene; }
        }

        public static int PropCounter
        {
            get { return propCounter++; }
        }

        private static float modelToViewAdjust;

        public static float ModelToViewAdjust
        {
            get { return modelToViewAdjust; }
        }

        //Zbiera Tile dla danego CompositeModelView
        private List<TileView> tempTileViews;

        private static Stack<SceneNode> availableSplashNodesPool;
        private static Queue<SceneNode> usedSplashNodesPool;

        public void Destroy()
        {
            if (availableSplashNodesPool != null)
            {
                availableSplashNodesPool.Clear();
                availableSplashNodesPool = null;
            }

            if (usedSplashNodesPool != null)
            {
                usedSplashNodesPool.Clear();
                usedSplashNodesPool = null;
            }

            if (ammunitionViews != null)
            {
                ammunitionViews.Clear();
                ammunitionViews = null;
            }
            RocketView.DestroyPool(); 
            TorpedoView.DestroyPool();
            BombView.DestroyPool();

            if (backgroundViews != null)
            {
                backgroundViews.Clear();
                backgroundViews = null;
            }

            carrierView = null;

            if (compositeModelViews != null)
            {
                compositeModelViews.Clear();
                compositeModelViews = null;
            }
          
            cameraHolders = null;

            if (planeViews != null)
            {
                planeViews.Clear();
                planeViews = null;
            }
            playerPlaneView = null;

            if (dyingSoldierViews != null)
            {
                dyingSoldierViews.Clear();
                dyingSoldierViews = null;
            }
            if (soldierViews != null)
            {
                soldierViews.Clear();
                soldierViews = null;
            }
            SoldierView.DestroyPool();
        
            if (tempTileViews != null)
            {
                tempTileViews.Clear();
                tempTileViews = null;
            }
            if (tileViews != null)
            {
                tileViews.Clear();
                tileViews = null;
            }
        }

        private void InitSplashPool(int size)
        {
            availableSplashNodesPool = new Stack<SceneNode>(size);
            usedSplashNodesPool = new Queue<SceneNode>(size);

            for (int i = 0; i < size; i++)
            {
                SceneNode s = sceneMgr.RootSceneNode.CreateChildSceneNode("SplashNode" + PropCounter);
                availableSplashNodesPool.Push(s);
            }
        }

        public SceneNode getSplashNode()
        {
            if (availableSplashNodesPool.Count == 0)
            {
                return null;
            }
            SceneNode s = availableSplashNodesPool.Pop();
            usedSplashNodesPool.Enqueue(s);
            return s;
        }

        public void freeSplashNode()
        {
            if (usedSplashNodesPool.Count == 0) return;
            SceneNode s = usedSplashNodesPool.Dequeue();
            availableSplashNodesPool.Push(s);
        }

        //Logic
        private Level level;
        private List<PlaneView> planeViews;

        private List<SoldierView> soldierViews;
        private List<SoldierView> dyingSoldierViews;
        private List<AmmunitionView> ammunitionViews;
        private List<TileView> tileViews;
        private List<CompositeModelView> compositeModelViews;
        private CarrierView carrierView;

        private List<CompositeModelView> backgroundViews;

        private PlayerPlaneView playerPlaneView = null;

        public FrameWork framework;
        protected SceneManager sceneMgr, minimapMgr;

        public SceneManager SceneMgr
        {
            get { return sceneMgr; }
        }

        public SceneManager MinimapMgr
        {
            get { return minimapMgr; }
        }

        protected IController controller;

        private readonly uint defaultVisibilityMask;

        public void SetVisible(bool visible)
        {
            if (visible)
            {
                sceneMgr.VisibilityMask = defaultVisibilityMask;
                if (FrameWork.DisplayMinimap)
                {
                    minimapMgr.VisibilityMask = defaultVisibilityMask;
                }
            }
            else
            {
                sceneMgr.VisibilityMask = 0;
                if (FrameWork.DisplayMinimap)
                {
                    minimapMgr.VisibilityMask = 0;
                }
            }
        }

        public LevelView(FrameWork framework, IController controller)
        {
            isNightScene = false;

            this.framework = framework;
            this.controller = controller;

            sceneMgr = FrameWork.SceneMgr;
            minimapMgr = FrameWork.MinimapMgr;

            defaultVisibilityMask = minimapMgr.VisibilityMask;
            // ukryj cala scene na czas ladowania

            SetVisible(false);

            tileViews = new List<TileView>();
            compositeModelViews = new List<CompositeModelView>();
            tempTileViews = new List<TileView>();

            planeViews = new List<PlaneView>();
            soldierViews = new List<SoldierView>();
            dyingSoldierViews = new List<SoldierView>();
            ammunitionViews = new List<AmmunitionView>();
            backgroundViews = new List<CompositeModelView>();
        }

        public PlaneView FindPlaneView(Plane p)
        {
            if (playerPlaneView != null && playerPlaneView.Plane == p) return playerPlaneView;

            if (p is StoragePlane)
            {
                if (carrierView != null)
                {
                    return carrierView.FindStoragePlaneView(p as StoragePlane);
                }
            }

            return planeViews.Find(delegate(PlaneView pv) { return pv.Plane == p; });
        }

        public TileView FindTileView(LevelTile l)
        {
            return tileViews.Find(delegate(TileView tv) { return tv.LevelTile == l; });
        }

        public ShipView FindShipView(ShipTile t)
        {
            return compositeModelViews.Find(delegate(CompositeModelView c) { 
                if(c is ShipView)
                {
                    foreach (TileView tv in (c as ShipView).TileViews)
                    {
                        if (tv.LevelTile == t) return true;
                    }
                }
               
                return false;
            }) as ShipView;
            
        }


        public void OnRegisterTile(LevelTile levelTile)
        {
            if (EngineConfig.DisplayBoundingQuadrangles && !(levelTile is OceanTile))
            {
                OnRegisterBoundingQuadrangle(levelTile);
            }

            if (levelTile is OceanTile)
            {
                if (tempTileViews.Count > 0)
                {
                    LevelTile lastTileView = tempTileViews[tempTileViews.Count - 1].LevelTile;
                    CompositeModelView cmv = null;

                    if (lastTileView is EndIslandTile)
                    {
                        cmv = new IslandView(tempTileViews, framework, sceneMgr.RootSceneNode);
                        BeginIslandTileView beginTileView = (BeginIslandTileView) tempTileViews[0];

                        int count = beginTileView.BackgroundViews.Count;
                        for (int i = 0; i < count; i++)
                        {
                            CompositeModelView bmv = beginTileView.BackgroundViews[i];
                            backgroundViews.Add(bmv);
                        }
                    }
                    else if (lastTileView is EndAircraftCarrierTile)
                    {
                        carrierView = new CarrierView(tempTileViews, framework, sceneMgr.RootSceneNode);
                        cmv = carrierView;
                    }
                    else if (lastTileView is EndShipTile)
                    {
                        cmv = new ShipView(tempTileViews, framework, sceneMgr.RootSceneNode);
                    }

                    if (cmv != null) compositeModelViews.Add(cmv);

                    tempTileViews = new List<TileView>();
                } else
                {
                    OceanTileView otv = new OceanTileView(levelTile, framework);
                    tileViews.Add(otv);
                    otv.initOnScene(this.sceneMgr.RootSceneNode, levelTile.TileIndex, 1);
                }
            }
            else if (levelTile is IslandTile || levelTile is AircraftCarrierTile || levelTile is ShipTile)
            {
                TileView tileView;
                if (levelTile is BeginIslandTile)
                {
                    tileView = new BeginIslandTileView(levelTile, framework);
                }
                else if (levelTile is BeginShipTile)
                {
                    tileView = new BeginShipTileView(levelTile, framework);
                }
                else if (levelTile is MiddleIslandTile)
                {
                    tileView = new MiddleIslandTileView(levelTile, framework);
                }
                else if (levelTile is EndIslandTile)
                {
                    tileView = new EndIslandTileView(levelTile, framework);
                }
                else if (levelTile is MiddleShipTile)
                {
                     tileView = new MiddleShipTileView(levelTile, framework);
                }
                else if (levelTile is EndShipTile)
                {
                    tileView = new EndShipTileView(levelTile, framework);
                }
                else if (levelTile is BarrackTile)
                {
                    tileView = new BarrackTileView(levelTile, framework);
                }
                else if (levelTile is ShipBunkerTile)
                {
                    tileView = new ShipBunkerTileView(levelTile, framework);
                }
                else if (levelTile is BunkerTile)
                {
                    tileView = new BunkerTileView(levelTile, framework);
                }
                
                else if (levelTile is BarrelTile)
                {
                    tileView = new BarrelTileView(levelTile, framework);
                }
                else
                {
                    tileView = new OceanTileView(levelTile, framework);
                }

                //Na uzytek LevelView
                tileViews.Add(tileView);

                //Na uzytek CompositeModelView
                tempTileViews.Add(tileView);
            }
            currentTilePositionOnScene += TileWidth;
        }

        public void OnRegisterSoldier(Soldier soldier)
        {
            soldierViews.Add(SoldierView.GetInstance(soldier));
        }

        private int FindSoldierViewIndex(Soldier soldier)
        {
            // TODO: predykaty
            int count = soldierViews.Count;
            for (int i = 0; i < count; i++)
            {
                if (soldierViews[i].Soldier.Equals(soldier)) return i;
            }
            return -1;
        }

        public void OnUnregisterSoldier(Soldier soldier)
        {
            int index = FindSoldierViewIndex(soldier);
            if (index != -1)
            {
                SoldierView soldierView = soldierViews[index];

                soldierViews.Remove(soldierView);
                SoldierView.FreeInstance(soldier, true);
            }
        }


        public void OnKillSoldier(Soldier soldier, Boolean dieFromExplosion, bool scream)
        {
            int index = FindSoldierViewIndex(soldier);
            if (index == -1) return;

            SoldierView soldierView = soldierViews[index];
            if(scream) soldierView.PlaySoldierDeathSound();

            soldierViews.Remove(soldierView);
            dyingSoldierViews.Add(soldierView);

            if (dieFromExplosion)
            {
                if (Math.RangeRandom(0.0f, 1.0f) < 0.7f)
                {
                    soldierView.DieFromExplosion();
                }
                else
                {
                    soldierView.DieFromGun();
                }
            }
            else
            {
                if (Math.RangeRandom(0.0f, 1.0f) < 0.7f)
                {
                    soldierView.DieFromGun();
                }
                else
                {
                    soldierView.DieFromExplosion();
                }
            }
        }

        public void OnRegisterAmmunition(Ammunition ammunition)
        {
            if (ammunition is Bomb)
            {
                ammunitionViews.Add(BombView.GetInstance(ammunition));
            }
            else if (ammunition is Rocket)
            {
                ammunitionViews.Add(RocketView.GetInstance(ammunition));
            }

            else if (ammunition is Torpedo)
            {
                ammunitionViews.Add(TorpedoView.GetInstance(ammunition, this));
            }
        }

        public void OnLoopEnemyPlaneEngineSound(EnemyPlane plane)
        {
            EnemyPlaneView pv = (EnemyPlaneView)FindPlaneView(plane);
            pv.LoopEngineSound();
        }

        public void OnLoopEnemyPlaneEngineSounds()
        {
            for (int i = 0; i < planeViews.Count; i++)
            {
                if (planeViews[i] is EnemyPlaneView) 
                    ((EnemyPlaneView)planeViews[i]).LoopEngineSound();
            }
        }

        public void OnStopPlayingEnemyPlaneEngineSound(EnemyPlane plane)
        {
            EnemyPlaneView pv = (EnemyPlaneView)FindPlaneView(plane);
            pv.StopEngineSound();
        }

        public void OnStopPlayingEnemyPlaneEngineSounds()
        {
            for (int i = 0; i < planeViews.Count; i++)
            {
                if (planeViews[i] is EnemyPlaneView)
                    ((EnemyPlaneView)planeViews[i]).StopEngineSound();
            }
        }

        public void OnRegisterPlane(Plane plane)
        {
            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                OnRegisterBoundingQuadrangle(plane);
            }

            if (plane is StoragePlane)
            {
                if (carrierView != null)
                {
                    carrierView.InitStoragePlaneOnCarrier(plane as StoragePlane);
                }
                else
                {
                    // error
                }
            }
            else
            {
                if (plane.IsEnemy)
                {
                    planeViews.Add(new EnemyPlaneView(plane, sceneMgr, sceneMgr.RootSceneNode));
                }
                else
                {
                    playerPlaneView = new PlayerPlaneView(plane, sceneMgr, sceneMgr.RootSceneNode);
                }
            }
        }

        public void OnUnregisterPlane(Plane plane)
        {
            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                OnUnregisterBoundingQuadrangle(plane);
            }

            // TODO: na razie samoloty sa ukrywane
            if (plane is StoragePlane)
            {
                if (carrierView != null)
                {
                    carrierView.DestoryStoragePlane((plane as StoragePlane).Tile);
                }
                else
                {
                    // error
                }
                return;
            }


            PlaneView p = FindPlaneView(plane);
            if (p != null)
            {
                p.PlaneNode.SetVisible(false);
                if (p is P47PlaneView)
                {
                    (p as P47PlaneView).StopSmokeTrails();
                }
                if(p.MinimapItem !=null) p.MinimapItem.Hide();
            }
            else
            {
                // error
            }
        }

        public void OnBunkerFire(BunkerTile bunker, Plane plane)
        {
            PlaneView p = FindPlaneView(plane);

            EffectsManager.Singleton.Sprite(
                sceneMgr,
                p.OuterNode,
                ViewHelper.RandomVector3(5),
                new Vector2(2, 2) + ViewHelper.UnsignedRandomVector2(5),
                EffectsManager.EffectType.EXPLOSION1,
                false,
                (uint) bunker.GetHashCode()
                );


            TileView t = FindTileView(bunker);
            if (t is EnemyInstallationTileView)
            {
                EnemyInstallationTileView tv = (t as EnemyInstallationTileView);
                tv.GunFire();

                // TODO: obracac wybuch razem z dzialkiem
             
            }
        }

        private int FindAmmunitionViewIndex(Ammunition ammunition)
        {
            int count = ammunitionViews.Count;
            for (int i = 0; i < count; i++)
            {
                if (ammunitionViews[i].Ammunition.Equals(ammunition)) return i;
            }
            return -1;
        }

        /// <summary>
        /// Oblicza lokalny wektor (wzgledem outernode) skierowany do gory
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector3 SmokeUpVector(PlaneView p)
        {
            // niestety co� nie dzia�a w przypadku storage planes. 'R�cznie' b�d� dymi� do g�ry.
            if(p.Plane != null && p.Plane is StoragePlane)
            {
                return new Vector3(0,1,0);
            }


            Vector3 smokeUp = Vector3.NEGATIVE_UNIT_Z;
            Quaternion q = smokeUp.GetRotationTo(p.OuterNode.WorldOrientation*Vector3.NEGATIVE_UNIT_Z);
            smokeUp = q*smokeUp;
            smokeUp = new Quaternion(new Degree(90), Vector3.NEGATIVE_UNIT_Z)*smokeUp;
            if (p.Plane.Direction == Direction.Left)
            {
                smokeUp = new Vector3(smokeUp.z, smokeUp.y, -smokeUp.x);
            }
            else
            {
                smokeUp = new Vector3(smokeUp.z, -smokeUp.y, -smokeUp.x);
            }
            return smokeUp;
        }


        public void OnPlaneDestroyed(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            if(p == null) return; //error

            EffectsManager.Singleton.Sprite(sceneMgr, p.PlaneNode, new Vector3(0, 0, 0), new Vector2(20, 20),
                                            EffectsManager.EffectType.PLANECRASH, false);

            if (p.IsSmokingSlightly)
            {
                EffectsManager.Singleton.NoSmoke(sceneMgr, p.OuterNode, EffectsManager.SmokeType.LIGHTSMOKE);
                p.IsSmokingSlightly = false;
            }

            p.IsSmokingHeavily = false; // aby updateplaneview nie wylaczyl zaraz dymu
            EffectsManager.Singleton.Smoke(sceneMgr, p.OuterNode, Vector3.ZERO, SmokeUpVector(p));


            // korkoci�g
            if (p.AnimationMgr.CanStopAnimation && Math.RangeRandom(0.0f, 1.0f) > 0.5)
            {
                p.AnimationMgr.switchToDeathSpin(true, null, null);
            }

            if (p is PlayerPlaneView) EngineConfig.SpinKeys = false;


            p.Smash(); // vertex animation
        }

       
        public void OnPlaneCrashed(Plane plane, TileKind tileKind)
        {
            PlaneView p = FindPlaneView(plane);
            if(p==null) return; //error
            switch (tileKind)
            {
                case TileKind.Ocean:
                {
                    float adjustSpeedFactor = plane.Speed - 24; //24 predkosc minimalna samolotu
                    float cos = Math.Cos(plane.Angle);
                    float cos2 = Math.Cos(plane.Angle + 0.1f);
                    float adjust = (1.4f + cos*3.8f) + adjustSpeedFactor*cos2*0.2f;
                    Vector3 posView =
                        new Vector3(
                            UnitConverter.LogicToWorldUnits(plane.Position).x +
                            ((plane.Direction == Direction.Left) ? -adjust : adjust), 0.5f, 0);

                    if (!EngineConfig.LowDetails)
                        EffectsManager.Singleton.RectangularEffect(sceneMgr, sceneMgr.RootSceneNode,
                                                                   "Submerge" + plane.GetHashCode(),
                                                                   EffectsManager.EffectType.SUBMERGE, posView,
                                                                   new Vector2(25, 25), Quaternion.IDENTITY, false);
                    EffectsManager.Singleton.WaterImpact(sceneMgr, sceneMgr.RootSceneNode, posView,
                                                         new Vector2(20, 32), false, "");
                }
                break;
                case TileKind.Ship:
                case TileKind.Island:
                case TileKind.AircraftCarrier:
                {
                  
                }
                break;
            }

            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                OnUnregisterBoundingQuadrangle(plane);
            }

            if (p is P47PlaneView)
            {
                (p as P47PlaneView).StopSmokeTrails();
            }

            // jesli model zglosil prosbe o zawracanie to nie nalezy przerywac animacji. UpdatePlaneView musi odpalic animacje zawracania i wyslac komunikat do modelu ze zawracanie rozpoczete
            if (p.AnimationMgr.CanStopAnimation)
            {
                p.AnimationMgr.disableAll();
            }

            if (p is PlayerPlaneView) EngineConfig.SpinKeys = false;


            EffectsManager.Singleton.Smoke(sceneMgr, p.OuterNode, Vector3.ZERO, SmokeUpVector(p));
            p.IsSmokingHeavily = false; // aby updateplaneview nie wylaczylo dymu
        }


        public void OnUnregisterRocket(Rocket rocket)
        {
            OnAmmunitionExplode(null, rocket);
        }

        public void OnUnregisterTorpedo(Torpedo torpedo)
        {
            OnAmmunitionExplode(null, torpedo);
        }


      


        public void OnEnemyPlaneBombed(Plane plane, Ammunition ammunition)
        {
            // PlaneView p = FindPlaneView(plane);
            OnAmmunitionExplode(null, ammunition);
        }


        /// <summary>
        /// Torpeda trafia w wod�
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="torpedo"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public void OnTorpedoHitGroundOrWater(LevelTile tile, Torpedo torpedo, float posX, float posY)
        {
            
            int index = FindAmmunitionViewIndex(torpedo);
            if (index == -1) return;
            AmmunitionView av = ammunitionViews[index];
            uint hash = (uint) tile.GetHashCode();
            
            SceneNode splashNode = getSplashNode();
            if (splashNode == null) return; // koniec poola

            // plusk
            NodeAnimation.NodeAnimation na;
           
            bool ocean = false;
            if (tile is OceanTile)
            {
                ocean = true;
            }
            EffectsManager.EffectType type;
            if (ocean)
            {
                type = EffectsManager.EffectType.WATERIMPACT2;
            }
            else
            {
                type = EffectsManager.EffectType.DIRTIMPACT1;
            }
           
            
            Vector3 position =
                new Vector3(posX + ModelToViewAdjust, posY, 0);

           
            na =
               EffectsManager.Singleton.RectangularEffect(sceneMgr, splashNode, type.ToString(), type, position,
                                                          new Vector2(4, 4), Quaternion.IDENTITY, false);
            na.Node.Orientation = new Quaternion(Math.HALF_PI, Vector3.UNIT_X);
            na.Node.Orientation *= new Quaternion(Math.RangeRandom(-0.1f, 0.1f) * Math.HALF_PI, Vector3.UNIT_Y);

         
            
            na.onFinishInfo = na.Node;
            na.onFinish = onFreeSplashNode;
            
        }

      


        public void OnAmmunitionExplode(LevelTile tile)
        {
            SceneNode splashNode = getSplashNode();
            if (splashNode == null) return; // koniec poola
            
            Vector3 position =
               new Vector3(Mathematics.IndexToPosition(tile.TileIndex) + ModelToViewAdjust, (tile.YBegin + tile.YEnd) / 2.0f, 0);

            NodeAnimation.NodeAnimation na;
            na = EffectsManager.Singleton.Sprite(
                   sceneMgr,
                   splashNode,
                   position,
                   new Vector2(3, 3) + ViewHelper.UnsignedRandomVector2(5),
                   EffectsManager.EffectType.EXPLOSION2,
                   false,
                   (uint)splashNode.GetHashCode()
                   );

            na.onFinishInfo = na.Node;
            na.onFinish = onFreeSplashNode;


        }
        public void OnAmmunitionVanish(LevelTile tile, Ammunition ammunition)
        {

            int index = FindAmmunitionViewIndex(ammunition);
            if (index == -1) return;
            AmmunitionView av = ammunitionViews[index];
            ammunitionViews.RemoveAt(index);
            av.Hide();


            if (av is RocketView)
            {
                RocketView.FreeInstance(ammunition);
            }
            else if (av is TorpedoView)
            {
                TorpedoView.FreeInstance(ammunition);
            }
            else
            {
                BombView.FreeInstance(ammunition);
            }
            if (EngineConfig.ExplosionLights && IsNightScene) av.ExplosionFlash.Visible = false;

        }

        /// <summary>
        /// Wybuch pocisku.
        /// </summary>
        /// <param name="tile">Tile na ktorych nastepuje wybuch. Wartosc moze byc null'em</param>
        /// <param name="ammunition"></param>
        public void OnAmmunitionExplode(LevelTile tile, Ammunition ammunition)
        {
            if (ammunition == null)
            {
                OnAmmunitionExplode(tile);
                return;
            }

            int index = FindAmmunitionViewIndex(ammunition);
            if (index == -1) return;
            AmmunitionView av = ammunitionViews[index];

            uint hash;
            bool ocean = false;

            if (tile != null)
            {
                hash = (uint) tile.GetHashCode();
                if (tile is OceanTile) ocean = true;
            }
            else
            {
                hash = 1;
            }

            NodeAnimation.NodeAnimation na;

            if (ocean)
            {
                na = EffectsManager.Singleton.Sprite(
                    sceneMgr,
                    av.AmmunitionNode,
                    new Vector3(0, 0.5f, (ammunition.Direction == Direction.Left) ? -1.55f : -1.65f),
                    new Vector2(3, 3) + ViewHelper.UnsignedRandomVector2(5),
                    EffectsManager.EffectType.WATERIMPACT2,
                    false,
                    hash
                    );
               
            }
            else
            {
                na = EffectsManager.Singleton.Sprite(
                    sceneMgr,
                    av.AmmunitionNode,
                    new Vector3(0, 0.5f, 0),
                    new Vector2(3, 3) + ViewHelper.UnsignedRandomVector2(5),
                    EffectsManager.EffectType.EXPLOSION2,
                    false,
                    hash
                    );
            }

           
            ammunitionViews.RemoveAt(index);
            av.Hide();
            
          
            if (!ocean && EngineConfig.ExplosionLights && IsNightScene)
            {
                // TODO: czas �wiecenia taki jak d�ugo�� efektu EffectsManager.EffectType.EXPLOSION2
                NodeAnimation.NodeAnimation ani =
                    EffectsManager.Singleton.AddCustomEffect(
                        new SinLightAttenuationAnimation(av.ExplosionFlash, 1.0f, 1.0f, Math.PI,
                                                         "LightAnimation" + av.GetHashCode()));
                ani.Enabled = true;
                ani.rewind();
                ani.Looped = false;
                av.ExplosionFlash.Visible = true;
            }

            na.onFinishInfo = new Object[4];
            Object[] args = (Object[]) na.onFinishInfo;
            args[0] = av;
            args[1] = ocean;
            args[2] = hash;
            args[3] = ammunition;

            na.onFinish = onAmmunitionExplodeFinish;
        }

      

        private void onAmmunitionExplodeFinish(Object o)
        {
            Object[] args = (Object[]) o;
            AmmunitionView av = (AmmunitionView) args[0];
            Boolean ocean = (Boolean) args[1];
            uint hash = (uint) args[2];
            Ammunition ammunition = (Ammunition) args[3];

            EffectsManager.Singleton.HideSprite(
                sceneMgr,
                av.AmmunitionNode,
                ocean ? EffectsManager.EffectType.WATERIMPACT2 : EffectsManager.EffectType.EXPLOSION2,
                hash
                );

            if (av is RocketView)
            {
                RocketView.FreeInstance(ammunition);
            }else if (av is TorpedoView)
            {
                TorpedoView.FreeInstance(ammunition);
            }
            else
            {
                BombView.FreeInstance(ammunition);
            }
            if (EngineConfig.ExplosionLights && IsNightScene) av.ExplosionFlash.Visible = false;
        }

        public void OnTileRestored(BunkerTile restoredBunker)
        {
            TileView v = FindTileView(restoredBunker);
            if (!(v is BunkerTileView)) return; // error
            (v as BunkerTileView).Restore();
        }

        protected void onUnregisterShip(BeginShipTile tile)
        {
            ShipView sv = FindShipView(tile);
            if (sv == null) return;

            sv.MainNode.SetVisible(false);
            if (sv.MinimapItem != null) sv.MinimapItem.Hide();
            
       
        }

        public void OnShipSunk(BeginShipTile tile)
        {
            onUnregisterShip(tile);
        }


        public void OnShipSinking(ShipTile tile)
        {
            ShipView sv  = FindShipView(tile);
            
            if(sv == null) return;


            Vector2 v = UnitConverter.LogicToWorldUnits(new PointD(Mathematics.IndexToPosition(tile.TileIndex), 0.5f));
            string name;
            if (!EngineConfig.LowDetails)
            {

                for (int i = 0; i < 3; i++ )
                {

                    Vector2 rand = ViewHelper.RandomVector2(8, 8);
                    Vector3 posView = new Vector3(v.x + rand.x, v.y, 0 + rand.y);
                    name = EffectsManager.BuildRectangularEffectName(sceneMgr.RootSceneNode, "Submerge" + tile.GetHashCode() + "_" + i);
                    if (!EffectsManager.Singleton.EffectExists(name) || EffectsManager.Singleton.EffectEnded(name))
                    {
                        EffectsManager.Singleton.RectangularEffect(sceneMgr, sceneMgr.RootSceneNode,
                                                                   "Submerge" + tile.GetHashCode() + "_" + i,
                                                                   EffectsManager.EffectType.SUBMERGE, posView,
                                                                   new Vector2(25, 25), Quaternion.IDENTITY, false);
                    }

                    name = EffectsManager.BuildRectangularEffectName(sceneMgr.RootSceneNode, "WaterImpact1_" + tile.GetHashCode() + "_" + i);
                    if (!EffectsManager.Singleton.EffectExists(name))
                    {
                        EffectsManager.Singleton.WaterImpact(sceneMgr, sceneMgr.RootSceneNode, posView, new Vector2(20, 32), false, tile.GetHashCode() + "_" + i);
                    }

                    
                }
                
            }
                
            

           
        }

        public void OnTileDestroyed(LevelTile tile)
        {
            if (tile is ConcreteBunkerTile || tile is WoodBunkerTile || tile is ShipBunkerTile)
            {
                EnemyInstallationTileView bunker = (EnemyInstallationTileView)FindTileView(tile);
                if (bunker == null) return; // error
                bunker.Destroy();
            }
            else if (tile is BarrackTile)
            {
                BarrackTileView barrack = (BarrackTileView)FindTileView(tile);
                if (barrack == null) return; // error
                barrack.Destroy();
            }
            else if (tile is BarrelTile)
            {
                BarrelTileView barrel = (BarrelTileView)FindTileView(tile);
                if (barrel == null) return; // error
                barrel.Destroy();
            } else if(tile is ShipTile)
            {
                /*
                ShipView sv = FindShipView(tile as ShipTile);
                foreach(TileView tv in sv.TileViews)
                {
                    if(tv is ShipBunkerTileView)
                    {
                        (tv as ShipBunkerTileView).Destroy(true, true, true);
                    }
                }*/
            }

        }

        public void OnWarCry(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            if(p is EnemyPlaneView)
            {
                (p as EnemyPlaneView).PlayWarcry();
            }
        }

        public void OnFireGun(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            Quaternion orient = new Quaternion(-Math.HALF_PI, Vector3.UNIT_Y); 
            orient *= new Quaternion(-Math.HALF_PI, Vector3.UNIT_X);

            Quaternion trailOrient = new Quaternion(-Math.HALF_PI, Vector3.UNIT_Y);
            trailOrient *= new Quaternion(-Math.HALF_PI, Vector3.UNIT_X);
            trailOrient *= new Quaternion(Math.HALF_PI * -0.05f, Vector3.UNIT_Y);

            
            
            EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "LeftGunHit",
                                                       EffectsManager.EffectType.GUNHIT2,
                                                       new Vector3(-4.3f, -0.3f, -5.3f), new Vector2(4.5f, 3.5f),
                                                       orient, false);


            EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "RightGunHit",
                                                       EffectsManager.EffectType.GUNHIT2,
                                                       new Vector3(4.3f, -0.3f, -5.3f), new Vector2(4.5f, 3.5f),
                                                       orient, false);


            float trailWidth = 64.0f*Math.RangeRandom(1.0f, 1.1f);
            string leftTrailName = EffectsManager.BuildRectangularEffectName(p.OuterNode, "LeftGunTrail");
            string rightTrailName = EffectsManager.BuildRectangularEffectName(p.OuterNode, "RightGunTrail");
            bool showLeftTrail = EffectsManager.Singleton.EffectEnded(leftTrailName) || !EffectsManager.Singleton.EffectExists(leftTrailName);
            bool showRightTrail = EffectsManager.Singleton.EffectEnded(rightTrailName) || !EffectsManager.Singleton.EffectExists(rightTrailName);

            if(p is PlayerPlaneView)
            {
                 // nie odpalaj za kazdym razem 
                 showLeftTrail &= (Math.RangeRandom(0.0f, 1.0f) > 0.4f);
                 showRightTrail &= (Math.RangeRandom(0.0f, 1.0f) > 0.4f);
            }

            showLeftTrail  |= (Math.RangeRandom(0.0f, 1.0f) > 0.95f); // czasem przerwij efekt i zacznij od poczatku
            showRightTrail |= (Math.RangeRandom(0.0f, 1.0f) > 0.95f); // czasem przerwij efekt i zacznij od poczatku
           
           
            Vector3 leftTrailBase = new Vector3(-4.3f, -3.0f, -5.3f - trailWidth * 0.5f);
            Vector3 rightTrailBase = new Vector3(4.3f, -3.0f, -5.3f - trailWidth * 0.5f);
            
            if (showLeftTrail)
            {
                EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "LeftGunTrail",
                                                           EffectsManager.EffectType.GUNTRAIL,
                                                           leftTrailBase - new Vector3(0, 0, Math.RangeRandom(0.5f, 10.0f)),
                                                           new Vector2(trailWidth, 1.0f),
                                                           trailOrient, false);
            }


            if (showRightTrail)
            {
                EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "RightGunTrail",
                                                           EffectsManager.EffectType.GUNTRAIL,
                                                           rightTrailBase - new Vector3(0, 0, Math.RangeRandom(0.5f, 10.0f)),
                                                           new Vector2(trailWidth, 1.0f),
                                                           trailOrient, false);
            }


            orient *= new Quaternion(Math.HALF_PI, Vector3.UNIT_X);
            trailOrient *= new Quaternion(Math.HALF_PI, Vector3.UNIT_X);
            EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "LeftGunHitTop",
                                                       EffectsManager.EffectType.GUNHIT2,
                                                       new Vector3(-4.3f, -0.3f, -5.3f), new Vector2(4.5f, 3.5f),
                                                       orient, false);

            EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "RightGunHitTop",
                                                       EffectsManager.EffectType.GUNHIT2,
                                                       new Vector3(4.3f, -0.3f, -5.3f), new Vector2(4.5f, 3.5f),
                                                       orient, false);
            

            if (showLeftTrail)
            {
                EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "LeftGunTrailTop",
                                                           EffectsManager.EffectType.GUNTRAIL,
                                                           leftTrailBase - new Vector3(0, 0, Math.RangeRandom(0.5f, 10.0f)),
                                                           new Vector2(trailWidth, 1.0f),
                                                           trailOrient, false);
            }
            if (showRightTrail)
            {
                EffectsManager.Singleton.RectangularEffect(sceneMgr, p.OuterNode, "RightGunTrailTop",
                                                           EffectsManager.EffectType.GUNTRAIL,
                                                           rightTrailBase - new Vector3(0, 0, Math.RangeRandom(0.5f, 10.0f)),
                                                           new Vector2(trailWidth, 1.0f),
                                                           trailOrient, false);
            }


        }

        public void OnGunHitPlane(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            if (p == null) return; //errro
            for (uint i = 1; i <= 3; i++)
            {
                String name = EffectsManager.BuildSpriteEffectName(p.OuterNode, EffectsManager.EffectType.DEBRIS, 100 + i);
                if(name != null)
                {
                    NodeAnimation.NodeAnimation anim = EffectsManager.Singleton.GetEffect(name);
                    if(anim != null && !anim.Ended)
                    {
                        continue; // poprzedni efekt jeszcze jest odtwarzany
                    }
                }

                EffectsManager.Singleton.Sprite(sceneMgr, p.OuterNode,
                                                new Vector3(-5f, -1.5f, -2) +
                                                ViewHelper.UnsignedRandomVector3(5, 1.5f, 4), new Vector2(12, 12),
                                                EffectsManager.EffectType.DEBRIS, false, 100 + i);
            }
        }

        public void OnBeginSpin(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            if (p == null)
            {
                // ERROR 
                return;
            }

            p.AnimationMgr.PrepareToSpin = true;

            if (p.AnimationMgr.CurrentAnimation != null)
            {
                p.AnimationMgr.CurrentAnimation.Looped = false;
            }
        }

        public void OnPrepareChangeDirection(Direction newDirection, Plane plane, TurnType turnType)
        {
            PlaneView p = FindPlaneView(plane);
            if (p == null)
            {
                // ERROR 
                return;
            }

            if (turnType == TurnType.Airborne)
            {
                p.AnimationMgr.PrepareToChangeDirection = true;
            }
            if (turnType == TurnType.Carrier)
            {
                p.AnimationMgr.PrepareToChangeDirectionOnCarrier = true;
            }

            if (p.AnimationMgr.CurrentAnimation != null)
            {
                p.AnimationMgr.CurrentAnimation.Looped = false;
            }
        }

        private void UpdatePlaneView(PlaneView p, float timeSinceLastFrame)
        {
            p.AnimationMgr.updateTimeAll(timeSinceLastFrame);
            p.AnimationMgr.animateAll();
            p.refreshPosition();

            (p as VertexAnimable).updateTime(timeSinceLastFrame);

            if (p.Plane != null)
            {
           
                // powieszona torpeda
                if (p.Plane.Weapon.SelectWeapon == WeaponType.Torpedo && p.Plane.Weapon.IsTorpedoAvailable && p.Plane.IsNextTorpedoAvailable)
                {
                    p.ShowTorpedo();
                }
                else
                {
                    p.HideTorpedo();
                }
                

                // slad na wodzie
                if (p.IsReadyForLastWaterTrail)
                {
                   

                    SceneNode splashNode = getSplashNode();
                    if (splashNode != null) // koniec poola
                    {
                        NodeAnimation.NodeAnimation na;
                        Vector3 position = new Vector3(p.PlaneNode.Position.x, 0.25f, 0);

                        na =
                            EffectsManager.Singleton.RectangularEffect(sceneMgr, splashNode, "PlaneWaterTrail",
                                                                       EffectsManager.EffectType.PLANEWATERTRAIL,
                                                                       position, new Vector2(5, 3), Quaternion.IDENTITY,
                                                                       false);
                        //na.Node.Orientation = new Quaternion(Mogre.Math.HALF_PI, Vector3.UNIT_X);
                        //na.Node.Orientation *= new Quaternion(Mogre.Math.RangeRandom(-0.2f, 0.2f) * Mogre.Math.HALF_PI, Vector3.UNIT_Y);
                        na.onFinishInfo = na.Node;
                        na.onFinish = onFreeSplashNode;
                        p.LastWaterTrailTime = Environment.TickCount;
                    }
                }

                // Dym
                if (p.Plane.Oil < p.Plane.MaxOil && p.PlaneNode.WorldPosition.y >= 0)
                {
                    // slaby dym
                    if (p.Plane.Oil < (p.Plane.MaxOil*0.9f))
                    {
                        if (!p.IsSmokingSlightly && !p.IsSmokingHeavily)
                        {
                            EffectsManager.Singleton.Smoke(sceneMgr, p.OuterNode, EffectsManager.SmokeType.LIGHTSMOKE,
                                                           Vector3.ZERO, Vector3.UNIT_Z, Vector2.ZERO);
                            p.IsSmokingSlightly = true;
                        }
                    }
                    // mocny dym
                    if (p.Plane.Oil < (p.Plane.MaxOil*0.25f))
                    {
                        if (!p.IsSmokingHeavily) // optymalizacja zeby za kazdym razem nie wywolywac
                        {
                            EffectsManager.Singleton.NoSmoke(sceneMgr, p.OuterNode, EffectsManager.SmokeType.LIGHTSMOKE);
                            EffectsManager.Singleton.Smoke(sceneMgr, p.OuterNode, Vector3.ZERO, Vector3.UNIT_Z);
                            p.SmashPaint();
                           // if (!EngineConfig.LowDetails)
                          //      ViewHelper.ReplaceMaterial(p.PlaneEntity, "P47/Body", "P47/DestroyedBody");
                            p.IsSmokingHeavily = true;
                            p.IsSmokingSlightly = false;
                        }
                    }
                }
                else
                {
                    // brak dymu
                    if (p.IsSmokingHeavily || p.IsSmokingSlightly)
                    {
                        EffectsManager.Singleton.NoSmoke(sceneMgr, p.OuterNode, EffectsManager.SmokeType.NORMAL);
                        EffectsManager.Singleton.NoSmoke(sceneMgr, p.OuterNode, EffectsManager.SmokeType.LIGHTSMOKE);
                        p.RestorePaint();
                     //   if (!EngineConfig.LowDetails)
                     //       ViewHelper.ReplaceMaterial(p.PlaneEntity, "P47/DestroyedBody", "P47/Body");
                        p.IsSmokingHeavily = false;
                        p.IsSmokingSlightly = false;
                    }
                }
                // smiglo              
                p.AnimationMgr.changeBladeSpeed(p.Plane.AirscrewSpeed);
                if (p is P47PlaneView)
                {
                    if (p.Plane.AirscrewSpeed < 100)
                    {
                        (p as P47PlaneView).SwitchToSlowEngine();
                    }
                    else
                    {
                        (p as P47PlaneView).SwitchToFastEngine();
                    }
                }

                // smuga dymu
                if (p is P47PlaneView)
                {
                    P47PlaneView p47 = (p as P47PlaneView);
                    if (!p47.HasSmokeTrail && p47.ShouldHaveSmokeTrail)
                    {
                        p47.StartSmokeTrails();
                    }
                    if (p47.HasSmokeTrail && !p47.ShouldHaveSmokeTrail)
                    {
                        p47.StopSmokeTrails();
                    }
                }
            }

            // rozpocznij obracanie z 'plec�w na brzuch' je�li zakolejkowany i zako�czy�a si� poprzednia animacja
            if (p.AnimationMgr.PrepareToSpin)
            {
                // jesli samolot jest juz rozbity to nie ma co go obraca�
                if (p.Plane.PlaneState == PlaneState.Crashed)
                {
                    p.AnimationMgr.PrepareToSpin = false;
                }
                else if (p.AnimationMgr.CurrentAnimation.Ended) // zacznij obraca� jak zako�czysz stara animacje
                {
                    p.AnimationMgr.switchToSpin(true, null, controller.OnSpinEnd, p.Plane , true);
                    p.AnimationMgr.PrepareToSpin = false;
                }
            }

            // je�li zako�czy� si� obr�t powr�� do IDLE
            if (p.AnimationMgr.isCurrentAnimation(PlaneNodeAnimationManager.AnimationType.SPIN_PHASE_TWO))
            {
                if (p.AnimationMgr.CurrentAnimation == null || p.AnimationMgr.CurrentAnimation.Ended ||
                    !p.AnimationMgr.CurrentAnimation.Enabled)
                {
                    EngineConfig.SpinKeys = false;
                    p.AnimationMgr.switchToIdle(true);
                }
            }

            // rozpocznij zakr�canie je�li zakolejkowano i zako�czy�a si� poprzednia animacja.
            if (p.AnimationMgr.PrepareToChangeDirection)
            {
                // jesli samolot jest juz rozbity to nie ma co go zawracac
                if (p.Plane.PlaneState == PlaneState.Crashed)
                {
                    p.AnimationMgr.PrepareToChangeDirection = false;
                }
                else if (p.AnimationMgr.CurrentAnimation.Ended) // zacznij zawracac jak zakonczysz stara animacje
                {
                    p.AnimationMgr.switchToTurn(true, controller.OnPrepareChangeDirectionEnd,
                                                controller.OnChangeDirectionEnd);
                    p.AnimationMgr.PrepareToChangeDirection = false;
                }
            }
            // je�li zako�czy� si� obr�t powr�� do IDLE
            if (p.AnimationMgr.isCurrentAnimation(PlaneNodeAnimationManager.AnimationType.OUTERTURN))
            {
                if (p.AnimationMgr.CurrentAnimation == null || p.AnimationMgr.CurrentAnimation.Ended ||
                    !p.AnimationMgr.CurrentAnimation.Enabled)
                {
                    p.AnimationMgr.switchToIdle(true);
                }
            }

            // analogicznie do p.AnimationMgr.PrepareToChangeDirection
            if (p.AnimationMgr.PrepareToChangeDirectionOnCarrier)
            {
                if (p.Plane.PlaneState == PlaneState.Crashed)
                {
                    p.AnimationMgr.PrepareToChangeDirectionOnCarrier = false;
                }
                else if (p.AnimationMgr.CurrentAnimation == null || p.AnimationMgr.CurrentAnimation.Ended ||
                         !p.AnimationMgr.CurrentAnimation.Enabled)
                {
                    p.AnimationMgr.switchToTurnOnCarrier(true, controller.OnPrepareChangeDirectionEnd,
                                                         controller.OnChangeDirectionEnd);
                    p.AnimationMgr.PrepareToChangeDirectionOnCarrier = false;
                }
            }
            // je�li zako�czy� si� obr�t powr�� do IDLE
            if (p.AnimationMgr.isCurrentAnimation(PlaneNodeAnimationManager.AnimationType.TURN_ON_CARRIER))
            {
                if (p.AnimationMgr.CurrentAnimation.Ended)
                {
                    if (p.Plane != null && !p.Plane.IsOnAircraftCarrier) p.AnimationMgr.switchToIdle(true);
                }
            }
        }

        public void NextLife()
        {
            if (playerPlaneView.AnimationMgr.PrepareToChangeDirection)
            {
                Console.WriteLine("BUG - view nie odes�a� komunikatu");
            }
            if(!GameConsts.UserPlane.GodMode) carrierView.RemoveNextStoragePlane();
            carrierView.CrewStatePlaneOnCarrier();
            playerPlaneView.Restore();

            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                OnRegisterBoundingQuadrangle(playerPlaneView.Plane);
            }
        }

        /// <summary>
        /// Rejestruje czworokat ktory rysowany jest w view (HELPER)
        /// </summary>
        /// <param name="q">obiekt implementuj�cy IRenderableQuadrangles</param>
        public static void OnRegisterBoundingQuadrangle(IRenderableQuadrangles q)
        {
            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                ViewHelper.AttachQuadrangles(FrameWork.SceneMgr, q);
            }
        }

        /// <summary>
        /// Odrejestruje czworokat ktory rysowany jest w view (HELPER)
        /// </summary>
        /// <param name="q">obiekt implementuj�cy IRenderableQuadrangles</param>
        public void OnUnregisterBoundingQuadrangle(IRenderableQuadrangles q)
        {
            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                ViewHelper.DetachQuadrangles(sceneMgr, q);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void OnFrameStarted(FrameEvent evt)
        {
            if (EngineConfig.DisplayBoundingQuadrangles)
            {
                ViewHelper.RefreshBoundingQuandrangles();
            }

            EffectsManager.Singleton.UpdateTimeAndAnimateAll(evt.timeSinceLastFrame);

            PlayerPlaneView p = playerPlaneView;
            int count = planeViews.Count;
            for (int i = 0; i < count; i++)
            {
                UpdatePlaneView(planeViews[i], evt.timeSinceLastFrame);
            }
            if (p != null) UpdatePlaneView(p, evt.timeSinceLastFrame);

            // tileviews
            VertexAnimable va;
            count = tileViews.Count;
            for (int i = 0; i < count; i++)
            {
                if (tileViews[i] is VertexAnimable)
                {
                    va = tileViews[i] as VertexAnimable;
                    va.updateTime(evt.timeSinceLastFrame);
                }
            }

            // soldiers
            SoldierView sv;
            count = soldierViews.Count;
            for (int i = 0; i < count; i++)
            {
                sv = soldierViews[i];
                sv.updateTime(evt.timeSinceLastFrame);
                sv.refreshPosition();
            }

            count = dyingSoldierViews.Count;
            for (int i = 0; i < count; i++)
            {
                sv = dyingSoldierViews[i];
                sv.updateTime(evt.timeSinceLastFrame);
                sv.refreshPosition();

                if (sv.IsAnimationFinished() && !EngineConfig.BodiesStay)
                {
                    dyingSoldierViews.Remove(sv);
                    i--;
                    count--;
                    SoldierView.FreeInstance(sv.Soldier, !EngineConfig.BodiesStay);
                }
            }

            // ammo
            AmmunitionView ammunitionView;
            count = ammunitionViews.Count;
            for (int i = 0; i < count; i++)
            {
                ammunitionView = ammunitionViews[i];
                ammunitionView.updateTime(evt.timeSinceLastFrame);
                ammunitionView.refreshPosition();
            }

            //Carrier
            if (carrierView != null)
            {
                carrierView.updateTime(evt.timeSinceLastFrame);
            }

            foreach(CompositeModelView cv in this.compositeModelViews)
            {
                if(cv is ShipView)
                {
                    (cv as ShipView).refreshPosition();
                }
            }

            count = backgroundViews.Count;
            for (int i = 0; i < count; i++)
            {
                CompositeModelView cmv = backgroundViews[i];
                if (cmv is CarrierView)
                {
                    CarrierView cv = (CarrierView) cmv;
                    cv.updateTime(evt.timeSinceLastFrame);
                }
            }
        }

        public void OnRegisterLevel(Level level)
        {
            this.level = level;

            InitSkies();
            InitOceanSurface();

            List<LevelTile> lvlTiles = level.LevelTiles;

            modelToViewAdjust = -(lvlTiles.Count/2)*TileWidth;
            currentTilePositionOnScene = modelToViewAdjust - 25.25f;

            int totalTilesNumber = lvlTiles.Count;

            for (int i = 0; i < totalTilesNumber; i++)
            {
                OnRegisterTile(lvlTiles[i]);
            }
            BombView.InitPool(100, framework);
            RocketView.InitPool(80, framework);
            TorpedoView.InitPool(10, framework);
            SoldierView.InitPool(70, framework);

            InitSplashPool(350);

            //Console.WriteLine("Level loaded");
        }

        public void OnEndLoadingLevel()
        {
        }


        private void InitOceanSurface()
        {
            // OCEAN 

            // nie dzia�a na GeForce2 :))))
            /*
            Mogre.Plane oceanPlane = new Mogre.Plane();
            oceanPlane.normal = Vector3.UNIT_Y;
            oceanPlane.d = 0;
            MeshManager.Singleton.CreatePlane("OceanPlane",
                                            ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, oceanPlane,
                                            oceanSize, oceanSize, 1, 1, true, 1, 10, 10, Vector3.UNIT_Z);

            Entity ocean = sceneMgr.CreateEntity("Ocean", "OceanPlane");
            ocean.SetMaterialName("Ocean2_HLSL_GLSL");
            ocean.CastShadows = false;
            sceneMgr.RootSceneNode.AttachObject(ocean);
           */

            Entity ocean2 = sceneMgr.CreateEntity("Ocean2", "OceanPlane.mesh");
            /* Entity xxx = sceneMgr.CreateEntity("OceanX", "OceanPlane.mesh");
            
           SceneNode xnode = sceneMgr.RootSceneNode.CreateChildSceneNode("aaa", new Vector3(0,0.1f,0));
           xnode.AttachObject(xxx);
           xnode.Scale(0.001f, 1.0f, 0.001f);*/

            ocean2.CastShadows = false;
            sceneMgr.RootSceneNode.AttachObject(ocean2);

            if (FrameWork.DisplayMinimap)
            {
                SceneNode mOceanNode = minimapMgr.RootSceneNode.CreateChildSceneNode("MinimapOceanNode");
                Entity mOcean = sceneMgr.CreateEntity("MinimapOcean", "TwoSidedPlane.mesh");
                mOcean.SetMaterialName("Minimap/Ocean");
                mOcean.CastShadows = false;
                mOcean.RenderQueueGroup = (byte) RenderQueueGroupID.RENDER_QUEUE_BACKGROUND;
                mOceanNode.AttachObject(mOcean);
                mOceanNode.Position = new Vector3(0, -10, 0);
                mOceanNode.SetScale(oceanSize, 0, 5);
                mOceanNode.Pitch(new Degree(90));
            }
            // OCEAN
        }

        private void InitSkies()
        {
            // Set the material
            ColourValue ambient = new ColourValue(0.5f, 0.5f, 0.5f);

            // zmienne odbicie w wodzie
            string texture = "morning.jpg";
            string material = "Skyplane/Morning";
            switch (level.DayTime)
            {
                case DayTime.Noon:
                    material = "Skyplane/Noon";
                    texture = "cloudy_noon.jpg";
                    InitLight();
                    break;

                case DayTime.Dawn:
                    material = "Skyplane/Morning";
                    texture = "morning.jpg";
                    InitDawnLight();
                    break;

                case DayTime.Night:
                    material = "Skyplane/Night";
                    texture = "night.jpg";
                    ambient = new ColourValue(0.27f, 0.27f, 0.32f);
                    InitNightLight();
                    isNightScene = true;
                    break;
            }
            MaterialPtr m = MaterialManager.Singleton.GetByName("Ocean2_HLSL_GLSL");
            m.Load(false);
            Pass p = m.GetBestTechnique().GetPass(0);
            TextureUnitState tu = p.GetTextureUnitState("Reflection");
            if (tu != null)
            {
                tu.SetCubicTextureName(texture, true);
            }
            if (p.HasFragmentProgram)
            {
                GpuProgramParametersSharedPtr param = p.GetVertexProgramParameters();
                param.SetNamedConstant("bumpSpeed", new Vector3(0.02f, -0.03f, 0));
                p.SetVertexProgramParameters(param);
            }
            m = null;

            Mogre.Plane skyPlane;
            skyPlane.normal = Vector3.UNIT_Z;
            skyPlane.d = oceanSize/2.0f;

            sceneMgr.SetSkyPlane(true, skyPlane, material, oceanSize/110.0f, 1, true, 0.5f, 10, 10);
            sceneMgr.AmbientLight = ambient;

            // mewy
            if (!EngineConfig.LowDetails)
            {
                EffectsManager.Singleton.AddSeagulls(sceneMgr, new Vector3(0, 150, -1500), new Vector2(20, 25),
                                                     new Degree(10), 20, 10);
            }

            // chmury
            int cloudsets = 10;
            int currentX = (int) (-oceanSize/2.0f);
            for (int i = -cloudsets; i < cloudsets; i += 2)
            {
                currentX += (oceanSize/cloudsets);

                if (!EngineConfig.LowDetails)
                    EffectsManager.Singleton.AddClouds(sceneMgr, new Vector3(currentX, 100, -500), new Vector2(150, 50),
                                                       new Degree(2), 10);
                EffectsManager.Singleton.AddClouds(sceneMgr, new Vector3(currentX, -100, -4200),
                                                   new Vector2(5000, 400) + ViewHelper.RandomVector2(1000, 100),
                                                   new Degree(1), 5);

                // nad samolotem (niebo)
                EffectsManager.Singleton.AddClouds(sceneMgr, new Vector3(currentX, 170, 0), new Vector2(500, 200), 0, 1);
            }

            /*if (Controller.FrameWork.DisplayMinimap)
            {

                Mogre.Plane skyPlane;// = new Mogre.Plane();
                skyPlane.normal = Vector3.UNIT_Z;// Vector3.UNIT_Z;
                skyPlane.d = oceanSize / 2.0f;
                minimapMgr.SetSkyPlane(true, skyPlane, "Skyplane/Morning", 200, 1);
                minimapMgr.AmbientLight = new ColourValue(0.5f, 0.5f, 0.5f);
                
            }
             */
        }

        private void InitDawnLight()
        {
            // create a default point light
            Light light = sceneMgr.CreateLight("MainLight");
            light.Type = Light.LightTypes.LT_DIRECTIONAL;
            light.Position = new Vector3(0, 1000, -500);
            light.Direction = new Vector3(10, -20, 10);
            light.DiffuseColour = new ColourValue(0.95f, 0.90f, 0.90f);
            light.SpecularColour = new ColourValue(0.05f, 0.05f, 0.05f);

            Camera texCamera = new Camera("TexCamera", sceneMgr);
            LiSPSMShadowCameraSetup c = new LiSPSMShadowCameraSetup();
            c.GetShadowCamera(sceneMgr, framework.Camera, framework.Viewport, light, texCamera);
            ShadowCameraSetupPtr p = new ShadowCameraSetupPtr(c);
            sceneMgr.SetShadowCameraSetup(p);

            sceneMgr.ShadowFarDistance = 200;
            sceneMgr.ShadowColour = new ColourValue(0.8f, 0.8f, 0.8f);
        }

        private void InitLight()
        {
            // create a default point light
            Light light = sceneMgr.CreateLight("MainLight");
            light.Type = Light.LightTypes.LT_DIRECTIONAL;
            light.Position = new Vector3(0, 2000, 500);
            light.Direction = new Vector3(2, -18, -20);
            light.DiffuseColour = new ColourValue(0.50f, 0.50f, 0.52f);
            light.SpecularColour = new ColourValue(0.02f, 0.02f, 0.03f);

            Camera texCamera = new Camera("TexCamera", sceneMgr);
            LiSPSMShadowCameraSetup c = new LiSPSMShadowCameraSetup();
            c.GetShadowCamera(sceneMgr, framework.Camera, framework.Viewport, light, texCamera);
            ShadowCameraSetupPtr p = new ShadowCameraSetupPtr(c);
            sceneMgr.SetShadowCameraSetup(p);

            sceneMgr.ShadowFarDistance = 200;
            sceneMgr.ShadowColour = new ColourValue(0.8f, 0.8f, 0.8f);
        }

        private void InitNightLight()
        {
            // create a default point light
            Light light = sceneMgr.CreateLight("MainLight");
            light.Type = Light.LightTypes.LT_DIRECTIONAL;
            light.Position = new Vector3(-300, 1000, 200);
            light.Direction = new Vector3(1, -5, 2);
            light.DiffuseColour = new ColourValue(0.7f, 0.7f, 0.80f);
            light.SpecularColour = new ColourValue(0.05f, 0.05f, 0.07f);

            sceneMgr.ShadowColour = new ColourValue(0.65f, 0.65f, 0.75f);
        }

        /// <summary>
        /// Zdarzenie jest wyzwalane przez kontroler w celu zmiany
        /// stanu podwozia samolotu przekazanego jako parametr metody.
        /// Zdarzenie moze oznaczac tak otwieranie, jak i skladanie podwozia.
        /// Rodzaj animacji jest wybierany na podstawie zmiennej opisujacej
        /// aktualny stan podwozia (mozliwe stany podwozia ToggleOut - nalezy
        /// otworzyc podwozie, ToggleIn - nalezy zwinac podwozie)
        /// 
        /// Po zakonczeniu animacji zmiany stanu podwozia, przekazywany jest
        /// odpowiedni event do kontrolera.
        /// </summary>
        /// <param name="plane">
        ///     Samolot, w ktorym nalezy zmienic stan podwozia.
        /// </param>
        /// <author>Jakub Tezycki</author>
        public void OnToggleGear(Plane plane)
        {
            PlaneView p = FindPlaneView(plane);
            p.AnimationMgr.switchToGearUpDown(false, null, controller.OnGearToggled);
            p.AnimationMgr.CurrentAnimation.onFinishInfo = plane;
        }

        /// <summary>
        /// Metody wywolywana gdy pocisk z dzialka samolotu trafia z ziemie / ocean
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public void OnGunHit(LevelTile tile, float posX, float posY)
        {
            SceneNode splashNode = getSplashNode();
            if (splashNode == null) return; // koniec poola
            Boolean ocean = false;
            NodeAnimation.NodeAnimation na;

            if (tile is OceanTile)
            {
                ocean = true;
            }
            EffectsManager.EffectType type;
            if (ocean)
            {
                type = EffectsManager.EffectType.WATERIMPACT2;
            }
            else
            {
                type = EffectsManager.EffectType.DIRTIMPACT1;
            }

            Vector3 position =
                new Vector3(posX + ModelToViewAdjust, ocean ? (float) posY : (float) (posY + 1.0f), 0);

            na =
                EffectsManager.Singleton.RectangularEffect(sceneMgr, splashNode, type.ToString(), type, position,
                                                           new Vector2(4, 4), Quaternion.IDENTITY, false);
            na.Node.Orientation = new Quaternion(Math.HALF_PI, Vector3.UNIT_X);
            na.Node.Orientation *= new Quaternion(Math.RangeRandom(-0.1f, 0.1f)*Math.HALF_PI, Vector3.UNIT_Y);
            na.onFinishInfo = na.Node;
            na.onFinish = onFreeSplashNode;
        }

        public void onFreeSplashNode(object o)
        {
            SceneNode animationNode = (SceneNode) o;
            animationNode.SetVisible(false);
            freeSplashNode();
        }


        public void OnCatchPlane(Plane plane, EndAircraftCarrierTile carrierTile)
        {
            carrierView.StartCatchingPlane(FindPlaneView(plane), carrierTile);
        }

        public void OnReleasePlane(Plane plane, EndAircraftCarrierTile carrierTile)
        {
            carrierView.StartReleasingPlane();
        }


        public void OnTakeOff()
        {
            playerPlaneView.AnimationMgr.switchToIdle(true);
            carrierView.CrewStatePlaneAirborne();
        }

        public void OnTouchDown()
        {
            playerPlaneView.AnimationMgr[PlaneNodeAnimationManager.AnimationType.IDLE].Enabled = false;
            carrierView.CrewStatePlaneOnCarrier();
        }


        public void BuildCameraHolders()
        {
            cameraHolders = playerPlaneView.GetCameraHolders();
        }

        public void OnChangeCamera()
        {
            // if (EngineConfig.ManualCamera)
            {
                //framework.CameraZoom = 0;
                framework.Camera.Position = Vector3.ZERO;
                framework.Camera.Orientation = Quaternion.IDENTITY;

                currentCameraHolderIndex = ((++currentCameraHolderIndex)%cameraHolders.Count);
                for (int i = 0; i < cameraHolders.Count; i++)
                {
                    CurrentCameraHolder.DetachObject(framework.Camera);
                }
                cameraHolders[currentCameraHolderIndex].AttachObject(framework.Camera);

                SoundManager3D.Instance.SetListener(framework.Camera);
                SoundManager3D.Instance.UpdateSoundObjects();
            }
        }

        public void OnResetCamera()
        {
            currentCameraHolderIndex = -1;
            OnChangeCamera();
        }


      
    }
}