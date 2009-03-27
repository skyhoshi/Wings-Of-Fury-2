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

using System.Text;
using Wof.Model.Configuration;
using Wof.Model.Level.Common;
using Wof.Model.Level.LevelTiles;
using Wof.Model.Level.LevelTiles.IslandTiles;
using Wof.Model.Level.LevelTiles.IslandTiles.EnemyInstallationTiles;
using Wof.Model.Level.LevelTiles.Watercraft;

namespace Wof.Model.Level.Infantry
{
    /// <summary>
    /// Klasa implementuje logike zachowania
    /// zolnierza na planszy.
    /// </summary>
    /// <author>Michal Ziober</author>
    public class Soldier : IMove
    {
        #region Const

        /// <summary>
        /// Przedzial czasu od ostatniego przesuniecia.
        /// </summary>
        private const int TimeUnit = 1000;

        /// <summary>
        /// Wspolczynnik prawdopodobienstwa tego, ze zolnierz 
        /// wejdzie do bunkra.
        /// </summary>
        private const int ProbabilityCoefficient = 5;

        #endregion

        #region Enums

        /// <summary>
        /// Stan zolnierza.
        /// </summary>
        public enum SoldierStatus : byte
        {
            /// <summary>
            /// Zolnierz jest zywy
            /// </summary>
            IsAlive = 0,

            /// <summary>
            /// Zolnierz jest martwy
            /// </summary>
            IsDead = 1,

            /// <summary>
            /// Schowal sie do bunkra
            /// </summary>
            InBunker = 2
        }

        #endregion

        #region Fields

        /// <summary>
        /// Pozycja X na planszy.
        /// </summary>
        private float xPos;

        /// <summary>
        /// Pozycja Y na planszy.
        /// </summary>
        private float yPos;

        /// <summary>
        /// Pozycja startowa zolnierza.
        /// </summary>
        private readonly float startPosition;

        /// <summary>
        /// Pocz�tkowy indeks z leveltiles
        /// </summary>
        private int startLevelIndex;

        /// <summary>
        /// Kierunek poruszania.
        /// </summary>
        private Direction direction;

        /// <summary>
        /// Czy zolnierz jest zywy.
        /// </summary>
        private SoldierStatus _soldierStatus;

        /// <summary>
        /// Szybkosc z jaka porusza sie zolnierz.
        /// </summary>
        private int speed;

        /// <summary>
        /// Referencja do planszy.
        /// </summary>
        private readonly Level refToLevel;

        /// <summary>
        /// Licznik czasu, pod czas ktorego zolnierz nie moze
        /// zostac zabity.
        /// </summary>
        private int protectedTime;

        /// <summary>
        /// Licznik czasu, pod czas ktorego zolnierz nie 
        /// moze wejsc do bunkra.
        /// </summary>
        private int homelessCounterTime = 0;

        /// <summary>
        /// Czy zolnierz moze wejsc znowu do bunkru.
        /// </summary>
        private bool canReEnter;

        /// <summary>
        /// Czy zolnierz moze zostac zabity.
        /// </summary>
        private bool canDie;

        private bool leftBornTile = false;

        protected SoldierType type;

        public enum SoldierType
        {
            SOLDIER,
            GENERAL,
            SEAMAN
        }

        #endregion

        #region Public Constructor

