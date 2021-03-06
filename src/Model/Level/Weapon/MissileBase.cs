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
using System.Collections.Generic;
using System.Diagnostics;

using Wof.Misc;
using Wof.Model.Configuration;
using Wof.Model.Level.Common;
using Wof.Model.Level.Infantry;
using Wof.Model.Level.LevelTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.EnemyInstallationTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.ExplosiveObjects;
using Wof.Model.Level.LevelTiles.Watercraft;
using Wof.Model.Level.Planes;
using Wof.View;
using Math = Mogre.Math;
using Plane = Wof.Model.Level.Planes.Plane;

namespace Wof.Model.Level.Weapon
{
	/// <summary>
	/// Description of MissileBase.
	/// </summary>
	public abstract class MissileBase : Ammunition
	{
		
		public enum CollisionDirectionLocation { FORWARD, BACKWARD, BOTH, NONE };
		
		#region Fields

        /// <summary>
        /// Pole widzenia.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const int ViewRange = 100;

        /// <summary>
        /// Szerokosc wrazliwego pola na trafienia.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float HitShift = 5.05f;

        /// <summary>
        /// Wspolczynnik zmiany pozycji.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public const int MoveInterval = 6000;

        /// <summary>
        /// Szerokosc prostokata opisujacego rakiete.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float RocketWidth = 0.2f;

        /// <summary>
        /// Wysokosc prostokata opisujacego rakiete.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float RocketHeight = 1.62f;

        /// <summary>
        /// Czas opadania w milisekundach.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const int dropTime = 200;

        /// <summary>
        /// Predkosc spradania.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const int DropSpeed = -70;

        /// <summary>
        /// Przesuniecie pionowe rakiety wzgledem samolotu
        /// przy starcie.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float HeightShift = 0.9f;

        /// <summary>
        /// Przesuniecie poziome rakiety wzgledem samolotu
        /// przy starcie.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float WidthShift = 0.9f;

        /// <summary>
        /// Wspolrzedna y, po przekroczeniu ktorej sprawdzana jest kolizja z lotniskowcem. 
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected const float MinYPositionForAircraft = 9;

        /// <summary>
        /// Licznik czasu.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected int timeCounter;
        
		public int TimeCounter {
			get { return timeCounter; }
		}

        /// <summary>
        /// Wektor ruchu z napedem silnika.
        /// </summary>
        /// <author>Michal Ziober</author>
        protected PointD flyVector;

      


        /// <summary>
        /// Okresla kat (w radianach) o jaki pocisk obroci sie w osi Z (czyli "podkreci" sie w gore lub dol ekranu ) w czasie sekundy lotu
        /// </summary>
        protected float zRotationPerSecond = 0;


		protected float travelledDistance = 0;
		
		public float TravelledDistance {
			get { return travelledDistance; }
		}
		

        /// <summary>
        /// Maksymalny dopuszczalny dystans pomiedzy samolotem rodzicem a rakieta.
        /// Jesli dystans bedzie wiekszy rakieta bedzie odrejestrowana.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float maxDistanceToOwner = 600;

        /// <summary>
        /// Maksymalny dopuszczalny pionowy dystans 
        /// pomiedzy rakieta a samolotem rodzicem.
        /// </summary>
        /// <author>Michal Ziober</author>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected float maxHeightDistanceToOwner = 200;

        protected bool canDestroyStoragePlanes = false;        
        
		public bool CanDestroyStoragePlanes {
			get { return canDestroyStoragePlanes; }
		}
        
        protected PointD initialVelocity;
        
		public PointD InitialVelocity {
			get { return initialVelocity; }
		}

        #endregion

        #region Public Constructor

