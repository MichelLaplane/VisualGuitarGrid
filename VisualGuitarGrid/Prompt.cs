using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisualGuitarGrid
{
    /// <summary>
    /// Simple modal input prompt used by the ChordGrid editor to edit cell contents.
    /// Returns the entered text, or null if cancelled.
    /// </summary>
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            using var form = new Form();
            form.Text = caption ?? "Input";
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.ClientSize = new Size(600, 150);
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.ShowIcon = false;
            form.ShowInTaskbar = false;

            var label = new Label()
            {
                Left = 12,
                Top = 12,
                Width = form.ClientSize.Width - 24,
                Height = 36,
                Text = text ?? string.Empty,
                AutoEllipsis = true
            };

            var textBox = new TextBox()
            {
                Left = 12,
                Top = label.Bottom + 6,
                Width = form.ClientSize.Width - 24,
                Text = defaultValue ?? string.Empty
            };

            var okButton = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = form.ClientSize.Width - 200,
                Width = 80,
                Height = 34,
              Top = textBox.Bottom + 12,
              UseVisualStyleBackColor = true
            };

            var cancelButton = new Button()
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = form.ClientSize.Width - 100,
                Width = 80,
                Height = 34,
              Top = textBox.Bottom + 12,
              UseVisualStyleBackColor = true

            };

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);

            form.AcceptButton = okButton;
            form.CancelButton = cancelButton;

            var dr = form.ShowDialog();
            return dr == DialogResult.OK ? textBox.Text : null;
        }
    }
}