        /// <summary>
        /// Publiczny konstruktor jednoparametrowy.
        /// </summary>
        /// <param name="posX">Pozycja startowa zolnierza (mierzona w tilesIndex.</param>
        /// <param name="direct">Kierunek w ktorym sie porusza.(Prawo,Lewo)</param>
        /// <param name="level">Referencja do obiektu planszy.</param>
        /// <author>Michal Ziober</author>
        /// <param name="offset"></param>
        internal Soldier(float posX, Direction direct, Level level, float offset)//, SoldierType type)
        {
            //przy starcie jest zywy.
            _soldierStatus = SoldierStatus.IsAlive;
            //pozycja startowa - pozycja zniszczonej instalacji
            xPos = posX * LevelTile.Width + offset;
            startPosition = posX;
            startLevelIndex = (int)posX;
            direction = direct;
            refToLevel = level;
            canDie = false;
            canReEnter = false;
            protectedTime = 0;
            //this.type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Ustawia lub pobiera predkosc zolnierza.
        /// </summary>
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        /// <summary>
        /// Jesli zwroci true - zolnierz zyje,
        /// w przeciwnym przypadku - zolnierz nie zyje.
        /// </summary>
        public bool IsAlive
        {
            get { return _soldierStatus == SoldierStatus.IsAlive; }
        }

        /// <summary>
        /// Zwraca stan w ktorym obecnie znajduje sie zolnierz.
        /// <see cref="SoldierStatus"/>
        /// </summary>
        public SoldierStatus Status
        {
            get { return _soldierStatus; }
        }

        public int StartLevelIndex
        {
            get { return startLevelIndex; }
        }

        /// <summary>
        /// Rodzaj �o�nierza
        /// </summary>
        public SoldierType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Zwraca pozycje X zolnierza na planszy.
        /// </summary>
        public float XPosition
        {
            get { return xPos; }
        }

        /// <summary>
        /// Zwraca pozycje Y zolnierza na planszy.
        /// </summary>
        public float YPosition
        {
            set { yPos = value; }
            get { return yPos; }
        }


        /// <summary>
        /// Zwraca pozycje zolnierza na planszy.
        /// </summary>
        public PointD Position
        {
            get { return new PointD(xPos, yPos); }
        }

        /// <summary>
        /// Zwraca kierunek w ktorym porusza sie zolnierz.
        /// </summary>
        public Direction Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Zwraca wartosc czy zolnierz moze zaostac zabity.
        /// </summary>
        public bool CanDie
        {
            get { return canDie; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float StartPosition
        {
            get { return startPosition; }
        }


        #endregion

        #region PrivateMethod

        /// <summary>
        /// Sprawdza sasiednie pole.
        /// </summary>
        /// <param name="index">Indeks pola na liscie.</param>
        /// <returns>Jesli zolnierz moze wejsc na pole zwraca true,
        /// false w przeciwnym przypadku.</returns>
        private bool Check(int index)
        {
            if (!leftBornTile) return true;
            IslandTile tiles = refToLevel.LevelTiles[index] as IslandTile;
            if (tiles == null)
            {
                ShipTile tiles2 = refToLevel.LevelTiles[index] as ShipTile;
                if (tiles2 == null) return false;
                else
                {
                    return tiles2.Traversable;
                }
            }
            else
            {
                return tiles.Traversable;
            }


        }

        /// <summary>
        /// Sprawdza czy sasiednie pole jest bunkrem.
        /// </summary>
        /// <param name="index">Indeks pola na liscie.</param>
        /// <returns>Jesli dane pole jest bunkrem - zwraca true,
        /// w przeciwnym przypadku false.</returns>
        private bool IsBunker(int index)
        {
            if (index >= 0 && index < refToLevel.LevelTiles.Count)
                return (refToLevel.LevelTiles[index] is BunkerTile);
            else
                return false;
        }

        /// <summary>
        /// Zmienia pozycje zolnierza.
        /// </summary>
        private void ChangeLocation(int time)
        {

            //jesli idzie w prawo
            if (direction == Direction.Right)
            {
                float tmpPosition = xPos + speed * Mathematics.GetMoveFactor(time, TimeUnit);
                //sprawdza czy moze wejsc na to pole.
                if (Check(Mathematics.PositionToIndex(tmpPosition)))
                    xPos = tmpPosition;
                else //zmienia kierunek
                    direction = Direction.Left;
                // Check(Mathematics.PositionToIndex(tmpPosition));

            } //jesli idzie w lewo
            else
            {
                float tmpPosition = xPos - speed * Mathematics.GetMoveFactor(time, TimeUnit);
                //sprawdza czy moze wejsc na to pole.
                if (Check(Mathematics.PositionToIndex(tmpPosition)))
                    xPos = tmpPosition; //wchodzi na sasiednie pole.
                else //zmienia kierunek.
                    direction = Direction.Right;
                //  Check(Mathematics.PositionToIndex(tmpPosition));
            }
            LevelTile tile = refToLevel.LevelTiles[Mathematics.PositionToIndex(xPos)];
            yPos = (tile.YBegin + tile.YEnd) / 2.0f;

        }

        #endregion

        #region Public Method

        /// <summary>
        /// Zwraca opis zolnierza.
        /// </summary>
        /// <returns>String z opisem zolnierza.</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Xpos: " + xPos);
            builder.AppendLine("Direction: " + direction.ToString());

            return builder.ToString();
        }

        /// <summary>
        /// Zabija zolnierza.
        /// </summary>
        public void Kill()
        {
            //zabijam zolnierza.
            _soldierStatus = SoldierStatus.IsDead;
        }

        #endregion

        #region IMove Members

        /// <summary>
        /// Zmienia pozycje zolnierza.
        /// </summary>
        /// <param name="time">Czas od ostatniego ruchu w milisekundach.</param>
        public void Move(int time)
        {
            //Jesli zolnierz zyje.
            if (IsAlive)
            {

                int tileIndex = Mathematics.PositionToIndex(xPos);
                if (tileIndex != startPosition) leftBornTile = true;
                if (canReEnter //czy moze wejsc ponownie do bunkra.
                    && (tileIndex != startPosition) //jesli bunkier nie jest rodzicem.
                    && IsBunker(tileIndex) //jesli to jest bunkier.
                    && (time % ProbabilityCoefficient == 0)) //losowosc.
                {
                    BunkerTile bunker = refToLevel.LevelTiles[tileIndex] as BunkerTile;
                    if (bunker.IsDestroyed && bunker.CanReconstruct)
                    {
                        bunker.Reconstruct();
                        refToLevel.Controller.OnTileRestored(bunker);
                    }
                    if (!bunker.IsDestroyed)
                    {
                        //dodaje zolnierza do bunkra.
                        bunker.AddSoldier();
                        //wyslam sygnal do controllera aby usunal zolnierza z widoku.
                        refToLevel.Controller.UnregisterSoldier(this);
                        //usuwam zolnierza z planszy.
                        _soldierStatus = SoldierStatus.InBunker;
                    }
                    else ChangeLocation(time);
                }
                else ChangeLocation(time);


                //sprawdza czy ulynal czas bezsmiertelnosci
                if (!canDie)
                {
                    protectedTime += time;
                    if (protectedTime >= TimeUnit)
                        canDie = true;
                }

                //sprawdza czy ponownie moze wejsc do bunkra.
                if (!canReEnter)
                {
                    homelessCounterTime += time;
                    if (homelessCounterTime >= GameConsts.Soldier.HomelessTime)
                        canReEnter = true;
                }
            }
        }



        #endregion
    }
}