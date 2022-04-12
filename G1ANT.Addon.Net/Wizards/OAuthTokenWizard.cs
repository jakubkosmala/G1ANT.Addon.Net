using G1ANT.Language;
using System.Windows.Forms;

namespace G1ANT.Addon.Net.Wizards
{
    [Wizard(Menu = "Tools\\Wizards", Name = "OAuth Generator", Tooltip = "Helps generating oauth structure for imap.openex command")]
    public class OAuthTokenWizard : Wizard
    {
        private IMainForm FindMainForm()
        {
            foreach (var form in Application.OpenForms)
            {
                if (form is IMainForm iForm)
                {
                    return iForm;
                }
            }
            return null;
        }

        public override void Execute(AbstractScripter scripter)
        {
            var mainForm = FindMainForm();
            if (mainForm != null)
            {
                using (var wizard = new OAuthTokenForm(mainForm))
                    wizard.ShowDialog();
            }
        }
    }               
}   