        /// <summary>
        /// Konstruktor szescioparametrowy. Tworzy 
        /// nowa rakiete na planszy.
        /// </summary>
        /// <param name="x">Wspolrzedna x.</param>
        /// <param name="y">Wspolrzedna y.</param>
        /// <param name="initialVelocity">Wektor ruchu.</param>
        /// <param name="level">Referencja do obiektu planszy.</param>
        /// <param name="angle">Kat nachylenia.</param>
        /// <param name="owner">Wlasciciel amunicji.</param>
        /// <author>Michal Ziober</author>
        public MissileBase(float x, float y, PointD initialVelocity, Level level, float angle, IObject2D owner)
            : base(level, angle, owner)
        {
            timeCounter = 0;
            this.initialVelocity = initialVelocity;

            //prostokat opisujacy obiekt.
            if (initialVelocity.X >= 0)
                boundRectangle = new Quadrangle(new PointD(x - WidthShift, y - HeightShift), RocketWidth, RocketHeight);
            else
                boundRectangle = new Quadrangle(new PointD(x + WidthShift, y - HeightShift), RocketWidth, RocketHeight);

            float yDropSpeed;

            yDropSpeed = owner.Bounds.IsObverse ? -DropSpeed : DropSpeed;


            //kierunek ruchu podczas lotu z silnikiem.
           // float speedX = initialVelocity.X >= 0 ? GameConsts.Rocket.BaseSpeed : -GameConsts.Rocket.BaseSpeed;

            //wektor ruchu podczas spadania.
            moveVector = new PointD(initialVelocity.X, yDropSpeed);
            
            if(initialVelocity.EuclidesLength < GameConsts.Rocket.BaseSpeed)
            {
            	initialVelocity.EuclidesLength =  GameConsts.Rocket.BaseSpeed;
            }
            //weektor ruchu podczas pracy silnika.
            if (initialVelocity.X >= 0)
            {
                flyVector = new PointD(initialVelocity.X * 0.7f * GameConsts.Rocket.BaseSpeed, initialVelocity.Y * 0.7f * GameConsts.Rocket.BaseSpeed);
            } else
            {
                flyVector = new PointD(initialVelocity.X * 0.7f * GameConsts.Rocket.BaseSpeed, initialVelocity.Y * 0.7f * GameConsts.Rocket.BaseSpeed);
            }

         
           
        }
        
       // protected abstract PointD GetInitialFlyVector() {
       // 	
       // }

        /// <summary>
        /// Konstruktor piecioparametrowy.Tworzy
        /// nowa rakiete na planszy.
        /// </summary>
        /// <param name="position">Pozycja rakiety.</param>
        /// <param name="initialVelocity">Wektor ruchu.</param>
        /// <param name="level">Referencja do obiektu planszy.</param>
        /// <param name="angle">Kat nachylenia.</param>
        /// <param name="owner">Wlasciciel rakiety.</param>
        /// <author>Michal Ziober</author>
        public MissileBase(PointD position, PointD initialVelocity, Level level, float angle, IObject2D owner)
            : this(position.X, position.Y, initialVelocity, level, angle, owner)
        {
        }

        /// <summary>
        /// Maksymalny dopuszczalny dystans pomiedzy samolotem rodzicem a rakieta.
        /// Jesli dystans bedzie wiekszy rakieta bedzie odrejestrowana.
        /// </summary>
        /// <author>Michal Ziober</author>
        public float MaxDistanceToOwner
        {
            get { return maxDistanceToOwner; }
            set { maxDistanceToOwner = value; }
        }

        /// <summary>
        /// Maksymalny dopuszczalny pionowy dystans 
        /// pomiedzy rakieta a samolotem rodzicem.
        /// </summary>
        /// <author>Michal Ziober</author>
        public float MaxHeightDistanceToOwner
        {
            get { return maxHeightDistanceToOwner; }
            set { maxHeightDistanceToOwner = value; }
        }

        #endregion

        #region IMove Members

        /// <summary>
        /// Zmienia pozycje rakiety w zaleznosci od czasu oraz
        /// sprawdza kolizje z obiektami na planszy.
        /// </summary>
        /// <param name="time">Czas od ostatniej zmieny</param>
        /// <author>Michal Ziober</author>
        public override void Move(int time)
        {
            base.Move(time);
            //zmienia pozycje.
            ChangePosition(time);

            //jesli nie zostala odrejstrowana.
            if (!OutOfFuel())
            {
                //jesli jest to rakieta samolotu gracza.
              /*  if (!ammunitionOwner.IsEnemy)
                {
                   
                }
                else*/
                {
                    CheckCollisionWithEnemyPlanes(); //sprawdzam zderzenie z wrogim samolotem.

                    //kolizje z samolotami na lotniskowcu
                    if (canDestroyStoragePlanes && Position.Y < MinYPositionForAircraft)
                        CheckCollisionWithStoragePlane();

                    //sprawdzam kolizje z samolotem gracza.
                    if (ammunitionOwner.IsEnemy) CheckCollisionWithUserPlane();
                }

                //obsluga zderzenia z ziemia.
                if (!(ammunitionOwner is Soldier) && !(ammunitionOwner is BunkerTile)) 
                {
                	CheckCollisionWithGround();
                }

                //sprawdzam kolizje z lotniskowce.
                CheckCollisionWithCarrier(this);
            }
        }

