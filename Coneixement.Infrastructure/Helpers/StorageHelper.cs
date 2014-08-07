using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
namespace Coneixement.Infrastructure.Helpers
{
    public class StorageHelper
    {
        /// <summary>
        /// Get the Application Guid
        /// </summary>
        /// 
        public static Boolean IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                //Don't change FileAccess to ReadWrite, 
                //because if a file is in readOnly, it fails. 
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to //or being processed by another thread //or does not exist (has already been processed)
                return true;
            }
            finally { if (stream != null) stream.Close(); }
            //file is not locked 
            return false;
        }
        public static Guid AppGuid
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }
        /// <summary>
        /// Get the current assembly Guid.
        /// <remarks>
        /// Note that the Assembly Guid is not necessarily the same as the
        /// Application Guid - if this code is in a DLL, the Assembly Guid
        /// will be the Guid for the DLL, not the active EXE file.
        /// </remarks>
        /// </summary>
        public static Guid AssemblyGuid
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }
        /// <summary>
        /// Get the current user data folder
        /// </summary>
        public static string UserDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }
        /// <summary>
        /// Get the current user roaming data folder
        /// </summary>
        public static string UserRoamingDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return Path.Combine(folderBase, "IKON", "Coneixement");
            }
        }
        /// <summary>
        /// Get all users data folder
        /// </summary>
        public static string AllUsersDataFolder
        {
            get
            {
                string baselocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return baselocation;
            }
        }
        public static string DecryptedDatabase
        {
            get
            {
                string baselocation = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(baselocation, "Plain");
            }
        }
        /// <summary>
        /// Check the specified folder, and create if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
