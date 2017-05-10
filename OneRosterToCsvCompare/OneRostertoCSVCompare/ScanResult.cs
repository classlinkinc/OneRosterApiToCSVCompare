using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OneRostertoCSVCompare
{
    public partial class ScanResult : Form
    {
        public ScanResult(DataTable resultDataTable)
        {
            InitializeComponent();

            resultLimit.Items.AddRange(new object[]
            {
                10,
                25,
                50,
                100,
                200
            });

            resultListView.View = View.Details;
            resultListView.Items.Clear();
            resultListView.Columns.Clear();
            resultListView.GridLines = true;
            resultListView.Columns.Add("Id", -2, HorizontalAlignment.Left);
            resultListView.Columns.Add("SourcedId", -2, HorizontalAlignment.Left);
            resultListView.Columns.Add("Status", -2, HorizontalAlignment.Left);
            _data = resultDataTable.Copy();

            resultLimit.SelectedIndex = 0;
            _limit = int.Parse(resultLimit.Text);
            Populate();
        }

        private int _limit;
        private int _page = 1;
        private int _totalPages;
        private readonly DataTable _data;

        private void Populate()
        {
            resultListView.Items.Clear();

            var data = _data.AsEnumerable().Skip(_limit * (_page - 1)).Take(_limit).Select(
                c => new ListViewItem(new[]
                {
                    c.Field<string>("id"),
                    c.Field<string>("sourcedId"),
                    c.Field<string>("status")
                }));
            foreach (ListViewItem d in data)
            {
                resultListView.Items.Add(d);
            }
            page.Text = $@"Page {_page} of {_totalPages}";
            SetButtons();
        }

        private void SetButtons()
        {
            nextPage.Enabled = true;
            prevPage.Enabled = true;
            if (_page == _totalPages)
                nextPage.Enabled = false;
            if (_page == 1)
                prevPage.Enabled = false;
        }

        private void nextPage_Click(object sender, System.EventArgs e)
        {
            _page++;
            Populate();
        }

        private void prevPage_Click(object sender, System.EventArgs e)
        {
            _page--;
            Populate();
        }

        private void limit_Change(object sender, System.EventArgs e)
        {
            _limit = int.Parse(resultLimit.Text);
            _totalPages = _data.Rows.Count / _limit + 1;
            Populate();
        }
    }
}
