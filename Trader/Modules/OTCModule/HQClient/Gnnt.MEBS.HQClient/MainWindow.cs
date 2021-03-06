using AppUpdate;
using Gnnt.MEBS.HQClient.gnnt.ClientForms;
using Gnnt.MEBS.HQClient.gnnt.MEBS.HQClient;
using Gnnt.MEBS.HQClient.gnnt.MEBS.HQClient.Indicator;
using Gnnt.MEBS.HQClient.Properties;
using Gnnt.MEBS.HQModel;
using Gnnt.MEBS.HQModel.DataVO;
using SplitButtonDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ToolsLibrary.util;
using TPME.Log;
namespace Gnnt.MEBS.HQClient
{
	public class MainWindow : Form
	{
		private delegate void SetPictureEnableCallBack(bool enable);
		public ButtonUtils buttonUtils;
		public PluginInfo pluginInfo;
		public SetInfo setInfo;
		public MultiQuoteData multiQuoteData;
		public HQClientForm mainForm;
		private Image backgroundImage;
		private Image backgroundImageleave;
		private SysMessage sys;
		private bool updateColse;
		private bool bUpdate;
		private Thread startCheckUpdate;
		public static List<string> diQuStrings = new List<string>();
		public static List<string> hangYeStrings = new List<string>();
		public static bool isLoadItemButtom = false;
		public static int selectIndexHY = -1;
		public static int selectIndexDQ = -1;
		private Color topBtMouseColor = Color.FromArgb(255, 255, 0);
		private Color topBtColor = Color.FromArgb(255, 138, 0);
		private About about;
		private int lbLocationY = 35;
		private int lbHeight = 25;
		private MainWindow.SetPictureEnableCallBack setEnable;
		private IContainer components;
		private ToolTip toolTipTopImage;
		private ContextMenuStrip contextMenuHY;
		private ContextMenuStrip contextMenuDiQu;
		private Panel panelHQ;
		private PictureBox pictureBoxConnect;
		private Label lbTime;
		private Label lbConnect;
		private Panel topPanel;
		private Label labelSet;
		private Label lbLogin;
		private SplitButton btDiQu;
		private SplitButton btHangYe;
		private Button btSysMessage;
		private Label lbAbout;
		private Label label1;
		private Button btNews;
		private Button btOwnGoods;
		private Panel panelLeftBtn;
		private Button btRanking;
		private Button btMultiRanking;
		private Button btLine;
		private Button btBill;
		private Panel panelMain;
		private Panel imagePanel;
		private PictureBox pictureSet;
		private PictureBox pictureF10;
		private PictureBox pictureDown;
		private PictureBox pictureUp;
		private PictureBox Indecator;
		private PictureBox pictureBoxBill;
		private PictureBox refreshBt;
		private PictureBox AnyMin;
		private PictureBox BackBtn;
		private PictureBox AnyDay;
		private PictureBox FifteenMin;
		private PictureBox KLineBtn;
		private PictureBox FourHr;
		private PictureBox Day;
		private PictureBox SearchBtn;
		private PictureBox FiveMin;
		private PictureBox Quarter;
		private PictureBox ThirtyMin;
		private PictureBox StartButton;
		private PictureBox FSZSBtn;
		private PictureBox TwoHr;
		private PictureBox Week;
		private PictureBox OneMin;
		private PictureBox ThreeMin;
		private PictureBox Hour;
		private PictureBox Month;
		private PictureBox BJPMBtn;
		private PictureBox Split;
		public MainWindow(PluginInfo _pluginInfo)
		{
			this.InitializeComponent();
			this.pluginInfo = _pluginInfo;
			this.setInfo = new SetInfo();
			this.setInfo.init(this.pluginInfo.ConfigPath);
			this.mainForm = new HQClientForm(this);
			base.Activated += new EventHandler(this.MainWindow_Activated);
			base.GotFocus += new EventHandler(this.MainWindow_GotFocus);
		}
		private void MainWindow_Load(object sender, EventArgs e)
		{
			try
			{
				this.BackColor = Color.Black;
				this.SetControl(true);
				this.SetControlText();
				this.iniSizeAndStyle();
				this.SetPicture();
				this.SetPictureEnable(false);
				if (this.mainForm != null)
				{
					this.mainForm.Size = this.panelHQ.Size;
					this.mainForm.TopLevel = false;
					this.mainForm.Parent = this.panelHQ;
					this.mainForm.Dock = DockStyle.Fill;
					this.mainForm.Show();
				}
				else
				{
					Logger.wirte(1, "设置窗体停靠时mainForm = null");
				}
				if (this.mainForm.CurHQClient != null)
				{
					this.mainForm.CurHQClient.setPictureEnable = new HQClientMain.SetPictureEnableCallback(this.SetPictureEnable);
				}
				this.buttonUtils = this.mainForm.buttonUtils;
				this.multiQuoteData = this.mainForm.multiQuoteData;
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "MainWindow_Load异常：" + ex.Message);
			}
		}
		private void SetPicture()
		{
			this.Indecator.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_Indicator");
			this.pictureUp.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_ZoomIn");
			this.pictureDown.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_ZoomOut");
			this.pictureF10.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_F10Btn");
			this.pictureSet.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_UserSet");
			this.BackBtn.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_back");
			this.StartButton.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_start");
			this.refreshBt.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_refresh");
			this.BJPMBtn.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_bjpm");
			this.FSZSBtn.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_fszs");
			this.KLineBtn.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_KLine");
			this.pictureBoxBill.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_Bill");
			this.Day.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_day");
			this.Week.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_week");
			this.Month.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_month");
			this.Quarter.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_quarter");
			this.AnyDay.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_anyday");
			this.Split.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_split");
			this.OneMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_onemin");
			this.ThreeMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_threemin");
			this.FiveMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_fivemin");
			this.FifteenMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_fifteenmin");
			this.ThirtyMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_thirtymin");
			this.Hour.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_hour");
			this.TwoHr.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_twohr");
			this.FourHr.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_fourhr");
			this.AnyMin.Image = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_anymin");
			this.backgroundImage = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_select");
			this.backgroundImageleave = (Image)this.pluginInfo.HQResourceManager.GetObject("HQImg_leave");
		}
		private void ShowMessage()
		{
			this.sys = new SysMessage(this);
			this.sys.ShowDialog();
		}
		private void MainWindow_Activated(object sender, EventArgs e)
		{
			this.mainForm.Focus();
		}
		private void MainWindow_GotFocus(object sender, EventArgs e)
		{
			this.mainForm.Focus();
		}
		private void StartCheckUpdate()
		{
			if (!this.bUpdate)
			{
				return;
			}
			this.startCheckUpdate = new Thread(new ParameterizedThreadStart(this.CheckUpdate));
			this.startCheckUpdate.Start();
		}
		private void CheckUpdate(object nul)
		{
			CheckForUpdate checkForUpdate = new CheckForUpdate("UpdateList.xml");
			if (checkForUpdate.StartCheck())
			{
				if (checkForUpdate.UpdateLevel == 1)
				{
					checkForUpdate.StartUpdate();
					this.updateColse = true;
					MethodInvoker method = new MethodInvoker(base.Close);
					base.BeginInvoke(method);
					return;
				}
				string @string = this.pluginInfo.HQResourceManager.GetString("TradeStr_LoginForm_UpdateTitle");
				string string2 = this.pluginInfo.HQResourceManager.GetString("TradeStr_LoginForm_UpdateContext");
				if (MessageBox.Show(string2, @string, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
				{
					checkForUpdate.StartUpdate();
					this.updateColse = true;
					MethodInvoker method2 = new MethodInvoker(base.Close);
					base.BeginInvoke(method2);
				}
			}
		}
		public void SetControl(bool isFristOnce = true)
		{
			if (!Tools.StrToBool(this.pluginInfo.HTConfig["MultiMarket"].ToString(), false) && isFristOnce)
			{
				this.topPanel.Visible = false;
				this.panelHQ.Top -= this.topPanel.Height;
				this.panelHQ.Height += this.topPanel.Height;
				return;
			}
			if ((this.contextMenuDiQu.Items.Count == 0 || this.contextMenuHY.Items.Count == 0) && this.mainForm.CurHQClient.m_codeList.Count != 0 && this.mainForm.CurHQClient.m_htRegion.Count != 0)
			{
				for (int i = 0; i < this.mainForm.CurHQClient.m_codeList.Count; i++)
				{
					CommodityInfo commodityInfo = (CommodityInfo)this.mainForm.CurHQClient.m_codeList[i];
					bool flag = true;
					bool flag2 = true;
					if (MainWindow.diQuStrings.Count == 0)
					{
						MainWindow.diQuStrings.Add(commodityInfo.region);
					}
					if (MainWindow.hangYeStrings.Count == 0)
					{
						MainWindow.hangYeStrings.Add(commodityInfo.industry);
					}
					for (int j = 0; j < MainWindow.diQuStrings.Count; j++)
					{
						if (MainWindow.diQuStrings[j] == commodityInfo.region)
						{
							flag = false;
						}
					}
					for (int k = 0; k < MainWindow.hangYeStrings.Count; k++)
					{
						if (MainWindow.hangYeStrings[k] == commodityInfo.industry)
						{
							flag2 = false;
						}
					}
					if (flag)
					{
						MainWindow.diQuStrings.Add(commodityInfo.region);
					}
					if (flag2)
					{
						MainWindow.hangYeStrings.Add(commodityInfo.industry);
					}
				}
				ToolStripMenuItem value = new ToolStripMenuItem("全部");
				ToolStripMenuItem value2 = new ToolStripMenuItem("全部");
				this.contextMenuHY.Items.Add(value2);
				this.contextMenuDiQu.Items.Add(value);
				for (int l = 0; l < MainWindow.diQuStrings.Count; l++)
				{
					string text = "";
					if (this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[l]] != null)
					{
						text = this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[l]].ToString();
					}
					else if (MainWindow.diQuStrings[l] != null)
					{
						if (l != MainWindow.selectIndexDQ)
						{
							text = MainWindow.diQuStrings[l].ToString();
						}
						else
						{
							text = MainWindow.diQuStrings[l].ToString() + " √";
						}
					}
					ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text);
					toolStripMenuItem.Tag = MainWindow.diQuStrings[l];
					this.contextMenuDiQu.Items.Add(toolStripMenuItem);
					this.contextMenuDiQu.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuDiQu_ItemClicked);
				}
				for (int m = 0; m < MainWindow.hangYeStrings.Count; m++)
				{
					string text2 = "";
					if (this.mainForm.CurHQClient.m_htIndustry.Count != 0)
					{
						text2 = this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[m]].ToString();
					}
					else if (MainWindow.hangYeStrings[m] != null)
					{
						if (m != MainWindow.selectIndexHY)
						{
							text2 = MainWindow.hangYeStrings[m].ToString();
						}
						else
						{
							text2 = MainWindow.hangYeStrings[m].ToString() + " √";
						}
					}
					ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem(text2);
					toolStripMenuItem2.Tag = MainWindow.hangYeStrings[m];
					this.contextMenuHY.Items.Add(toolStripMenuItem2);
					this.contextMenuHY.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuHY_ItemClicked);
				}
				MainWindow.isLoadItemButtom = true;
			}
		}
		public void clearMenuItem()
		{
			if (this.buttonUtils.CurButtonName != "Select")
			{
				if (MainWindow.selectIndexDQ != -1)
				{
					foreach (ToolStripMenuItem toolStripMenuItem in this.contextMenuDiQu.Items)
					{
						if (toolStripMenuItem.Text.Contains(" √"))
						{
							toolStripMenuItem.Text = toolStripMenuItem.Text.Replace(" √", "");
							MainWindow.selectIndexDQ = -1;
							break;
						}
					}
				}
				if (MainWindow.selectIndexHY != -1)
				{
					foreach (ToolStripMenuItem toolStripMenuItem2 in this.contextMenuHY.Items)
					{
						if (toolStripMenuItem2.Text.Contains(" √"))
						{
							toolStripMenuItem2.Text = toolStripMenuItem2.Text.Replace(" √", "");
							MainWindow.selectIndexHY = -1;
							break;
						}
					}
				}
			}
		}
		private void contextMenuDiQu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			try
			{
				ToolStripItem clickedItem = e.ClickedItem;
				if (clickedItem != null)
				{
					if (clickedItem.Text != "全部")
					{
						for (int i = 0; i < MainWindow.diQuStrings.Count; i++)
						{
							if (clickedItem != this.contextMenuDiQu.Items[i + 1])
							{
								if (this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[i]] != null)
								{
									this.contextMenuDiQu.Items[i + 1].Text = this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[i]].ToString();
								}
							}
							else
							{
								if (MainWindow.diQuStrings[i] != null && this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[i]] != null)
								{
									this.contextMenuDiQu.Items[i + 1].Text = this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[i]].ToString() + " √";
								}
								MainWindow.selectIndexDQ = i;
							}
						}
					}
					else
					{
						if (MainWindow.selectIndexDQ != -1 && this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[MainWindow.selectIndexDQ]] != null)
						{
							this.contextMenuDiQu.Items[MainWindow.selectIndexDQ + 1].Text = this.mainForm.CurHQClient.m_htRegion[MainWindow.diQuStrings[MainWindow.selectIndexDQ]].ToString();
						}
						MainWindow.selectIndexDQ = -1;
					}
					this.selectCurQuoteList();
				}
				this.multiQuoteData.iStart = 0;
				this.multiQuoteData.yChange = 0;
				this.buttonUtils.CurButtonName = "Select";
				foreach (MyButton myButton in this.buttonUtils.ButtonList)
				{
					if (myButton.Selected)
					{
						myButton.Selected = false;
					}
				}
				if (this.buttonUtils.ButtonList.Count > 0)
				{
					MyButton myButton2 = (MyButton)this.buttonUtils.ButtonList[0];
					myButton2.Selected = true;
				}
				this.multiQuoteData.MultiQuotePage = 0;
				this.mainForm.UserCommand("60");
				this.mainForm.Repaint();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "contextMenuDiQu_ItemClicked异常：" + ex.Message);
			}
		}
		private void contextMenuHY_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			try
			{
				ToolStripItem clickedItem = e.ClickedItem;
				if (clickedItem != null)
				{
					if (clickedItem.Text != "全部")
					{
						for (int i = 0; i < MainWindow.hangYeStrings.Count; i++)
						{
							if (clickedItem != this.contextMenuHY.Items[i + 1])
							{
								if (this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[i]] != null)
								{
									this.contextMenuHY.Items[i + 1].Text = this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[i]].ToString();
								}
							}
							else
							{
								if (MainWindow.hangYeStrings[i] != null && this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[i]] != null)
								{
									this.contextMenuHY.Items[i + 1].Text = this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[i]].ToString() + " √";
								}
								MainWindow.selectIndexHY = i;
							}
						}
					}
					else
					{
						if (MainWindow.selectIndexHY != -1 && this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[MainWindow.selectIndexHY]] != null)
						{
							this.contextMenuHY.Items[MainWindow.selectIndexHY + 1].Text = this.mainForm.CurHQClient.m_htIndustry[MainWindow.hangYeStrings[MainWindow.selectIndexHY]].ToString();
						}
						MainWindow.selectIndexHY = -1;
					}
					this.selectCurQuoteList();
				}
				this.multiQuoteData.iStart = 0;
				this.multiQuoteData.yChange = 0;
				this.buttonUtils.CurButtonName = "Select";
				foreach (MyButton myButton in this.buttonUtils.ButtonList)
				{
					if (myButton.Selected)
					{
						myButton.Selected = false;
					}
				}
				if (this.buttonUtils.ButtonList.Count > 0)
				{
					MyButton myButton2 = (MyButton)this.buttonUtils.ButtonList[0];
					myButton2.Selected = true;
				}
				this.multiQuoteData.MultiQuotePage = 0;
				this.mainForm.UserCommand("60");
				this.mainForm.Repaint();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "contextMenuHY_ItemClicked异常：" + ex.Message);
			}
		}
		private void selectCurQuoteList()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.mainForm.CurHQClient.m_quoteList.Length; i++)
			{
				ProductDataVO productDataVO = this.mainForm.CurHQClient.m_quoteList[i];
				if (MainWindow.selectIndexHY != -1 && MainWindow.selectIndexDQ != -1)
				{
					if (MainWindow.hangYeStrings[MainWindow.selectIndexHY] == productDataVO.industry && MainWindow.diQuStrings[MainWindow.selectIndexDQ] == productDataVO.region)
					{
						arrayList.Add(productDataVO);
					}
				}
				else if (MainWindow.selectIndexHY == -1 && MainWindow.selectIndexDQ == -1)
				{
					arrayList.Add(productDataVO);
				}
				else if (MainWindow.selectIndexHY == -1)
				{
					if (MainWindow.diQuStrings[MainWindow.selectIndexDQ] == productDataVO.region)
					{
						arrayList.Add(productDataVO);
					}
				}
				else if (MainWindow.selectIndexDQ == -1 && MainWindow.hangYeStrings[MainWindow.selectIndexHY] == productDataVO.industry)
				{
					arrayList.Add(productDataVO);
				}
			}
			this.multiQuoteData.m_curQuoteList = (ProductDataVO[])arrayList.ToArray(typeof(ProductDataVO));
		}
		private void MainWindow_KeyPress(object sender, KeyPressEventArgs e)
		{
			this.mainForm.HQMainForm_KeyPress(this.mainForm, e);
		}
		private void iniSizeAndStyle()
		{
			this.mainForm.Height = this.panelHQ.Height - 1;
			this.mainForm.Width = this.panelHQ.Width - 1;
			this.setLeftBtBackcolor(this.btRanking);
			this.topPanel.BackColor = Color.FromArgb(37, 37, 37);
			foreach (object current in this.topPanel.Controls)
			{
				if (current is Button)
				{
					Button button = current as Button;
					button.BackColor = this.topBtColor;
					button.MouseEnter += new EventHandler(this.button_MouseEnter);
					button.MouseLeave += new EventHandler(this.button_MouseLeave);
				}
			}
			foreach (object current2 in this.imagePanel.Controls)
			{
				if (current2 is PictureBox)
				{
					PictureBox pictureBox = current2 as PictureBox;
					pictureBox.MouseEnter += new EventHandler(this.pic_MouseEnter);
					pictureBox.MouseLeave += new EventHandler(this.pic_MouseLeave);
				}
			}
		}
		private void button_MouseLeave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackColor = this.topBtColor;
		}
		private void button_MouseEnter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackColor = this.topBtMouseColor;
		}
		private void setLeftBtBackcolor(Button clickBt)
		{
			Image lbtn = Resources.lbtn3;
			this.btRanking.BackgroundImage = lbtn;
			this.btMultiRanking.BackgroundImage = lbtn;
			this.btBill.BackgroundImage = lbtn;
			this.btLine.BackgroundImage = lbtn;
			if (clickBt != null)
			{
				clickBt.BackgroundImage = Resources.lbtn;
			}
		}
		public void changeBtColor()
		{
			switch (this.mainForm.CurHQClient.CurrentPage)
			{
			case 0:
				this.setLeftBtBackcolor(this.btRanking);
				return;
			case 1:
				this.setLeftBtBackcolor(this.btLine);
				return;
			case 2:
				this.setLeftBtBackcolor(this.btLine);
				return;
			case 4:
				this.setLeftBtBackcolor(this.btBill);
				return;
			case 5:
				this.setLeftBtBackcolor(this.btMultiRanking);
				return;
			}
			this.setLeftBtBackcolor(null);
		}
		public void CloseMainForm()
		{
			try
			{
				if (this.mainForm.CurHQClient.CurrentPage != 0)
				{
					this.setInfo.lastSave();
				}
				this.setInfo.saveSetInfo("iStyle", this.mainForm.iStyle.ToString());
				if (this.mainForm.CurHQClient != null)
				{
					this.mainForm.CurHQClient.Dispose();
				}
				if (this.mainForm.MainGraph != null)
				{
					this.mainForm.MainGraph.PageDispose();
				}
				this.buttonUtils.ButtonList.Clear();
				this.buttonUtils.RightButtonList.Clear();
				this.buttonUtils.isTidyBtnFlag = 0;
				SelectServer.GetInstance().Close();
				this.mainForm.Dispose();
				this.mainForm.Close();
				base.Dispose();
				base.Close();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "CloseMainForm异常：" + ex.Message);
			}
		}
		private void SetControlText()
		{
			this.btRanking.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btRanking");
			this.btMultiRanking.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btMultiRanking");
			this.btLine.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btLine");
			this.btBill.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btBill");
			base.Icon = (Icon)this.pluginInfo.HQResourceManager.GetObject("Logo.ico");
			this.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_title");
			this.btHangYe.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btHangYe");
			this.btDiQu.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btDiQu");
			this.btNews.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btNews");
			this.btOwnGoods.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btOwnGoods");
			this.btSysMessage.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_btSysMessage");
			this.labelSet.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_labelSet");
			this.lbAbout.Text = this.pluginInfo.HQResourceManager.GetString("VHQStr_lbAbout");
			if (!Tools.StrToBool(this.pluginInfo.HTConfig["LeftButton"].ToString(), false))
			{
				this.panelLeftBtn.Visible = false;
				this.panelHQ.Left -= this.panelLeftBtn.Width - 5;
				this.panelHQ.Width += this.panelLeftBtn.Width - 5;
			}
			this.bUpdate = Tools.StrToBool((string)this.pluginInfo.HTConfig["AutoUpdate"], false);
		}
		private void pic_MouseEnter(object sender, EventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;
			try
			{
				if (!Tools.StrToBool(pictureBox.Tag.ToString(), false))
				{
					pictureBox.BackgroundImage = this.backgroundImage;
				}
			}
			catch (Exception)
			{
			}
		}
		private void pic_MouseLeave(object sender, EventArgs e)
		{
			PictureBox pictureBox = sender as PictureBox;
			this.toolTipTopImage.Hide(pictureBox);
			if (!Tools.StrToBool(pictureBox.Tag.ToString(), false))
			{
				pictureBox.BackgroundImage = this.backgroundImageleave;
			}
		}
		public void ChangeKLineBtnColor()
		{
			foreach (Control control in this.imagePanel.Controls)
			{
				if (control is PictureBox)
				{
					if (this.mainForm.CurHQClient.CurrentPage == 2)
					{
						if (control.Name == "StartButton" || control.Name == "BJPMBtn" || control.Name == "FSZSBtn" || control.Name == "KLineBtn")
						{
							control.BackgroundImage = this.backgroundImageleave;
						}
						else
						{
							control.BackgroundImage = this.backgroundImageleave;
							if (this.mainForm.CurHQClient.m_iKLineCycle == 1 && control.Name == "Day")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 13 && control.Name == "AnyDay")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 14 && control.Name == "AnyMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 11 && control.Name == "TwoHr")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 12 && control.Name == "FourHr")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 5 && control.Name == "OneMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 8 && control.Name == "FifteenMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 6 && control.Name == "ThreeMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 9 && control.Name == "ThirtyMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 7 && control.Name == "FiveMin")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 10 && control.Name == "Hour")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 3 && control.Name == "Month")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 4 && control.Name == "Quarter")
							{
								control.BackgroundImage = this.backgroundImage;
							}
							if (this.mainForm.CurHQClient.m_iKLineCycle == 2 && control.Name == "Week")
							{
								control.BackgroundImage = this.backgroundImage;
							}
						}
					}
					else
					{
						if (this.mainForm.CurHQClient.CurrentPage == 0 && control.Name == "StartButton")
						{
							control.BackgroundImage = this.backgroundImage;
						}
						if (this.mainForm.CurHQClient.CurrentPage == 0 && control.Name == "BJPMBtn")
						{
							control.BackgroundImage = this.backgroundImage;
						}
						else if (this.mainForm.CurHQClient.CurrentPage == 1 && control.Name == "FSZSBtn")
						{
							control.BackgroundImage = this.backgroundImage;
						}
						else if (this.mainForm.CurHQClient.CurrentPage == 4 && control.Name == "Bill")
						{
							control.BackgroundImage = this.backgroundImage;
						}
						else
						{
							control.BackgroundImage = this.backgroundImageleave;
						}
					}
				}
			}
		}
		private void image_Click(object sender, EventArgs e)
		{
			try
			{
				if (sender is PictureBox)
				{
					this.ChangeBtnBackGround((PictureBox)sender);
				}
				bool flag = true;
				this.mainForm.IsMultiCycle = false;
				PictureBox pictureBox = sender as PictureBox;
				if (pictureBox.Name == "Day")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 1;
					flag = true;
				}
				else if (pictureBox.Name == "Week")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 2;
					flag = true;
				}
				else if (pictureBox.Name == "Month")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 3;
					flag = true;
				}
				else if (pictureBox.Name == "Quarter")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 4;
					flag = true;
				}
				else if (pictureBox.Name == "AnyDay")
				{
					InputWindow inputWindow = new InputWindow(1);
					inputWindow.ShowDialog();
					if (inputWindow.DialogResult == DialogResult.Yes)
					{
						this.mainForm.CurHQClient.m_iKLineCycle = 13;
						this.mainForm.CurHQClient.KLineValue = inputWindow.KValue;
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else if (pictureBox.Name == "OneMin")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 5;
					flag = true;
				}
				else if (pictureBox.Name == "ThreeMin")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 6;
					flag = true;
				}
				else if (pictureBox.Name == "FiveMin")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 7;
					flag = true;
				}
				else if (pictureBox.Name == "FifteenMin")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 8;
					flag = true;
				}
				else if (pictureBox.Name == "ThirtyMin")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 9;
					flag = true;
				}
				else if (pictureBox.Name == "Hour")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 10;
					flag = true;
				}
				else if (pictureBox.Name == "TwoHr")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 11;
					flag = true;
				}
				else if (pictureBox.Name == "FourHr")
				{
					this.mainForm.CurHQClient.m_iKLineCycle = 12;
					flag = true;
				}
				else if (pictureBox.Name == "AnyMin")
				{
					InputWindow inputWindow2 = new InputWindow(2);
					inputWindow2.ShowDialog();
					if (inputWindow2.DialogResult == DialogResult.Yes)
					{
						this.mainForm.CurHQClient.m_iKLineCycle = 14;
						this.mainForm.CurHQClient.KLineValue = inputWindow2.KValue;
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					if (this.mainForm.CurHQClient.CurrentPage != 1)
					{
						this.mainForm.CurHQClient.CurrentPage = 1;
					}
					this.mainForm.UserCommand("05");
					this.mainForm.CurHQClient.globalData.PrePage = 2;
					this.mainForm.Repaint();
				}
				this.mainForm.Focus();
				CommodityInfoF.CommodityInfoClose();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "image_Click异常：" + ex.Message);
			}
		}
		private void ChangeBtnBackGround(PictureBox pic)
		{
			foreach (Control control in this.imagePanel.Controls)
			{
				if (control is PictureBox)
				{
					if (((PictureBox)control).Equals(pic))
					{
						control.Tag = true;
					}
					else
					{
						control.Tag = false;
					}
				}
			}
		}
		private void btRanking_Click(object sender, EventArgs e)
		{
			try
			{
				if (sender is PictureBox)
				{
					this.ChangeBtnBackGround((PictureBox)sender);
				}
				if ((this.buttonUtils.CurButtonName == "MyCommodity" || this.buttonUtils.CurButtonName == "Select") && this.mainForm.CurHQClient.CurrentPage == 0)
				{
					this.multiQuoteData.iStart = 0;
					this.multiQuoteData.yChange = 0;
					this.multiQuoteData.MultiQuotePage = 0;
					this.buttonUtils.CurButtonName = "AllMarket";
					if (this.buttonUtils.ButtonList.Count > 0)
					{
						MyButton myButton = (MyButton)this.buttonUtils.ButtonList[0];
						myButton.Selected = true;
						foreach (MyButton myButton2 in this.buttonUtils.ButtonList)
						{
							if (myButton2 != myButton && myButton2.Selected)
							{
								myButton2.Selected = false;
							}
						}
					}
				}
				this.mainForm.UserCommand("60");
				this.mainForm.Repaint();
				this.mainForm.Focus();
				CommodityInfoF.CommodityInfoClose();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "btRanking_Click异常：" + ex.Message);
			}
		}
		private void btMultiRanking_Click(object sender, EventArgs e)
		{
			this.mainForm.UserCommand("80");
			this.mainForm.Repaint();
			this.mainForm.Focus();
		}
		private void btLine_Click(object sender, EventArgs e)
		{
			if (this.mainForm.CurHQClient.CurrentPage != 1 && this.mainForm.CurHQClient.CurrentPage != 2)
			{
				this.mainForm.UserCommand("05");
			}
			this.mainForm.Repaint();
			this.mainForm.Focus();
		}
		private void btBill_Click(object sender, EventArgs e)
		{
			this.mainForm.UserCommand("01");
			this.mainForm.Repaint();
			this.mainForm.Focus();
		}
		private void FSZSBtn_Click(object sender, EventArgs e)
		{
			if (sender is PictureBox)
			{
				this.ChangeBtnBackGround((PictureBox)sender);
			}
			if (this.mainForm.CurHQClient.curCommodityInfo == null)
			{
				return;
			}
			this.mainForm.CurHQClient.CurrentPage = 2;
			this.mainForm.UserCommand("05");
			this.mainForm.Repaint();
			this.mainForm.Focus();
			CommodityInfoF.CommodityInfoClose();
		}
		private void KLineBtn_Click(object sender, EventArgs e)
		{
			if (sender is PictureBox)
			{
				this.ChangeBtnBackGround((PictureBox)sender);
			}
			if (this.mainForm.CurHQClient.curCommodityInfo == null)
			{
				return;
			}
			this.mainForm.CurHQClient.CurrentPage = 1;
			this.mainForm.UserCommand("05");
			this.mainForm.Repaint();
			this.mainForm.Focus();
			CommodityInfoF.CommodityInfoClose();
		}
		private void btNews_Click(object sender, EventArgs e)
		{
			Process.Start("http://www.gnnt.com.cn/");
			this.mainForm.Focus();
		}
		private void lbAbout_Click(object sender, EventArgs e)
		{
			if (this.about == null || this.about.IsDisposed)
			{
				this.about = new About(this);
				this.about.Show();
			}
			else
			{
				this.about.TopMost = true;
			}
			this.mainForm.Focus();
		}
		private void btSysMessage_Click(object sender, EventArgs e)
		{
			if (this.sys == null || this.sys.IsDisposed)
			{
				this.sys = new SysMessage(this);
			}
			this.sys.Show();
			this.sys.TopMost = true;
		}
		private void panelHQ_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				this.mainForm.Size = this.panelHQ.Size;
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "panelHQ_SizeChanged窗体出错：" + ex.Message + ex.StackTrace);
			}
		}
		private void btOwnGoods_Click(object sender, EventArgs e)
		{
			try
			{
				this.buttonUtils.CurButtonName = "MyCommodity";
				if (this.buttonUtils.ButtonList.Count > 0)
				{
					MyButton myButton = (MyButton)this.buttonUtils.ButtonList[this.buttonUtils.ButtonList.Count - 1];
					myButton.Selected = true;
				}
				this.multiQuoteData.MultiQuotePage = 1;
				this.mainForm.UserCommand("60");
				this.mainForm.Repaint();
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "btOwnGoods_Click异常：" + ex.Message);
			}
		}
		private void timerStatusTime_Tick(object sender, EventArgs e)
		{
		}
		private void setStatusAndTime()
		{
			try
			{
				if (this.mainForm == null)
				{
					Logger.wirte(1, "setStatusAndTime:mainForm == null");
				}
				else if (this.mainForm.CurHQClient == null)
				{
					Logger.wirte(1, "setStatusAndTime:mainForm.CurHQClient == null");
				}
				else
				{
					string text = TradeTimeVO.HHMMIntToString(this.mainForm.CurHQClient.m_iTime / 100);
					if (text.EndsWith(":"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					string text2 = this.mainForm.CurHQClient.m_iDate.ToString("####-##-##") + " " + text;
					this.lbTime.Text = text2;
					if (this.mainForm.CurHQClient.Connected)
					{
						this.lbConnect.ForeColor = Color.LimeGreen;
						this.lbConnect.Text = "连接正常";
					}
					else
					{
						this.lbConnect.ForeColor = Color.Red;
						this.lbConnect.Text = "连接失败";
					}
				}
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "设置系统时间错误：" + ex.Message + ex.StackTrace);
			}
		}
		private void BackBtn_Click(object sender, EventArgs e)
		{
			int currentPage = this.mainForm.CurHQClient.CurrentPage;
			if (this.mainForm.CurHQClient.oldPage == 1 || this.mainForm.CurHQClient.oldPage == 2)
			{
				if (currentPage != 1 && currentPage != 2)
				{
					this.mainForm.UserCommand("05");
				}
				else
				{
					this.mainForm.OnF5();
				}
			}
			else if (this.mainForm.CurHQClient.oldPage == 0)
			{
				this.mainForm.UserCommand("60");
			}
			else if (this.mainForm.CurHQClient.oldPage == 5)
			{
				this.mainForm.UserCommand("80");
			}
			else if (this.mainForm.CurHQClient.oldPage == 4)
			{
				this.mainForm.UserCommand("01");
			}
			else if (this.mainForm.CurHQClient.oldPage == 6)
			{
				this.mainForm.UserCommand("70");
			}
			if (this.mainForm.CurHQClient.CurrentPage != -1)
			{
				this.mainForm.CurHQClient.oldPage = currentPage;
			}
			this.mainForm.Repaint();
			this.mainForm.Focus();
			CommodityInfoF.CommodityInfoClose();
		}
		private void btNews_MouseLeave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn;
		}
		private void btNews_MouseEnter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn1;
		}
		private void btSysMessage_MouseEnter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn1;
		}
		private void btSysMessage_MouseLeave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn;
		}
		private void btOwnGoods_MouseEnter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn1;
		}
		private void btOwnGoods_MouseLeave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			button.BackgroundImage = Resources.btn;
		}
		private void btHangYe_MouseEnter(object sender, EventArgs e)
		{
			SplitButton splitButton = sender as SplitButton;
			splitButton.BackgroundImage = Resources.cbox1;
		}
		private void btHangYe_MouseLeave(object sender, EventArgs e)
		{
			SplitButton splitButton = sender as SplitButton;
			splitButton.BackgroundImage = Resources.cbox;
		}
		private void btDiQu_MouseEnter(object sender, EventArgs e)
		{
			SplitButton splitButton = sender as SplitButton;
			splitButton.BackgroundImage = Resources.cbox1;
		}
		private void btDiQu_MouseLeave(object sender, EventArgs e)
		{
			SplitButton splitButton = sender as SplitButton;
			splitButton.BackgroundImage = Resources.cbox;
		}
		private void MainWindow_Shown(object sender, EventArgs e)
		{
			if (Tools.StrToBool(this.pluginInfo.HTConfig["WelcomeInfo"].ToString(), false))
			{
				this.ShowMessage();
			}
		}
		private void labelSet_Click(object sender, EventArgs e)
		{
			try
			{
				ServerSet serverSet = new ServerSet(this);
				if (DialogResult.OK == serverSet.ShowDialog())
				{
					Process.Start(Assembly.GetExecutingAssembly().Location);
					base.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "labelSet_Click异常：" + ex.Message);
			}
		}
		private void setTimeMarket_Click(object sender, EventArgs e)
		{
			try
			{
				if (Tools.StrToBool(this.pluginInfo.HTConfig["MultiMarket"].ToString(), false))
				{
					Label label = sender as Label;
					string arg_34_0 = label.Text;
					MarketForm marketForm = label.Parent.Parent as MarketForm;
					string text = label.Tag.ToString();
					this.setInfo.saveSetInfo("CurTimeMarketId", text);
					this.setInfo.CurTimeMarketId = text;
					marketForm.Close();
					MarketDataVO marketDataVO = (MarketDataVO)this.mainForm.CurHQClient.m_htMarketData[text];
					this.mainForm.CurHQClient.m_iTime = marketDataVO.time;
					this.mainForm.CurHQClient.m_iDate = marketDataVO.date;
				}
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "setTimeMarket_Click异常：" + ex.Message);
			}
		}
		private void lbTime_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (Tools.StrToBool(this.pluginInfo.HTConfig["MultiMarket"].ToString(), false))
				{
					if (this.mainForm.CurHQClient.m_htMarketData.Count != 0)
					{
						MarketForm marketForm = new MarketForm();
						marketForm.Text = "选择对应的市场为当前系统时间";
						int num = 0;
						foreach (DictionaryEntry dictionaryEntry in this.mainForm.CurHQClient.m_htMarketData)
						{
							MarketDataVO marketDataVO = (MarketDataVO)dictionaryEntry.Value;
							Label label = new Label
							{
								Tag = marketDataVO.marketID,
								Parent = marketForm.MainPanel,
								ForeColor = Color.White,
								Location = new Point(20, this.lbLocationY + num * this.lbHeight),
								Font = new Font("宋体", 12f, (this.setInfo.CurTimeMarketId == marketDataVO.marketID) ? FontStyle.Regular : FontStyle.Underline),
								TextAlign = ContentAlignment.MiddleRight,
								AutoSize = true,
								Text = marketDataVO.marketName
							};
							if (this.setInfo.CurTimeMarketId != marketDataVO.marketID)
							{
								label.Cursor = Cursors.Hand;
								label.Click += new EventHandler(this.setTimeMarket_Click);
							}
							num++;
						}
						marketForm.labelId.Visible = false;
						marketForm.labelName.Left = 20;
						marketForm.ShowDialog();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.wirte(3, "lbTime_DoubleClick异常：" + ex.Message);
			}
		}
		private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.CloseMainForm();
		}
		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (this.mainForm.CurHQClient.CurrentPage != 0)
			{
				this.setInfo.lastSave();
			}
		}
		private void refreshBt_Click(object sender, EventArgs e)
		{
			this.mainForm.UserCommand("refreshBt");
			this.mainForm.Repaint();
			this.mainForm.Focus();
			CommodityInfoF.CommodityInfoClose();
		}
		private void pictureBoxBill_Click(object sender, EventArgs e)
		{
			if (sender is PictureBox)
			{
				this.ChangeBtnBackGround((PictureBox)sender);
			}
			if (this.mainForm.CurHQClient.curCommodityInfo == null)
			{
				return;
			}
			this.mainForm.CurHQClient.CurrentPage = 4;
			this.mainForm.UserCommand("01");
			this.mainForm.Repaint();
			this.mainForm.Focus();
			CommodityInfoF.CommodityInfoClose();
		}
		private void pictureUp_Click(object sender, EventArgs e)
		{
			this.KLineUpDown(1);
		}
		private void pictureDown_Click(object sender, EventArgs e)
		{
			this.KLineUpDown(2);
		}
		private void KLineUpDown(int udFlag)
		{
			if (this.mainForm.CurHQClient.kLineUpDown != null && this.mainForm.CurHQClient.CurrentPage == 2)
			{
				this.mainForm.CurHQClient.kLineUpDown(udFlag);
				this.mainForm.Repaint();
			}
		}
		private void pictureF10_Click(object sender, EventArgs e)
		{
			CommodityInfo curCommodityInfo = this.mainForm.CurHQClient.curCommodityInfo;
			if (curCommodityInfo != null)
			{
				string commodityCode = curCommodityInfo.commodityCode;
				this.mainForm.CurHQClient.m_hqForm.DisplayCommodityInfo(commodityCode);
			}
		}
		private void pictureSet_Click(object sender, EventArgs e)
		{
			UserSet userSet = new UserSet(this.mainForm.CurHQClient.m_hqForm);
			userSet.ShowDialog();
		}
		private void Indecator_Click(object sender, EventArgs e)
		{
			int length = IndicatorBase.INDICATOR_NAME.GetLength(0);
			ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
			for (int i = 0; i < length; i++)
			{
				string text = IndicatorBase.INDICATOR_NAME[i, 0];
				string @string = this.pluginInfo.HQResourceManager.GetString("HQStr_T_" + IndicatorBase.INDICATOR_NAME[i, 0]);
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text + "  " + @string);
				toolStripMenuItem.Name = "Indicator_" + text;
				contextMenuStrip.Items.Add(toolStripMenuItem);
			}
			contextMenuStrip.Show(this, this.Indecator.Location.X, this.Indecator.Location.Y + this.Indecator.Height);
			contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenu_ItemClicked);
		}
		private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			string name = e.ClickedItem.Name;
			this.mainForm.CurHQClient.m_strIndicator = name.Substring(10);
			this.setInfo.StrIndicator = name.Substring(10);
			this.setInfo.saveSetInfo("StrIndicator", name.Substring(10));
			this.mainForm.CurHQClient.createIndicator();
			this.mainForm.Repaint();
		}
		private void SetPictureEnable(bool enable)
		{
			this.setEnable = new MainWindow.SetPictureEnableCallBack(this.SetPictureInfo);
			base.Invoke(this.setEnable, new object[]
			{
				enable
			});
		}
		private void SetPictureInfo(bool enable)
		{
			this.Indecator.Visible = enable;
			this.pictureUp.Visible = enable;
			this.pictureDown.Visible = enable;
			string text = (string)this.pluginInfo.HTConfig["CommodityInfoAddress"];
			if (text != null && text.Length > 0)
			{
				this.pictureF10.Visible = true;
			}
			else
			{
				this.pictureF10.Visible = false;
			}
			if (enable)
			{
				if (this.pictureF10.Visible)
				{
					this.pictureF10.Location = new Point(855, 0);
					this.pictureSet.Location = new Point(890, 0);
					return;
				}
				this.pictureSet.Location = new Point(855, 0);
				return;
			}
			else
			{
				if (this.pictureF10.Visible)
				{
					this.pictureF10.Location = new Point(750, 0);
					this.pictureSet.Location = new Point(785, 0);
					return;
				}
				this.pictureSet.Location = new Point(750, 0);
				return;
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainWindow));
			this.contextMenuDiQu = new ContextMenuStrip(this.components);
			this.contextMenuHY = new ContextMenuStrip(this.components);
			this.toolTipTopImage = new ToolTip(this.components);
			this.lbTime = new Label();
			this.panelHQ = new Panel();
			this.pictureBoxConnect = new PictureBox();
			this.lbConnect = new Label();
			this.topPanel = new Panel();
			this.labelSet = new Label();
			this.lbLogin = new Label();
			this.btDiQu = new SplitButton();
			this.btHangYe = new SplitButton();
			this.btSysMessage = new Button();
			this.lbAbout = new Label();
			this.label1 = new Label();
			this.btNews = new Button();
			this.btOwnGoods = new Button();
			this.panelLeftBtn = new Panel();
			this.btRanking = new Button();
			this.btMultiRanking = new Button();
			this.btLine = new Button();
			this.btBill = new Button();
			this.panelMain = new Panel();
			this.imagePanel = new Panel();
			this.Split = new PictureBox();
			this.pictureSet = new PictureBox();
			this.pictureF10 = new PictureBox();
			this.pictureDown = new PictureBox();
			this.pictureUp = new PictureBox();
			this.Indecator = new PictureBox();
			this.pictureBoxBill = new PictureBox();
			this.refreshBt = new PictureBox();
			this.AnyMin = new PictureBox();
			this.BackBtn = new PictureBox();
			this.AnyDay = new PictureBox();
			this.FifteenMin = new PictureBox();
			this.KLineBtn = new PictureBox();
			this.FourHr = new PictureBox();
			this.Day = new PictureBox();
			this.SearchBtn = new PictureBox();
			this.FiveMin = new PictureBox();
			this.Quarter = new PictureBox();
			this.ThirtyMin = new PictureBox();
			this.StartButton = new PictureBox();
			this.FSZSBtn = new PictureBox();
			this.TwoHr = new PictureBox();
			this.Week = new PictureBox();
			this.OneMin = new PictureBox();
			this.ThreeMin = new PictureBox();
			this.Hour = new PictureBox();
			this.Month = new PictureBox();
			this.BJPMBtn = new PictureBox();
			((ISupportInitialize)this.pictureBoxConnect).BeginInit();
			this.topPanel.SuspendLayout();
			this.panelLeftBtn.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.imagePanel.SuspendLayout();
			((ISupportInitialize)this.Split).BeginInit();
			((ISupportInitialize)this.pictureSet).BeginInit();
			((ISupportInitialize)this.pictureF10).BeginInit();
			((ISupportInitialize)this.pictureDown).BeginInit();
			((ISupportInitialize)this.pictureUp).BeginInit();
			((ISupportInitialize)this.Indecator).BeginInit();
			((ISupportInitialize)this.pictureBoxBill).BeginInit();
			((ISupportInitialize)this.refreshBt).BeginInit();
			((ISupportInitialize)this.AnyMin).BeginInit();
			((ISupportInitialize)this.BackBtn).BeginInit();
			((ISupportInitialize)this.AnyDay).BeginInit();
			((ISupportInitialize)this.FifteenMin).BeginInit();
			((ISupportInitialize)this.KLineBtn).BeginInit();
			((ISupportInitialize)this.FourHr).BeginInit();
			((ISupportInitialize)this.Day).BeginInit();
			((ISupportInitialize)this.SearchBtn).BeginInit();
			((ISupportInitialize)this.FiveMin).BeginInit();
			((ISupportInitialize)this.Quarter).BeginInit();
			((ISupportInitialize)this.ThirtyMin).BeginInit();
			((ISupportInitialize)this.StartButton).BeginInit();
			((ISupportInitialize)this.FSZSBtn).BeginInit();
			((ISupportInitialize)this.TwoHr).BeginInit();
			((ISupportInitialize)this.Week).BeginInit();
			((ISupportInitialize)this.OneMin).BeginInit();
			((ISupportInitialize)this.ThreeMin).BeginInit();
			((ISupportInitialize)this.Hour).BeginInit();
			((ISupportInitialize)this.Month).BeginInit();
			((ISupportInitialize)this.BJPMBtn).BeginInit();
			base.SuspendLayout();
			this.contextMenuDiQu.Name = "contextMenuHY";
			this.contextMenuDiQu.ShowImageMargin = false;
			this.contextMenuDiQu.Size = new Size(36, 4);
			this.contextMenuHY.Name = "contextMenuHY";
			this.contextMenuHY.ShowImageMargin = false;
			this.contextMenuHY.Size = new Size(36, 4);
			this.lbTime.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.lbTime.AutoSize = true;
			this.lbTime.BackColor = Color.Transparent;
			this.lbTime.Cursor = Cursors.Hand;
			this.lbTime.ForeColor = Color.LimeGreen;
			this.lbTime.Location = new Point(866, 519);
			this.lbTime.Name = "lbTime";
			this.lbTime.Size = new Size(101, 12);
			this.lbTime.TabIndex = 0;
			this.lbTime.Text = "2013-01-06 18:15";
			this.toolTipTopImage.SetToolTip(this.lbTime, "双击设置对应市场的时间为当前系统时间");
			this.lbTime.Visible = false;
			this.lbTime.DoubleClick += new EventHandler(this.lbTime_DoubleClick);
			this.panelHQ.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panelHQ.BackColor = Color.Black;
			this.panelHQ.Location = new Point(33, 66);
			this.panelHQ.Margin = new Padding(0);
			this.panelHQ.Name = "panelHQ";
			this.panelHQ.Size = new Size(950, 470);
			this.panelHQ.TabIndex = 15;
			this.panelHQ.SizeChanged += new EventHandler(this.panelHQ_SizeChanged);
			this.pictureBoxConnect.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.pictureBoxConnect.BackColor = Color.Transparent;
			this.pictureBoxConnect.Location = new Point(780, 518);
			this.pictureBoxConnect.Name = "pictureBoxConnect";
			this.pictureBoxConnect.Size = new Size(22, 20);
			this.pictureBoxConnect.TabIndex = 28;
			this.pictureBoxConnect.TabStop = false;
			this.pictureBoxConnect.Visible = false;
			this.lbConnect.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.lbConnect.AutoSize = true;
			this.lbConnect.BackColor = Color.Transparent;
			this.lbConnect.ForeColor = Color.LimeGreen;
			this.lbConnect.Location = new Point(809, 519);
			this.lbConnect.Name = "lbConnect";
			this.lbConnect.Size = new Size(53, 12);
			this.lbConnect.TabIndex = 29;
			this.lbConnect.Text = "连接正常";
			this.lbConnect.Visible = false;
			this.topPanel.BackColor = Color.Black;
			this.topPanel.Controls.Add(this.labelSet);
			this.topPanel.Controls.Add(this.lbLogin);
			this.topPanel.Controls.Add(this.btDiQu);
			this.topPanel.Controls.Add(this.btHangYe);
			this.topPanel.Controls.Add(this.btSysMessage);
			this.topPanel.Controls.Add(this.lbAbout);
			this.topPanel.Controls.Add(this.label1);
			this.topPanel.Controls.Add(this.btNews);
			this.topPanel.Controls.Add(this.btOwnGoods);
			this.topPanel.Dock = DockStyle.Top;
			this.topPanel.Location = new Point(0, 0);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new Size(989, 29);
			this.topPanel.TabIndex = 26;
			this.labelSet.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.labelSet.AutoSize = true;
			this.labelSet.BackColor = SystemColors.Control;
			this.labelSet.ForeColor = Color.Black;
			this.labelSet.Location = new Point(844, 8);
			this.labelSet.Name = "labelSet";
			this.labelSet.Size = new Size(53, 12);
			this.labelSet.TabIndex = 27;
			this.labelSet.Text = "网络设置";
			this.labelSet.Click += new EventHandler(this.labelSet_Click);
			this.lbLogin.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.lbLogin.AutoSize = true;
			this.lbLogin.BackColor = SystemColors.Control;
			this.lbLogin.ForeColor = Color.Black;
			this.lbLogin.Location = new Point(769, 8);
			this.lbLogin.Name = "lbLogin";
			this.lbLogin.Size = new Size(29, 12);
			this.lbLogin.TabIndex = 19;
			this.lbLogin.Text = "登录";
			this.lbLogin.Visible = false;
			this.btDiQu.BackColor = Color.Transparent;
			this.btDiQu.BackgroundImage = (Image)componentResourceManager.GetObject("btDiQu.BackgroundImage");
			this.btDiQu.BackgroundImageLayout = ImageLayout.Stretch;
			this.btDiQu.ClickedImage = "Clicked";
			this.btDiQu.ContextMenuStrip = this.contextMenuDiQu;
			this.btDiQu.DisabledImage = "Disabled";
			this.btDiQu.FlatStyle = FlatStyle.Popup;
			this.btDiQu.FocusedImage = "Focused";
			this.btDiQu.HoverImage = "Hover";
			this.btDiQu.ImageAlign = ContentAlignment.BottomRight;
			this.btDiQu.ImageKey = "Normal";
			this.btDiQu.Location = new Point(131, 3);
			this.btDiQu.Name = "btDiQu";
			this.btDiQu.NormalImage = "Normal";
			this.btDiQu.Size = new Size(95, 23);
			this.btDiQu.TabIndex = 25;
			this.btDiQu.Text = "  地区板块";
			this.btDiQu.TextAlign = ContentAlignment.MiddleLeft;
			this.btDiQu.UseVisualStyleBackColor = false;
			this.btDiQu.MouseEnter += new EventHandler(this.btDiQu_MouseEnter);
			this.btDiQu.MouseLeave += new EventHandler(this.btDiQu_MouseLeave);
			this.btHangYe.BackColor = Color.Transparent;
			this.btHangYe.BackgroundImage = (Image)componentResourceManager.GetObject("btHangYe.BackgroundImage");
			this.btHangYe.BackgroundImageLayout = ImageLayout.Stretch;
			this.btHangYe.ClickedImage = "Clicked";
			this.btHangYe.ContextMenuStrip = this.contextMenuHY;
			this.btHangYe.DisabledImage = "Disabled";
			this.btHangYe.FlatStyle = FlatStyle.Popup;
			this.btHangYe.FocusedImage = "Focused";
			this.btHangYe.HoverImage = "Hover";
			this.btHangYe.ImageAlign = ContentAlignment.BottomRight;
			this.btHangYe.ImageKey = "Normal";
			this.btHangYe.Location = new Point(26, 3);
			this.btHangYe.Name = "btHangYe";
			this.btHangYe.NormalImage = "Normal";
			this.btHangYe.Size = new Size(95, 23);
			this.btHangYe.TabIndex = 24;
			this.btHangYe.Text = "  行业板块";
			this.btHangYe.TextAlign = ContentAlignment.MiddleLeft;
			this.btHangYe.UseVisualStyleBackColor = false;
			this.btHangYe.MouseEnter += new EventHandler(this.btHangYe_MouseEnter);
			this.btHangYe.MouseLeave += new EventHandler(this.btHangYe_MouseLeave);
			this.btSysMessage.BackColor = Color.Transparent;
			this.btSysMessage.BackgroundImage = (Image)componentResourceManager.GetObject("btSysMessage.BackgroundImage");
			this.btSysMessage.BackgroundImageLayout = ImageLayout.Stretch;
			this.btSysMessage.FlatStyle = FlatStyle.Popup;
			this.btSysMessage.Location = new Point(321, 3);
			this.btSysMessage.Name = "btSysMessage";
			this.btSysMessage.Size = new Size(75, 23);
			this.btSysMessage.TabIndex = 23;
			this.btSysMessage.Text = "系统消息";
			this.btSysMessage.UseVisualStyleBackColor = false;
			this.btSysMessage.Click += new EventHandler(this.btSysMessage_Click);
			this.btSysMessage.MouseEnter += new EventHandler(this.btSysMessage_MouseEnter);
			this.btSysMessage.MouseLeave += new EventHandler(this.btSysMessage_MouseLeave);
			this.lbAbout.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.lbAbout.AutoSize = true;
			this.lbAbout.BackColor = SystemColors.Control;
			this.lbAbout.ForeColor = Color.Black;
			this.lbAbout.Location = new Point(914, 8);
			this.lbAbout.Name = "lbAbout";
			this.lbAbout.Size = new Size(29, 12);
			this.lbAbout.TabIndex = 18;
			this.lbAbout.Text = "关于";
			this.lbAbout.Click += new EventHandler(this.lbAbout_Click);
			this.label1.AutoSize = true;
			this.label1.BackColor = SystemColors.Control;
			this.label1.ForeColor = Color.Black;
			this.label1.Location = new Point(739, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(29, 12);
			this.label1.TabIndex = 17;
			this.label1.Text = "预警";
			this.label1.Visible = false;
			this.btNews.BackColor = Color.Transparent;
			this.btNews.BackgroundImage = (Image)componentResourceManager.GetObject("btNews.BackgroundImage");
			this.btNews.BackgroundImageLayout = ImageLayout.Stretch;
			this.btNews.FlatStyle = FlatStyle.Popup;
			this.btNews.Location = new Point(236, 3);
			this.btNews.Name = "btNews";
			this.btNews.Size = new Size(75, 23);
			this.btNews.TabIndex = 22;
			this.btNews.Text = "新闻资讯";
			this.btNews.UseVisualStyleBackColor = false;
			this.btNews.Click += new EventHandler(this.btNews_Click);
			this.btNews.MouseEnter += new EventHandler(this.btNews_MouseEnter);
			this.btNews.MouseLeave += new EventHandler(this.btNews_MouseLeave);
			this.btOwnGoods.BackColor = Color.Transparent;
			this.btOwnGoods.BackgroundImage = (Image)componentResourceManager.GetObject("btOwnGoods.BackgroundImage");
			this.btOwnGoods.BackgroundImageLayout = ImageLayout.Stretch;
			this.btOwnGoods.FlatStyle = FlatStyle.Popup;
			this.btOwnGoods.Location = new Point(405, 3);
			this.btOwnGoods.Name = "btOwnGoods";
			this.btOwnGoods.Size = new Size(75, 23);
			this.btOwnGoods.TabIndex = 15;
			this.btOwnGoods.Text = "自选商品";
			this.btOwnGoods.UseVisualStyleBackColor = false;
			this.btOwnGoods.Click += new EventHandler(this.btOwnGoods_Click);
			this.btOwnGoods.MouseEnter += new EventHandler(this.btOwnGoods_MouseEnter);
			this.btOwnGoods.MouseLeave += new EventHandler(this.btOwnGoods_MouseLeave);
			this.panelLeftBtn.BackColor = Color.Black;
			this.panelLeftBtn.Controls.Add(this.btRanking);
			this.panelLeftBtn.Controls.Add(this.btMultiRanking);
			this.panelLeftBtn.Controls.Add(this.btLine);
			this.panelLeftBtn.Controls.Add(this.btBill);
			this.panelLeftBtn.Dock = DockStyle.Left;
			this.panelLeftBtn.Location = new Point(0, 65);
			this.panelLeftBtn.Name = "panelLeftBtn";
			this.panelLeftBtn.Size = new Size(30, 474);
			this.panelLeftBtn.TabIndex = 0;
			this.btRanking.BackColor = Color.Transparent;
			this.btRanking.BackgroundImage = (Image)componentResourceManager.GetObject("btRanking.BackgroundImage");
			this.btRanking.BackgroundImageLayout = ImageLayout.Stretch;
			this.btRanking.FlatStyle = FlatStyle.Popup;
			this.btRanking.ForeColor = Color.Black;
			this.btRanking.Location = new Point(4, 12);
			this.btRanking.Name = "btRanking";
			this.btRanking.Size = new Size(22, 76);
			this.btRanking.TabIndex = 16;
			this.btRanking.Text = "报价排名";
			this.btRanking.UseVisualStyleBackColor = false;
			this.btRanking.Click += new EventHandler(this.btRanking_Click);
			this.btMultiRanking.BackColor = Color.Transparent;
			this.btMultiRanking.BackgroundImage = (Image)componentResourceManager.GetObject("btMultiRanking.BackgroundImage");
			this.btMultiRanking.BackgroundImageLayout = ImageLayout.Stretch;
			this.btMultiRanking.FlatStyle = FlatStyle.Popup;
			this.btMultiRanking.ForeColor = Color.Black;
			this.btMultiRanking.Location = new Point(4, 92);
			this.btMultiRanking.Name = "btMultiRanking";
			this.btMultiRanking.Size = new Size(22, 76);
			this.btMultiRanking.TabIndex = 17;
			this.btMultiRanking.Text = "综合排名";
			this.btMultiRanking.UseVisualStyleBackColor = false;
			this.btMultiRanking.Click += new EventHandler(this.btMultiRanking_Click);
			this.btLine.BackColor = Color.Transparent;
			this.btLine.BackgroundImage = (Image)componentResourceManager.GetObject("btLine.BackgroundImage");
			this.btLine.BackgroundImageLayout = ImageLayout.Stretch;
			this.btLine.FlatStyle = FlatStyle.Popup;
			this.btLine.ForeColor = Color.Black;
			this.btLine.Location = new Point(4, 172);
			this.btLine.Name = "btLine";
			this.btLine.Size = new Size(22, 76);
			this.btLine.TabIndex = 18;
			this.btLine.Text = "个股分析";
			this.btLine.UseVisualStyleBackColor = false;
			this.btLine.Click += new EventHandler(this.btLine_Click);
			this.btBill.BackColor = Color.Transparent;
			this.btBill.BackgroundImage = (Image)componentResourceManager.GetObject("btBill.BackgroundImage");
			this.btBill.BackgroundImageLayout = ImageLayout.Stretch;
			this.btBill.FlatStyle = FlatStyle.Popup;
			this.btBill.ForeColor = Color.Black;
			this.btBill.Location = new Point(4, 252);
			this.btBill.Name = "btBill";
			this.btBill.Size = new Size(22, 76);
			this.btBill.TabIndex = 19;
			this.btBill.Text = "成交明细";
			this.btBill.UseVisualStyleBackColor = false;
			this.btBill.Click += new EventHandler(this.btBill_Click);
			this.panelMain.BackColor = Color.Black;
			this.panelMain.Controls.Add(this.panelLeftBtn);
			this.panelMain.Controls.Add(this.imagePanel);
			this.panelMain.Controls.Add(this.topPanel);
			this.panelMain.Controls.Add(this.lbConnect);
			this.panelMain.Controls.Add(this.lbTime);
			this.panelMain.Controls.Add(this.pictureBoxConnect);
			this.panelMain.Controls.Add(this.panelHQ);
			this.panelMain.Dock = DockStyle.Fill;
			this.panelMain.Location = new Point(0, 0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new Size(989, 539);
			this.panelMain.TabIndex = 16;
			this.imagePanel.BackColor = Color.FromArgb(27, 27, 27);
			this.imagePanel.Controls.Add(this.Split);
			this.imagePanel.Controls.Add(this.pictureSet);
			this.imagePanel.Controls.Add(this.pictureF10);
			this.imagePanel.Controls.Add(this.pictureDown);
			this.imagePanel.Controls.Add(this.pictureUp);
			this.imagePanel.Controls.Add(this.Indecator);
			this.imagePanel.Controls.Add(this.pictureBoxBill);
			this.imagePanel.Controls.Add(this.refreshBt);
			this.imagePanel.Controls.Add(this.AnyMin);
			this.imagePanel.Controls.Add(this.BackBtn);
			this.imagePanel.Controls.Add(this.AnyDay);
			this.imagePanel.Controls.Add(this.FifteenMin);
			this.imagePanel.Controls.Add(this.KLineBtn);
			this.imagePanel.Controls.Add(this.FourHr);
			this.imagePanel.Controls.Add(this.Day);
			this.imagePanel.Controls.Add(this.SearchBtn);
			this.imagePanel.Controls.Add(this.FiveMin);
			this.imagePanel.Controls.Add(this.Quarter);
			this.imagePanel.Controls.Add(this.ThirtyMin);
			this.imagePanel.Controls.Add(this.StartButton);
			this.imagePanel.Controls.Add(this.FSZSBtn);
			this.imagePanel.Controls.Add(this.TwoHr);
			this.imagePanel.Controls.Add(this.Week);
			this.imagePanel.Controls.Add(this.OneMin);
			this.imagePanel.Controls.Add(this.ThreeMin);
			this.imagePanel.Controls.Add(this.Hour);
			this.imagePanel.Controls.Add(this.Month);
			this.imagePanel.Controls.Add(this.BJPMBtn);
			this.imagePanel.Dock = DockStyle.Top;
			this.imagePanel.Location = new Point(0, 29);
			this.imagePanel.Name = "imagePanel";
			this.imagePanel.Size = new Size(989, 36);
			this.imagePanel.TabIndex = 27;
			this.Split.Image = (Image)componentResourceManager.GetObject("Split.Image");
			this.Split.Location = new Point(429, 10);
			this.Split.Name = "Split";
			this.Split.Size = new Size(2, 19);
			this.Split.TabIndex = 52;
			this.Split.TabStop = false;
			this.Split.Tag = "false";
			this.pictureSet.Location = new Point(890, 0);
			this.pictureSet.Name = "pictureSet";
			this.pictureSet.Size = new Size(35, 35);
			this.pictureSet.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureSet.TabIndex = 51;
			this.pictureSet.TabStop = false;
			this.pictureSet.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.pictureSet, "设置");
			this.pictureSet.Click += new EventHandler(this.pictureSet_Click);
			this.pictureF10.Location = new Point(855, 0);
			this.pictureF10.Name = "pictureF10";
			this.pictureF10.Size = new Size(35, 35);
			this.pictureF10.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureF10.TabIndex = 50;
			this.pictureF10.TabStop = false;
			this.pictureF10.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.pictureF10, "商品详情");
			this.pictureF10.Click += new EventHandler(this.pictureF10_Click);
			this.pictureDown.Location = new Point(820, 0);
			this.pictureDown.Name = "pictureDown";
			this.pictureDown.Size = new Size(35, 35);
			this.pictureDown.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureDown.TabIndex = 49;
			this.pictureDown.TabStop = false;
			this.pictureDown.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.pictureDown, "缩小");
			this.pictureDown.Click += new EventHandler(this.pictureDown_Click);
			this.pictureUp.Location = new Point(785, 0);
			this.pictureUp.Name = "pictureUp";
			this.pictureUp.Size = new Size(35, 35);
			this.pictureUp.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureUp.TabIndex = 48;
			this.pictureUp.TabStop = false;
			this.pictureUp.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.pictureUp, "放大");
			this.pictureUp.Click += new EventHandler(this.pictureUp_Click);
			this.Indecator.Location = new Point(750, 0);
			this.Indecator.Name = "Indecator";
			this.Indecator.Size = new Size(35, 35);
			this.Indecator.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Indecator.TabIndex = 47;
			this.Indecator.TabStop = false;
			this.Indecator.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Indecator, "技术指标");
			this.Indecator.Click += new EventHandler(this.Indecator_Click);
			this.pictureBoxBill.Cursor = Cursors.Default;
			this.pictureBoxBill.Image = (Image)componentResourceManager.GetObject("pictureBoxBill.Image");
			this.pictureBoxBill.Location = new Point(215, 0);
			this.pictureBoxBill.Margin = new Padding(0);
			this.pictureBoxBill.Name = "pictureBoxBill";
			this.pictureBoxBill.Size = new Size(35, 35);
			this.pictureBoxBill.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBoxBill.TabIndex = 46;
			this.pictureBoxBill.TabStop = false;
			this.pictureBoxBill.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.pictureBoxBill, "成交明细");
			this.pictureBoxBill.Click += new EventHandler(this.pictureBoxBill_Click);
			this.refreshBt.BackColor = Color.Black;
			this.refreshBt.Cursor = Cursors.Default;
			this.refreshBt.Image = (Image)componentResourceManager.GetObject("refreshBt.Image");
			this.refreshBt.Location = new Point(75, 0);
			this.refreshBt.Margin = new Padding(0);
			this.refreshBt.Name = "refreshBt";
			this.refreshBt.Size = new Size(35, 35);
			this.refreshBt.SizeMode = PictureBoxSizeMode.CenterImage;
			this.refreshBt.TabIndex = 45;
			this.refreshBt.TabStop = false;
			this.refreshBt.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.refreshBt, "刷新");
			this.refreshBt.Click += new EventHandler(this.refreshBt_Click);
			this.AnyMin.Cursor = Cursors.Default;
			this.AnyMin.Image = (Image)componentResourceManager.GetObject("AnyMin.Image");
			this.AnyMin.Location = new Point(715, 0);
			this.AnyMin.Margin = new Padding(0);
			this.AnyMin.Name = "AnyMin";
			this.AnyMin.Size = new Size(35, 35);
			this.AnyMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.AnyMin.TabIndex = 40;
			this.AnyMin.TabStop = false;
			this.AnyMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.AnyMin, "任意分钟K线图");
			this.AnyMin.Click += new EventHandler(this.image_Click);
			this.BackBtn.Cursor = Cursors.Default;
			this.BackBtn.Image = (Image)componentResourceManager.GetObject("BackBtn.Image");
			this.BackBtn.Location = new Point(5, 0);
			this.BackBtn.Margin = new Padding(0);
			this.BackBtn.Name = "BackBtn";
			this.BackBtn.Size = new Size(35, 35);
			this.BackBtn.SizeMode = PictureBoxSizeMode.CenterImage;
			this.BackBtn.TabIndex = 24;
			this.BackBtn.TabStop = false;
			this.BackBtn.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.BackBtn, "后退");
			this.BackBtn.Click += new EventHandler(this.BackBtn_Click);
			this.AnyDay.Cursor = Cursors.Default;
			this.AnyDay.Image = (Image)componentResourceManager.GetObject("AnyDay.Image");
			this.AnyDay.Location = new Point(390, 0);
			this.AnyDay.Margin = new Padding(0);
			this.AnyDay.Name = "AnyDay";
			this.AnyDay.Size = new Size(35, 35);
			this.AnyDay.SizeMode = PictureBoxSizeMode.CenterImage;
			this.AnyDay.TabIndex = 35;
			this.AnyDay.TabStop = false;
			this.AnyDay.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.AnyDay, "任意天K线图");
			this.AnyDay.Click += new EventHandler(this.image_Click);
			this.FifteenMin.Cursor = Cursors.Default;
			this.FifteenMin.Image = (Image)componentResourceManager.GetObject("FifteenMin.Image");
			this.FifteenMin.Location = new Point(540, 0);
			this.FifteenMin.Margin = new Padding(0);
			this.FifteenMin.Name = "FifteenMin";
			this.FifteenMin.Size = new Size(35, 35);
			this.FifteenMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.FifteenMin.TabIndex = 35;
			this.FifteenMin.TabStop = false;
			this.FifteenMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.FifteenMin, "15分钟K线图");
			this.FifteenMin.Click += new EventHandler(this.image_Click);
			this.KLineBtn.Cursor = Cursors.Default;
			this.KLineBtn.Image = (Image)componentResourceManager.GetObject("KLineBtn.Image");
			this.KLineBtn.Location = new Point(180, 0);
			this.KLineBtn.Margin = new Padding(0);
			this.KLineBtn.Name = "KLineBtn";
			this.KLineBtn.Size = new Size(35, 35);
			this.KLineBtn.SizeMode = PictureBoxSizeMode.CenterImage;
			this.KLineBtn.TabIndex = 30;
			this.KLineBtn.TabStop = false;
			this.KLineBtn.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.KLineBtn, "技术分析");
			this.KLineBtn.Click += new EventHandler(this.KLineBtn_Click);
			this.FourHr.Cursor = Cursors.Default;
			this.FourHr.Image = (Image)componentResourceManager.GetObject("FourHr.Image");
			this.FourHr.Location = new Point(680, 0);
			this.FourHr.Margin = new Padding(0);
			this.FourHr.Name = "FourHr";
			this.FourHr.Size = new Size(35, 35);
			this.FourHr.SizeMode = PictureBoxSizeMode.CenterImage;
			this.FourHr.TabIndex = 39;
			this.FourHr.TabStop = false;
			this.FourHr.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.FourHr, "240分钟K线图");
			this.FourHr.Click += new EventHandler(this.image_Click);
			this.Day.Cursor = Cursors.Default;
			this.Day.Image = (Image)componentResourceManager.GetObject("Day.Image");
			this.Day.Location = new Point(250, 0);
			this.Day.Margin = new Padding(0);
			this.Day.Name = "Day";
			this.Day.Size = new Size(35, 35);
			this.Day.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Day.TabIndex = 31;
			this.Day.TabStop = false;
			this.Day.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Day, "日线图");
			this.Day.Click += new EventHandler(this.image_Click);
			this.SearchBtn.Cursor = Cursors.Default;
			this.SearchBtn.Image = (Image)componentResourceManager.GetObject("SearchBtn.Image");
			this.SearchBtn.Location = new Point(750, 0);
			this.SearchBtn.Margin = new Padding(0);
			this.SearchBtn.Name = "SearchBtn";
			this.SearchBtn.Size = new Size(35, 35);
			this.SearchBtn.SizeMode = PictureBoxSizeMode.CenterImage;
			this.SearchBtn.TabIndex = 25;
			this.SearchBtn.TabStop = false;
			this.SearchBtn.Tag = "前进";
			this.SearchBtn.Visible = false;
			this.SearchBtn.Click += new EventHandler(this.KLineBtn_Click);
			this.FiveMin.Cursor = Cursors.Default;
			this.FiveMin.Image = (Image)componentResourceManager.GetObject("FiveMin.Image");
			this.FiveMin.Location = new Point(505, 0);
			this.FiveMin.Margin = new Padding(0);
			this.FiveMin.Name = "FiveMin";
			this.FiveMin.Size = new Size(35, 35);
			this.FiveMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.FiveMin.TabIndex = 34;
			this.FiveMin.TabStop = false;
			this.FiveMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.FiveMin, "5分钟K线图");
			this.FiveMin.Click += new EventHandler(this.image_Click);
			this.Quarter.Cursor = Cursors.Default;
			this.Quarter.Image = (Image)componentResourceManager.GetObject("Quarter.Image");
			this.Quarter.Location = new Point(355, 0);
			this.Quarter.Margin = new Padding(0);
			this.Quarter.Name = "Quarter";
			this.Quarter.Size = new Size(35, 35);
			this.Quarter.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Quarter.TabIndex = 34;
			this.Quarter.TabStop = false;
			this.Quarter.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Quarter, "季线图");
			this.Quarter.Click += new EventHandler(this.image_Click);
			this.ThirtyMin.Cursor = Cursors.Default;
			this.ThirtyMin.Image = (Image)componentResourceManager.GetObject("ThirtyMin.Image");
			this.ThirtyMin.Location = new Point(575, 0);
			this.ThirtyMin.Margin = new Padding(0);
			this.ThirtyMin.Name = "ThirtyMin";
			this.ThirtyMin.Size = new Size(35, 35);
			this.ThirtyMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.ThirtyMin.TabIndex = 36;
			this.ThirtyMin.TabStop = false;
			this.ThirtyMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.ThirtyMin, "30分钟K线图");
			this.ThirtyMin.Click += new EventHandler(this.image_Click);
			this.StartButton.BackColor = Color.Black;
			this.StartButton.Cursor = Cursors.Default;
			this.StartButton.Image = (Image)componentResourceManager.GetObject("StartButton.Image");
			this.StartButton.Location = new Point(40, 0);
			this.StartButton.Margin = new Padding(0);
			this.StartButton.Name = "StartButton";
			this.StartButton.Size = new Size(35, 35);
			this.StartButton.SizeMode = PictureBoxSizeMode.CenterImage;
			this.StartButton.TabIndex = 27;
			this.StartButton.TabStop = false;
			this.StartButton.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.StartButton, "起始页");
			this.StartButton.Click += new EventHandler(this.btRanking_Click);
			this.FSZSBtn.Cursor = Cursors.Default;
			this.FSZSBtn.Image = (Image)componentResourceManager.GetObject("FSZSBtn.Image");
			this.FSZSBtn.Location = new Point(145, 0);
			this.FSZSBtn.Margin = new Padding(0);
			this.FSZSBtn.Name = "FSZSBtn";
			this.FSZSBtn.Size = new Size(35, 35);
			this.FSZSBtn.SizeMode = PictureBoxSizeMode.CenterImage;
			this.FSZSBtn.TabIndex = 29;
			this.FSZSBtn.TabStop = false;
			this.FSZSBtn.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.FSZSBtn, "分时走势");
			this.FSZSBtn.Click += new EventHandler(this.FSZSBtn_Click);
			this.TwoHr.Cursor = Cursors.Default;
			this.TwoHr.Image = (Image)componentResourceManager.GetObject("TwoHr.Image");
			this.TwoHr.Location = new Point(645, 0);
			this.TwoHr.Margin = new Padding(0);
			this.TwoHr.Name = "TwoHr";
			this.TwoHr.Size = new Size(35, 35);
			this.TwoHr.SizeMode = PictureBoxSizeMode.CenterImage;
			this.TwoHr.TabIndex = 37;
			this.TwoHr.TabStop = false;
			this.TwoHr.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.TwoHr, "120分钟K线图");
			this.TwoHr.Click += new EventHandler(this.image_Click);
			this.Week.Cursor = Cursors.Default;
			this.Week.Image = (Image)componentResourceManager.GetObject("Week.Image");
			this.Week.Location = new Point(285, 0);
			this.Week.Margin = new Padding(0);
			this.Week.Name = "Week";
			this.Week.Size = new Size(35, 35);
			this.Week.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Week.TabIndex = 32;
			this.Week.TabStop = false;
			this.Week.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Week, "周线图");
			this.Week.Click += new EventHandler(this.image_Click);
			this.OneMin.Cursor = Cursors.Default;
			this.OneMin.Image = (Image)componentResourceManager.GetObject("OneMin.Image");
			this.OneMin.Location = new Point(435, 0);
			this.OneMin.Margin = new Padding(0);
			this.OneMin.Name = "OneMin";
			this.OneMin.Size = new Size(35, 35);
			this.OneMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.OneMin.TabIndex = 32;
			this.OneMin.TabStop = false;
			this.OneMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.OneMin, "1分钟K线图");
			this.OneMin.Click += new EventHandler(this.image_Click);
			this.ThreeMin.Cursor = Cursors.Default;
			this.ThreeMin.Image = (Image)componentResourceManager.GetObject("ThreeMin.Image");
			this.ThreeMin.Location = new Point(470, 0);
			this.ThreeMin.Margin = new Padding(0);
			this.ThreeMin.Name = "ThreeMin";
			this.ThreeMin.Size = new Size(35, 35);
			this.ThreeMin.SizeMode = PictureBoxSizeMode.CenterImage;
			this.ThreeMin.TabIndex = 33;
			this.ThreeMin.TabStop = false;
			this.ThreeMin.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.ThreeMin, "3分钟K线图");
			this.ThreeMin.Click += new EventHandler(this.image_Click);
			this.Hour.Cursor = Cursors.Arrow;
			this.Hour.Image = (Image)componentResourceManager.GetObject("Hour.Image");
			this.Hour.Location = new Point(610, 0);
			this.Hour.Margin = new Padding(0);
			this.Hour.Name = "Hour";
			this.Hour.Size = new Size(35, 35);
			this.Hour.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Hour.TabIndex = 38;
			this.Hour.TabStop = false;
			this.Hour.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Hour, "60分钟K线图");
			this.Hour.Click += new EventHandler(this.image_Click);
			this.Month.Cursor = Cursors.Default;
			this.Month.Image = (Image)componentResourceManager.GetObject("Month.Image");
			this.Month.Location = new Point(320, 0);
			this.Month.Margin = new Padding(0);
			this.Month.Name = "Month";
			this.Month.Size = new Size(35, 35);
			this.Month.SizeMode = PictureBoxSizeMode.CenterImage;
			this.Month.TabIndex = 33;
			this.Month.TabStop = false;
			this.Month.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.Month, "月线图");
			this.Month.Click += new EventHandler(this.image_Click);
			this.BJPMBtn.Cursor = Cursors.Default;
			this.BJPMBtn.Image = (Image)componentResourceManager.GetObject("BJPMBtn.Image");
			this.BJPMBtn.Location = new Point(110, 0);
			this.BJPMBtn.Margin = new Padding(0);
			this.BJPMBtn.Name = "BJPMBtn";
			this.BJPMBtn.Size = new Size(35, 35);
			this.BJPMBtn.SizeMode = PictureBoxSizeMode.CenterImage;
			this.BJPMBtn.TabIndex = 28;
			this.BJPMBtn.TabStop = false;
			this.BJPMBtn.Tag = "false";
			this.toolTipTopImage.SetToolTip(this.BJPMBtn, "报价排名");
			this.BJPMBtn.Click += new EventHandler(this.btRanking_Click);
			base.AutoScaleMode = AutoScaleMode.None;
			this.AutoScroll = true;
			base.AutoScrollMinSize = new Size(750, 350);
			this.BackColor = SystemColors.Control;
			base.ClientSize = new Size(989, 539);
			base.Controls.Add(this.panelMain);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "MainWindow";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "金网行情";
			base.FormClosing += new FormClosingEventHandler(this.MainWindow_FormClosing);
			base.FormClosed += new FormClosedEventHandler(this.MainWindow_FormClosed);
			base.Load += new EventHandler(this.MainWindow_Load);
			base.Shown += new EventHandler(this.MainWindow_Shown);
			((ISupportInitialize)this.pictureBoxConnect).EndInit();
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			this.panelLeftBtn.ResumeLayout(false);
			this.panelMain.ResumeLayout(false);
			this.panelMain.PerformLayout();
			this.imagePanel.ResumeLayout(false);
			((ISupportInitialize)this.Split).EndInit();
			((ISupportInitialize)this.pictureSet).EndInit();
			((ISupportInitialize)this.pictureF10).EndInit();
			((ISupportInitialize)this.pictureDown).EndInit();
			((ISupportInitialize)this.pictureUp).EndInit();
			((ISupportInitialize)this.Indecator).EndInit();
			((ISupportInitialize)this.pictureBoxBill).EndInit();
			((ISupportInitialize)this.refreshBt).EndInit();
			((ISupportInitialize)this.AnyMin).EndInit();
			((ISupportInitialize)this.BackBtn).EndInit();
			((ISupportInitialize)this.AnyDay).EndInit();
			((ISupportInitialize)this.FifteenMin).EndInit();
			((ISupportInitialize)this.KLineBtn).EndInit();
			((ISupportInitialize)this.FourHr).EndInit();
			((ISupportInitialize)this.Day).EndInit();
			((ISupportInitialize)this.SearchBtn).EndInit();
			((ISupportInitialize)this.FiveMin).EndInit();
			((ISupportInitialize)this.Quarter).EndInit();
			((ISupportInitialize)this.ThirtyMin).EndInit();
			((ISupportInitialize)this.StartButton).EndInit();
			((ISupportInitialize)this.FSZSBtn).EndInit();
			((ISupportInitialize)this.TwoHr).EndInit();
			((ISupportInitialize)this.Week).EndInit();
			((ISupportInitialize)this.OneMin).EndInit();
			((ISupportInitialize)this.ThreeMin).EndInit();
			((ISupportInitialize)this.Hour).EndInit();
			((ISupportInitialize)this.Month).EndInit();
			((ISupportInitialize)this.BJPMBtn).EndInit();
			base.ResumeLayout(false);
		}
	}
}
