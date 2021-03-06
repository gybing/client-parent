﻿// Decompiled with JetBrains decompiler
// Type: FuturesTrade.Gnnt.UI.Modules.Query.Trade
// Assembly: FuturesTrade, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5A5E55F8-98BB-430C-A129-1553E99BE7C3
// Assembly location: D:\test\长三角商品交易所(模拟)\Trader\Modules\MEBSModule\FuturesTrade.dll

using FuturesTrade.Gnnt.BLL.Manager;
using FuturesTrade.Gnnt.BLL.Query.QueryTradeOperation;
using FuturesTrade.Gnnt.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using TabTest;
using TPME.Log;

namespace FuturesTrade.Gnnt.UI.Modules.Query
{
    public class Trade : UserControl
    {
        private bool isFirstLoad = true;
        private TradeItemInfo tradeItemInfo = new TradeItemInfo();
        private OperationManager operationManager = OperationManager.GetInstance();
        private byte buttonFlag;
        private int pageNum;
        private bool isTradeHeaderLoad;
        private int dgTradeHeight;
        private int bindNavHeight;
        private Trade.FillTrade fillTrade;
        private IContainer components;
        private GroupBox groupBoxTrade;
        private MyCombobox comboB_S;
        private Label labelB_S;
        internal DataGridView dgTrade;
        internal MyCombobox comboTrancF3;
        internal MyCombobox comboCommodityF3;
        internal Label labelTrancF3;
        private Label labelCommodityF3;
        private MyButton buttonSelF3;
        private GroupBox groupBoxF3_1;
        private MyRadioButton radioOF3;
        private MyRadioButton radioLF3;
        private MyRadioButton radioAllF3;
        internal BindingNavigator bindNavTrade;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton tSBtnTradeFirst;
        private ToolStripButton tSBtnTradePrevious;
        private ToolStripLabel tSLblTradePage;
        private ToolStripButton tSBtnTradeNext;
        private ToolStripButton tSBtnTradeLast;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel tSLblTradeNum;
        private ToolStripTextBox tSTbxTradeCurPage;
        private ToolStripLabel tSLblTradeP;
        private ToolStripButton tSBtnTradeGO;

        public Trade()
        {
            this.InitializeComponent();
            this.operationManager.queryTradeOperation.TradeFill = new QueryTradeOperation.TradeFillCallBack(this.TradeInfoFill);
            this.operationManager.SetComboCommodityEvent += new OperationManager.SetComboCommodityCallBack(this.SetComboCommodityIDList);
            this.CreateHandle();
        }

        private void TradeInfoFill(DataTable dTable, bool isPage)
        {
            try
            {
                this.fillTrade = new FillTrade(this.dsTradeFill);
                this.HandleCreated();
                base.Invoke(this.fillTrade, new object[] { dTable, isPage });
            }
            catch (Exception exception)
            {
                Logger.wirte(MsgType.Error, exception.StackTrace + exception.Message);
            }

        }

        private void dsTradeFill(DataTable dt, bool isShowPagingControl)
        {
            this.dgTrade.DataSource = (object)dt;
            this.SetDataGridViewHeader();
            this.SetBindNavLayOut(isShowPagingControl);
        }

        private void SetDataGridViewHeader()
        {
            if (this.isTradeHeaderLoad)
                return;
            for (int index = 0; index < this.dgTrade.Columns.Count; ++index)
            {
                ColItemInfo colItemInfo = (ColItemInfo)this.tradeItemInfo.m_htItemInfo[(object)this.dgTrade.Columns[index].Name];
                if (colItemInfo != null)
                {
                    this.dgTrade.Columns[index].MinimumWidth = colItemInfo.width;
                    this.dgTrade.Columns[index].FillWeight = (float)colItemInfo.width;
                    this.dgTrade.Columns[index].HeaderText = colItemInfo.name;
                    this.dgTrade.Columns[index].DefaultCellStyle.Format = colItemInfo.format;
                    this.dgTrade.Columns[index].DefaultCellStyle.FormatProvider = (IFormatProvider)Global.MyNumberFormatInfo;
                    this.dgTrade.Columns[index].SortMode = colItemInfo.sortID != 1 ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
                    if (!this.tradeItemInfo.m_strItems.Contains(this.dgTrade.Columns[index].Name))
                        this.dgTrade.Columns[index].Visible = false;
                    if (index == 0)
                        this.dgTrade.Columns[index].ReadOnly = false;
                    else
                        this.dgTrade.Columns[index].ReadOnly = true;
                }
            }
            this.isTradeHeaderLoad = true;
        }

