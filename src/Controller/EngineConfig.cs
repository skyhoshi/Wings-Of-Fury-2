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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using FSLOgreCS;
using Mogre;
using Wof.Languages;
using Wof.Model.Level.Planes;
using Wof.Properties;
using Wof.Tools;

namespace Wof.Controller
{
    /// <summary>
    /// Statyczne ustawienia gry
    /// <author>Adam Witczak</author>
    /// </summary>
    public class EngineConfig
    {
        /// <summary>
        /// Wersja tej kompilacji WOfa. Powinna by� w formacie X.XX
        /// </summary>
        public static readonly String C_WOF_VERSION = "3.51";

        public static readonly bool C_IS_INTERNAL_TEST = false;
        public static readonly String C_IS_INTERNAL_TEST_INFO = "!!! Internal test version !!! ";

 		public static readonly String C_GAME_NAME = "Wings Of Fury 2: Return of the legend";
 		
        /// <summary>
        /// Czy bie��ca kompilacja jest demem?
        /// </summary>
        public static readonly bool C_IS_DEMO = false;

      
        public static readonly bool AdManagerDisabled = true;




        public static readonly String C_LOCAL_DIRECTORY = getLocalDirectoryByReflection();/*Application.LocalUserAppDataPath;*/

        public static string getLocalDirectoryByReflection()
        {
             string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
             string ret;
              Assembly a = Assembly.GetAssembly(typeof(Game));
                
                
                string company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(a, typeof(AssemblyCompanyAttribute), false)).Company;
                string version = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(a, typeof(AssemblyFileVersionAttribute), false)).Version;
                string product = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(a, typeof(AssemblyProductAttribute), false)).Product;
            /*
               string company = "Ravenlore";
               string version = "3.5.0.0";
               string product = "Wings of Fury 2";
*/
               ret = Path.Combine(basePath, company);
               ret = Path.Combine(ret, product);               
               ret = Path.Combine(ret, version);
               
            //   File.WriteAllText("c:/test.log", "Got the following path by reflection :"+ret);
            
            return ret;
        }
          
        
        public static readonly String C_ENGINE_CONFIG = Path.Combine(C_LOCAL_DIRECTORY, "wofconf.dat");
        public static readonly String C_FIRST_RUN = Path.Combine(C_LOCAL_DIRECTORY, "firstrun.dat");
        public static readonly String C_OGRE_CFG = Path.Combine(C_LOCAL_DIRECTORY, "ogre.cfg");
        public static readonly String C_LOG_FILE = Path.Combine(C_LOCAL_DIRECTORY, "ogre.log");
        public static readonly String C_ERROR_LOG_FILE = Path.Combine(C_LOCAL_DIRECTORY, "ogre_error_{0:yyyy-MM-dd_HH_mm_ss}.log");
        public static readonly String C_PERF_LOG_FILE = Path.Combine(C_LOCAL_DIRECTORY, "perf_ogre.log");

        public static readonly String C_COMPLETED_LEVELS_FILE = Path.Combine(C_LOCAL_DIRECTORY, "game.dat");

        public static readonly String C_HIGHSCORES_FILE = Path.Combine(C_LOCAL_DIRECTORY, "highscores.dat");
        public static readonly String C_SURVIVAL_FILE = Path.Combine(C_LOCAL_DIRECTORY, "survival.dat"); 

        public static readonly String C_GAME_INSTALL_DIR = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../.."));  // hardcode, nie chcemy patrzec do rejestru
        
        public static readonly String C_WOF_HOME_PAGE = "http://www.wingsoffury2.com";
        public static readonly String C_WOF_NEWS_PAGE = C_WOF_HOME_PAGE+"/news.php";
        public static readonly String C_WOF_UPDATE_CHECK_PAGE = C_WOF_HOME_PAGE+"/update_chk.php";
        public static readonly String C_WOF_SUPPORT_PAGE = C_WOF_HOME_PAGE;
       
        

        public static readonly bool UseLastHardwareSettings = false;

        
        public static readonly bool DisplayAxes = false;

        public static readonly bool DisplayBoundingQuadrangles = false;

        public static readonly bool AutoEncodeXMLs = true;


      

        /// <summary>
        /// Ustawiane tylko przy ladowaniu zmiennych z argv (-FreeLook). Punkt odniesienia
        /// </summary>
        public static bool FreeLook = false;

        public static bool AttachCameraToPlayerPlane = true;
        public static bool ManualCamera = false;

        public static bool BloomEnabled = false;
       
       
        public const float GameSpeedMultiplierSlow = 0.6f;
 		public const float GameSpeedMultiplierNormal = 1.0f;

               
 		
        /// <summary>
        /// Do efektu bullet-time
        /// </summary>
        public static float CurrentGameSpeedMultiplier = GameSpeedMultiplierNormal;

   

        public static bool UseAsyncModel = false;
        public static bool UpdateHydraxEveryFrame = true;

        
        public static bool Gore = false;
        public static bool MinimapNoseCamera = false;

        public static readonly bool StaticGeometryIslands = true; // wysypy u�ywaj� geometri statycznej do renderownia (untested)
        public static bool LowDetails = false; // niskie detale obiektow, mniej efektow (nadpisywane przez wofconf.dat)
        public static bool InverseKeys = false; // czy przyciski UP / DOWN s� zamienione? (nadpisywane przez wofconf.dat)
        public static bool SpinKeys = false; // Nie zapisywane do Wofconf.dat , czy trzeba chwilowo odwr�cic przyciski podczas spinu
     
        
        public static bool ShowIntro = true; // czy ma by� odgrywane intro? (nadpisywane przez wofconf.dat)
        public static bool LanguageDebugMode = false;

        public static PlaneType CurrentPlayerPlaneType;

        public static bool DisplayMinimap = true;


        private static bool displayingMinimap;
        public static bool DisplayingMinimap
        {
            set { displayingMinimap = value; }
            get { return displayingMinimap; }
        } // czy pokazywa� minimape? (nadpisywane przez wofconf.dat)

      

 		public static bool UseHydrax = true; // czy korzysta� z zaawansowanej symulacji wody? (nadpisywane przez wofconf.dat)

 		public static bool UseHardwareTexturePreloader = true; // czy wysylac do karty graficznej tesktury przed rozpoczeciem gry
 		
 		public static int HardwareTexturePreloaderTextureLimit = 64;

        public static bool AudioStreaming = false;
 		
    //    public static string Language = "en-GB";

        public static bool UseAlternativeSpinControl = false; // czy uzywac alternatywnego podejscia do sterowania spinem?

        
        public enum DifficultyLevel
        {
            Easy = 0,
            Medium = 1,
            Hard = 2
        };

       
        
        
        
        public enum ShadowsQualityTypes
        {
        	None = 0,
        	Low  = 1,
        	Medium = 2,
        	High  = 3
        	
        }

        public static DifficultyLevel Difficulty = DifficultyLevel.Easy; 

        public static bool SoundEnabled = true;
        public static FreeSL.FSL_SOUND_SYSTEM SoundSystem = FreeSL.FSL_SOUND_SYSTEM.FSL_SS_DIRECTSOUND;
        public static int MusicVolume = 50;

        protected static int soundVolume = 40;
        public static int SoundVolume
        {
             set 
             { 
                 soundVolume = value;
             }

             get
             {
                 return soundVolume;
             }
        }

        public static readonly bool IsEnhancedVersion = true; // Licensing.IsEhnancedVersion();
       
        public static string CopyLogFileToErrorLogFile()
        {
            try
            {
                string targetFilename = String.Format(EngineConfig.C_ERROR_LOG_FILE, DateTime.Now);
                File.Copy(EngineConfig.C_LOG_FILE, targetFilename);
                return targetFilename;
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        /// <summary>
        /// Warto�� przeciwna do LowDetails
        /// </summary>
        public static bool ExplosionLights = true; // rozb�yski �wiat�a przy wybuchach bomb

        /// <summary>
        /// Warto�� przeciwna do LowDetails
        /// </summary>
        public static bool BodiesStay = true;

        /// <summary>
        /// Warto�� przeciwna do LowDetails
        /// </summary>
        public static ShadowsQualityTypes ShadowsQuality = ShadowsQualityTypes.Medium;

        /// <summary>
        /// Pokazuje dodatkowe informacje w trakcie gry
        /// </summary>
        public static bool DebugInfo = false;
        
        /// <summary>
        /// jesli jest na tak to nacisniecie przycisku 'N' powoduje przejscie do nastepnego poziomu.
        /// </summary>
        public static bool DebugNextLevel = false;
          
        /// <summary>
        /// jesli jest na tak to nacisniecie przycisku 'K' powoduje zniszczenie samolotu gracza
        /// </summary>
        public static bool DebugKillPlane = false;
        

        /// <summary>
        /// Szybki start z pomini�ciem intro oraz menu
        /// </summary>
        public static bool DebugStart = false;

        /// <summary>
        /// Ustawia samolot w locie (nie trzeba startowa�)
        /// </summary>
        public static bool DebugStartFlying = false;

        /// <summary>
        /// Poziom (level) uruchamiany przy trybie DebugStart
        /// </summary>
        public static uint DebugStartLevel = 1;


        public static readonly int C_LOADING_DELAY_AD = 0;

        /// <summary>
        /// Bazowa wysoko�� czcionki wyra�ona w procentowej wysoko�ci wzgl�dem ekranu. Wykorzystywane przez AbstractScreen
        /// </summary>
        public static float CurrentFontSize = 0.028f;



        protected static bool AutoDetectLanguage()
        {
            String inputLanguage = InputLanguage.CurrentInputLanguage.Culture.IetfLanguageTag;
            if(LanguageManager.AvailableLanguages.ContainsValue(inputLanguage))
            {
                LanguageManager.SetLanguage(inputLanguage);
                return true;
            }

            return false;
        }


      
     /*   public static void SetDisplayMinimap(bool enabled)
        {
            EngineConfig.DisplayingMinimap = enabled;
        }*/
        public static void LoadEngineConfig()
        {
            try
            {
              
                // test to see if the file exists
                if (!File.Exists(C_FIRST_RUN))
                {
                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Game is running for the first time - auto detecting language");
                    try
                    {
                        if (AutoDetectLanguage())
                        {
                            LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Language has been detected: " + LanguageManager.ActualLanguageName);
                        }
                        else
                        {
                            LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Unable to detect language for: " + InputLanguage.CurrentInputLanguage.Culture.IetfLanguageTag);
                        }
                        
                    }
                    catch (Exception)
                    {
                      
                    }
                    
                }
               
                if (File.Exists(C_ENGINE_CONFIG))
                {
                  //  string inputLanguage = .;
                    
                    String[] configOptions = File.ReadAllLines(C_ENGINE_CONFIG);

                    BloomEnabled = Boolean.Parse(configOptions[0]);
                    SoundEnabled = Boolean.Parse(configOptions[1]);
                    try
                    {
                        SoundSystem =
                            (FreeSL.FSL_SOUND_SYSTEM) Enum.Parse(typeof (FreeSL.FSL_SOUND_SYSTEM), configOptions[2]);

                    }
                    catch (Exception)
                    {
                        SoundSystem = FreeSL.FSL_SOUND_SYSTEM.FSL_SS_DIRECTSOUND;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Sound system:" + SoundSystem);

                    try
                    {
                        SoundVolume = Int32.Parse(configOptions[3]);
                    }
                    catch (Exception)
                    {
                        SoundVolume = 40;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Sound volume:" + SoundVolume);

                    try
                    {
                        MusicVolume = Int32.Parse(configOptions[4]);
                    }
                    catch (Exception)
                    {
                        MusicVolume = 50;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Music volume:" + MusicVolume);
                  
                    try
                    {
                        LowDetails = Boolean.Parse(configOptions[5]);
                    }
                    catch (Exception)
                    {
                        LowDetails = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Low details:" + LowDetails);
                  

                    ExplosionLights = !LowDetails;
                    BodiesStay = !LowDetails;

                    try
                    {
                        InverseKeys = Boolean.Parse(configOptions[6]);
                    }
                    catch (Exception)
                    {
                        InverseKeys = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Inverse keys:" + InverseKeys);

                    //Console.WriteLine(System.Threading.Thread.CurrentThread.CurrentCulture.Name);

                    /*
                    try
                    {

                        if(configOptions[7].Length > 0)
                        {
                         //   LanguageManager.SetLanguage(configOptions[7]);
                        }
                        else
                        {
                            // autodetekcja
                        }
                        
                    }
                    catch (Exception)
                    {
                      //  LanguageManager.SetLanguage("en-GB");
                    }*/


                    try
                    {
                        UseAlternativeSpinControl = Boolean.Parse(configOptions[7]);
                    }
                    catch (Exception)
                    {
                        UseAlternativeSpinControl = false;
                    }
                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "UseAlternativeSpinControl:" + UseAlternativeSpinControl);



                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Language:" + LanguageManager.ActualLanguageCode);


					try
					{
						 //Difficulty
	                    switch (configOptions[8])
	                    {
	                        case "Easy":
	                            Difficulty = DifficultyLevel.Easy;
	                            break;
	                        case "Medium":
	                            Difficulty = DifficultyLevel.Medium;
	                            break;
	                        case "Hard":
	                            Difficulty = DifficultyLevel.Hard;
	                            break;
	                    }
					}
					catch(Exception)
					{
						Difficulty = DifficultyLevel.Easy;
					}
                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Difficulty:" + Difficulty);

					
                    try
					{
                    	if(ShowIntro)
                    	{
                    		// intro mo�na tylko wy�aczy�. Chodzi o to zeby zapobiec w��czeniu intra je�li uruchomiono program z -SkipIntro
                            ShowIntro = Boolean.Parse(configOptions[9]);
                    	}
                    	
                    }
                    catch(Exception)
                    {
                    	ShowIntro = true;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "ShowIntro:" + ShowIntro);

                     try
					{
                        DisplayMinimap = Boolean.Parse(configOptions[10]);
                    }
                    catch(Exception)
                    {
                    	DisplayMinimap = true;
                    }

                     LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "DisplayMinimap:" + DisplayMinimap);
                  //  DisplayingMinimap = DisplayMinimap;


                    try
					{
                    	UseHydrax = Boolean.Parse(configOptions[11]);
                    }
                    catch(Exception)
                    {
                    	UseHydrax = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "UseHydrax:" + UseHydrax);
                    
                //  UseHydrax = false;
                  
                    try
					{
						 //Difficulty
	                    switch (configOptions[12])
	                    {
	                        case "None":
	                            ShadowsQuality = ShadowsQualityTypes.None;
	                            break;
	                         case "Low":
	                            ShadowsQuality = ShadowsQualityTypes.Low;
	                            break;
	                        case "Medium":
	                             ShadowsQuality = ShadowsQualityTypes.Medium;
	                            break;
	                        case "High":
	                             ShadowsQuality = ShadowsQualityTypes.High;
	                            break;
	                    }
					}                    
					catch(Exception)
					{
						ShadowsQuality = ShadowsQualityTypes.Medium;
					}

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "ShadowsQuality:" + ShadowsQuality);
					
					try
					{
                    	 AudioStreaming = Boolean.Parse(configOptions[13]);
                    }
                    catch(Exception)
                    {
                    	 AudioStreaming = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "AudioStreaming:" + AudioStreaming);

                    try
					{
                        UseHardwareTexturePreloader = Boolean.Parse(configOptions[14]);
                    }
                    catch(Exception)
                    {
                        UseHardwareTexturePreloader = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "UseHardwareTexturePreloader:" + UseHardwareTexturePreloader);


					try
					{
                        Gore = Boolean.Parse(configOptions[15]);
                    }
                    catch(Exception)
                    {
                        Gore = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Gore:" + Gore);

                    
                    try
					{
                        HardwareTexturePreloaderTextureLimit = Int32.Parse(configOptions[16]);
                    }
                    catch(Exception)
                    {
                        HardwareTexturePreloaderTextureLimit = 64;
                    }
                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "HardwareTexturePreloaderTextureLimit:" + HardwareTexturePreloaderTextureLimit);

                   


                    

                    try
                    {
                        UseAsyncModel = Boolean.Parse(configOptions[17]);
                    }
                    catch (Exception)
                    {
                        UseAsyncModel = false;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "UseAsyncModel:" + UseAsyncModel);

                   
                    
                    try
                    {
                        UpdateHydraxEveryFrame = Boolean.Parse(configOptions[18]);
                    }
                    catch (Exception)
                    {
                        UpdateHydraxEveryFrame = true;
                    }

                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "UpdateHydraxEveryFrame:" + UpdateHydraxEveryFrame);

                   

                    try
                    {

                        CurrentPlayerPlaneType =
                            (PlaneType)Enum.Parse(typeof(PlaneType), configOptions[19]);
                    }
                    catch (Exception)
                    {
                        CurrentPlayerPlaneType = PlaneType.P47;
                    }

                    if(!IsEnhancedVersion)
                    {
                        CurrentPlayerPlaneType = PlaneType.P47;
                    }


                    LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "CurrentPlayerPlaneType:" + CurrentPlayerPlaneType);

                    
                }
                else
                {
                    SaveEngineConfig();
                }

              

            }
            catch (Exception ex)
            {
            	if(LogManager.Singleton != null) {
            		LogManager.Singleton.LogMessage(LogMessageLevel.LML_NORMAL, "Exception in EngineConfig: "+ex.Message+" "+ex.InnerException+", "+ex.StackTrace);
            	}
                //Console.WriteLine(ex.ToString());
            }

           

        }

        public static void SaveEngineConfig()
        {
            String[] configuration = new String[20];
            configuration[0] = BloomEnabled.ToString();
            configuration[1] = SoundEnabled.ToString();
            configuration[2] = SoundSystem.ToString();
            configuration[3] = SoundVolume.ToString();
            configuration[4] = MusicVolume.ToString();
            configuration[5] = LowDetails.ToString();
            configuration[6] = InverseKeys.ToString();
            configuration[7] = UseAlternativeSpinControl.ToString();// Settings.Default.Language;
            configuration[8] = Difficulty.ToString();
            configuration[9] = ShowIntro.ToString();
            configuration[10] = DisplayMinimap.ToString();
            configuration[11] = UseHydrax.ToString();
            configuration[12] = ShadowsQuality.ToString();
            configuration[13] = AudioStreaming.ToString();
            configuration[14] = UseHardwareTexturePreloader.ToString();
            configuration[15] = Gore.ToString();
            configuration[16] = HardwareTexturePreloaderTextureLimit.ToString();
            configuration[17] = UseAsyncModel.ToString();
            configuration[18] = UpdateHydraxEveryFrame.ToString();
            configuration[19] = CurrentPlayerPlaneType.ToString();
            
             
            ExplosionLights = !LowDetails;
            BodiesStay = !LowDetails;
         
            //File.Create(C_ENGINE_CONFIG);
            File.WriteAllLines(C_ENGINE_CONFIG, configuration);
           
          //  string materialDir = "../../media/materials/scripts/ParentScripts/";
            if(ShadowsQuality >0 /*&& UseHydrax*/)
            {
            //	File.Copy(materialDir+"0NormalMappedSpecular.base", materialDir+"0NormalMappedSpecular.material",true );
            } else
            {
            //	File.Copy(materialDir+"0NormalMappedSpecularNoShadows.base", materialDir+"0NormalMappedSpecular.material",true );
            	
            }



           
        }


       
    }
}