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
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Wof.Controller;

namespace Wof.Tools
{

    public class Licensing
    {

       public static readonly string C_LICENSE_FILE = "enhanced.dat";
       private static readonly string C_ENHANCED_VERSION_LICENSE = "asZc2czA4l6a12sd";
       private static string hash;


       private static void Main(string[] args)
       {
           try
           {
               Console.WriteLine("Wof license builder v. " + EngineConfig.C_WOF_VERSION);
           	 //  BuildLicenseFile(Hash);
             //  IsEhnancedVersion();
               if(args.Length != 1)
               {
                   Console.WriteLine("License builder for WOF. Usage: ");
                   Console.WriteLine("licenseBuilder.exe hash");
                   return;
               }
           
               BuildLicenseFile(args[0]);
               MessageBox.Show("License for hash ("+args[0]+") save to file: " + C_LICENSE_FILE );
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
            
       }


       private static string DecryptLicense(string licenseContents)
       {
        
           return RijndaelSimple.Decrypt(licenseContents, Hash, RijndaelSimple.saltValue,
                                  RijndaelSimple.hashAlgorithm, RijndaelSimple.passwordIterations,
                                  RijndaelSimple.initVector, RijndaelSimple.keySize);

            
       }
       private static string DecryptLicensePHP(string licenseContents, string key)
       {
       		return RijndaelSimple.AES_decrypt(licenseContents, key, RijndaelSimple.AES_IV);
       }
      
       

       public static bool BuildLicenseFile(string inputHash)
       {
           string encrypted = RijndaelSimple.Encrypt(C_ENHANCED_VERSION_LICENSE, inputHash, RijndaelSimple.saltValue,
                                 RijndaelSimple.hashAlgorithm, RijndaelSimple.passwordIterations,
                                 RijndaelSimple.initVector, RijndaelSimple.keySize);

          
           
       	   string desEncryptedHash = RijndaelSimple.AES_encrypt(inputHash, RijndaelSimple.AES_Key, RijndaelSimple.AES_IV);
       	   string licenseKey = desEncryptedHash.Substring(0, 32);
       	   
       	   string encryptedLicense = RijndaelSimple.AES_encrypt(C_ENHANCED_VERSION_LICENSE, licenseKey, RijndaelSimple.AES_IV);

           File.WriteAllText(C_LICENSE_FILE, encrypted+"\r\n"+desEncryptedHash+"\r\n"+encryptedLicense); // pojedyncza licencja
           
           return true;          
       }


       public static string Hash
       {
           get
           {
               if (hash == null) BuildHash();

               return hash;
           }
       }

       public static bool BuildLicenseFile()
       {
           try
           {
               BuildHash();
               BuildLicenseFile(hash);

               return true;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex);
               return false;
           }
          

       }

       /// <summary>
       /// Determines whether the specified file is readable.
       /// </summary>
       /// <param name="filename">The filename.</param>
       /// <returns>
       ///   <c>true</c> if the specified file is readable; otherwise, <c>false</c>.
       /// </returns>
       public static bool IsReadable(string filename)
       {
           try
           {
               string[] bytes = File.ReadAllLines(filename);
           }
           catch (Exception ex)
           {
               return false;
           }

           return true;
           

       }

       /// <summary>
       /// Determines whether the specified file is writeable.
       /// </summary>
       /// <param name="filename">The filename.</param>
       /// <returns>
       ///   <c>true</c> if the specified file is writeable; otherwise, <c>false</c>.
       /// </returns>
       public static bool IsWriteable(string filename)
       {
           // Ensure that the file is readonly.
           try
           {
               File.SetAttributes(filename, File.GetAttributes(filename));

               //Create the file. 
               using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
               {
                   if (fs.CanWrite)
                   {
                       return true;
                       //Console.WriteLine("The stream for file {0} is writable.", filename);
                   }
                   else
                   {
                       return false;
                       // Console.WriteLine("The stream for file {0} is not writable.", filename);
                   }
               }
           }
           catch (Exception ex)
           {
               return false;
              
           }
         
       }


       public static bool IsUser()
       {
           //bool value to hold our return value
           bool isUser;
           try
           {

               //get the currently logged in user
               WindowsIdentity user = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(user);
               isUser = principal.IsInRole(WindowsBuiltInRole.User);
             
           }
           catch (UnauthorizedAccessException ex)
           {
               isUser = false;
           }
           catch (Exception ex)
           {
               isUser = false;
           }
           return isUser;
       }

       public static bool IsUserAdministrator()
       {
           //bool value to hold our return value
           bool isAdmin;
           try
           {
             
               //get the currently logged in user
               WindowsIdentity user = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(user);
               isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
           }
           catch (UnauthorizedAccessException ex)
           {
               isAdmin = false;
           }
           catch (Exception ex)
           {
               isAdmin = false;
           }
           return isAdmin;
       }
       public static bool CanBuildEnhancedVersionHash()
       {
           BuildHash();
           return hash != null;

       }

