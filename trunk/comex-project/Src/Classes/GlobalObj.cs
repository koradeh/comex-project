using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Pcsc_Sharp;
using Gtk;


namespace comex
{
	
	/// <summary>
	/// Static global class
	/// </summary>
	public static class GlobalObj
	{

		private static string languageFolder = "";
		private static string languageTag = "";
		
		private static LanguageManager lMan = null;
		
		
		#region Properties
		
		
		/// <summary>
		/// Return language manager object
		/// </summary>
		public static LanguageManager LMan { get { return lMan; } }
		
		
		
		/// <summary>
		/// PCSC reader manager
		/// </summary>
		public static Pcsc PCSC { get; set ;}
		
		
		/// <summary>
		/// PCSC readers name
		/// </summary>
		public static string[] PCSC_Readers { get; set ;}
		
		
		
		/// <summary>
		/// Return selected reader name
		/// </summary>
		public static string SelectedReader { get; set;	}
		
		
		/// <summary>
		/// Return language tag to use
		/// </summary>
		public static string LanguageTag { get { return languageTag; }	}
		
		
		
		
		/// <summary>
		/// Application folder path
		/// </summary>
		public static string AppPath { get; set; }
		
		
		
		
		/// <summary>
		/// Application command line arguments
		/// </summary>
		public static List<string> AppArgs { get; set; }
		
		
		
		
		/// <summary>
		/// Get Application name and release
		/// </summary>
		public static string AppNameVer
		{
			get 
			{
				return Assembly.GetExecutingAssembly().GetName().Name + " " +
					Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}
		
		
		/// <summary>
		/// Path of log file used
		/// </summary>
		public static string LogFilePath { get; set; }
		
		
		
		public static bool LogToConsole = false;
		public static bool LogToFile = false;
		
		
		
		
		#endregion Properties
		
		
		
		
		
		
		
		
		#region Public Methods
		
		/// <summary>
		/// Wait for gui processes
		/// </summary>
		public static void GtkWait()
		{
			while (Gtk.Application.EventsPending ())
			{
                Gtk.Application.RunIteration ();
			}
		}
		
		
		
		
		/// <summary>
		/// Set language to use
		/// </summary>
		public static void SetLanguage()
		{
			string envLang = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag;
			languageFolder = AppPath + Path.DirectorySeparatorChar + "Languages";
			
			// check for language folder
			if (!Directory.Exists(languageFolder))
			{
				throw new Exception("no language folder founded... ");
			}
			
			// check for language file
			DirectoryInfo di = new DirectoryInfo(languageFolder);
			if (di.GetFiles(envLang + ".xml").Length == 1)
			{
				// language file exists, use it
				languageTag = envLang;
			}
			else
			{
				// language file don't exists, use en-US as default
				languageTag = "en-US";
			}
			
			lMan = new LanguageManager(languageFolder + Path.DirectorySeparatorChar + languageTag + ".xml");
			
		}
		
		
		
		
		
		public static string InitPCSC()
		{
			PCSC = new Pcsc();
			string retStr = PCSC.EstablishContext();
			
			if (retStr != "")
			{
				return retStr;
			}
			
			// retrieve pcsc readers
			string[] pcsc_readers = new string[0];
			retStr = PCSC.ListReaders(out pcsc_readers);			
			
			if (retStr != "")
			{
				return retStr;
			}
			PCSC_Readers = pcsc_readers;
			
			return "";
		}
		
		
		
		
		public static void ClosePCSC()
		{
			PCSC.Disconnect();
			PCSC.ReleaseContext();
		}
		
		
		
		
		
		
		
		#endregion Public Methods
		
		
	}
}