        public void HandleCreated()
        {
            while (!this.IsHandleCreated)
                Thread.Sleep(100);
        }

        private void SetBindNavLayOut(bool isShowPagingControl)
        {
            if (isShowPagingControl)
            {
                if (!this.bindNavTrade.Visible)
                {
                    this.bindNavTrade.Visible = true;
                    this.dgTrade.Height = this.dgTradeHeight - this.bindNavHeight;
                }
            }
            else if (this.bindNavTrade.Visible)
            {
                this.bindNavTrade.Visible = false;
                this.dgTrade.Height = this.dgTradeHeight;
            }
            this.tSLblTradePage.Text = this.operationManager.queryTradeOperation.TradeCurrentPage.ToString() + "/" + this.operationManager.queryTradeOperation.TradeAllPage.ToString();
        }

        private void AllOrderSetEnable(bool isEnable)
        {
            if (this.operationManager.queryTradeOperation.TradeCurrentPage == 1)
            {
                this.tSBtnTradeFirst.Enabled = !isEnable;
                this.tSBtnTradePrevious.Enabled = !isEnable;
                this.tSBtnTradeNext.Enabled = isEnable;
                this.tSBtnTradeLast.Enabled = isEnable;
            }
            else if (this.operationManager.queryTradeOperation.TradeCurrentPage == this.operationManager.queryTradeOperation.TradeAllPage)
            {
                this.tSBtnTradeFirst.Enabled = isEnable;
                this.tSBtnTradePrevious.Enabled = isEnable;
                this.tSBtnTradeNext.Enabled = !isEnable;
                this.tSBtnTradeLast.Enabled = !isEnable;
            }
            else
            {
                this.tSBtnTradeFirst.Enabled = isEnable;
                this.tSBtnTradePrevious.Enabled = isEnable;
                this.tSBtnTradeNext.Enabled = isEnable;
                this.tSBtnTradeLast.Enabled = isEnable;
            }
        }

        private void tSBtnTradeFirst_Click(object sender, EventArgs e)
        {
            this.buttonFlag = (byte)0;
            this.QueryPagingTrade();
        }

        private void tSBtnTradePrevious_Click(object sender, EventArgs e)
        {
            this.buttonFlag = (byte)1;
            this.QueryPagingTrade();
        }

        private void tSBtnTradeNext_Click(object sender, EventArgs e)
        {
            this.buttonFlag = (byte)2;
            this.QueryPagingTrade();
        }

        private void tSBtnTradeLast_Click(object sender, EventArgs e)
        {
            this.buttonFlag = (byte)3;
            this.QueryPagingTrade();
        }