       public static bool IsEhnancedVersion()
       {          	  
        
		   BuildHash();

           string loc2 = EngineConfig.C_LOCAL_DIRECTORY + "\\" + C_LICENSE_FILE;
           if (!File.Exists(C_LICENSE_FILE) && !File.Exists(loc2) && !File.Exists("../../"+C_LICENSE_FILE))
           {
               return false;
           }
           else
           {
               if(hash == null)
               {
                   // nie udalo sie stworzyc hasha. Ktos probuje odpalic wersje rozszerzona
                   MessageBox.Show("Unable to build Enhanced version hash.\r\nWe are sorry but " + EngineConfig.C_GAME_NAME +
                                   " Enhanced version cannot be run under Windows Guest Account. Please run the game under Administrator account.\r\n" +
                                   " Starting standard version", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                   return false;
               }
           }
           string contents;

           try
           {

               if (File.Exists(C_LICENSE_FILE))
               {
                   contents = File.ReadAllText(C_LICENSE_FILE);
               }
               else if (File.Exists(loc2))
               {
                   contents = File.ReadAllText(loc2);
               } else if (File.Exists("../../"+C_LICENSE_FILE))
               {
                   contents = File.ReadAllText("../../"+C_LICENSE_FILE);
               }
               else
               {
                   return false;
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show("Unable to read Enhanced version license\r\nWe are sorry but " + EngineConfig.C_GAME_NAME +
                                  " Enhanced version cannot be run under Windows Guest Account. Please run the game as the Administrator.\r\n" +
                                  " Starting standard version \r\nThe error was:"+ex, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return false;
           }
          
              
           
           try
           {
           	
           	   string[] licenses = contents.Split(new String[]{"\r\n"}, StringSplitOptions.None);
           	                                   
           	   int i = 0;  
           	   if(licenses.Length == 1)
           	   {
           	   	   string plain = DecryptLicense(licenses[0]);
	               if (plain.Equals(C_ENHANCED_VERSION_LICENSE))
	               {
	                    return true;
	               } else {
	               		return false;
	               }
           	   }else if(licenses.Length == 3) {
           	   	
           	   	   try{
	           	   	   // old fashion license
	           	   	   if(licenses[0].Length >0)
	           	   	   {
		           	   	   string plain = DecryptLicense(licenses[0]);
		           	   	   if (plain.Equals(C_ENHANCED_VERSION_LICENSE))
			               {
			                   return true;
			               } 
	           	   	   }
	           	   	   
	           	   	   // new PHP license
	           	   	   string encryptedHash = licenses[1];
	           	   	   string plainHash = DecryptLicensePHP(encryptedHash, RijndaelSimple.AES_Key);
		               if (plainHash.Equals(Hash))
		               { 		               	   
			               string license = DecryptLicensePHP(licenses[2], encryptedHash.Substring(0, 32));
			               if (license.Equals(C_ENHANCED_VERSION_LICENSE))
			               {
			                   return true;
			               }
		                   
		               }	               
	           	   	   return false;
	           	   	
           	   	   }
	           	   catch(Exception ex)
	           	   {
                        MessageBox.Show("The license 3.4/3.5 file was invalid. Starting standard version\r\nThe error was:" + ex, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      
	           	   		return false;
	           	    }
	           	   	
           	   }
           	   return false;
           	   
		     
		            
		     
              
           }
           catch (Exception ex)
           {
               MessageBox.Show("The license file was invalid. Starting standard version\r\nThe error was:" + ex, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      
               return false;
           }
           

           return false;
       }
       

      

       private static void BuildHash()
       {
         
           string ret = "";
           string id = null;
           try
           {
               id = GetId();
           }
           catch (Exception)
           {
               id = null;
              
           }

           if(id == null)
           {
               hash = null;
               return;
           }


           byte[] arr = SHA1_Hash.DigestMessage(id);
           foreach (byte b in arr)
           {
               ret += b.ToString()+ ";";
           }

           hash = ret;
       }

       public static string GetId()
       {
          
           ManagementObjectSearcher searcher;
           string[] keys = new string[] { "Win32_baseboard", "Win32_Processor" };
           string ret = "";

           try
           {
               searcher = new ManagementObjectSearcher("select * from " + keys[0]);
               var mobos = searcher.Get();

               foreach (var m in mobos)
               {
                   foreach (PropertyData PC in m.Properties)
                   {
                       if (PC.Name.Equals("SerialNumber") || PC.Name.Equals("Product"))
                       {
                           ret += PC.Value;
                       }

                   }
               }


               searcher = new ManagementObjectSearcher("select * from " + keys[1]);
               mobos = searcher.Get();

               foreach (var m in mobos)
               {
                   foreach (PropertyData PC in m.Properties)
                   {
                       if (PC.Name.Equals("ProcessorId"))
                       {
                           ret += PC.Value;
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               
               throw;
           }
          


           return ret;

       }
    }
}
