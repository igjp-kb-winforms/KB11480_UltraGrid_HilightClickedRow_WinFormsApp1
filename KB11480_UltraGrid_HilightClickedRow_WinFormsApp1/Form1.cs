using Infragistics.Win.UltraWinGrid;
using System.Data;

namespace KB11480_UltraGrid_HilightClickedRow_WinFormsApp1;

public partial class Form1 : Form
{
    private DataTable _sampleDataItems;
    private Infragistics.Win.Appearance _selectedRowAppearance;
    private Infragistics.Win.Appearance _lowlightRowAppearance;

    public Form1()
    {
        InitializeComponent();

        this.ultraGrid1.InitializeLayout += UltraGrid1_InitializeLayout;
        this.ultraGrid1.AfterSelectChange += UltraGrid1_AfterSelectChange;

        this._sampleDataItems = GetData();

        // ハイライト用の Appearance
        this._selectedRowAppearance = new Infragistics.Win.Appearance()
        {
            ForeColor = Color.OrangeRed,
            BackColor = Color.White
        };

        // ローライト用の Appearance
        this._lowlightRowAppearance = new Infragistics.Win.Appearance()
        {
            ForeColor = Color.LightGray,
            BackColor = Color.White,
        };
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        this.ultraGrid1.DataSource = _sampleDataItems;
    }

    private DataTable GetData()
    {
        DataTable dt = new DataTable();

        var idColum = dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("FamilyeName", typeof(string));
        dt.Columns.Add("GivenName", typeof(string));
        dt.Columns.Add("Prefecture", typeof(string));
        dt.Columns.Add("City", typeof(string));

        dt.Rows.Add(new object[] { 1, "露木", "優海", "鳥取県", "倉吉市" });
        dt.Rows.Add(new object[] { 2, "川崎", "典大", "富山県", "富山市" });
        dt.Rows.Add(new object[] { 3, "高橋", "伊吹", "宮崎県", "宮崎市" });
        dt.Rows.Add(new object[] { 4, "板橋", "範明", "高知県", "吾川郡いの町" });
        dt.Rows.Add(new object[] { 5, "吉野", "幸治", "静岡県", "静岡市葵区" });
        dt.Rows.Add(new object[] { 6, "秋田", "正治", "長崎県", "西彼杵郡長与町" });
        dt.Rows.Add(new object[] { 7, "平田", "琴", "京都府", "京都市下京区" });
        dt.Rows.Add(new object[] { 8, "江藤", "正敏", "三重県", "鈴鹿市" });
        dt.Rows.Add(new object[] { 9, "滝本", "政一", "奈良県", "橿原市" });
        dt.Rows.Add(new object[] { 10, "岡本", "次夫", "沖縄県", "那覇市" });

        dt.PrimaryKey = new DataColumn[] { idColum };
        return dt;
    }

    private void UltraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
    {
        // セルクリック時の動作を行選択にする。
        // ※セルクリック時の既定の動作は編集モードに入る、です。
        // ※今回はクリックしたらその行を選択できるようにした方が操作性が良くなるので、この設定もしておきます。
        e.Layout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

        // https://jp.infragistics.com/help/winforms/wingrid-setting-cell-active-and-selected-appearance
        e.Layout.Override.ActiveAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False; // アクティブ時の外観変更を無効化
        e.Layout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.True;    // 選択時の外観変更を有効化
        e.Layout.Override.SelectedRowAppearance = this._selectedRowAppearance;  // 行選択時の外観を設定
    }

    // 選択操作もしくは非選択操作が起こった時のイベントハンドラーの実装
    // https://jp.infragistics.com/help/winforms/wingrid-working-with-selected-rows
    private void UltraGrid1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
    {
        // 選択行がある場合
        if (this.ultraGrid1.Selected.Rows.Count > 0)
        {
            // 行のデフォルトの外観をローライトの外観にする。
            // ※結果、選択行の外観についてはInitializeLayoutで指定しているので、
            // ※それ以外の行、つまり非選択行がローライトの外観になります。
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = this._lowlightRowAppearance;
        }
        // 選択行がない場合
        else
        {
            // 行のデフォルトの外観をクリアする。
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = null;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.ultraGrid1.Selected.Rows.Clear();
    }
}