        /// <summary>
        /// Sprawdzam kolizje z samolotami na lotniskowcu.
        /// </summary>
        /// <author>Michal Ziober</author>
        protected virtual void CheckCollisionWithStoragePlane()
        {
            List<StoragePlane> storageToRemove = new List<StoragePlane>();

            if (refToLevel.StoragePlanes != null && refToLevel.StoragePlanes.Count > 0)
            {
                foreach (StoragePlane storagePlane in refToLevel.StoragePlanes)
                    if (boundRectangle.Intersects(storagePlane.Bounds))
                    {
                        //niszczy samolot na lotniskowcu.
                        storagePlane.Destroy();

                        //zmniejsza liczbe zyc.
                        refToLevel.SubtractionLive();

                        //odrejestruje samolot na lotniskowcu.
                        refToLevel.Controller.OnUnregisterPlane(storagePlane);

                        //niszcze rakiete.
                        Destroy();

                        storageToRemove.Add(storagePlane);
                        break;
                    }

                if (storageToRemove.Count > 0)
                {
                    foreach (StoragePlane sp in storageToRemove)
                    {
                        refToLevel.StoragePlanes.Remove(sp);
                    }
                    storageToRemove.Clear();
                }
            }
        }

        

        public void SetZRotationPerSecond(float f)
        {
            zRotationPerSecond = f;
        }
  
        /// <summary> 
        /// Zmienia pozycje rakiety.
        /// </summary>
        /// <param name="time">Czas od ostatniego przesuniecia.</param>
        /// <author>Michal Ziober</author>
        protected virtual  void ChangePosition(int time)
        {
            float coefficient = Mathematics.GetMoveFactor(time, MoveInterval);

            timeCounter += time;
            if (timeCounter <= dropTime) //swobodne spadanie
            {
                PointD vector = new PointD(moveVector.X * coefficient * 6, moveVector.Y * coefficient);
                boundRectangle.Move(vector);
            }
            else //naped silnikowy
            {
               // Console.WriteLine(flyVector.X);

                float minFlyingSpeed = Owner.IsEnemy ? GameConsts.EnemyFighter.Singleton.RangeFastWheelingMaxSpeed * GameConsts.EnemyFighter.Singleton.MaxSpeed : GameConsts.GenericPlane.CurrentUserPlane.RangeFastWheelingMaxSpeed * GameConsts.GenericPlane.CurrentUserPlane.MaxSpeed;


                // rakieta wytraca prędkość uzyskaną od samolotu
                if (Math.Abs(flyVector.X) > Math.Abs(minFlyingSpeed * GameConsts.Rocket.BaseSpeed))
                {
                    flyVector.X *= 0.995f;
                }

                if (Math.Abs(flyVector.Y) > Math.Abs(minFlyingSpeed * GameConsts.Rocket.BaseSpeed))
                {
                    flyVector.Y *= 0.995f;
                }

                float angle = zRotationPerSecond * coefficient;
                //  boundRectangle.Rotate(angle);
                //  moveVector.Rotate(PointD.ZERO, angle);
                relativeAngle += angle * (int)Direction;
                flyVector.Rotate(PointD.ZERO, angle);

                PointD vector = new PointD(flyVector.X * coefficient, flyVector.Y * coefficient);
                boundRectangle.Move(vector);
                travelledDistance += vector.EuclidesLength;
               
            }
        }
        
      
	    /// <summary> 
	    /// Sprawdza kolizje z wrogimi samolotami 
	    /// oraz obsluguje zderzania z nimi.
	    /// </summary>
	    /// <author>Michal Ziober</author>
	    protected abstract void CheckCollisionWithEnemyPlanes();
      

        /// <summary>
        /// Sprawdza kolizje z samolotem gracza.
        /// </summary>
        /// <remarks>Tylko dla rakiety wroga.</remarks>
        /// <author>Michal Ziober</author>
        protected abstract void CheckCollisionWithUserPlane();
        
        /// <summary>
        /// Sprawdza kolizje z podlozem.
        /// </summary>
        /// <author>Michal Ziober</author>
        protected abstract  void CheckCollisionWithGround();
       

     
        

