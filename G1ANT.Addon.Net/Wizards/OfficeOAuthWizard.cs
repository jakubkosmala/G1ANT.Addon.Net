using G1ANT.Language;
using System.Windows.Forms;

namespace G1ANT.Addon.Net.Wizards
{
    [Wizard(Menu = "Tools\\Wizards", Name = "MSOffice OAuth Generator", Tooltip = "Helps generating officeoauth structure for imap.openex command")]
    public class OfficeOAuthWizard : Wizard
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
                using (var wizard = new OfficeOAuthForm(mainForm))
                    wizard.ShowDialog();
            }
        }
    }               
}   
