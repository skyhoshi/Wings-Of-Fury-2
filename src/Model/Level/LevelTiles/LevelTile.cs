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
using System.Text;

using Wof.Model.Level.Common;
using Wof.Model.Level.LevelTiles.AircraftCarrierTiles;
using Wof.Model.Level.LevelTiles.IslandTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.EnemyInstallationTiles;
using Wof.Model.Level.LevelTiles.Watercraft;

namespace Wof.Model.Level.LevelTiles
{

    #region TileKind
    
    public enum CollisionType 
    {
    		CollisionRectagle,
    		Altitude,
    		Hitbound,
    		None
    }

    /// <summary>
    /// Rodzaj czesci planszy.
    /// </summary>
    public enum TileKind
    {
        /// <summary>
        /// Ocean.
        /// </summary>
        Ocean,

        /// <summary>
        /// Lotniskowiec.
        /// </summary>
        AircraftCarrier,

        /// <summary>
        /// Wyspa.
        /// </summary>
        Island,
        /// <summary>
        /// statek.
        /// </summary>
        Ship
    }

    #endregion

    /// <summary>
    /// Klasa bazowa dla wszystkich obiektow wczytywanych z 
    /// pliku xml.
    /// </summary>
    public abstract class LevelTile : IRenderableQuadrangles, IAttractorSource
    {
        #region Const

        
        /// <summary>
        /// Szerokosc jednego elementu.
        /// </summary>
        public const int TileWidth = 10;

        #endregion

        #region Fields

        
        
         protected PointD attractorForce = new PointD(0,0);


   


    


        /// <summary>
        /// Wysokosc poczatku elementu.
        /// </summary>
        protected float yBegin;

        /// <summary>
        /// Przesuni�cie w widoku.
        /// </summary>
        protected float viewXShift;

        

        /// <summary>
        /// Wysokosc konca elementu.
        /// </summary>
        protected float yEnd;

        /// <summary>
        /// Prostokat opisujacy obiekt.
        /// </summary>
        protected Quadrangle hitBound;

        /// <summary>
        /// Dodatkowa lista obiektow z ktorymi moga
        /// wystapic kolizje.
        /// </summary>
        private List<Quadrangle> collisionRectangles;

        /// <summary>
        /// Indeks tile na liscie obiektow.
        /// </summary>
        protected int tilesIndex;

        /// <summary>
        /// Typ pola.
        /// </summary>
        protected int type;
        
        /// <summary>
        /// Maksymalne Y jakie wchodzi w sklad obiektu
        /// </summary>
        protected float maxY;

        #endregion

        #region Properties

        public abstract string GetXMLName
        {
            get;
        }

        /// <summary>
        /// Zwraca wysokosc poczatku elementu.
        /// </summary>
        public float YBegin
        {
            get { return yBegin; }
            set { yBegin = value; }
        }

        /// <summary>
        /// Zwraca przesuni�cie o jakie ma by� przesuni�ty model w widoku (w stosunku do modelu)
        /// </summary>
        public float ViewXShift
        {
            get { return viewXShift; }
        }
        
        
        
        public float MaxY
        {
        	get { return maxY; }
        }

        

        /// <summary>
        /// Zwraca wysokosc konca elementu.
        /// </summary>
        public float YEnd
        {
            get { return yEnd; }
            set { yEnd = value; }
        }

        /// <summary>
        /// Zwraca czworokat opisuj�cy tile'a (teren wyspu, ocean, lotniskowiec).
        /// </summary>
        public Quadrangle HitBound
        {
            get { return hitBound; }
        }

        /// <summary>
        /// Zwraca liste prostokatow, z ktorymi moze wystapic 
        /// kolizja podczas gry.
        /// </summary>
        public List<Quadrangle> ColisionRectangles
        {
            get { return collisionRectangles; }
        }

   

        /// <summary>
        /// Pobiera lub ustawia index obiektu na plaszy.
        /// </summary>
        public virtual int TileIndex
        {
            set
            {
                tilesIndex = value;
                int positionX = value * TileWidth;
                if(hitBound == null)
                {
                	// domyslny hitbound zbudowany w oparciu o YBegin i YEnd
	                hitBound = new Quadrangle(new PointD(positionX, 0), new PointD(positionX, YBegin),
	                                          new PointD(positionX + TileWidth, yEnd), new PointD(positionX + TileWidth, 0));
                } else
                {
                	// hitbound podany 'explicite'. Do tej pory w zmiennej hitBound jest prostokat o wierzcholkach ulozonych relatywnie (tak jak collision-rect) -> zamieniamy na wartosci bezwzgledne
                	hitBound = new Quadrangle(new PointD(positionX + hitBound.LeftMostX, hitBound.LowestY), new PointD(positionX + hitBound.LeftMostX, hitBound.HighestY ),
	                                          new PointD(positionX + hitBound.RightMostX, hitBound.HighestY), new PointD(positionX + hitBound.RightMostX, hitBound.LowestY));
                }

                if (collisionRectangles != null && collisionRectangles.Count > 0)
                {
                    List<Quadrangle> tmpList = collisionRectangles;
                    collisionRectangles = new List<Quadrangle>();
                    for (int i = 0; i < tmpList.Count; i++)
                    {
                        collisionRectangles.Add(new Quadrangle(tmpList[i].Peaks));
                    }
                    for (int i = 0; i < collisionRectangles.Count; i++)
                    {
                        collisionRectangles[i].Move(positionX, 0);
                    }
                }
            }
            get { return tilesIndex; }
        }