        private void tSBtnTradeGO_Click(object sender, EventArgs e)
        {
            if (this.tSTbxTradeCurPage.Text.Trim().Length == 0)
            {
                int num = (int)MessageBox.Show(this.operationManager.InputPageNum, this.operationManager.Prompt, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.tSTbxTradeCurPage.Focus();
            }
            else
            {
                int num1 = int.Parse(this.tSTbxTradeCurPage.Text.Trim());
                if (num1 > 0)
                {
                    if (num1 == this.operationManager.queryTradeOperation.TradeCurrentPage)
                        return;
                    if (num1 <= this.operationManager.queryTradeOperation.TradeAllPage)
                    {
                        this.buttonFlag = (byte)4;
                        this.pageNum = num1;
                        this.QueryPagingTrade();
                    }
                    else
                    {
                        int num2 = (int)MessageBox.Show(this.operationManager.InputPageNum, this.operationManager.PageNumError, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        this.tSTbxTradeCurPage.Focus();
                        this.tSTbxTradeCurPage.SelectAll();
                    }
                }
                else
                {
                    int num2 = (int)MessageBox.Show(this.operationManager.InputPageNum, this.operationManager.PageNumError, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.tSTbxTradeCurPage.Focus();
                    this.tSTbxTradeCurPage.SelectAll();
                }
            }
        }

        private void QueryPagingTrade()
        {
            this.operationManager.queryTradeOperation.QueryPageTradeData(this.buttonFlag, this.pageNum);
            this.AllOrderSetEnable(true);
            this.tSLblTradePage.Text = this.operationManager.queryTradeOperation.TradeCurrentPage.ToString() + "/" + this.operationManager.queryTradeOperation.TradeAllPage.ToString();
        }

        private void dgTrade_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.SetTradeTotalRowsColor();
            Global.BSAlign(this.dgTrade);
        }

        private void SetTradeTotalRowsColor()
        {
            if (this.dgTrade.RowCount > 1)
            {
                if (this.dgTrade.Rows[this.dgTrade.RowCount - 1].Cells[1].Value.ToString().Trim() == this.operationManager.Total)
                {
                    this.dgTrade.Rows[this.dgTrade.RowCount - 1].DefaultCellStyle.BackColor = Color.LightPink;
                    this.dgTrade.Rows[this.dgTrade.RowCount - 1].ReadOnly = true;
                }
            }
            try
            {
                this.dgTrade.Columns["AutoID"].Visible = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void QueryConditionChanged(object sender, EventArgs e)
        {
            if (this.isFirstLoad || sender != null && sender is MyRadioButton && !((MyRadioButton)sender).Checked)
                return;
            string _commodityID = string.Empty;
            string sql = this.OrderSql();
            if (this.comboCommodityF3.SelectedIndex != 0)
                _commodityID = this.comboCommodityF3.Text;
            short _buySellType = (short)this.comboB_S.SelectedIndex;
            short _se_f = !this.radioOF3.Checked ? (!this.radioLF3.Checked ? (short)0 : (short)2) : (short)1;
            this.operationManager.queryTradeOperation.ScreeningTradeData(_commodityID, _buySellType, _se_f, sql);
        }

        private string OrderSql()
        {
            string str1 = " 1=1 ";
            string str2 = " and TransactionsCode<>'' ";
            string str3 = " and B_S<>''";
            if (this.comboCommodityF3.SelectedIndex != 0 && this.comboCommodityF3.Items.Count != 0)
                str1 = str1 + " and CommodityID = '" + this.comboCommodityF3.Text + "' ";
            if (this.comboTrancF3.SelectedIndex != 0 && this.comboTrancF3.Items.Count != 0)
            {
                string str4 = " and TransactionsCode= '" + this.comboTrancF3.Text + "' ";
            }
            else
                str1 += str2;
            string str5 = this.comboB_S.SelectedIndex != 1 ? (this.comboB_S.SelectedIndex != 2 ? str1 + str3 : str1 + " and B_S='" + Global.BuySellStrArr[2] + "' ") : str1 + " and B_S='" + Global.BuySellStrArr[1] + "' ";
            if (this.radioOF3.Checked)
                str5 = str5 + " and O_L='" + Global.SettleBasisStrArr[1] + "' ";
            else if (this.radioLF3.Checked)
                str5 = str5 + " and O_L='" + Global.SettleBasisStrArr[2] + "' ";
            return str5;
        }

        private void Trade_SizeChanged(object sender, EventArgs e)
        {
            this.dgTradeHeight = this.dgTrade.Height;
            this.bindNavHeight = this.bindNavTrade.Height;
        }

        private void dgTrade_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.operationManager.queryTradeOperation.TradeDataGridViewSort(this.dgTrade.Columns[e.ColumnIndex].Name.ToString());
        }

        private void Trade_Load(object sender, EventArgs e)
        {
            this.dgTradeHeight = this.dgTrade.Height;
            this.bindNavHeight = this.bindNavTrade.Height;
            this.ComboLoad();
        }

        private void ComboLoad()
        {
            this.groupBoxTrade.Text = Global.M_ResourceManager.GetString("TradeStr_GroupBoxF3");
            this.labelCommodityF3.Text = Global.M_ResourceManager.GetString("TradeStr_CommodityCode");
            this.labelTrancF3.Text = Global.M_ResourceManager.GetString("TradeStr_TrancCode");
            this.labelB_S.Text = Global.M_ResourceManager.GetString("TradeStr_LabelB_S");
            this.radioAllF3.Text = Global.M_ResourceManager.GetString("TradeStr_RadioAllF3");
            this.radioOF3.Text = Global.M_ResourceManager.GetString("TradeStr_radioOF3");
            this.radioLF3.Text = Global.M_ResourceManager.GetString("TradeStr_RadioLF3");
            this.buttonSelF3.Text = Global.M_ResourceManager.GetString("TradeStr_ButtonSelF3");
            this.buttonSelF3.TextAlign = ContentAlignment.TopCenter;
            this.bindNavTrade.Visible = false;
            this.tSTbxTradeCurPage.MaxLength = 10;
            if (Global.CustomerCount < 2)
            {
                this.labelTrancF3.Visible = false;
                this.comboTrancF3.Visible = false;
                this.labelB_S.Location = new Point(this.labelB_S.Location.X - 169, this.labelB_S.Location.Y);
                this.comboB_S.Location = new Point(this.comboB_S.Location.X - 169, this.comboB_S.Location.Y);
                this.groupBoxF3_1.Location = new Point(this.groupBoxF3_1.Location.X - 169, this.groupBoxF3_1.Location.Y);
                this.buttonSelF3.Location = new Point(this.buttonSelF3.Location.X - 169, this.buttonSelF3.Location.Y);
            }
            this.radioAllF3.Checked = true;
            this.comboB_S.Items.Clear();
            this.comboB_S.Items.Add((object)this.operationManager.StrAll);
            this.comboB_S.Items.Add((object)this.operationManager.StrBuy);
            this.comboB_S.Items.Add((object)this.operationManager.StrSale);
            this.comboB_S.SelectedIndex = 0;
        }

        public void SetComboCommodityIDList(List<string> commodityIDList)
        {
            this.comboCommodityF3.Items.Clear();
            this.comboCommodityF3.Items.AddRange((object[])commodityIDList.ToArray());
            this.comboCommodityF3.SelectedIndex = 0;
            this.isFirstLoad = false;
        }

        private void buttonSelF3_Click(object sender, EventArgs e)
        {
            this.operationManager.queryTradeOperation.ButtonRefreshFlag = 1;
            this.operationManager.queryTradeOperation.QueryTradeInfoLoad();
            this.operationManager.IdleRefreshButton = 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Trade));
            DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
            this.groupBoxTrade = new GroupBox();
            this.bindNavTrade = new BindingNavigator(this.components);
            this.bindingNavigatorSeparator1 = new ToolStripSeparator();
            this.tSBtnTradeFirst = new ToolStripButton();
            this.tSBtnTradePrevious = new ToolStripButton();
            this.tSLblTradePage = new ToolStripLabel();
            this.tSBtnTradeNext = new ToolStripButton();
            this.tSBtnTradeLast = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tSLblTradeNum = new ToolStripLabel();
            this.tSTbxTradeCurPage = new ToolStripTextBox();
            this.tSLblTradeP = new ToolStripLabel();
            this.tSBtnTradeGO = new ToolStripButton();
            this.dgTrade = new DataGridView();
            this.comboB_S = new MyCombobox();
            this.labelB_S = new Label();
            this.comboTrancF3 = new MyCombobox();
            this.comboCommodityF3 = new MyCombobox();
            this.labelTrancF3 = new Label();
            this.labelCommodityF3 = new Label();
            this.buttonSelF3 = new MyButton();
            this.groupBoxF3_1 = new GroupBox();
            this.radioOF3 = new MyRadioButton();
            this.radioLF3 = new MyRadioButton();
            this.radioAllF3 = new MyRadioButton();
            this.groupBoxTrade.SuspendLayout();
            this.bindNavTrade.BeginInit();
            this.bindNavTrade.SuspendLayout();
            ((ISupportInitialize)this.dgTrade).BeginInit();
            this.groupBoxF3_1.SuspendLayout();
            this.SuspendLayout();
            this.groupBoxTrade.Controls.Add((Control)this.bindNavTrade);
            this.groupBoxTrade.Controls.Add((Control)this.dgTrade);
            this.groupBoxTrade.Controls.Add((Control)this.comboB_S);
            this.groupBoxTrade.Controls.Add((Control)this.labelB_S);
            this.groupBoxTrade.Controls.Add((Control)this.comboTrancF3);
            this.groupBoxTrade.Controls.Add((Control)this.comboCommodityF3);
            this.groupBoxTrade.Controls.Add((Control)this.labelTrancF3);
            this.groupBoxTrade.Controls.Add((Control)this.labelCommodityF3);
            this.groupBoxTrade.Controls.Add((Control)this.buttonSelF3);
            this.groupBoxTrade.Controls.Add((Control)this.groupBoxF3_1);
            this.groupBoxTrade.Dock = DockStyle.Fill;
            this.groupBoxTrade.Location = new Point(0, 0);
            this.groupBoxTrade.Name = "groupBoxTrade";
            this.groupBoxTrade.Size = new Size(720, 200);
            this.groupBoxTrade.TabIndex = 22;
            this.groupBoxTrade.TabStop = false;
            this.groupBoxTrade.Text = "成交查询";
            this.groupBoxTrade.BackColor = Color.FromArgb(235,235,235);
            //this.groupBoxTrade.ForeColor = Color.FromArgb(235, 235, 235);
            this.bindNavTrade.AddNewItem = (ToolStripItem)null;
            this.bindNavTrade.AutoSize = false;
            this.bindNavTrade.BackColor = Color.Gainsboro;
            this.bindNavTrade.CountItem = (ToolStripItem)null;
            this.bindNavTrade.DeleteItem = (ToolStripItem)null;
            this.bindNavTrade.Dock = DockStyle.Bottom;
            this.bindNavTrade.Items.AddRange(new ToolStripItem[11]
            {
        (ToolStripItem) this.bindingNavigatorSeparator1,
        (ToolStripItem) this.tSBtnTradeFirst,
        (ToolStripItem) this.tSBtnTradePrevious,
        (ToolStripItem) this.tSLblTradePage,
        (ToolStripItem) this.tSBtnTradeNext,
        (ToolStripItem) this.tSBtnTradeLast,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.tSLblTradeNum,
        (ToolStripItem) this.tSTbxTradeCurPage,
        (ToolStripItem) this.tSLblTradeP,
        (ToolStripItem) this.tSBtnTradeGO
            });
            this.bindNavTrade.LayoutStyle = ToolStripLayoutStyle.Flow;
            this.bindNavTrade.Location = new Point(3, 177);
            this.bindNavTrade.MoveFirstItem = (ToolStripItem)null;
            this.bindNavTrade.MoveLastItem = (ToolStripItem)null;
            this.bindNavTrade.MoveNextItem = (ToolStripItem)null;
            this.bindNavTrade.MovePreviousItem = (ToolStripItem)null;
            this.bindNavTrade.Name = "bindNavTrade";
            this.bindNavTrade.PositionItem = (ToolStripItem)null;
            this.bindNavTrade.RenderMode = ToolStripRenderMode.System;
            this.bindNavTrade.Size = new Size(694, 20);
            this.bindNavTrade.TabIndex = 29;
            this.bindNavTrade.Text = "bindingNavigator3";
            this.bindNavTrade.Visible = false;
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new Size(6, 23);
            this.tSBtnTradeFirst.AutoSize = false;
            this.tSBtnTradeFirst.BackgroundImage = (Image)componentResourceManager.GetObject("tSBtnTradeFirst.BackgroundImage");
            this.tSBtnTradeFirst.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tSBtnTradeFirst.ImageTransparentColor = Color.Magenta;
            this.tSBtnTradeFirst.Name = "tSBtnTradeFirst";
            this.tSBtnTradeFirst.Size = new Size(16, 16);
            this.tSBtnTradeFirst.Click += new EventHandler(this.tSBtnTradeFirst_Click);
            this.tSBtnTradePrevious.AutoSize = false;
            this.tSBtnTradePrevious.BackgroundImage = (Image)componentResourceManager.GetObject("tSBtnTradePrevious.BackgroundImage");
            this.tSBtnTradePrevious.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tSBtnTradePrevious.Image = (Image)componentResourceManager.GetObject("tSBtnTradePrevious.Image");
            this.tSBtnTradePrevious.ImageTransparentColor = Color.Magenta;
            this.tSBtnTradePrevious.Name = "tSBtnTradePrevious";
            this.tSBtnTradePrevious.Size = new Size(16, 16);
            this.tSBtnTradePrevious.Click += new EventHandler(this.tSBtnTradePrevious_Click);
            this.tSLblTradePage.AutoSize = false;
            this.tSLblTradePage.Name = "tSLblTradePage";
            this.tSLblTradePage.Size = new Size(95, 16);
            this.tSLblTradePage.Text = "Page/Total";
            this.tSBtnTradeNext.AutoSize = false;
            this.tSBtnTradeNext.BackgroundImage = (Image)componentResourceManager.GetObject("tSBtnTradeNext.BackgroundImage");
            this.tSBtnTradeNext.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tSBtnTradeNext.Image = (Image)componentResourceManager.GetObject("tSBtnTradeNext.Image");
            this.tSBtnTradeNext.ImageTransparentColor = Color.Magenta;
            this.tSBtnTradeNext.Name = "tSBtnTradeNext";
            this.tSBtnTradeNext.Size = new Size(16, 16);
            this.tSBtnTradeNext.Click += new EventHandler(this.tSBtnTradeNext_Click);
            this.tSBtnTradeLast.AutoSize = false;
            this.tSBtnTradeLast.BackgroundImage = (Image)componentResourceManager.GetObject("tSBtnTradeLast.BackgroundImage");
            this.tSBtnTradeLast.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tSBtnTradeLast.Image = (Image)componentResourceManager.GetObject("tSBtnTradeLast.Image");
            this.tSBtnTradeLast.ImageTransparentColor = Color.Magenta;
            this.tSBtnTradeLast.Name = "tSBtnTradeLast";
            this.tSBtnTradeLast.Size = new Size(16, 16);
            this.tSBtnTradeLast.Click += new EventHandler(this.tSBtnTradeLast_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 23);
            this.tSLblTradeNum.AutoSize = false;
            this.tSLblTradeNum.Name = "tSLblTradeNum";
            this.tSLblTradeNum.Size = new Size(17, 16);
            this.tSLblTradeNum.Text = "第";
            this.tSTbxTradeCurPage.AutoSize = false;
            this.tSTbxTradeCurPage.BorderStyle = BorderStyle.None;
            this.tSTbxTradeCurPage.Name = "tSTbxTradeCurPage";
            this.tSTbxTradeCurPage.Size = new Size(50, 16);
            this.tSTbxTradeCurPage.TextBoxTextAlign = HorizontalAlignment.Center;
            this.tSLblTradeP.AutoSize = false;
            this.tSLblTradeP.Name = "tSLblTradeP";
            this.tSLblTradeP.Size = new Size(17, 16);
            this.tSLblTradeP.Text = "页";
            this.tSBtnTradeGO.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.tSBtnTradeGO.Image = (Image)componentResourceManager.GetObject("tSBtnTradeGO.Image");
            this.tSBtnTradeGO.ImageTransparentColor = Color.Magenta;
            this.tSBtnTradeGO.Name = "tSBtnTradeGO";
            this.tSBtnTradeGO.Size = new Size(23, 16);
            this.tSBtnTradeGO.Text = "GO";
            this.tSBtnTradeGO.Click += new EventHandler(this.tSBtnTradeGO_Click);
            this.dgTrade.AllowUserToAddRows = false;
            this.dgTrade.AllowUserToDeleteRows = false;
            this.dgTrade.AllowUserToOrderColumns = true;
            this.dgTrade.AllowUserToResizeRows = false;
            this.dgTrade.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgTrade.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)134);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.dgTrade.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            this.dgTrade.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)134);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.dgTrade.DefaultCellStyle = gridViewCellStyle2;
            this.dgTrade.Location = new Point(6, 37);
            this.dgTrade.Name = "dgTrade";
            this.dgTrade.ReadOnly = true;
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)134);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            this.dgTrade.RowHeadersDefaultCellStyle = gridViewCellStyle3;
            this.dgTrade.RowHeadersVisible = false;
            this.dgTrade.RowTemplate.Height = 16;
            this.dgTrade.ScrollBars = ScrollBars.Vertical;
            this.dgTrade.Size = new Size(711, 157);
            this.dgTrade.TabIndex = 21;
            this.dgTrade.TabStop = false;
            this.dgTrade.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dgTrade_ColumnHeaderMouseClick);
            this.dgTrade.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dgTrade_DataBindingComplete);
            this.comboB_S.FormattingEnabled = true;
            this.comboB_S.Location = new Point(376, 13);
            this.comboB_S.Name = "comboB_S";
            this.comboB_S.Size = new Size(57, 20);
            this.comboB_S.TabIndex = 23;
            this.comboB_S.TabStop = false;
            this.comboB_S.SelectedIndexChanged += new EventHandler(this.QueryConditionChanged);
            this.labelB_S.AutoSize = true;
            this.labelB_S.ImeMode = ImeMode.NoControl;
            this.labelB_S.Location = new Point(333, 17);
            this.labelB_S.Name = "labelB_S";
            this.labelB_S.Size = new Size(35, 12);
            this.labelB_S.TabIndex = 22;
            this.labelB_S.Text = "买/卖";
            this.comboTrancF3.Location = new Point(236, 13);
            this.comboTrancF3.Name = "comboTrancF3";
            this.comboTrancF3.Size = new Size(88, 20);
            this.comboTrancF3.TabIndex = 20;
            this.comboTrancF3.TabStop = false;
            this.comboTrancF3.SelectedIndexChanged += new EventHandler(this.QueryConditionChanged);
            this.comboCommodityF3.Location = new Point(76, 13);
            this.comboCommodityF3.Name = "comboCommodityF3";
            this.comboCommodityF3.Size = new Size(80, 20);
            this.comboCommodityF3.TabIndex = 19;
            this.comboCommodityF3.TabStop = false;
            this.comboCommodityF3.SelectedIndexChanged += new EventHandler(this.QueryConditionChanged);
            this.labelTrancF3.ImeMode = ImeMode.NoControl;
            this.labelTrancF3.Location = new Point(162, 15);
            this.labelTrancF3.Name = "labelTrancF3";
            this.labelTrancF3.Size = new Size(72, 16);
            this.labelTrancF3.TabIndex = 18;
            this.labelTrancF3.Text = "交易代码：";
            this.labelTrancF3.TextAlign = ContentAlignment.MiddleCenter;
            this.labelCommodityF3.ImeMode = ImeMode.NoControl;
            this.labelCommodityF3.Location = new Point(4, 15);
            this.labelCommodityF3.Name = "labelCommodityF3";
            this.labelCommodityF3.Size = new Size(72, 16);
            this.labelCommodityF3.TabIndex = 17;
            this.labelCommodityF3.Text = "商品代码：";
            this.labelCommodityF3.TextAlign = ContentAlignment.MiddleCenter;
            this.buttonSelF3.ImeMode = ImeMode.NoControl;
            this.buttonSelF3.Location = new Point(624, 12);
            this.buttonSelF3.Name = "buttonSelF3";
            this.buttonSelF3.Size = new Size(57, 20);
            this.buttonSelF3.TabIndex = 14;
            this.buttonSelF3.TabStop = false;
            this.buttonSelF3.Text = "刷新";
            this.buttonSelF3.UseVisualStyleBackColor = true;
            this.buttonSelF3.Click += new EventHandler(this.buttonSelF3_Click);
            this.groupBoxF3_1.BackColor = Color.Transparent;
            this.groupBoxF3_1.Controls.Add((Control)this.radioOF3);
            this.groupBoxF3_1.Controls.Add((Control)this.radioLF3);
            this.groupBoxF3_1.Controls.Add((Control)this.radioAllF3);
            this.groupBoxF3_1.Location = new Point(444, 4);
            this.groupBoxF3_1.Name = "groupBoxF3_1";
            this.groupBoxF3_1.Size = new Size(172, 31);
            this.groupBoxF3_1.TabIndex = 13;
            this.groupBoxF3_1.TabStop = false;
            this.radioOF3.AutoSize = true;
            this.radioOF3.ImeMode = ImeMode.NoControl;
            this.radioOF3.Location = new Point(62, 11);
            this.radioOF3.Name = "radioOF3";
            this.radioOF3.Size = new Size(47, 16);
            this.radioOF3.TabIndex = 1;
            this.radioOF3.Text = "订立";
            this.radioOF3.UseVisualStyleBackColor = true;
            this.radioOF3.CheckedChanged += new EventHandler(this.QueryConditionChanged);
            this.radioLF3.AutoSize = true;
            this.radioLF3.ImeMode = ImeMode.NoControl;
            this.radioLF3.Location = new Point(119, 11);
            this.radioLF3.Name = "radioLF3";
            this.radioLF3.Size = new Size(47, 16);
            this.radioLF3.TabIndex = 1;
            this.radioLF3.Text = "转让";
            this.radioLF3.UseVisualStyleBackColor = true;
            this.radioLF3.CheckedChanged += new EventHandler(this.QueryConditionChanged);
            this.radioAllF3.AutoSize = true;
            this.radioAllF3.Checked = true;
            this.radioAllF3.ImeMode = ImeMode.NoControl;
            this.radioAllF3.Location = new Point(6, 11);
            this.radioAllF3.Name = "radioAllF3";
            this.radioAllF3.Size = new Size(47, 16);
            this.radioAllF3.TabIndex = 0;
            this.radioAllF3.Text = "全部";
            this.radioAllF3.UseVisualStyleBackColor = true;
            this.radioAllF3.CheckedChanged += new EventHandler(this.QueryConditionChanged);
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add((Control)this.groupBoxTrade);
            this.Margin = new Padding(0);
            this.Name = "Trade";
            this.Size = new Size(720, 200);
            this.Load += new EventHandler(this.Trade_Load);
            this.SizeChanged += new EventHandler(this.Trade_SizeChanged);
            this.groupBoxTrade.ResumeLayout(false);
            this.groupBoxTrade.PerformLayout();
            this.bindNavTrade.EndInit();
            this.bindNavTrade.ResumeLayout(false);
            this.bindNavTrade.PerformLayout();
            ((ISupportInitialize)this.dgTrade).EndInit();
            this.groupBoxF3_1.ResumeLayout(false);
            this.groupBoxF3_1.PerformLayout();
            this.ResumeLayout(false);
        }

        private delegate void FillTrade(DataTable dt, bool isShowPagingControl);
    }
}