        /// <summary>
        /// Funkcja sprawdza czy mozna odrejestrowac rakiete. Jesli mozna
        /// odrejetrowuje ja.
        /// </summary>
        /// <returns>Jesli rakieta zostanie odrejestrowana, zwroci true,
        /// false w przeciwnym przupadku.</returns>
        /// <author>Michal Ziober</author>
        protected virtual bool OutOfFuel()
        {
        	//&&
           //      flyVector.Y > 0
           
            if ((System.Math.Abs(Center.X - ammunitionOwner.Center.X) > MaxDistanceToOwner) ||
                ((System.Math.Abs(Center.Y - ammunitionOwner.Center.Y) > MaxHeightDistanceToOwner)))
            {
                Destroy();
                return true;
            }
            else
                return false;
           
           
        }

        #endregion

        #region Static Method

        public static CollisionDirectionLocation CanHitEnemyPlane(Plane plane, Plane enemyPlane,  bool biDirectional)
        {
            return CanHitEnemyPlane(plane, enemyPlane, 0, biDirectional);
        }

        /// <summary>
        /// Funkcja sprawdza czy samolot bedzie mogl trafic rakieta w inny obiekt.
        /// </summary>
        /// <param name="plane">Samolot strzelajacy.</param>
        /// <param name="enemyPlane">Samolot, ktory chemy trafic.</param>
        /// <returns>Zwraca true jesli moze trafic wrogi samolot; false - w przeciwnym
        /// przypadku.</returns> 
        /// <author>Michal Ziober</author>
        public static CollisionDirectionLocation CanHitEnemyPlane(Plane plane, Plane enemyPlane, float tolerance, bool biDirectional)
        {
        	
        	if(!biDirectional) {
	            if (plane.Direction == Direction.Right && plane.Center.X > enemyPlane.Center.X)
	                return CollisionDirectionLocation.NONE;
	
	            if (plane.Direction == Direction.Left && plane.Center.X < enemyPlane.Center.X)
	                return CollisionDirectionLocation.NONE;
        	}

            if (System.Math.Abs(plane.Center.X - enemyPlane.Center.X) < 10 &&
                System.Math.Abs(plane.Center.Y - enemyPlane.Center.Y) < 10)
                return CollisionDirectionLocation.NONE;
            
            if(System.Math.Abs((plane.Center - enemyPlane.Center).EuclidesLength) > ViewRange)
            {
            	return CollisionDirectionLocation.NONE;
            }

            Quadrangle planeQuad = new Quadrangle(plane.Bounds.Peaks);
            planeQuad.Move(0, -HeightShift);
            Line lineA = new Line(planeQuad.Peaks[1], planeQuad.Peaks[2]);
            for (int i = 0; i < enemyPlane.Bounds.Peaks.Count - 1; i++)
            {
                PointD start = enemyPlane.Bounds.Peaks[i];
                start += new PointD(Math.RangeRandom(-tolerance * 0.5f, -tolerance * 0.5f), Math.RangeRandom(-tolerance * 0.5f, -tolerance * 0.5f));

                PointD finish = enemyPlane.Bounds.Peaks[i + 1];
                finish += new PointD(Math.RangeRandom(-tolerance * 0.5f, -tolerance * 0.5f), Math.RangeRandom(-tolerance * 0.5f, -tolerance * 0.5f));

                Line lineB = new Line(start, finish);
                
                

                PointD cut = lineA.Intersect(lineB);
                if (cut == null)
                    continue;
                
                if((enemyPlane.Center - cut).EuclidesLength < HitShift)
                {
                	//ViewHelper.AttachCross(plane.Level.Controller.GetFramework().SceneMgr, cut, 10);
                	//return true;
                	
                	return (plane.Direction == Direction.Right && plane.Center.X > enemyPlane.Center.X || plane.Direction == Direction.Left && plane.Center.X < enemyPlane.Center.X) ? CollisionDirectionLocation.BACKWARD : CollisionDirectionLocation.FORWARD; 
                	
	                	
	             }
            }

            return CollisionDirectionLocation.NONE;
        }

        /// <summary>
        /// Sprawdza czy punkt przeciecia jest w zasiegu samolotu.
        /// </summary>
        /// <param name="cut">Punkt przeciecia dwoch prostych.</param>
        /// <param name="plane">Pozycja samolotu.</param>
        /// <returns>true jesli punkt przeciecia jest w zasiegu, false
        /// w przeciwnym przypadku.</returns>
        /// <author>Michal Ziober</author>
        private static bool InEnemyRange(PointD cut, PointD plane, PointD enemyPlane)
        {
            return
                System.Math.Abs(cut.X - plane.X) < ViewRange &&
                ((cut.Y > enemyPlane.Y - HitShift) && (cut.Y < enemyPlane.Y + HitShift));
        }

        #endregion

	}
}