        /// <summary>
        /// Zwraca rodzaj danej czesci planszy.
        /// </summary>
        public TileKind TileKind
        {
            get
            {
                if (this is IslandTile)
                    return TileKind.Island;
                if (this is ShipTile)
                    return TileKind.Ship;
                if (this is OceanTile)
                    return TileKind.Ocean;
                if (this is AircraftCarrierTile)
                    return TileKind.AircraftCarrier;
                throw new Exception("Nieznany typ Tile'a");
            }
        }

        /// <summary>
        /// Zwraca czy dany tile jest ko�cem lub pocz�tkiem lotniskowca.
        /// </summary>
        public bool IsAircraftCarrier
        {
            get { return (this is BeginAircraftCarrierTile || this is EndAircraftCarrierTile || this is AircraftCarrierTile); }
        }

        public bool isShipBunker
        {
            get { return (this is ShipBunkerTile); }
        }

        /// <summary>
        /// Pobiera typ(wariant) pola.
        /// </summary>
        public int Variant
        {
            get { return type; }
        }

        #endregion

        #region Public Constructor

        /// <summary>
        /// Konstruktor czteroparametrowy.
        /// </summary>
        /// <param name="yBegin">Wysokosc poczatku elementu.</param>
        /// <param name="yEnd">Wysokosc konca elementu.</param>
        /// <param name="hitBound">Czworokat opisujacy.</param>
        /// <param name="colisionRectanglesList">Lista prostokatow z ktorymi moga wystapic zderzenia.</param>    
        public LevelTile(float yBegin, float yEnd, float viewXShift, Quadrangle hitBound, List<Quadrangle> colisionRectanglesList)
        {
            this.viewXShift = viewXShift; 
            this.yBegin = yBegin;
            this.yEnd = yEnd;
            if (hitBound != null)
                this.hitBound = hitBound;

            collisionRectangles = colisionRectanglesList;
            
            float maxY = float.MinValue;
            maxY = yBegin;
            if(yEnd > maxY)
            {
            	maxY = yEnd;
            }

            float temp;
            if (hitBound != null)
            {
                temp = hitBound.HighestY;
                if(temp > maxY)
                {
            	    maxY = temp;
                }
            }

            if (collisionRectangles != null)
            {
                foreach (Quadrangle q in collisionRectangles)
                {
                    temp = q.HighestY;
                    if (temp > maxY)
                    {
                        maxY = temp;
                    }
                }
            }
            
        }

        #endregion

        #region Public Method


        public virtual void Update(int time, float timeUnit)
        {
            
        } 

        /// <summary>
        /// Zwraca opis danego elementu.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("yBegin: " + yBegin);
            builder.AppendLine("yEnd: " + yEnd);
            if (hitBound != null)
                builder.AppendLine("Hit bound: " + hitBound.ToString());

            return builder.ToString();
        }


     
        

         /// <summary>
        /// Funkcja sprawdza czy dany obiekt jest w kolizji
        /// z innym prostokatem:bomba, rakieta, etc
        /// </summary>
        /// <param name="quad">Prostokat z ktorym sprawdzamy kolizje.</param>
        /// <returns>Jesli kolizja wystapila choc z jednym elementem zwraca true;
        /// w przeciwnym przypadku zwraca false.</returns>
        public virtual CollisionType InCollision(Quadrangle quad)
        {
        
        	if(this is FortressBunkerTile)
        	{
        		
        	}
        	
        	if (quad == null) return CollisionType.None;
        	if(this.InSimpleCollision(quad.Center)) {
        		return CollisionType.Altitude;
        	}
        	
        	if(hitBound != null && hitBound.Intersects(quad)) {
        		return CollisionType.Hitbound;
        	}
           
            List<Quadrangle> list = ColisionRectangles;
            if(list != null) {
            	for (int i = 0; i < list.Count; i++)
	                if (list[i].Intersects(quad))
	                    return CollisionType.CollisionRectagle;	
            }
           
            return CollisionType.None;
        }

        /// <summary>
        /// Funkcja sprawdza czy dany obiekt jest w kolizji z innym obiektem. Por�wnywana jest wy��cznie wysoko�� (YBegin / hitBound.LowestY)
        /// </summary>
        /// <param name="center">Punkt srodkowy obiektu z ktorym sprawdzamy kolizje.</param>
        /// <returns>Jesli punkt srodkowy obiektu jest ponizej linni powierzchni - kolizja nastapila.</returns>
        public virtual bool InSimpleCollision(PointD center)
        {
        	if(hitBound != null)
        	{
            	return this.hitBound.LowestY > center.Y;
        	} else
        	{
        		return false;
        	}
        }
        
        
        public PointD GetAttractorForce()
		{
			return attractorForce;
		}

        #endregion

        #region IBoundingBoxes Members

        /// <summary>
        /// Lista collisionRectangles + hitBound
        /// </summary>
        public List<Quadrangle> BoundingQuadrangles
        {
            get
            {
                List<Quadrangle> result = new List<Quadrangle>();
                if (collisionRectangles != null && collisionRectangles.Count > 0)
                {
                    result.AddRange(collisionRectangles);
                }
                result.Add(hitBound);
                return result;
            }
        }

        public string Name
        {
            get { return "Tile" + GetHashCode(); }
        }

        #endregion
    	
        
       
		
    }
}