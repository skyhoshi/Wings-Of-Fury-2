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
using System.Drawing;
using System.Windows.Forms;
using Wof.Model.Level;
using Wof.Model.Level.Carriers;
using Wof.Model.Level.Common;
using Wof.Model.Level.LevelTiles.AircraftCarrierTiles;
using Wof.Model.Level.Planes;

namespace Tests.ModelTest.PlaneTest
{
    public partial class PlaneOnAircraftTestForm : Form
    {
        //Plane plane;
        private Carrier aircraft;
        private Level level;

        public PlaneOnAircraftTestForm()
        {
            InitializeComponent();
            //przerobione przez Emila:
            level = new Level("levels/level-1.dat", null, PlaneType.P47);
            //level.UserPlane.StartEngine();

          //  level.Controller

            List<AircraftCarrierTile> list = new List<AircraftCarrierTile>();
            BeginAircraftCarrierTile begin =
                new BeginAircraftCarrierTile(0.4f, 0.5f, 0,new Quadrangle(new PointD(10, 10), 100, 50));
            MiddleAircraftCarrierTile middle =
                new MiddleAircraftCarrierTile(0.4f, 0.5f, 0,new Quadrangle(new PointD(10, 10), 190, 50));
            EndAircraftCarrierTile end =
                new EndAircraftCarrierTile(0.4f, 0.5f, 0,new Quadrangle(new PointD(190, 10), 100, 50));
            list.Add(begin);
            list.Add(middle);
            list.Add(end);
            aircraft = new Carrier(list);

            timerMain.Start();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            level.Update(timerMain.Interval);
            pictureBoxMain.Refresh();
            //Text = level.UserPlane.MovementVector.ToString() + level.UserPlane.PlaneNoseDirection.ToString();
            Text = "K�t samolotu (for view): ";
            Text += Convert.ToString(level.UserPlane.RelativeAngle/(2*Math.PI)*360);
            Text += " K�t wektora ruchu: ";
            Text += Convert.ToString(level.UserPlane.MovementVector.Angle/(2*Math.PI)*360);
            Text += " Kierunek samolotu : " + level.UserPlane.Direction;
            Text += " Pr�dko�� : " + level.UserPlane.MovementVector.EuclidesLength;
            Text += " Y: " + level.UserPlane.Bounds.Peaks[0].Y;
            Text += " Y: " + level.UserPlane.Bounds.Peaks[3].Y;

            Text += "Silnik: " + level.UserPlane.IsEngineWorking;
        }

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            level.UserPlane.Bounds.Draw(e.Graphics, Color.Black);

            //foreach (AircraftCarrierTile tile in aircraft.AircraftCarrierTileList)
            //{
            //    tile.HitBound.Draw(e.Graphics, Color.Black);
            //}
            new Quadrangle(new PointD(0, 00), 200, 50).Draw(e.Graphics, Color.Black);
        }

        private void PlaneOnAircraftTestForm_KeyDown(object sender, KeyEventArgs e)
        {
            // const float angle = Math.PI / 36;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    level.OnSteerUp();
                    break;
                case Keys.Down:
                    level.OnSteerDown();
                    break;
                case Keys.Left:
                    level.OnSteerLeft();
                    break;
                case Keys.Right:
                    level.OnSteerRight();
                    break;
                case Keys.E:
                    level.OnToggleEngineOn();
                    break;
                    //case Keys.R: level.OnToggleEngineOff(); break;

                    //case Keys.P: level.UserPlane.Rotate(Math.PI * 0.02); break;
                    //case Keys.O: level.UserPlane.Rotate((-1) * Math.PI * 0.02); break;
                    //
                    //case Keys.R: plane.StopEngine(); break;
                    //case Keys.NumPad8: activeQ.Rotate(angle); break;
                    //case Keys.NumPad2: activeQ.Rotate(-angle); break;
                    //case Keys.B: activeQ = q1; break;
                    //case Keys.G: activeQ = q2; break;
                    //case Keys.NumPad9: activeQ.Move(Step, Step); break;
                    //case ' ': MessageBox.Show(q1.ToString()); break;
                default:
                    break;
            }
            pictureBoxMain.Refresh();
        }
    }
}