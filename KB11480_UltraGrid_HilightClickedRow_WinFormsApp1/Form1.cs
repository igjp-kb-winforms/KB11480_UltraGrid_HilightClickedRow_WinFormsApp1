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

        // �n�C���C�g�p�� Appearance
        this._selectedRowAppearance = new Infragistics.Win.Appearance()
        {
            ForeColor = Color.OrangeRed,
            BackColor = Color.White
        };

        // ���[���C�g�p�� Appearance
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

        dt.Rows.Add(new object[] { 1, "�I��", "�D�C", "���挧", "�q�g�s" });
        dt.Rows.Add(new object[] { 2, "���", "�T��", "�x�R��", "�x�R�s" });
        dt.Rows.Add(new object[] { 3, "����", "�ɐ�", "�{�茧", "�{��s" });
        dt.Rows.Add(new object[] { 4, "��", "�͖�", "���m��", "���S���̒�" });
        dt.Rows.Add(new object[] { 5, "�g��", "�K��", "�É���", "�É��s����" });
        dt.Rows.Add(new object[] { 6, "�H�c", "����", "���茧", "���ދn�S���^��" });
        dt.Rows.Add(new object[] { 7, "���c", "��", "���s�{", "���s�s������" });
        dt.Rows.Add(new object[] { 8, "�]��", "���q", "�O�d��", "�鎭�s" });
        dt.Rows.Add(new object[] { 9, "��{", "����", "�ޗǌ�", "�����s" });
        dt.Rows.Add(new object[] { 10, "���{", "���v", "���ꌧ", "�ߔe�s" });

        dt.PrimaryKey = new DataColumn[] { idColum };
        return dt;
    }

    private void UltraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
    {
        // �Z���N���b�N���̓�����s�I���ɂ���B
        // ���Z���N���b�N���̊���̓���͕ҏW���[�h�ɓ���A�ł��B
        // ������̓N���b�N�����炻�̍s��I���ł���悤�ɂ����������쐫���ǂ��Ȃ�̂ŁA���̐ݒ�����Ă����܂��B
        e.Layout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;

        // https://jp.infragistics.com/help/winforms/wingrid-setting-cell-active-and-selected-appearance
        e.Layout.Override.ActiveAppearancesEnabled = Infragistics.Win.DefaultableBoolean.False; // �A�N�e�B�u���̊O�ϕύX�𖳌���
        e.Layout.Override.SelectedAppearancesEnabled = Infragistics.Win.DefaultableBoolean.True;    // �I�����̊O�ϕύX��L����
        e.Layout.Override.SelectedRowAppearance = this._selectedRowAppearance;  // �s�I�����̊O�ς�ݒ�
    }

    // �I�𑀍�������͔�I�𑀍삪�N���������̃C�x���g�n���h���[�̎���
    // https://jp.infragistics.com/help/winforms/wingrid-working-with-selected-rows
    private void UltraGrid1_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
    {
        // �I���s������ꍇ
        if (this.ultraGrid1.Selected.Rows.Count > 0)
        {
            // �s�̃f�t�H���g�̊O�ς����[���C�g�̊O�ςɂ���B
            // �����ʁA�I���s�̊O�ςɂ��Ă�InitializeLayout�Ŏw�肵�Ă���̂ŁA
            // ������ȊO�̍s�A�܂��I���s�����[���C�g�̊O�ςɂȂ�܂��B
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = this._lowlightRowAppearance;
        }
        // �I���s���Ȃ��ꍇ
        else
        {
            // �s�̃f�t�H���g�̊O�ς��N���A����B
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = null;
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.ultraGrid1.Selected.Rows.Clear();
    }
}
