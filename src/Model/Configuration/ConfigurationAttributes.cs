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

namespace Wof.Model.Configuration
{
    /// <summary>
    /// Odwzorowanie pliku xml na obiekty klas.
    /// </summary>
    /// <remarks>Wszystkie pola w klasach sa wywolywane dynamicznie.</remarks>
    public static class ConfigurationAttributes
    {
        public class UserPlane
        {
            public const string oilLoss = @"oilLoss";
            public const string moveStep = @"moveStep";
            public const string maxSpeed = @"maxSpeed";
            public const string petrolLoss = @"petrolLoss";
            public const string bombCount = @"bombCount";
            public const string rocketCount = @"rocketCount";
            public const string torpedoCount = @"torpedoCount";
            public const string breakingPower = @"breakingPower";
            public const string hitCoefficient = @"hitCoefficient";
            public const string rangeSlowWheelingSpeed = @"rangeSlowWheelingSpeed";
            public const string rangeFastWheelingMaxSpeed = @"rangeFastWheelingMaxSpeed";
            public const string engineCounterThreshold = @"engineCounterThreshold";
            public const string engineCounterThresholdInAir = @"engineCounterThresholdInAir";
            public const string breakingMinSpeed = @"breakingMinSpeed";
            public const string sinkingSpeed = @"sinkingSpeed";
            public const string maxHeight = @"maxHeight";
            public const string landingSpeed = @"landingSpeed";
            public const string rotateStep = @"rotateStep";
            public const string userRotateBrakingFactor = @"userRotateBrakingFactor";
            public const string userMaxRotateValue = @"userMaxRotateValue";
            public const string godMode = @"godMode";
            public const string width = @"width";
            public const string height = @"height";
            public const string canSpin = @"canSpin";
            public const string turningDuration = @"turningDuration";
           
        }

        public class EnemyPlane : UserPlane
        {
            public const string speed = @"speed";          
            public const string minPitch = @"minPitch";
            public const string viewRange = @"viewRange";        
            public const string nextRocketInterval = @"nextRocketInterval";
            public const string storagePlaneDistanceFault = @"storagePlaneDistanceFault";
            public const string safeUserPlaneDistance = @"safeUserPlaneDistance";
            public const string attackStoragePlaneDistance = @"attackStoragePlaneDistance";
            public const string accuracy = @"accuracy";
            public const string carrierDistanceAlarm = @"carrierDistanceAlarm";        
            public const string maxSimultaneousEnemyPlanes = @"maxSimultaneousEnemyPlanes";
        }

        public class Soldier
        {
            public const string minSpeed = @"minSpeed";
            public const string maxSpeed = @"maxSpeed";
            public const string homelessTime = @"homelessTime";
        }

        public class Bunker
        {
            public const string fireDelay = @"fireDelay";
            public const string horizonHeight = @"horizonHeight";
            public const string horizonWidth = "horizonWidth";
        }
        
        public class FlakBunker
        {
            public const string fireDelay = @"fireDelay";
            public const string horizonMinDistance = @"horizonMinDistance";
            public const string horizonMaxDistance = @"horizonMaxDistance";
            public const string fireSpreadX = @"fireSpreadX";
            public const string fireSpreadY = @"fireSpreadY";
            public const string maxDamagePerHit = @"maxDamagePerHit";
            public const string damageRange = @"damageRange";
            public const string initialFlakSpeed = @"initialFlakSpeed";  
            
            
            
        }


        public class Gun
        {
             public const string fireInterval = @"fireInterval";
             public const string baseSpeed = @"baseSpeed";
             public const string baseDamage = @"baseDamage";
        }

        public class Bomb
        {
            public const string accelerationInterval = @"accelerationInterval";
            public const string airResistance = @"airResistance";
            public const string fireInterval = @"fireInterval";
            public const string gravitation = @"gravitation";
        }

        public class Rocket
        {
            public const string fireInterval = @"fireInterval";
            public const string baseSpeed = @"baseSpeed";
        }

        public class Torpedo
        {
            public const string fireInterval = @"fireInterval";
            public const string baseSpeed = @"baseSpeed";
        }

        public class Effects
        {
            public const string bulletLoadTime = @"bulletLoadTime";
        }        
    }
}