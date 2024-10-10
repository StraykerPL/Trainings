using Eto.Forms;

namespace EtoFormTraining
{
    public sealed class DefaultClickCommand : Command
    {
        public DefaultClickCommand(Form parent, string displayText)
        {
            MenuText = displayText;
            Executed += (sender, e) => MessageBox.Show(parent, "I was clicked!");
        }
    }
}