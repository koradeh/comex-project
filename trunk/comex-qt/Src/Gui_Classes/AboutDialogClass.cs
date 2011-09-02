
using System;
using Qyoto;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using comexbase;

using log4net;

namespace comexqt
{
	
	
	public class AboutDialogClass: QDialog
	{
		
		// ATTRIBUTES
		Ui.AboutDialog aboutdialog_UI;
		
		
		
		// CONSTRUCTOR
		public AboutDialogClass()
		{
			
			// Create new aboutwindow_UI object
			aboutdialog_UI = new Ui.AboutDialog();
			
			// Configure layout of this new QDialog with 
			// aboutdialog_UI objects and data			
			aboutdialog_UI.SetupUi(this);
			
			// Update Qt graphic objects
			UpdateGraphicObjects();

			
			// Update reactors (eventhandler)
			UpdateReactors();			
			
			
		}
		
		
		
		
		
		private void UpdateGraphicObjects()
		{
			
			// fill graphic objects informations
			this.WindowTitle = GlobalObj.LMan.GetString("frmabout");
			aboutdialog_UI.tabInfo.SetTabText(0, GlobalObj.LMan.GetString("about"));
			aboutdialog_UI.TxtInfo.SetHtml(GlobalObj.LMan.GetString("infodesc").Replace("\t", ""));

			aboutdialog_UI.tabInfo.SetTabText(1, GlobalObj.LMan.GetString("thanks"));
			aboutdialog_UI.TxtThanks.SetText(GlobalObj.LMan.GetString("thanksdesc").Replace("\t", "") + GlobalConst.ThanksTo);

			aboutdialog_UI.LblName.Text = MainClass.AppNameVer + " [" + GlobalObj.AppNameVer + "]";			
			aboutdialog_UI.LblDesc.Text =  GlobalObj.LMan.GetString("description");
			
			
			

			
			
			// add logo
			aboutdialog_UI.FrameTop.Palette.Background().SetColor(new QColor(255,255,255));
			
		}
		
		
		
		
		private void UpdateReactors()
		{
			// Configure events reactors
			Connect( aboutdialog_UI.buttonBox, SIGNAL("clicked(QAbstractButton*)"), this, SLOT("ActionExit(QAbstractButton*)"));			
			
		}
		
		
		
		
		#region Q_SLOTS
		
		[Q_SLOT]
		public void ActionExit(QAbstractButton buttonPressed)
		{
			//Console.WriteLine("Pressed::" + buttonPressed.Text);
			this.Close();
			
		}
		
		
		
		
		#endregion Q_SLOTS
		
		
		
	}
}
