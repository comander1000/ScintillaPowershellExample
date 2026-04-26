using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScintillaPowershellExample
{
    public partial class frmPowershellEditor : Form
    {
        private static readonly string[] PowerShellWords =
    {
        "Get-ChildItem",
        "Get-Content",
        "Set-Content",
        "Get-Process",
        "Stop-Process",
        "Start-Process",
        "Get-Service",
        "Start-Service",
        "Stop-Service",
        "Where-Object",
        "ForEach-Object",
        "Select-Object",
        "Sort-Object",
        "Write-Host",
        "Write-Output",
        "Import-Module",
        "Export-Csv",
        "Import-Csv",
        "New-Item",
        "Remove-Item",
        "Copy-Item",
        "Move-Item",
        "Test-Path",
        "param",
        "function",
        "if",
        "else",
        "elseif",
        "foreach",
        "while",
        "switch",
        "return",
        "$true",
        "$false",
        "$null"
    };

        public frmPowershellEditor()
        {
            InitializeComponent();

            ConfigureEditor();
            ConfigureAutocomplete();
        }

        private void ConfigureEditor()
        {
            scintillaBox.Font = new Font("Consolas", 10);
            scintillaBox.WrapMode = WrapMode.None;
            scintillaBox.IndentationGuides = IndentView.LookBoth;

            scintillaBox.Margins[0].Type = MarginType.Number;
            scintillaBox.Margins[0].Width = 40;

            scintillaBox.TabWidth = 4;
            scintillaBox.UseTabs = false;

            // PowerShell lexer
            scintillaBox.Lexer = Lexer.PowerShell;

            scintillaBox.StyleResetDefault();
            scintillaBox.Styles[Style.Default].Font = "Consolas";
            scintillaBox.Styles[Style.Default].Size = 10;
            scintillaBox.StyleClearAll();

            scintillaBox.Styles[Style.LineNumber].ForeColor = Color.Gray;
            scintillaBox.Styles[Style.LineNumber].BackColor = Color.FromArgb(245, 245, 245);
        }

        private void ConfigureAutocomplete()
        {
            scintillaBox.AutoCIgnoreCase = true;
            scintillaBox.AutoCAutoHide = true;
            scintillaBox.AutoCCancelAtStart = true;
            scintillaBox.AutoCChooseSingle = false;

            scintillaBox.CharAdded += (_, e) =>
            {
                char typedChar = (char)e.Char;

                if (!char.IsLetterOrDigit(typedChar) &&
                    typedChar != '-' &&
                    typedChar != '_' &&
                    typedChar != '$')
                {
                    return;
                }

                string currentWord = GetCurrentWord();

                if (currentWord.Length < 2)
                    return;

                var matches = PowerShellWords
                    .Where(w => w.StartsWith(currentWord, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(w => w)
                    .ToArray();

                if (matches.Length == 0)
                    return;

                scintillaBox.AutoCShow(currentWord.Length, string.Join(" ", matches));
            };
        }

        private string GetCurrentWord()
        {
            int currentPosition = scintillaBox.CurrentPosition;
            int startPosition = currentPosition;

            while (startPosition > 0)
            {
                char ch = (char)scintillaBox.GetCharAt(startPosition - 1);

                if (!char.IsLetterOrDigit(ch) &&
                    ch != '-' &&
                    ch != '_' &&
                    ch != '$')
                {
                    break;
                }

                startPosition--;
            }

            return scintillaBox.GetTextRange(startPosition, currentPosition - startPosition);
        }

        private void ConfigurePowerShellAutoComplete()
        {
            scintillaBox.AutoCIgnoreCase = true;
            scintillaBox.AutoCAutoHide = true;
            scintillaBox.AutoCChooseSingle = false;

            scintillaBox.CharAdded += ScintillaBox_CharAdded;
        }

        private void ScintillaBox_CharAdded(object sender, CharAddedEventArgs e)
        {
            char typedChar = (char)e.Char;

            // Do not show autocomplete after whitespace or punctuation.
            if (!IsPowerShellTokenChar(typedChar))
            {
                if (scintillaBox.AutoCActive)
                    scintillaBox.AutoCCancel();

                return;
            }

            string currentToken = GetCurrentPowerShellToken();

            // Show popup after first typed character.
            if (currentToken.Length < 1)
                return;

            string[] matches = PowerShellWords
                .Where(word => word.StartsWith(currentToken, StringComparison.OrdinalIgnoreCase))
                .OrderBy(word => word)
                .ToArray();

            if (matches.Length == 0)
            {
                if (scintillaBox.AutoCActive)
                    scintillaBox.AutoCCancel();

                return;
            }

            // Refresh popup while user types.
            if (scintillaBox.AutoCActive)
                scintillaBox.AutoCCancel();

            scintillaBox.AutoCShow(currentToken.Length, string.Join(" ", matches));
        }

        private string GetCurrentPowerShellToken()
        {
            int currentPosition = scintillaBox.CurrentPosition;
            int startPosition = currentPosition;

            while (startPosition > 0)
            {
                char ch = (char)scintillaBox.GetCharAt(startPosition - 1);

                if (!IsPowerShellTokenChar(ch))
                    break;

                startPosition--;
            }

            return scintillaBox.GetTextRange(startPosition, currentPosition - startPosition);
        }

        private bool IsPowerShellTokenChar(char ch)
        {
            return char.IsLetterOrDigit(ch)
                || ch == '-'
                || ch == '_'
                || ch == '$';
        }
    }
}
