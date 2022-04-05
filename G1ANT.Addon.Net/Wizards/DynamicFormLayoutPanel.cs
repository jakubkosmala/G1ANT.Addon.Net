using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace G1ANT.Addon.Net.Wizards
{
    public partial class DynamicFormLayoutPanel : TableLayoutPanel
    {
        private const string dummyControlName = "42036F9B-ADF5-4E57-A49D-1008992CB8A2";
        public List<FormControlInfo> Items { get; } = new List<FormControlInfo>();

        public DynamicFormLayoutPanel()
        {
            InitializeComponent();
        }

        public DynamicFormLayoutPanel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        protected override ControlCollection CreateControlsInstance()
        {
            foreach (ColumnStyle column in ColumnStyles)
                column.SizeType = SizeType.AutoSize;
            return base.CreateControlsInstance();
        }

        private Label CreateLabel(string text)
        {
            return new Label()
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleRight,
                AutoSize = true
            };
        }

        public void AddRow(FormControlInfo controlInfo)
        {
            Controls.RemoveByKey(dummyControlName);
            Controls.Add(CreateLabel(controlInfo.Label), 0, Items.Count);
            controlInfo.Control.Dock = DockStyle.Fill;
            Controls.Add(controlInfo.Control, 1, Items.Count);
            RowStyles[Items.Count].SizeType = SizeType.AutoSize;

            Items.Add(controlInfo);

            if (RowStyles.Count == Items.Count)
            {
                RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                var dummyLabel = CreateLabel("");
                dummyLabel.Name = dummyControlName;
                Controls.Add(dummyLabel, 0, Items.Count);
            }
        }
    }
}
