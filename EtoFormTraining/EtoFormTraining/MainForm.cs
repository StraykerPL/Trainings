using Eto.Drawing;
using Eto.Forms;
using System.Threading;

namespace EtoFormTraining
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            var hostedService = ApplicationServiceProvider.GetService<MainHostedService>();
            hostedService.StartAsync(new CancellationToken()).ConfigureAwait(false);

            var buttonCommand = new DefaultClickCommand(this, "Click me!");
            Title = "My Eto Form";
            MinimumSize = new Size(450, 450);

            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            var ok = "File";
            Menu = new MenuBar
            {
                Items =
                {
                    new SubMenuItem { Text = $"&{ok}", Items = { buttonCommand } },
                    new ButtonMenuItem { Text = ok, Items = { buttonCommand } }
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };

            var layout = new TableLayout
            {
                Spacing = new Size(5, 5),
                Padding = new Padding(10)
            };

            layout.Rows.Add(new TableRow(
                new TableCell(new Label { Text = "First Column" }, true),
                new TableCell(new Label { Text = "Second Column" }, true),
                new Label { Text = "Third Column" }
            ));

            layout.Rows.Add(new TableRow(
                new TextBox { Text = "Some Text" },
                new DropDown { Items = { "Item 1", "Item 2", "Item 3" } },
                new CheckBox { Text = "A checkbox" }
            ));

            layout.Rows.Add(new StackLayout
            {
                Padding = 10,
                Items =
                {
                    "Hello World!",
                    new Button { Command = buttonCommand, Text = "Click Here" },
                }
            });

            layout.Rows.Add(null);

            Content = layout;
        }
    }
}